using BCrypt.Net;
using Dapper;
using FleursDeLilas.API.Data;
using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Entities;
using FleursDeLilas.API.Repositories.Interfaces;
using FleursDeLilas.API.Services.Interfaces;

namespace FleursDeLilas.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IFleursUserRepository _userRepository;
        private readonly IDbConnectionFactory _connectionFactory;

        public AuthService(IFleursUserRepository userRepository, IDbConnectionFactory connectionFactory)
        {
            _userRepository = userRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            // Check if username already exists
            var existingUser = await GetUserByUsernameAsync(dto.Username);
            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Username already exists."
                };
            }

            // Hash password using BCrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Create new user
            var user = new FleursUser
            {
                Username = dto.Username,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            try
            {
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "User registered successfully.",
                    User = new LoginResponseDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        CreatedAt = user.CreatedAt,
                        UpdatedAt = user.UpdatedAt
                    }
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}"
                };
            }
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            // Get user by username
            var user = await GetUserByUsernameAsync(dto.Username);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid username or password."
                };
            }

            // Verify password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid username or password."
                };
            }

            // Login successful
            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful.",
                User = new LoginResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                }
            };
        }

        private async Task<FleursUser?> GetUserByUsernameAsync(string username)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT id, username, password, created_at, updated_at 
                FROM fleurs_user 
                WHERE username = @Username;";

            return await connection.QueryFirstOrDefaultAsync<FleursUser>(sql, new { Username = username });
        }
    }
}
