namespace smdcrmClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmdJson = new System.Windows.Forms.Button();
            this.optNegocio = new System.Windows.Forms.RadioButton();
            this.optCotizacion = new System.Windows.Forms.RadioButton();
            this.optOrden = new System.Windows.Forms.RadioButton();
            this.optPedido = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 104);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(661, 124);
            this.textBox1.TabIndex = 0;
            // 
            // cmdJson
            // 
            this.cmdJson.Location = new System.Drawing.Point(12, 63);
            this.cmdJson.Name = "cmdJson";
            this.cmdJson.Size = new System.Drawing.Size(75, 23);
            this.cmdJson.TabIndex = 1;
            this.cmdJson.Text = "Json";
            this.cmdJson.UseVisualStyleBackColor = true;
            this.cmdJson.Click += new System.EventHandler(this.button1_Click);
            // 
            // optNegocio
            // 
            this.optNegocio.AutoSize = true;
            this.optNegocio.Checked = true;
            this.optNegocio.Location = new System.Drawing.Point(23, 12);
            this.optNegocio.Name = "optNegocio";
            this.optNegocio.Size = new System.Drawing.Size(65, 17);
            this.optNegocio.TabIndex = 2;
            this.optNegocio.TabStop = true;
            this.optNegocio.Text = "Negocio";
            this.optNegocio.UseVisualStyleBackColor = true;
            // 
            // optCotizacion
            // 
            this.optCotizacion.AutoSize = true;
            this.optCotizacion.Location = new System.Drawing.Point(103, 12);
            this.optCotizacion.Name = "optCotizacion";
            this.optCotizacion.Size = new System.Drawing.Size(74, 17);
            this.optCotizacion.TabIndex = 3;
            this.optCotizacion.Text = "Cotización";
            this.optCotizacion.UseVisualStyleBackColor = true;
            // 
            // optOrden
            // 
            this.optOrden.AutoSize = true;
            this.optOrden.Location = new System.Drawing.Point(184, 13);
            this.optOrden.Name = "optOrden";
            this.optOrden.Size = new System.Drawing.Size(94, 17);
            this.optOrden.TabIndex = 4;
            this.optOrden.Text = "Orden de taller";
            this.optOrden.UseVisualStyleBackColor = true;
            // 
            // optPedido
            // 
            this.optPedido.AutoSize = true;
            this.optPedido.Location = new System.Drawing.Point(284, 13);
            this.optPedido.Name = "optPedido";
            this.optPedido.Size = new System.Drawing.Size(58, 17);
            this.optPedido.TabIndex = 5;
            this.optPedido.Text = "Pedido";
            this.optPedido.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 272);
            this.Controls.Add(this.optPedido);
            this.Controls.Add(this.optOrden);
            this.Controls.Add(this.optCotizacion);
            this.Controls.Add(this.optNegocio);
            this.Controls.Add(this.cmdJson);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button cmdJson;
        private System.Windows.Forms.RadioButton optNegocio;
        private System.Windows.Forms.RadioButton optCotizacion;
        private System.Windows.Forms.RadioButton optOrden;
        private System.Windows.Forms.RadioButton optPedido;
    }
}

