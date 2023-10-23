using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;


namespace CsvRecord
{
    public class Record
    {
        public string ProductId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
    public sealed class RecordMap : ClassMap<Record>
    {
        public RecordMap()
        {
            Map(m => m.ProductId).Name("productid");
            Map(m => m.Type).Name("type");
            Map(m => m.Description).Name("description");
            Map(m => m.Amount).Name("amount");
        }
    }
}
