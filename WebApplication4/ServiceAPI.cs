using System;
using System.Collections;
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

        // Saten

        public bool CheckUserName(String userName)
        {

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String userExist = "";
            bool userCheck = false;

            String query = "Select count(userName) as userExist from allUsersTable where userName = @UserName";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userExist = reader["userExist"].ToString();
                }
            }
            if (userExist.Equals("0"))
            {
                userCheck = true;
            }

            reader.Close();
            dbConnection.Close();
            return userCheck;

        }

        public bool CheckEmailID(String emailId, String userCategory)
        {

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String emailExist = "";
            bool emailCheck = false;
            String query = "";

            switch (userCategory)
            {

                case "individual":
                    {
                        query = "Select count(emailId) as emailExist from individualUser where emailId = @EmailID";
                        break;
                    }
                case "shop":
                    {
                        query = "Select count(emailId) as emailExist from shopUser where emailId = @EmailID";
                        break;
                    }
                case "organization":
                    {
                        query = "Select count(emailId) as emailExist from organizationUser where emailId = @EmailID";
                        break;
                    }
            }

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@EmailID", emailId);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    emailExist = reader["emailExist"].ToString();
                }
            }
            if (emailExist.Equals("0"))
            {
                emailCheck = true;
            }

            reader.Close();
            dbConnection.Close();
            return emailCheck;

        }

        public String CreateIndividualProfile(String userName, String passWord, String firstName, String middleName, String lastName, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, String profilePictureURL)
        {
            bool userChecker = CheckUserName(userName);
            bool emailChecker = CheckEmailID(emailId, "individual");
            String result = "";


            if (userChecker && emailChecker)
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
                    command.Parameters.AddWithValue("@ProfilePictureURL", profilePictureURL);
                    command.ExecuteNonQuery();

                    dbConnection.Close();

                    AddIntoAllUsers(userName, passWord, "individual");

                    result = "Success";
                }
                catch (Exception e)
                {
                    result = "" + e;
                }

            }
            else
            {

                if (!userChecker && emailChecker)
                    result = "User Failure";
                else if (!emailChecker && userChecker)
                    result = "Email Failure";
                else
                    result = "Both Failure";
            }

            return result;

        }

        public String CreateShopProfile(String userName, String passWord, String shopName, String shopOwner, String panNo, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, Double latitude, Double longitude, String shopPictureURL)
        {
            bool userChecker = CheckUserName(userName);
            bool emailChecker = CheckEmailID(emailId, "individual");
            String result = "";

            if (userChecker && emailChecker)
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

                    dbConnection.Close();

                    AddIntoAllUsers(userName, passWord, "shop");

                    result = "Success";
                }
                catch (Exception e)
                {
                    result = "" + e;
                }

            }
            else
            {

                if (!userChecker && emailChecker)
                    result = "User Failure";
                else if (!emailChecker && userChecker)
                    result = "Email Failure";
                else
                    result = "Both Failure";
            }

            return result;

        }

        public String CreateOrganizationProfile(String userName, String passWord, String organizationName, String registrationNo, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, Double latitude, Double longitude, String organizationPicture)
        {
            bool userChecker = CheckUserName(userName);
            bool emailChecker = CheckEmailID(emailId, "individual");
            String result = "";

            if (userChecker && emailChecker)
            {
                try
                {
                    if (dbConnection.State.ToString() == "Closed")
                    {
                        dbConnection.Open();
                    }

                    String query = "Insert into organizationUser (userName,pasword,organizationName,registrationNo,addres,contactNo,mobileNo,emailId,webSite,latitude,longitude,organizationPictureURL) values(@UserName,@Password,@OrganizationName,@RegistrationNo,@Address,@ContactNo,@MobileNo,@EmailID,@Website,@Latitude,@Longitude,@OrganizationPictureURL)";

                    SqlCommand command = new SqlCommand(query, dbConnection);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", passWord);
                    command.Parameters.AddWithValue("@OrganizationName", organizationName);
                    command.Parameters.AddWithValue("@RegistrationNo", registrationNo);
                    command.Parameters.AddWithValue("@Address", aDdress);
                    command.Parameters.AddWithValue("@ContactNo", contactNo);
                    command.Parameters.AddWithValue("@MobileNo", mobileNo);
                    command.Parameters.AddWithValue("@EmailID", emailId);
                    command.Parameters.AddWithValue("@Website", webSite);
                    command.Parameters.AddWithValue("@Latitude", latitude);
                    command.Parameters.AddWithValue("@Longitude", longitude);
                    command.Parameters.AddWithValue("@OrganizationPictureURL", organizationPicture);
                    command.ExecuteNonQuery();

                    dbConnection.Close();

                    AddIntoAllUsers(userName, passWord, "organization");

                    result = "Success";
                }
                catch (Exception e)
                {
                    result = "" + e;
                }

            }
            else
            {

                if (!userChecker && emailChecker)
                    result = "User Failure";
                else if (!emailChecker && userChecker)
                    result = "Email Failure";
                else
                    result = "Both Failure";
            }

            return result;

        }

        public void AddIntoAllUsers(String userName, String passWord, String userCategory)
        {

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Insert into allUsersTable (userName,pasword,userCategory) values (@UserName,@Password,@UserCategory)";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", passWord);
            command.Parameters.AddWithValue("@UserCategory", userCategory);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public DataTable UserAuthentication(String userName, String passWord)
        {
            DataTable authentication = new DataTable();
            String[] authenticate = { "False", "", "" };

            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                authentication.Columns.Add(new DataColumn("userCategory", typeof(String)));
                authentication.Columns.Add(new DataColumn("fullUserName", typeof(String)));
                authentication.Columns.Add(new DataColumn("pictureURL", typeof(String)));

                String query = "SELECT pasword,userCategory FROM allUsersTable WHERE userName = @UserName";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (PasswordEncrytionService.ValidatePassword(passWord, reader["pasword"].ToString()))
                        {
                            authenticate[0] = reader["userCategory"].ToString();
                        }
                    }
                }

                reader.Close();

                dbConnection.Close();

                if (authenticate[0].Equals("individual") || authenticate[0].Equals("shop") || authenticate[0].Equals("organization"))
                {
                    authenticate[1] = getName(userName, authenticate[0])[0];
                    authenticate[2] = getName(userName, authenticate[0])[1];
                }

                authentication.Rows.Add(authenticate[0], authenticate[1], authenticate[2]);

                return authentication;

            }
            catch (Exception e)
            {
                authenticate[0] = "Error";
                authenticate[1] = "";
                authenticate[2] = "";

                authentication.Rows.Add(authenticate[0], authenticate[1], authenticate[2]);

                return authentication;
            }

        }

        public String[] getName(String userName, String userCategory)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String[] fullUserName = { "", "" };
            String query = "";
            String[] userType = { "", "", "", "" };

            switch (userCategory)
            {
                case "individual":
                    {
                        query = "Select firstName,middleName,lastName,profilePictureURL from individualUser where userName = @UserName";
                        userType[0] = "firstName";
                        userType[1] = "middleName";
                        userType[2] = "lastName";
                        userType[3] = "profilePictureURL";
                        break;
                    }
                case "shop":
                    {
                        query = "Select shopName,shopPictureURL from shopUser where userName = @userName";
                        userType[0] = "shopName";
                        userType[1] = "shopPictureURL";
                        break;
                    }
                case "organization":
                    {
                        query = "Select organizationName,organizationPictureURL from organizationUser where userName = @userName";
                        userType[0] = "organizationName";
                        userType[1] = "organizationPictureURL";
                        break;
                    }

            }

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (userType[2].Equals("") && userType[3].Equals(""))
                    {
                        fullUserName[0] = reader[userType[0]].ToString();
                        fullUserName[1] = reader[userType[1]].ToString();
                    }
                    else
                    {
                        if (reader[userType[1]].ToString().Equals("-")) { fullUserName[0] = reader[userType[0]].ToString() + " " + reader[userType[2]].ToString(); }
                        else { fullUserName[0] = reader[userType[0]].ToString() + " " + reader[userType[1]].ToString() + " " + reader[userType[2]].ToString(); }
                        fullUserName[1] = reader[userType[3]].ToString();
                    }
                }
            }

            reader.Close();
            dbConnection.Close();

            return fullUserName;

        }

        public DataTable GetIndividualDetail(String userName)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            DataTable userDetail = new DataTable();
            userDetail.Columns.Add(new DataColumn("firstName", typeof(String)));
            userDetail.Columns.Add(new DataColumn("middleName", typeof(String)));
            userDetail.Columns.Add(new DataColumn("lastName", typeof(String)));
            userDetail.Columns.Add(new DataColumn("pasword", typeof(String)));
            userDetail.Columns.Add(new DataColumn("addres", typeof(String)));
            userDetail.Columns.Add(new DataColumn("contactNo", typeof(String)));
            userDetail.Columns.Add(new DataColumn("mobileNo", typeof(String)));
            userDetail.Columns.Add(new DataColumn("emailId", typeof(String)));
            userDetail.Columns.Add(new DataColumn("webSite", typeof(String)));
            userDetail.Columns.Add(new DataColumn("profilePictureURL", typeof(String)));

            String query = "SELECT firstName,middleName,lastName,pasword,addres,contactNo,mobileNo,emailId,webSite,profilePictureURL FROM individualUser where userName = @UserName";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetail.Rows.Add(reader["firstName"], reader["middleName"], reader["lastName"], reader["pasword"], reader["addres"], reader["contactNo"], reader["mobileNo"], reader["emailId"], reader["webSite"], reader["profilePictureURL"]);
                }
            }

            reader.Close();
            dbConnection.Close();

            return userDetail;
        }

        public DataTable GetShopDetail(String userName)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            DataTable userDetail = new DataTable();
            userDetail.Columns.Add(new DataColumn("shopName", typeof(String)));
            userDetail.Columns.Add(new DataColumn("shopOwner", typeof(String)));
            userDetail.Columns.Add(new DataColumn("pasword", typeof(String)));
            userDetail.Columns.Add(new DataColumn("panNo", typeof(String)));
            userDetail.Columns.Add(new DataColumn("addres", typeof(String)));
            userDetail.Columns.Add(new DataColumn("contactNo", typeof(String)));
            userDetail.Columns.Add(new DataColumn("mobileNo", typeof(String)));
            userDetail.Columns.Add(new DataColumn("emailId", typeof(String)));
            userDetail.Columns.Add(new DataColumn("webSite", typeof(String)));
            userDetail.Columns.Add(new DataColumn("latitude", typeof(Double)));
            userDetail.Columns.Add(new DataColumn("longitude", typeof(Double)));
            userDetail.Columns.Add(new DataColumn("shopPictureURL", typeof(String)));

            String query = "SELECT shopName,shopOwner,pasword,panNo,addres,contactNo,mobileNo,emailId,webSite,latitude,longitude,shopPictureURL FROM shopUser where userName = @UserName";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetail.Rows.Add(reader["shopName"], reader["shopOwner"], reader["pasword"], reader["panNo"], reader["addres"], reader["contactNo"], reader["mobileNo"], reader["emailId"], reader["webSite"], reader["latitude"], reader["longitude"], reader["shopPictureURL"]);
                }
            }
            reader.Close();
            dbConnection.Close();

            return userDetail;
        }

        public DataTable GetOrganizationDetail(String userName)
        {

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            DataTable userDetail = new DataTable();
            userDetail.Columns.Add(new DataColumn("organizationName", typeof(String)));
            userDetail.Columns.Add(new DataColumn("registrationNo", typeof(String)));
            userDetail.Columns.Add(new DataColumn("pasword", typeof(String)));
            userDetail.Columns.Add(new DataColumn("addres", typeof(String)));
            userDetail.Columns.Add(new DataColumn("contactNo", typeof(String)));
            userDetail.Columns.Add(new DataColumn("mobileNo", typeof(String)));
            userDetail.Columns.Add(new DataColumn("emailId", typeof(String)));
            userDetail.Columns.Add(new DataColumn("webSite", typeof(String)));
            userDetail.Columns.Add(new DataColumn("latitude", typeof(Double)));
            userDetail.Columns.Add(new DataColumn("longitude", typeof(Double)));
            userDetail.Columns.Add(new DataColumn("organizationPictureURL", typeof(String)));

            String query = "SELECT organizationName,registrationNo,pasword,addres,contactNo,mobileNo,emailId,webSite,latitude,longitude,organizationPictureURL FROM organizationUser where userName = @UserName";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetail.Rows.Add(reader["organizationName"], reader["registrationNo"], reader["pasword"], reader["addres"], reader["contactNo"], reader["mobileNo"], reader["emailId"], reader["webSite"], reader["latitude"], reader["longitude"], reader["organizationPictureURL"]);
                }
            }

            reader.Close();
            dbConnection.Close();

            return userDetail;
        }

        public String AddContactsAds(String userName, String title, String description, String category, String aDdress, String contactNo, String mobileNo, String emailId, Double latitude, Double longitude, String picURL)
        {
            String result = "";

            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Insert into contacts (username,title,ad_description,Category,addres,contact,mobile,email,latitude,longitude,photoURL) values (@UserName,@Title,@Description,@Category,@Address,@ContactNo,@MobileNo,@EmailID,@Latitude,@Longitude,@PictureURL)";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@EmailID", emailId);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@PictureURL", picURL);
                command.ExecuteNonQuery();

                dbConnection.Close();

                result = getContactsAdID(userName);
            }
            catch (Exception e)
            {
                result = "Failure";
            }

            return result;
        }

        public String UpdateContactsAd(String adId, String pictureURL)
        {
            String result = "";
            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Update contacts set photoURL = @PhotoURL where adid = @ADID";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@PhotoURL", pictureURL);
                command.Parameters.AddWithValue("@ADID", adId);
                command.ExecuteNonQuery();

                dbConnection.Close();

                result = "Success";
            }
            catch (Exception e)
            {
                result = "False";
            }

            return result;
        }

        public String getContactsAdID(String userName)
        {
            String adID = "";

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select top 1 adid from contacts where username = @UserName order by ad_insertdate desc";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    adID = reader["adid"].ToString();
                }
            }

            reader.Close();
            dbConnection.Close();

            return adID;
        }

        public DataTable GetContactsCategory()
        {
            DataTable contactsCategory = new DataTable();
            contactsCategory.Columns.Add(new DataColumn("Category", typeof(String)));
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select category from contacts_category";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    contactsCategory.Rows.Add(reader["category"]);
                }
            }
            reader.Close();
            dbConnection.Close();

            return contactsCategory;
        }

        public String AddWantedAds(String userName, String title, String description, String category, String aDdress, String contactNo, String mobileNo, String emailId, Double latitude, Double longitude, String picURL)
        {
            String result = "";

            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Insert into wanted (username,title,ad_description,Category,addres,contact,mobile,email,latitude,longitude,photoURL) values (@UserName,@Title,@Description,@Category,@Address,@ContactNo,@MobileNo,@EmailID,@Latitude,@Longitude,@PictureURL)";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@EmailID", emailId);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@PictureURL", picURL);
                command.ExecuteNonQuery();

                dbConnection.Close();

                result = getWantedAdID(userName);
            }
            catch (Exception e)
            {
                result = "Failure";
            }

            return result;
        }

        public String UpdateWantedAd(String adId, String pictureURL)
        {
            String result = "";
            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Update wanted set photoURL = @PhotoURL where adid = @ADID";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@PhotoURL", pictureURL);
                command.Parameters.AddWithValue("@ADID", adId);
                command.ExecuteNonQuery();

                dbConnection.Close();

                result = "Success";
            }
            catch (Exception e)
            {
                result = "False: "+e;
            }

            return result;
        }

        public String getWantedAdID(String userName)
        {
            String adID = "";

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select top 1 adid from wanted where username = @UserName order by ad_insertdate desc";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    adID = reader["adid"].ToString();
                }
            }

            reader.Close();
            dbConnection.Close();

            return adID;
        }

        public DataTable GetWantedCategory()
        {
            DataTable wantedCategory = new DataTable();
            wantedCategory.Columns.Add(new DataColumn("Category", typeof(String)));
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select category from wanted_category";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    wantedCategory.Rows.Add(reader["category"]);
                }
            }
            reader.Close();
            dbConnection.Close();

            return wantedCategory;
        }

        public String AddSalesAds(String userName, String title, String description, String brand, String model, String price, String salesStatus, String condition, String timeUsed, String contactNo, String avgRating, String salesCategory)
        {
            String result = "";

            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Insert into sales (username,title,ad_description,brand,model,price,salesStatus,condition,timeused,contact,averageRating,salesCategory) values (@UserName,@Title,@Description,@Brand,@Model,@Price,@SalesStatus,@Condition,@TimeUsed,@ContactNo,@Rating,@SalesCategory)";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Brand", brand);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@SalesStatus", salesStatus);
                command.Parameters.AddWithValue("@Condition", condition);
                command.Parameters.AddWithValue("@TimeUsed", timeUsed);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@Rating", (float)float.Parse(avgRating));
                command.Parameters.AddWithValue("@SalesCategory", salesCategory);
                command.ExecuteNonQuery();

                dbConnection.Close();

                result = getSalesAdID(userName, salesCategory);
            }
            catch (Exception e)
            {
                result = "Failure "+e;
            }

            return result;
        }

        public String AddtoSalesGallery(String salesId, String salesCategory, String[] pictureURL)
        {

            String result = "";

            try
            {
                for (int i = 0; i < pictureURL.Length; i++)
                {
                    AddImagetoSalesGallery(salesId, salesCategory, pictureURL[i]);
                }

                result = "Success";
            }
            catch (Exception e)
            {
                result = "Failure: " + e;
            }

            return result;
        }

        public void AddImagetoSalesGallery(String salesId, String salesCategory, String pictureURL)
        {

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Insert into salesGallery (salesID,salesCategory,sales_pictureURL) values (@SalesID,@SalesCategory,@PictureURL)";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@PictureURL", pictureURL);
            command.Parameters.AddWithValue("@SalesID", salesId);
            command.Parameters.AddWithValue("@SalesCategory", salesCategory);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public String getSalesAdID(String userName, String salesCategory)
        {
            String adID = "";

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select top 1 salesID from sales where username = @UserName AND salesCategory = @SalesCategory order by ad_insertdate desc";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@SalesCategory", salesCategory);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    adID = reader["salesID"].ToString();
                }
            }

            reader.Close();
            dbConnection.Close();

            return adID;
        }

        public String AddRealEstateAds(String userName, String title, String description, String houseNo, String propertyType, String saleType, String price, String aDdress, String contactNo, String mobileNo, Double latitude, Double longitude)
        {

            String result = "";

            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Insert into realestate (username,title,ad_description,houseNo,propertyType,saleType,price,addres,contact,mobile,latitude,longitude) values (@UserName,@Title,@Description,@HouseNo,@PropertyType,@SaleType,@Price,@Address,@ContactNo,@MobileNo,@Latitude,@Longitude)";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@HouseNo", houseNo);
                command.Parameters.AddWithValue("@PropertyType", propertyType);
                command.Parameters.AddWithValue("@SaleType", saleType);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.ExecuteNonQuery();

                dbConnection.Close();

                result = getRealEstateAdID(userName);
            }
            catch (Exception e)
            {
                result = "Failure";
            }

            return result;
        }

        public String AddtoRealEstateGallery(String realId, String[] pictureURL)
        {

            String result = "";

            try
            {
                for (int i = 0; i < pictureURL.Length; i++)
                {
                    AddImagetoRealGallery(realId, pictureURL[i]);
                }

                result = "Success";
            }
            catch (Exception e)
            {
                result = "Failure";
            }

            return result;
        }

        public void AddImagetoRealGallery(String realId, String pictureURL)
        {

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Insert into realestateGallery (realestateID,realestate_pictureURL) values (@RealEstateID,@PictureURL)";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@PictureURL", pictureURL);
            command.Parameters.AddWithValue("@RealEstateID", realId);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public String getRealEstateAdID(String userName)
        {
            String adID = "";

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select top 1 realestateID from realestate where username = @UserName order by ad_insertdate desc";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    adID = reader["realestateID"].ToString();
                }
            }

            reader.Close();
            dbConnection.Close();

            return adID;
        }

        public DataTable GetPropertyType()
        {
            DataTable propertyType = new DataTable();
            propertyType.Columns.Add(new DataColumn("propertyType", typeof(String)));
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select propertyType from realestate_propertytype";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    propertyType.Rows.Add(reader["propertyType"]);
                }
            }
            reader.Close();
            dbConnection.Close();

            return propertyType;
        }

        public String AddJobAds(String userName, String jobTitle, String jobDescription, String responsibility, String skills, String jobCategory, String jobTiming, String vacancy, String salary, String aDdress, String contactNo, String emailId, String webSite, Double latitude, Double longitude, String organizationLogoURL)
        {
            String result = "";

            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Insert into jobs (username,title,ad_description,responsibility,skills,jobCategory,jobTime,vaccancyNo,salary,addres,contact,email,webSite,latitude,longitude,logoURL) values (@UserName,@JobTitle,@JobDescription,@Responsibilty,@Skills,@JobCategory,@JobTiming,@Vacancy,@Salary,@Address,@ContactNo,@EmailID,@Website,@Latitude,@Longitude,@LogoURL)";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@JobTitle", jobTitle);
                command.Parameters.AddWithValue("@JobDescription", jobDescription);
                command.Parameters.AddWithValue("@Responsibilty", responsibility);
                command.Parameters.AddWithValue("@Skills", skills);
                command.Parameters.AddWithValue("@JobCategory", jobCategory);
                command.Parameters.AddWithValue("@JobTiming", jobTiming);
                command.Parameters.AddWithValue("@Vacancy", vacancy);
                command.Parameters.AddWithValue("@Salary", salary);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@EmailID", emailId);
                command.Parameters.AddWithValue("@Website", webSite);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@LogoURL", organizationLogoURL);
                command.ExecuteNonQuery();

                dbConnection.Close();

                result = getJobsAdID(userName);

            }
            catch (Exception e)
            {
                result = "Failure";
            }

            return result;
        }

        public String UpdateJobAd(String jobId, String pictureURL)
        {
            String result = "";
            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Update jobs set logoURL = @LogoURL where jobID = @JOBID";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@LogoURL", pictureURL);
                command.Parameters.AddWithValue("@JOBID", jobId);
                command.ExecuteNonQuery();

                dbConnection.Close();

                result = "Success";
            }
            catch (Exception e)
            {
                result = "False";
            }

            return result;
        }

        public String getJobsAdID(String userName)
        {
            String adID = "";

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select top 1 jobID from jobs where username = @UserName order by ad_insertdate desc";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    adID = reader["jobID"].ToString();
                }
            }

            reader.Close();
            dbConnection.Close();

            return adID;
        }

        public DataTable GetJobCategory()
        {
            DataTable jobCategory = new DataTable();
            jobCategory.Columns.Add(new DataColumn("Category", typeof(String)));
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select job_category from job_category";
            SqlCommand command = new SqlCommand(query,dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) {
                while (reader.Read()) { 
                    jobCategory.Rows.Add(reader["job_category"]);
                }
            }
            reader.Close();
            dbConnection.Close();

            return jobCategory;
        }

        // Ruchi

        public DataTable GetSalesDetail(int salesID)
        {
            DataTable SalesDetailTable = new DataTable();
            SalesDetailTable.Columns.Add(new DataColumn("salesID", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("dateOnly", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("username", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("title", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("ad_description", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("brand", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("model", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("price", typeof(Double)));
            SalesDetailTable.Columns.Add(new DataColumn("salesStatus", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("condition", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("timeused", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("contact", typeof(String)));
            SalesDetailTable.Columns.Add(new DataColumn("averageRating", typeof(Double)));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select salesID,convert(nvarchar(10),ad_insertdate,101)as dateOnly,username,title,ad_description,brand,model,price,salesStatus,condition,timeused,contact,averageRating from sales where salesID=@adid";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", salesID);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SalesDetailTable.Rows.Add(reader["salesID"], reader["username"], reader["dateOnly"],reader["title"], reader["ad_description"], reader["brand"],reader["model"], reader["price"], reader["salesStatus"], reader["condition"], reader["timeused"], reader["contact"],reader["averageRating"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return SalesDetailTable;
        }

        public void DeleteSalesAd(int adid,String category)
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
            deletefromcomments(adid, category);
        }


            
        public DataTable GetContactsList(String category)
            {
            String query1=null;
             DataTable ContactsList = new DataTable();
            ContactsList.Columns.Add(new DataColumn("adid", typeof(int)));
            ContactsList.Columns.Add(new DataColumn("photoURL", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("username", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("title", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("addres", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("contact", typeof(String)));
             ContactsList.Columns.Add(new DataColumn("mobile", typeof(String)));
                            

            
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            switch (category)
            {
                case "contacts":
                   query1="Select adid,photoURL,username,title,addres,contact,mobile from contacts order by ad_insertdate desc";
                    break;
                case "wanted":
                    query1="Select adid,photoURL,username,title,addres,contact,mobile from wanted order by ad_insertdate desc";
                    break;
                default:
                    break;
            }

             
             SqlCommand command = new SqlCommand(query1, dbConnection);
            
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ContactsList.Rows.Add(reader["adid"], reader["photoURL"], reader["username"], reader["title"], reader["addres"], reader["contact"], reader["mobile"]);
                }
        }
         reader.Close();
            dbConnection.Close();
            return ContactsList;

        }


        public DataTable GetContactDetails(int adid,String category)
        {
            String query2 = null;
            DataTable ContactDetails = new DataTable();
            ContactDetails.Columns.Add(new DataColumn("adid", typeof(int)));
            ContactDetails.Columns.Add(new DataColumn("dateOnly", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("username", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("title", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("ad_description", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("Category", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("contact", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("mobile", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("addres", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("email", typeof(String)));
            ContactDetails.Columns.Add(new DataColumn("latitude", typeof(Double)));
            ContactDetails.Columns.Add(new DataColumn("longitude", typeof(Double)));
            ContactDetails.Columns.Add(new DataColumn("photoURL", typeof(String)));        

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            switch (category)
            {
                case "contacts":
                    query2 = "Select adid,convert(nvarchar(10),ad_insertdate,101) as dateOnly,username,title,ad_description,Category,contact,mobile,addres,email,latitude,longitude,photoURL from contacts where adid=@Adid";
                    break;
                case "wanted":
                     query2 = "Select adid,convert(nvarchar(10),ad_insertdate,101) as dateOnly,username,title,ad_description,Category,contact,mobile,addres,email,latitude,longitude,photoURL from wanted where adid=@Adid";
                    break;
                default:
                    break;
            }

            
            SqlCommand command = new SqlCommand(query2, dbConnection);
         
            command.Parameters.AddWithValue("@Adid", adid);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ContactDetails.Rows.Add(reader["adid"], reader["dateOnly"], reader["username"], reader["title"], reader["ad_description"], reader["Category"], reader["contact"], reader["mobile"], reader["addres"], reader["email"], reader["latitude"], reader["longitude"], reader["photoURL"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return ContactDetails;

        }

        public DataTable GetMyComment(int adid, String category, String username)
        {
            DataTable MyCommentTable = new DataTable();
            MyCommentTable.Columns.Add(new DataColumn("postedDate", typeof(String)));
            MyCommentTable.Columns.Add(new DataColumn("username", typeof(String)));
            MyCommentTable.Columns.Add(new DataColumn("commentText", typeof(String)));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select top 1 convert(nvarchar(10),commentDate,101) as postedDate,username,commentText from comments where adid=@Adid and category=@Category and username=@Username order by commentDate desc";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@Adid", adid);
            command.Parameters.AddWithValue("@Category", category);
            command.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MyCommentTable.Rows.Add(reader["postedDate"], reader["username"],reader["commentText"]);
                }
            }
                 
            reader.Close();
            dbConnection.Close();
            return MyCommentTable;
        }

        public DataTable GetAllComments(int adid, String category)
        {
            DataTable AllComments = new DataTable();
            AllComments.Columns.Add(new DataColumn("postedDate",typeof(String)));
            AllComments.Columns.Add(new DataColumn("username", typeof(String)));
            AllComments.Columns.Add(new DataColumn("commentText", typeof(String)));
            AllComments.Columns.Add(new DataColumn("adid", typeof(int)));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select convert(nvarchar(10),commentDate,101) as postedDate,username,commentText,adid from comments where adid=@Adid and category=@Category order by commentDate desc";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@Adid", adid);
            command.Parameters.AddWithValue("@Category", category);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AllComments.Rows.Add(reader["postedDate"], reader["username"],reader["commentText"],reader["adid"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return AllComments;
        }

        public void PushComments(String category, String username, int adid, String commentText)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Insert into comments (category,username,adid,commentText) values(@Category,@Username,@Adid,@CommentText)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@Category", category);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Adid", adid);
            command.Parameters.AddWithValue("@CommentText", commentText);

            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public bool PushtoWatchlist(int adid,String category,String username)
        {
            bool flag = false;
           
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select * from watchlist where adid=@Adid and category=@Category and username=@Username";
            SqlCommand command= new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@Category", category);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Adid", adid);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                dbConnection.Close();
                return false;
            }
            else
            {
                dbConnection.Close();
                flag = toinsert(adid, category, username);
                return flag;
               
            }
              
        }

        public bool toinsert(int adid, String category, String username)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query1 = "Insert into watchlist (adid,category,username) values (@Adid,@Category,@Username)";
            SqlCommand command = new SqlCommand(query1, dbConnection);
            command.Parameters.AddWithValue("@Category", category);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Adid", adid);
            command.ExecuteNonQuery();
            dbConnection.Close();
            return true ;
        }

        public DataTable GetRealestateList()
        {
            DataTable RealEstateList = new DataTable();
            RealEstateList.Columns.Add(new DataColumn("realestateID", typeof(int)));
            RealEstateList.Columns.Add(new DataColumn("title",typeof(String)));
            RealEstateList.Columns.Add(new DataColumn("price", typeof(Double)));
            RealEstateList.Columns.Add(new DataColumn("saleType", typeof(String)));
            RealEstateList.Columns.Add(new DataColumn("addres", typeof(String)));
            RealEstateList.Columns.Add(new DataColumn("contact", typeof(String)));
            RealEstateList.Columns.Add(new DataColumn("username", typeof(String)));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select realestateID,title,price,saleType,addres,contact,username from realestate order by ad_insertdate desc";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read()){
                    RealEstateList.Rows.Add(reader["realestateID"], reader["title"], reader["price"], reader["saleType"], reader["addres"], reader["contact"], reader["username"]);
                }
               
            }
            reader.Close();
            dbConnection.Close();
            return RealEstateList;
        }

        public DataTable GetRealestateDetails(int realestateID)
        {
            DataTable RealestateDetails = new DataTable();
            RealestateDetails.Columns.Add(new DataColumn("realestateID", typeof(int)));
            RealestateDetails.Columns.Add(new DataColumn("dateOnly",typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("username",typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("title",typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("ad_description", typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("houseNo",typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("propertyType",typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("saleType",typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("price",typeof(Double)));
            RealestateDetails.Columns.Add(new DataColumn("contact",typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("mobile",typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("addres", typeof(String)));
            RealestateDetails.Columns.Add(new DataColumn("latitude",typeof(Double)));
            RealestateDetails.Columns.Add(new DataColumn("longitude", typeof(Double)));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select realestateID,convert(nvarchar(10),ad_insertdate,101) as dateOnly,username,title,ad_description,houseNo,propertyType,saleType,price,contact,mobile,addres,latitude,longitude from realestate where realestateID=@adid";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@Adid", realestateID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RealestateDetails.Rows.Add(reader["realestateID"], reader["dateOnly"], reader["username"], reader["title"], reader["ad_description"], reader["houseNo"], reader["propertyType"], reader["saleType"], reader["price"], reader["contact"], reader["mobile"], reader["addres"], reader["latitude"], reader["longitude"]);

                }
            }
            reader.Close();
            dbConnection.Close();
            return RealestateDetails;
        }

        public DataTable GetJobsList()
        {
            DataTable JobsList = new DataTable();
            JobsList.Columns.Add(new DataColumn("jobID", typeof(int)));
            JobsList.Columns.Add(new DataColumn("title",typeof(String)));
            JobsList.Columns.Add(new DataColumn("jobCategory",typeof(String)));
            JobsList.Columns.Add(new DataColumn("vaccancyNo",typeof(String)));
            JobsList.Columns.Add(new DataColumn("salary",typeof(String)));
            JobsList.Columns.Add(new DataColumn("username",typeof(String)));
            JobsList.Columns.Add(new DataColumn("logoURL", typeof(String)));
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            String query = "Select jobID,title,jobCategory,vaccancyNo,salary,username,logoURL from jobs order by ad_insertdate desc";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    JobsList.Rows.Add(reader["jobID"], reader["title"], reader["jobCategory"], reader["vaccancyNo"], reader["salary"], reader["username"],reader["logoURL"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return JobsList;
        }

        public DataTable GetJobsDetail(int jobID)
        {
            DataTable JobsDetail = new DataTable();
            JobsDetail.Columns.Add(new DataColumn("dateOnly",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("username",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("title",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("ad_description",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("responsibility",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("skills",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("jobCategory",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("jobTime",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("vaccancyNo",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("salary",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("addres",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("contact",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("email",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("website",typeof(String)));
            JobsDetail.Columns.Add(new DataColumn("latitude",typeof(Double)));
            JobsDetail.Columns.Add(new DataColumn("longitude",typeof(Double)));
            JobsDetail.Columns.Add(new DataColumn("logoURL",typeof(String)));
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select convert(nvarchar(10),ad_insertdate,101) as  dateOnly,username,title,ad_description,responsibility,skills,jobCategory,jobTime,vaccancyNo,salary,addres,contact,email,website,latitude,longitude,logoURL from jobs where jobID=@Adid";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@Adid", jobID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    JobsDetail.Rows.Add(reader["dateOnly"], reader["username"], reader["title"], reader["ad_description"], reader["responsibility"], reader["skills"],
                        reader["jobCategory"], reader["jobTime"], reader["vaccancyNo"], reader["salary"], reader["addres"], reader["contact"], reader["email"], reader["website"],
                        reader["latitude"], reader["longitude"], reader["logoURL"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return JobsDetail;
        }

        public String GetRealestatePictureURL(int adid)
        {
            String realestatePicURL="";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query="Select top 1 realestate_pictureURL from realestateGallery where realestateID=@adid";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", adid);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    realestatePicURL = reader["realestate_pictureURL"].ToString();
                }
            }
            reader.Close();
            dbConnection.Close();
            return realestatePicURL;
          
        }

        public ArrayList GetAllImages(int adid)
        {
            ArrayList realestatePicURL=new ArrayList();
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select realestate_pictureURL from realestateGallery where realestateID=@adid";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", adid);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    realestatePicURL.Add(reader["realestate_pictureURL"]);
                   
                }
            }
            reader.Close();
            dbConnection.Close();
            return realestatePicURL;
        }

        public DataTable GetSalesList(String salesCategory,String orderads)
        {
            String query="";
            DataTable SalesList = new DataTable();
            SalesList.Columns.Add(new DataColumn("salesID",typeof(int)));
            SalesList.Columns.Add(new DataColumn("username",typeof(String)));
            SalesList.Columns.Add(new DataColumn("title",typeof(String)));
            SalesList.Columns.Add(new DataColumn("brand",typeof(String)));
            SalesList.Columns.Add(new DataColumn("model",typeof(String)));
            SalesList.Columns.Add(new DataColumn("price",typeof(String)));
            SalesList.Columns.Add(new DataColumn("salesStatus",typeof(String)));
            SalesList.Columns.Add(new DataColumn("condition", typeof(String)));
            SalesList.Columns.Add(new DataColumn("averageRating", typeof(Double)));
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            switch (orderads)
            {
                case "newads":
                    query = "Select salesID,username,title,brand,model,price,salesStatus,condition,averageRating from sales where salesCategory=@SalesCategory order by ad_insertdate desc";
                    break;
                case "topads":
                    query = "Select salesID,username,title,brand,model,price,salesStatus,condition,averageRating from sales where salesCategory=@SalesCategory order by averageRating desc";
                    break;
            }
           
             SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@SalesCategory", salesCategory);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SalesList.Rows.Add(reader["salesID"], reader["username"], reader["title"], reader["brand"], reader["model"], reader["price"], reader["salesStatus"],reader["condition"],reader["averageRating"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return SalesList;
        }

  

        public ArrayList GetSalesImages(int adid)
        {
            ArrayList salesPicURL = new ArrayList();
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select sales_pictureURL from salesGallery where salesID=@adid";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", adid);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    salesPicURL.Add(reader["sales_pictureURL"]);

                }
            }
            reader.Close();
            dbConnection.Close();
            return salesPicURL;
        }

        public String GetSalesPictureURL(int adid)
        {
            String salesPicURL = "";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select top 1 sales_pictureURL from salesGallery where salesID=@adid";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", adid);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    salesPicURL = reader["sales_pictureURL"].ToString();
                }
            }
            reader.Close();
            dbConnection.Close();
            return salesPicURL;

        }

        public void PushRateValue(int salesID, String userID, String salesCategory,Double myrating)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select * from allrating where salesID=@SalesID and username=@userID and category=@salesCategory";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@SalesID", salesID);
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@salesCategory", salesCategory);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                dbConnection.Close();
                updateintable(myrating,salesID,userID,salesCategory);
            }
            else
            {
                reader.Close();
                dbConnection.Close();
                insertintable(myrating, salesID, userID, salesCategory);
            }
        }

        public void updateintable(Double myrating,int salesID,String userID,String salesCategory)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Update allrating set rateValue=@Myrating where salesID=@adid and username=@UserId and category=@SalesCategory ";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", salesID);
            command.Parameters.AddWithValue("@UserId", userID);
            command.Parameters.AddWithValue("@SalesCategory", salesCategory);
            command.Parameters.AddWithValue("@Myrating", myrating);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public void insertintable(Double myrating, int salesID, String userID, String salesCategory)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Insert into allrating (category,username,salesID,ratevalue) values(@SalesCategory,@UserId,@adid,@Myrating)";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", salesID);
            command.Parameters.AddWithValue("@UserId", userID);
            command.Parameters.AddWithValue("@SalesCategory", salesCategory);
            command.Parameters.AddWithValue("@Myrating", myrating);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public Double GetMyRating(int salesID,String userID,String salesCategory){
            Double myrate=0.0;
             if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query="Select rateValue from allrating where salesID=@adid and username=@userID and category=@salesCategory";
             SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", salesID);
            command.Parameters.AddWithValue("@userId", userID);
            command.Parameters.AddWithValue("@salesCategory", salesCategory);
             SqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows){
                while(reader.Read()){
                    myrate=(Double)reader["rateValue"];
                }
            }
            return myrate;
        }

        public Double GetCommentRating(int adid,String username)
        {
            Double myrate=0.0;
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select rateValue from allrating where salesID=@Adid and username=@Username";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@Adid", adid);
            command.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    myrate = (Double)reader["rateValue"];
                }
            }
            return myrate;
        }

        public DataTable GetContactsForMap(String category)
        {
            String query1 = null;
            DataTable ContactsList = new DataTable();
            ContactsList.Columns.Add(new DataColumn("adid", typeof(int)));
            ContactsList.Columns.Add(new DataColumn("photoURL", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("Category", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("title", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("addres", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("contact", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("mobile", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("latitude", typeof(Double)));
            ContactsList.Columns.Add(new DataColumn("longitude", typeof(Double)));



            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            switch (category)
            {
                case "contacts":
                    query1 = "Select adid,photoURL,Category,title,addres,contact,mobile,latitude,longitude from contacts";
                    break;
                case "wanted":
                    query1 = "Select adid,photoURL,Category,title,addres,contact,mobile,latitude,longitude from wanted";
                    break;
                default:
                    break;
            }


            SqlCommand command = new SqlCommand(query1, dbConnection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ContactsList.Rows.Add(reader["adid"], reader["photoURL"], reader["Category"], reader["title"], reader["addres"], reader["contact"], reader["mobile"],reader["latitude"],reader["longitude"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return ContactsList;

        }

        public String GetUserCategory(String username)
        {
            String userCategory = "";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            String query = "Select userCategory from allUsersTable where userName=@Username";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userCategory = reader["userCategory"].ToString();
                }
            }
            reader.Close();
            dbConnection.Close();
            return userCategory;
        }

        public DataTable GetMyContactsList(String userID, String category)
        {
            String query1 = null;
            DataTable ContactsList = new DataTable();
            ContactsList.Columns.Add(new DataColumn("adid", typeof(int)));
            ContactsList.Columns.Add(new DataColumn("photoURL", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("username", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("title", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("addres", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("contact", typeof(String)));
            ContactsList.Columns.Add(new DataColumn("mobile", typeof(String)));



            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            switch (category)
            {
                case "contacts":
                    query1 = "Select adid,photoURL,username,title,addres,contact,mobile from contacts where username=@userID order by ad_insertdate desc";
                    break;
                case "wanted":
                    query1 = "Select adid,photoURL,username,title,addres,contact,mobile from wanted where username=@userID order by ad_insertdate desc";
                    break;
                default:
                    break;
            }


            SqlCommand command = new SqlCommand(query1, dbConnection);
            command.Parameters.AddWithValue("@userID", userID);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ContactsList.Rows.Add(reader["adid"], reader["photoURL"], reader["username"], reader["title"], reader["addres"], reader["contact"], reader["mobile"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return ContactsList;

        }

        public void DeleteContactsWantedAd(int adid, String category)
        {
            String query ="";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            switch (category)
            {
                case "contacts":
                   query = "Delete from contacts where adid=@adid";
                   break;
                case "wanted":
                     query = "Delete from wanted where adid=@adid";
                   break;
                default:
                   break;
            }
            
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", adid);
            command.ExecuteNonQuery();
            dbConnection.Close();
            deletefromcomments(adid, category);
        }

        public void deletefromcomments(int adid, String category)
        {
            String query = "";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            query = "Delete from comments where adid=@adid and category=@category";
            
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", adid);
            command.Parameters.AddWithValue("@category", category);
            command.ExecuteNonQuery();
            dbConnection.Close();
            deletefromwatchlist(adid,category);
        }

        public void deletefromwatchlist(int adid, String category)
        {
            String query = "";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            query = "Delete from watchlist where adid=@adid and category=@category";

            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@adid", adid);
            command.Parameters.AddWithValue("@category", category);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public void UpdateContactsAds(int adid,String title, String description, String category, String aDdress, String contactNo, String mobileNo, String emailId, Double latitude, Double longitude, String picURL)
        {
          
            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Update contacts set title=@Title,ad_description=@Description ,Category=@Category ,addres=@Address ,contact=@ContactNo ,mobile=@MobileNo,email=@EmailID ,latitude=@Latitude ,longitude=@Longitude ,photoURL=@PictureURL where adid=@Adid ";
               
                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@Adid", adid);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@EmailID", emailId);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@PictureURL", picURL);
                command.ExecuteNonQuery();

                dbConnection.Close();
                           
            }
            catch (Exception e)
            {
                
            }

          
        }

        public void UpdateWantedAds(int adid, String title, String description, String category, String aDdress, String contactNo, String mobileNo, String emailId, Double latitude, Double longitude, String picURL)
        {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }

                String query = "Update wanted set title=@Title,ad_description=@Description ,Category=@Category ,addres=@Address ,contact=@ContactNo ,mobile=@MobileNo,email=@EmailID ,latitude=@Latitude ,longitude=@Longitude ,photoURL=@PictureURL where adid=@Adid ";

                SqlCommand command = new SqlCommand(query, dbConnection);
                command.Parameters.AddWithValue("@Adid", adid);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@Address", aDdress);
                command.Parameters.AddWithValue("@ContactNo", contactNo);
                command.Parameters.AddWithValue("@MobileNo", mobileNo);
                command.Parameters.AddWithValue("@EmailID", emailId);
                command.Parameters.AddWithValue("@Latitude", latitude);
                command.Parameters.AddWithValue("@Longitude", longitude);
                command.Parameters.AddWithValue("@PictureURL", picURL);
                command.ExecuteNonQuery();

                dbConnection.Close();
          }

        public DataTable GetMySalesList(String userID)
        {
            String query = "";
            DataTable SalesList = new DataTable();
            SalesList.Columns.Add(new DataColumn("salesID", typeof(int)));
            SalesList.Columns.Add(new DataColumn("username", typeof(String)));
            SalesList.Columns.Add(new DataColumn("title", typeof(String)));
            SalesList.Columns.Add(new DataColumn("brand", typeof(String)));
            SalesList.Columns.Add(new DataColumn("model", typeof(String)));
            SalesList.Columns.Add(new DataColumn("price", typeof(String)));
            SalesList.Columns.Add(new DataColumn("salesStatus", typeof(String)));
            SalesList.Columns.Add(new DataColumn("condition", typeof(String)));
            SalesList.Columns.Add(new DataColumn("averageRating", typeof(Double)));
            SalesList.Columns.Add(new DataColumn("salesCategory", typeof(String)));
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
           
          
                    query = "Select salesID,username,title,brand,model,price,salesStatus,condition,averageRating,salesCategory from sales where username=@userID order by ad_insertdate desc";
                  
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@userID", userID);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SalesList.Rows.Add(reader["salesID"], reader["username"], reader["title"], reader["brand"], reader["model"], reader["price"], reader["salesStatus"], reader["condition"], reader["averageRating"],reader["salesCategory"]);
                }
            }
            reader.Close();
            dbConnection.Close();
            return SalesList;
        }
    }
}
