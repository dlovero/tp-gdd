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
    public partial class frmAutomovil : Form
    {
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
            DialogResult resultado = MessageBox.Show("¿Esta segura/o de " + funcion + " esta/e nueva/o " + rol, funcion + " " + rol,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            return (resultado == DialogResult.Yes);
        }

        public Control.ControlCollection obtenerGrupoControlesDeDatosDeAutomovil(frmAutomovil formulario, String nombreGrupoDeControles)
        {
            return (formulario.Controls[nombreGrupoDeControles]).Controls;
        }

        internal static void construite(frmAutomovil frmAutomovil)
        {
            contruirFormularioAgregar(frmAutomovil);
            //construirComboTurno(frmAutomovil);
            //construirComboMarca(frmAutomovil);
            ////construirComboModelo(frmAutomovil);
            //construirComboChofer(frmAutomovil);
            //inhabilitarComboModelo(frmAutomovil);
            frmAutomovil.Show();
        }

        private static void contruirFormularioAgregar(frmAutomovil frmAutomovil)
        {
            desabilitarGrupoControlesDeBusqueda(frmAutomovil);
            construirComboTurno(frmAutomovil);
            construirComboMarca(frmAutomovil);
            construirComboModelo(frmAutomovil);
            construirComboChofer(frmAutomovil);
            asociarModeloASeleccionDeMarca(frmAutomovil);
            construirBotonAccionAgregar(frmAutomovil);
        }

        private static void construirBotonAccionAgregar(frmAutomovil frmAutomovil)
        {
            construirNombreBotonAceptar(frmAutomovil, "Agregar Automovil");
            (frmAutomovil.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
                SingletonDatosUsuario.Instance.rol.accionBotonAgregarAutomovil(sender, e, frmAutomovil, "Agregar", "Automovil");
        }

        private static void desabilitarGrupoControlesDeBusqueda(frmAutomovil frmAutomovil)
        {
            ((GroupBox)(obtenerControlesDeGrupo(frmAutomovil, "grupoBusquedaABM")
                )).Enabled = false;
            ((GroupBox)(obtenerControlesDeGrupo(frmAutomovil, "grupoBusquedaABM")
                )).Visible = false;
                
        }

        private static void construirNombreBotonAceptar(frmAutomovil frmAutomovil, String nombre)
        {
            ((Button)(obtenerControlesDeGrupo(frmAutomovil, "grupoDatosAutomovil").
                Controls["btnAceptar"])).Text = nombre;
        }

        private static void asociarModeloASeleccionDeMarca(frmAutomovil frmAutomovil)
        {
            ((ComboBox)(obtenerControlesDeGrupo(frmAutomovil, "grupoDatosAutomovil")
                .Controls["comboMarca"])).SelectedIndexChanged += (sender, e) =>
                 comboMarcaSelectedIndexChanged(sender, e, frmAutomovil);
        }

        private static void inhabilitarComboModelo(frmAutomovil frmAutomovil)
        {
            GroupBox grupoControles = obtenerControlesDeGrupo(frmAutomovil, "grupoDatosAutomovil");
            grupoControles.Controls["comboModelo"].Enabled = false;
        }

        private static void construirComboTurno(frmAutomovil frmAutomovil)
        {
            GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter();
            DataTable tblTurnosDisponibles = adaptador.obtenerTurnosDisponibles("%");
            ComboBox frmAutomovilComboTurno = (ComboBox)obtenerControlesDeGrupo(frmAutomovil, "grupoDatosAutomovil").Controls["comboTurno"];
            frmAutomovilComboTurno.DataSource = tblTurnosDisponibles;
            frmAutomovilComboTurno.DisplayMember = "Turno_Descripcion";
            frmAutomovilComboTurno.ValueMember = "Turno_Id";
        }

        private static GroupBox obtenerControlesDeGrupo(frmAutomovil frmAutomovil, String nombreGrupo)
        {
            return (GroupBox)frmAutomovil.Controls[nombreGrupo];
        }

        private static void construirComboMarca(frmAutomovil frmAutomovil)
        {
            GD1C2017DataSetTableAdapters.PRC_LISTA_MARCATableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTA_MARCATableAdapter();
            DataTable tblMarcas = adaptador.obtenerListadoMarcasAutomovil();
            ComboBox frmAutomovilComboMarca = (ComboBox)frmAutomovil.Controls["grupoDatosAutomovil"].Controls["comboMarca"];
            frmAutomovilComboMarca.DataSource = tblMarcas;
            frmAutomovilComboMarca.DisplayMember = "Marca_Nombre";
            frmAutomovilComboMarca.ValueMember = "Marca_Id";
        }

        private static void construirComboModelo(frmAutomovil frmAutomovil)
        {
            GD1C2017DataSetTableAdapters.PRC_LISTA_MODELO_X_MARCATableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTA_MODELO_X_MARCATableAdapter();
            int idMarca = (int)((ComboBox)obtenerControlesDeGrupo(frmAutomovil, 
                "grupoDatosAutomovil").Controls["comboMarca"]).SelectedValue;
            DataTable tblModelos = adaptador.obtenerModeloSegunMarca(Convert.ToInt32(idMarca));
            ComboBox frmAutomovilComboModelo = (ComboBox)frmAutomovil.Controls["grupoDatosAutomovil"].Controls["comboModelo"];
            frmAutomovilComboModelo.DataSource = tblModelos;
            frmAutomovilComboModelo.DisplayMember = "Modelo_Nombre";
            frmAutomovilComboModelo.ValueMember = "Modelo_Id";
        }

        private static void construirComboChofer(frmAutomovil frmAutomovil)
        {
            GD1C2017DataSetTableAdapters.PRC_BUSCAR_CHOFERTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_BUSCAR_CHOFERTableAdapter();
            DataTable tblChofer = adaptador.obtenerChoferesHabilitados("%", "%", null);
            ComboBox frmAutomovilComboChofer = (ComboBox)frmAutomovil.Controls["grupoDatosAutomovil"].Controls["comboChofer"];
            var diccionarioDatosChofer = new Dictionary<int, String>();
            foreach (DataRow fila in tblChofer.Rows)
            {
                diccionarioDatosChofer.Add((int)fila["id"], ((string)fila["Persona_Apellido"]) + " " +((string)fila["Persona_Nombre"]));
            }

            frmAutomovilComboChofer.DataSource = new BindingSource(diccionarioDatosChofer,null);
            frmAutomovilComboChofer.DisplayMember = "Value";
            frmAutomovilComboChofer.ValueMember = "Key";
        }

        private static void comboMarcaSelectedIndexChanged(object sender, EventArgs e, frmAutomovil formulario)
        {
            construirComboModelo(formulario);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
