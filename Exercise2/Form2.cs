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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MostrarData();



        }

        void MostrarData()
        {
            /*Realizar un listado de los empleados solo con 3 campos , 
             * uno de ellos es el nombre completo de la persona , 
             * el otro es la edad de la persona y el ultimo es la edad que tendrá dentro de 10 años (edad+10).*/

            using (var db = new PruebaDataContext())
            {
                var query = db.Empleado.Select(x => new
                {
                    NombreCompleto = string.Join(" ", x.nombre_empleado, x.apepaterno_empleado, x.apematerno_empleado),
                    x.edad_empleado,
                    edadEn10Anos = x.edad_empleado + 10
                });

                dgvDatos.DataSource = query.ToList();

            }
        }
    }
}
