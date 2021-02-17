using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise4.Exercise1_CRUD
{
    public partial class frmPopup : Form
    {
        public string Accion { get; set; }
        public string IdTerritory { get; set; }

        public frmPopup()
        {
            InitializeComponent();
        }

        private void frmPopup_Load(object sender, EventArgs e)
        {
            LlenarComboRegions();
            Interfaz();
        }

        void Interfaz()
        {
            cmbRegion.SelectedIndex = -1;
            txtID.Focus();

            if (Accion.Equals("Nuevo"))
                this.Text = "Ingrese Territorio";
            else
            {
               
                this.Text = "Actualizar Territorio";

                using (var db = new NorthwindDataContext())
                {
                    db.Territories.Where(x => x.TerritoryID.Equals(IdTerritory))
                        .ToList().ForEach(x =>
                        {
                            txtID.Text = x.TerritoryID;
                            txtNombre.Text = x.TerritoryDescription;
                            cmbRegion.SelectedValue = x.RegionID;
                        });
                }

                txtID.ReadOnly = true;
            }
        }

        private void LlenarComboRegions()
        {
            using (var db = new NorthwindDataContext())
            {
                cmbRegion.DataSource = db.Region.ToList();
                cmbRegion.DisplayMember = "RegionDescription";
                cmbRegion.ValueMember = "RegionID";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Equals(""))
            {
                errValidator.SetError(txtID, "Campo Obligatorio");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(txtID, null);

            if (txtNombre.Text.Equals(""))
            {
                errValidator.SetError(txtNombre, "Campo Obligatorio");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(txtNombre, null);

            if (cmbRegion.Text.Equals(""))
            {
                errValidator.SetError(cmbRegion, "Campo Obligatorio");
                this.DialogResult = DialogResult.None;
                return;
            }
            else
                errValidator.SetError(cmbRegion, null);       

            if (Accion.Equals("Nuevo"))
            {
                Agregar();
            }
            else
            {
                Actualizar();
            }
        }

        private void Agregar()
        {
            if (ExisteID(txtID.Text) == true)
            {
                MessageBox.Show("El ID ya existe");
                this.DialogResult = DialogResult.None;
                return;
            }

            if (ExisteNombre(txtNombre.Text) == true)
            {
                MessageBox.Show("El nombre ya existe");
                this.DialogResult = DialogResult.None;
                return;
            }

            using (var db = new NorthwindDataContext())
            {
                Territories territory = new Territories()
                {
                    TerritoryID = txtID.Text,
                    TerritoryDescription = txtNombre.Text,
                    RegionID = (int)cmbRegion.SelectedValue,
                    habilitado = true
                };

                db.Territories.InsertOnSubmit(territory);

                try
                {
                    db.SubmitChanges();
                    MessageBox.Show("Se ingreso Correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex.Message);
                }
            }
        }


        private void Actualizar()
        {
            using (var db = new NorthwindDataContext())
            {
                var query = db.Territories.Where(x => x.TerritoryID.Equals(IdTerritory)).ToList();

                foreach (Territories oTerritory in query)
                {
                    oTerritory.TerritoryDescription = txtNombre.Text;
                    oTerritory.RegionID = (int)cmbRegion.SelectedValue;
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

        bool ExisteID(string id)
        {
            using (var db = new NorthwindDataContext())
            {
                return db.Territories.Any(x => x.TerritoryID.Equals(id));
            }
        }

        bool ExisteNombre(string nombre)
        {
            using (var db = new NorthwindDataContext())
            {
                return db.Territories.Any(x => x.TerritoryDescription.Equals(nombre));
            }
        }

    }
}
