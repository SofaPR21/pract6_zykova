namespace pract6_zykova.Controllers
{
    public class CoursesClass
    {
        public string? Title { get; set; } = null!;

        public string? Teacher { get; set; } = null!;

        public int? DurationHours { get; set; }

        public decimal? Price { get; set; }

        public bool? IsActive { get; set; }
    }
}
