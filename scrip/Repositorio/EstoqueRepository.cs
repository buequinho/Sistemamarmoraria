// Repositories/EstoqueRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;

public class EstoqueRepository
{
    private readonly Database db;
    public EstoqueRepository(Database database) => this.db = database;

    public async Task<List<Bloco>> ListarBlocosAsync()
    {
        var sql = "SELECT * FROM Blocos";
        using var conn = db.GetConnection();
        return await conn.QueryAsync<Bloco>(sql).ToListAsync();
    }

    public async Task<List<Chapa>> ListarChapasAsync()
    {
        var sql = "SELECT * FROM Chapas";
        using var conn = db.GetConnection();
        return await conn.QueryAsync<Chapa>(sql).ToListAsync();
    }

    public async Task CadastrarBlocoAsync(Bloco bloco)
    {
        var sql = @"INSERT INTO Blocos (Codigo, PedreiraOrigem, MedidaM3, TipoMaterial, ValorCompra, NotaFiscal)
                    VALUES (@Codigo, @PedreiraOrigem, @MedidaM3, @TipoMaterial, @ValorCompra, @NotaFiscal)";
        using var conn = db.GetConnection();
        await conn.ExecuteAsync(sql, bloco);
    }

    public async Task CadastrarChapaAsync(Chapa chapa)
    {
        var sql = @"INSERT INTO Chapas (BlocoId, TipoMaterial, MedidaLargura, MedidaAltura, Valor)
                    VALUES (@BlocoId, @TipoMaterial, @MedidaLargura, @MedidaAltura, @Valor)";
        using var conn = db.GetConnection();
        await conn.ExecuteAsync(sql, chapa);
    }
}
