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
    public partial class frmABM : Form, IGrilla
    {
        public String tipoUsuario {set;get;}
        public String tipoFuncion { set; get; }
        public int idTipoRol { set; get; }
        public int idPersona { set; get; }

        public frmABM()
        {
            InitializeComponent();
            this.selectorFechaNacimiento.MaxDate = DateTime.Now.AddYears(-18);
            this.selectorFechaNacimiento.Value = DateTime.Now.AddYears(-18);
            this.selectorFechaNacimiento.Format = DateTimePickerFormat.Custom;
            this.selectorFechaNacimiento.CustomFormat = "dd 'de' MMMM 'de' yyyy";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            GD1C2017DataSetTableAdapters.PRC_OBTENER_DATOS_USUARIOSTableAdapter adaptador
                = new GD1C2017DataSetTableAdapters.PRC_OBTENER_DATOS_USUARIOSTableAdapter();
            DataTable tblDatosResultadoBusquedaUsuarios;
            if (string.IsNullOrEmpty(this.txtBusquedaDNI.Text))
            {
                tblDatosResultadoBusquedaUsuarios = adaptador.obtenerDatosUsuario(this.tipoUsuario, this.txtBusquedaNombre.Text, this.txtBusquedaApellido.Text, null);
            }
            else
            {
                tblDatosResultadoBusquedaUsuarios = adaptador.obtenerDatosUsuario(this.tipoUsuario, this.txtBusquedaNombre.Text, this.txtBusquedaApellido.Text, int.Parse(this.txtBusquedaDNI.Text));
            }
            if (tblDatosResultadoBusquedaUsuarios != null && tblDatosResultadoBusquedaUsuarios.Rows.Count > 0)
            {
                frmResultadoBusquedaUsuarioABM formularioResultadoBusqueda = new frmResultadoBusquedaUsuarioABM();
                DataGridView grillaBusquedaUsuarios = (DataGridView)formularioResultadoBusqueda.Controls["grillaDatosResultadoBusqueda"];
                grillaBusquedaUsuarios.DataSource = tblDatosResultadoBusquedaUsuarios;
                grillaBusquedaUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grillaBusquedaUsuarios.AutoGenerateColumns = true;
                formularioResultadoBusqueda.formularioABM = this;
                formularioResultadoBusqueda.Controls["btnSeleccionar"].Text = "Seleccionar "+this.tipoUsuario;
                formularioResultadoBusqueda.Show();
            } else {
                MessageBox.Show("No Existe " + this.tipoUsuario + " coincidente con los parametros de busqueda");
            }
            
        }

        //public static Boolean mensajeAlertaAntesDeAccion(String rol, String funcion)
        //{
        //    DialogResult resultado = MessageBox.Show("¿Esta segura/o de " + funcion + " esta/e nueva/o " + rol, funcion + " " + rol,
        //        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        //    return (resultado == DialogResult.Yes);
        //}

        public static void mensajeAutoEliminacionYSalidaDeAplicacion(String funcion, String usuario)
        {
            DialogResult resultado = MessageBox.Show("¿Esta segura/o de " + funcion + " esta/e nueva/o " + usuario, funcion + " " + usuario,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show("La aplicacion se cerrara, debido a que usted dio de baja su rol de " + usuario +
                " si posee otro rol, debera iniciar e ingresar nuevamente al sistema con otro rol.", "Salida de la aplicacion",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }

        //public Boolean verificarDatosNoSeanNulos()
        //{
        //    Boolean resultado=true;
        //    foreach (Control c in this.Controls)
        //    {
        //        if (c is TextBox)
        //        {
        //            TextBox textBox = c as TextBox;
        //            if (String.IsNullOrEmpty(textBox.Text) && !textBox.Name.Equals("txtCorreo"))
        //            {
        //                MessageBox.Show("El correo electronico es el unico dato opcional, el resto son obligatorios", "Datos requeridos",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                resultado= false;
        //                break;
        //            }
        //        }
        //    }
        //    return resultado;
        //}

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void completarFormularioConDatosDeUsuarioSeleccionado(DataRowView filaDeDatos)
        {
            String nombre = (String)filaDeDatos.Row["Persona_Nombre"];
            poblarDatosDelFormulario(this.FindForm(), filaDeDatos);
            (this.FindForm().Controls["grupoDatosPersona"]).Enabled = true;
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
    }
}
