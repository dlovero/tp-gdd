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
    public partial class frmABMTurno : Form, IGrilla
    {
        public frmABMTurno()
        {
            InitializeComponent();
        }

        public virtual Boolean construite()
        { return false; }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter adaptador =
                new GD1C2017DataSetTableAdapters.PRC_LISTADO_TURNOS_DISPONIBLESTableAdapter();
            DataTable tblListadoTurnos = adaptador.obtenerTurnosDisponibles(
                this.Controls["grupoBusquedaTurno"].Controls["txtBusquedaDescripcion"].Text);

            if (tblListadoTurnos != null && tblListadoTurnos.Rows.Count > 0)
            {
                frmResultadoBusquedaUsuarioABM formularioResultadoBusqueda = new frmResultadoBusquedaUsuarioABM();
                DataGridView grillaBusquedaTurnos = (DataGridView)formularioResultadoBusqueda.Controls["grillaDatosResultadoBusqueda"];
                grillaBusquedaTurnos.DataSource = tblListadoTurnos;
                grillaBusquedaTurnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grillaBusquedaTurnos.AutoGenerateColumns = true;
                formularioResultadoBusqueda.frmTurno = this;
                formularioResultadoBusqueda.Controls["btnSeleccionar"].Text = "Seleccionar Turno";
                formularioResultadoBusqueda.Show();
            }
            else
            {
                MessageBox.Show("No Existe Turno habilitado, coincidente con los parametros de busqueda.",
                    "Automovil No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void completarFormularioConDatosDeUsuarioSeleccionado(DataRowView filaDeDatos)
        {
            ((TextBox)(this.Controls["grupoDatosTurno"]).Controls["txtHoraInicio"]).Text = filaDeDatos.Row["Turno_Hora_Inicio"].ToString();
            ((TextBox)(this.Controls["grupoDatosTurno"]).Controls["txtHoraFin"]).Text = filaDeDatos.Row["Turno_Hora_Fin"].ToString();
            ((TextBox)(this.Controls["grupoDatosTurno"]).Controls["txtValorKilometro"]).Text = filaDeDatos.Row["Turno_Valor_Kilometro"].ToString();
            ((TextBox)(this.Controls["grupoDatosTurno"]).Controls["txtPrecioBase"]).Text = filaDeDatos.Row["Turno_Precio_Base"].ToString();
            ((TextBox)(this.Controls["grupoDatosTurno"]).Controls["txtDescripcion"]).Text = filaDeDatos.Row["Turno_Descripcion"].ToString();
            ((CheckBox)(this.Controls["grupoDatosTurno"]).Controls["ccHabilitado"]).Checked = (Boolean)filaDeDatos.Row["Turno_Habilitado"];
        }

        public virtual void construirBotonAccion()
        {
        }

        protected void construirNombreBotonAceptar(String nombre)
        {
            ((Button)(obtenerControlesDeGrupo("grupoDatosTurno").
                Controls["btnAceptar"])).Text = nombre;
        }

        public Control.ControlCollection obtenerGrupoControlesDelFormulario(String nombreGrupoDeControles)
        {
            return (Controls[nombreGrupoDeControles]).Controls;
        }

        protected GroupBox obtenerControlesDeGrupo(String nombreGrupo)
        {
            return (GroupBox)this.Controls[nombreGrupo];
        }


    }

    public partial class frmTurnoAgregar : frmABMTurno
    {
        public override Boolean construite()
        {
            this.Controls["grupoBusquedaTurno"].Visible = false;
            //construirComboMarca("grupoDatosAutomovil", "comboMarca");
            return true;
        }

        public virtual void construirBotonAccion()
        {
            construirNombreBotonAceptar("Agregar Turno");
            (this.Controls["grupoDatosAutomovil"]).Controls["btnAceptar"].Click += (sender, e) =>
                SingletonDatosUsuario.Instance.rol.accionBotonTurno(
                sender, e, this, "Agregar", "Turno",
                obtenerGrupoControlesDelFormulario("grupoDatosTurno")
            );
        }
    }

    public partial class frmTurnoEliminar : frmABMTurno
    {
        public override Boolean construite()
        {
            this.Controls["grupoDatosTurno"].Visible = false;
            construirBotonAccion();
            return true;
        }

        public virtual void construirBotonAccion()
        {
        }
    }
    
    public partial class frmTurnoModificar : frmABMTurno
    {
        public override Boolean construite()
        {
            this.Controls["grupoDatosTurno"].Visible = false;
            //construirComboMarca("grupoDatosAutomovil", "comboMarca");
            return true;
        }

        public virtual void construirBotonAccion()
        {
        }
    }
}
