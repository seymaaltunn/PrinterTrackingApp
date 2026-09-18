using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTrackingApp.Core.Entities
{
    public class BakimKaydi
    {
        public int Id { get; set; }

        public int YaziciId { get; set; }

        public string ParcaTipi { get; set; } = string.Empty;

        public string? Renk { get; set; }

        public DateTime DegisimTarihi { get; set; }

        public string? Notlar { get; set; }

        public DateTime? OlusturmaTarihi { get; set; }
    }
}
