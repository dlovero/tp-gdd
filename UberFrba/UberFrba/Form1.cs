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
                GD1C2017DataSetTableAdapters.PRC_VALIDAR_USUARIOTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_VALIDAR_USUARIOTableAdapter();
                DataTable tblUsuarioYRoles = adaptador.validarUsuario(textoUsuario.Text, sha256(textoClave.Text));
            List<Tuple<String,String>> roles = new List<Tuple<string,string>>();

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
        void agregarClienteChofer(String rol);
        void eliminarClienteChofer(String rol);
        void modificarClienteChofer(String rol);
        void agregarAutomovil(String rol);
        void eliminarAutomovil(String rol);
        void modificarAutomovil(String rol);
        void agregarRol(String rol);
        void eliminarRol(String rol);
        void modificarRol(String rol);
        void accionBotonAutomovil(object sender, EventArgs e, frmAutomovil formulario, String funcion, String rol, object datos);
        void accionBotonTurno(object sender, EventArgs e, frmABMTurno formulario, string funcion, string rol, object datos);
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

        public abstract void agregarClienteChofer(String rol);
        public abstract void eliminarClienteChofer(String rol);
        public abstract void modificarClienteChofer(String rol);
        public abstract void agregarAutomovil(String rol);
        public abstract void eliminarAutomovil(String rol);
        public abstract void modificarAutomovil(String rol);
        public abstract void agregarRol(String rol);
        public abstract void eliminarRol(String rol);
        public abstract void modificarRol(String rol);
        public abstract void accionBotonAutomovil(object sender, EventArgs e, frmAutomovil formulario, String funcion, String rol, object datos);
        public abstract void accionBotonTurno(object sender, EventArgs e, frmABMTurno formulario, string funcion, string rol, object datos);
        public abstract void completarConfiguracion
            (frmABM formulario, String textoFuncion, String textoTipo);

        protected void configurarFormularioAgregarOModificar
            (frmABM formulario, String textoFuncion, String textoTipo)
        {
            completarConfiguracion(formulario, textoFuncion, textoTipo);
            formulario.Text = textoFuncion + " " + textoTipo;
            ((TextBox)(formulario.Controls["grupoDatosPersona"]).Controls["txtNombre"]).Focus();
            ((Button)(formulario.Controls["grupoDatosPersona"]).Controls["btnAceptar"]).Text = textoFuncion + " " + textoTipo;
        }

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

        public override void agregarClienteChofer(String rol)
        {
            Action<object, EventArgs, frmABM, string, string> metodo = accionBotonAgregar;
            armarFormulario(rol, metodo, "Agregar");
        }

        public override void eliminarClienteChofer(String rol)
        {
            Action<object, EventArgs, frmABM, string, string> metodo = accionBotonEliminar;
            armarFormulario(rol, metodo, "Eliminar");
        }

        public override void modificarClienteChofer(String rol)
        {
            Action<object, EventArgs, frmABM, string, string> metodo = accionBotonModificar;
            armarFormulario(rol, metodo, "Modificar");
        }

        public override void agregarAutomovil(String rol)
        {
            frmAutomovilAgregar frmAutomovil = new frmAutomovilAgregar();
            if (frmAutomovil.construite())
            {
                frmAutomovil.Show();
            }
        }
        public override void accionBotonAutomovil(object sender, EventArgs e, frmAutomovil formulario, string funcion, string rol, object datos)
        {
            if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulos,
                                                        MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
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
        }

        public override void accionBotonTurno(object sender, EventArgs e, frmABMTurno formulario, string funcion, string rol, object datos)
        {
            if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulos,
                                                        MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
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
        }

        public override void eliminarAutomovil(String rol)
        {
            frmAutomovilEliminar frmAutomovil = new frmAutomovilEliminar();
            if (frmAutomovil.construite())
            {
                frmAutomovil.Show();
            }
        }

        public override void modificarAutomovil(String rol)
        {
            frmAutomovilModificar frmAutomovil = new frmAutomovilModificar();
            if (frmAutomovil.construite())
            {
                frmAutomovil.Show();
            }
        }

        private void armarFormulario(String rol, Action<object, EventArgs, frmABM, string, string> metodo, String funcion)
        {
            frmABM frmAlta = parametrizarFormulario(funcion, rol);
            (frmAlta.Controls["grupoDatosPersona"]).Controls["btnAceptar"].Click += (sender, e) =>
                metodo(sender, e, frmAlta, funcion, rol);
            frmAlta.Show();
        }

        private void accionBotonAgregar(object sender, EventArgs e, frmABM formulario, string funcion, string rol)
        {
            if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulosAltaClienteChofer,
                                                        MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
            {
                if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
                {
                    ejecutarMetodoDeAccionConParametros(obtenerNombreMetodo(funcion, rol), new object[] { 
                        obtenerGrupoControlesDeFormularioABM(formulario, "grupoDatosPersona")
                        ,obtenerAdaptadorBD() });
                    formulario.Close();
                }
            }
        }

        private void accionBotonEliminar(object sender, EventArgs e, frmABM formulario, string funcion, string rol)
        {
            if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
            {
                ejecutarMetodoDeAccionConParametros(
                    obtenerNombreMetodo(funcion, rol),
                    new object[] { formulario.idTipoRol, obtenerAdaptadorBD() });
                formulario.Close();
            }
        }

        private void accionBotonModificar(object sender, EventArgs e, frmABM formulario, string funcion, string rol)
        {
            if (MetodosGlobales.verificarDatosNoSeanNulos(formulario, MetodosGlobales.Mensajes.mensajeDatosNulosAltaClienteChofer,
                                                        MetodosGlobales.Mensajes.mensajeTituloVentanaDatosNulos))
            {
                if (MetodosGlobales.mensajeAlertaAntesDeAccion(rol, funcion))
                {
                    ejecutarMetodoDeAccionConParametros(
                        obtenerNombreMetodo(funcion, rol),
                        new object[] { 
                        obtenerGrupoControlesDeFormularioABM(formulario, "grupoDatosPersona"), 
                        obtenerAdaptadorBD(), formulario.idPersona });
                    formulario.Close();
                }
            }
        }

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
            //mensajeCreacionDeAutomovil(c["txtNombre"].Text, c["txtApellido"].Text);
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
            //mensajeCreacionDeAutomovil(c["txtNombre"].Text, c["txtApellido"].Text);
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
            //mensajeCreacionDeAutomovil(c["txtNombre"].Text, c["txtApellido"].Text);
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

        public void modificarClienteEnBD(Control.ControlCollection c, GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador, int idPersona)
        {
            try
            {
                adaptador.modificarCliente
                            (idPersona, Convert.ToInt32(c["txtDNI"].Text), c["txtNombre"].Text, c["txtApellido"].Text, c["txtCalle"].Text
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

        public frmABM parametrizarFormulario(String funcion, String rol)
        {
            frmABM frmAltaCliente = new frmABM();
            configurarFormularioAgregarOModificar(frmAltaCliente, funcion, rol);
            frmAltaCliente.Controls["grupoBusquedaABM"].Visible = verificarCondicionesParaVisualizarPAnelDeBusqueda(funcion);
            return frmAltaCliente;
        }

        private bool verificarCondicionesParaVisualizarPAnelDeBusqueda(String funcion)
        {
            return SingletonDatosUsuario.Instance.soyRolAdministrador(NombreRol) && !(funcion.ToUpper().Equals("AGREGAR"));
        }

        public override void completarConfiguracion
            (frmABM formulario, String textoFuncion, String textoTipo)
        {
            (((GroupBox)formulario.Controls["grupoBusquedaABM"]).Controls["btnBuscar"]).Text = "Buscar " + textoTipo;
            ((frmABM)formulario).tipoUsuario = textoTipo;
            ((frmABM)formulario).tipoFuncion = textoFuncion;
        }
       
        public void mensajeCreacionDeUsuario(String nombre, String apellido)
        {
            //FIXME: evitar overflow al utilizar substring, agregar consulta a db para traer el nuevo usuario y mostrarlo. Acciona como validacion
            MessageBox.Show("Se ha creado el usuario \"" + apellido.Substring(0, 4) + nombre.Substring(0, 3)+ "\" con clave \"Inicio2017\"", "Se ha creado Usuario",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public override void agregarRol(String rol)
        {
            frmTurnoAgregar frmTurno = new frmTurnoAgregar();
            if (frmTurno.construite())
            {
                frmTurno.Show();
            }
        }
        public override void eliminarRol(String rol) { }
        public override void modificarRol(String rol) { }
    }

    public class RolGenerico : FuncionalidadSegunRol
    {
        public RolGenerico(int idRol, int idUsuario, String nombreRol)
            : base(idRol, idUsuario, nombreRol)
        { }

        public override void agregarClienteChofer(String rol)
        {
            mensajeFuncionNoValidaParaElRol(rol);
        }

        public override void agregarRol(String rol) { }
        public override void eliminarRol(String rol) { }
        public override void modificarRol(String rol) { }
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
                adaptador.modificarCliente(SingletonDatosUsuario.Instance.obtenerIdPersona(),Convert.ToInt32(c["txtDNI"].Text), c["txtNombre"].Text, c["txtApellido"].Text, c["txtCalle"].Text
                            , Convert.ToInt16(c["txtPisoManzana"].Text), c["txtDeptoLote"].Text, c["txtLocalidad"].Text, c["txtCodigoPostal"].Text
                            , Convert.ToInt32(c["txtTelefono"].Text), c["txtCorreo"].Text, ((DateTimePicker)c["selectorFechaNacimiento"]).Value, Convert.ToBoolean(((CheckBox)c["ccHabilitado"]).Checked));
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

        public override void completarConfiguracion
            (frmABM formulario, String textoFuncion, String textoTipo)
        {
            ((GroupBox)formulario.Controls["grupoBusquedaABM"]).Visible = false;
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
        public static Boolean verificarDatosNoSeanNulos(Form formulario, String mensaje, String titulo)
        {
            Boolean resultado = true;
            foreach (Control c in formulario.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (String.IsNullOrEmpty(textBox.Text) && !textBox.Name.Equals("txtCorreo"))
                    {
                        MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }

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
        }
    }
}