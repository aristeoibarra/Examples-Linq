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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            MostrarData();
        }

        void MostrarData()
        {
            using (var db = new PruebaDataContext())
            {
                dgvDatos.DataSource = db.Empleado.ToList();
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            using (var db = new PruebaDataContext())
            {
                string nombre = txtNombre.Text.ToLower().Trim();

                var query = db.Empleado
                    .Where(x => x.nombre_empleado.ToLower().Contains(nombre));

                dgvDatos.DataSource = query.ToList();
            }
        }
    }
}
