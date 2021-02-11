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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /// 1.Listar el id del territorio, 
            /// nombre de los territorios(tabla territories) 
            /// y el nombre de la región a la que corresponden(tabla región)
            MostrarData();
        }
        
        void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                var query = from t in db.Territories
                            join r in db.Region on t.RegionID equals r.RegionID
                            select new {t.TerritoryID, t.TerritoryDescription, r.RegionDescription };

                dgvTerritorios.DataSource = query;
            }
        }
    }
}
