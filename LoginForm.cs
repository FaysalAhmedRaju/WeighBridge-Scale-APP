using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Security.Cryptography;



namespace ScaleApp
{
    public partial class LoginForm : Form
    {
        public string usr = "";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constr = "";

            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Plesae enter Login ID;");
                textBox1.Focus();
                return;

            }
            if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Plesae enter Password;");
                textBox2.Focus();
                return;
            }
            int pass = 0;
            constr = constring();
            if (constr.Equals("")) return;

            MySqlConnection con = new MySqlConnection(constr);
            string query = @"SELECT COUNT(*) AS cnt,u.id, pu.port_id,p.port_name,p.port_alias,
                                p.Operator_description,p.port_add1 
                                FROM users AS u
                                JOIN port_user AS pu ON pu.user_id=u.id
                                JOIN ports AS p ON p.id = pu.port_id 
                                WHERE u.username='" + textBox1.Text.Trim() + "' AND u.password1=SHA1('" + textBox2.Text.Trim() + "') ";

            MySqlCommand cmd = new MySqlCommand(query, con);


            con.Open();
            MySqlDataReader mred = cmd.ExecuteReader();

            if (mred.Read())
            {
                //pass = mred.GetString("cnt");
                pass = mred.GetInt16("cnt");
                if (pass == 1)
                {


                    //utility.UserId = mred.GetUInt16("id");
                    //utility.UserName = textBox1.Text;
                    //utility.PortId= mred.GetString("port_id");
                    //utility.PortName = mred.GetString("port_name");
                    //utility.PortAdress= mred.GetString("port_add1");
                    //MessageBox.Show(utility.UserName);
                    ReportViewForm rvForm = new ReportViewForm();
                    rvForm.username = textBox1.Text;
                    usr = textBox1.Text;
                    int userId = mred.GetUInt16("id");
                    WeighbridgeEntry frm = new WeighbridgeEntry();
                    this.Hide();
                    frm.userName = textBox1.Text;
                    frm.userID = userId;
                    frm.portName = mred.GetString("port_name");
                    frm.portId = mred.GetString("port_id");
                    frm.portAdress = mred.GetString("port_add1");
                 

                    frm.constr = constr;
                    frm.Show();// this.Dispose();
                    


                }
                else
                {
                    MessageBox.Show("Wrong Password;");
                    textBox2.Focus();
                    con.Close();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Plesae enter Correct Login ID;");
                textBox1.Focus();
                con.Close();
                return;
            }



            con.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Equals(""))
                {
                    MessageBox.Show("Plesae enter Login ID;");
                    textBox1.Focus();
                    return;

                }
                
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (textBox2.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Password ");
                    textBox2.Focus();
                    return;

                }

                button1.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private string constring()
        {
            string server = "", dbname = "", uname = "", upass = "";
            string constr = "Server=ServerName;Database=DataBaseName;UID=username;Password=password";

            string filePath = @"configfile.dat";
            //  string line;
            string firstline = "";

            if (File.Exists(filePath))

            {
                StreamReader file = null;
                try
                {
                    file = new StreamReader(filePath);


                       firstline = file.ReadLine();


                       String[] Sline = firstline.Split('|');

                    server = Sline[0];
                    dbname = Sline[1];
                    uname = Sline[2];
                    upass = Sline[3];
                  
                    constr = " Server=" + server + ";Database=" + dbname + ";UID=" + uname + ";Password=" + upass;

                    return constr;
                }
                catch (Exception ex)
                {
                    if (file != null)
                        file.Close();
                    MessageBox.Show(ex.Message);

                }

            }
            else
            {
                MessageBox.Show("Please configure Database or Contact System Administrator.");
                return "";
            }
            return constr;

        }

        private void WeighScaleLoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
