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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            MostrarData();
        }

        private void MostrarData()
        {
            using(var db = new PruebaDataContext())
            {
                dgvDatos.DataSource = db.Empleado.ToList();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            using (var db = new PruebaDataContext())
            {
                int rango1 = int.Parse(nudRango1.Value.ToString());
                int rango2 = int.Parse(nudRango2.Value.ToString());

                var query = from d in db.Empleado
                            where d.edad_empleado >= rango1 && d.edad_empleado <= rango2
                            select d;

                dgvDatos.DataSource = query.ToList();
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
