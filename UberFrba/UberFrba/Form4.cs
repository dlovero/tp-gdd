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
        public String tipoFuncion { set; get; }
        public int idTipoRol { set; get; }
        public int idPersona { set; get; }

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
                formularioResultadoBusqueda.formularioABM = this;
                formularioResultadoBusqueda.Show();
            } else {
                MessageBox.Show("No Existe " + this.tipoUsuario + " coincidente con los parametros de busqueda");
            }
            
        }

        public static void dispararVentanaConMensaje(String tipoUsuario, String tipoFuncion)
        {
            MessageBox.Show("¿Esta segura/o de " + tipoFuncion + " esta/e nueva/o " + tipoUsuario, tipoFuncion + " " + tipoUsuario,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        public void btnAceptar_click(object sender, EventArgs e)
        {
            if (verificarDatosNoSeanNulos())
            {
                dispararVentanaConMensaje(this.tipoUsuario, this.tipoFuncion);
                ajecutarFuncionConTipoUsuario();
            }
        }

        public void ajecutarFuncionConTipoUsuario()
        {
            GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.QueriesTableAdapter();
            switch (tipoFuncion)
            {
                case "Agregar":
                    if (tipoUsuario.Equals("Cliente"))
                    {
                            adaptador.agregarCliente
                            (Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtCalle.Text
                            , Convert.ToInt16(txtPisoManzana.Text), txtDeptoLote.Text, txtLocalidad.Text, txtCodigoPostal.Text
                            , Convert.ToInt32(txtTelefono.Text), txtCorreo.Text, Convert.ToDateTime(txtFechaNacimiento.Text));
                    }
                    else
                    {
                        adaptador.agregarChofer
                            (Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtCalle.Text
                            , Convert.ToInt16(txtPisoManzana.Text), txtDeptoLote.Text, txtLocalidad.Text, txtCodigoPostal.Text
                            , Convert.ToInt32(txtTelefono.Text), txtCorreo.Text, Convert.ToDateTime(txtFechaNacimiento.Text));
                    }
                    break;
                case "Eliminar":
                    switch (SingletonDatosUsuario.Instance.obtenerIdRol())
                    {
                        case 3:
                            adaptador.eliminarCliente
                                (SingletonDatosUsuario.Instance.obtenerIdTipoRol());
                            break;
                        case 2:
                            adaptador.eliminarChofer
                                (SingletonDatosUsuario.Instance.obtenerIdTipoRol());
                            break;
                        case 1:
                            if (tipoUsuario.Equals("Cliente"))
                            {
                                adaptador.eliminarCliente
                                    (idTipoRol);
                            } else {
                                adaptador.eliminarChofer
                                    (idTipoRol);
                            }
                            break;
                    }
                    break;
                case "Modificar":
                    
                    switch (SingletonDatosUsuario.Instance.obtenerIdRol())
                    {
                        case 3:
                            adaptador.modificarCliente
                            (SingletonDatosUsuario.Instance.obtenerIdPersona(),Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtCalle.Text
                            , Convert.ToInt16(txtPisoManzana.Text), txtDeptoLote.Text, txtLocalidad.Text, txtCodigoPostal.Text
                            , Convert.ToInt32(txtTelefono.Text), txtCorreo.Text, Convert.ToDateTime(txtFechaNacimiento.Text), ccHabilitado.Checked);
                            break;
                        case 2:
                            adaptador.modificarChofer
                            (SingletonDatosUsuario.Instance.obtenerIdPersona(), Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtCalle.Text
                            , Convert.ToInt16(txtPisoManzana.Text), txtDeptoLote.Text, txtLocalidad.Text, txtCodigoPostal.Text
                            , Convert.ToInt32(txtTelefono.Text), txtCorreo.Text, Convert.ToDateTime(txtFechaNacimiento.Text), ccHabilitado.Checked);
                            break;
                        case 1:
                            if (tipoUsuario.Equals("Cliente"))
                            {
                                
                                adaptador.modificarCliente
                           (idPersona,Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtCalle.Text
                           , Convert.ToInt16(txtPisoManzana.Text), txtDeptoLote.Text, txtLocalidad.Text, txtCodigoPostal.Text
                           , Convert.ToInt32(txtTelefono.Text), txtCorreo.Text, Convert.ToDateTime(txtFechaNacimiento.Text), ccHabilitado.Checked);
                            } else {
                                adaptador.modificarChofer
                            (idPersona,Convert.ToInt32(txtDNI.Text), txtNombre.Text, txtApellido.Text, txtCalle.Text
                            , Convert.ToInt16(txtPisoManzana.Text), txtDeptoLote.Text, txtLocalidad.Text, txtCodigoPostal.Text
                            , Convert.ToInt32(txtTelefono.Text), txtCorreo.Text, Convert.ToDateTime(txtFechaNacimiento.Text), ccHabilitado.Checked);
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
            
        }

        private Boolean verificarDatosNoSeanNulos()
        {
            Boolean resultado=true;
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (String.IsNullOrEmpty(textBox.Text) && !textBox.Name.Equals("txtCorreo"))
                    {
                        MessageBox.Show("El correo electronico es el unico dato opcional, el resto son obligatorios", "Datos requeridos",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultado= false;
                        break;
                    }
                }
            }
            return resultado;
        }

        public void btnEliminar_click(object sender, EventArgs e)
        {
            dispararVentanaConMensaje(this.tipoUsuario, this.tipoFuncion);
        }
        public void btnModificar_click(object sender, EventArgs e)
        {
            dispararVentanaConMensaje(this.tipoUsuario, this.tipoFuncion);
        }
        public void btnDefecto_click(object sender, EventArgs e)
        {
            MessageBox.Show("Excelente");
        }
    }
}
