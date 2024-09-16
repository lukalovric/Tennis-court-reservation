using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

public class ReservationRepository : IReservationRepository
{
    private readonly string _connectionString;

    public ReservationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        var reservations = new List<Reservation>();
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT * FROM \"Reservations\"", connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            reservations.Add(new Reservation
            {
                Id = reader.GetInt32(0),
                ReservationDate = reader.GetDateTime(1),
                CourtId = reader.GetInt32(2),
                UserId = reader.GetGuid(3)
            });
        }

        return reservations;
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("INSERT INTO \"Reservations\" (\"ReservationDate\", \"CourtId\", \"UserId\") VALUES (@ReservationDate, @CourtId, @UserId)", connection);
        command.Parameters.AddWithValue("@ReservationDate", reservation.ReservationDate);
        command.Parameters.AddWithValue("@CourtId", reservation.CourtId);
        command.Parameters.AddWithValue("@UserId", reservation.UserId);
        await command.ExecuteNonQueryAsync();
    }
    public async Task<bool> DeleteReservationAsync(int id)
    {
        try
        {
            const string query = "DELETE FROM \"Reservations\" WHERE \"Id\" = @Id";

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting reservation: {ex.Message}");
            return false;
        }
    }
}