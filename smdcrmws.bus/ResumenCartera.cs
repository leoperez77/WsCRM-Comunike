using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smdcrmws.dto
{
    public class ResumenCartera
    {
        public string nit { get; set; }

        public string razon_social { get; set; }

        public decimal cupo_credito { get; set; }

        public short dias_gracia { get; set; }

        public string estado { get; set; }

        public string forma_pago { get; set; }

        public decimal saldo { get; set; }

        public decimal? disponible { get; set; }

        public short? sw { get; set; }

        public int? NumVencidos { get; set; }

        public decimal? saldo_vencido { get; set; }

    }

}
