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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /// Realizar un mantenimiento de la tabla (employees),
            /// se debe de listar en el datagridview, 
            /// el código del empleado, el primer nombre, su segundo nombre, el titulo, la fecha de nacimiento, su dirección.
            /// Y se deben de registrar y actualizar los mismo campos. Buscar por nombre en el mantenimiento.
            MostrarData();
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvEmployees.DataSource = db.Employees.Where(X => X.bhabilitado.Equals(true))
                    .Select(x => new
                    {
                        x.EmployeeID,
                        x.FirstName,
                        x.LastName,
                        x.Title,
                        x.BirthDate,
                        x.Address
                    }).ToList();
            }
        }

        private void tslblNew_Click(object sender, EventArgs e)
        {
            frmPopup frm = new frmPopup();
            frm.Action = "New";
            frm.ShowDialog();

            if (frm.DialogResult.Equals(DialogResult.OK))
                MostrarData();
        }

        private void tslblEdit_Click(object sender, EventArgs e)
        {
            frmPopup frm = new frmPopup();
            frm.Action = "Edit";
            frm.IdEmployee = this.idEmployee;
            frm.ShowDialog();

            if (frm.DialogResult.Equals(DialogResult.OK))
                MostrarData();
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            using (var db = new NorthwindDataContext())
            {
                string name = txtFirstName.Text.ToLower().Trim();

                var query = from em in db.Employees
                            where em.bhabilitado.Equals(true) && em.FirstName.ToLower().Contains(name)
                            select new
                            {
                                em.EmployeeID,
                                em.FirstName,
                                em.LastName,
                                em.Title,
                                em.BirthDate,
                                em.Address
                            };

                dgvEmployees.DataSource = query.ToList();
            }
        }

        private void tslblDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.Rows.Count!=0)
            {
                if (MessageBox.Show("Desea Eliminar?", "Eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = new NorthwindDataContext())
                    {
                        var query = db.Employees.Where(x => x.EmployeeID.Equals(idEmployee)).ToList();

                        foreach (Employees OEmployees in query)
                        {
                            OEmployees.bhabilitado = false;
                        }

                        try
                        {
                            db.SubmitChanges();
                            MessageBox.Show("Se elimino correctamente");
                            MostrarData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("error: " + ex.Message);
                        }
                    }  
                }
            }
        }

        int idEmployee;
        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idEmployee = (int)dgvEmployees.CurrentRow.Cells[0].Value;
        }

        private void tslblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
