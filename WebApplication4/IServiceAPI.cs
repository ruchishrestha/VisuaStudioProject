using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebApplication4
{
    public interface IServiceAPI
    {
        bool CreateIndividualProfile(String userName, String passWord, String firstName, String middleName, String lastName, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, String profilePic);
        
        bool CreateOrganizationProfile(String userName, String passWord, String organizationName, String registrationNo, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, Double latitude, Double longitude, String organizationPicture);

        bool CreateShopProfile(String userName, String passWord, String shopName, String shopOwner, String panNo, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, Double latitude, Double longitude, String shopPictureURL);

        String UserAuthentication(String userName, String passWord);
        
        DataTable GetUserDetail(String userName, String userCategory);

        void PushAdstoSales(String adid, String username, String title, String ad_desc, String brand, Double price, String ad_stat, String condition, String timeused, Int64 contact);

        DataTable GetSalesDetail();

        void DeleteSalesAd(String adid);

        DataTable GetContactsList();

      
    }
}
