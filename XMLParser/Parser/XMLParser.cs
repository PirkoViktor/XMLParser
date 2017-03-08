using System;
using System.Collections;
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

        public DataSet Parse()
        {

            var rootElement = document.Root;

            var tableNames =rootElement.Elements().GroupBy(c=>c.Name).Select(c=>c.Key);

            DataSet ds = new DataSet();

            foreach (var name in tableNames)
            {
                DataTable dt = new DataTable(name.ToString());

                var firstEl = rootElement.Elements().First(c => c.Name == name);
                foreach (var column in firstEl.Elements())
                {
                    dt.Columns.Add(column.Name.ToString());
                }
                
                foreach (var tableElement in rootElement.Elements().Where(c => c.Name==name))
                {
                    List<String> columnValues=new List<string>();
                    for (int i = 0; i < tableElement.Elements().Count(); i++)
                    {
                        columnValues.Add(tableElement.Elements().ToArray()[i].Value);
                    }
                    dt.Rows.Add(columnValues.ToArray());
                }
                ds.Tables.Add(dt);
            }
            return ds;
        }

        private bool CheckDocument()
        {
           // document.
            return true;
        }
    }

}
