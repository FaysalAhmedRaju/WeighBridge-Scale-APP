using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaleApp
{
    class CargoFunctions
    {
        DbConnection dbconn = new DbConnection();
        public double getLastTareWeight(string truckType, string truckNo, int portId)
        {
            string constr = dbconn.constring();
            //  if (constr.Equals("")) return;

            double tareWeight = 0.00;


            string query = @"SELECT tr.tr_weight
                                FROM truck_entry_regs AS tr
                                WHERE tr.truck_type='" + truckType +
                               "' AND tr.truck_no ='" + truckNo +
                               "' AND tr.port_id = " + portId + " AND tr.tr_weight IS NOT NULL ORDER BY tr.id DESC LIMIT 1";
            MySqlConnection con = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader mred = cmd.ExecuteReader();



            while (mred.Read())
            {

                tareWeight = mred.GetValue(0).ToString() != "" ? mred.GetDouble("tr_weight") : 0.00;
            }
            return tareWeight;

        }
    }
}
