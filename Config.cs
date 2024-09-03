using System.Drawing;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.IO;

namespace Recipe
{
    public class Config
    {
        public TPos ToolPosition { get; set; }

        public VOStyle VObjStyle { get; set; }

        [JsonIgnore]
        public bool Loaded { get; set; }
        [JsonIgnore]
        public bool Changed { get; set; }

        public Config()
        {
            ToolPosition = new TPos(this);
            VObjStyle = new VOStyle(this);
        }

        /**/

        public class TPos
        {
            public Point Library 
            { 
                get
                {
                    return lib;
                }
                set
                {
                    lib = Validate(value);
                    cfg.Changed = true;
                }
            }

            public int Library_Height
            {
                get
                {
                    return libH;
                }
                set
                {
                    libH = value;
                    if (libH + lib.Y > Screen.PrimaryScreen.Bounds.Height)
                    {
                        lib.Y = 10;
                        libH = Screen.PrimaryScreen.Bounds.Height - 100;
                    }

                    cfg.Changed = true;
                }
            }

            public Point PropEditor
            {
                get
                {
                    return props;
                }
                set
                {
                    props = Validate(value);
                    cfg.Changed = true;
                }
            }

            [JsonIgnore]
            private Point lib;
            [JsonIgnore]
            private int libH;
            [JsonIgnore]
            private Point props;
            [JsonIgnore]
            private Config cfg;

            public TPos(Config config)
            {
                cfg = config;
            }


            private Point Validate(Point input)
            {
                Rectangle screen = Screen.PrimaryScreen.Bounds;

                if (input.X < 0 || input.X > screen.Width - 20)
                {
                    return new Point(0, 0);
                }

                if (input.Y < 0 || input.Y > screen.Height - 20)
                {
                    return new Point(0, 0);
                }

                return input;
            }
        }

        public class VOStyle
        {
            public bool IconHasBorder { get; set; }
            public bool LabelHasBorder { get; set; }

            [JsonIgnore]
            public BorderStyle IconBorder
            {
                get
                {
                    var bs = BorderStyle.None;

                    if(IconHasBorder)
                    {
                        bs = BorderStyle.FixedSingle;
                    }

                    return bs;
                }
                set
                {
                    IconHasBorder = value == BorderStyle.FixedSingle;
                    cfg.Changed = true;
                }
            }

            [JsonIgnore]
            public BorderStyle LabelBorder
            {
                get
                {
                    var bs = BorderStyle.None;

                    if (LabelHasBorder)
                    {
                        bs = BorderStyle.FixedSingle;
                    }

                    return bs;
                }
                set
                {
                    LabelHasBorder = value == BorderStyle.FixedSingle;
                    cfg.Changed = true;
                }
            }

            public VOStyle(Config config)
            {
                cfg = config;
            }

            [JsonIgnore]
            private Config cfg;


            public void IconBorderEnable(bool en)
            {
                IconHasBorder = en;
                cfg.Changed = true;
            }

            public void LabelBorderEnable(bool en)
            {
                LabelHasBorder = en;
                cfg.Changed = true;
            }
        }

        /**/

        public void Save(string path)
        {
            if (Changed)
            {
                Changed = false;
                var data = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(path, data);
            }
        }

        public static Config Load(string path)
        {
            if (File.Exists(path))
            {
                var data = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<Config>(data);

                config.Loaded = true;
                config.Changed = false;

                return config;
            }
            else
            {
                return new Config();
            }
        }
    }
}
