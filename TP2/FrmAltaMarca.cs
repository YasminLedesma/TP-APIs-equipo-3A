using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2
{
    public partial class FrmAltaMarca : Form
    {
        private Marca marca = null;
        public FrmAltaMarca()
        {
            InitializeComponent();
        }

        public FrmAltaMarca(Marca marca)
        {
            InitializeComponent();
            this.marca = marca;
            Text = "Editar Marca";
        }
        private void FrmAltaMarca_Load(object sender, EventArgs e)
        {
            btnAceptar.Enabled = false;
            if (marca != null)
            {
                txtMarca.Text = marca.Descripcion;
                btnAceptar.Enabled = true;
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                if(marca == null)
                {
                    marca = new Marca();
                }

                marca.Descripcion = txtMarca.Text;

                if (marca.Id == 0)
                {
                    marca.Descripcion = txtMarca.Text.Trim();

                    List<Marca> categorias = negocio.Listar();

                    bool exists = categorias.Any(m => m.Descripcion.Equals(marca.Descripcion, StringComparison.OrdinalIgnoreCase));

                    if (exists)
                    {
                        MessageBox.Show("Esta categoria ya existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        negocio.AgregarMarca(marca);
                        MessageBox.Show("Agregado exitosamente");
                        Close();
                    }
                }
                else
                {
                    negocio.ModificarMarca(marca);
                    MessageBox.Show("Marca modificada exitosamente");
                    Close();
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = !string.IsNullOrWhiteSpace(txtMarca.Text);
        }
    }
}
