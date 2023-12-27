using System;
using System.Data.SqlClient;

string connectionString = "Data Source=mystoredb, 1433;Initial Catalog=master;User ID=sa; Password=testsql@123;";

// Specify the new database name
string databaseName = "myStore";

// SQL command to create a new database
string createDatabaseSql = $"CREATE DATABASE {databaseName}";

try
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        using (SqlCommand command = new SqlCommand(createDatabaseSql, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine($"Database '{databaseName}' created successfully.");
        }
        connection.ChangeDatabase(databaseName);

        string tableQuery = "CREATE TABLE clients (id INT NOT NULL PRIMARY KEY IDENTITY," +
            "name VARCHAR (100) NOT NULL, email VARCHAR (150) NOT NULL UNIQUE," +
            "phone VARCHAR(20) NULL, address VARCHAR(100) NULL, created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP);" +
            "INSERT INTO clients (name, email, phone, address) VALUES('Bill Gates', 'bill.gates@microsoft.com', '+123456789', 'New York, USA'),('Elon Musk', 'elon.musk@spacex.com', '+111222333', 'Florida, USA')";
        using (SqlCommand command = new SqlCommand(tableQuery, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine($"Database '{databaseName}' Updated successfully.");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error creating database: {ex.Message}");
}



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
