using System;
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
            int rolId = Convert.ToInt16(comboRol.SelectedValue);
            this.Hide();
            datosUsuario.configurarRol(rolId, comboRol.Text);
            
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

        private void agregarCliente(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.agregarClienteChofer("Cliente");
        }
        
        private void eliminarCliente(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.eliminarClienteChofer("Cliente");
        }

        private void modificarCliente(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.modificarClienteChofer("Cliente");
        }

        private void agregarChofer(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.agregarClienteChofer("Chofer");
        }

        private void eliminarChofer(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.eliminarClienteChofer("Chofer");
        }

        private void modificarChofer(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.modificarClienteChofer("Chofer");
        }

        private void agregarAutomovil(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.agregarAutomovil("Automovil");
        }
        private void eliminarAutomovil(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.eliminarAutomovil("Automovil");
        }
        private void modificarAutomovil(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.modificarAutomovil("Automovil");
        }
        private void agregarRol(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.agregarTurno("Rol");
        }
        private void eliminarRol(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.eliminarTurno("Rol");
        }
        private void modificarRol(object sender, EventArgs e)
        {
            this.Hide();
            SingletonDatosUsuario.Instance.rol.modificarTurno("Rol");
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
    }
}
