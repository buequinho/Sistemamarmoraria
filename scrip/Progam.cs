using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "sua-string-de-conexao"; // Substitua pela sua conexão SQL Server
        var database = new Database(connectionString);
        var usuarioRepo = new UsuarioRepository(database);
        var estoqueRepo = new EstoqueRepository(database);
        var usuarioService = new UsuarioService(usuarioRepo);
        var marmoreService = new MarmoreService(estoqueRepo);

        Console.WriteLine("=== Sistema de Marmoraria ===");

        // Autenticação
        bool autenticado = false;
        for (int tentativas = 0; tentativas < 3 && !autenticado; tentativas++)
        {
            Console.Write("Usuário: ");
            string usuario = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            if (await usuarioService.ValidarLoginAsync(usuario, senha))
            {
                autenticado = true;
                Console.WriteLine("Login bem-sucedido!");
            }
            else
            {
                Console.WriteLine("Usuário ou senha incorretos. Tente novamente.");
            }
        }

        if (!autenticado)
        {
            Console.WriteLine("Número de tentativas excedido. Encerrando o sistema.");
            return;
        }

        // Menu principal
        int opcao;
        do
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1 - Cadastrar Usuário");
            Console.WriteLine("2 - Cadastrar Bloco");
            Console.WriteLine("3 - Cadastrar Chapa");
            Console.WriteLine("4 - Processar Serragem");
            Console.WriteLine("5 - Listar Blocos");
            Console.WriteLine("6 - Listar Chapas");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    await CadastrarUsuarioAsync(usuarioService);
                    break;
                case 2:
                    await CadastrarBlocoAsync(marmoreService);
                    break;
                case 3:
                    await CadastrarChapaAsync(marmoreService);
                    break;
                case 4:
                    await ProcessarSerragemAsync(marmoreService);
                    break;
                case 5:
                    await marmoreService.ListarBlocosAsync();
                    break;
                case 6:
                    await marmoreService.ListarChapasAsync();
                    break;
                case 0:
                    Console.WriteLine("Encerrando o sistema. Até logo!");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

        } while (opcao != 0);
    }

    private static async Task CadastrarUsuarioAsync(UsuarioService usuarioService)
    {
        Console.Write("Nome de usuário: ");
        string usuarioNome = Console.ReadLine();
        Console.Write("Senha: ");
        string senha = Console.ReadLine();

        var novoUsuario = new Usuario { UsuarioNome = usuarioNome, Senha = senha };
        await usuarioService.CadastrarUsuarioAsync(novoUsuario);
        Console.WriteLine("Usuário cadastrado com sucesso!");
    }

    private static async Task CadastrarBlocoAsync(MarmoreService marmoreService)
    {
        Console.Write("Código do bloco: ");
        string codigo = Console.ReadLine();
        Console.Write("Pedreira de origem: ");
        string origem = Console.ReadLine();
        Console.Write("Medida em M3: ");
        double medidaM3 = double.Parse(Console.ReadLine());
        Console.Write("Tipo de material: ");
        string tipoMaterial = Console.ReadLine();
        Console.Write("Valor de compra: ");
        decimal valorCompra = decimal.Parse(Console.ReadLine());
        Console.Write("Número da nota fiscal: ");
        string notaFiscal = Console.ReadLine();

        var novoBloco = new Bloco
        {
            Codigo = codigo,
            PedreiraOrigem = origem,
            MedidaM3 = medidaM3,
            TipoMaterial = tipoMaterial,
            ValorCompra = valorCompra,
            NotaFiscal = notaFiscal
        };

        await marmoreService.CadastrarBlocoAsync(novoBloco);
        Console.WriteLine("Bloco cadastrado com sucesso!");
    }

    private static async Task CadastrarChapaAsync(MarmoreService marmoreService)
    {
        Console.Write("ID do bloco de origem (ou digite o código do bloco): ");
        int blocoId;
        while (!int.TryParse(Console.ReadLine(), out blocoId))
        {
            Console.Write("Entrada inválida. Digite o ID do bloco (número): ");
        }

        Console.Write("Tipo de material: ");
        string tipoMaterial = Console.ReadLine();
        Console.Write("Medida de Largura (cm): ");
        double medidaLargura = double.Parse(Console.ReadLine());
        Console.Write("Medida de Altura (cm): ");
        double medidaAltura = double.Parse(Console.ReadLine());
        Console.Write("Valor: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        var novaChapa = new Chapa
        {
            BlocoId = blocoId,
            TipoMaterial = tipoMaterial,
            MedidaLargura = medidaLargura,
            MedidaAltura = medidaAltura,
            Valor = valor
        };

        await marmoreService.CadastrarChapaAsync(novaChapa);
        Console.WriteLine("Chapa cadastrada com sucesso!");
    }

    private static async Task ProcessarSerragemAsync(MarmoreService marmoreService)
    {
        Console.Write("ID do bloco a ser processado: ");
        int blocoId;
        while (!int.TryParse(Console.ReadLine(), out blocoId))
        {
            Console.Write("Entrada inválida. Digite o ID do bloco: ");
        }

        Console.Write("Largura da chapa (cm): ");
        double medidaLargura = double.Parse(Console.ReadLine());
        Console.Write("Altura da chapa (cm): ");
        double medidaAltura = double.Parse(Console.ReadLine());
        Console.Write("Valor da chapa: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        await marmoreService.ProcessarSerragemAsync(blocoId, medidaLargura, medidaAltura, valor);
        Console.WriteLine("Processo de serragem concluído.");
    }
}
