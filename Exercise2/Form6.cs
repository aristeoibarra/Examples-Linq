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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        void DisplayData()
        {
            using (var db= new PruebaDataContext())
            {
                dgvDatos.DataSource = db.Empleado.ToList();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var db = new PruebaDataContext())
            {
                string value = txtValue.Text.ToLower().Trim();
                List<Empleado> list = new List<Empleado>();
                switch (cmbOpcion.SelectedIndex)
                { 
                    case 0:
                        try
                        {
                            list = db.Empleado.Where(x => x.cve_empleado.Equals(value)).ToList();
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show("error: " + E.Message);
                            Clean();
                            return;
                        }
                        break;
                    case 1:
                        list = db.Empleado.Where(x => x.nombre_empleado.ToLower().Contains(value)).ToList();
                        break;
                    case 2:
                        list = db.Empleado.Where(x => x.apepaterno_empleado.ToLower().Contains(value)).ToList();
                        break;
                    case 3:
                        list = db.Empleado.Where(x => x.apematerno_empleado.ToLower().Contains(value)).ToList();
                        break;
                    default:
                        break;
                }
                dgvDatos.DataSource = list;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void Clean()
        {
            DisplayData();
            cmbOpcion.SelectedIndex = -1;
            txtValue.Clear();
        }
    }
}
