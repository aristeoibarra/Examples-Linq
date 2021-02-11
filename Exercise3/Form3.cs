using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            /// Elaborar una consulta , que permita insertar la información de una región.
            /// Validar que el ID no exista En la base de datos,
            /// en el caso que exista mostrar un mensaje ‘Ya existe ese ID ’ .

            MostrarData();
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvEmpleado.DataSource = db.Region.OrderBy(x => x.RegionID).ToList();
            }
        }

        void Insertar()
        {
            using (var db = new NorthwindDataContext())
            {
                try
                {
                    int id = int.Parse(txtID.Text);
                    if (!ExiteID(id))
                    {
                        Region region = new Region
                        {
                            RegionID = id,
                            RegionDescription = txtNombre.Text
                        };

                        db.Region.InsertOnSubmit(region);

                        try
                        {
                            db.SubmitChanges();
                            MessageBox.Show("Se registro con exito");
                            Limpiar();
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show("error: " + E.Message);
                            Limpiar();
                        } 
                    }
                    else
                    {
                        MessageBox.Show("Ya existe ese ID");
                        Limpiar();
                    }                  
                }
                catch (Exception E)
                {
                    MessageBox.Show("error: " + E.Message);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        void Limpiar()
        {
            txtID.Clear();
            txtNombre.Clear();
            MostrarData();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            Insertar();
        }

        bool ExiteID(int id)
        {
            using (var db = new NorthwindDataContext())
            {
                bool existe = db.Region.Any(x => x.RegionID.Equals(id));
                return existe;
            }
        }
    }
}
