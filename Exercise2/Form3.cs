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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var db = new PruebaDataContext())
            {
                string nombre = txtNombre.Text;

                dgvDatos.DataSource = db.Empleado
                    .Where(x => x.nombre_empleado.Equals(nombre));
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            MostrarData();
        }
    }
}
