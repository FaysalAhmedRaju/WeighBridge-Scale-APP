using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ScaleApp
{
    public partial class AddBdTruck : Form
    {
        public string constr = "";
        public AddBdTruck()
        {
            InitializeComponent();
            constr = constring();
        }

        public string constring()
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string manifest = textBox1.Text;
            string truckNo = textBox2.Text;
            string driverName = textBox3.Text;
            if (manifest=="" || truckNo=="" ||  driverName=="")
            {
                MessageBox.Show("All Information Should be Given.");
            }
            else
            {
            string strSelectId = "SELECT id FROM manifests WHERE manifest='" + manifest + "'";
            MySqlConnection con = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand(strSelectId, con);
            con.Open();
            MySqlDataReader mred = cmd.ExecuteReader();
            string manifest_id = "";
            while (mred.Read())
            {
                manifest_id = mred.GetString("id");
            }
            mred.Close();
            string strInsert = "INSERT INTO truck_deliverys(manf_id,truck_no,driver_name) VALUES('" + manifest_id +
                               "','" + truckNo + "','" + driverName + "')";
            MySqlCommand cmdInsert = new MySqlCommand(strInsert, con);
            int st = cmdInsert.ExecuteNonQuery();
            //int st = 1;
            if (st == 1)
            {
                MessageBox.Show("Truck added.");
                this.Close();
            }
                con.Close();

            }

    }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
