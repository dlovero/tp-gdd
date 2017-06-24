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
            return new ToolStripMenuItem(nombre, null, new EventHandler(altaCliente), null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmIngreso.SingletonDatosUsuario datosUsuario = frmIngreso.SingletonDatosUsuario.Instance;
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
            
           /* ToolStripMenuItem mnuABM = new ToolStripMenuItem("ABM");
            ToolStripMenuItem mnuCliente = new ToolStripMenuItem("Cliente");
            ToolStripMenuItem fncAltaCliente = new ToolStripMenuItem("Agregar",null,new EventHandler(altaCliente), (Keys)Shortcut.Alt7);
            mnuCliente.DropDownItems.Add(fncAltaCliente);
            mnuABM.DropDownItems.Add(mnuCliente);
            fmPrincipalMenu.Items.Add(mnuABM);     
            fmPrincipalMenu.Items.Add(new ToolStripMenuItem("Facturacion y Rendicion"));
            fmPrincipalMenu.Items.Add(new ToolStripMenuItem("Listados"));*/
            fmPrincipal.Show();
        }

        private void altaCliente(object sender, EventArgs e)
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
