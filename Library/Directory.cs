using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Recipe.Library
{
    public class Directory : ICloneable
    {
        public string Name { get; set; }
        public List<Directory> Directories { get; set; }
        public List<Item> Items { get; set; }
        [JsonIgnore]
        public Directory Parent { get; set; }

        
        public Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent;

            Items = new List<Item>();
            Directories = new List<Directory>();
        }

        /**/

        public void GenerateRelations()
        {
            Relations(this);
        }

        public void CreateDirectory(string path)
        {
            var lpath = DeserializePath(path);
            int level = 0;
            Directory directory = this;


            while (level < lpath.Count)
            {
                bool found = false;
                foreach (Directory dir in directory.Directories)
                {
                    if (dir.Name == lpath[level])
                    {
                        directory = dir;
                        level++;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    directory.Directories.Add(new Directory(lpath[level], directory));
                    directory = directory.Directories.Last();
                    level++;
                }
            }
        }

        public void DeleteDirectory(string path)
        {
            var directory = GetDirectory(path);
            var parent = directory.Parent;
            parent.Directories.Remove(directory);
        }

        public static void DeleteDirectory(Directory directory)
        {
            var parent = directory.Parent;
            parent.Directories.Remove(directory);
        }

        public Directory GetDirectory(string path)
        {
            var lpath = DeserializePath(path);
            int level = 0;
            Directory directory = this;

            while (level < lpath.Count)
            {
                bool found = false;
                foreach (Directory dir in directory.Directories)
                {
                    if (dir.Name == lpath[level])
                    {
                        directory = dir;
                        level++;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return null;
                }
            }

            return directory;
        }

        public void CreateItem(string path, Item item)
        {
            var dir = GetDirectory(path);

            dir.Items.Add(item);
        }

        public static string TrimPath(string path)
        {
            var root = Routine.Directories.Root;
            var lib = System.IO.Path.GetFileName(Routine.Directories.Library);

            start:
            while (path.Length > 0 && path.First() == '\\')
            {
                path = path.Substring(1, path.Length - 1);
            }

            if (path.IndexOf(root) == 0)
            {
                path = path.Substring(root.Length, path.Length - root.Length);
                goto start;
            }

            if (path.IndexOf(lib) == 0)
            {
                path = path.Substring(lib.Length, path.Length - lib.Length);
                goto start;
            }

            while (path.Length > 0 && path.Last() == '\\')
            {
                path = path.Substring(0, path.Length - 1);
            }

            return path;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /*Private methods*/

        private List<string> DeserializePath(string path)
        {
            var list = new List<string>();
            int beg = 0;
            int end = 0;

            path = TrimPath(path);

            while (end != -1)
            {
                end = path.IndexOf('\\', beg);
                if (end != -1)
                {
                    var name = path.Substring(beg, end - beg);
                    if (name != "")
                    {
                        list.Add(name);
                        beg = end + 1;
                    }
                }
                else
                {
                    var name = path.Substring(beg, path.Length - beg);
                    if (name != "")
                    {
                        list.Add(name);
                    }
                    
                }
            }

            return list;
        }

        private string SerializePath(List<string> lpath, int endLevel)
        {
            string path = "";

            for (int i = 0; i <= endLevel; i++)
            {
                if (i > 0)
                {
                    path += "\\";
                }
                path += lpath[i];
            }

            return path;
        }

        private void Relations(Directory parent)
        {
            foreach (Directory child in parent.Directories)
            {
                Relations(child);
                child.Parent = parent;
            }
        }
    }
}
