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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            /// Hacer una inserción de un producto , ingresar nombre , categoría , proveedor , descripción , precio y stock.
            /// Listar los campos en el DataGridView.Validar con ayuda de un error provider ,que el nombre de la categoría
            /// y la descripción sean obligatorios.
            /// 
            MostrarData();
            LlenarCategoria();
            LlenarProveedor();
            Limpiar();
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                var query = from p in db.Products
                            join c in db.Categories on p.CategoryID equals c.CategoryID
                            join s in db.Suppliers on p.SupplierID equals s.SupplierID
                            select new
                            {
                                Nombre = p.ProductName,
                                Categoria = c.CategoryName,
                                Proveedor = s.CompanyName,
                                Descripcion = p.QuantityPerUnit,
                                Precio = p.UnitPrice,
                                Stock= p.UnitsInStock
                            };

                dgvProductos.DataSource = query.OrderBy(x => x.Nombre).ToList();
            }
        }

        void LlenarCategoria()
        {
            using (var db = new NorthwindDataContext())
            {
                cmbCategoria.DataSource = db.Categories;
                cmbCategoria.DisplayMember = "CategoryName";
                cmbCategoria.ValueMember = "CategoryID";
            }
        }

        void LlenarProveedor()
        {
            using (var db = new NorthwindDataContext())
            {
                cmbProveedor.DataSource = db.Suppliers;
                cmbProveedor.DisplayMember = "CompanyName";
                cmbProveedor.ValueMember = "SupplierID";
            }
        }

        void Limpiar()
        {
            txtNombre.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            txtDescripcion.Clear();
            nudPrecio.Value = 0;
            nudStock.Value = 0;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Equals(""))
            {
                errText.SetError(cmbCategoria, "Dato Obligatorio");
                cmbCategoria.Focus();
                return;
            }
            else
                errText.SetError(cmbCategoria, null);

            if(txtDescripcion.Text.Equals(""))
            {
                errText.SetError(txtDescripcion, "Dato Obligatorio");
                txtDescripcion.Focus();
                return;
            }
            else
                errText.SetError(txtDescripcion, null);

            try
            {
                Products product = new Products()
                {
                    ProductName = txtNombre.Text,
                    CategoryID = int.Parse(cmbCategoria.SelectedValue.ToString()),
                    SupplierID = int.Parse(cmbCategoria.SelectedValue.ToString()),
                    QuantityPerUnit = txtDescripcion.Text,
                    UnitPrice = decimal.Parse(nudPrecio.Value.ToString()),
                    UnitsInStock = short.Parse(nudStock.Value.ToString())
                };

                using (var db = new NorthwindDataContext())
                {
                    db.Products.InsertOnSubmit(product);

                    try
                    {
                        db.SubmitChanges();
                        MessageBox.Show("Se registro correctamente");
                        Limpiar();
                        MostrarData();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("error: " + E);
                        Limpiar();
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("error: " + E.Message);
                Limpiar();
            }
        }
    }
}
