﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebApplication4
{
    public interface IServiceAPI
    {
        // Saten
        DataTable MyFullTextSearcher(String myQuery);

        String CreateIndividualProfile(String userName, String passWord, String firstName, String middleName, String lastName, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, String profilePictureURL);
        
        String CreateOrganizationProfile(String userName, String passWord, String organizationName, String registrationNo, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, Double latitude, Double longitude, String organizationPicture);

        String CreateShopProfile(String userName, String passWord, String shopName, String shopOwner, String panNo, String aDdress, String contactNo, String mobileNo, String emailId, String webSite, Double latitude, Double longitude, String shopPictureURL);

        DataTable UserAuthentication(String userName, String passWord);       

        DataTable GetIndividualDetail(String userName);

        DataTable GetShopDetail(String userName);

        DataTable GetOrganizationDetail(String userName);

        String AddContactsAds(String userName, String title, String description, String category, String aDdress, String contactNo, String mobileNo, String emailId, Double latitude, Double longitude, String picURL);

        String UpdateContactsAd(String adId, String pictureURL);

        DataTable GetContactsCategory();
        
        String AddWantedAds(String userName, String title, String description, String category, String aDdress, String contactNo, String mobileNo, String emailId, Double latitude, Double longitude, String picURL);

        String UpdateWantedAd(String adId, String pictureURL);

        DataTable GetWantedCategory(); 

        String AddSalesAds(String userName, String title, String description, String brand, String model, String price, String salesStatus, String condition, String timeUsed, String contactNo, String avgRating, String salesCategory);

        String AddtoSalesGallery(String salesId, String salesCategory , String[] pictureURL);
         
        String AddRealEstateAds(String userName, String title, String description, String houseNo, String propertyType, String saleType, String price, String aDdress, String contactNo, String mobileNo, Double latitude, Double longitude);

        String AddtoRealEstateGallery(String realId, String[] pictureURL);

        DataTable GetPropertyType();
        
        String AddJobAds(String userName, String jobTitle, String jobDescription, String responsibility, String skills, String jobCategory, String jobTiming, String vacancy, String salary, String aDdress, String contactNo, String emailId, String webSite, Double latitude, Double longitude, String organizationLogoURL);

        String UpdateJobAd(String jobId, String pictureURL);

        DataTable GetJobCategory();
    

        // Ruchi
        DataTable GetSalesDetail(int salesID);

        void DeleteSalesAd(int adid,String category);

        DataTable GetContactsList(String category);

        DataTable GetContactDetails(int adid,String category);

        DataTable GetMyComment(int adid, String username, String category);

        DataTable GetAllComments(int adid, String category);

        void PushComments(String category, String username, int adid, String commentText);

        bool PushtoWatchlist(int adid, String category, String username);

        DataTable GetRealestateList();

        DataTable GetRealestateDetails(int realestateID);

        DataTable GetJobsList();

        DataTable GetJobsDetail(int jobID);

        String GetRealestatePictureURL(int adid);

        ArrayList GetAllImages(int adid);

        DataTable GetSalesList(String salesCategory,String orderads);

        ArrayList GetSalesImages(int adid);

        String GetSalesPictureURL(int adid);

        void PushRateValue(int salesID, String userID, String salesCategory,Double myrating);

        Double GetMyRating(int salesID, String userID, String salesCategory);

        Double GetCommentRating(int adid,String username);

        DataTable GetContactsForMap(String category);

        String GetUserCategory(String username);

        DataTable GetMyContactsList(String userID, String category);

        void DeleteContactsWantedAd(int adid, String category);

        void UpdateContactsAds(int adid, String title, String description, String category, String aDdress, String contactNo, String mobileNo, String emailId, Double latitude, Double longitude, String picURL);

        void UpdateWantedAds(int adid, String title, String description, String category, String aDdress, String contactNo, String mobileNo, String emailId, Double latitude, Double longitude, String picURL);

        DataTable GetMySalesList(String userID);
    }
}
