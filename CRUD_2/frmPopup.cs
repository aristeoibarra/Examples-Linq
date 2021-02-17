using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_2
{
    public partial class frmPopup : Form
    {
        public frmPopup()
        {
            InitializeComponent();
        }

        public string Action { get; set; }
        public int IdEmployee { get; set; }

        private void frmPopup_Load(object sender, EventArgs e)
        {
            Interfaz();
        }

        void Interfaz()
        {
            if (Action.Equals("New"))
            {
                this.Text = "New Employee";
            }
            else
            {
                this.Text = "Edit Employee";

                using (var db = new NorthwindDataContext())
                {
                    db.Employees.Where(x => x.EmployeeID.Equals(IdEmployee))
                        .ToList().ForEach(x =>
                        {
                            txtID.Text = x.EmployeeID.ToString();
                            txtFirstName.Text = x.FirstName;
                            txtLastName.Text = x.LastName;
                            txtTitle.Text = x.Title;
                            dtpFnacimiento.Value = x.BirthDate.Value;
                            txtAddress.Text = x.Address;
                        });
                }
            }          
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text.Equals(""))
            {
                errValidator.SetError(txtFirstName, "Campo Obligarorio");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(txtFirstName, null);

            if (txtLastName.Text.Equals(""))
            {
                errValidator.SetError(txtLastName, "Campo Obligarorio");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(txtLastName, null);

            if (txtTitle.Text.Equals(""))
            {
                errValidator.SetError(txtTitle, "Campo Obligarorio");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(txtTitle, null);

            if (txtAddress.Text.Equals(""))
            {
                errValidator.SetError(txtAddress, "Campo Obligarorio");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(txtAddress, null);

            if (dtpFnacimiento.Value.Year > DateTime.Now.Year)
            {
                MessageBox.Show("Año no valido");
                this.DialogResult = DialogResult.None;
                return;
            }

            if (Action.Equals("New"))
            {
                Agregar();
            }
            else
            {
                Actualizar();
            }
        }

        private void Actualizar()
        {
            using (var db = new NorthwindDataContext())
            {
                var query = db.Employees.Where(x => x.EmployeeID.Equals(IdEmployee)).ToList();

                foreach (Employees oEmployee in query)
                {
                    oEmployee.FirstName = txtFirstName.Text;
                    oEmployee.LastName = txtLastName.Text;
                    oEmployee.Title = txtTitle.Text;
                    oEmployee.BirthDate = DateTime.Parse(dtpFnacimiento.Value.ToShortDateString());
                    oEmployee.Address = txtAddress.Text;
                }

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Se actualizo correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex.Message);
                }           
            }
        }

        private void Agregar()
        {
            using (var db = new NorthwindDataContext())
            {
                Employees employee = new Employees()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Title = txtTitle.Text,
                    BirthDate = DateTime.Parse(dtpFnacimiento.Value.ToString()),
                    Address = txtAddress.Text,
                    bhabilitado = true
                };

                db.Employees.InsertOnSubmit(employee);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Se registro correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex.Message);
                }
            }
        }
    }
}
