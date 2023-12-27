using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo ci = new ClientInfo();
        public string successmessage = "";
        public string errormessage = "";
        public void OnGet()
        {
            String currentId = Request.Query["id"];
            var dbhost = Environment.GetEnvironmentVariable("DB_HOST");

            string connectionstring = $"Data Source=mystoredb;Initial Catalog=myStore;User ID=sa; Password=testsql@123;";
            try
            {

                using(SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "SELECT * FROM clients WHERE id=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@id", currentId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                ci.id = "" + reader.GetInt32(0);
                                ci.name = reader.GetString(1);
                                ci.email = reader.GetString(2);
                                ci.phone = reader.GetString(3);
                                ci.address = reader.GetString(4);
                            }
                            
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void OnPost() 
        {
            ci.id = Request.Form["id"];
            ci.name = Request.Form["name"];
            ci.email = Request.Form["email"];
            ci.phone = Request.Form["phone"];
            ci.address = Request.Form["address"];

            try
            {
                var dbhost = Environment.GetEnvironmentVariable("DB_HOST");

                string connectionstring = $"Data Source=mystoredb;Initial Catalog=myStore;User ID=sa; Password=testsql@123;";
                using (SqlConnection conn = new SqlConnection(connectionstring)) 
                {
                    conn.Open();
                    string sql = "UPDATE clients "+
                        "SET name=@name, email=@email,phone=@phone, address=@address " +
                        "WHERE id=@id";
                    using(SqlCommand command = new SqlCommand(sql, conn)) 
                    {
                        command.Parameters.AddWithValue("@name", ci.name);
                        command.Parameters.AddWithValue("@email", ci.email);
                        command.Parameters.AddWithValue("@phone", ci.phone);
                        command.Parameters.AddWithValue("@address", ci.address);
                        command.Parameters.AddWithValue("@id", ci.id);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
                Console.WriteLine(ex.Message);
            }
            successmessage = "Successfully Updated Entry !";
            Response.Redirect("/Clients/Index");
            //successmessage = "Successfully Updated Entry!";
        }
    }
}
