﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UberFrba
{
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private ToolStripMenuItem dameUnItemDeMenu(string nombre)
        {
            return new ToolStripMenuItem(nombre);
        }

        private ToolStripMenuItem dameUnItemDeFuncion(string nombre, string nombreMetodo)
        {
            MethodInfo methodInfo = this.GetType().GetMethod(nombreMetodo, BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandler handler = (EventHandler)Delegate.CreateDelegate(
              typeof(EventHandler), this, methodInfo);
            return new ToolStripMenuItem(nombre, null, handler, null);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            SingletonDatosUsuario datosUsuario = SingletonDatosUsuario.Instance;
            int rolId = Convert.ToInt32(comboRol.SelectedValue);
            datosUsuario.setearRolId(rolId);
            this.Hide();
            GD1C2017DataSetTableAdapters.PRC_OBTENER_MENU_X_ROLTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_OBTENER_MENU_X_ROLTableAdapter();
            DataTable tblMenuSegunRol = adaptador.menuSegunRol(rolId);

            frmPrincipal fmPrincipal = new frmPrincipal();
            MenuStrip fmPrincipalMenu = (MenuStrip)fmPrincipal.Controls["mnuPrincipal"];
            ToolStripMenuItem mnuAuxiliar=null, subMenuAuxiliar=null;

            foreach (DataRow fila in tblMenuSegunRol.Rows)
            {
                if (fila.Field<int>("Ascendente") == 0)
                {
                    if (fila.Field<string>("Metodo") == null)
                    {
                        if (mnuAuxiliar != null)
                        {
                            if (subMenuAuxiliar != null)
                            {
                                mnuAuxiliar.DropDownItems.Add(subMenuAuxiliar);
                                subMenuAuxiliar = null;
                            }
                            fmPrincipalMenu.Items.Add(mnuAuxiliar);
                            mnuAuxiliar = dameUnItemDeMenu(fila.Field<string>("Nombre"));
                        }
                        else
                        {
                            mnuAuxiliar = dameUnItemDeMenu(fila.Field<string>("Nombre"));
                        }
                    }
                    else
                    {
                        fmPrincipalMenu.Items.Add(dameUnItemDeFuncion(fila.Field<string>("Nombre"), fila.Field<string>("Metodo")));
                    }
                }
                else
                {
                    if (fila.Field<string>("Metodo") == null)
                    {
                        if (subMenuAuxiliar != null)
                        {
                            mnuAuxiliar.DropDownItems.Add(subMenuAuxiliar);
                            subMenuAuxiliar = dameUnItemDeMenu(fila.Field<string>("Nombre"));
                        }
                        else
                        {
                            subMenuAuxiliar = dameUnItemDeMenu(fila.Field<string>("Nombre"));
                        }
                    }
                    else
                    {
                        if (subMenuAuxiliar != null)
                        {
                            subMenuAuxiliar.DropDownItems.Add(dameUnItemDeFuncion(fila.Field<string>("Nombre"), fila.Field<string>("Metodo")));
                        }
                        else
                        {
                            mnuAuxiliar.DropDownItems.Add(dameUnItemDeFuncion(fila.Field<string>("Nombre"), fila.Field<string>("Metodo")));
                        }
                    }

                }
                //string nombreMenu = fila.Field<string>("Nombre");
                
            }
            if (subMenuAuxiliar != null)
            {
                mnuAuxiliar.DropDownItems.Add(subMenuAuxiliar);
            }
            if (mnuAuxiliar != null)
            {
                fmPrincipalMenu.Items.Add(mnuAuxiliar);
            }
            fmPrincipal.Show();
        }

        private static bool esAdministrador()
        {
            return (SingletonDatosUsuario.Instance).obtenerIdRol() == 1;
        }

        private static void configurarFormularioAgregarOModificar
            (Form formulario, String textoFuncion, String textoTipo)
        {
            if (esAdministrador())
            {
                (((GroupBox)formulario.Controls["grupoBusquedaABM"]).Controls["btnBuscar"]).Text = "Buscar " + textoTipo;
               // ((Button)formulario.Controls["btnBuscar"]).Text = "Buscar " + textoTipo;
                ((frmABM)formulario).tipoUsuario = textoTipo;
            }
            ((GroupBox)formulario.Controls["grupoBusquedaABM"]).Visible = esAdministrador();
            formulario.Text = textoFuncion + textoTipo;
            ((TextBox)formulario.Controls["txtNombre"]).Focus();
            ((Button)formulario.Controls["btnAceptar"]).Text = textoFuncion + textoTipo;
        }

        private void agregarCliente(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            configurarFormularioAgregarOModificar(frmAltaCliente, "Agregar ", "Cliente");
            frmAltaCliente.Show();
        }
        
        private void eliminarCliente(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            configurarFormularioAgregarOModificar(frmAltaCliente, "Eliminar ", "Cliente");
            frmAltaCliente.Show();
        }
        private void modificarCliente(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            configurarFormularioAgregarOModificar(frmAltaCliente, "Modificar ", "Cliente");
            frmAltaCliente.Show();
        }
        private void agregarChofer(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            configurarFormularioAgregarOModificar(frmAltaCliente, "Agregar ", "Chofer");
            frmAltaCliente.Show();
        }
        private void eliminarChofer(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            configurarFormularioAgregarOModificar(frmAltaCliente, "Eliminar ", "Chofer");
            frmAltaCliente.Show();
        }
        private void modificarChofer(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            configurarFormularioAgregarOModificar(frmAltaCliente, "Modificar ", "Chofer");
            frmAltaCliente.Show();
        }
        private void agregarAutomovil(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void eliminarAutomovil(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void modificarAutomovil(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void agregarRol(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void eliminarRol(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void modificarRol(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void facturarCliente(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void rendicionChofer(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void choferMayorRecaudacion(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void choferViajeMasLargo(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void clienteMayorConsumo(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void clienteMismoMovil(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void registroViajes(object sender, EventArgs e)
        {
            this.Hide();
            frmABM frmAltaCliente = new frmABM();
            frmAltaCliente.Show();
        }
        private void frmRoles_Load(object sender, EventArgs e)
        {

        }
    }
}
