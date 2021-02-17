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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            /// Listar los territorios , con el botón eliminar hacer una eliminación lógica de todo el registro.
            /// Importante añadir una columna “habilitado” a la tabla para poder eliminar
            /// (considerar 0 como deshabilitado y 1 como habilitado)
            /// 


            MostrarData();
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvTerritory.DataSource = db.Territories.Where(x=>x.habilitado.Equals(true))
                    .Select(x => new { x.RegionID, x.TerritoryDescription }).ToList();
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

                        var query = db.Territories.Where(x => x.RegionID.Equals(id)).ToList();

                        foreach (Territories oTerritory in query)
                        {
                            oTerritory.habilitado = false;
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
