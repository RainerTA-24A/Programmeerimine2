using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class Tellimus
    {
        public int Id { get; set; }

        // rida tasemed
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        // VAT
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }

        // viide Toode
        public Toode Toode { get; set; }
        public int ToodeId { get; set; }

        // viide Arve
        public Arve Arve { get; set; }
        public int ArveId { get; set; }
    }
}
