using System;
using System.Collections.Generic;

namespace Laundry.Models
{
    public partial class ReadOnly
    {
        public int IdPesanan { get; set; }
        public string NamaCustomer { get; set; }
        public string Tipe { get; set; }
        public int? BeratTotal { get; set; }
        public int? HrgSatuan { get; set; }
        public int? HrgTotal { get; set; }
        public DateTime? TglPesanan { get; set; }
    }
}
