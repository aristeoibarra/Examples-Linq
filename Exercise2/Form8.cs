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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            MostraData();
        }

        private void MostraData()
        {
            //Listar en un dataGridView el nombre completo de la persona y su promedio de sus 4 notas.
            //Observación: El promedio se saca de la siguiente forma: 
            //(nota1 + nota2 + nota3 + nota4) / 4

            using (var db = new PruebaDataContext())
            {
                var query = from d in db.Alumno
                            select new
                            {
                                NombreCompleto = String.Join(" ", d.nombre_alumno, d.apepaterno_alumno, d.apematerno_alumno),
                                Promedio = (d.nota1_alumno + d.nota2_alumno + d.nota3_alumno + d.nota4_alumno) / 4
                            };
                dgvDatos.DataSource = query;
            }
        }
    }
}
