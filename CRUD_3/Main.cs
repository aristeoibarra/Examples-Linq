using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_3
{
    public partial class Main : Form
    {
        private int idProduct = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /// 1.Realizar un mantenimiento a la tabla producto.
            /// Actualizar e ingresar los campos
            /// nombre de producto, id de categoría(combobox) y id de proveedor(combobox).
            /// En el datagridview me debe listar el Id del producto, nombre del producto,
            /// el nombre de la categoría(tabla categories) , nombre del proveedor(tabla suppliers).
            /// Ademas me debe permitir una consulta sensitiva por nombre.
            /// Ademas debe permitir hacer una eliminación de la tabla producto(eliminación lógica).
            /// Para eso ir a la tabla products y añadir un campo bhabilitado(donde 0 es inhabilitado y 1 es habilitado).
            /// 

            DisplayData();
            RefreshForm();
        }

        private void RefreshForm()
        {
            dgvProducts.ClearSelection();
            idProduct = 0;
        }

        private void DisplayData()
        {
            txtProductName.Clear();

            using (var db = new NorthwindDataContext())
            {
                dgvProducts.DataSource = (from p in db.Products
                                         join c in db.Categories on p.CategoryID equals c.CategoryID
                                         join s in db.Suppliers on p.SupplierID equals s.SupplierID
                                         where p.bhabilitado.Equals(true)
                                         select new
                                         {
                                             p.ProductID,
                                             p.ProductName,
                                             c.CategoryName,
                                             s.CompanyName
                                         }).ToList();
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idProduct = (int)dgvProducts.CurrentRow.Cells[0].Value;
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            using (var db = new NorthwindDataContext())
            {
                string name = txtProductName.Text.ToLower().Trim();

                var query = from p in db.Products
                                          join c in db.Categories on p.CategoryID equals c.CategoryID
                                          join s in db.Suppliers on p.SupplierID equals s.SupplierID
                                          where p.ProductName.ToLower().Contains(name)
                                          select new
                                          {
                                              p.ProductID,
                                              p.ProductName,
                                              c.CategoryName,
                                              s.CompanyName
                                          };

                dgvProducts.DataSource = query.ToList();
            }
            RefreshForm();
        }

        private void tslblNew_Click(object sender, EventArgs e)
        {           
            frmPopup frm = new frmPopup();
            frm.Action = "New";
            frm.ShowDialog();

            if (frm.DialogResult.Equals(DialogResult.OK))
                DisplayData();

            RefreshForm();
        }

        private void tslblEdit_Click(object sender, EventArgs e)
        {
            if (idProduct > 0)
            {
                frmPopup frm = new frmPopup();
                frm.Action = "Edit";
                frm.IdProduct = this.idProduct;
                frm.ShowDialog();

                if (frm.DialogResult.Equals(DialogResult.OK))
                    DisplayData();
                    
                RefreshForm();
            }
            else
            {
                MessageBox.Show("Select a record");
            }
        }  

        private void tslblDelete_Click(object sender, EventArgs e)
        {

            if (idProduct > 0)
            {
                if (dgvProducts.Rows.Count != 0)
                {
                    if (MessageBox.Show("Desea eliminar?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (var db = new NorthwindDataContext())
                        {
                            var query = db.Products.Where(x => x.ProductID.Equals(idProduct)).ToList();

                            foreach (Products oProduct in query)
                            {
                                oProduct.bhabilitado = false;
                            }

                            try
                            {
                                db.SubmitChanges();
                                MessageBox.Show("Successful removal");
                                DisplayData();
                                RefreshForm();                             
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("error: " + ex.Message);
                            }
                        }
                    }
                } 
            }
            else
            {
                MessageBox.Show("Select a record");
            }
        }

        private void tslblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
