using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace UberFrba
{
    public partial class frmIngreso : Form
    {
        static string sha256(string clave)
        {
            System.Security.Cryptography.SHA256Managed encriptador
                = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] claveEncriptada
                = encriptador.ComputeHash(Encoding.UTF8.GetBytes(clave), 0, Encoding.UTF8.GetByteCount(clave));
            foreach (byte unByte in claveEncriptada)
            {
                hash.Append(unByte.ToString("x2"));
            }
            return hash.ToString();
        }

        class Usuario
        {
            private int idUsuario;
            private List<Tuple<int, String>> roles;

            public int obtenerIdUsuario()
            {
                return idUsuario;
            }

            public void setearIdUsuario(int usuario)
            {
                idUsuario = usuario;
            }

            public void agregarRol(int idRol, String rol)
            {
                roles.Add(new Tuple<int, String>(idRol, rol));
            }

            public List<Tuple<int, String>> obtenerRoles()
            {
                return roles;
            }
        }

        public frmIngreso()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (validarDatosDelFormulario())
            {
                GD1C2017DataSetTableAdapters.PRC_VALIDAR_USUARIOTableAdapter adaptador
                        = new GD1C2017DataSetTableAdapters.PRC_VALIDAR_USUARIOTableAdapter();
                DataTable tblUsuarioYRoles = adaptador.validarUsuario(textoUsuario.Text, sha256(textoClave.Text));
                List<Tuple<String, String>> roles = new List<Tuple<string, string>>();

                int codigoUsuario = tblUsuarioYRoles.Rows[0].Field<int>("UserId");
                String nombreUsuario = tblUsuarioYRoles.Rows[0].Field<String>("Nombre");
                String apellidoUsuario = tblUsuarioYRoles.Rows[0].Field<String>("Apellido");
                int idPersona = tblUsuarioYRoles.Rows[0].Field<int>("idPersona");

                switch (codigoUsuario)
                {
                    case -1:
                        MessageBox.Show("Usuario no existe.");
                        break;
                    case -2:
                        MessageBox.Show("Usuario Bloqueado.");
                        break;
                    case -3:
                        MessageBox.Show("Usuario o Clave Incorrecta.");
                        break;
                    default:
                        this.Hide();
                        SingletonDatosUsuario datosUsuario = new SingletonDatosUsuario(codigoUsuario, textoUsuario.Text, nombreUsuario, apellidoUsuario, idPersona);
                        frmRoles fmRoles = new frmRoles();
                        ((ComboBox)fmRoles.Controls["comboRol"]).Focus();
                        ComboBox frmRolComboRol = (ComboBox)fmRoles.Controls["comboRol"];
                        frmRolComboRol.DataSource = tblUsuarioYRoles;
                        frmRolComboRol.DisplayMember = "Rol_Nombre";
                        frmRolComboRol.ValueMember = "Rol_Id";
                        fmRoles.Show();
                        break;
                }
            } else {
                MetodosGlobales.mansajeErrorValidacion();
            }
        }

        private bool validarDatosDelFormulario()
        {
            return (Validaciones.validarCampoAlfanumerico(textoUsuario.Text) 
                && Validaciones.validarCampoClave(textoClave.Text));
        }
    }

    public class SingletonDatosUsuario
    {
        public class DatosUsuario
        {
            private int idPersona;
            public int IdPersona
            {
                get { return idPersona; }
                set { idPersona = value; }
            }
            private int idUsuario;
            public int IdUsuario
            {
                get { return idUsuario; }
                set { idUsuario = value; }
            }
            private int rolId;
            public int RolId
            {
                get { return rolId; }
                set { rolId = value; }
            }
            private int idTipoRol;
            public int IdTipoRol
            {
                get { return idTipoRol; }
                set { idTipoRol = value; }
            }
            private String nombreUsuario;
            public String NombreUsuario
            {
                get { return nombreUsuario; }
                set { nombreUsuario = value; }
            }
            private String nombre;
            public String Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }
            private String apellido;
            public String Apellido
            {
                get { return apellido; }
                set { apellido = value; }
            }
        }
        private static SingletonDatosUsuario instance;
        private DatosUsuario datosUsuario;
        public IFuncionalidadRoles rol {set; get;}

        public SingletonDatosUsuario() { }
        public SingletonDatosUsuario(int id, String nombreUsuario, String nombre, String apellido, int idPersona)
        {
            datosUsuario = new DatosUsuario();
            this.datosUsuario.IdUsuario = id;
            this.datosUsuario.NombreUsuario = nombreUsuario;
            this.datosUsuario.Nombre = nombre;
            this.datosUsuario.Apellido = apellido;
            this.datosUsuario.IdPersona = idPersona;
            instance = this;
        }

        public void configurarRol(int idRol, String nombreRol)
        {
            //TODO:MEJORAR para que no dependa del harcodeo por string "ADMINISTRATIVO"
            rol = soyRolAdministrador(nombreRol) ? (IFuncionalidadRoles)new RolAdministrador(idRol, this.datosUsuario.IdUsuario, nombreRol) : (IFuncionalidadRoles)new RolGenerico(idRol, this.datosUsuario.IdUsuario, nombreRol);
        }

        public bool soyRolAdministrador(string nombreRol)
        {
            return nombreRol.ToUpper().Equals(VariablesGlobales.NOMBRE_ROL_ADMINISTRADOR);
        }

        public int obtenerIdUsuario()
        {
            return this.datosUsuario.IdUsuario;
        }

        public int obtenerIdTipoRol()
        {
            return this.datosUsuario.IdTipoRol;
        }

        public int obtenerIdRol()
        {
            return this.datosUsuario.RolId;
        }

        public void setearIdPersona(int idPersona)
        {
            this.datosUsuario.IdPersona = idPersona;
        }

        public int obtenerIdPersona()
        {
            return this.datosUsuario.IdPersona;
        }

        public void setearRolId(int rolId)
        {
            this.datosUsuario.RolId = rolId;
        }

        public void setearIdTipoRol(int idTipoRol)
        {
            this.datosUsuario.IdTipoRol = idTipoRol;
        }

        public static SingletonDatosUsuario Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonDatosUsuario();
                }
                return instance;
            }
        }
    }

    public interface IFuncionalidadRoles
    {
        Boolean soyAdministrador();
        void agregarClienteChofer(String rol);
        void eliminarClienteChofer(String rol);
        void modificarClienteChofer(String rol);
        void agregarAutomovil(String rol);
        void eliminarAutomovil(String rol);
        void modificarAutomovil(String rol);
        void agregarTurno(String rol);
        void eliminarTurno(String rol);
        void modificarTurno(String rol);
        void accionBotonAutomovil(object sender, EventArgs e, frmAutomovil formulario, String funcion, String rol, object datos);
        void accionBotonTurno(object sender, EventArgs e, frmABMTurno formulario, string funcion, string rol, object datos);
        void accionBotonClienteChofer(object sender, EventArgs e, frmABM formulario, string funcion, string rol, object datos);
    }

    public abstract class FuncionalidadSegunRol : IFuncionalidadRoles
    {
        public FuncionalidadSegunRol(int idRol, int idUsuario, String nombreRol)
        {
            this.idRol = idRol;
            this.idTipoRol = obtenerIdTipoRol(idRol, idUsuario); ;
            this.nombreRol = nombreRol;
        }

        private int idRol;
        public int IdRol
        {
            get { return idRol; }
            set { idRol = value; }
        }

        private int idTipoRol;
        public int IdTipoRol
        {
            get { return idTipoRol; }
            set { idTipoRol = value; }
        }

        private String nombreRol;
        public String NombreRol
        {
            get { return nombreRol; }
            set { nombreRol = value; }
        }

        private int obtenerIdTipoRol(int idRol, int idUsuario)
        {
            return (int)(new GD1C2017DataSetTableAdapters.PRC_OBTENER_ID_CLIENTE_O_CHOFERTableAdapter())
                .obtenerIdEnTablaClienteOChofer(idUsuario, IdRol);
        }

        public abstract Boolean soyAdministrador();
        public abstract void agregarClienteChofer(String rol);
        public abstract void eliminarClienteChofer(String rol);
        public abstract void modificarClienteChofer(String rol);
        public abstract void agregarAutomovil(String rol);
        public abstract void eliminarAutomovil(String rol);
        public abstract void modificarAutomovil(String rol);
        public abstract void agregarTurno(String rol);
        public abstract void eliminarTurno(String rol);
        public abstract void modificarTurno(String rol);
        public abstract void accionBotonAutomovil(object sender, EventArgs e, frmAutomovil formulario, String funcion, String rol, object datos);
        public abstract void accionBotonTurno(object sender, EventArgs e, frmABMTurno formulario, string funcion, string rol, object datos);
        public abstract void accionBotonClienteChofer(object sender, EventArgs e, frmABM formulario, string funcion, string rol, object datos);
       
        public void mensajeErrorEnDB()
        {
            MessageBox.Show("Error al operar en la BD", "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected Control.ControlCollection obtenerGrupoControlesDeFormularioABM(frmABM formulario, String grupoControles)
        {
            return (formulario.Controls[grupoControles]).Controls;
        }

        protected GD1C2017DataSetTableAdapters.QueriesTableAdapter obtenerAdaptadorBD()
        {
            return new GD1C2017DataSetTableAdapters.QueriesTableAdapter();
        }

        protected void ejecutarMetodoDeAccionConParametros(MethodInfo methodInfo, object[] objParametros)
        {
            methodInfo.Invoke(this, objParametros);
        }

        protected MethodInfo obtenerNombreMetodo(string funcion, string rol)
        {
            return this.GetType().GetMethod(funcion.ToLower() + rol + "EnBD");
        }
    }

    public class ArgumentosParaEventoBotonAgregar : EventArgs
    {
        public frmABM formulario { set; get; }
    }
    
    public class RolAdministrador : FuncionalidadSegunRol
    {
        public RolAdministrador(int idRol, int idUsuario, String nombreRol)
            : base(idRol, idUsuario, nombreRol)
        { }

        public override Boolean soyAdministrador() { return true; }
        public override void agregarClienteChofer(String rol)
        {
            construirFormularioClienteChofer(new frmClienteChoferAgregar(), rol);
        }

        private void construirFormularioClienteChofer(frmABM frmClienteChoferAgregar, String rolParaAlta)
        {
            if (frmClienteChoferAgregar.construite(rolParaAlta))
            {
                frmClienteChoferAgregar.Show();
            }
        }

        public override void eliminarClienteChofer(String rol)
        {
            construirFormularioClienteChofer(new frmClienteChoferEliminar(), rol);
        }

        public override void modificarClienteChofer(String rol)
        {
            construirFormularioClienteChofer(new frmClienteChoferModificar(), rol);
        }

        public override void agregarAutomovil(String rol)
        {
            construirFormularioAutomovil(new frmAutomovilAgregar());
        }

        public override void accionBotonAutomovil(object sender, EventArgs e, frmAutomovil formulario, string funcion, string rol, object datos)
        {
            //if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulos,
            //                                            MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
            if (formulario.verificarDatosDeFormulario())
            {
                if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
                {
                    ejecutarMetodoDeAccionConParametros(
                        obtenerNombreMetodo(funcion, rol),
                        new object[] { 
                            datos
                            ,obtenerAdaptadorBD() });
                    formulario.Close();
                }
            }
            else
            {
                MessageBox.Show(MetodosGlobales.Mensajes.mensajeDatosNulos,
                     MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public override void accionBotonTurno(object sender, EventArgs e, frmABMTurno formulario, string funcion, string rol, object datos)
        {
            //if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulos,
            //                                            MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
            if (formulario.verificarDatosDeFormulario())
            {
                if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
                {
                    ejecutarMetodoDeAccionConParametros(
                        obtenerNombreMetodo(funcion, rol),
                        new object[] { 
                            datos
                            ,obtenerAdaptadorBD() });
                    formulario.Close();
                }
            }
            else
            {
                MessageBox.Show(MetodosGlobales.Mensajes.mensajeDatosNulos,
                     MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public override void accionBotonClienteChofer(object sender, EventArgs e, frmABM formulario, string funcion, string rol, object datos)
        {
            //if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulos,
            //                                            MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
            if (formulario.verificarDatosDeFormulario())
            {
                if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
                {
                    ejecutarMetodoDeAccionConParametros(
                        obtenerNombreMetodo(funcion, rol),
                        new object[] { 
                            datos
                            ,obtenerAdaptadorBD() });
                    formulario.Close();
                }
            }
            else
            {
                MessageBox.Show( MetodosGlobales.Mensajes.mensajeDatosNulos,
                     MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos,
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public override void eliminarAutomovil(String rol)
        {
            construirFormularioAutomovil(new frmAutomovilEliminar());
        }

        private static void construirFormularioAutomovil(frmAutomovil frmAutomovil)
        {
            if (frmAutomovil.construite())
            {
                frmAutomovil.Show();
            }
        }

        public override void modificarAutomovil(String rol)
        {
            construirFormularioAutomovil(new frmAutomovilModificar());
        }

        //private void accionBotonAgregar(object sender, EventArgs e, frmABM formulario, string funcion, string rol)
        //{
        //    if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulosAltaClienteChofer,
        //                                                MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
        //    {
        //        if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
        //        {
        //            ejecutarMetodoDeAccionConParametros(obtenerNombreMetodo(funcion, rol), new object[] { 
        //                obtenerGrupoControlesDeFormularioABM(formulario, "grupoDatosPersona")
        //                ,obtenerAdaptadorBD() });
        //            formulario.Close();
        //        }
        //    }
        //}

        //private void accionBotonEliminar(object sender, EventArgs e, frmABM formulario, string funcion, string rol)
        //{
        //    if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
        //    {
        //        ejecutarMetodoDeAccionConParametros(
        //            obtenerNombreMetodo(funcion, rol),
        //            new object[] { formulario.idTipoRol, obtenerAdaptadorBD() });
        //        formulario.Close();
        //    }
        //}

        //private void accionBotonModificar(object sender, EventArgs e, frmABM formulario, string funcion, string rol)
        //{
        //    if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulosAltaClienteChofer,
        //                                                MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
        //    {
        //        if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
        //        {
        //            ejecutarMetodoDeAccionConParametros(
        //                obtenerNombreMetodo(funcion, rol),
        //                new object[] { 
        //                obtenerGrupoControlesDeFormularioABM(formulario, "grupoDatosPersona"), 
        //                obtenerAdaptadorBD(), formulario.idPersona });
        //            formulario.Close();
        //        }
        //    }
        //}

        public void agregarAutomovilEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.agregarAutomovil
                            (Convert.ToInt32(((ComboBox)c["comboMarca"]).SelectedValue),
                            Convert.ToInt32(((ComboBox)c["comboModelo"]).SelectedValue),
                            c["txtPatente"].Text,
                            Convert.ToInt32(((ComboBox)c["comboTurno"]).SelectedValue),
                            Convert.ToInt32(((ComboBox)c["comboChofer"]).SelectedValue));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void eliminarAutomovilEnBD(int idAuto, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.eliminarAutomovil
                            (idAuto);
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void modificarAutomovilEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.modificarAutomovil
                            (Convert.ToInt32(((ComboBox)c["comboMarca"]).SelectedValue),
                            Convert.ToInt32(((ComboBox)c["comboModelo"]).SelectedValue),
                            c["txtPatente"].Text,
                            Convert.ToInt32(((ComboBox)c["comboTurno"]).SelectedValue),
                            Convert.ToInt32(((ComboBox)c["comboChofer"]).SelectedValue),
                            Convert.ToBoolean(((CheckBox)c["ccHabilitado"]).Checked));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void agregarClienteEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.agregarCliente
                            (Convert.ToInt32(c["txtDNI"].Text), c["txtNombre"].Text, c["txtApellido"].Text, c["txtCalle"].Text
                            , Convert.ToInt16(c["txtPisoManzana"].Text), c["txtDeptoLote"].Text, c["txtLocalidad"].Text, c["txtCodigoPostal"].Text
                            , Convert.ToInt32(c["txtTelefono"].Text), c["txtCorreo"].Text, Convert.ToDateTime(((DateTimePicker)c["selectorFechaNacimiento"]).Value));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
            mensajeCreacionDeUsuario(c["txtNombre"].Text, c["txtApellido"].Text);
        }

        public void agregarChoferEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.agregarChofer
                            (Convert.ToInt32(c["txtDNI"].Text), c["txtNombre"].Text, c["txtApellido"].Text, c["txtCalle"].Text
                            , Convert.ToInt16(c["txtPisoManzana"].Text), c["txtDeptoLote"].Text, c["txtLocalidad"].Text, c["txtCodigoPostal"].Text
                            , Convert.ToInt32(c["txtTelefono"].Text), c["txtCorreo"].Text, Convert.ToDateTime(((DateTimePicker)c["selectorFechaNacimiento"]).Value));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
            mensajeCreacionDeUsuario(c["txtNombre"].Text, c["txtApellido"].Text);
        }

        public void eliminarClienteEnBD(int idTipoRol, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.eliminarCliente(idTipoRol);
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void eliminarChoferEnBD(int idTipoRol, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.eliminarChofer(idTipoRol);
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void modificarClienteEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.modificarCliente
                            (Convert.ToInt32(c["lblIdPersona"].Text), Convert.ToInt32(c["txtDNI"].Text), c["txtNombre"].Text, c["txtApellido"].Text, c["txtCalle"].Text
                            , Convert.ToInt16(c["txtPisoManzana"].Text), c["txtDeptoLote"].Text, c["txtLocalidad"].Text, c["txtCodigoPostal"].Text
                            , Convert.ToInt32(c["txtTelefono"].Text), c["txtCorreo"].Text, ((DateTimePicker)c["selectorFechaNacimiento"]).Value
                            , Convert.ToBoolean(((CheckBox)c["ccHabilitado"]).Checked));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }


        public void modificarChoferEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador, int idPersona)
        {
            try
            {
                adaptador.modificarChofer
                            (idPersona, Convert.ToInt32(c["txtDNI"].Text), c["txtNombre"].Text, c["txtApellido"].Text, c["txtCalle"].Text
                            , Convert.ToInt16(c["txtPisoManzana"].Text), c["txtDeptoLote"].Text, c["txtLocalidad"].Text, c["txtCodigoPostal"].Text
                            , Convert.ToInt32(c["txtTelefono"].Text), c["txtCorreo"].Text, ((DateTimePicker)c["selectorFechaNacimiento"]).Value,
                            Convert.ToBoolean(((CheckBox)c["ccHabilitado"]).Checked));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void agregarTurnoEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.agregarTurno
                            (Convert.ToInt16(c["txtHoraInicio"].Text),
                            Convert.ToInt16(c["txtHoraFin"].Text),
                            c["txtDescripcion"].Text,
                            Convert.ToDecimal(c["txtValorKilometro"].Text),
                            Convert.ToDecimal(c["txtPrecioBase"].Text),
                            Convert.ToBoolean(((CheckBox)c["ccHabilitado"]).Checked));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void eliminarTurnoEnBD(int idTurno, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.eliminarTurno
                            (idTurno);
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void modificarTurnoEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.modificarTurno
                            (Convert.ToInt32(c["lblIdTurno"].Text),
                            Convert.ToInt16(c["txtHoraInicio"].Text),
                            Convert.ToInt16(c["txtHoraFin"].Text),
                            c["txtDescripcion"].Text,
                            Convert.ToDecimal(c["txtValorKilometro"].Text),
                            Convert.ToDecimal(c["txtPrecioBase"].Text),
                            Convert.ToBoolean(((CheckBox)c["ccHabilitado"]).Checked));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void mensajeCreacionDeUsuario(String nombre, String apellido)
        {
            //FIXME: evitar overflow al utilizar substring, agregar consulta a db para traer el nuevo usuario y mostrarlo. Acciona como validacion
            MessageBox.Show("Se ha creado el usuario \"" + apellido.Substring(0, 4) + nombre.Substring(0, 3)+ "\" con clave \"Inicio2017\"", "Se ha creado Usuario",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public override void agregarTurno(String rol)
        {
            construirFormularioTurno(new frmTurnoAgregar());
        }

        private static void construirFormularioTurno(frmABMTurno frmTurno)
        {
            if (frmTurno.construite())
            {
                frmTurno.Show();
            }
        }
        public override void eliminarTurno(String rol)
        {
            construirFormularioTurno(new frmTurnoEliminar()); 
        }

        public override void modificarTurno(String rol)
        {
            construirFormularioTurno(new frmTurnoModificar()); 
        }
    }

    public class RolGenerico : FuncionalidadSegunRol
    {
        public RolGenerico(int idRol, int idUsuario, String nombreRol)
            : base(idRol, idUsuario, nombreRol)
        { }
        public override Boolean soyAdministrador() { return false; }

        public override void agregarClienteChofer(String rol)
        {
            mensajeFuncionNoValidaParaElRol(rol);
        }

        public override void agregarTurno(String rol) { }
        public override void eliminarTurno(String rol) { }
        public override void modificarTurno(String rol) { }
        public override void agregarAutomovil(String rol) { }
        public override void eliminarAutomovil(String rol) { }
        public override void modificarAutomovil(String rol) { }

        public override void eliminarClienteChofer(String rol)
        {
            String funcion = "Eliminar";
            if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
            {
                GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador
                        = new GD1C2017DataSetTableAdapters.QueriesTableAdapter();
                String nombreMetodo = funcion.ToLower() + rol + "EnBD";
                MethodInfo methodInfo = this.GetType().GetMethod(nombreMetodo);
                methodInfo.Invoke(this, new object[] { IdTipoRol, adaptador });
                frmABM.mensajeAutoEliminacionYSalidaDeAplicacion(funcion, rol);
            }
        }

        public void eliminarClienteEnBD(int idTipoRol, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.eliminarCliente(idTipoRol);
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public void eliminarChoferEnBD(int idTipoRol, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.eliminarChofer(idTipoRol);
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public override void modificarClienteChofer(String rol)
        {
            String funcion = "Modificar";
            if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
            {
                GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador
                        = new GD1C2017DataSetTableAdapters.QueriesTableAdapter();
                String nombreMetodo = funcion.ToLower() + rol + "EnBD";
                MethodInfo methodInfo = this.GetType().GetMethod(nombreMetodo);
                methodInfo.Invoke(this, new object[] { IdTipoRol, adaptador });
            }
        }

        public void modificarClienteEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.modificarCliente(Convert.ToInt32(c["lblIdPersona"].Text), Convert.ToInt32(c["txtDNI"].Text), 
                    c["txtNombre"].Text, c["txtApellido"].Text, c["txtCalle"].Text
                    , Convert.ToInt16(c["txtPisoManzana"].Text), c["txtDeptoLote"].Text, c["txtLocalidad"].Text,
                    c["txtCodigoPostal"].Text, Convert.ToInt32(c["txtTelefono"].Text),
                    c["txtCorreo"].Text, ((DateTimePicker)c["selectorFechaNacimiento"]).Value,
                    Convert.ToBoolean(((CheckBox)c["ccHabilitado"]).Checked));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }

        public override void accionBotonAutomovil(object sender, EventArgs e, frmAutomovil formulario, string funcion, string rol, object datos)
        {
        }

        public override void accionBotonTurno(object sender, EventArgs e, frmABMTurno formulario, string funcion, string rol, object datos)
        {
        }

        public override void accionBotonClienteChofer(object sender, EventArgs e, frmABM formulario, string funcion, string rol, object datos)
        {
        }

        public void modificarChoferEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador)
        {
            try
            {
                adaptador.modificarChofer(SingletonDatosUsuario.Instance.obtenerIdPersona(), Convert.ToInt32(c["txtDNI"].Text), c["txtNombre"].Text, c["txtApellido"].Text, c["txtCalle"].Text
                            , Convert.ToInt16(c["txtPisoManzana"].Text), c["txtDeptoLote"].Text, c["txtLocalidad"].Text, c["txtCodigoPostal"].Text
                            , Convert.ToInt32(c["txtTelefono"].Text), c["txtCorreo"].Text, ((DateTimePicker)c["selectorFechaNacimiento"]).Value, Convert.ToBoolean(((CheckBox)c["ccHabilitado"]).Checked));
            }
            catch (SqlException e)
            {
                mensajeErrorEnDB();
            }
        }  

        private void mensajeFuncionNoValidaParaElRol(String rol)
        {
            MessageBox.Show("Un " + NombreRol + " no puede agregar un "+rol, "Funcion no permitida para un " + NombreRol,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    public static class VariablesGlobales
    {
        public const string NOMBRE_ROL_ADMINISTRADOR = "ADMINISTRATIVO";
    }

    interface IGrilla
    {
        void completarFormularioConDatosDeUsuarioSeleccionado(DataRowView filaDeDatos);
    }

    public static class MetodosGlobales
    {
        

        //public static Boolean verificarDatosNoSeanNulos(Form formulario, String mensaje, String titulo)
        //{
        //    Boolean resultado = true;
        //    foreach (Control c in formulario.Controls)
        //    {
        //        if (c is TextBox)
        //        {
        //            TextBox textBox = c as TextBox;
        //            if (String.IsNullOrEmpty(textBox.Text) && !textBox.Name.Equals("txtCorreo"))
        //            {
        //                MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                resultado = false;
        //                break;
        //            }
        //        }
        //    }
        //    return resultado;
        //}

        public static Boolean mensajeAlertaAntesDeAccion(String rol, String funcion)
        {
            DialogResult resultado = MessageBox.Show(Mensajes.mensajeAlertaAntesDeAccionInicio + funcion
                        + Mensajes.mensajeAlertaAntesDeAccionFin + rol
                        , funcion + " " + rol
                        , MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question
                        , MessageBoxDefaultButton.Button2);
            return (resultado == DialogResult.Yes);
        }

        public static void mansajeErrorValidacion()
        {
            MessageBox.Show(Mensajes.mensajeErrorEnValidacionDatosDeFormulario
                        , Mensajes.mensajeErrorEnValidacionDatosDeFormularioTitulo
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
        }

        public static void permitirSoloIngresoNumerico(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void permitirSoloIngresoAlfanumerico(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void permitirSoloIngresoCorreoElectronico(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !esSimboloPermitido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static bool esSimboloPermitido(char caracter)
        {
            return (new HashSet<string> { "@", ".", "-", "_" }).Any(v => (new KeysConverter()).ConvertToString(caracter).Equals(v));
        }

        public static void permitirSoloIngresoAlfanumericoConBlancos(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void permitirSoloIngresoAlfabeticoConBlancos(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void permitirSoloIngresoAlfabetico(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        public static void permitirSoloIngresoHorario(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !esSeparadorPermitido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static bool esSeparadorPermitido(char caracter)
        {
            return (new KeysConverter()).ConvertToString(caracter).Equals(":");
        }

        public static void permitirSoloIngresoMoneda(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !esPuntoDecimalPermitido(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static bool esPuntoDecimalPermitido(char caracter)
        {
            return (new KeysConverter()).ConvertToString(caracter).Equals(".");
        }

        public static class Mensajes
        {
            public static String mensajeDatosNulos{
                get {
                    return "Todos los datos son obligatorios";
                }}
            public static String mensajeTituloVentanaDatosNulos
            {
                get {
                    return "Datos requeridos";;
                }}
            public static String mensajeDatosNulosAltaClienteChofer
            {
                get {
                    return "El correo electronico es el unico dato opcional, el resto son obligatorios";
                }}
            public static String mensajeAlertaAntesDeAccionInicio
            {
                get {
                    return "¿Esta segura/o de ";
                }}
            public static String mensajeAlertaAntesDeAccionFin
            {
                get {
                    return " este nuevo ";
                }}
            public static String mensajeErrorEnValidacionDatosDeFormulario
            {
                get
                {
                    return "Error en validacion de datos. Revise los mismos y reintente.";
                }
            }
            public static String mensajeErrorEnValidacionDatosDeFormularioTitulo
            {
                get
                {
                    return "Error Validacion";
                }
            }
        }

    }

    public static class Validaciones
    {
        public static Boolean validarCampoAlfanumericoConVacio(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"^[a-zA-Z][a-zA-Z0-9]*$");
        }

        public static Boolean validarCampoAlfanumerico(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"^[a-zA-Z0-9]+$");
        }

        public static Boolean validarCampoClave(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"[a-zA-Z0-9\!\#\$\%\._-]+$");
        }

        public static Boolean validarCampoNumericoConVacio(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"^[0-9]*$");
        }

        public static Boolean validarCampoNumerico(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"^[0-9]+$");
        }

        public static Boolean validarCampoAlfabeticoConVacio(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"^[a-zA-Z]*$");
        }

        public static Boolean validarCampoAlfabetico(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"^[a-zA-Z]+$");
        }

        public static Boolean validarCodigoPostal(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"[a-zA-Z][0-9]{4}[a-zA-Z]{3}");
        }

        public static Boolean validarCorreoElectronico(String correoElectronico)
        {
            Boolean resultado;
            try
            {
                if (correoElectronico.Length > 0)
                {
                    String address = new MailAddress(correoElectronico).Address;
                }
                resultado = true;
            }
            catch (FormatException)
            {
                resultado = false;
                //MessageBox.Show("La direccion de correo electronico no es valida.", "Error",
                //MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resultado;
        }

        public static Boolean validarPatente(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"[a-zA-Z]{2}[0-9]{3}[a-zA-Z]{2}|[a-zA-Z]{3}[0-9]{3}");
        }

        public static Boolean validarCampoHorario(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"([01][0-9]|2[0-3]):[0-5][0-9]");
        }

        public static Boolean validarCampoMonetario(String cadenaAValidar)
        {
            return evaluarCadenaConExpresion(cadenaAValidar, @"[0-9]+\.[0-9]{1,2}");
        }

        private static bool evaluarCadenaConExpresion(String cadenaAValidar, String expresionRegular)
        {
            Match match = Regex.Match(cadenaAValidar, expresionRegular);
            return match.Success;
        }
    }
}