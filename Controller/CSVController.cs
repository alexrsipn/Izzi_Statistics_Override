using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izzi_Statistics_Override_WPF.Controller
{
    public class CSVController
    {
        private DataTable _DTable;
        public DataTable DTable
        {
            get { return _DTable; }
            set { _DTable = value; }
        }
        public async Task<DataTable> ReadCSV(string rutaArchivo)
        {
            DataTable dt = new DataTable();
            using (StreamReader reader = new StreamReader(rutaArchivo))
            {
                string[] headers = (await reader.ReadLineAsync()).Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while(!reader.EndOfStream)
                {
                    string[] rows = (await reader.ReadLineAsync()).Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public DataTable ValidateCSV(DataTable dataTable)
        {
            DataTable dt = new DataTable();
            dt = DTable;
            foreach (DataRow row in dt.Rows)
            {
                if (row[2].ToString() == "" || row[2] == null || row[2] == DBNull.Value)
                {
                    row[2] = 0;
                }
                //for (int i=0; i < dt.Columns.Count; i++)
                //{
                //    if (row[i] == null || row[i] == DBNull.Value)
                //    {
                //        row[i] = 0;
                //    }
                //}
            }
            return dt;
        }
    }
}
