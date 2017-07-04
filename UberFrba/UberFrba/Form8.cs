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
    public partial class frmRegistroViaje : Form
    {
        public frmRegistroViaje()
        {
            InitializeComponent();
        }

        public Boolean construite()
        {
            Boolean continua = construirComboChofer();
            if (continua)
            {
                construirComboTurno();
                ((ComboBox)this.Controls["comboChofer"]).SelectedIndexChanged += (sender, e) => 
                comboChoferModificacionEnSeleccion(sender, e);
                //inhabilitarControles();
                //construirComboMarca("grupoDatosAutomovil", "comboMarca");
                //construirComboModelo();
                //asociarModeloASeleccionDeMarca();
                //construirBotonAccion();
            }
            return continua;
        }

        //private void inhabilitarControles()
        //{
        //    this.Controls["comboTurno"].Enabled=false;
        //}

        protected Boolean construirComboChofer()
        {
            GD1C2017DataSetTableAdapters.PRC_BUSCAR_CHOFER_HABILITADOTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_BUSCAR_CHOFER_HABILITADOTableAdapter();
            DataTable tblChofer = adaptador.obtenerListadoChoferesHabilitados();
            ComboBox frmRendirViajeComboChofer = (ComboBox)this.Controls["comboChofer"];
            if (!MetodosGlobales.armarComboChofer(tblChofer, frmRendirViajeComboChofer))
            {
                dispararMensajeYCancelarAccion();
                this.Close();
                return false;
            }
            return true;
        }

        public void dispararMensajeYCancelarAccion()
        {
            DialogResult resultado = MessageBox.Show("No hay choferes habilitados.", "Registrar Viaje",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private void comboChofer_SelectedIndexChanged(object sender, EventArgs e)
        private void comboChoferModificacionEnSeleccion(object sender, EventArgs e)
        {
            //this.Controls["comboTurno"].Enabled = true;
            //construirComboTurno();
            GD1C2017DataSetTableAdapters.PRC_LISTADO_UNI_DISPONIBLE_X_CHOTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTADO_UNI_DISPONIBLE_X_CHOTableAdapter();
            DataTable tblTurnosDisponibles = adaptador.obtenerListadoTurnosYAutomovilesSegunChofer((int)((ComboBox)this.Controls["comboChofer"]).SelectedValue);
            ComboBox frmAutomovilComboTurno = (ComboBox)this.Controls["comboTurno"];
            frmAutomovilComboTurno.DataSource = tblTurnosDisponibles;
            frmAutomovilComboTurno.DisplayMember = "Turno_Descripcion";
            frmAutomovilComboTurno.ValueMember = "Turno_Id";
            this.Controls["txtAutomovil"].Text = Convert.ToString(((DataRowView)frmAutomovilComboTurno.SelectedItem)["Auto_Detalle"]);
        }

        private void construirComboTurno()
        {
            GD1C2017DataSetTableAdapters.PRC_LISTADO_UNI_DISPONIBLE_X_CHOTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTADO_UNI_DISPONIBLE_X_CHOTableAdapter();
            DataTable tblTurnosDisponibles = adaptador.obtenerListadoTurnosYAutomovilesSegunChofer((int)((ComboBox)this.Controls["comboChofer"]).SelectedValue);
            ComboBox frmAutomovilComboTurno = (ComboBox)this.Controls["comboTurno"];
            frmAutomovilComboTurno.DataSource = tblTurnosDisponibles;
            frmAutomovilComboTurno.DisplayMember = "Turno_Descripcion";
            frmAutomovilComboTurno.ValueMember = "Turno_Id";
            this.Controls["txtAutomovil"].Text = Convert.ToString(((DataRowView)frmAutomovilComboTurno.SelectedItem)["Auto_Detalle"]);
        }

        private void comboTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox frmAutomovilComboTurno = (ComboBox)this.Controls["comboTurno"];
            this.Controls["txtAutomovil"].Text = Convert.ToString(((DataRowView)frmAutomovilComboTurno.SelectedItem)["Auto_Detalle"]);
        }
    }
}
