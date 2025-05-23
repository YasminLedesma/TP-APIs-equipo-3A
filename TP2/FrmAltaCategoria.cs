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
    public partial class FrmAltaCategoria : Form
    {
        private Categoria Categoria = null;
        public FrmAltaCategoria()
        {
            InitializeComponent();
        }

        public FrmAltaCategoria(Categoria categoria)
        {
            InitializeComponent();
            this.Categoria = categoria;
            Text = "Modificar Categoria";


        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                if (Categoria == null)
                {
                    Categoria = new Categoria();
                }

                Categoria.Descripcion = txtCategoria.Text;

               

                if (Categoria.Id == 0)
                {
                    Categoria.Descripcion = txtCategoria.Text.Trim();

                    List<Categoria> categorias = categoriaNegocio.Listar();

                    bool exists = categorias.Any(c => c.Descripcion.Equals(Categoria.Descripcion, StringComparison.OrdinalIgnoreCase));

                    if (exists)
                    {
                        MessageBox.Show("Esta categoria ya existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        categoriaNegocio.AgregarCategoria(Categoria);
                        MessageBox.Show("Agregado exitosamente");
                        Close();
                    }
                }
                else
                {
                    categoriaNegocio.ModificarCategoria(Categoria);
                    MessageBox.Show("Categoria modificada exitosamente");
                    Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmAltaCategoria_Load(object sender, EventArgs e)
        {
            btnAceptar.Enabled = false;
            if (Categoria != null)
            {
                txtCategoria.Text = Categoria.Descripcion;
                btnAceptar.Enabled = true;
            }
        }

        private void txtCategoria_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = !string.IsNullOrWhiteSpace(txtCategoria.Text);
        }
    }
}
