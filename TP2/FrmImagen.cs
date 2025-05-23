using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TP2
{
    public partial class FrmImagen : Form
    {
        private List<Imagen> ListaImagen;
        private List<Articulo> ListaArticulos;
        private bool sincronizandoSeleccion = false;

        public FrmImagen()
        {
            InitializeComponent();
        }

        private void Cargar()
        {
            ImagenNegocio negocio = new ImagenNegocio();
            ListaImagen = negocio.Listar()
                         .OrderBy(img => img.IdArticulo) // Ordeno lista dgvImagenes por IdArticulo
                         .ToList();
            dgvImagenes.DataSource = ListaImagen;
            dgvImagenes.Columns["ImagenUrl"].Width = 280;
            dgvImagenes.Columns["IdArticulo"].Width = 70;
            dgvImagenes.Columns["Id"].Visible = false;
            pbxImagen.Load(ListaImagen[0].ImagenUrl.ToString());

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            ListaArticulos = articuloNegocio.Listar()
                                    .OrderBy(a => a.Id) // Ordeno lista dgvArticulos por id
                                    .ToList();
            dgvArticulos.DataSource = ListaArticulos;
            dgvArticulos.Columns["Id"].Width = 50;
            dgvArticulos.Columns["Codigo"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Precio"].Visible = false;
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Marca"].Visible = false;
            dgvArticulos.Columns["Categoria"].Visible = false;
        }

        private void FrmImagen_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaImagen FrmAltaImagen = new FrmAltaImagen();
            FrmAltaImagen.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvImagenes.CurrentRow == null || dgvImagenes.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor seleccione una url de la lista",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
            ImagenNegocio negocio = new ImagenNegocio();
            Imagen seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Esta seguro que quiere eliminar este articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;
                    negocio.EliminarImagen(seleccionado.Id);
                    Cargar();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvImagenes.CurrentRow == null || dgvImagenes.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor seleccione un url de la lista",
                          "Selección requerida",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
                return;
            }
            Imagen seleccionada;
            seleccionada = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;

            FrmAltaImagen frmModificar = new FrmAltaImagen(seleccionada);
            frmModificar.ShowDialog();
            Cargar();
        }

        private void dgvImagenes_SelectionChanged(object sender, EventArgs e)
        {
            if (sincronizandoSeleccion) return;
            sincronizandoSeleccion = true;

            if (dgvImagenes.CurrentRow != null && dgvImagenes.CurrentRow.DataBoundItem != null)
            {
                Imagen seleccionada = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;

                if (seleccionada.ImagenUrl != null)
                {
                    string urlSeleccionada = seleccionada.ImagenUrl;

                    foreach (DataGridViewRow fila in dgvArticulos.Rows)
                    {
                        Articulo articulo = fila.DataBoundItem as Articulo;
                        if (articulo != null && articulo.UrlImagen != null && articulo.UrlImagen.ImagenUrl == urlSeleccionada)
                        {
                            fila.Selected = true;

                            foreach (DataGridViewCell celda in fila.Cells)
                            {
                                if (celda.Visible)
                                {
                                    dgvArticulos.CurrentCell = celda;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    cargarImagen(urlSeleccionada);
                }
            }

            sincronizandoSeleccion = false;
        }


        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch (Exception)
            {
                pbxImagen.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDQQGGvsya8PwOD-0KOh6bClw8zRFxEpUaIWvZawv5IdEHPdLMs6C4DalMrGeinUXpp4I&usqp=CAU1");
                //pbxImagen.Load("https://static.vecteezy.com/system/resources/thumbnails/008/695/917/small_2x/no-image-available-icon-simple-two-colors-template-for-no-image-or-picture-coming-soon-and-placeholder-illustration-isolated-on-white-background-vector.jpg");
                //throw;
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (sincronizandoSeleccion) return;
            sincronizandoSeleccion = true;

            if (dgvArticulos.CurrentRow != null && dgvArticulos.CurrentRow.DataBoundItem != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                if (seleccionado.UrlImagen != null)
                {
                    string urlSeleccionada = seleccionado.UrlImagen.ImagenUrl;

                    foreach (DataGridViewRow fila in dgvImagenes.Rows)
                    {
                        Imagen imagen = fila.DataBoundItem as Imagen;
                        if (imagen != null && imagen.ImagenUrl == urlSeleccionada)
                        {
                            fila.Selected = true;

                            foreach (DataGridViewCell celda in fila.Cells)
                            {
                                if (celda.Visible)
                                {
                                    dgvImagenes.CurrentCell = celda;
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    cargarImagen(urlSeleccionada);
                }
            }

            sincronizandoSeleccion = false;
        }
    }
}
