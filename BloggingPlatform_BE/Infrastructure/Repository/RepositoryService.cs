using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Domain.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using LusiUtilsLibrary.Backend.Initialization;

namespace BloggingPlatform_BE.Infrastructure.Repository;


/* tips:
 * For create the tables at the startup, it's better to get the connection from an InitializeTables method
 * and using the same instance of the connection (to avoid creating a connection for each table)
 *
 * For the CRUD operations, it's better to create a connection for each query that need to be execute
 * 
 * For saving the guid, its better to use ToUpperInvariant, because = operator of sqlite is case sensitive
 */

public class RepositoryService : IRepositoryService
{
    #region private fields
    private readonly string _dbName; // BloggingPlatform.db
    private readonly string _connectionString; // only the path to the db
    private readonly ILogger<IRepositoryService> _logger;
    #endregion

    #region constructor
    public RepositoryService(string connectionString, string dbName, ILogger<IRepositoryService> logger)
    {
        #region InitialChecks
        InitializeChecks.InitialCheck(connectionString, "ConnectionString cannot be null");
        InitializeChecks.InitialCheck(dbName, "Database name cannot be null");
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");
        #endregion

        _connectionString = connectionString;
        _dbName = dbName;
        _logger = logger;
    }
    #endregion

    #region User
    public void AddUser(UserDto user)
    {
        try
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();

            string query = "INSERT INTO Users (UserGuid, UserName, UserSurname, UserEmail, UserCreatedOn, Salt, HashCode) " +
                           "VALUES (:UserGuid, :UserName, :UserSurname, :UserEmail, :UserCreatedOn, :Salt, :HashCode)    ";

            SqliteParameter[] parameters = new SqliteParameter[]
            {
                new(":UserGuid", user.UserGuid.ToString().ToUpperInvariant()),
                new(":UserName", user.UserName),
                new(":UserSurname", user.UserSurname),
                new(":UserEmail", user.UserEmail),
                new(":UserCreatedOn", user.UserCreatedOn.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                new(":Salt",user.Salt.ToString()),
                new(":HashCode", user.HashCode.ToString())
            };

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
            _logger.LogInformation("Repository Service - Correctly added a new user with guid <{userGuid}> to the db.", user.UserGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during adding user with guid <{user.UserGuid}> to db", ex);
        }
    }

    public void UpdateUser(UserDto user)
    {
        try
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();

            string query = "UPDATE Users SET                " +
                           "UserName = :UserName,           " +
                           "UserSurname = :UserSurname,     " +
                           "UserEmail = :UserEmail,         " +
                           "UserCreatedOn = :UserCreatedOn, " +
                           "Salt = :Salt,                   " +
                           "HashCode = :HashCode            " +
                           "WHERE UserGuid = :UserGuid      ";

            SqliteParameter[] parameters = new SqliteParameter[]
            {
                new(":UserName", user.UserName),
                new(":UserSurname", user.UserSurname),
                new(":UserEmail", user.UserEmail),
                new(":UserCreatedOn", user.UserCreatedOn.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                new(":Salt", user.Salt.ToString()),
                new(":HashCode", user.HashCode.ToString()),
                new(":UserGuid", user.UserGuid.ToString().ToUpperInvariant())
            };

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
            _logger.LogInformation("Repository Service - Correctly updated user with guid <{userGuid}>", user.UserGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during updating user with guid <{user.UserGuid}>", ex);
        }
    }

    public void DeleteUser(Guid userGuid)
    {
        try
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM Users           " +
                           "WHERE UserGuid = :UserGuid  ";

            SqliteParameter parameter = new SqliteParameter(":UserGuid", userGuid);

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.Add(parameter);
            command.ExecuteNonQuery();
            _logger.LogInformation("Repository Service - Correctly deleted user with user guid <{userGuid}>", userGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during deleting user with guid <{userGuid}> to db", ex);
        }
    }

    public UserDto GetUserByGuid(Guid userGuid)
    {
        try
        {
            UserDto user = new UserDto();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users         " +
                               "WHERE UserGuid = :UserGuid  ";

                SqliteParameter parameter = new SqliteParameter(":UserGuid", userGuid);

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(parameter);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.UserGuid = Guid.Parse(Convert.ToString(reader["UserGuid"]).ToUpperInvariant());
                    user.UserName = Convert.ToString(reader["UserName"]);
                    user.UserSurname = Convert.ToString(reader["UserSurname"]);
                    user.UserEmail = Convert.ToString(reader["UserEmail"]);
                    user.UserCreatedOn = Convert.ToDateTime(reader["UserCreatedOn"]);
                    user.Salt = reader["Salt"].ToString();
                    user.HashCode = reader["HashCode"].ToString();
                }
            }

            if (user != null)
            {
                _logger.LogInformation("Repository Service - Correctly returned user with guid <{userGuid}>", userGuid);
                return user;
            }

            _logger.LogInformation("Repository Service - No user found with guid <{userGuid}>", userGuid);
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during retrieving user with guid <{userGuid}> from db", ex);
        }
    }

