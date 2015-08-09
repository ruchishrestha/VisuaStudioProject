using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WebApplication4
{
    public class ServiceAPI:IServiceAPI
    {
        SqlConnection dbConnection;

        public ServiceAPI()
        {
            dbConnection = DBConnect.getConnection();
        }

        public void CIndividualProfile(String username, String pasword, String fname, String mname, String lname, String address1, Int64 contact, Int64 mobile, String email, String website, byte[] profilePic)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            string query = "Insert into individual (username,pasword,fname,mname,lname,address1,contact,mobile,email,website) values(@UserName,@Pasword,@FName,@MName,@LName,@Address,@Contact,@Mobile,@Email,@Website,@photo)";
           
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@Pasword", pasword);
            command.Parameters.AddWithValue("@FName", fname);
            command.Parameters.AddWithValue("@MName", mname);
            command.Parameters.AddWithValue("@LName", lname);
            command.Parameters.AddWithValue("@Address", address1);
            command.Parameters.AddWithValue("@Contact", contact);
            command.Parameters.AddWithValue("@Mobile", mobile);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Website", website);
            command.Parameters.AddWithValue("@Photo", profilePic);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public bool UserAuthentication(string userName, string pasword)
        {
            bool auth = false;

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            string query = "SELECT pasword FROM allusers WHERE username= @username";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@username",userName);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    auth = PasswordEncrytionService.ValidatePassword(pasword,reader["pasword"].ToString());
                }
            }

            reader.Close();
            dbConnection.Close();

            return auth;

        }

        public void CCompanyProfile(String username, String pasword, String cname, String address1, Int64 contact, Int64 mobile, String email, String website, float latitude, float longitude, byte[] photo)
        {

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Insert into company (username,pasword,cname,address1,contact,mobile,email,website,latitude,longitude,photo) values(@UserName,@Pasword,@CName,@Address,@Contact,@Mobile,@Email,@Website,@Latitude,@Longitude,@Photo)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@Pasword",pasword);
            command.Parameters.AddWithValue("@CName", cname);
            command.Parameters.AddWithValue("@Address", address1);
            command.Parameters.AddWithValue("@Contact", contact);
            command.Parameters.AddWithValue("@Mobile", mobile);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Website", website);
            command.Parameters.AddWithValue("@Latitude", latitude);
            command.Parameters.AddWithValue("@Longitude", longitude);
            command.Parameters.AddWithValue("@Photo", photo);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public void CShopProfile(String username, String pasword, String shpname, String panno, String address1, Int64 contact, Int64 mobile, String email, String website, float latitude, float longitude, byte[] photo)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Insert into shop_table (username,pasword,shpname,panno,address1,contact,mobile,email,website,latitude,longitude,photo) values(@UserName,@Pasword,@ShpName,@Panno,@Address,@Contact,@Mobile,@Email,@Website,@Latitude,@Longitude,@Photo)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@Pasword", pasword);
            command.Parameters.AddWithValue("@ShpName", shpname);
            command.Parameters.AddWithValue("@Panno", panno);
            command.Parameters.AddWithValue("@Address", address1);
            command.Parameters.AddWithValue("@Contact", contact);
            command.Parameters.AddWithValue("@Mobile", mobile);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Website", website);
            command.Parameters.AddWithValue("@Latitude", latitude);
            command.Parameters.AddWithValue("@Longitude", longitude);
            command.Parameters.AddWithValue("@Photo", photo);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public void PushAdstoSales(String adid, String username, String title, String ad_desc, String brand, Double price, String ad_stat, String condition, String timeused, Int64 contact)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            string query = "Insert into sales (adid,username,title,ad_desc,brand,price,ad_stat,condition,timeused,contact) values(@adid,@username,@title,@ad_desc,@brand,@price,@ad_stat,@condition,@timeused,@contact)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", adid);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@ad_desc", ad_desc);
            command.Parameters.AddWithValue("@brand", brand);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@ad_stat", ad_stat);
            command.Parameters.AddWithValue("@condition", condition);
            command.Parameters.AddWithValue("@timeused", timeused);
            command.Parameters.AddWithValue("@contact", contact);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public DataTable GetSalesDetail()
        {
            DataTable SalesDetailTable = new DataTable();
            SalesDetailTable.Columns.Add(new DataColumn("adid", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("username", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("title", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("ad_desc", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("brand", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("price", typeof(Double)));
            SalesDetailTable.Columns.Add(new DataColumn("ad_stat", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("condition", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("timeused", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("contact", typeof(Int64)));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select adid,username,title,ad_desc,brand,price,ad_stat,condition,timeused,contact from sales where category='mobile'";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SalesDetailTable.Rows.Add(reader["adid"], reader["username"], reader["title"], reader["ad_desc"], reader["brand"], reader["price"], reader["ad_stat"], reader["condition"], reader["timeused"], reader["contact"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return SalesDetailTable;
        }

        public void DeleteSalesAd(String adid)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Delete from sales where adid=@adid";
             SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid",adid);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

    }
}
