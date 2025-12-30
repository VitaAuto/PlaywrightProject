using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Dapper;
using ApiPortfolioProject.Models;
using System;
using System.Collections.Generic;

namespace ApiPortfolioProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=Test.db";
        private const string EmailExistsError = "User with this email already exists.";
        private const string EmailInvalidError = "Email is not valid.";

        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
        }

        /// <summary>
        /// POST: Create user. All fields except Id, CreatedOn, ModifiedOn are required.
        /// Returns 400 if missing fields, 409 if email not unique, 201 with created user.
        /// </summary>
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User data is required.");

            if (string.IsNullOrWhiteSpace(user.FirstName))
                return BadRequest("FirstName is required.");
            if (string.IsNullOrWhiteSpace(user.LastName))
                return BadRequest("LastName is required.");
            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Email is required.");
            if (!IsValidEmail(user.Email))
                return BadRequest(EmailInvalidError);

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var exists = connection.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Users WHERE Email = @Email", new { user.Email });
            if (exists > 0)
                return Conflict(EmailExistsError);

            var sql = @"INSERT INTO Users (FirstName, LastName, Email, IsActive)
                        VALUES (@FirstName, @LastName, @Email, @IsActive);
                        SELECT last_insert_rowid();";
            var id = connection.ExecuteScalar<int>(sql, user);
            user.Id = id;

            var createdUser = connection.QueryFirstOrDefault<User>(
                "SELECT * FROM Users WHERE Id = @Id", new { Id = id });

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// GET: Get user by id. Returns 404 if not found.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var user = connection.QueryFirstOrDefault<User>(
                "SELECT * FROM Users WHERE Id = @Id", new { Id = id });

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// GET: Get all users.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var users = connection.Query<User>("SELECT * FROM Users").AsList();
            return Ok(users);
        }

        /// <summary>
        /// PUT: Update user. All fields except Id, CreatedOn, ModifiedOn are required.
        /// Returns 400 if missing fields, 404 if not found, 409 if email not unique, 204 if no changes, 200 with updated user.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
                return BadRequest("User data is required.");
            if (string.IsNullOrWhiteSpace(user.FirstName))
                return BadRequest("FirstName is required.");
            if (string.IsNullOrWhiteSpace(user.LastName))
                return BadRequest("LastName is required.");
            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Email is required.");
            if (!IsValidEmail(user.Email))
                return BadRequest(EmailInvalidError);

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var exists = connection.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Users WHERE Email = @Email AND Id != @Id", new { user.Email, Id = id });
            if (exists > 0)
                return Conflict(EmailExistsError);

            var currentUser = connection.QueryFirstOrDefault<User>(
                "SELECT * FROM Users WHERE Id = @Id", new { Id = id });

            if (currentUser == null)
                return NotFound();

            if (currentUser.FirstName == user.FirstName &&
                currentUser.LastName == user.LastName &&
                currentUser.Email == user.Email &&
                currentUser.IsActive == user.IsActive)
            {
                return NoContent(); // 204
            }

            var sql = @"UPDATE Users
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    IsActive = @IsActive,
                    ModifiedOn = @ModifiedOn
                WHERE Id = @Id";

            connection.Execute(sql, new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.IsActive,
                ModifiedOn = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Id = id
            });

            var updatedUser = connection.QueryFirstOrDefault<User>(
                "SELECT * FROM Users WHERE Id = @Id", new { Id = id });

            return Ok(updatedUser);
        }

        /// <summary>
        /// PATCH: Partial update user. At least one field must be provided.
        /// Returns 400 if no fields, 404 if not found, 409 if email not unique, 204 if no changes, 200 with updated user.
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, [FromBody] UserPatchDto patch)
        {
            if (patch == null)
                return BadRequest("Patch data is required.");

            if (patch.FirstName == null && patch.LastName == null && patch.Email == null && patch.IsActive == null)
                return BadRequest("At least one field must be provided for patch.");

            using var connection = new SqliteConnection(_connectionString);
            connection.Open
();

            var user = connection.QueryFirstOrDefault<User>(
                "SELECT * FROM Users WHERE Id = @Id", new { Id = id });

            if (user == null)
                return NotFound();

            // at least 1 field should be updated
            bool changed = false;
            if (patch.FirstName != null && patch.FirstName != user.FirstName)
            {
                user.FirstName = patch.FirstName;
                changed = true;
            }
            if (patch.LastName != null && patch.LastName != user.LastName)
            {
                user.LastName = patch.LastName;
                changed = true;
            }
            if (patch.Email != null && patch.Email != user.Email)
            {
                user.Email = patch.Email;
                changed = true;
            }
            if (patch.IsActive.HasValue && patch.IsActive.Value != user.IsActive)
            {
                user.IsActive = patch.IsActive.Value;
                changed = true;
            }

            if (!changed)
                return NoContent();

            // Mandatory fields validations
            if (string.IsNullOrWhiteSpace(user.FirstName))
                return BadRequest("FirstName is required.");
            if (string.IsNullOrWhiteSpace(user.LastName))
                return BadRequest("LastName is required.");
            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Email is required.");
            if (!IsValidEmail(user.Email))
                return BadRequest(EmailInvalidError);

            var exists = connection.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Users WHERE Email = @Email AND Id != @Id", new { user.Email, Id = id });
            if (exists > 0)
                return Conflict(EmailExistsError);

            var sql = @"UPDATE Users
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    IsActive = @IsActive,
                    ModifiedOn = @ModifiedOn
                WHERE Id = @Id";

            connection.Execute(sql, new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.IsActive,
                ModifiedOn = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Id = id
            });

            var updatedUser = connection.QueryFirstOrDefault<User>(
                "SELECT * FROM Users WHERE Id = @Id", new { Id = id });

            return Ok(updatedUser);
        }

        /// <summary>
        /// DELETE: Delete user by id. Returns 404 if not found, 204 if deleted.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var sql = "DELETE FROM Users WHERE Id = @Id";
            var affected = connection.Execute(sql, new { Id = id });

            if (affected == 0)
                return NotFound();

            return NoContent();
        }
    }
}