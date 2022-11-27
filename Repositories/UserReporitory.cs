using Dapper;
using Lesson_DotNet_Dapper.Entities;
using System.Data;

namespace Lesson_DotNet_Dapper_Advanced.Repositories;

internal class UserReporitory
{
    private readonly IDbConnection _dbConnection;

    public UserReporitory(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<User>> GetAsync()
    {
        var queryCommand = @"
            SELECT
                UsuarioID AS UserId,
                Nome AS Name,
                Email AS Email,
                DataCriacao AS CreateDate
            FROM dbo.tblUsuario";

        return await _dbConnection.QueryAsync<User>(queryCommand);
    }

    public async Task<IEnumerable<User>> GetWithStatusAsync()
    {
        var queryCommand = @"
            SELECT
                usuario.UsuarioID AS UserId,
                usuario.Nome AS Name,
                usuario.Email AS Email,
                usuario.DataCriacao AS CreateDate,
                status.UsuarioStatusID AS StatusID,
                status.Nome AS Name,
                status.DataCriacao AS CreateDate
            FROM dbo.tblUsuario usuario
            INNER JOIN dbo.tblUsuarioStatus status ON
                status.UsuarioStatusID = usuario.UsuarioStatusID";

        return await _dbConnection.QueryAsync<User, Status, User>(
            queryCommand,
            GetUserWithStatus,
            splitOn: "StatusID");
    }

    private User GetUserWithStatus(User user, Status status)
    {
        return new User()
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            CreateDate = user.CreateDate,
            Status = new Status()
            {
                StatusId = status.StatusId,
                Name = status.Name,
                CreateDate = status.CreateDate,
            }
        };
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        var queryCommand = @"
            SELECT
                UsuarioID AS UserId,
                Nome AS Name,
                Email AS Email,
                DataCriacao AS CreateDate
            FROM dbo.tblUsuario
            WHERE
                UsuarioID = @UserId";

        return await _dbConnection.QueryFirstOrDefaultAsync<User?>(
            queryCommand,
            new
            {
                userId
            });
    }

    public async Task<User?> GetByIdWithStatusAsync(int userId)
    {
        var queryCommand = @"
            SELECT
                usuario.UsuarioID AS UserId,
                usuario.Nome AS Name,
                usuario.Email AS Email,
                usuario.DataCriacao AS CreateDate,
                status.UsuarioStatusID AS StatusID,
                status.Nome AS Name,
                status.DataCriacao AS CreateDate
            FROM dbo.tblUsuario usuario
            INNER JOIN dbo.tblUsuarioStatus status ON
                status.UsuarioStatusID = usuario.UsuarioStatusID
            WHERE
                usuario.UsuarioID = @UserId";

        var data = await _dbConnection.QueryAsync<User, Status, User>(
            queryCommand,
            (user, status) => new User()
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                CreateDate = user.CreateDate,
                Status = new Status()
                {
                    StatusId = status.StatusId,
                    Name = status.Name,
                    CreateDate = status.CreateDate,
                }
            },
            new
            {
                userId
            }, 
            splitOn: "StatusID");

        return data.FirstOrDefault();
    }

    public async Task Insert(User user)
    {
        var queryCommand = @"
            INSERT INTO tblUsuario(Nome, UsuarioStatusId, Email)
            VALUES(@Name, 1, @Email";

        await _dbConnection.ExecuteAsync(
            queryCommand,
            user);
    }

    public async Task Insert(IEnumerable<User> users)
    {
        var queryCommand = @"
            INSERT INTO tblUsuario(Nome, UsuarioStatusId, Email)
            VALUES(@Name, 1, @Email)";

        await _dbConnection.ExecuteAsync(
            queryCommand, 
            users);
    }

    public async Task InsertPipelined(IEnumerable<User> users)
    {
        var queryCommand = @"
            INSERT INTO tblUsuario(Nome, UsuarioStatusId, Email)
            VALUES(@Name, 1, @Email)"
        ;

        var commandDefinition = new CommandDefinition(
            queryCommand,
            users,
            transaction: null,
            commandTimeout: null,
            CommandType.Text,
            CommandFlags.Pipelined,
            default);

        await _dbConnection.ExecuteAsync(commandDefinition);
    }
}
