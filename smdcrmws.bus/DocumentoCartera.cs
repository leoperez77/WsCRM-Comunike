using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smdcrmws.dto
{
    public class DocumentoCartera
    {
        public string bodega { get; set; }

        public string tipo { get; set; }

        public int numero { get; set; }

        public DateTime? fecha { get; set; }

        public DateTime? vencimiento { get; set; }

        public int? dias { get; set; }

        public decimal? valor_total { get; set; }

        public decimal? valor_aplicado { get; set; }

        public decimal? saldo { get; set; }

        public short? dias_gracia { get; set; }
    }
}
