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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            //Crear un formulario que permita listar el nombre del alumno, la suma de sus notas.
            //permitir ingresar 2 numeros , y filtrar solo aquellos que la suma de sus notas esten entre ese rango
            MostrarData();
        }
        void MostrarData()
        {
            using (var db = new PruebaDataContext())
            {
                dgvDatos.DataSource = db.Alumno.Select(x => new
                {
                    x.nombre_alumno,
                    SumaNotas = (x.nota1_alumno + x.nota2_alumno + x.nota3_alumno+ x.nota4_alumno)
                });
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            using (var db = new PruebaDataContext())
            {
                dgvDatos.DataSource = db.Alumno
                    .Select(x => new
                    {
                        x.nombre_alumno,
                        SumaNotas = (x.nota1_alumno + x.nota2_alumno + x.nota3_alumno + x.nota4_alumno)
                    }).Where(x => x.SumaNotas >= nudRango1.Value && x.SumaNotas <= nudRango2.Value);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            nudRango1.Value = 0;
            nudRango2.Value = 0;
            MostrarData();
        }
    }
}