    public List<UserDto> GetAllUsers()
    {
        try
        {
            List<UserDto> users = new List<UserDto>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM USERS";

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = query;
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserDto user = new UserDto();
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.UserGuid = Guid.Parse(Convert.ToString(reader["UserGuid"]).ToUpperInvariant());
                    user.UserName = Convert.ToString(reader["UserName"]);
                    user.UserSurname = Convert.ToString(reader["UserSurname"]);
                    user.UserEmail = Convert.ToString(reader["UserEmail"]);
                    user.UserCreatedOn = Convert.ToDateTime(reader["UserCreatedOn"]);
                    user.Salt = Convert.ToString(reader["Salt"]);
                    user.HashCode = Convert.ToString(reader["HashCode"]);

                    users.Add(user);
                }
            }

            if (users.Count > 0)
            {
                _logger.LogInformation("Repository Service - Correctly returned all users");
                return users;
            }

            _logger.LogInformation($"Repository Service - No users found with in database.");
            return [];
        }
        catch (Exception ex)
        {
            throw new Exception("Repository Service - Error during retrieving all users from db.", ex);
        }
    }
    #endregion

    #region BlogPost
    public void AddBlogPost(BlogPostDto blogPost)
    {
        try
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();

            int userId = blogPost.UserId;

            string query = "INSERT INTO BlogPosts (UserId, PostGuid, PostTitle, PostContent, PostTags, PostCreatedOn, PostModifiedOn)   " +
                           "VALUES (:UserId, :PostGuid, :PostTitle, :PostContent, :PostTags, :PostCreatedOn, :PostModifiedOn)           ";

            SqliteParameter[] parameters = new SqliteParameter[]
            {
                new(":UserId", userId),
                new(":PostGuid", blogPost.PostGuid.ToString().ToUpperInvariant()),
                new(":PostTitle", blogPost.PostTitle),
                new(":PostContent", blogPost.PostContent),
                new(":PostTags", blogPost.PostTags),
                new(":PostCreatedOn", blogPost.PostCreatedOn.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                new(":PostModifiedOn", DBNull.Value)
            };

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
            _logger.LogInformation("Repository Service - Correctly added a new blog post with guid <{blogPostGuid}>", blogPost.PostGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during adding blog post with guid <{blogPost.PostGuid}>", ex);
        }
    }

    public void UpdateBlogPost(BlogPostDto blogPost)
    {
        try
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();

            string query = "UPDATE BlogPosts SET                " +
                           "PostTitle = :PostTitle,             " +
                           "PostContent = :PostContent,         " +
                           "PostTags = :PostTags,               " +
                           "PostModifiedOn = :PostModifiedOn    " +
                           "WHERE PostGuid = :PostGuid          ";

            SqliteParameter[] parameters = new SqliteParameter[]
            {                
                new(":PostTitle", blogPost.PostTitle),
                new(":PostContent", blogPost.PostContent),
                new(":PostTags", blogPost.PostTags),
                new(":PostModifiedOn", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                new(":PostGuid", blogPost.PostGuid.ToString().ToUpperInvariant())
            };

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
            _logger.LogInformation("Repository Service - Correctly updated blog post with guid <{blogPostGuid}>", blogPost.PostGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during updating blog post wit guid <{blogPost.PostGuid}> to db", ex);
        }
    }

    public void DeleteBlogPost(Guid blogPostGuid)
    {
        try
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);

            connection.Open();

            string query = "DELETE FROM BlogPosts       " +
                           "WHERE PostGuid = :PostGuid  ";

            SqliteParameter parameter = new SqliteParameter(":PostGuid", blogPostGuid);

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.Add(parameter);
            command.ExecuteNonQuery();
            _logger.LogInformation("Repository Service - Correctly deleted blog post with guid <{blogPostGuid}>", blogPostGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during deleting blog post with guid <{blogPostGuid}> to db", ex);
        }
    }

    public BlogPostDto GetBlogPostByGuid(Guid blogPostGuid)
    {
        // todo add json handling for post tags:
        /* something like:
         * {
         *     tag1 : "tag1",
         *     tag2 : "tag2"
         * }
         */
        try
        {
            BlogPostDto blogPost = new BlogPostDto();

            using SqliteConnection connection = new SqliteConnection(_connectionString);

            connection.Open();

            string query = "SELECT * FROM BlogPosts " +
                           "WHERE PostGuid = :PostGuid  ";

            SqliteParameter parameter = new SqliteParameter(":PostGuid", blogPostGuid);

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.Add(parameter);

            SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                blogPost.PostId = Convert.ToInt32(reader["PostId"]);
                blogPost.UserId = Convert.ToInt32(reader["UserId"]);
                blogPost.PostGuid = Guid.Parse(Convert.ToString(reader["PostGuid"]).ToUpperInvariant());
                blogPost.PostTitle = Convert.ToString(reader["PostTitle"]);
                blogPost.PostContent = Convert.ToString(reader["PostContent"]);
                blogPost.PostTags = Convert.ToString(reader["PostTags"]);
                blogPost.PostCreatedOn = Convert.ToDateTime(reader["PostCreatedOn"]);
                blogPost.PostModifiedOn = (reader["PostModifiedOn"] is DBNull) ? DateTime.MinValue : Convert.ToDateTime(reader["PostModifiedOn"]);
            }

            if (blogPost != null)
            {
                _logger.LogInformation("Repository Service - Correctly returned blog post with guid <{blogPostGuid}>", blogPostGuid);
                return blogPost;
            }

            _logger.LogInformation("Repository Service - No blog post found with guid <{blogPostGuid}>", blogPostGuid);
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during retrieving blog post with guid <{blogPostGuid}> from db", ex);
        }
    }

    public List<BlogPostDto> GetAllBlogPosts()
    {
        try
        {
            List<BlogPostDto> blogPosts = new List<BlogPostDto>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM BlogPosts";

                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = query;
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BlogPostDto blogPost = new BlogPostDto();
                    blogPost.PostId = Convert.ToInt32(reader["PostId"]);
                    blogPost.UserId = Convert.ToInt32(reader["UserId"]);
                    blogPost.PostGuid = Guid.Parse(Convert.ToString(reader["PostGuid"]).ToUpperInvariant());
                    blogPost.PostTitle = Convert.ToString(reader["PostTitle"]);
                    blogPost.PostContent = Convert.ToString(reader["PostContent"]);
                    blogPost.PostTags = Convert.ToString(reader["PostTags"]);
                    blogPost.PostCreatedOn = Convert.ToDateTime(reader["PostCreatedOn"]);
                    blogPost.PostModifiedOn = (reader["PostModifiedOn"] is DBNull) ? DateTime.MinValue : Convert.ToDateTime(reader["PostModifiedOn"]);

                    blogPosts.Add(blogPost);
                }
            }

            if (blogPosts.Count > 0)
            {
                _logger.LogInformation("Repository Service - Correctly returned all blog posts");
                return blogPosts;
            }

            _logger.LogInformation($"Repository Service - No blog posts found with in database.");
            return [];
        }
        catch (Exception ex)
        {
            throw new Exception($"Repository Service - Error during retrieving all posts from db", ex);
        }
    }


    #endregion

    #region startup methods
    public void InitializeTables(string directory, string filePath)
    {
        //using (SqliteConnection connection = new SqliteConnection(_connectionString))
        //{
        //    connection.Open();
        //    CreateUserTable(connection);
        //    CreateBlogPostTable(connection);
        //    CreateAdminUser(connection);
        //}ù
        CreateDatabase(directory, filePath);
    }

    private void CreateUserTable(SqliteConnection connection)
    {
        string query = "CREATE TABLE IF NOT EXISTS Users ( " +
                       "UserId INT PRIMARY KEY,             " +
                       "UserGuid TEXT NOT NULL,             " +
                       "UserName TEXT NOT NULL,             " +
                       "UserSurname TEXT NOT NULL,          " +
                       "UserEmail TEXT NOT NULL,            " +
                       "UserPassword TEXT NOT NULL,         " +
                       "UserCreatedOn TEXT NOT NULL,        " +
                       "Salt TEXT NULL,                     " +
                       "Hash TEXT NULL,                     " +
                       ")                                   ";

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
        _logger.LogInformation("Repository Service - Correctly created Users table to db : {dbName}", _dbName);
    }

    private void CreateBlogPostTable(SqliteConnection connection)
    {
        string query = "CREATE TABLE IF NOT EXISTS BlogPosts (         " +
                       "PostId INT PRIMARY KEY,                         " +
                       "UserId INT,                                     " +
                       "PostGuid TEXT NOT NULL,                         " +
                       "PostTitle TEXT NOT NULL,                        " +
                       "PostContent TEXT NOT NULL,                      " +
                       "PostCreatedOn TEXT NOT NULL,                    " +
                       "PostModifiedOn TEXT NULL,                       " +
                       "FOREIGN KEY (UserId) REFERENCES Users(UserId)   " +
                       ")                                               ";

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
        _logger.LogInformation("Repository Service - Correctly created BlogPosts table to db : {dbName}", _dbName);
    }

    private void CreateAdminUser(SqliteConnection connection)
    {
        string query = "INSERT INTO Users (UserGuid, UserName, UserSurname, UserEmail, UserPassword, UserCreatedOn) " +
                       "VALUES (:UserGuid, :UserName, :UserSurname, :UserEmail, :UserPassword, :UserCreatedOn)      ";

        SqliteParameter[] parameters = new SqliteParameter[]
        {
            new(":UserGuid", Guid.NewGuid().ToString()),
            new(":UserName", "admin"),
            new(":UserSurname", "admin"),
            new(":UserEmail", "admin@admin.admin"),
            new(":UserPassword", "admin"),
            new(":UserCreatedOn", DateTime.UtcNow.ToString()),
        };

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = query;
        command.Parameters.AddRange(parameters);
        command.ExecuteNonQuery();

        _logger.LogInformation("Repository Service - Correctly added admin user to Users table");
    }

    private void CreateDatabase(string directory, string filePath)
    {
        if (!File.Exists(filePath))
            SQLiteConnection.CreateFile(filePath);

        string createSqlPath = Path.Combine(directory, "BloggingPlatform.sql");
        string creationQuery = File.ReadAllText(createSqlPath);

        using SqliteConnection connection = new SqliteConnection(_connectionString);

        connection.Open();

        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = creationQuery;
        command.ExecuteNonQuery();
    }
    #endregion
}
