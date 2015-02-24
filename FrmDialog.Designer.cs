namespace ShuffleApplication
{
    partial class FrmDialog
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tablas");
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.CmdOpenDialog = new System.Windows.Forms.Button();
            this.TxtConnectionString = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TvwTables = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CmdPrevious = new System.Windows.Forms.Button();
            this.CmdExit = new System.Windows.Forms.Button();
            this.CmdNext = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.TxtScript = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(876, 452);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.CmdOpenDialog);
            this.tabPage1.Controls.Add(this.TxtConnectionString);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(868, 426);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connection string";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connection String";
            // 
            // CmdOpenDialog
            // 
            this.CmdOpenDialog.Location = new System.Drawing.Point(169, 81);
            this.CmdOpenDialog.Name = "CmdOpenDialog";
            this.CmdOpenDialog.Size = new System.Drawing.Size(26, 23);
            this.CmdOpenDialog.TabIndex = 1;
            this.CmdOpenDialog.Text = "...";
            this.CmdOpenDialog.UseVisualStyleBackColor = true;
            this.CmdOpenDialog.Click += new System.EventHandler(this.CmdOpenDialog_Click);
            // 
            // TxtConnectionString
            // 
            this.TxtConnectionString.Location = new System.Drawing.Point(201, 84);
            this.TxtConnectionString.Multiline = true;
            this.TxtConnectionString.Name = "TxtConnectionString";
            this.TxtConnectionString.Size = new System.Drawing.Size(614, 113);
            this.TxtConnectionString.TabIndex = 0;
            this.TxtConnectionString.Text = "Data Source=SRVSQL;Initial Catalog=ChequesXPIntraSoftTest;Persist Security Info=T" +
    "rue;User ID=sa;Password=IntrasoftSA2027";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TvwTables);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(868, 426);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tablas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TvwTables
            // 
            this.TvwTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TvwTables.Location = new System.Drawing.Point(3, 3);
            this.TvwTables.Name = "TvwTables";
            treeNode1.Name = "Tables";
            treeNode1.Text = "Tablas";
            this.TvwTables.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.TvwTables.Size = new System.Drawing.Size(862, 420);
            this.TvwTables.TabIndex = 0;
            this.TvwTables.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TvwTables_AfterCheck);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CmdPrevious);
            this.panel1.Controls.Add(this.CmdExit);
            this.panel1.Controls.Add(this.CmdNext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(876, 61);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // CmdPrevious
            // 
            this.CmdPrevious.Location = new System.Drawing.Point(580, 22);
            this.CmdPrevious.Name = "CmdPrevious";
            this.CmdPrevious.Size = new System.Drawing.Size(75, 23);
            this.CmdPrevious.TabIndex = 8;
            this.CmdPrevious.Text = "<- &Previous";
            this.CmdPrevious.UseVisualStyleBackColor = true;
            this.CmdPrevious.Click += new System.EventHandler(this.CmdPrevious_Click);
            // 
            // CmdExit
            // 
            this.CmdExit.Location = new System.Drawing.Point(661, 22);
            this.CmdExit.Name = "CmdExit";
            this.CmdExit.Size = new System.Drawing.Size(75, 23);
            this.CmdExit.TabIndex = 7;
            this.CmdExit.Text = "E&xit";
            this.CmdExit.UseVisualStyleBackColor = true;
            this.CmdExit.Click += new System.EventHandler(this.CmdExit_Click);
            // 
            // CmdNext
            // 
            this.CmdNext.Location = new System.Drawing.Point(742, 22);
            this.CmdNext.Name = "CmdNext";
            this.CmdNext.Size = new System.Drawing.Size(75, 23);
            this.CmdNext.TabIndex = 6;
            this.CmdNext.Text = "&Next ->";
            this.CmdNext.UseVisualStyleBackColor = true;
            this.CmdNext.Click += new System.EventHandler(this.CmdNext_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.TxtScript);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(868, 426);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Script";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // TxtScript
            // 
            this.TxtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtScript.Location = new System.Drawing.Point(3, 3);
            this.TxtScript.Multiline = true;
            this.TxtScript.Name = "TxtScript";
            this.TxtScript.Size = new System.Drawing.Size(862, 420);
            this.TxtScript.TabIndex = 0;
            // 
            // FrmDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmDialog";
            this.Text = "Shuffle Rows From SQL Server";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CmdOpenDialog;
        private System.Windows.Forms.TextBox TxtConnectionString;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CmdPrevious;
        private System.Windows.Forms.Button CmdExit;
        private System.Windows.Forms.Button CmdNext;
        private System.Windows.Forms.TreeView TvwTables;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox TxtScript;
    }
}

