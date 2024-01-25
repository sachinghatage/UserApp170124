using System.Data.SqlClient;


namespace UserApplication.Model
{
    public class DataAccessLayer
    {
        public void Saveuser(Users user, IConfiguration configuration,Address address)
        {
            using (
                SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                connection.Open();
                string query = "INSERT INTO users (Name, Email, Phone, FileContent,Gender) VALUES (@Name, @Email, @Phone, CONVERT(VARBINARY(MAX), @FileContent),@Gender); select SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@FileContent", user.FileContent); // Assuming user.FileContent is a byte array
                    command.Parameters.AddWithValue("@Gender", user.UserGender.ToString());//by default numbers are stored in db for enum so tostring() is used

                    
                    int userId=Convert.ToInt32(command.ExecuteScalar());

                    // Now insert the address with the obtained userId
                    string addressQuery = "insert into address(UserId,Street,City,State,Zipcode) values(@UserId,@Street,@City,@State,@Zipcode)";

                    using(SqlCommand addressCommand= new SqlCommand(addressQuery, connection))
                    {
                        addressCommand.Parameters.AddWithValue("@UserId",userId);
                        addressCommand.Parameters.AddWithValue("@Street", address.Street);
                        addressCommand.Parameters.AddWithValue("@City", address.City);
                        addressCommand.Parameters.AddWithValue("@State", address.State);
                        addressCommand.Parameters.AddWithValue("@Zipcode", address.ZipCode);

                        addressCommand.ExecuteNonQuery();
                    }
                   
                }
            }
        }

        public List<Users> GetUsers(IConfiguration configuration)
        {
            List<Users> users = new List<Users>();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS")))
            {
                connection.Open();
               
                string query = "select u.Id,u.name,u.email,u.gender,a.city,a.state from users u INNER JOIN address a on u.Id=a.userId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users user = new Users();
                            user.Id =Convert.ToInt32( reader["Id"]);
                            user.Name = Convert.ToString(reader["Name"]);
                            user.Email = Convert.ToString(reader["Email"]);
                         
                            string userGender = Convert.ToString(reader["Gender"]);

                            if (Enum.TryParse(userGender, out Gender enumgender))
                            {
                                user.UserGender = enumgender;
                            }

                            Address address = new Address();
                            address.City =Convert.ToString( reader["City"]);
                            address.State = Convert.ToString(reader["State"]);
                            user.UserAddress = address;

                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public void deleteUser(int id, IConfiguration configuration)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS")))
            {
                connection.Open();
                string query = "delete from users where id = '" + id + "'";

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateUser(Users user, IConfiguration configuration)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS")))
            {
                connection.Open();
                string query = "update users set Name='" + user.Name + "',Email='" + user.Email + "',Phone='" + user.Phone + "' where id='" + user.Id + "'";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Users GetUser(int id, IConfiguration configuration)
        {
            Users user = new Users();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS")))
            {
                connection.Open();
                string query = "select * from users where Id='" + id + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = Convert.ToInt32(reader["Id"]);
                            user.Name = Convert.ToString(reader["Name"]);
                            user.Email = Convert.ToString(reader["Email"]);
                            user.Phone = Convert.ToInt32(reader["Phone"]);

                        }
                    }
                }
            }
            return user;
        }
     

    }
}
