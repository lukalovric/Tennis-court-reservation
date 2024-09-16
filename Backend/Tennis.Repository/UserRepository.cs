using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using Tennis.Model;
using Tennis.Repository.Common;

    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var conn = await dataSource.OpenConnectionAsync();
            using(var command = new NpgsqlCommand("INSERT INTO \"Users\" (\"FullName\", \"UserName\", \"Email\", \"Password\", \"IsAdmin\") " + "VALUES (@p1, @p2, @p3, @p4, @p5)", conn))
                {
                command.Parameters.AddWithValue("p1", user.FullName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("p2", user.Username ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("p3", user.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("p4", user.Password ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("p5", user.IsAdmin ?? false);
                await command.ExecuteNonQueryAsync();

            }
            return user;

        }
        public async Task<User> GetUserLoginAsync(User user)
        {
            var userResult = new User();
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var conn = await dataSource.OpenConnectionAsync();

            string query = @"SELECT ""UserName"", ""Email"", ""Password"", ""IsAdmin"", ""Id""
                         FROM ""Users""
                         WHERE ""UserName"" = @p1;";

            await using var command = new NpgsqlCommand(query, conn);
            command.Parameters.Add(new NpgsqlParameter("p1", NpgsqlDbType.Text) { Value = user.Username ?? (object)DBNull.Value });

        await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                userResult = new User
                {
                    Username = reader.IsDBNull(0) ? null : reader.GetString(0), // Username
                    Email = reader.IsDBNull(1) ? null : reader.GetString(1), // Email
                    Password = reader.IsDBNull(2) ? null : reader.GetString(2), // Password
                    IsAdmin = reader.IsDBNull(3) ? (bool?)null : reader.GetBoolean(3), // IsAdmin
                    Id = reader.IsDBNull(4) ? null : reader.GetGuid(4)
                };
            }

            await conn.CloseAsync();

            return userResult;
        }
    }

