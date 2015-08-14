using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WebApplication4
{
    public class ServiceAPI : IServiceAPI
    {
        SqlConnection dbConnection;

        public ServiceAPI()
        {
            dbConnection = DBConnect.getConnection();
        }

        public bool CreateIndividualProfile(String userName, String passWord, String firstName, String middleName, String lastName, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, String profilePicURL)
        {
            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Insert into individualUser (userName,pasword,firstName,middleName,lastName,addres,contactNo,mobileNo,emailId,webSite,profilePictureURL) values(@UserName,@Password,@FirstName,@MiddleName,@LastName,@Address,@ContactNo,@MobileNo,@EmailID,@Website,@ProfilePictureURL)";
           
                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", passWord);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@MiddleName", middleName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@EmailID", emailId);
                command.Parameters.AddWithValue("@Website", webSite);
                command.Parameters.AddWithValue("@ProfilePictureURL", profilePicURL);
                command.ExecuteNonQuery();             
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally {
                dbConnection.Close();
            }
        }

        public bool CreateOrganizationProfile(String userName, String passWord, String organizationName, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, Double latitude, Double longitude, String organizationPicture)
        {
            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Insert into organizationUser (userName,pasword,organizationName,addres,contactNo,mobileNo,emailId,webSite,latitude,longitude,organizationPictureURL) values(@UserName,@Password,@OrganizationName,@Address,@ContactNo,@MobileNo,@EmailID,@Website,@Latitude,@Longitude,@OrganizationPictureURL)";
                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", passWord);
                command.Parameters.AddWithValue("@OrganizationName", organizationName);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@EmailID", emailId);
                command.Parameters.AddWithValue("@Website", webSite);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@OrganizationPictureURL", organizationPicture);
                command.ExecuteNonQuery();

                return true;
            }
            catch(Exception e){
                return false;
            }
            finally{
                dbConnection.Close();
            }
        }

        public bool CreateShopProfile(String userName, String passWord, String shopName, String shopOwner,String panNo, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, Double latitude, Double longitude, String shopPictureURL)
        {
            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Insert into shopUser (userName,pasword,shopName,panNo,addres,contactNo,mobileNo,emailId,webSite,latitude,longitude,shopPictureURL,shopOwner) values(@UserName,@Password,@ShopName,@PANNo,@Address,@ContactNo,@MobileNo,@EmailID,@Website,@Latitude,@Longitude,@ShopPictureURL,@ShopOwner)";
                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", passWord);
                command.Parameters.AddWithValue("@ShopName", shopName);
                command.Parameters.AddWithValue("@ShopOwner", shopOwner);
                command.Parameters.AddWithValue("@PANNo", panNo);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@EmailID", emailId);
                command.Parameters.AddWithValue("@Website", webSite);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@ShopPictureURL", shopPictureURL);
                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public String UserAuthentication(String userName, String passWord)
        {
            String authenticate = "False";

            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "SELECT pasword,userCategory FROM allUsersTable WHERE userName= @UserName";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if(PasswordEncrytionService.ValidatePassword(passWord, reader["pasword"].ToString())){
                            authenticate = reader["userCategory"].ToString();
                        }
                    }
                }

                reader.Close();

                return authenticate;

            }
            catch (Exception e)
            {
                return "Error";
            }
            finally
            {
                dbConnection.Close();
            }           

        }

        public DataTable GetUserDetail(String userName,String userCategory) {

            DataTable userDetail = null;
            String query = "";
            SqlCommand command=null;
            SqlDataReader reader=null;

            try
            {

                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                switch (userCategory)
                {
                    case "Individual":
                        {
                            userDetail = new DataTable();
                            userDetail.Columns.Add(new DataColumn("firstName", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("middleName", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("lastName", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("addres", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("contactNo", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("mobileNo", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("emailId", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("webSite", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("profilePictureURL", typeof(String)));

                            query = "SELECT firstName,middleName,lastName,addres,contactNo,mobileNo,emailId,webSite,profilePictureURL FROM individualUser where userName = @UserName";
                            command = new SqlCommand(query, dbConnection);
                            command.Parameters.AddWithValue("@UserName", userName);
                            reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    userDetail.Rows.Add(reader["firstName"], reader["middleName"], reader["lastName"], reader["addres"], reader["contactNo"], reader["mobileNo"], reader["emailId"], reader["webSite"], reader["profilePictureURL"]);
                                }
                            }
                            
                            break;
                        }
                    case "Shop":
                        {
                            userDetail = new DataTable();
                            userDetail.Columns.Add(new DataColumn("shopName", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("shopOwner", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("panNo", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("addres", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("contactNo", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("mobileNo", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("emailId", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("webSite", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("latitude", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("longitude", typeof(Double)));
                            userDetail.Columns.Add(new DataColumn("longitude", typeof(Double)));
                            userDetail.Columns.Add(new DataColumn("shopPictureURL", typeof(String)));

                            query = "SELECT shopName,shopOwner,panNo,addres,contactNo,mobileNo,emailId,webSite,latitude,longitude,shopPictureURL FROM shopUser where userName = @UserName";
                            command = new SqlCommand(query, dbConnection);
                            command.Parameters.AddWithValue("@UserName", userName);
                            reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    userDetail.Rows.Add(reader["shopName"], reader["shopOwner"], reader["panNo"], reader["addres"], reader["contactNo"], reader["mobileNo"], reader["emailId"], reader["webSite"], reader["latitude"], reader["longitude"], reader["shopPictureURL"]);
                                }
                            }
                           
                            break;
                        }

                    case "Organization":
                        {
                            userDetail = new DataTable();
                            userDetail.Columns.Add(new DataColumn("organizationName", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("registrationNo", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("addres", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("contactNo", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("mobileNo", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("emailId", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("webSite", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("latitude", typeof(String)));
                            userDetail.Columns.Add(new DataColumn("longitude", typeof(Double)));
                            userDetail.Columns.Add(new DataColumn("shopPictureURL", typeof(String)));

                            query = "SELECT organizationName,registrationNo,addres,contactNo,mobileNo,emailId,webSite,latitude,longitude,organizationPictureURL FROM organizationUser where userName = @UserName";
                            command = new SqlCommand(query, dbConnection);
                            command.Parameters.AddWithValue("@UserName", userName);
                            reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    userDetail.Rows.Add(reader["organizationName"], reader["registrationNo"], reader["addres"], reader["contactNo"], reader["mobileNo"], reader["emailId"], reader["webSite"], reader["latitude"], reader["longitude"], reader["shopPictureURL"]);
                                }
                            }
                            
                            break;
                        }
                }

                return userDetail;

            }
            catch (Exception e)
            {
                return null;
            }
            finally {
                reader.Close();
                dbConnection.Close();
            }
            
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
            command.Parameters.AddWithValue("@adid", adid);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public DataTable GetContactsList()
        {
            DataTable ContactsList = new DataTable();
            ContactsList.Columns.Add(new DataColumn("contactID", typeof(int)));
             ContactsList.Columns.Add(new DataColumn("contact_photo", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("username", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("title", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("addres", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("contact", typeof(String)));

            
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query="Select contactID,contact_photo,username,title,addres,contact from contacts order by ad_insertdate desc";
             SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ContactsList.Rows.Add(reader["contactID"],reader["contact_photo"],reader["username"],reader["title"],reader["addres"],reader["contact"]);
                }
        }
         reader.Close();
            dbConnection.Close();
            return ContactsList;
    }

        public DataTable GetContactsDetail(int contactID)
        {
             DataTable ContactsList = new DataTable();
            ContactsList.Columns.Add(new DataColumn("contactID", typeof(int)));
             ContactsList.Columns.Add(new DataColumn("contact_photo", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("username", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("title", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("addres", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("contact", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("ad_description", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("contactsCategory", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("email", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("latitude", typeof(Double)));
            ContactsList.Columns.Add(new DataColumn("longitude", typeof(Double)));

            
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

              String query="Select contactID,contact_photo,username,title,addres,contact,ad_description,contactsCategory,email,latitude,longitude from contacts";
             SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ContactsList.Rows.Add(reader["contactID"],reader["contact_photo"],reader["username"],reader["title"],reader["addres"],reader["contact"],reader["ad_description"],reader["contactsCategory"],reader["email"],reader["latitude"],reader["longitude"]);
                }
        }
         reader.Close();
            dbConnection.Close();
            return ContactsList;

        }
    }
}
