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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            /// Realizar una actualización de datos de la tabla Regions,
            /// al dar click en una fila del datagridview, 
            /// debe cargar esa información en los controles , al dar actualizar
            MostrarData();
        }

        void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvRegion.DataSource = db.Region.ToList();
            }
        }

        private void dgvRegion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvRegion.CurrentRow.Cells[0].Value.ToString();
            txtNombre.Text = dgvRegion.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == string.Empty || txtNombre.Text== string.Empty)
            {
                MessageBox.Show("Campos Vacios");
                return;
            }

            using (var db = new NorthwindDataContext())
            {
                try
                {
                    int id = int.Parse(txtID.Text);
                    // Query the database for the row to be updated.
                    var query =
                        from r in db.Region
                        where r.RegionID.Equals(id)
                        select r;

                    // Execute the query, and change the column values
                    // you want to change.
                    foreach (Region oRegion in query)
                    {
                        oRegion.RegionID = id;
                        oRegion.RegionDescription = txtNombre.Text.Trim();
                        // Insert any additional changes to column values.
                    }

                    // Submit the changes to the database.
                    try
                    {
                        db.SubmitChanges();
                        MessageBox.Show("Se actualizo correctamente");
                        Limpiar();
                        MostrarData();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("error: " + E.Message);
                        Limpiar();
                        // Provide for exceptions.
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("error: " + E.Message);
                    Limpiar();
                }
            }         
        }

        private void Limpiar()
        {
            txtID.Clear();
            txtNombre.Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
