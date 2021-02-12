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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            /// Usando como referencia la base de datos Northwind:
            /// Ingresar un registro a la tabla(Territories).
            /// Es decir crear un formulario que permita ingresar un código de Territories,
            /// el nombre del Territories y la región a la que pertenece
            /// (la región debe estar en un combobox y el usuario debe seleccionar cual es).
            /// En la parte de abajo un DataGridView que muestre el nombre del territorio y
            /// el nombre de la region y se debe refrezcar cada vez que se ingrese un valor.

            /// Validar:
            /// -Que el código del Territories no exista, en el caso que exista mostrar una alerta que diga:
            /// El código del territorio ya existe.
            /// 
            /// -Que el nombre del Territories no exista, en el caso que exista mostrar una alerta que diga:
            /// Este territorio ya fue registrado.
            /// 
            /// -Usando un errorprovider validar que se sea obligatorio el ingreso del código del territorio,
            /// y el nombre de un territorio.

            MostrarData();
            LlenarRegions();
            Limpiar();
        }

        private void Limpiar()
        {
            txtTerritoryID.Clear();
            txtDescription.Clear();
            cmbRegion.SelectedIndex = -1;

            errValidator.SetError(txtTerritoryID, null);
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
                                            select new { t.TerritoryID, t.TerritoryDescription,r.RegionDescription};
            }
        }

        bool ExisteID(string id)
        {
            /// -Que el código del Territories no exista, en el caso que exista 
            /// mostrar una alerta que diga: El código del territorio ya existe.
            ///
            using (var db = new NorthwindDataContext())
            {
                return db.Territories.Any(x => x.TerritoryID.Equals(id));
            }
        }

        bool ExisteNombre(string name)
        {
            /// -Que el nombre del Territories no exista, en el caso que exista mostrar una alerta que diga:
            /// Este territorio ya fue registrado.
            /// 
            using (var db = new NorthwindDataContext())
            {
                return db.Territories.Any(x => x.TerritoryDescription.Equals(name));
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtTerritoryID.Text.Equals(""))
            {
                errValidator.SetError(txtTerritoryID, "Campo Obligatorio");
                return;
            }
            else
                errValidator.SetError(txtTerritoryID, null);

            if (txtDescription.Text.Equals(""))
            {
                errValidator.SetError(txtDescription, "Campo Obligatorio");
                return;
            }
            else
                errValidator.SetError(txtDescription, null);

            string idTerritory = txtTerritoryID.Text;
            string nombre = txtDescription.Text;
            int idRegion = (int)cmbRegion.SelectedValue;

            if (ExisteID(idTerritory))
            {
                MessageBox.Show("El código del territorio ya existe.");
                return;
            }

            if (ExisteNombre(nombre))
            {
                MessageBox.Show("Este territorio ya fue registrado.");
                return;
            }

            try
            {
                using (var db = new NorthwindDataContext())
                {
                    Territories territory = new Territories()
                    {
                        TerritoryID = idTerritory,
                        TerritoryDescription = nombre,
                        RegionID = idRegion
                    };

                    db.Territories.InsertOnSubmit(territory);

                    try
                    {
                        db.SubmitChanges();
                        MessageBox.Show("Se registro correctamente");
                        MostrarData();
                        Limpiar();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("error: " + E.Message);
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
