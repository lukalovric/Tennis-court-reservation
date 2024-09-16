using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tennis.Model;
using Tennis.Service.Common;
using System.Security.Claims;
using Tennis.Repository.Common;
using Tennis.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


    public class UserService : IUserService
    {
        public bool requestValid = true;
        private IConfiguration _configuration;
        private IUserRepository _userRepository;
        public static User user = new User();
        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
        _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Username = request.Username;
            user.Password = passwordHash;
            user.Email = request.Email;
            user.IsAdmin = request.IsAdmin;
            user.FullName = request.FullName;
            user.Token= request.Token;
            var result = await _userRepository.CreateUserAsync(user);
            return result;
        }
        public async Task<User> GetUserLoginAsync(User request)
        {
            var result = await _userRepository.GetUserLoginAsync(request);
            if (request.Username != result.Username)
            {
                requestValid = false;
                throw new Exception("Wrong username");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, result.Password))
            {
                requestValid = false;
                throw new Exception("Wrong password");
            }
            else
            {
                BCrypt.Net.BCrypt.Verify(request.Password, result.Password);
                string token = CreateToken(result);
                result.Token = token;
                return result;
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("IsAdmin", user.IsAdmin.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
    }


