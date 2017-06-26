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
        public frmResultadoBusquedaUsuarioABM()
        {
            InitializeComponent();
        }

        private void grillaDatosResultadoBusqueda_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            DataRowView row = ((DataRowView)(this.grillaDatosResultadoBusqueda.Rows[e.RowIndex]).DataBoundItem);
            String nombre = (String)row.Row["Persona_Nombre"];
            
           poblarDatosDelFormulario(formularioABM, row);
           this.Close();
        }

        public static void poblarDatosDelFormulario(Form formulario, DataRowView filadeDatos)
        {
            ((TextBox)formulario.Controls["txtNombre"]).Text = filadeDatos.Row["Persona_Nombre"].ToString();
            ((TextBox)formulario.Controls["txtApellido"]).Text = filadeDatos.Row["Persona_Apellido"].ToString();
            ((TextBox)formulario.Controls["txtDNI"]).Text = filadeDatos.Row["Persona_Dni"].ToString();
            ((TextBox)formulario.Controls["txtCorreo"]).Text = filadeDatos.Row["Persona_Mail"].ToString();
            ((TextBox)formulario.Controls["txtTelefono"]).Text = filadeDatos.Row["Persona_Telefono"].ToString();
            ((TextBox)formulario.Controls["txtLocalidad"]).Text = filadeDatos.Row["Persona_Localidad"].ToString();
            ((TextBox)formulario.Controls["txtCodigoPostal"]).Text = filadeDatos.Row["Persona_Cod_Postal"].ToString();
            ((TextBox)formulario.Controls["txtCalle"]).Text = filadeDatos.Row["Persona_Direccion"].ToString();
            ((TextBox)formulario.Controls["txtDeptoLote"]).Text = filadeDatos.Row["Persona_Departamento"].ToString();
            ((TextBox)formulario.Controls["txtPisoManzana"]).Text = filadeDatos.Row["Persona_Piso"].ToString();
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
