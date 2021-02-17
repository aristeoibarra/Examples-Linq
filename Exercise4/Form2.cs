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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            /// Se tiene la lista de territories, agregar un botón que diga eliminar y
            /// me muestre una alerta indicando si es que se desea eliminar un ítem de la base de datos,
            /// en el caso que sea si , que se elimine.En el caso que sea no, que no se haga ningún cambio.
            MostrarData();
        }

        void MostrarData()
        {
            using (NorthwindDataContext db = new NorthwindDataContext())
            {

                dgvTerritory.DataSource = from t in db.Territories
                                           select new { t.TerritoryID, t.TerritoryDescription, t.RegionID };
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTerritory.Rows.Count != 0)
            {
                if (MessageBox.Show("Desea Eliminar?", "Eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = new NorthwindDataContext())
                    {
                        string id = dgvTerritory.CurrentRow.Cells[0].Value.ToString();

                        var query = db.Territories.Where(x => x.TerritoryID.Equals(id)).ToList();

                        foreach (Territories oTerritory in query)
                        {
                            db.Territories.DeleteOnSubmit(oTerritory);
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
