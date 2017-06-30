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
            construirComboTurno(frmAutomovil);
            construirComboMarca(frmAutomovil);
            //construirComboModelo(frmAutomovil);
            //construirComboChofer(frmAutomovil);
            frmAutomovil.Show();
        }

        private static void construirComboTurno(frmAutomovil frmAutomovil)
        {
            GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter();
            DataTable tblTurnosDisponibles = adaptador.obtenerTurnosDisponibles("%");
            ComboBox frmAutomovilComboTurno = (ComboBox)frmAutomovil.Controls["grupoDatosAutomovil"].Controls["comboTurno"];
            frmAutomovilComboTurno.DataSource = tblTurnosDisponibles;
            frmAutomovilComboTurno.DisplayMember = "Turno_Descripcion";
            frmAutomovilComboTurno.ValueMember = "Turno_Id";
        }

        private static void construirComboMarca(frmAutomovil frmAutomovil)
        {
            GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter();
            DataTable tblTurnosDisponibles = adaptador.obtenerTurnosDisponibles("%");
            ComboBox frmAutomovilComboTurno = (ComboBox)frmAutomovil.Controls["grupoDatosAutomovil"].Controls["comboTurno"];
            frmAutomovilComboTurno.DataSource = tblTurnosDisponibles;
            frmAutomovilComboTurno.DisplayMember = "Turno_Descripcion";
            frmAutomovilComboTurno.ValueMember = "Turno_Id";
        }
    }
}
