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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            /// Usando como referencia la base de datos Northwind:
            /// Actualizar un registro en la tabla(Territories).
            /// Es decir crear un formulario que permita actualizar el nombre del Territories 
            /// y la región a la que pertenece(la región debe estar en un combobox y el usuario debe seleccionar cual es).
            /// En el formulario debe aparecer un datagridview listando los datos,
            /// al dar click en una fila, debe mostrar todos los datos tanto en en los textboxs como en los comboboxs 
            /// y al dar click en el botón actualizar se debe modificar los valores. (El Id del territorio no puede ser modificado)
            /// 
            /// -Validar antes de actualizar que el nombre del territorio no este vació(Validar con un errorProvider)
            /// 
            MostrarData();
            LlenarRegions();
            Limpiar();
        }

        private void Limpiar()
        {
            txtTerritoryID.Clear();
            txtDescription.Clear();
            cmbRegion.SelectedIndex = -1;

            errValidator.SetError(txtDescription, null);
        }

        private void LlenarRegions()
        {
            using (var db = new NorthwindDataContext())
            {
                cmbRegion.DataSource = db.Region.ToList();
                cmbRegion.DisplayMember = "RegionDescription";
                cmbRegion.ValueMember = "RegionID";
            }
        }

        private void MostrarData()
        {
            using (var db = new NorthwindDataContext())
            {
                dgvTerritories.DataSource = from t in db.Territories
                                            join r in db.Region on t.RegionID equals r.RegionID
                                            select new { t.TerritoryID, t.TerritoryDescription, r.RegionDescription };
            }
        }

        private void dgvTerritories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTerritoryID.Text = dgvTerritories.CurrentRow.Cells[0].Value.ToString();
            txtDescription.Text = dgvTerritories.CurrentRow.Cells[1].Value.ToString();
            cmbRegion.Text = dgvTerritories.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text == string.Empty)
            {
                errValidator.SetError(txtDescription, "Campo Obligatorio");
                return;
            }
            else
                errValidator.SetError(txtDescription, null);

            using (var db = new NorthwindDataContext())
            {
                try
                {
                    string id = txtTerritoryID.Text;

                    var query = from t in db.Territories
                                where t.TerritoryID.Equals(id)
                                select t;

                    foreach (Territories oTerritory in query)
                    {
                        oTerritory.TerritoryID = id;
                        oTerritory.TerritoryDescription = txtDescription.Text;
                        oTerritory.RegionID = (int)cmbRegion.SelectedValue;
                    }

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
}
