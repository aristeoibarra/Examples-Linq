
namespace Exercise4.Exercise1_CRUD
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslblNuevo = new System.Windows.Forms.ToolStripLabel();
            this.tslblEditar = new System.Windows.Forms.ToolStripLabel();
            this.tslblEliminar = new System.Windows.Forms.ToolStripLabel();
            this.tslblSalir = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreTerritorio = new System.Windows.Forms.TextBox();
            this.dgvTerritorio = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerritorio)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblNuevo,
            this.tslblEditar,
            this.tslblEliminar,
            this.tslblSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(615, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslblNuevo
            // 
            this.tslblNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblNuevo.Name = "tslblNuevo";
            this.tslblNuevo.Size = new System.Drawing.Size(56, 22);
            this.tslblNuevo.Text = "Nuevo";
            this.tslblNuevo.Click += new System.EventHandler(this.tslblNuevo_Click);
            // 
            // tslblEditar
            // 
            this.tslblEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblEditar.Name = "tslblEditar";
            this.tslblEditar.Size = new System.Drawing.Size(50, 22);
            this.tslblEditar.Text = "Editar";
            this.tslblEditar.Click += new System.EventHandler(this.tslblEditar_Click);
            // 
            // tslblEliminar
            // 
            this.tslblEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblEliminar.Name = "tslblEliminar";
            this.tslblEliminar.Size = new System.Drawing.Size(67, 22);
            this.tslblEliminar.Text = "Eliminar";
            this.tslblEliminar.Click += new System.EventHandler(this.tslblEliminar_Click);
            // 
            // tslblSalir
            // 
            this.tslblSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslblSalir.Name = "tslblSalir";
            this.tslblSalir.Size = new System.Drawing.Size(41, 22);
            this.tslblSalir.Text = "Salir";
            this.tslblSalir.Click += new System.EventHandler(this.tslblSalir_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvTerritorio);
            this.splitContainer1.Size = new System.Drawing.Size(615, 425);
            this.splitContainer1.SplitterDistance = 65;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNombreTerritorio);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(615, 65);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 20);
            this.label1.TabIndex = 44;
            this.label1.Text = "Nombre del territorio:";
            // 
            // txtNombreTerritorio
            // 
            this.txtNombreTerritorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreTerritorio.Location = new System.Drawing.Point(228, 29);
            this.txtNombreTerritorio.Name = "txtNombreTerritorio";
            this.txtNombreTerritorio.Size = new System.Drawing.Size(262, 24);
            this.txtNombreTerritorio.TabIndex = 1;
            this.txtNombreTerritorio.TextChanged += new System.EventHandler(this.txtNombreTerritorio_TextChanged);
            // 
            // dgvTerritorio
            // 
            this.dgvTerritorio.AllowUserToAddRows = false;
            this.dgvTerritorio.AllowUserToDeleteRows = false;
            this.dgvTerritorio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTerritorio.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvTerritorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTerritorio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTerritorio.Location = new System.Drawing.Point(0, 0);
            this.dgvTerritorio.Name = "dgvTerritorio";
            this.dgvTerritorio.ReadOnly = true;
            this.dgvTerritorio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTerritorio.Size = new System.Drawing.Size(615, 356);
            this.dgvTerritorio.TabIndex = 42;
            this.dgvTerritorio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTerritorio_CellClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Territorios";
            this.Load += new System.EventHandler(this.Main_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerritorio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslblNuevo;
        private System.Windows.Forms.ToolStripLabel tslblEditar;
        private System.Windows.Forms.ToolStripLabel tslblEliminar;
        private System.Windows.Forms.ToolStripLabel tslblSalir;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvTerritorio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreTerritorio;
    }
}