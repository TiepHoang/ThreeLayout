using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ThreeLayoutVer2._0.Core
{
    public class XmlSetting
    {
        public static string fileName = "LogSave.xml";

        public void Write(Setting setting)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    XmlSerializer sr = new XmlSerializer(typeof(Setting));
                    sr.Serialize(fs, setting);
                    fs.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Setting Read()
        {
            try
            {
                if (!File.Exists(fileName)) return null;
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer sr = new XmlSerializer(typeof(Setting));

                    object result = sr.Deserialize(fs);

                    fs.Close();
                    return result as Setting;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
