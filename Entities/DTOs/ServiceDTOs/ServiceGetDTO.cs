namespace Entities.DTOs.ServiceDTOs
{
    public class ServiceGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public bool IsDeleted { get; set; }
    }
}
