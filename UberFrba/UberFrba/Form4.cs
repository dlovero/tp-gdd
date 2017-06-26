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
    public partial class frmABM : Form
    {
        public GD1C2017DataSetTableAdapters.PRC_OBTENER_DATOS_USUARIOSTableAdapter adaptadorDatosUsuarios { set; get; }

        public frmABM()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmABM_Load(object sender, EventArgs e)
        {

        }

        private void txtLocalidad_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (adaptadorDatosUsuarios != null)
            {
                frmRoles.poblarDatosDelFormulario(this, adaptadorDatosUsuarios);
            }
        }
    }
}
