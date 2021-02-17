using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarData();
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvRegion.DataSource = db.Region.ToList();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(dgvRegion.Rows.Count != 0)
            {
                if (MessageBox.Show("Desea Eliminar?", "Eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = new NorthwindDataContext())
                    {
                        string id = dgvRegion.CurrentRow.Cells[0].Value.ToString();

                        var query = db.Region.Where(x => x.RegionID.Equals(id)).ToList();

                        foreach (Region oRegion in query)
                        {
                            db.Region.DeleteOnSubmit(oRegion);
                        }

                        try
                        {
                            db.SubmitChanges();
                            MessageBox.Show("Se elimino con exito");
                            MostrarData();

                        }
                        catch (Exception E)
                        {
                            MessageBox.Show("error: " + E.Message);
                        }
                    }
                }
            }       
        }
    }
}
