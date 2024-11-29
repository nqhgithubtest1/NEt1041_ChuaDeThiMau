namespace NET1041_ChuaDeThiMau.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
