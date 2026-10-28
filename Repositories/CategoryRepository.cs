using System.Data;
using ECommerceManagement.Models;
using Microsoft.Data.SqlClient;

namespace Ecommerce.Repositories;

public class CategoryRepository
{
    private readonly string _connectionString;
    public CategoryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<int> AddAsync(Category category)

    {

        const string query = @"

                INSERT INTO Categories (CategoryName,Aislelocation, IsActive)

                OUTPUT INSERTED.CategoryId

                VALUES (@CategoryName, @Aislelocation,@IsActive);";

        using SqlConnection connection = new SqlConnection(_connectionString);

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 100).Value = category.CategoryName;
        command.Parameters.Add("@Aislelocation", SqlDbType.NVarChar, 100).Value = category.Aislelocation;

        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = category.IsActive;

        await connection.OpenAsync();

        object? result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);

    }
}