using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Parser
{
    public class XmlParser
    {
        private XDocument document;
        public XmlParser(string filename)
        {

            document = new XDocument();
            document= XDocument.Load(filename);
        }

        public DataSet GetData()
        {
            DataSet resultDataSet = new DataSet();
            using (XmlReader xr= document.CreateReader())
            {
                resultDataSet.ReadXml(xr);
            }

            return resultDataSet;
        }

        private bool CheckDocument()
        {
           // document.
            return true;
        }
    }

}
