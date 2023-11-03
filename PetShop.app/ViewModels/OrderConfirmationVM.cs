namespace PetShop.app.ViewModels
{
    public class OrderConfirmationVM
    {
        public string Owner { get; set; }
        public int Cvv { get; set; }
        public int CardNumber { get; set; }
        public string CardMonth { get; set; }
        public string CardYear { get; set; }
    }
}
