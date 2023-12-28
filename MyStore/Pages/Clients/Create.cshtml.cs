using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errormessage = "";
        public string successmessage = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0
                || clientInfo.address.Length == 0)
            {
                errormessage = "All fields are required !";
                return;
            }

            //save the client into database
            try
            {
                string connectionstring = $"Server=host.docker.internal,1433;Initial Catalog=myStore;User ID=sa; Password=testsql@123;";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "INSERT INTO clients" +
                        "(name, email, phone, address) VALUES" +
                        "(@name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("name", clientInfo.name);
                        command.Parameters.AddWithValue("email", clientInfo.email);
                        command.Parameters.AddWithValue("phone", clientInfo.phone);
                        command.Parameters.AddWithValue("address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            successmessage = "New Client added successfully";
            Response.Redirect("/Clients/Index");
        }
    }
}
