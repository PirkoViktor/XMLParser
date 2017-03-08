using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
       static void Main(string[] args)
        {
            XmlParser p = new XmlParser("doc.xml");
            DataSet ds = p.GetData();
            DataTable dt = ds.Tables[0];
        }
    }
}
