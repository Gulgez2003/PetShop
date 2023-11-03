namespace Entities.DTOs.OrderDTOs
{
    public class OrderPostDTO
    {
        public List<Product> Products { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsCourier { get; set; }
        public bool IsCard { get; set; }
        public bool IsCash { get; set; }
        public bool IsSms { get; set; }
        public string Comment { get; set; }
    }
}
