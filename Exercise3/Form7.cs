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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            /// Usando como referencia la base de datos Northwind:
            /// Mostrar en un control dataGridView,
            /// el nombre de los productos , su precio y el nombre de la categoría(tabla categories)
            /// 

            MostraData();
        }

        private void MostraData()
        {
            using (var db = new NorthwindDataContext())
            {
                var query = from p in db.Products
                            join c in db.Categories on p.CategoryID equals c.CategoryID
                            select new { p.ProductName, p.UnitPrice, c.CategoryName };

                dgvProducts.DataSource = query.ToList();
            }
        }
    }
}
