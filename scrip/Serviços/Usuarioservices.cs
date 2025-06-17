public class UsuarioService
{
    private readonly UsuarioRepository usuarioRepo;

    public UsuarioService(UsuarioRepository repo)
    {
        this.usuarioRepo = repo;
    }

    public async Task<bool> ValidarLoginAsync(string usuario, string senha)
    {
        var user = await usuarioRepo.GetUsuarioAsync(usuario);
        return user != null && user.Senha == senha;
    }

    public async Task CadastrarUsuarioAsync(Usuario usuario)
    {
        await usuarioRepo.AddUsuarioAsync(usuario);
    }
}
