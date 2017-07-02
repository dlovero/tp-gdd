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
    public partial class frmAutomovil : Form, IGrilla
    {

        public int idAutomovil { set; get; }

        public virtual void configurarEstadoFormulario()
        {
        }

        public frmAutomovil()
        {
            InitializeComponent();
        }

        public Boolean verificarDatosNoSeanNulos()
        {
            Boolean resultado = true;
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (String.IsNullOrEmpty(textBox.Text) && !textBox.Name.Equals("txtCorreo"))
                    {
                        MessageBox.Show("El correo electronico es el unico dato opcional, el resto son obligatorios", "Datos requeridos",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }

        public static Boolean mensajeAlertaAntesDeAccion(String rol, String funcion)
        {
            DialogResult resultado = MessageBox.Show("¿Esta segura/o de " + funcion + " este nuevo " + rol + "?", funcion + " " + rol,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            return (resultado == DialogResult.Yes);
        }

        public Control.ControlCollection obtenerGrupoControlesDeDatosDeAutomovil(String nombreGrupoDeControles)
        {
            return (Controls[nombreGrupoDeControles]).Controls;
        }

        public virtual Boolean construite()
        { return false; }
        
        //protected void construirBotonAccionAgregar()
        //{
        //    construirNombreBotonAceptar("Agregar Automovil");
        //    (this.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
        //        SingletonDatosUsuario.Instance.rol.accionBotonAgregarAutomovil(sender, e, this, "Agregar", "Automovil");
        //}

        protected void desabilitarGrupoControlesDeBusqueda()
        {
            ((GroupBox)(obtenerControlesDeGrupo("grupoBusquedaABM")
                )).Enabled = false;
            ((GroupBox)(obtenerControlesDeGrupo("grupoBusquedaABM")
                )).Visible = false;
                
        }

        protected void construirNombreBotonAceptar(String nombre)
        {
            ((Button)(obtenerControlesDeGrupo("grupoDatosAutomovil").
                Controls["btnAceptar"])).Text = nombre;
        }

        protected void asociarModeloASeleccionDeMarca()
        {
            ((ComboBox)(obtenerControlesDeGrupo("grupoDatosAutomovil")
                .Controls["comboMarca"])).SelectedIndexChanged += (sender, e) => comboMarcaSelectedIndexChanged(sender, e);
        }

        protected void habilitarGrupoDatosAutomovil()
        {
            actualizarEstadoHabilitacionGrupoDatosAutomovil(true);
        }

        protected void actualizarEstadoHabilitacionGrupoDatosAutomovil(Boolean estado)
        {
            obtenerControlesDeGrupo("grupoDatosAutomovil").Enabled = estado;
        }

        protected void construirComboTurno()
        {
            GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter();
            DataTable tblTurnosDisponibles = adaptador.obtenerTurnosDisponibles("%");
            ComboBox frmAutomovilComboTurno = (ComboBox)obtenerControlesDeGrupo("grupoDatosAutomovil").Controls["comboTurno"];
            frmAutomovilComboTurno.DataSource = tblTurnosDisponibles;
            frmAutomovilComboTurno.DisplayMember = "Turno_Descripcion";
            frmAutomovilComboTurno.ValueMember = "Turno_Id";
        }

        protected GroupBox obtenerControlesDeGrupo(String nombreGrupo)
        {
            return (GroupBox)this.Controls[nombreGrupo];
        }

        protected void construirComboMarca(String grupo, String combo)
        {
            GD1C2017DataSetTableAdapters.PRC_LISTA_MARCATableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTA_MARCATableAdapter();
            DataTable tblMarcas = adaptador.obtenerListadoMarcasAutomovil();
            ComboBox frmAutomovilComboMarca = (ComboBox)this.Controls[grupo].Controls[combo];
            frmAutomovilComboMarca.DataSource = tblMarcas;
            frmAutomovilComboMarca.DisplayMember = "Marca_Nombre";
            frmAutomovilComboMarca.ValueMember = "Marca_Id";
        }

        protected void construirComboModelo()
        {
            GD1C2017DataSetTableAdapters.PRC_LISTA_MODELO_X_MARCATableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTA_MODELO_X_MARCATableAdapter();
            int idMarca = (int)((ComboBox)obtenerControlesDeGrupo(
                "grupoDatosAutomovil").Controls["comboMarca"]).SelectedValue;
            DataTable tblModelos = adaptador.obtenerModeloSegunMarca(Convert.ToInt32(idMarca));
            ComboBox frmAutomovilComboModelo = (ComboBox)this.Controls["grupoDatosAutomovil"].Controls["comboModelo"];
            frmAutomovilComboModelo.DataSource = tblModelos;
            frmAutomovilComboModelo.DisplayMember = "Modelo_Nombre";
            frmAutomovilComboModelo.ValueMember = "Modelo_Id";
        }

        protected Boolean construirComboChofer()
        {
            GD1C2017DataSetTableAdapters.PRC_LISTA_CHOFERES_NO_ASIGTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTA_CHOFERES_NO_ASIGTableAdapter();
            DataTable tblChofer = adaptador.obtenerChoferesHabilitados();
            if (tblChofer.Rows.Count > 0)
            {
                ComboBox frmAutomovilComboChofer = (ComboBox)this.Controls["grupoDatosAutomovil"].Controls["comboChofer"];
                var diccionarioDatosChofer = new Dictionary<int, String>();
                foreach (DataRow fila in tblChofer.Rows)
                {
                    diccionarioDatosChofer.Add((int)fila["CHOFER_ID"], ((string)fila["CHOFER_APELLIDO"]) + " " + ((string)fila["CHOFER_NOMBRE"]));
                }

                frmAutomovilComboChofer.DataSource = new BindingSource(diccionarioDatosChofer, null);
                frmAutomovilComboChofer.DisplayMember = "Value";
                frmAutomovilComboChofer.ValueMember = "Key";
            } else {
                dispararMensajeYCancelarAccion();
            }
            return tblChofer.Rows.Count > 0;
        }

        protected void dispararMensajeYCancelarAccion()
        {           
            DialogResult resultado = MessageBox.Show("No hay choferes disponibles para asociar.", "Agregar Automovil",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        protected void comboMarcaSelectedIndexChanged(object sender, EventArgs e)
        {
            construirComboModelo();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            GD1C2017DataSetTableAdapters.PRC_LISTADO_AUTOS_SIN_CONDI_PARA_MODIFICACIONTableAdapter adaptador =
                new GD1C2017DataSetTableAdapters.PRC_LISTADO_AUTOS_SIN_CONDI_PARA_MODIFICACIONTableAdapter();
            DataTable tblListadoAutomoviles = adaptador.obtenerListadoAutosSegunDatosBusqueda(
                 this.Controls["grupoBusquedaABM"].Controls["comboMarcaBusqueda"].Text
                ,this.Controls["grupoBusquedaABM"].Controls["txtBusquedaModelo"].Text
                ,this.Controls["grupoBusquedaABM"].Controls["txtBusquedaPatente"].Text
                ,this.Controls["grupoBusquedaABM"].Controls["txtBusquedaNombreChofer"].Text
                ,this.Controls["grupoBusquedaABM"].Controls["txtBusquedaApellidoChofer"].Text);


            if (tblListadoAutomoviles != null && tblListadoAutomoviles.Rows.Count > 0)
            {
                frmResultadoBusquedaUsuarioABM formularioResultadoBusqueda = new frmResultadoBusquedaUsuarioABM();
                DataGridView grillaBusquedaUsuarios = (DataGridView)formularioResultadoBusqueda.Controls["grillaDatosResultadoBusqueda"];
                grillaBusquedaUsuarios.DataSource = tblListadoAutomoviles;
                grillaBusquedaUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grillaBusquedaUsuarios.AutoGenerateColumns = true;
                formularioResultadoBusqueda.frmAutomovil = this;
                formularioResultadoBusqueda.Controls["btnSeleccionar"].Text = "Seleccionar Automovil";
                formularioResultadoBusqueda.Show();
            }
            else
            {
                MessageBox.Show("No Existe Automovil habilitado, coincidente con los parametros de busqueda.",
                    "Automovil No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void completarFormularioConDatosDeUsuarioSeleccionado(DataRowView filaDeDatos)
        {
            construirComboChofer();
            habilitarGrupoDatosAutomovil();
            construirComboTurno();
            construirComboMarca("grupoDatosAutomovil", "comboMarca");
            construirComboModelo();
            asociarModeloASeleccionDeMarca();
            construirBotonAccion();
            
            poblarDatosDelFormulario(filaDeDatos);
            configurarEstadoFormulario();
        }

        public virtual void construirBotonAccion()
        {
            //construirNombreBotonAceptar("Eliminar Automovil");
            //(this.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
            //    SingletonDatosUsuario.Instance.rol.accionBotonEliminarAutomovil(sender, e, this, "Eliminar", "Automovil");
        }

        protected void poblarDatosDelFormulario(DataRowView filaDeDatos)
        {
            ((ComboBox)(this.Controls["grupoDatosAutomovil"]).Controls["comboMarca"]).Text
                = (String)filaDeDatos.Row["Marca_Nombre"];
            ((ComboBox)(this.Controls["grupoDatosAutomovil"]).Controls["comboTurno"]).Text
                = (String)filaDeDatos.Row["Turno_Descripcion"];
            ((ComboBox)(this.Controls["grupoDatosAutomovil"]).Controls["comboModelo"]).Text
                = (String)filaDeDatos.Row["Modelo_Nombre"];
            ((ComboBox)(this.Controls["grupoDatosAutomovil"]).Controls["comboChofer"]).Text
                = (String)filaDeDatos.Row["Persona_Apellido"] + " " + (String)filaDeDatos.Row["Persona_Nombre"];
            ((TextBox)(this.Controls["grupoDatosAutomovil"]).Controls["txtPatente"]).Text
                = (String)filaDeDatos.Row["Auto_Patente"];
            ((CheckBox)(this.Controls["grupoDatosAutomovil"]).Controls["ccHabilitado"]).Checked
                = (Boolean)filaDeDatos.Row["Auto_Habilitado"];

            this.idAutomovil = (int)filaDeDatos.Row["Auto_id"];
            asociarModeloASeleccionDeMarca();
        }

        internal static bool construiteComoFormularioModificar(frmAutomovil frmAutomovil)
        {
            throw new NotImplementedException();
        }
    }

    public partial class frmAutomovilEliminar:frmAutomovil
    {

        public override Boolean construite()
        {
            try
            {
                actualizarEstadoHabilitacionGrupoDatosAutomovil(false);
                construirComboMarca("grupoBusquedaABM", "comboMarcaBusqueda");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("No fue posible ejecutar el formulario.",
                    "Error en formulario ABM Automovil", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        public override void construirBotonAccion()
        {
            construirNombreBotonAceptar("Eliminar Automovil");
            (this.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
               SingletonDatosUsuario.Instance.rol.accionBotonAutomovil(sender, e, this, "Eliminar", "Automovil",
               this.idAutomovil);
            //(this.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
            //    SingletonDatosUsuario.Instance.rol.accionBotonEliminarAutomovil(sender, e, this, "Eliminar", "Automovil");
        }

        public override void configurarEstadoFormulario()
        {
            (this.Controls["grupoDatosAutomovil"].Controls["comboMarca"]).Enabled = false;
            (this.Controls["grupoDatosAutomovil"].Controls["comboModelo"]).Enabled = false;
            (this.Controls["grupoDatosAutomovil"].Controls["comboTurno"]).Enabled = false;
            (this.Controls["grupoDatosAutomovil"].Controls["comboChofer"]).Enabled = false;
            (this.Controls["grupoDatosAutomovil"].Controls["txtPatente"]).Enabled = false;
            (this.Controls["grupoDatosAutomovil"].Controls["ccHabilitado"]).Enabled = false;
        }
    }

    public partial class frmAutomovilAgregar:frmAutomovil
    {
        public override Boolean construite()
        {
            Boolean continua = construirComboChofer();
            if (continua)
            {
                desabilitarGrupoControlesDeBusqueda();
                construirComboTurno();
                construirComboMarca("grupoDatosAutomovil", "comboMarca");
                construirComboModelo();
                asociarModeloASeleccionDeMarca();
                construirBotonAccion();
            }
            return continua;
        }

        public override void construirBotonAccion()
        {
            construirNombreBotonAceptar("Agregar Automovil");
            (this.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
                SingletonDatosUsuario.Instance.rol.accionBotonAutomovil(sender, e, this, "Agregar", "Automovil",
                obtenerGrupoControlesDeDatosDeAutomovil("grupoDatosAutomovil"));
            //(this.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
            //    SingletonDatosUsuario.Instance.rol.accionBotonAgregarAutomovil(sender, e, this, "Agregar", "Automovil");
        }

    }

    public partial class frmAutomovilModificar:frmAutomovil
    {
        public override Boolean construite()
        {
            try
            {
                actualizarEstadoHabilitacionGrupoDatosAutomovil(false);
                construirComboMarca("grupoBusquedaABM", "comboMarcaBusqueda");
                return true;
            }
            catch (Exception e)
            {
                //TODO: mensaje aviso
                return false;
            }
        }

        public override void construirBotonAccion()
        {
            construirNombreBotonAceptar("Modificar Automovil");
            (this.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
                SingletonDatosUsuario.Instance.rol.accionBotonAutomovil(
                sender, e, this, "Modificar", "Automovil",
                obtenerGrupoControlesDeDatosDeAutomovil("grupoDatosAutomovil")
                );
        }

        public override void configurarEstadoFormulario()
        {

        }
    }
}
