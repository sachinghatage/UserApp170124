namespace UserApplication.Model
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }    //? so that property should not be null  (string?)
        public string Email { get; set; }   //? so that property should not be null
        public int Phone { get; set; }

        public byte[] FileContent { get; set; }    //? so that property should not be null

        public Gender UserGender { get; set; } = Gender.Unknown;

    }

    public enum Gender
    {
        Male,Female,Unknown
    }
}
