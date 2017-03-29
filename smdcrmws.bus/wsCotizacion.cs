using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace smdcrmws.dto
{
    [DataContract]
    [Serializable]
    public class wsCotizacion
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String IdEmpresa { get; set; }

        [DataMember]
        public String Usuario { get; set; }

        [DataMember]
        public String Bodega { get; set; }

        [DataMember]
        public String Fecha { get; set; }

        [DataMember]
        public String TipoDocumento { get; set; }

        [DataMember]
        public String Cliente { get; set; }

        [DataMember]
        public String Vendedor { get; set; }

        [DataMember]
        public String FormaPago { get; set; }

        [DataMember]
        public String Contacto { get; set; }

        [DataMember]
        public String Dias { get; set; }

        [DataMember]
        public String Subtotal { get; set; }

        [DataMember]
        public String Descuento { get; set; }

        [DataMember]
        public String Iva { get; set; }

        [DataMember]
        public String Total { get; set; }

        [DataMember]
        public String Estado { get; set; }

        [DataMember]
        public String Moneda { get; set; }

        [DataMember]
        public String Tasa { get; set; }

        [DataMember]
        public String FechaEstimada { get; set; }

        [DataMember]
        public String Factibilidad { get; set; }

        [DataMember]
        public String NotasInternas { get; set; }

        [DataMember]
        public String Notas { get; set; }

        [DataMember]
        public String Negocio { get; set; }

        //[DataMember]
        //List<wsDetalleCotizacion> Detalle = new List<wsDetalleCotizacion>();

        //@Final int,
        //@nada char (1), --no se usa
        //    @Km int,
        //@IdItemLote int=0,
        //@Debe smallint = 0,
        //   @Ret1 money=0,
        //@Ret2 money = 0,
        //   @Ret3 money=0,
        //@Ret4 money = 0,
        //   @Ret5 money=0,
        //@Ret6 money = 0,
        //   @Rombo smallint=0,
        //@docref_tipo varchar(5)='',
        //@docref_numero varchar(20)='',
        //@ajuste money = 0,
        //   @id_ant int=0,
        //@IdCli2 int=0,
        //@Deducible decimal (11,8)=0,
        //@DeducibleMinimo money = 0,
        //   @IdProyecto int=0,
        //@haytaller smallint = 0,
        //   @id_med int=0,
        //@id_ent int=0,
        //@bod_dest int=0,
        //@Motivo int=0,
        //@r1 money = 0,
        //   @r2 money=0,
        //@r3 money = 0,
        //   @r4 money=0,
        //@r5 money = 0,
        //   @r6 money=0,
        //@FechaCartera datetime = '',
        //   @FechaDesconectada datetime='',
        //@PrecioNro smallint = 0,
        //   @NuevoDia tinyint=0, --truco de momento para salamanca
        //   @Id_Veh_hn_enc int=0,
        //@id_orden_taller int=0,
        //@id_pedido_taller int=0,
        //@tal_ope varCHAR(1)='',
        //@tal_def TINYINT = 0
    }
    
    class wsDetalleCotizacion
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String Renglon { get; set; }

        [DataMember]
        public String IdCotizacion { get; set; }

        [DataMember]
        public String IdItem { get; set; }

        [DataMember]
        public String Cantidad { get; set; }

        [DataMember]
        public String Precio { get; set; }

        [DataMember]
        public String PrecioCotizado { get; set; }

        [DataMember]
        public String Iva { get; set; }

        [DataMember]
        public String Notas { get; set; }

        [DataMember]
        public String Subtotal { get; set; }

        [DataMember]
        public String Descuento { get; set; }

        [DataMember]
        public String Und { get; set; }

        [DataMember]
        public String Conversion { get; set; }

    }

    //  @Emp Int,
    //   @Bod Int,
    //@Usu Int,
    //   @Renglon Int,
    //@IdCot Int,
    //   @Id Int,
    //@IdItem Int,
    //   @Cant Money,
    //@PrecioLista Money,
    //   @PrecioCotizado Money,
    //@Iva Decimal(5,2),
    //@Notas NVarchar(250),
    //@preciomasiva Money = 0,
    //   @porcentaje_descuento Decimal(18,10) = 0,
    //@id_operario Int = 0,
    //   @id_cot_item_lote Int = 0,
    //@porcentaje_descuento_grupo Decimal(18,10) = -1,
    //@Facturar_A Varchar(1) = '', --usar cuando se necesite

    //   @IdPed Int = 0,
    //   @TipoTran Int = 0,
    //@EsDevRemi Bit = 0,
    //   @conv Dec(28,16) = 1,
    //@und Smallint = 0,
    //   @can_tot_dis Money = 0,
    //@bod_dest Int = 0,
    //   @descu_escal Money = 0,
    //@ignorar_stock Bit = 0,
    //   @coscia Smallint = -1,
    //@id_promo Int = 0,
    //   @quitar_en_dic2016 Int=0
}
