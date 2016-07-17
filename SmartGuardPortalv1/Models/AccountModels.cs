using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Web;

namespace SmartGuardPortalv1.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserInformation> UserInfos { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<GeoLocation> GeoLocations { get; set; }
        public DbSet<Fall> Falls { get; set; }
        public DbSet<FallProfile> FallProfiles { get; set; }
        public DbSet<ChargeData> Charges { get; set; }
        public DbSet<Medical> MedicalRecords { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ContactSchedule> ContactSchedules { get; set; }
        public DbSet<VitalInfo> VitalInfos { get; set; }
        public DbSet<AppBuild> AppBuilds { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public DbSet<FAQ> FAQs { get; set; }

        public DbSet<LocateStatus> LocateDevicesStatus { get; set; }

        public System.Data.Entity.DbSet<SmartGuardPortalv1.Models.Inventory> Inventories { get; set; }


    }

    [Table("InventoryTable")]
    public class Inventory
    {
        
        [Key]
        public string serialKey{ get; set;}
        
        public DateTime? stamp { get; set; }
        public int? fkUserId { get; set; }
    }

    [Table("VideosTable")]
    public class Video
    {
        [Key]
        public int VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoUrl { get; set; }
        public string VideoCaption { get; set; }
        public string VideoDescription { get; set; }
        
    }

    [Table("UpdateTable")]
    public class Update
    {
        [Key]
        public string UpdateId { get; set; }
        public string changes { get; set; }
        public byte[] UpdateFile { get; set; }
        public DateTime timeStamp { get; set; }
    }

    [Table("AppBuildTable")]
    public class AppBuild
    {
        [Key]
        public int AppBuildId { get; set; }
        public string AppVersion { get; set; }
        public byte[] AppDocumentation { get; set; }
        public byte[] Apk { get; set; }
        public bool isActive { get; set; }
        public DateTime timeStamp { get; set; }
    }

    [Table("LocateStatusTable")]
    public class LocateStatus
    {
        [Key]
        public int LocateStatusId { get; set; }
        public int fkUserId { get; set; }
        public bool isMissing { get; set; }
    }

    [Table("MedicalTable")]
    public class Medical
    {
        [Key]
        public int MedicalId { get; set; }
        public int fkUserId { get; set; }
        public string description { get; set; }
        //0-me only, 1-primary 2-secondary
        public int accessLevel { get; set; }
        public DateTime TimeStamp { get; set; }
        public byte[] MedicalFile { get; set; }
        public string FileType { get; set; }
        //public HttpPostedFile File { get; set; }
    }

    [Table("ReminderTable")]
    public class Reminder
    {
        [Key]
        public int ReminderId { get; set; }
        public int fkUserId { get; set; }
        public string description { get; set; }
        public DateTime TimeStamp { get; set; }
        
    }

    [Table("ContactScheduleTable")]
    public class ContactSchedule
    {
        [Key]
        public int ContactScheduleId { get; set; }
        public int fkUserId { get; set; }
        //0-Sunday, 1-Monday
        public string ContactSchedules { get; set; }
        public bool canContactOutsideSched { get; set; }

    }

    [Table("Subscription")]
    public class Subscription
    {
        [Key]
        public int SubId { get; set; }
        public int fkUserId { get; set; }
        //0-basic, 1-day rhythm, 2-home area, 3-nav, 4-mem&reminder, 5-door mgt, 6-medical data mgt
        //7-personal health record, 8-service & updates
        public int SubType { get; set; }
    }

    [Table("UserInformation")]
    public class UserInformation
    {
        [Key]
        public int InfoId { get; set; }
        public int fkUserId { get; set; }
        [Display(Name = "Title")]
        public short FkTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        [Display(Name = "Female?")]
        public bool Gender { get; set; }
        [Display(Name = "Right-handed?")]
        public bool Hand { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required(ErrorMessage = "Username is required")]
        //[RegularExpression(@"_[A-Z][0-9][a-z]){8,12}", ErrorMessage = "Username must be have a minimum of 8 characters.")]
        public string UserName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public string Country { get; set; }
        
     
    }


    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    [Table("FeatureTable")]
    public class Feature
    {
        public int FeatureId { get; set; }
        [Display(Name = "Feature Name")]
        public string FeatureName { get; set; }
        public string FeatureFxns { get; set; }
    }

    [Table("PlacesTable")]
    public class Place
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public int fkUserId { get; set; }
        public string PlaceLat { get; set; }
        public string PlaceLong { get; set; }
    }

    [Table("TracksTable")]
    public class Track
    {
        public int TrackId { get; set; }
        public int fkUserId { get; set; }
        public string TrackLat { get; set; }
        public string TrackLong { get; set; }
        public DateTime timestamp { get; set; }
    }

