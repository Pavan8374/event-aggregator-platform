using Identity.Domain.Common;

namespace Identity.Domain.Enumerations
{
    public class RoleType : Enumeration
    {
        public static RoleType Attendee = new(1, "Attendee");
        public static RoleType Organizer = new(2, "Organizer");
        public static RoleType Vendor = new(3, "Vendor");
        public static RoleType SpeakerPerformer = new(4, "Speaker/Performer");
        public static RoleType Admin = new(5, "Admin");
        public static RoleType SystemService = new(6, "System Service");
        public static RoleType Moderator = new(7, "Moderator");
        public static RoleType Advertiser = new(8, "Advertiser");

        public RoleType(int id, string name) : base(id, name) { }

        // Business rules methods
        public bool CanManageUsers() =>
            this == Admin || this == Moderator;

        public bool CanCreateEvents() =>
            this == Organizer || this == Admin;

        public bool CanAccessAdminPanel() =>
            this == Admin || this == SystemService;

        public bool CanModerateContent() =>
            this == Admin || this == Moderator;

        public bool IsStandardUser() =>
            this == Attendee;

        public bool CanCreateAds() =>
            this == Advertiser || this == Admin;

        public string GetDefaultPermissionLevel() =>
            this == Admin ? "Full" :
            this == Moderator ? "High" :
            this == SystemService ? "System" :
            this == Organizer ? "Medium" : "Basic";
    }
}
