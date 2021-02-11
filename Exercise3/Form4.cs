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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            /// Registrar en la tabla Suppliers los campos
            /// companyName , contactName , contactTitle , Address y City.
            /// Validar que el  companyName no exista en la base de datos,
            /// el IdSupliers es un autogenerado. 
            /// 
            MostrarData();
            txtCompanyName.Focus();
        }

        void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvSuppliers.DataSource = db.Suppliers.Select(x => new
                {
                    x.CompanyName,
                    x.ContactName,
                    x.ContactTitle,
                    x.Address,
                    x.City
                });
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var db = new NorthwindDataContext())
            {
                string companyName = txtCompanyName.Text;

                if (!ExisteCompanyName(companyName))
                {
                    // Create a new Order object.
                    Suppliers suppliers = new Suppliers
                    {
                        CompanyName = companyName,
                        ContactName = txtContactName.Text,
                        ContactTitle = txtContactTitle.Text,
                        Address = txtAddress.Text,
                        City = txtCity.Text
                    };

                    // Add the new object to the Orders collection.
                    db.Suppliers.InsertOnSubmit(suppliers);

                    // Submit the change to the database.
                    try
                    {
                        db.SubmitChanges();
                        MessageBox.Show("Se registro con exito");
                        Limpiar();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("error: " + E.Message);
                        Limpiar();
                    } 
                }
                else
                {
                    MessageBox.Show("Ya existe ese company Name");
                    txtCompanyName.Clear();
                    txtCompanyName.Focus();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtCompanyName.Clear();
            txtContactName.Clear();
            txtContactTitle.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            MostrarData();
            txtCompanyName.Focus();
        }

        bool ExisteCompanyName(string companyName)
        {
            using (var db = new NorthwindDataContext())
            {
                bool existe = db.Suppliers.Any(x => x.CompanyName.Equals(companyName));
                return existe;
            }
        }
    }
}
