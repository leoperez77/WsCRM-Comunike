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
    }


}
