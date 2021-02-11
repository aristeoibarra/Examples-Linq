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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            MostraData();
        }

        void MostraData()
        {
            //Usando como datos la referencia la tabla . 
            //Listar en un dataGridView el nombre completo del alumno
            //(unión de nombre, apellido paterno, apellido materno),
            //su curso favorito y su nivel académico.

            using (var db = new PruebaDataContext())
            {
                var query = from d in db.Alumno
                            select new
                            {
                                NombreCompleto = String.Join(" ", d.nombre_alumno, d.apepaterno_alumno, d.apematerno_alumno),
                                CursoFavorito = d.cursofavorito_alumno,
                                NivelAcademico = d.nivelacademico_alumno
                            };

                dgvDatos.DataSource = query;
            }
        }
    }
}
