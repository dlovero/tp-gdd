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
    public partial class frmFacturarViaje : Form
    {
        public frmFacturarViaje()
        {
            InitializeComponent();
            this.selectorFechaFacturacionHasta.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); 
            this.selectorFechaFacturacionHasta.MaxDate = DateTime.Now;
        }

        public Boolean construite()
        {
            return construirComboCliente();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador =
                new GD1C2017DataSetTableAdapters.QueriesTableAdapter();
            adaptador.insertarFactura(
                (int)this.comboCliente.SelectedValue,
                this.selectorFechaFacturacionHasta.Value);
            this.Close();
        }

        private Boolean construirComboCliente()
        {
            GD1C2017DataSetTableAdapters.PRC_BUSCAR_CLIENTE_HABILITADOTableAdapter adaptador
                   = new GD1C2017DataSetTableAdapters.PRC_BUSCAR_CLIENTE_HABILITADOTableAdapter();
            DataTable tblCliente = adaptador.obtenerListadoClientesHabilitados();
            ComboBox frmFacturarViajeComboCliente = (ComboBox)this.Controls["comboCliente"];
            if (!MetodosGlobales.armarComboSeleccionSegunRol(tblCliente, frmFacturarViajeComboCliente))
            {
                MetodosGlobales.dispararMensajeYCancelarAccion("Clientes", "Factura Viajes");
                this.Close();
                return false;
            }
            return true;
        }
    }
}
