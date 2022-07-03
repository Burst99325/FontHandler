using System;

namespace FontHandler
{
    public class PicBase
    {
        public Pictogram LoadPic(PicType picType, string filePath)
        {
            Type type = picType.Assembly.GetType("FontHandlerFormat.FontHandlerPic");
            if (type == null) throw new Exception(String.Format("This FontHandler plugin cannot load pictograms"));
            var obj = Activator.CreateInstance(type);
            return (Pictogram)type.GetMethod("Load").Invoke(obj, new object[] { filePath });
        }

        public void SavePic(Pictogram pic, string filePath)
        {
            Type type = pic.Type.Assembly.GetType("FontHandlerFormat.FontHandlerPic");
            if (type == null) throw new Exception(String.Format("This FontHandler plugin cannot save pictograms"));
            var obj = Activator.CreateInstance(type);
            type.GetMethod("Save").Invoke(obj, new object[] { pic, filePath });
        }
    }
}
