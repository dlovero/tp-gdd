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
        public class Usuario
        {
            public String nombre {get;set;}
            public String apellido { get; set; }
            public String dni { get; set; }
            public String correo { get; set; }
            public String telefono { get; set; }
            public String localidad { get; set; }
            public String cofigoPostal { get; set; }
            public String calle { get; set; }
            public String departamentoLote { get; set; }
            public String pisoCasa { get; set; }
        }

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
