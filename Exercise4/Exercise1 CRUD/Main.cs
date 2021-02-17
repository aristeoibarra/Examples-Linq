using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise4.Exercise1_CRUD
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }    

        private void Main_Load(object sender, EventArgs e)
        {
            MostrarData();
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvTerritorio.DataSource = from t in db.Territories
                                           join r in db.Region on t.RegionID equals r.RegionID
                                           where t.habilitado.Equals(1)
                                           select new
                                           {
                                               t.TerritoryID,
                                               t.TerritoryDescription,
                                               r.RegionDescription
                                           };
            }
          
        }

        private void tslblNuevo_Click(object sender, EventArgs e)
        {
            frmPopup form = new frmPopup();
            form.Accion = "Nuevo";
            form.ShowDialog();

            if (form.DialogResult.Equals(DialogResult.OK))
                MostrarData();
        }

        private void tslblEditar_Click(object sender, EventArgs e)
        {
            frmPopup form = new frmPopup();
            form.Accion = "Editar";
            form.IdTerritory = id;
            form.ShowDialog();

            if (form.DialogResult.Equals(DialogResult.OK))
                MostrarData();
        }

        private void txtNombreTerritorio_TextChanged(object sender, EventArgs e)
        {
            using (var db = new NorthwindDataContext())
            {
                string nombre = txtNombreTerritorio.Text.ToLower().Trim();

                var query = from t in db.Territories
                            join r in db.Region on t.RegionID equals r.RegionID
                            where t.habilitado == true && t.TerritoryDescription.ToLower().Contains(nombre)
                            select new
                            {
                                t.TerritoryID,
                                t.TerritoryDescription,
                                r.RegionDescription
                            };

                dgvTerritorio.DataSource = query.ToList();
            }
        }

        private void tslblEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTerritorio.Rows.Count != 0)
            {
                if (MessageBox.Show("Desea Eliminar?", "Eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = new NorthwindDataContext())
                    {
                        var query = db.Territories.Where(x => x.TerritoryID.Equals(id)).ToList();

                        foreach (Territories oTerritory in query)
                        {
                            oTerritory.habilitado = false;
                        }

                        try
                        {
                            db.SubmitChanges();
                            MessageBox.Show("Se elimino correctamente");
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

        string id;
        private void dgvTerritorio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Obtener a ID
            id = dgvTerritorio.CurrentRow.Cells[0].Value.ToString();
        }

        private void tslblSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
