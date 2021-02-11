using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise2
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            //Crear un formulario que me permita buscar un alumno por su ID.
            //Mostrar solo su nombre y sus 4 notas
            MostrarData();
            txtBuscarID.Focus();
        }

        private void MostrarData()
        {
            using (var db = new PruebaDataContext())
            {
                var query = from d in db.Alumno
                            select new
                            {
                                d.nombre_alumno,
                                d.nota1_alumno,
                                d.nota2_alumno,
                                d.nota3_alumno,
                                d.nota4_alumno,
                            };

                dgvDatos.DataSource = query;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var db = new PruebaDataContext())
            {
                try
                {
                    int id = int.Parse(txtBuscarID.Text);

                    var query = from d in db.Alumno
                                where d.cve_alumno.Equals(id)
                                select new
                                {
                                    d.nombre_alumno,
                                    d.nota1_alumno,
                                    d.nota2_alumno,
                                    d.nota3_alumno,
                                    d.nota4_alumno,
                                };
                    dgvDatos.DataSource = query;
                }
                catch (Exception E)
                {
                    MessageBox.Show("Error: " + E.Message);
                    Limpiar();
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            txtBuscarID.Clear();
            MostrarData();
            txtBuscarID.Focus();
        }
    }
}
