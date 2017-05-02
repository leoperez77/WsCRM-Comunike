using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using smdcrmws.dto;
namespace smdcrmClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (optNegocio.Checked)
            {
                var obj = new smdcrmws.dto.wsNegocio();
                obj.Id = "0";
                obj.IdEmpresa = "320";
                obj.Fecha = "2017-03-21";
                obj.Estado = "0";
                obj.TipoDocumento = "4081";
                obj.Bodega = "300";
                obj.Contacto = "145557";
                obj.Usuario = "2985";
                obj.Vendedor = "2985";
                obj.UsuarioRevisa = "0";
                obj.UsuarioAprueba = "0";
                obj.FechaEntrega = "2017-03-21";
                obj.DocReferencia = "";
                obj.Notas = "Notas del documento";
                obj.NotasAprobacion = "";
                obj.NotasRevision = "";
                textBox1.Text = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }

            if(optCotizacion.Checked)
            {
                var obj = new smdcrmws.dto.wsCotizacion();
                obj.Id = "0";
                obj.IdEmpresa = "320";
                obj.Usuario = "2985";
                obj.Fecha = "2017-04-29";
                obj.TipoDocumento = "156";
                obj.Cliente = "223395";
                obj.Vendedor = "2985";
                obj.FormaPago = "0";
                obj.Contacto = "145557";
                obj.Dias = "30";
                obj.Subtotal = "89150000";
                obj.Descuento = "0";
                obj.Iva = "22274000";
                obj.Total = "111424000";
                obj.Estado = "0"; // envía la misma empresa. revisar
                obj.Moneda = "0";
                obj.Tasa = "1";
                obj.FechaEstimada = "2017-05-10";
                obj.Factibilidad = "50";
                obj.NotasInternas = "Estas son las notas internas no visibles para el cliente";
                obj.Notas = "Se incluye seguro todo riesgo";
                obj.Bodega = "300";
                obj.Negocio = "0";

                var det = new smdcrmws.dto.wsDetalleCotizacion();
                det.Cantidad = "1";
                det.Conversion = "1";
                det.Descuento = "0";
                det.Id = "0";
                det.IdCotizacion = "0";
                det.IdItem = "123369";
                det.Precio = "89000000";
                det.PrecioCotizado = "89000000";
                det.Iva = "25";
                det.Notas = "Este es un vehículo";
                det.Renglon = "0";
                det.Subtotal = "0"; // Así lo manda la app
                obj.Detalle.Add(det);
                               

                det = new smdcrmws.dto.wsDetalleCotizacion();
                det.Cantidad = "1";
                det.Conversion = "1";
                det.Descuento = "0";
                det.Id = "0";
                det.IdCotizacion = "0";
                det.IdItem = "19479";
                det.Precio = "150000";
                det.PrecioCotizado = "150000";
                det.Iva = "16";
                det.Notas = "Esta es una película en la misma cotización, línea adicional";
                det.Renglon = "1";
                det.Subtotal = "0"; // Así lo manda la app
                obj.Detalle.Add(det);
                
                textBox1.Text = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }

            if (optOrden.Checked)
            {               
                var obj = new smdcrmws.dto.wsOrdenTaller();
                obj.Id = "0";
                obj.IdEmpresa = "320";
                obj.Usuario = "2985";
                obj.Bodega = "2985";
                obj.Fecha = "2017-03-21";
                obj.TipoDocumento = "4083";
                obj.Cliente = "223395";
                obj.Vendedor = "2985";
                obj.Contacto = "145557";
                obj.Subtotal = "0";
                obj.Descuento = "0";
                obj.Iva = "0";
                obj.Total = "0";
                obj.FechaEstimada = "20170427 15:30:00";
                obj.Notas = "Notas de la orden de taller";
                obj.NotasInternas = "Notas internas que no se imprimen";
                obj.Kilometraje = "18500";
                obj.IdLote = "110997";
                obj.Rombo = "ROB001";
                obj.TipoReferencia = "T001";
                obj.NumeroReferencia = "1023";
                obj.Aseguradora = "0";
                obj.Deducible = "0";
                obj.MinimoDeducible = "0";
                obj.IdMotivo = "22";
                obj.ValorHora = "85000";
                obj.IdPlan = "0";
                obj.IdCita = "0";
                                
                var det = new smdcrmws.dto.wsOperacionOrden();
                det.Id = "0";
                det.IdCotizacion = "0";
                det.IdItem = "123371";
                det.Cantidad = "1";
                det.Tiempo = "1";
                det.ValorHora = "85000";
                det.ValorOperacion = "0";
                det.Iva = "19";
                det.Notas = "Revisar ruido en el lado derecho";
                det.PorcentajeDescuento = "0";
                det.Operario = "3580";
                det.Facturar = "C";
                det.TipoOperacion = "L";

                obj.Detalle.Add(det);


                det = new smdcrmws.dto.wsOperacionOrden();
                det.Id = "0";
                det.IdCotizacion = "0";
                det.IdItem = "123371";
                det.Cantidad = "1";
                det.Tiempo = "1";
                det.ValorHora = "85000";
                det.ValorOperacion = "0";
                det.Iva = "19";
                det.Notas = "Revisar ruido en el lado izquierdo";
                det.PorcentajeDescuento = "0";
                det.Operario = "3580";
                det.Facturar = "C";
                det.TipoOperacion = "L";

                obj.Detalle.Add(det);

                textBox1.Text = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }

            if(optPedido.Checked)
            {
                var obj = new smdcrmws.dto.wsCotizacion();
                obj.Id = "0";
                obj.IdEmpresa = "320";
                obj.Usuario = "2985";
                obj.Fecha = "2017-04-29";
                obj.TipoDocumento = "432";
                obj.Cliente = "223395";
                obj.Vendedor = "2985";
                obj.FormaPago = "0";
                obj.Contacto = "145557";
                obj.Dias = "30";
                obj.Subtotal = "89150000";
                obj.Descuento = "0";
                obj.Iva = "22274000";
                obj.Total = "111424000";
                obj.Estado = "0"; // envía la misma empresa. revisar
                obj.Moneda = "0";
                obj.Tasa = "1";
                obj.FechaEstimada = "2017-05-10";
                obj.Factibilidad = "50";
                obj.NotasInternas = "Estas son las notas internas no visibles para el cliente";
                obj.Notas = "Notas del pedido";
                obj.Bodega = "300";
                obj.Negocio = "0";

                var det = new smdcrmws.dto.wsDetalleCotizacion();
                det.Cantidad = "1";
                det.Conversion = "1";
                det.Descuento = "0";
                det.Id = "0";
                det.IdCotizacion = "0";
                det.IdItem = "19510";
                det.Precio = "60000";
                det.PrecioCotizado = "60000";
                det.Iva = "19";
                det.Notas = "Item 1";
                det.Renglon = "0";
                det.Subtotal = "0"; // Así lo manda la app
                obj.Detalle.Add(det);


                det = new smdcrmws.dto.wsDetalleCotizacion();
                det.Cantidad = "1";
                det.Conversion = "1";
                det.Descuento = "0";
                det.Id = "0";
                det.IdCotizacion = "0";
                det.IdItem = "19512";
                det.Precio = "60000";
                det.PrecioCotizado = "60000";
                det.Iva = "16";
                det.Notas = "Item 2";
                det.Renglon = "1";
                det.Subtotal = "0"; // Así lo manda la app
                obj.Detalle.Add(det);

                textBox1.Text = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }

            if (optCita.Checked)
            {
                var obj = new wsCita();
                obj.Id = "0";
                obj.IdEmpresa = "320";
                obj.Hora = "2017-05-02 11:15:00";
                obj.Bodega = "958";
                obj.IdCamp = "0";
                obj.IdPlan = "0";
                obj.Responsable = "Nombre del responsable";
                obj.Telefono = "3017217928";
                obj.Notas = "Notas de la cita";
                obj.IdVehiculo = "111006";
                textBox1.Text = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
        }
    }


}
