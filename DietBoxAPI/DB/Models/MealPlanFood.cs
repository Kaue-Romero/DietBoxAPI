namespace DietBoxAPI.DB.Models
{
    public class MealPlanFood
    {
        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}
