using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace FontHandler
{
    public class FontType
    {
        public string Name;
        public string[] FileFormats;
        public Assembly Assembly;
    }

    public class PicType
    {
        public string Name;
        public string[] FileFormats;
        public Assembly Assembly;
    }

    public class FontChar
    {
        public char Char;
        public bool[,] Pixels;
        public int Width;
    }

    public class Font
    {
        public string Name;
        public int Height;
        public FontType Type;
        public string FilePath;
        public List<FontChar> Chars = new List<FontChar>();
        public int Spacing = 0;
    }

    public class Pictogram
    {
        public string Name;
        public PicType Type;
        public string FilePath;
        public int Width;
        public int Height;
        public bool[,] Pixels;
    }

    public class FontHandler
    {
        public List<FontType> fontTypes = new List<FontType>();
        public List<PicType> picTypes = new List<PicType>();

        public bool LoadDLL(string path)
        {
            Assembly loadDLL = Assembly.LoadFile(String.Format("{0}\\{1}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path));
            Type type = loadDLL.GetType("FontHandlerFormat.Base");
            if (type == null) return false;
            var obj = Activator.CreateInstance(type);
            string formatName = (string)type.GetMethod("GetFormatName").Invoke(obj, null);
            string[] fontTypeFileFormats = (string[])type.GetMethod("GetFontTypeFileFormats").Invoke(obj, null);
            string[] picTypeFileFormats = (string[])type.GetMethod("GetPicTypeFileFormats").Invoke(obj, null);
            if (fontTypeFileFormats.Length > 0) AddFontType(formatName, fontTypeFileFormats, loadDLL);
            if (picTypeFileFormats.Length > 0) AddPicType(formatName, picTypeFileFormats, loadDLL);
            return true;
        }

        private void AddFontType(string name, string[] fileFormats, Assembly assembly)
        {
            fontTypes.Add(new FontType { Name = name, FileFormats = fileFormats, Assembly = assembly });
        }

        private void AddPicType(string name, string[] fileFormats, Assembly assembly)
        {
            picTypes.Add(new PicType { Name = name, FileFormats = fileFormats, Assembly = assembly });
        }
    }
}
