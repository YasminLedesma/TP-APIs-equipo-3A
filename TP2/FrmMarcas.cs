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
    public partial class FrmMarcas : Form
    {
        private List<Marca> ListaMarca;
        public FrmMarcas()
        {
            InitializeComponent();
        }

        private void Cargar()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            ListaMarca = negocio.Listar();
            dgvMarcas.DataSource = ListaMarca;
            dgvMarcas.Columns["Id"].Visible = false;
        }

        private void FrmMarcas_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaMarca FrmAltaMarca = new FrmAltaMarca();
            FrmAltaMarca.ShowDialog();
            Cargar();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null || dgvMarcas.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor seleccione una marca de la lista",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
            Marca seleccionada;
            seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;

            FrmAltaMarca frmModificar = new FrmAltaMarca(seleccionada);
            frmModificar.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null || dgvMarcas.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor seleccione una marca de la lista",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
            MarcaNegocio negocio = new MarcaNegocio();
            Marca seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Esta seguro que quiere eliminar este articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                    negocio.EliminarMarca(seleccionado.Id);
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
