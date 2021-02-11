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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            //Crear un formulario que me permita filtrar por 
            //grado academico los alumnos de manera sensitiva haciendo uso de un TextBox, listando  
            //su nombre, apellido paterno , apellido materno y curso favorito

            BuscarbyGradoAcademico();
        }

        void BuscarbyGradoAcademico()
        {
            using (var db = new PruebaDataContext())
            {
                string gradoAcademico = txtValue.Text.ToLower().Trim();

                var query = from d in db.Alumno
                            where d.nivelacademico_alumno.ToLower().Contains(gradoAcademico)
                            select new
                            { 
                                d.nombre_alumno,
                                d.apepaterno_alumno, 
                                d.apematerno_alumno,
                                d.cursofavorito_alumno 
                            };

                dgvDatos.DataSource = query;
            }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            BuscarbyGradoAcademico();
        }
    }
}
