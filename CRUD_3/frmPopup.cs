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
    public partial class frmPopup : Form
    {
        public int IdProduct { get; set; }
        public string Action { get; set; }

        public frmPopup()
        {
            InitializeComponent();
        }

        private void frmPopup_Load(object sender, EventArgs e)
        {
            FillCategories();
            FillSupplier();

            txtProductName.Focus();
            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;

            if (Action.Equals("New"))
            {
                this.Text = "New Product";
            }
            else
            {
                this.Text = "Edit Product";
                GetData(IdProduct);
            }               
        }

        private void GetData(int idProduct)
        {
            using (var db = new NorthwindDataContext())
            {
                (from p in db.Products
                 join c in db.Categories on p.CategoryID equals c.CategoryID
                 join s in db.Suppliers on p.SupplierID equals s.SupplierID
                 where p.ProductID.Equals(idProduct)
                 select new
                 {
                     p.ProductName,
                     c.CategoryName,
                     s.CompanyName
                 })
                 .ToList()
                 .ForEach(x =>
                 {
                     txtProductName.Text = x.ProductName;
                     cmbCategory.Text = x.CategoryName;
                     cmbSupplier.Text = x.CompanyName;
                 });
            }
        }

        private void FillCategories()
        {
            using (var db = new NorthwindDataContext())
            {
                cmbCategory.DataSource = db.Categories.ToList();
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
            }
        }

        private void FillSupplier()
        {
            using (var db = new NorthwindDataContext())
            {
                cmbSupplier.DataSource = db.Suppliers.ToList();
                cmbSupplier.DisplayMember = "CompanyName";
                cmbSupplier.ValueMember = "SupplierID";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Equals(""))
            {
                errValidator.SetError(txtProductName, "Obligatory field");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(txtProductName, null);

            if (cmbCategory.Text.Equals(""))
            {
                errValidator.SetError(cmbCategory, "Obligatory field");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(cmbCategory, null);

            if (cmbSupplier.Text.Equals(""))
            {
                errValidator.SetError(cmbSupplier, "Obligatory field");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(cmbSupplier, null);


            if (Action.Equals("New"))
                AddProduct();
            else
                UpdateProduct(IdProduct);
        }

        private void AddProduct()
        {
            using (var db = new NorthwindDataContext())
            {
                Products oProduct = new Products
                {
                    ProductName = txtProductName.Text,
                    CategoryID = (int)cmbCategory.SelectedValue,
                    SupplierID = (int)cmbSupplier.SelectedValue,
                    bhabilitado = true
                };

                db.Products.InsertOnSubmit(oProduct);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Successfully registered");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex.Message);
                }
            }
        }

        private void UpdateProduct(int idProduct)
        {
            using (var db = new NorthwindDataContext())
            {
                var query = db.Products.Where(x => x.ProductID.Equals(idProduct));

                foreach (Products oProduct in query)
                {
                    oProduct.ProductName = txtProductName.Text;
                    oProduct.CategoryID = (int)cmbCategory.SelectedValue;
                    oProduct.SupplierID = (int)cmbSupplier.SelectedValue;
                }

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("successfully updated");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex.Message);
                }               
            }
        }
    }
}
