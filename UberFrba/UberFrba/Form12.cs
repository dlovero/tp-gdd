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
            organizarComponentesEnFormulario();
            return true;
        }

        protected void organizarComponentesEnFormulario()
        {
            this.cajaListaFunciones.DataSource = obtenerListaFuncionesSegunAccion();
            this.cajaListaFunciones.DisplayMember = "funcion";
            this.cajaListaFunciones.ValueMember = "id";
        }

        protected abstract object obtenerListaFuncionesSegunAccion();
        
        protected void cargarDatosEnCuadrosDeLista()
        {
            //this.cajaListaFunciones.DataSource = listaFuncionalidades();
            //this.cajaListaFunciones.DataSource = listaFuncionalidades();

            //List<DataRow> listaFuncionalidades = obtenerFuncionalidades();
            //List<DataRow> listaFuncionalidadesSegunRol
            //    = obtenerFuncionalidadesSegunRol(idRol);
        }

        //protected List<DataRow> obtenerListaFunciones()
        protected List<String> obtenerListaFunciones()
        {
            //List<DataRow> listaFunciones = new List<DataRow>();
            List<String> listaFunciones = new List<String>();
            listaFunciones.Add("Hola");
            listaFunciones.Add("Chau");
            listaFunciones.Add("Que Tal");
            listaFunciones.Add("Buen dia");
            //return new List<DataRow>();
            return new List<String>();
        }

        protected List<String> obtenerListaFuncionesSegunRol()
        {
            //List<DataRow> listaFunciones = new List<DataRow>();
            List<String> listaFunciones = new List<String>();
            listaFunciones.Add("Hola");
            listaFunciones.Add("Chau");
            //return new List<DataRow>();
            return new List<String>();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

    public partial class frmRolAgregar : frmRol
    {
        protected override object obtenerListaFuncionesSegunAccion()
        {
            return obtenerListaFunciones();
        }

    }

    public partial class frmRolModificar : frmRol
    {
        protected override object obtenerListaFuncionesSegunAccion()
        {
            return obtenerListaFunciones().
                Except(obtenerListaFuncionesSegunRol()).ToList();
        }
    }

    public partial class frmRolEliminar : frmRol
    {
        protected override object obtenerListaFuncionesSegunAccion()
        {
            return new List<String>();
        }
    }
}
