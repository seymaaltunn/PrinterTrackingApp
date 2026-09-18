using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTrackingApp.Core.Entities
{
    public class YaziciSarfKodu
    {
        public int Id { get; set; }

        public int YaziciId { get; set; }

        public string Renk { get; set; } = string.Empty;

        public string TonerKodu { get; set; } = string.Empty;

        public string? DrumKodu { get; set; }
    }
}