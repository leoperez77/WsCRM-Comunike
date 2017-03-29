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
            this.button1 = new System.Windows.Forms.Button();
            this.optNegocio = new System.Windows.Forms.RadioButton();
            this.optCotizacion = new System.Windows.Forms.RadioButton();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // optNegocio
            // 
            this.optNegocio.AutoSize = true;
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
            this.optCotizacion.TabStop = true;
            this.optCotizacion.Text = "Cotización";
            this.optCotizacion.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 272);
            this.Controls.Add(this.optCotizacion);
            this.Controls.Add(this.optNegocio);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton optNegocio;
        private System.Windows.Forms.RadioButton optCotizacion;
    }
}

