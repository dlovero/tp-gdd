using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UberFrba
{
    public partial class frmResultadoBusquedaUsuarioABM : Form
    {
        public frmABM formularioABM { set; get; }
        public frmAutomovil frmAutomovil { set; get; }
        public frmResultadoBusquedaUsuarioABM()
        {
            InitializeComponent();
        }

        private void grillaDatosResultadoBusqueda_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            completarFormularioABMConDatosDeUsuarioSeleccionado();
        }

        private void completarFormularioABMConDatosDeUsuarioSeleccionado()
        {
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            DataRowView row = ((DataRowView)(this.grillaDatosResultadoBusqueda.CurrentRow).DataBoundItem);

            obtenerFormulario().completarFormularioConDatosDeUsuarioSeleccionado(row);

            this.Close();
        }

        private IGrilla obtenerFormulario()
        {
            return (formularioABM != null) ? (IGrilla)formularioABM : (IGrilla)frmAutomovil;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            completarFormularioABMConDatosDeUsuarioSeleccionado();
        }
    }
}
