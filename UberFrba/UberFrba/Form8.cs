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

        public override Boolean construite()
        {
            Boolean continua = construirComboChofer();
            if (continua)
            {
                desabilitarGrupoControlesDeBusqueda();
                construirComboTurno();
                construirComboMarca("grupoDatosAutomovil", "comboMarca");
                construirComboModelo();
                asociarModeloASeleccionDeMarca();
                construirBotonAccion();
            }
            return continua;
        }

        protected Boolean construirComboChofer()
        {
            GD1C2017DataSetTableAdapters.PRC_LISTA_CHOFERES_NO_ASIGTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTA_CHOFERES_NO_ASIGTableAdapter();
            DataTable tblChofer = adaptador.obtenerChoferesHabilitados();
            ComboBox frmAutomovilComboChofer = (ComboBox)this.Controls["comboChofer"];
            if (!MetodosGlobales.armarComboChofer(tblChofer, frmAutomovilComboChofer))
            {
                dispararMensajeYCancelarAccion();
                this.Close();
                return false;
            }
            return true;
        }

        public void dispararMensajeYCancelarAccion()
        {
            DialogResult resultado = MessageBox.Show("No hay choferes disponibles para asociar.", "Agregar Automovil",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
