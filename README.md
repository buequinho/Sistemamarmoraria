# Sistemamarmoregranito
#Colaboradores
  - Fábio Henrique dos Santos Buecker
  - André muruci
  - João Pedro Maroquio
  - Carlos Petersen
  - Pedro sana
  - Davi Silva
  
1. Introdução
Este documento descreve o desenvolvimento de um sistema voltado para empresas do ramo de mármore e granito, com o objetivo de facilitar o controle de blocos, chapas e processos de serragem.
2. Processo de Desenvolvimento
Foi adotado um processo de desenvolvimento ágil, baseado em Scrum, com foco em entregas incrementais semanais. Isso garante maior controle e adaptabilidade às necessidades do cliente.

3. Tecnologias Utilizadas
- .NET 8
- C#
- SQL Server
- Entity Framework Core
- GitHub
Funcionamento e explicação do código
Documentação das Classes de Modelo
1. Classe Usuario
Descrição:
Representa um usuário do sistema, contendo suas informações de login.
Atributos:
Id (int):
Identificador único do usuário no banco de dados. É gerado automaticamente pelo banco (auto-incremento).


UsuarioNome (string):
Nome de usuário utilizado para login e identificação do sistema.


Senha (string):
Senha do usuário. Para sistemas reais, recomenda-se que a senha seja armazenada de forma segura (hashing), porém, neste modelo está como string simples.



2. Classe Bloco
Descrição:
Representa um bloco de material de mármore ou granito cadastrado na loja, com suas características e informações de origem.
Atributos:
Id (int):
Identificador único do bloco no banco de dados. É gerado automaticamente pelo banco (auto-incremento).


Codigo (string):
Código de identificação do bloco, utilizado para referência rápida.


PedreiraOrigem (string):
Nome ou identificação da pedreira de onde o bloco foi adquirido.


MedidaM3 (double):
Medida do bloco em metros cúbicos (m³), indicando seu volume total.


TipoMaterial (string):
Tipo de material do bloco, por exemplo, mármore, granito, etc.


ValorCompra (decimal):
Valor de compra do bloco, representando quanto foi pago na aquisição.


NotaFiscal (string):
Número ou identificação da nota fiscal de entrada do bloco.



3. Classe Chapa
Descrição:
Representa uma chapa de material extraída de um bloco, cadastrada no estoque da loja.
Atributos:
Id (int):
Identificador único da chapa no banco de dados. É gerado automaticamente pelo banco (auto-incremento).


BlocoId (int):
Referência (chave estrangeira) ao Id do bloco de origem do qual a chapa foi extraída.


TipoMaterial (string):
Tipo de material da chapa (por exemplo, mármore branco, granito preto).


MedidaLargura (double):
Largura da chapa em centímetros.


MedidaAltura (double):
Altura da chapa em centímetros.


Valor (decimal):
Valor atribuído à chapa, que pode refletir o valor de venda ou custo.


Documentação da Classe Database
Descrição:
A classe Database é responsável por gerenciar a conexão com o banco de dados SQL Server, facilitando a abertura de conexões sempre que necessário.
Funcionamento:
Possui uma propriedade connectionString que armazena a string de conexão ao banco de dados. Essa string deve ser configurada com os detalhes do servidor, banco de dados, autenticação, etc.
O método GetConnection() retorna uma nova instância de SqlConnection, conectando-se ao banco de dados usando a connectionString fornecida.


Documentação da Classe UsuarioRepository
Descrição:
A classe UsuarioRepository é responsável pelo acesso e manipulação dos dados relacionados aos usuários no banco de dados. Ela encapsula as operações de leitura e escrita na tabela de usuários.
Principais métodos:
GetUsuarioAsync(string usuarioNome)


Objetivo: Buscar um usuário pelo nome de usuário no banco de dados.
Funcionamento: Executa uma consulta SQL que retorna o usuário correspondente ao nome fornecido. Se não encontrar, retorna null.
Retorno: Uma instância do objeto Usuario ou null, de forma assíncrona.
AddUsuarioAsync(Usuario usuario)


Objetivo: Inserir um novo usuário na tabela do banco.
Funcionamento: Executa um comando SQL de insert usando os dados do objeto Usuario passado como parâmetro.
Retorno: Uma tarefa assíncrona, indicando a conclusão da operação.


Documentação da Classe EstoqueRepository
Descrição:
A classe EstoqueRepository gerencia o acesso aos dados de estoque, ou seja, os blocos e chapas armazenados na base de dados. Ela fornece métodos para listar e cadastrar esses itens.
Principais métodos:
ListarBlocosAsync()


Objetivo: Buscar toda a lista de blocos cadastrados.
Funcionamento: Executa uma consulta SQL que recupera todos os registros da tabela Blocos.
Retorno: Uma lista assíncrona de objetos Bloco.
ListarChapasAsync()


Objetivo: Buscar toda a lista de chapas cadastradas.
Funcionamento: Executa uma consulta SQL que recupera todos os registros da tabela Chapas.
Retorno: Uma lista assíncrona de objetos Chapa.
CadastrarBlocoAsync(Bloco bloco)


Objetivo: Inserir um novo bloco no banco de dados.
Funcionamento: Executa um comando SQL de insert, usando os dados do objeto Bloco fornecido.
Retorno: Uma tarefa assíncrona indicando quando a operação foi concluída.
CadastrarChapaAsync(Chapa chapa)


Objetivo: Inserir uma nova chapa no banco de dados.
Funcionamento: Executa um comando SQL de insert, usando os dados do objeto Chapa.
Retorno: Uma tarefa assíncrona indicando quando a operação foi concluída.


Documentação das Classes de Serviço
1. Classe UsuarioService
Descrição:
Encapsula a lógica de negócio relacionada aos usuários do sistema, utilizando o repositório de usuários para operações de validação e cadastro.
Principais métodos:
ValidarLoginAsync(string usuario, string senha)


Objetivo: Verificar se o usuário e senha fornecidos são válidos.
Funcionamento: Busca o usuário pelo nome e compara a senha. Retorna true se a combinação estiver correta, caso contrário, false.
Tipo de retorno: Task<bool> (assincrono).
CadastrarUsuarioAsync(Usuario usuario)


Objetivo: Realizar o cadastro de um novo usuário.
Funcionamento: Chama o repositório para inserir o usuário no banco.
Tipo de retorno: Task.

2. Classe MarmoreService
Descrição:
Gerencia as operações relacionadas ao estoque de blocos e chapas, incluindo listagem, cadastro e processamento de serragem (transformação de blocos em chapas).
Principais métodos:
ListarBlocosAsync()


Objetivo: Exibir todos os blocos cadastrados.
Funcionamento: Recupera a lista de blocos do repositório e imprime suas informações.
ListarChapasAsync()


Objetivo: Exibir todas as chapas cadastradas.
Funcionamento: Recupera a lista de chapas e imprime seus detalhes.
ProcessarSerragemAsync(int blocoId, double medidaLargura, double medidaAltura, decimal valor)


Objetivo: Converter um bloco em uma chapa, gerando uma nova chapa no estoque.
Funcionamento: Cria uma chapa com os dados fornecidos e cadastra no banco via repositório, informando que a chapa foi criada a partir do bloco.
CadastrarBlocoAsync(Bloco bloco)


Objetivo: Cadastrar um novo bloco no estoque.
Funcionamento: Chama o repositório para inserir o bloco.
CadastrarChapaAsync(Chapa chapa)


Objetivo: Cadastrar uma nova chapa no estoque.
Funcionamento: Chama o repositório para inserir a chapa.
