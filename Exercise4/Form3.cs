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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            /// Para hacer eliminación Lógica, probaremos con la tabla Regions,
            /// por lo que debemos de añadir un campo a cada uno de los registros que lo llamaremos “bhabilitado
            /// 
            MostrarData();

        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvRegion.DataSource = db.Region.Where(x => x.bhabilitado.Equals(true))
                    .Select(x => new { x.RegionID, x.RegionDescription })
                    .ToList();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvRegion.Rows.Count != 0)
            {
                if (MessageBox.Show("Desea Eliminar?", "Eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = new NorthwindDataContext())
                    {
                        int id = (int)dgvRegion.CurrentRow.Cells[0].Value;

                        var query = db.Region.Where(x => x.RegionID.Equals(id)).ToList();

                        foreach (Region oRegion in query)
                        {
                            oRegion.bhabilitado = false;
                        }

                        try
                        {
                            db.SubmitChanges();
                            MessageBox.Show("Se elimino con exito");
                            MostrarData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("error: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}
