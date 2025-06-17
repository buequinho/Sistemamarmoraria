// Repositories/UsuarioRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

public class UsuarioRepository
{
    private readonly Database db;
    public UsuarioRepository(Database database) => this.db = database;

    public async Task<Usuario> GetUsuarioAsync(string usuarioNome)
    {
        var sql = "SELECT * FROM Usuarios WHERE UsuarioNome = @UsuarioNome";
        using var conn = db.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<Usuario>(sql, new { UsuarioNome = usuarioNome });
    }

    public async Task AddUsuarioAsync(Usuario usuario)
    {
        var sql = "INSERT INTO Usuarios (UsuarioNome, Senha) VALUES (@UsuarioNome, @Senha)";
        using var conn = db.GetConnection();
        await conn.ExecuteAsync(sql, usuario);
    }
}
