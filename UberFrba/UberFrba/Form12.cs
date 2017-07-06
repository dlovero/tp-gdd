﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UberFrba
{
    public abstract partial class frmRol : Form
    {

        protected List<FuncionalidadSegunRol> listaFunciones { set; get; }
        protected List<FuncionalidadSegunRol> listaFuncionesSegunRol { set; get; }

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
            cargarListas();
            armarListaFunciones();
            armarListaFuncionesAsociadasARol();
            cargarAccionABoton();
        }

        protected void cargarAccionABoton()
        {
            (this.btnAceptar).Click += (sender, e) =>
                accionBotonAceptar(sender, e);
        }

        //protected abstract void accionBotonAceptar(object sender, EventArgs e);

        protected void cargarListas()
        {
            this.listaFunciones = obtenerListaFunciones();
            this.listaFuncionesSegunRol = obtenerListaFuncionesAsociadas();
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

        protected static string armarListaConItem(List<FuncionalidadSegunRol> lista)
        {
            List<int> listaFuncionalidades = new List<int>();
            foreach (var item in lista)
                listaFuncionalidades.Add(item.id);
            return string.Join(",", listaFuncionalidades.ToList());
        }

        private void comboRol_KeyPress(object sender, KeyPressEventArgs e)
        {
            verificarCaracterIngresado(e);
        }

        protected abstract void verificarCaracterIngresado(KeyPressEventArgs e);

        protected void accionBotonAceptar(object sender, EventArgs e)
        {
            if (mensajeAlertaAntesDeAccion())
            {
                try
                {
                    ejecutarSegunAccion();
                }
                catch (SqlException ex)
                {
                    mensajeErrorEnDB();
                }
                mensajeConfirmaAccion();
            }
        }

        protected abstract void mensajeConfirmaAccion();

        protected void dispararMensajeConfirmaAccion(String funcion, String rol)
        {
            DialogResult resultado = MessageBox.Show("Se ha " + funcion + " el rol.", funcion + " " + rol,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected abstract Boolean mensajeAlertaAntesDeAccion();

        public void mensajeErrorEnDB()
        {
            MessageBox.Show("Error al operar en la BD", "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected abstract void ejecutarSegunAccion();


    }

    public partial class frmRolAgregar : frmRol
    {
        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesAsociadasSegunAccion()
        {
            return new List<FuncionalidadSegunRol>();
        }

        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesSegunAccion()
        {
            return this.listaFunciones;
        }

        protected override void modificarListas()
        {
        }

        protected override void ejecutarSegunAccion()
        {
            if (Validaciones.validarCampoAlfanumerico(this.comboRol.Text))
            {
                GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador =
                new GD1C2017DataSetTableAdapters.QueriesTableAdapter();
                adaptador.agregarRol(this.comboRol.Text, armarCadenaConIdsFunciones());
                this.Close();
            }
            else
            {
                MetodosGlobales.mansajeErrorValidacion();
            }

        }

        private string armarCadenaConIdsFunciones()
        {
            
            (this.cajaListaFuncionesSegunRol.Items.Cast<FuncionalidadSegunRol>())
                .Select(funcionalidad => funcionalidad.id).ToList();
            List<FuncionalidadSegunRol> lista = (this.cajaListaFuncionesSegunRol.Items.Cast<FuncionalidadSegunRol>()).ToList();
            return armarListaConItem(lista);
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

        protected override void verificarCaracterIngresado(KeyPressEventArgs e)
        {
            MetodosGlobales.permitirSoloIngresoAlfanumerico(e);
        }

        protected override void mensajeConfirmaAccion()
        {
            dispararMensajeConfirmaAccion("Agregado", "Rol");
        }
        
        protected override Boolean mensajeAlertaAntesDeAccion()
        {
            return MetodosGlobales.mensajeAlertaAntesDeAccion("Rol", "Agregar");
        }
    }

    public partial class frmRolModificar : frmRol
    {
        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesAsociadasSegunAccion()
        {
            return this.listaFuncionesSegunRol;
        }

        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesSegunAccion()
        {
            return this.listaFunciones.Where(item =>
                !listaFuncionesSegunRol.Any(funcion => funcion.nombreFuncion.Equals(item.nombreFuncion))).ToList();
        }

        protected override DataTable obtenerDatosParaComboRol()
        {
            return (new GD1C2017DataSetTableAdapters.LISTAR_ROLES_SIN_CONDITableAdapter())
                .listadoRoles();
        }

        protected override void modificarListas()
        {
            cargarListas();
            armarListaFuncionesAsociadas();
            armarListaFunciones();
        }

        protected override void armarListaFuncionesAsociadasARol()
        {
            armarListaFuncionesAsociadas();
        }

        protected override void prepararFormulario()
        {
            //this.ccHabilitado.Checked;
            agregarNombres("Modificar Rol");
        }

        

        protected override void ejecutarSegunAccion()
        {
            GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador =
                new GD1C2017DataSetTableAdapters.QueriesTableAdapter();
            List<FuncionalidadSegunRol> listaNueva = this.cajaListaFuncionesSegunRol.Items
                .Cast<FuncionalidadSegunRol>().ToList();
            List<FuncionalidadSegunRol> listaConFuncionesParaAgregar = listaNueva.Where(item =>
                !listaFuncionesSegunRol.Any(funcion => funcion.nombreFuncion.Equals(item.nombreFuncion))).ToList();
            List<FuncionalidadSegunRol> listaConFuncionesParaQuitar = this.listaFuncionesSegunRol.Where(item =>
                !listaNueva.Any(funcion => funcion.nombreFuncion.Equals(item.nombreFuncion))).ToList();
            adaptador.modificarRol(Convert.ToInt32(this.comboRol.SelectedValue),
                this.comboRol.SelectedText,
                Convert.ToInt16(this.ccHabilitado.Checked),
                armarListaConItem(listaConFuncionesParaAgregar),
                armarListaConItem(listaConFuncionesParaQuitar));
            this.Close();
        }

        protected override Boolean mensajeAlertaAntesDeAccion()
        {
            return MetodosGlobales.mensajeAlertaAntesDeAccion("Rol", "Modificar");
        }

        protected override void mensajeConfirmaAccion()
        {
            dispararMensajeConfirmaAccion("Modificado", "Rol");
        }

        protected override void verificarCaracterIngresado(KeyPressEventArgs e)
        {
        }
    }

    public partial class frmRolEliminar : frmRol
    {
        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesAsociadasSegunAccion()
        {
            return this.listaFuncionesSegunRol;
        }

        protected override List<FuncionalidadSegunRol> obtenerListaFuncionesSegunAccion()
        {
            return new List<FuncionalidadSegunRol>();
        }

        protected override void modificarListas()
        {
            cargarListas();
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
            this.cajaListaFuncionesSegunRol.SelectionMode = SelectionMode.None;
            this.ccHabilitado.Visible = false;
            this.cajaListaFunciones.Visible = false;
            this.btnAgregar.Visible = false;
            this.btnQuitar.Visible = false;
            this.lblFunciones.Visible = false;
            centrarControlHorizontal(this.lblCajaFuncionesSegunRol);
            centrarControlHorizontal(this.cajaListaFuncionesSegunRol);
            agregarNombres("Eliminar Rol");
        }

        protected override void ejecutarSegunAccion()
        {
            GD1C2017DataSetTableAdapters.QueriesTableAdapter adaptador =
                new GD1C2017DataSetTableAdapters.QueriesTableAdapter();
            adaptador.eliminarRol(Convert.ToInt32(this.comboRol.SelectedValue));
            this.Close();
        }

        protected override Boolean mensajeAlertaAntesDeAccion()
        {
            return MetodosGlobales.mensajeAlertaAntesDeAccion("Rol", "Eliminar");
        }

        protected override void mensajeConfirmaAccion()
        {
            dispararMensajeConfirmaAccion("Eliminado", "Rol");
        }

        protected override void verificarCaracterIngresado(KeyPressEventArgs e)
        {
        }
    }
}
