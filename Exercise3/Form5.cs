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

            /// Realizar una actualización de datos de la tabla producto solo de el nombre del producto,
            /// id de la categoría, id del proveedor, precio y stock.
            /// Al dar click en una fila del datagridview, debe cargar la información.

            MostrarData();
            LlenarCategoria();
            LlenarProveedor();
            Limpiar();

            MostrarBotones(true, false);
            dgvProductos.Columns["ProductID"].Visible = false;
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
                                p.ProductID,
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

        void MostrarBotones(bool add, bool edit)
        {
            btnAgregar.Enabled = add;
            btnActualizar.Enabled = edit;
        }

        void Limpiar()
        {
            txtNombre.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            txtDescripcion.Clear();
            nudPrecio.Value = 0.0000m;
            nudStock.Value = 0;

            errText.SetError(cmbCategoria, null);
            errText.SetError(txtDescripcion, null);

            txtNombre.Focus();
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
                    ProductName = txtNombre.Text.Trim(),
                    CategoryID = (int)cmbCategoria.SelectedValue,
                    SupplierID = (int)cmbCategoria.SelectedValue,
                    QuantityPerUnit = txtDescripcion.Text.Trim(),
                    UnitPrice = (decimal)nudPrecio.Value,
                    UnitsInStock = (short)nudStock.Value
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Equals(""))
            {
                errText.SetError(cmbCategoria, "Dato Obligatorio");
                cmbCategoria.Focus();
                return;
            }
            else
                errText.SetError(cmbCategoria, null);

            if (txtDescripcion.Text.Equals(""))
            {
                errText.SetError(txtDescripcion, "Dato Obligatorio");
                txtDescripcion.Focus();
                return;
            }
            else
                errText.SetError(txtDescripcion, null);

            using (var db = new NorthwindDataContext())
            {
                try
                {
                    int id = (int)dgvProductos.CurrentRow.Cells[0].Value;
                    // Query the database for the row to be updated.
                    var query = db.Products.Where(x => x.ProductID.Equals(id));

                    // Execute the query, and change the column values
                    // you want to change.
                    foreach (Products oProduct in query)
                    {
                        oProduct.ProductName = txtNombre.Text.Trim();
                        oProduct.CategoryID = (int)cmbCategoria.SelectedValue;
                        oProduct.SupplierID = (int)cmbProveedor.SelectedValue;
                        oProduct.QuantityPerUnit = txtDescripcion.Text.Trim();
                        oProduct.UnitPrice = (decimal)nudPrecio.Value;
                        oProduct.UnitsInStock = (short)nudStock.Value;
                    }

                    // Submit the changes to the database.
                    try
                    {
                        db.SubmitChanges();
                        MessageBox.Show("Se actualizo correctamente");
                        Limpiar();
                        MostrarData();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("error: " + E.Message);
                        Limpiar();
                        // Provide for exceptions.
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("error: " + E.Message);
                    Limpiar();
                }
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            cmbCategoria.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString();
            cmbProveedor.Text = dgvProductos.CurrentRow.Cells[3].Value.ToString();
            txtDescripcion.Text = dgvProductos.CurrentRow.Cells[4].Value.ToString();
            nudPrecio.Value = (decimal)dgvProductos.CurrentRow.Cells[5].Value;
            nudStock.Value = (short)dgvProductos.CurrentRow.Cells[6].Value;

            MostrarBotones(false, true);
        }
    
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            MostrarBotones(true, false);
        }
    }
}