    [Table("GeoLocationTable")]
    public class GeoLocation
    {
        public int GeoLocationId { get; set; }
        public string GeoLocationLat { get; set; }
        public string GeoLocationLong { get; set; }
        public DateTime GeoLocationTimeStamp { get; set; }
        public int fkUserId { get; set; }
        
    }

    [Table("FallTable")]
    public class Fall
    {
        public int FallId { get; set; }
        public DateTime FallTimeStamp { get; set; }
        public string FallResult { get; set; }
        public int fkUserId { get; set; }
        public string FallLat { get; set; }
        public string FallLong { get; set; }
        public byte[] image { get; set; }
       
    }

    [Table("VitalInfo")]
    public class VitalInfo
    {
        public int VitalInfoId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public int fkUserId { get; set; }
    }

    [Table("FallProfileTable")]
    public class FallProfile
    {
        
        public int FallProfileId { get; set; }
        public double fallUpperThreshold { get; set; }
        public double fallLowerThreshold { get; set; }
        public long fallWindowDuration { get; set; }
        public int fallPeakUpperThreshold { get; set; }
        public int fallPeakLowerThreshold { get; set; }
        public double residualMovementThreshold { get; set; }
        public long residualWindowDuration { get; set; }
        public bool isActive { get; set; }
        public string description { get; set; }
        public double activeThreshold { get; set; }
        public double fitminuteThreshold { get; set; }
        public int fitminuteDuration { get; set; }
        public int inactivityDuration { get; set; }
    }
    

    [Table("MemoryTable")]
    public class Memory
    {
        public int MemoryId { get; set; }
        [Display(Name = "Title")]
        [StringLength(30, ErrorMessage = "The title must be less than 30 characters.", MinimumLength = 0)]
        public string MemoryName { get; set; }
        public int fkUserId { get; set; }
        
        [Display(Name = "Frequency")]
        public int MemoryFreq { get; set; }
        
        [Display(Name = "Instruction")]
        [StringLength(120, ErrorMessage = "The instruction must be less than 120 characters.", MinimumLength = 0)]
        public string MemoryInstructions { get; set; }
        public string MemoryDates { get; set; }
        public int MemoryType { get; set; } //0-Normal, 1-Medical, 2-Fitminutes, 3-Reminder, 4-Weigh
    }

    [Table("ContactUsTable")]
    public class ContactUs
    {
        [Key]
        public int ContactUsId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    [Table("ProductTable")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public string ProductCaption { get; set; }
        public string ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public bool ProductVisible { get; set; } 
    }

    [Table("ChargeTable")]
    public class ChargeData
    {
        public int ChargeDataId { get; set; }
        public DateTime ChargeTimeStamp { get; set; }
        public int fkUserId { get; set; }
        public int ChargePct { get; set; }
        public String ActivePct { get; set; }
        public String InactivePct { get; set; }
        public int FallCount { get; set; }
    }

    [Table("ContactTable")]
    public class Contact
    {
        public int ContactId { get; set; }
        [Display(Name = "First")]
        public string FirstName { get; set; }
        [Display(Name = "Last")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }
        [Display(Name = "Relation")]
        public string Relationship { get; set; }
        public short Rank { get; set; }
        public int fkUserId { get; set; }
        //0-false, 1-true
        public bool type { get; set; }

        public string sched { get; set; }

        public bool canContactOutside { get; set; }
        //public bool canContactOutsideSched { get; set; }

    }

    

    [Table("CountryTable")]
    public class Country
    {
        [Key]
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string AdminRole { get; set; }
    }

    [Table("MyLocationTable")]
    public class MyLocation
    {
        [Key]
        public int MyLocationId { get; set; }
        public string MyLat { get; set; }
        public string MyLong { get; set; }
        public int fkUserId { get; set; }
        public DateTime MyLocationStamp { get; set; }
    }

    [Table("FAQTable")]
    public class FAQ
    {
        [Key]
        public int FaqId { get; set; }
        public string FaqQuestion { get; set; }
        public string FaqAnswer { get; set; }

        public int typeFAQ { get; set; } //0-user, 1-contact
    }
    


    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[RegularExpression(@"_[A-Z][0-9][a-z]{8,}", ErrorMessage = "Username must be between 8 and 12 alphanumberic characters")]
        public string UserName { get; set; }
        /*
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        */
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Title")]
        public short FkTitle { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public string Zip { get; set; }
        [Display(Name = "Female?")]
        public bool Gender { get; set; }
        [Required]
        [Display(Name = "Right-handed?")]
        public bool Hand { get; set; }
     
        //0 - user, 1 - contact, 2 - content admin, 3 - local admin, 4 - super admin
        public short UserType { get; set; }
      
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    public class MainInfo
    {
        public int id { get; set; }
        public string ambulance { get; set; }
        public string ambulanceNumber { get; set; }
        public string doorAddress { get; set; }
        public string doorCode { get; set; }
    }
}
