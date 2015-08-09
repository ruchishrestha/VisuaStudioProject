using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebApplication4
{
    public interface IServiceAPI
    {
        void CIndividualProfile(string username, string pasword, string fname, string mname, string lname, string address1, Int64 contact, Int64 mobile, string email, string website, byte[] profilePic);
        bool UserAuthentication(string userName, string pasword);

        void CCompanyProfile(String username, String pasword, String cname, String address1, Int64 contact, Int64 mobile, String email, String website, float latitude, float longitude, byte[] photo);

        void CShopProfile(String username, String pasword, String shpname, String panno, String address1, Int64 contact, Int64 mobile, String email, String website, float latitude, float longitude, byte[] photo);

        void PushAdstoSales(String adid, String username, String title, String ad_desc, String brand,Double price, String ad_stat, String condition, String timeused, Int64 contact);

        DataTable GetSalesDetail();

        void DeleteSalesAd(String adid);
    }
}
