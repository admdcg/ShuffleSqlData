using Microsoft.Data.ConnectionUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShuffleApplication
{
    public partial class FrmDialog : Form
    {
        public FrmDialog()
        {
            InitializeComponent();
        }
        private SqlConnection _connection;
        private SqlConnection Connection
        {
            get
            {                
                return _connection;
            }
            set
            {
                _connection = value;
            }                
        }        

        private void CmdOpenDialog_Click(object sender, EventArgs e)
        {
            DataConnectionDialog dcd = new DataConnectionDialog();
            DataSource.AddStandardDataSources(dcd);
            DataConnectionDialog.Show(dcd);
            if (dcd.DialogResult == System.Windows.Forms.DialogResult.OK)            
            {
                TxtConnectionString.Text = dcd.ConnectionString;                                
            } 

            else
            {

            }                                   
        }       
        private void CmdNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    TvwTables.Nodes.Clear();
                    Connection = new SqlConnection(TxtConnectionString.Text);
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY table_name", Connection);
                    TvwTables.CheckBoxes = true;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(TxtConnectionString.Text);
                    TreeNode nodeDatabase = new TreeNode(builder.InitialCatalog);
                    nodeDatabase.Name = "Database";
                    nodeDatabase.Checked = true;
                    TreeNode nodeTables = new TreeNode("Tablas");
                    nodeTables.Name = "Tables";
                    nodeTables.Checked = true;
                    nodeDatabase.Nodes.Add(nodeTables);
                    TvwTables.Nodes.Add(nodeDatabase);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TreeNode nodeColumns = new TreeNode("Columnas");
                            nodeColumns.Name = "Columns";
                            TreeNode nodeTable = new TreeNode(reader.GetString(0));
                            nodeTable.Name = "Table";
                            nodeTable.Nodes.Add(nodeColumns);
                            nodeTables.Nodes.Add(nodeTable);
                        }
                    }
                    tabControl1.SelectedTab = tabPage2;
                    nodeDatabase.Expand();
                    nodeTables.Expand();
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TvwTables_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown) return;  //Evita la reentrada              
            
            if(e.Node.Name == "Table") // Checkea una Tabla            
            {                
                if (e.Node.Checked)
                {
                    // Carga columnas
                    SqlCommand cmd = new SqlCommand("Select column_name from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table ORDER BY ORDINAL_POSITION", Connection);
                    cmd.Parameters.Add("@table", SqlDbType.VarChar);
                    cmd.Parameters["@table"].Value = e.Node.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TreeNode nodeColumn = new TreeNode(reader.GetString(0));
                            nodeColumn.Name = "Colunm";
                            TreeNode nodeColumns = e.Node.FirstNode;
                            nodeColumns.Nodes.Add(nodeColumn);                            
                        }
                    }
                }
                else
                {
                    // Vacío columnas
                    TreeNode nodeColumns = e.Node.FirstNode;
                    nodeColumns.Nodes.Clear();
                }
            } 
            else if(e.Node.Name == "Columns") // Checkea en Columnas               
            {                              
                foreach (TreeNode nodeCol in e.Node.Nodes)
                {
                    nodeCol.Checked = e.Node.Checked;
                }                               
            }            
        }

        private void CmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CmdPrevious_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                tabControl1.SelectedTab = tabPage1;
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                CmdNext.Enabled = true;
                CmdPrevious.Enabled = false;
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                CmdNext.Enabled = true;
                CmdPrevious.Enabled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }      
    }
}
