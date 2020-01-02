using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScaleApp
{
    class DbConnection
    {
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


                }

            }
            else
            {
                return "Please configure Database or Contact System Administrator.";
            }
            return constr;

        }
    }
}
