namespace UserApplication.Model
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }    //? so that property should not be null
        public string Email { get; set; }   //? so that property should not be null
        public int Phone { get; set; }

        public byte[] FileContent { get; set; }    //? so that property should not be null
    }
}
