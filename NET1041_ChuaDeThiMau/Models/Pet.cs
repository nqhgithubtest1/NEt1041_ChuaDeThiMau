namespace NET1041_ChuaDeThiMau.Models
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public decimal Price { get; set; }
        public Customer? Customer { get; set; }
    }
}
