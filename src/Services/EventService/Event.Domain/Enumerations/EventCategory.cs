using Event.Domain.Common;

namespace Event.Domain.Enumerations
{
    public class EventCategory : Enumeration
    {
        public static EventCategory MusicAndConcerts = new EventCategory(1, nameof(MusicAndConcerts));
        public static EventCategory ArtsAndTheatre = new EventCategory(2, nameof(ArtsAndTheatre));
        public static EventCategory ConferencesAndSeminars = new EventCategory(3, nameof(ConferencesAndSeminars));
        public static EventCategory SocialAndParties = new EventCategory(4, nameof(SocialAndParties));
        public static EventCategory SportsAndFitness = new EventCategory(5, nameof(SportsAndFitness));
        public static EventCategory GamingAndEsports = new EventCategory(6, nameof(GamingAndEsports));
        public static EventCategory FoodAndDrink = new EventCategory(7, nameof(FoodAndDrink));
        public static EventCategory EducationAndWorkshops = new EventCategory(8, nameof(EducationAndWorkshops));
        public static EventCategory FamilyAndCommunity = new EventCategory(9, nameof(FamilyAndCommunity));
        public static EventCategory ScienceAndTech = new EventCategory(10, nameof(ScienceAndTech));

        public EventCategory(int id, string name) : base(id, name)
        {
        }
    }
}
