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
        

        // viide Arve
        public Arve Arve { get; set; }
        public int ArveId { get; set; }

        public IList<TellimuseRida> Read { get; set; }
    }
}
