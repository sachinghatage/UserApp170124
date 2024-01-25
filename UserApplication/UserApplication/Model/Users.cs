using System.ComponentModel.DataAnnotations;

namespace UserApplication.Model
{
    public class Users
    {
        public long Id { get; set; }
        public string Name { get; set; }    //? so that property should not be null  (string?)
        public string Email { get; set; }   //? so that property should not be null

        [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Phone number must be 10 digits.")]
        public int Phone { get; set; }

        public byte[] FileContent { get; set; }    //? so that property should not be null

        public Gender UserGender { get; set; } = Gender.Unknown;

        public Address UserAddress { get; set; }

    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public enum Gender
    {
        Male,Female,Unknown
    }
}
