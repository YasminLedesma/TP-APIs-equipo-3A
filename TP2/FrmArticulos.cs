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
using Tp2;

namespace TP2
{
    public partial class FrmArticulos : Form
    {
        private List<Articulo> listaArticulos;

        private void Cargar()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                /// DataGridView
                listaArticulos = negocio.Listar();
                dgvArticulos.DataSource = listaArticulos;

                ajustarColumnas();

                /// PictureBox
                //cargarImagen(listaArticulos[0].UrlImagen.ToString()); // Cargo imagen
                if (listaArticulos != null && listaArticulos.Any())
                {
                    dgvArticulos.ClearSelection();
                    dgvArticulos.Rows[0].Selected = true; // Seleccionar primera fila
                    cargarImagen(listaArticulos.First().UrlImagen?.ToString() ?? string.Empty);
                    pbxArticulo.Visible = true;
                }
                else
                {
                    pbxArticulo.Visible = false;
                }

                bool hayArticulos = listaArticulos != null && listaArticulos.Any();
                btnModificar.Enabled = hayArticulos;
                btnEliminar.Enabled = hayArticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //throw;
            }
        }
        public FrmArticulos()
        {
            InitializeComponent();
        }

        private void FrmArticulos_Load(object sender, EventArgs e)
        {
            Cargar();
            cboFiltro.Items.Add("Nombre");
            cboFiltro.Items.Add("Marca");
            cboFiltro.Items.Add("Categoria");
            cboFiltro.Items.Add("Precio");
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDQQGGvsya8PwOD-0KOh6bClw8zRFxEpUaIWvZawv5IdEHPdLMs6C4DalMrGeinUXpp4I&usqp=CAU");
                // CONSULTAR POR QUE DA WARNING
                //pbxArticulo.Load("https://static.vecteezy.com/system/resources/thumbnails/008/695/917/small_2x/no-image-available-icon-simple-two-colors-template-for-no-image-or-picture-coming-soon-and-placeholder-illustration-isolated-on-white-background-vector.jpg");
                //throw;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo frmAltaArticulo = new FrmAltaArticulo();
            frmAltaArticulo.ShowDialog();
            Cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null || dgvArticulos.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor seleccione un artículo de la lista",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
            Articulo articuloSeleccionado;
            articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            
            FrmAltaArticulo frmModificar = new FrmAltaArticulo(articuloSeleccionado);
            frmModificar.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null || dgvArticulos.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor seleccione un artículo de la lista",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Esta seguro que quiere eliminar este articulo?","Eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    articuloNegocio.eliminarArticulo(seleccionado.Id);
                    Cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                    return;

                string columna = cboFiltro.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string buscar = txtBuscar2.Text;

                dgvArticulos.DataSource = negocio.filtrar(columna, criterio, buscar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ajustarColumnas()
        {
            dgvArticulos.Columns["Descripcion"].Width = 150; // Ajusto ancho columna Descripcion
            dgvArticulos.Columns["Id"].Visible = false; // Oculto Columna Id
            dgvArticulos.Columns["UrlImagen"].Visible = false; // Oculto Columna ImagenUrl
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtBuscar.Text;

            if (filtro.Length > 1)
            {
                //Guarda coincidencias de txt.Buscar en dgv actual para luego guardar las coincidencias en listaFiltrada
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper())
                || x.Descripcion.ToUpper().Contains(filtro.ToUpper()));

                // Filtrado opcional por marca y categoria.
                //|| x.Marca.Descripcion.ToString().ToUpper().Contains(filtro.ToUpper()));
                //TODO Al filtrar por categoria pincha porque esta devolviendo NULL en algunos articulos cuya descripcion esta vacia en DGV.
                //|| x.Categoria.Descripcion.ToString().ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            //Limpiamos dgv actual para luego actualizarla con la listaFiltrada con las coincidencias halladas
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ajustarColumnas();

        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFiltro.SelectedIndex != -1) // Prueba manejo de posible null Por boton Limpiar
            {
                string opcion = cboFiltro.SelectedItem.ToString();
                // Criterio:
                if (opcion != "Precio")
                {
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Comienza con");
                    cboCriterio.Items.Add("Termina con");
                    cboCriterio.Items.Add("Contiene");
                }
                else
                {
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Igual a");
                    cboCriterio.Items.Add("Menor a");
                    cboCriterio.Items.Add("Mayor a");
                }
            }
        }

        private bool validarFiltro()
        {
            if (cboFiltro.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una de las opciones de Filtro");
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una de las opciones de Criterio");
                return true;
            }
            if (cboFiltro.SelectedItem.ToString() == "Precio")
            {
                //if (string.IsNullOrEmpty(txtBuscar2.Text))
                //{
                //    MessageBox.Show("Ingrese solo numeros en el campo a buscar");
                //    return true;
                //}
                if (!(soloNumeros(txtBuscar2.Text)))
                {
                    MessageBox.Show("Ingrese solo numeros en el campo a buscar");
                    return true;
                }
            }
            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Cargar();
            try
            {
                cboFiltro.SelectedIndex = -1;
                cboCriterio.SelectedIndex = -1;
                txtBuscar.Text = string.Empty;
                txtBuscar2.Text = string.Empty;
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaArticulos;
                ajustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //throw;
            }
        }

        private void TsCategorias_Click(object sender, EventArgs e)
        {
            FrmCategorias FrmCategoria = new FrmCategorias();
            FrmCategoria.ShowDialog();
            Cargar();
        }

        private void TsMarcas_Click(object sender, EventArgs e)
        {
            FrmMarcas frmMarcas = new FrmMarcas();
            frmMarcas.ShowDialog();
            Cargar();
        }

        private void MstCategorias_Click(object sender, EventArgs e)
        {
            FrmCategorias FrmCategoria = new FrmCategorias();
            FrmCategoria.ShowDialog();
            Cargar();
        }

        private void MtsMarcas_Click(object sender, EventArgs e)
        {
            FrmMarcas frmMarcas = new FrmMarcas();
            frmMarcas.ShowDialog();
            Cargar();
        }

        private void MstAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desarrollado por: \n\n" +
                            "Fogliatto Federico \n" +
                            "Ledesma Yanet \n" +
                            "Olguera Alejando \n\n" +
                            "Grupo 3A", "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            
        }

        private void iMAGENESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmImagen frmImagenes = new FrmImagen();
            frmImagenes.ShowDialog();
            Cargar();
        }
    }

    
}
