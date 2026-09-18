using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterTrackingApp.Core.DTOs;

namespace PrinterTrackingApp.Core.Interfaces
{
    public interface IPrinterMonitor
    {
        PrinterSnapshot Oku(string ipAdresi);
    }
}
