using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                var dbhost = Environment.GetEnvironmentVariable("DB_HOST");

                string connectionstring = $"Data Source=mystoredb;Initial Catalog=myStore;User ID=sa; Password=testsql@123;";
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    sqlConnection.Open();
                    string sql = "SELECT * FROM clients";
                    using(SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                    {
                        using(SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo ci = new ClientInfo();
                                ci.id =" " + reader.GetInt32(0);
                                ci.name = reader.GetString(1);
                                ci.email = reader.GetString(2);
                                ci.phone = reader.GetString(3);
                                ci.address = reader.GetString(4);
                                ci.createdAt = reader.GetDateTime(5).ToString();
                                listClients.Add(ci);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : "+ex.Message);
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String createdAt;
    }
}
