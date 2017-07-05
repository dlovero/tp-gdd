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
        public frmABMTurno frmTurno { set; get; }

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
            Form formulario;
            if (formularioABM != null)
            {
                formulario = formularioABM;
            }
            else
            {
                if (frmAutomovil != null)
                {
                    formulario = frmAutomovil;
                }
                else
                {
                    formulario = frmTurno;
                }
            }
            return (IGrilla)formulario;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            completarFormularioABMConDatosDeUsuarioSeleccionado();
        }
        //public void armarGrillaListadoChoferesMayorRecaudacion()
        //{
        //    GD1C2017DataSetTableAdapters.CHOFERES_MAYOR_RECAUDACIONTableAdapter adaptador
        //    = new GD1C2017DataSetTableAdapters.CHOFERES_MAYOR_RECAUDACIONTableAdapter();
        //    DataTable tblListadoChoferesConMayorRecaudacion = adaptador.listadoChoferesConMayorRecaudacion();
        //    enlazarGrillaConDatos(tblListadoChoferesConMayorRecaudacion);
        //}

        //private void enlazarGrillaConDatos(DataTable tblListadoChoferesConMayorRecaudacion)
        //{

        //    this.grillaDatosResultadoBusqueda.DataSource = tblListadoChoferesConMayorRecaudacion;
        //    this.grillaDatosResultadoBusqueda.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    this.grillaDatosResultadoBusqueda.AutoGenerateColumns = true;
        //    //this.grillaDatosResultadoBusqueda.formularioABM = this;
        //    this.grillaDatosResultadoBusqueda.Controls["btnSeleccionar"].Text = "Seleccionar " + this.tipoUsuario;
        //    this.grillaDatosResultadoBusqueda.Show();
        //}

    }
}
