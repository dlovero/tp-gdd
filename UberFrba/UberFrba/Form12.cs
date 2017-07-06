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
    public abstract partial class frmRol : Form
    {
        public frmRol()
        {
            InitializeComponent();
        }

        public Boolean construite()
        {
            prepararFormulario();
            cargarDatosDeFormulario();
            return true;
        }

        protected abstract void prepararFormulario();

        protected void cargarDatosDeFormulario()
        {
            construirComboRol();
            armarListaFunciones();
            armarListaFuncionesAsociadasARol();
        }

        protected abstract void armarListaFuncionesAsociadasARol();
        
        protected void armarListaFunciones()
        {
            this.cajaListaFunciones.DataSource = obtenerListaFuncionesSegunAccion();
            this.cajaListaFunciones.DisplayMember = "nombreFuncion";
            this.cajaListaFunciones.ValueMember = "id";
        }

        protected void armarListaFuncionesAsociadas()
        {
            this.cajaListaFunciones.DataSource = obtenerListaFuncionesAsociadasSegunAccion();
            this.cajaListaFunciones.DisplayMember = "nombreFuncion";
            this.cajaListaFunciones.ValueMember = "id";
        }

        private void construirComboRol()
        {
            this.comboFunciones.DataSource = obtenerDatosParaComboRol();
            this.comboFunciones.DisplayMember = "nombre";
            this.comboFunciones.ValueMember = "id";
        }

        protected abstract DataTable obtenerDatosParaComboRol();

        protected abstract object obtenerListaFuncionesSegunAccion();
        
        protected void cargarDatosEnCuadrosDeLista()
        {
            //this.cajaListaFunciones.DataSource = listaFuncionalidades();
            //this.cajaListaFunciones.DataSource = listaFuncionalidades();

            //List<DataRow> listaFuncionalidades = obtenerFuncionalidades();
            //List<DataRow> listaFuncionalidadesSegunRol
            //    = obtenerFuncionalidadesSegunRol(idRol);
        }

        protected List<FuncionalidadSegunRol> obtenerListaFunciones()
        {
            GD1C2017DataSetTableAdapters.LISTAR_FUNCIONALIDADESTableAdapter adaptador =
                new GD1C2017DataSetTableAdapters.LISTAR_FUNCIONALIDADESTableAdapter();
            return adaptador.listaDeFunciones().AsEnumerable().Select(
                elemento => new FuncionalidadSegunRol()
                            {
                                id = elemento.Field<int>("id"),
                                nombreFuncion = elemento.Field<String>("nombreFuncion"),
                            }).ToList();
        }

        protected List<FuncionalidadSegunRol> obtenerListaFuncionesAsociadas()
        {
            GD1C2017DataSetTableAdapters.LISTAR_FUNC_X_ROL_HABITableAdapter adaptador =
                new GD1C2017DataSetTableAdapters.LISTAR_FUNC_X_ROL_HABITableAdapter();
            return adaptador.listaDeFunciones(
                Convert.ToInt32((this.comboFunciones.SelectedValue)))
                .AsEnumerable().Select(
                elemento => new FuncionalidadSegunRol()
                {
                    id = elemento.Field<int>("id"),
                    nombreFuncion = elemento.Field<String>("nombreFuncion"),
                }).ToList();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected object obtenerListaFuncionesAsociadasSegunAccion()
        {
            return obtenerListaFuncionesAsociadas();
        }

        protected class FuncionalidadSegunRol
        {
            public int id {set; get;}
            public String nombreFuncion {set; get;}
        }

        protected void centrarControlHorizontal(System.Windows.Forms.Control control)
        {
            control.Left = (this.ClientSize.Width - this.cajaListaFunciones.Width) / 2;
            //control.Top = (this.ClientSize.Height - this.cajaListaFunciones.Height) / 2;
        }

        protected void agregarNombres(String nombre)
        {
            this.Text = nombre;
            this.btnAceptar.Text = nombre;
        }
    }

    public partial class frmRolAgregar : frmRol
    {
        protected override object obtenerListaFuncionesSegunAccion()
        {
            return obtenerListaFunciones();
        }

        protected override DataTable obtenerDatosParaComboRol()
        {
            return null;
        }

        protected override void armarListaFuncionesAsociadasARol()
        {
        }

        protected override void prepararFormulario()
        {
            this.comboFunciones.DropDownStyle = ComboBoxStyle.Simple;
            agregarNombres("Agregar Rol");
        }
    }

    public partial class frmRolModificar : frmRol
    {
        protected override object obtenerListaFuncionesSegunAccion()
        {
            return obtenerListaFunciones().
                Except(obtenerListaFuncionesAsociadas()).ToList();
        }

        protected override DataTable obtenerDatosParaComboRol()
        {
            return (new GD1C2017DataSetTableAdapters.LISTAR_ROLES_SIN_CONDITableAdapter())
                .listadoRoles();
        }

        protected override void armarListaFuncionesAsociadasARol()
        {
            armarListaFuncionesAsociadas();
        }

        protected override void prepararFormulario()
        {
            agregarNombres("Modificar Rol");
        }
    }

    public partial class frmRolEliminar : frmRol
    {
        protected override object obtenerListaFuncionesSegunAccion()
        {
            return new List<FuncionalidadSegunRol>();
        }

        protected override DataTable obtenerDatosParaComboRol()
        {
            return (new GD1C2017DataSetTableAdapters.LISTAR_ROLES_HABITableAdapter())
                .listadoRoles();
        }

        protected override void armarListaFuncionesAsociadasARol()
        {
            armarListaFuncionesAsociadas();
        }

        protected override void prepararFormulario()
        {
            this.cajaListaFunciones.Visible = false;
            this.btnAgregar.Visible = false;
            this.btnQuitar.Visible = false;
            this.lblFunciones.Visible = false;
            centrarControlHorizontal(this.lblCajaFuncionesSegunRol);
            this.comboFunciones.DropDownStyle = ComboBoxStyle.Simple;
            this.comboFunciones.Enabled = false;
            centrarControlHorizontal(this.cajaListaFuncionesSegunRol);
            agregarNombres("Eliminar Rol");
        }
    }
}
