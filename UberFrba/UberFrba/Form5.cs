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

        public frmResultadoBusquedaUsuarioABM()
        {
            InitializeComponent();
        }

        private void grillaDatosResultadoBusqueda_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            DataGridViewRow row = this.grillaDatosResultadoBusqueda.Rows[e.RowIndex];
            datosUsuarioResultadoBusqueda datosUsuario = row.DataBoundItem as datosUsuarioResultadoBusqueda;
            MessageBox.Show(datosUsuario.Persona_Apellido, "CellContentDoubleClick Event");
        }

        private class datosUsuarioResultadoBusqueda
        {
            public String Persona_Nombre { get; set; }
            public String Persona_Apellido { get; set; }
            public String Persona_Dni { get; set; }
            public String Persona_Localidad { get; set; }
            public String Persona_Cod_Postal { get; set; }
            public String Persona_Direccion { get; set; }
            public String Persona_Piso { get; set; }
            public String Persona_Departamento { get; set; }
            public String Persona_Telefono { get; set; }
            public String Persona_Mail { get; set; }
            public String Persona_Fecha_Nacimiento { get; set; }
        }
    }

}
