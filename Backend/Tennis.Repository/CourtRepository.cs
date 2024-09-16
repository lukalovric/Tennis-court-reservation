using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using Tennis.Model;
using Tennis.Repository.Common;

public class CourtRepository : ICourtRepository
{
    private readonly string _connectionString;

    public CourtRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Court>> GetAllCourtsAsync()
    {
        var courts = new List<Court>();
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT * FROM \"Courts\"", connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            courts.Add(new Court
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                IsIndoor = reader.GetBoolean(2)
            });
        }

        return courts;
    }

    public async Task AddCourtAsync(Court court)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("INSERT INTO \"Courts\" (\"Id\", \"Name\", \"IsIndoor\") VALUES (@Id, @Name, @IsIndoor)", connection);
        command.Parameters.AddWithValue(@"id", court.Id);
        command.Parameters.AddWithValue("@Name", court.Name);
        command.Parameters.AddWithValue("@IsIndoor", court.IsIndoor);
        await command.ExecuteNonQueryAsync();
    }
}