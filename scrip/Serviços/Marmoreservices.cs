public class MarmoreService
{
    private readonly EstoqueRepository estoqueRepo;

    public MarmoreService(EstoqueRepository repo)
    {
        this.estoqueRepo = repo;
    }

    public async Task ListarBlocosAsync()
    {
        var blocos = await estoqueRepo.ListarBlocosAsync();
        foreach (var bloco in blocos)
        {
            Console.WriteLine($"Código: {bloco.Codigo}, Origem: {bloco.PedreiraOrigem}, Medida: {bloco.MedidaM3} m³, Material: {bloco.TipoMaterial}");
        }
    }

    public async Task ListarChapasAsync()
    {
        var chapas = await estoqueRepo.ListarChapasAsync();
        foreach (var chapa in chapas)
        {
            Console.WriteLine($"ID: {chapa.Id}, Bloco ID: {chapa.BlocoId}, Material: {chapa.TipoMaterial}, Medidas: {chapa.MedidaLargura}x{chapa.MedidaAltura}");
        }
    }

    public async Task ProcessarSerragemAsync(int blocoId, double medidaLargura, double medidaAltura, decimal valor)
    {
        var chapa = new Chapa
        {
            BlocoId = blocoId,
            TipoMaterial = "Material do Bloco", // Pode ser melhorado
            MedidaLargura = medidaLargura,
            MedidaAltura = medidaAltura,
            Valor = valor
        };
        await estoqueRepo.CadastrarChapaAsync(chapa);
        Console.WriteLine("Chapa gerada a partir do bloco.");
    }

    public async Task CadastrarBlocoAsync(Bloco bloco)
    {
        await estoqueRepo.CadastrarBlocoAsync(bloco);
    }

    public async Task CadastrarChapaAsync(Chapa chapa)
    {
        await estoqueRepo.CadastrarChapaAsync(chapa);
    }
}
