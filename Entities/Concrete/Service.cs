namespace Entities.Concrete
{
    public class Service : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public bool IsDeleted { get; set; }
    }
}
