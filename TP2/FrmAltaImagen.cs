using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TP2
{
    public partial class FrmAltaImagen : Form
    {
        private Imagen imagen = null;
        public FrmAltaImagen()
        {
            InitializeComponent();
        }

        public FrmAltaImagen(Imagen imagen)
        {
            InitializeComponent();
            this.imagen = imagen;
            Text = "Editar Imagen";
        }

        private void FrmAltaImagen_Load(object sender, EventArgs e)
        {
            btnAceptar.Enabled = false;
            if (imagen != null)                 
            {
                txtUrlImagen.Text = imagen.ImagenUrl;
                txtIdArticulo.Text = imagen.IdArticulo.ToString();
                btnAceptar.Enabled = true;
                cargarImagen(imagen.ImagenUrl.ToString());
                txtIdArticulo.Enabled = false;
            }
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ImagenNegocio negocio = new ImagenNegocio();

            try
            {
                if (imagen == null)
                {
                    imagen = new Imagen();
                }

                imagen.IdArticulo = int.Parse(txtIdArticulo.Text);
                imagen.ImagenUrl = txtUrlImagen.Text;
                string patron = @"^https:\/\/.+"; // Patron a cumplir
                bool validar = Regex.IsMatch(imagen.ImagenUrl, patron, RegexOptions.IgnoreCase);

                if (imagen.Id == 0)
                {
                    List<Articulo> listaArticulos;
                    ArticuloNegocio negocioArticulos = new ArticuloNegocio();

                    listaArticulos = negocioArticulos.Listar();

                    bool ExisteArticulo = listaArticulos.Any(a => a.Id == imagen.IdArticulo);

                    if(!ExisteArticulo)
                    {
                        MessageBox.Show("El artículo no existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    imagen.ImagenUrl = txtUrlImagen.Text.Trim();

                    List<Imagen> imagenes = negocio.Listar();

                    bool exists = imagenes.Any(m => m.ImagenUrl.Equals(imagen.ImagenUrl, StringComparison.OrdinalIgnoreCase));

                    if (exists)
                    {
                        MessageBox.Show("Esta imagen ya existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (!validar)
                        {
                            MessageBox.Show("La Url de la imagen ingresada no es valida. Ingresela nuevamente.", "Url Invalida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        negocio.Agregar(imagen);
                        MessageBox.Show("Agregado exitosamente");
                        Close();
                    }
                }
                else
                {
                    if (!validar)
                    {
                        MessageBox.Show("La Url de la imagen ingresada no es valida. Ingresela nuevamente.", "Url Invalida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    negocio.ModificarImagen(imagen);
                    MessageBox.Show("Imagen modificada exitosamente");
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

        private void txtIdArticulo_TextChanged(object sender, EventArgs e)
        {
            indicarObligatorio();
            controlAceptar();
        }

        private void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            //btnAceptar.Enabled = !string.IsNullOrWhiteSpace(txtImagen.Text);
            indicarObligatorio();
            controlAceptar();
            cargarImagen(txtUrlImagen.Text);
        }

        private void controlAceptar()
        {
            if (!string.IsNullOrWhiteSpace(txtUrlImagen.Text)
                && !string.IsNullOrWhiteSpace(txtIdArticulo.Text))
            {
                lblCampoObligatorio.Visible = false;
                btnAceptar.Enabled = true;//**
            }
            else
            {
                lblCampoObligatorio.Visible = true;
                btnAceptar.Enabled = false;//**
            }
        }

        private void indicarObligatorio()
        {
            if (string.IsNullOrWhiteSpace(txtUrlImagen.Text))
            {
                LblAsteriscoImagen.Visible = true;
            }
            else
            {
                LblAsteriscoImagen.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtIdArticulo.Text))
            {
                lblAsteriscoIdArticulo.Visible = true;
            }
            else
            {
                lblAsteriscoIdArticulo.Visible = false;
            }

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxAltaImagen.Load(imagen);
            }
            catch (Exception)
            {
                pbxAltaImagen.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDQQGGvsya8PwOD-0KOh6bClw8zRFxEpUaIWvZawv5IdEHPdLMs6C4DalMrGeinUXpp4I&usqp=CAU");
                //pbxAltaImagen.Load("https://static.vecteezy.com/system/resources/thumbnails/008/695/917/small_2x/no-image-available-icon-simple-two-colors-template-for-no-image-or-picture-coming-soon-and-placeholder-illustration-isolated-on-white-background-vector.jpg");
                //throw;
            }
        }
    }
}
