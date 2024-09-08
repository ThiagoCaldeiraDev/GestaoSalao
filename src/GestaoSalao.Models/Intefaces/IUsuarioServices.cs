using GestaoSalao.Models.Entity;

namespace GestaoSalao.Models.Intefaces
{
    public interface IUsuarioServices : IBaseServices<Usuario>
    {
        Usuario ExisteUsuario(string cpf, string senha);
        string Inserir(Usuario dados);
        string Atualizar(Usuario dados);
        string Excluir(int id);
    }
}
