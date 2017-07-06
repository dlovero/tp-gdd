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
            (this.comboRol).SelectedIndexChanged += (sender, e) =>
                comboRolModificarEnSeleccion(sender, e);
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
            this.cajaListaFunciones.Items.AddRange(obtenerListaFuncionesSegunAccion().ToArray());
            this.cajaListaFunciones.DisplayMember = "nombreFuncion";
            this.cajaListaFunciones.ValueMember = "id";
        }

        protected void armarListaFuncionesAsociadas()
        {
            this.cajaListaFuncionesSegunRol.Items.AddRange(obtenerListaFuncionesAsociadasSegunAccion().ToArray());
            this.cajaListaFuncionesSegunRol.DisplayMember = "nombreFuncion";
            this.cajaListaFuncionesSegunRol.ValueMember = "id";
        }

        private void construirComboRol()
        {
            this.comboRol.DataSource = obtenerDatosParaComboRol();
            this.comboRol.DisplayMember = "nombre";
            this.comboRol.ValueMember = "id";
        }

        protected abstract DataTable obtenerDatosParaComboRol();
        protected abstract List<FuncionalidadSegunRol> obtenerListaFuncionesSegunAccion();
        protected abstract List<FuncionalidadSegunRol> obtenerListaFuncionesAsociadasSegunAccion();
        
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
                Convert.ToInt32((this.comboRol.SelectedValue)))
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

        protected class FuncionalidadSegunRol
        {
            public int id {set; get;}
            public String nombreFuncion {set; get;}
            public FuncionalidadSegunRol()
            {
            }
            public FuncionalidadSegunRol(int idFuncionalidad, String nombre)
            {
                this.id = idFuncionalidad;
                this.nombreFuncion = nombre;
            }
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

        private void comboRolModificarEnSeleccion(object sender, EventArgs e)
        {
            this.cajaListaFunciones.Items.Clear();
            this.cajaListaFuncionesSegunRol.Items.Clear();
            modificarListas();
        }

        protected abstract void modificarListas();

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(this.cajaListaFunciones.SelectedIndex != -1)
            {
                this.cajaListaFuncionesSegunRol.Items.Add(this.cajaListaFunciones.SelectedItem);
                this.cajaListaFunciones.Items.Remove(this.cajaListaFunciones.SelectedItem);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (this.cajaListaFuncionesSegunRol.SelectedIndex != -1)
            {
                this.cajaListaFunciones.Items.Add(this.cajaListaFuncionesSegunRol.SelectedItem);
                this.cajaListaFuncionesSegunRol.Items.Remove(this.cajaListaFuncionesSegunRol.SelectedItem);
            }

        }
    }

    public partial class frmRolAgregar : frmRol
    {
        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesAsociadasSegunAccion()
        {
            return obtenerListaFunciones().Except(obtenerListaFuncionesAsociadas()).ToList();
        }

        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesSegunAccion()
        {
            return obtenerListaFuncionesAsociadas().Except(obtenerListaFunciones()).ToList(); ;
        }

        protected override void modificarListas()
        {
        }
        
        protected override DataTable obtenerDatosParaComboRol()
        {
            return null;
        }

        protected override void armarListaFuncionesAsociadasARol()
        {
            this.cajaListaFuncionesSegunRol.DisplayMember = "nombreFuncion";
            this.cajaListaFuncionesSegunRol.ValueMember = "id";
        }

        protected override void prepararFormulario()
        {
            this.comboRol.DropDownStyle = ComboBoxStyle.Simple;
            agregarNombres("Agregar Rol");
        }

    }

    public partial class frmRolModificar : frmRol
    {
        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesAsociadasSegunAccion()
        {
            return obtenerListaFunciones().
                Except(obtenerListaFuncionesAsociadas()).ToList();
        }

        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesSegunAccion()
        {
            return obtenerListaFuncionesAsociadas().
                Except(obtenerListaFunciones()).ToList();
        }

        protected override DataTable obtenerDatosParaComboRol()
        {
            return (new GD1C2017DataSetTableAdapters.LISTAR_ROLES_SIN_CONDITableAdapter())
                .listadoRoles();
        }

        protected override void modificarListas()
        {
            armarListaFuncionesAsociadas();
            armarListaFunciones();
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
        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesAsociadasSegunAccion()
        {
            return obtenerListaFuncionesAsociadas();
        }

        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesSegunAccion()
        {
            return new List<FuncionalidadSegunRol>();
        }

        protected override void modificarListas()
        {
            armarListaFuncionesAsociadas();
            armarListaFunciones();
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
            centrarControlHorizontal(this.cajaListaFuncionesSegunRol);
            cajaListaFuncionesSegunRol.Enabled = false;
            agregarNombres("Eliminar Rol");
        }
    }
}
