using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UberFrba
{
    public partial class frmIngreso : Form
    {
        public class SingletonDatosUsuario
        {
            public class DatosUsuario
        {
            private int idUsuario;
            public int IdUsuario
            {
                get { return idUsuario; }
                set { idUsuario = value; }
            }
            private int rolId;
            public int RolId
            {
                get { return rolId; }
                set { rolId = value; }
            }
            private String nombreUsuario;
            public String NombreUsuario
            {
                get { return nombreUsuario; }
                set { nombreUsuario = value; }
            }
            private String nombre;
            public String Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }
            private String apellido;
            public String Apellido
            {
                get { return apellido; }
                set { apellido = value; }
            }
        }
           private static SingletonDatosUsuario instance;
           private DatosUsuario datosUsuario;
           
           public SingletonDatosUsuario() { }
           public SingletonDatosUsuario(int id, String nombreUsuario, String nombre, String apellido) { 
               datosUsuario = new DatosUsuario();
                this.datosUsuario.IdUsuario = id;
               this.datosUsuario.NombreUsuario = nombreUsuario;
               this.datosUsuario.Nombre = nombre;
               this.datosUsuario.Apellido = apellido;
               instance = this;
           }

           public void setearRolId(int rolId)
           {
               this.datosUsuario.RolId = rolId;
           }
           public static SingletonDatosUsuario Instance
           {
              get 
              {
                 if (instance == null)
                 {
                     instance = new SingletonDatosUsuario();
                 }
                 return instance;
              }
           }
        }
        static string sha256(string clave)
        {
            System.Security.Cryptography.SHA256Managed encriptador
                = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] claveEncriptada
                = encriptador.ComputeHash(Encoding.UTF8.GetBytes(clave), 0, Encoding.UTF8.GetByteCount(clave));
            foreach (byte unByte in claveEncriptada)
            {
                hash.Append(unByte.ToString("x2"));
            }
            return hash.ToString();
        }
        class Usuario
        {
            private int idUsuario;
            private List<Tuple<int, String>> roles;

            public int obtenerIdUsuario()
            {
                return idUsuario;
            }

            public void setearIdUsuario(int usuario)
            {
                idUsuario = usuario;
            }

            public void agregarRol(int idRol, String rol)
            {
                roles.Add(new Tuple<int, String>(idRol, rol));
            }

            public List<Tuple<int, String>> obtenerRoles()
            {
                return roles;
            }
        }
        public frmIngreso()
        {
            InitializeComponent();           
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
                GD1C2017DataSetTableAdapters.PRC_VALIDAR_USUARIOTableAdapter adaptador
                    = new GD1C2017DataSetTableAdapters.PRC_VALIDAR_USUARIOTableAdapter();
                DataTable tblUsuarioYRoles = adaptador.validarUsuario(textoUsuario.Text, sha256(textoClave.Text));
            List<Tuple<String,String>> roles = new List<Tuple<string,string>>();
            int codigoUsuario=0;
            String nombreUsuario = "", apellidoUsuario="";
                foreach (DataRow fila in tblUsuarioYRoles.Rows)
                {
                    codigoUsuario = fila.Field<int>("UserId");
                    nombreUsuario = fila.Field<String>("Nombre");
                    apellidoUsuario = fila.Field<String>("Apellido");
                }

                switch (codigoUsuario)
                {
                    case -1:
                         MessageBox.Show("Usuario no existe.");
                        break;
                    case -2:
                        MessageBox.Show("Usuario Bloqueado.");
                        break;
                    case -3:
                        MessageBox.Show("Usuario o Clave Incorrecta.");
                        break;
                    default:
                        this.Hide();
                        SingletonDatosUsuario datosUsuario = new SingletonDatosUsuario(codigoUsuario, textoUsuario.Text, nombreUsuario, apellidoUsuario);
                        frmRoles fmRoles = new frmRoles();
                        ComboBox frmRolComboRol = (ComboBox)fmRoles.Controls["comboRol"];
                        frmRolComboRol.DataSource = tblUsuarioYRoles;
                        frmRolComboRol.DisplayMember = "Rol_Nombre";
                        frmRolComboRol.ValueMember = "Rol_Id";
                        fmRoles.Show();
                        break;
                }
            }

        private void frmIngreso_Load(object sender, EventArgs e)
        {

        }
        }
    }