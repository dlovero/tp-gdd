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
            completarFormularioABMConDatosDeUsuarioSeleccionado();
        }

        private void completarFormularioABMConDatosDeUsuarioSeleccionado()
        {
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            DataRowView row = ((DataRowView)(this.grillaDatosResultadoBusqueda.CurrentRow).DataBoundItem);
            String nombre = (String)row.Row["Persona_Nombre"];
            poblarDatosDelFormulario(formularioABM, row);
            (formularioABM.Controls["grupoDatosPersona"]).Enabled = true;
            this.Close();
        }

        public static void poblarDatosDelFormulario(Form formulario, DataRowView filadeDatos)
        {
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtNombre"]).Text = filadeDatos.Row["Persona_Nombre"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtApellido"]).Text = filadeDatos.Row["Persona_Apellido"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtDNI"]).Text = filadeDatos.Row["Persona_Dni"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtCorreo"]).Text = filadeDatos.Row["Persona_Mail"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtTelefono"]).Text = filadeDatos.Row["Persona_Telefono"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtLocalidad"]).Text = filadeDatos.Row["Persona_Localidad"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtCodigoPostal"]).Text = filadeDatos.Row["Persona_Cod_Postal"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtCalle"]).Text = filadeDatos.Row["Persona_Direccion"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtDeptoLote"]).Text = filadeDatos.Row["Persona_Departamento"].ToString();
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtPisoManzana"]).Text = filadeDatos.Row["Persona_Piso"].ToString();
            ((DateTimePicker)(formulario.Controls["grupoDatosPersona"]).Controls["selectorFechaNacimiento"]).Value = Convert.ToDateTime(filadeDatos.Row["Persona_Fecha_Nac"].ToString());
            ((CheckBox)(formulario.Controls["grupoDatosPersona"]).Controls["ccHabilitado"]).Checked = (Boolean)filadeDatos.Row["habilitado"];
            ((frmABM)formulario).idTipoRol = (int)filadeDatos.Row["idTipoRol"];
            ((frmABM)formulario).idPersona = (int)filadeDatos.Row["Persona_Id"];
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
