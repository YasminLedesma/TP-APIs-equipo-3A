namespace TP2
{
    partial class FrmAltaImagen
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
            this.lblAsteriscoIdArticulo = new System.Windows.Forms.Label();
            this.txtIdArticulo = new System.Windows.Forms.TextBox();
            this.lblIdArticulo = new System.Windows.Forms.Label();
            this.LblAsteriscoImagen = new System.Windows.Forms.Label();
            this.lblCampoObligatorio = new System.Windows.Forms.Label();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtUrlImagen = new System.Windows.Forms.TextBox();
            this.lblImagenUrl = new System.Windows.Forms.Label();
            this.lblImagenTitulo = new System.Windows.Forms.Label();
            this.pbxAltaImagen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAltaImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAsteriscoIdArticulo
            // 
            this.lblAsteriscoIdArticulo.AutoSize = true;
            this.lblAsteriscoIdArticulo.ForeColor = System.Drawing.Color.Red;
            this.lblAsteriscoIdArticulo.Location = new System.Drawing.Point(378, 152);
            this.lblAsteriscoIdArticulo.Name = "lblAsteriscoIdArticulo";
            this.lblAsteriscoIdArticulo.Size = new System.Drawing.Size(15, 20);
            this.lblAsteriscoIdArticulo.TabIndex = 27;
            this.lblAsteriscoIdArticulo.Text = "*";
            // 
            // txtIdArticulo
            // 
            this.txtIdArticulo.Location = new System.Drawing.Point(112, 146);
            this.txtIdArticulo.Name = "txtIdArticulo";
            this.txtIdArticulo.Size = new System.Drawing.Size(260, 26);
            this.txtIdArticulo.TabIndex = 1;
            this.txtIdArticulo.TextChanged += new System.EventHandler(this.txtIdArticulo_TextChanged);
            // 
            // lblIdArticulo
            // 
            this.lblIdArticulo.AutoSize = true;
            this.lblIdArticulo.Location = new System.Drawing.Point(28, 149);
            this.lblIdArticulo.Name = "lblIdArticulo";
            this.lblIdArticulo.Size = new System.Drawing.Size(84, 20);
            this.lblIdArticulo.TabIndex = 25;
            this.lblIdArticulo.Text = "Id Articulo:";
            // 
            // LblAsteriscoImagen
            // 
            this.LblAsteriscoImagen.AutoSize = true;
            this.LblAsteriscoImagen.ForeColor = System.Drawing.Color.Red;
            this.LblAsteriscoImagen.Location = new System.Drawing.Point(378, 115);
            this.LblAsteriscoImagen.Name = "LblAsteriscoImagen";
            this.LblAsteriscoImagen.Size = new System.Drawing.Size(15, 20);
            this.LblAsteriscoImagen.TabIndex = 24;
            this.LblAsteriscoImagen.Text = "*";
            // 
            // lblCampoObligatorio
            // 
            this.lblCampoObligatorio.AutoSize = true;
            this.lblCampoObligatorio.ForeColor = System.Drawing.Color.Red;
            this.lblCampoObligatorio.Location = new System.Drawing.Point(28, 186);
            this.lblCampoObligatorio.Name = "lblCampoObligatorio";
            this.lblCampoObligatorio.Size = new System.Drawing.Size(147, 20);
            this.lblCampoObligatorio.TabIndex = 23;
            this.lblCampoObligatorio.Text = "* Campo obligatorio";
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Location = new System.Drawing.Point(240, 214);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(106, 38);
            this.BtnCancelar.TabIndex = 3;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(128, 214);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(106, 38);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtUrlImagen
            // 
            this.txtUrlImagen.Location = new System.Drawing.Point(112, 109);
            this.txtUrlImagen.Name = "txtUrlImagen";
            this.txtUrlImagen.Size = new System.Drawing.Size(260, 26);
            this.txtUrlImagen.TabIndex = 0;
            this.txtUrlImagen.TextChanged += new System.EventHandler(this.txtUrlImagen_TextChanged);
            // 
            // lblImagenUrl
            // 
            this.lblImagenUrl.AutoSize = true;
            this.lblImagenUrl.Location = new System.Drawing.Point(26, 112);
            this.lblImagenUrl.Name = "lblImagenUrl";
            this.lblImagenUrl.Size = new System.Drawing.Size(91, 20);
            this.lblImagenUrl.TabIndex = 19;
            this.lblImagenUrl.Text = "Url Imagen:";
            // 
            // lblImagenTitulo
            // 
            this.lblImagenTitulo.AutoSize = true;
            this.lblImagenTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImagenTitulo.Location = new System.Drawing.Point(160, 32);
            this.lblImagenTitulo.Name = "lblImagenTitulo";
            this.lblImagenTitulo.Size = new System.Drawing.Size(136, 40);
            this.lblImagenTitulo.TabIndex = 18;
            this.lblImagenTitulo.Text = "Imagen";
            // 
            // pbxAltaImagen
            // 
            this.pbxAltaImagen.Location = new System.Drawing.Point(450, 37);
            this.pbxAltaImagen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbxAltaImagen.Name = "pbxAltaImagen";
            this.pbxAltaImagen.Size = new System.Drawing.Size(225, 231);
            this.pbxAltaImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxAltaImagen.TabIndex = 28;
            this.pbxAltaImagen.TabStop = false;
            // 
            // FrmAltaImagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 303);
            this.Controls.Add(this.pbxAltaImagen);
            this.Controls.Add(this.lblAsteriscoIdArticulo);
            this.Controls.Add(this.txtIdArticulo);
            this.Controls.Add(this.lblIdArticulo);
            this.Controls.Add(this.LblAsteriscoImagen);
            this.Controls.Add(this.lblCampoObligatorio);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtUrlImagen);
            this.Controls.Add(this.lblImagenUrl);
            this.Controls.Add(this.lblImagenTitulo);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmAltaImagen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAltaImagen";
            this.Load += new System.EventHandler(this.FrmAltaImagen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxAltaImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxAltaImagen;
        private System.Windows.Forms.Label lblAsteriscoIdArticulo;
        private System.Windows.Forms.TextBox txtIdArticulo;
        private System.Windows.Forms.Label lblIdArticulo;
        private System.Windows.Forms.Label LblAsteriscoImagen;
        private System.Windows.Forms.Label lblCampoObligatorio;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtUrlImagen;
        private System.Windows.Forms.Label lblImagenUrl;
        private System.Windows.Forms.Label lblImagenTitulo;
    }
}