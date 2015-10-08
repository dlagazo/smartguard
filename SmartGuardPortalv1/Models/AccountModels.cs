using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace SmartGuardPortalv1.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Memory> Memories { get; set; }
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
        public short FkTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public bool Gender { get; set; }
        public bool Hand { get; set; }
     
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

    [Table("MemoryTable")]
    public class Memory
    {
        public int MemoryId { get; set; }
        public string MemoryName { get; set; }
        public int fkUserId { get; set; }
        public DateTime MemoryDate { get; set; }
        public int MemoryFreq { get; set; }
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
        public string Mobile { get; set; }
        [Display(Name = "Relation")]
        public string Relationship { get; set; }
        public short Rank { get; set; }
        public int fkUserId { get; set; }

    }

    [Table("SubscriptionTable")]
    public class Subscription
    {
        public int SubId { get; set; }
        public string SubName { get; set; }
        public string SubType { get; set; }
        public DateTime SubStart { get; set; }
        public DateTime SubEnd { get; set; }
        public Boolean SubStatus { get; set; }
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
        [StringLength(20)]
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public short FkTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public bool Gender { get; set; }
        public bool Hand { get; set; }

        public short UserType { get; set; }
      
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
