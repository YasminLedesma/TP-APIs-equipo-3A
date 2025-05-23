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
using Tp2;

namespace TP2
{
    public partial class FrmCategorias : Form
    {
        private List<Categoria> listaCategoria;
        public FrmCategorias()
        {
            InitializeComponent();
        }

        private void Cargar()
        {
            try
            {
                Categoria categorias = new Categoria();
                CategoriaNegocio negocio = new CategoriaNegocio();

                listaCategoria = negocio.Listar();
                dgvCategorias.DataSource = listaCategoria;
                dgvCategorias.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            Cargar();

        }
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            FrmAltaCategoria frmAltaCategoria = new FrmAltaCategoria();
            frmAltaCategoria.ShowDialog();
            Cargar();
        }

        private void btnModificarCategoria_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null || dgvCategorias.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor seleccione una categoria de la lista",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
            Categoria categoriaSeleccionada;
            categoriaSeleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;

            FrmAltaCategoria frmModificar = new FrmAltaCategoria(categoriaSeleccionada);
            frmModificar.ShowDialog();
            Cargar();
        }

        private void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null || dgvCategorias.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor seleccione una categoria de la lista",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            Categoria seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Esta seguro que quiere eliminar este articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
                    categoriaNegocio.EliminarCategoria(seleccionado.Id);
                    Cargar();
                }
            }
            catch (Exception ex)
            {

               throw ex;
            }
        }
    }
}
