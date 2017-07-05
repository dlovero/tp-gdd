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
    public partial class frmListados : Form
    {
        public frmListados()
        {
            InitializeComponent();
            this.selectorDiaHoraInicio.MaxDate = DateTime.Now;
            armarComboListados();
        }

        private void armarComboListados()
        {
            var diccionarioDatosListado = new Dictionary<String, String>();
            diccionarioDatosListado.Add("listadoChoferConMayorRecaudacion",
                "Chóferes con mayor recaudación");
            diccionarioDatosListado.Add("listadoChoferConViajeMasLargoRealizado",
                "Choferes con el viaje más largo realizado");
            diccionarioDatosListado.Add("listadoClienteConMayorConsumo",
                "Clientes con mayor consumo");
            diccionarioDatosListado.Add("listadoClienteUtilizoMasVecesElMismoAuto",
                "Cliente que utilizo más veces mismo automóvil");
            this.comboListados.DataSource = new BindingSource(diccionarioDatosListado, null);
            this.comboListados.DisplayMember = "Value";
            this.comboListados.ValueMember = "Key";
        }

        public void construite()
        {
            this.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
