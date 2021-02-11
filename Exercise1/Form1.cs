using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        List<Empleado> listaEmpleado;
        private void Form1_Load(object sender, EventArgs e)
        {
            listaEmpleado = new List<Empleado>
            {
                new Empleado(){IdEmpleado=1 , Nombre="Felipe" , Apellidos="Contreras", IdModalidad=1},
                new Empleado(){IdEmpleado=2 , Nombre="Josue" , Apellidos="Lopez", IdModalidad=2},
                new Empleado(){IdEmpleado=3 , Nombre="Enrique" , Apellidos="Valle", IdModalidad=2},
                new Empleado(){IdEmpleado=4 , Nombre="Carmen" , Apellidos="Rojas", IdModalidad=1},
                new Empleado(){IdEmpleado=5 , Nombre="Ricardo" , Apellidos="Garma", IdModalidad=3},
                new Empleado(){IdEmpleado=6 , Nombre="Rolando" , Apellidos="Minchan", IdModalidad=3}
            };

            ///- Se pide realizar un listado de empleados (usar el control datagridview) 
            /// y solo listar el idEmpleado , nombre y apellido.

            MostrarData(listaEmpleado);

            List<Modalidad> listaModalidad = new List<Modalidad>()
            {
                new Modalidad(){IdModalidad=1 , Nombre="CAS"},
                new Modalidad(){IdModalidad=2 , Nombre="Temporal"},
                new Modalidad(){IdModalidad=3 , Nombre="Plazo Indeterminado"}
            };

            cmbOpcion.DataSource = listaModalidad;
            cmbOpcion.DisplayMember = "Nombre";
            cmbOpcion.ValueMember = "IdModalidad";
        }

        void MostrarData(List<Empleado> lista)
        {
            var empleados = lista.Select(x => new { x.IdEmpleado, x.Nombre, x.Apellidos });

            dgvEmpleado.DataSource = null;
            dgvEmpleado.DataSource = empleados.ToList();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            /// Crear un boton Ingresar que nos permita insertar un nuevo empleado (idEmpleado , nombre y apellidos)
            /// y se vea en el datagridview

            /// Validar usando el control errorProvider que ninguno de los campos se pueda ingresar vacio

            if (txtIdEmpleado.Text.Equals(""))
            {
                errDato.SetError(txtIdEmpleado, "Campo Obligatorio");
                return;
            }
            else
                errDato.SetError(txtIdEmpleado, null);
            

            if (txtNombre.Text.Equals(""))
            {
                errDato.SetError(txtNombre, "Campo Obligatorio");
                return;
            }
            else
                errDato.SetError(txtNombre, null);
            

            if (txtApellidos.Text.Equals(""))
            {
                errDato.SetError(txtApellidos, "Campo Obligatorio");
                return;
            }
            else
                errDato.SetError(txtApellidos, null);

            try
            {
                var empleado = new Empleado()
                {
                    IdEmpleado = int.Parse(txtIdEmpleado.Text),
                    Nombre = txtNombre.Text,
                    Apellidos = txtApellidos.Text
                };

                listaEmpleado.Add(empleado);

                MostrarData(listaEmpleado);
                Limpiar();
            }
            catch (FormatException Fe)
            {
                MessageBox.Show(Fe.Message);
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }  
        }

        private void Limpiar()
        {
            txtIdEmpleado.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
        }

        private void Filtrar(object sender, EventArgs e)
        {
            var buscarByApellido = listaEmpleado
                .Where(x => x.Apellidos.ToLower().Contains(txtFiltrar.Text.ToLower().Trim()));

            MostrarData(buscarByApellido.ToList());
        }

        private void OpcionFiltrar(object sender, EventArgs e)
        {
            int id = int.Parse(cmbOpcion.SelectedValue.ToString());
            var query = listaEmpleado.Where(x => x.IdModalidad.Equals(id));

            MostrarData(query.ToList());
        }


        //Con la misma lista empleado del ejercicio anterior 
        //, realizar una consulta por apellido mediante un textbox de manera sensitiva . (Usar el evento textchanged)


    }
}
