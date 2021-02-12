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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            /// Usando como referencia la base de datos Northwind
            /// Realizar una consulta de productos por categoría
            /// (Se tiene que listar las categorías en un comboBox para que el usuario pueda hacer el filtro) 
            /// y al dar click en el botón 'Buscar' se activara la búsqueda,
            /// que listara el nombre del producto, nombre de categoría y stock
            /// 

            MostrarData();
            LlenarCategorias();
            cmbCategoria.SelectedIndex = -1;
        }

        private void LlenarCategorias()
        {
            using (var db = new NorthwindDataContext())
            {
                cmbCategoria.DataSource = db.Categories.ToList();
                cmbCategoria.DisplayMember = "CategoryName";
                cmbCategoria.ValueMember = "CategoryID";
            }
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvProducts.DataSource = from p in db.Products
                                         join c in db.Categories on p.CategoryID equals c.CategoryID
                                         select new { p.ProductName, c.CategoryName, p.UnitsInStock };
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var db = new NorthwindDataContext())
            {
                int idCategory = (int)cmbCategoria.SelectedValue;

                dgvProducts.DataSource = from p in db.Products
                                         join c in db.Categories on p.CategoryID equals c.CategoryID
                                         where c.CategoryID.Equals(idCategory)
                                         select new { p.ProductName, c.CategoryName, p.UnitsInStock };
            }
        }
    }
}
