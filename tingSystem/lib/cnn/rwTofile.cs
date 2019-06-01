using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ting.lib
{
    internal class rwTofile
    {
        /// <summary>
        /// save connection string to disk file
        /// </summary>
        public bool UpdateConnectionString(string name, string strcnn, bool isencrypted,string file)
        {
            XDocument doc;
            if (!File.Exists(file))
            {
                // create
                doc =
                    new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XComment("tingSystem config"),
                        new XProcessingInstruction("ting", "159-7906-2229"),
                        new XElement("tingSystem",
                            new XElement("connectionstrings",
                                new XElement("connectionstring",
                                    new XAttribute("name", name),
                                    new XAttribute("value", strcnn),
                                    new XAttribute("protected", isencrypted)
                                )
                            )
                        )
                    );
            }
            else
            {

                doc = XDocument.Load(file);
                var cnn = (from c in doc.Element("tingSystem").Element("connectionstrings").Elements()
                           where (string)c.Attribute("name").Value == name
                           select c).FirstOrDefault();

                if (cnn == null)
                {
                    // insert
                    doc.Element("tingSystem").Element("connectionstrings").Add(
                        new XElement("connectionstring",
                            new XAttribute("name", name),
                            new XAttribute("value", strcnn),
                            new XAttribute("protected", isencrypted)
                        )
                    );
                }
                else
                {
                    // alter 
                    foreach (XElement xe in doc.Element("tingSystem").Element("connectionstrings").Elements())
                    {
                        if (xe.Attribute("name").Value == name)
                        {
                            xe.Attribute("value").Value = strcnn;
                            xe.Attribute("protected").Value = isencrypted.ToString();
                        }
                    }
                }
            }
            try
            {
                doc.Save(file);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// get connection string from disk file
        /// </summary>
        public string GetConnectionString(string name, string file)
        {
            XDocument doc;
            string result;
            if (File.Exists(file))
            {
                doc = XDocument.Load(file);
                var cnn = (from c in doc.Element("tingSystem").Element("connectionstrings").Elements()
                           where c.Attribute("name").Value == name
                           select c).FirstOrDefault();
                if (cnn != null)
                {
                    result = cnn.Attribute("value").Value;
                }
                else
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
