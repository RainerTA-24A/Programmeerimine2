using System;

namespace KooliProjekt.WindowsForms.Api
{
    public class Arve
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal GrandTotal { get; set; }
        public string Status { get; set; }
        public Klient Klient { get; set; }
    }
}