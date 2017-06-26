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
        public String tipoUsuario {set;get;}

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
                formularioResultadoBusqueda.Show();
            } else {
                MessageBox.Show("No Existe " + this.tipoUsuario + " coincidente con los parametros de busqueda");
            }
            
            //poblarDatosDelFormulario(formulario, adaptador);
            //((frmABM)formulario).adaptadorDatosUsuarios = adaptador;
        }

        // public static void poblarDatosDelFormulario(Form formulario, GD1C2017DataSetTableAdapters.PRC_OBTENER_DATOS_USUARIOSTableAdapter adaptador)
        /* public static void poblarDatosDelFormulario(Form formulario)
         {
             ((TextBox)formulario.Controls["txtNombre"]).Text = usuario["Persona_Nombre"].ToString();
             ((TextBox)formulario.Controls["txtApellido"]).Text = usuario["Persona_Apellido"].ToString();
             ((TextBox)formulario.Controls["txtDNI"]).Text = usuario["Persona_Dni"].ToString();
             ((TextBox)formulario.Controls["txtCorreo"]).Text = usuario["Persona_Mail"].ToString();
             ((TextBox)formulario.Controls["txtTelefono"]).Text = usuario["Persona_Telefono"].ToString();
             ((TextBox)formulario.Controls["txtLocalidad"]).Text = usuario["Persona_Localidad"].ToString();
             ((TextBox)formulario.Controls["txtCodigoPostal"]).Text = usuario["Persona_Cod_Postal"].ToString();
             ((TextBox)formulario.Controls["txtCalle"]).Text = usuario["Persona_Direccion"].ToString();
             ((TextBox)formulario.Controls["txtDeptoLote"]).Text = usuario["Persona_Departamento"].ToString();
             ((TextBox)formulario.Controls["txtPisoManzana"]).Text = usuario["Persona_Piso"].ToString();
         }*/

    }
}
