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
                else if (tabControl1.SelectedTab == tabPage2)
                {
                    MountScript();
                    tabControl1.SelectedTab = tabPage3;                   
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MountScript()
        {            
            TreeNode nodeTables = TvwTables.Nodes[0].Nodes["Tables"];
            foreach (TreeNode nodeTable in nodeTables.Nodes)
            {             
                if (nodeTable.Checked)
                {
                    TreeNode nodeColumns = nodeTable.Nodes["Columns"];
                    string strColIns = "";
                    string strColUpd = "";
                    string strColWhe = "";
                    Dictionary<string, string> dicColUpd = new Dictionary<string, string>();
                    foreach(TreeNode nodeColumn in nodeColumns.Nodes)
                    {                       
                        bool isPrimary = (nodeColumn.Tag !=null && nodeColumn.Tag.ToString() == "PK") ? true: false; 
                        
                        if (isPrimary || nodeColumn.Checked)
                        {
                            if (strColIns != "")
                            {
                                strColIns += ",\r\n";
                            }
                            strColIns += "[" + nodeColumn.Text + "] , [" + nodeColumn.Text + "] AS [" + nodeColumn.Text + "_shuffle_new],\r\n" +
                                    "ROW_NUMBER() OVER (ORDER BY NEWID()) AS [" + nodeColumn.Text + "_shuffle_order]";

                            string strUpd = "UPDATE s2\r\n" +
                                "SET [" + nodeColumn.Text + "_shuffle_new] = s1.[" + nodeColumn.Text + "]\r\n" +
                                "FROM #Shuffle s1, #Shuffle s2\r\n" +
                                "WHERE s1.[Shuffle_OriginalOrder] = s2.[" + nodeColumn.Text + "_shuffle_order]\r\n";
                            dicColUpd.Add (nodeColumn.Text , strUpd);
                            if (!isPrimary) // No update PK Columns
                            {
                                if (strColUpd != "")
                                {
                                    strColUpd += ",\r\n";
                                }
                                strColUpd += "[" + nodeColumn.Text + "] = " + "s1.[" + nodeColumn.Text + "_shuffle_new]";                            
                            }
                            else // Mount Where with PK
                            {
                                if (strColWhe != "")
                                {
                                    strColWhe += " AND ";
                                }
                                strColWhe = "s1.[" + nodeColumn.Text + "] = [" + nodeTable.Text +"].[" + nodeColumn.Text + "]";                                
                            }       
                        }                    
                    }
                    // Drop Temp Table if exist
                    string strSql = "/* Drop Temp Table if exist*/\r\n" +
                                "IF OBJECT_ID('tempdb..#Shuffle') IS NOT NULL\r\n" +
                                "BEGIN\r\n" +
                                "  DROP TABLE #Shuffle\r\n" +
                                "END\r\n" +
                                "\r\n";

                   // Insert to Temp Table
                   strSql += "/* Insert to Temp Table */\r\n" +                                            
                                "SELECT IDENTITY(INT,1,1) AS [Shuffle_OriginalOrder],\r\n" +                     	
                                strColIns + "\r\n" +
                                "INTO #Shuffle \r\n" +
                                "FROM [" + nodeTable.Text + "]\r\n\r\n";

                    //Update(s) on temp table for every columns
                    strSql += "/* Update on temp table for every column */\r\n";                    
                    foreach (string upd in dicColUpd.Values)
                    {
                        strSql += upd + "\r\n";
                    }

                    //Update on original table from Temp Table with PrimaryKey
                    strSql += "\r\n/* Update on original table from Temp Table with PrimaryKey */\r\n";                                            
                    strSql += "UPDATE [" + nodeTable.Text + "]\r\n" +
	                          "SET\r\n" +  strColUpd + "\r\n" +
                              "FROM [" + nodeTable.Text + "], #Shuffle s1\r\n" +
	                          "WHERE " + strColWhe + "\r\n\r\n";

                    // Drop Temp table
                    strSql += "\r\n/* Drop Temp Table */\r\n";                                            
                    strSql += "DROP TABLE #Shuffle\r\n\r\n";                    

                    TxtScript.Text += strSql.ToString();
                }                
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
                    SqlCommand cmd = new SqlCommand("SELECT column_name FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table ORDER BY ORDINAL_POSITION", Connection);
                    cmd.Parameters.Add("@table", SqlDbType.VarChar);
                    cmd.Parameters["@table"].Value = e.Node.Text;
                    SqlDataAdapter a= new SqlDataAdapter(cmd);
                    DataTable t1 = new DataTable();
                    a.Fill(t1);
                    foreach (DataRow drRow in t1.Rows)
                    {
                        
                        TreeNode nodeColumn = new TreeNode(drRow[0].ToString());
                        nodeColumn.Name = "Column";
                        string strSql = "SELECT COLUMN_NAME" +
                                            " FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE" +
                                            " WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1" +
                                                " AND table_name = @table" +
                                                " AND column_name = @column";
                        cmd = new SqlCommand(strSql, Connection);
                        cmd.Parameters.Add("@table", SqlDbType.VarChar);
                        cmd.Parameters["@table"].Value = e.Node.Text;
                        cmd.Parameters.Add("@column", SqlDbType.VarChar);
                        cmd.Parameters["@column"].Value = nodeColumn.Text;
                        object pk = cmd.ExecuteScalar();
                        if (pk != null)
                        {
                            nodeColumn.Tag = "PK";
                            nodeColumn.ForeColor = Color.Gray;
                        }                            
                        TreeNode nodeColumns = e.Node.FirstNode;
                        nodeColumns.Nodes.Add(nodeColumn);
                        cmd.Dispose();                    
                    }
                }
                else
                {
                    // Vacío columnas
                    TreeNode nodeColumns = e.Node.FirstNode;
                    nodeColumns.Nodes.Clear();
                }
            } 
            else if(e.Node.Name == "Columns") // Check on Columns
            {                              
                foreach (TreeNode nodeCol in e.Node.Nodes)
                {
                    nodeCol.Checked = e.Node.Checked;
                }                               
            }            
            else if (e.Node.Name == "Column") // Check on a Column
            {
                if (e.Node.Tag != null && e.Node.Tag.ToString() == "PK")
                {
                    if (e.Node.Checked == true)
                    {
                        MessageBox.Show("The fields on 'PrimaryKey' can't shuffle.");
                        e.Node.Checked = false;
                    }                    
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
            else if (tabControl1.SelectedTab == tabPage3)
            {
                tabControl1.SelectedTab = tabPage2;
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
