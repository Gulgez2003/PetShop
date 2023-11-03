namespace Entities.Concrete
{
    public class Setting : IEntity
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Information { get; set; }
        public string? TwitterIcon { get; set; }
        public string? FaceBookIcon { get; set; }
        public string? InstagramIcon { get; set; }
    }
}
