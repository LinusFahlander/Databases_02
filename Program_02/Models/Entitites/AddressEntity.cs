namespace Program_2.Models.Entitites
{
    internal class AddressEntity
    {
        public int Id { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
