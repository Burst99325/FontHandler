using System;

namespace FontHandler
{
    public class FontBase
    {
        public Font LoadFont(FontType fontType, string filePath)
        {
            Type type = fontType.Assembly.GetType("FontHandlerFormat.FontHandlerFont");
            if (type == null) throw new Exception("This FontHandler plugin cannot load fonts!");
            var obj = Activator.CreateInstance(type);
            return (Font)type.GetMethod("Load").Invoke(obj, new object[] { filePath });
        }

        public void SaveFont(Font font, string filePath)
        {
            Type type = font.Type.Assembly.GetType("FontHandlerFormat.FontHandlerFont");
            if (type == null) throw new Exception("This FontHandler plugin cannot save fonts!");
            var obj = Activator.CreateInstance(type);
            type.GetMethod("Save").Invoke(obj, new object[] { font, filePath });
        }
    }
}
