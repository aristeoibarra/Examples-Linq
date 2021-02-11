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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            /// Elaborar una consulta , que permita listar el nombre del territorio,
            /// el nombre de la región , y permita filtrar en un comboBox por región.
            /// 
           
            MostrarData();
            FillRegions();
            cmbRegion.SelectedValue = -1;
        }

        private void FillRegions()
        {
            using (var db = new NorthwindDataContext())
            {
                cmbRegion.DataSource = db.Region;
                cmbRegion.DisplayMember = "RegionDescription";
                cmbRegion.ValueMember = "RegionID";
            }
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                var query = from t in db.Territories
                            join r in db.Region on t.RegionID equals r.RegionID
                            select new { t.TerritoryDescription, r.RegionDescription };

                dgvTerritorios.DataSource = query;
            }
        }

        private void cmbRegion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (var db = new NorthwindDataContext())
            {
                int id = int.Parse(cmbRegion.SelectedValue.ToString());

                var query = from t in db.Territories
                            join r in db.Region on t.RegionID equals r.RegionID
                            where r.RegionID.Equals(id)
                            select new { t.TerritoryDescription, r.RegionDescription };

                dgvTerritorios.DataSource = query;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbRegion.SelectedValue = -1;
            MostrarData();
        }
    }
}
