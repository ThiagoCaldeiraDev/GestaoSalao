using GestaoSalao.Models.Entity;
using GestaoSalao.Models.Intefaces;
using GestaoSalao.Models.Intefaces.Data;

namespace GestaoSalao.Services.Services
{
    public class UsuarioServices : BaseServices<Usuario>, IUsuarioServices
    {
        #region Propriedades e Construtor


        public UsuarioServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        public Usuario ExisteUsuario(string cpf, string senha)
        {
            return _repository.ListarPrimeiroPor(x => x.Cpf == cpf && x.Senha == senha);
        }

        #region CRUD

        public string Inserir(Usuario dados)
        {
            var validacao = Validar(dados);
            if (!string.IsNullOrEmpty(validacao))
            {
                return validacao;
            }

            try
            {
                // Insere o cenário primeiro
                _unitOfWork.BeginTransaction();

                _repository.Inserir(dados);

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                return $"Erro ao inserir, segue a exceção: {ex.Message}";
            }

            return string.Empty;
        }

        public string Atualizar(Usuario dados)
        {
            var validacao = Validar(dados, false);
            if (!string.IsNullOrEmpty(validacao))
            {
                return validacao;
            }

            try
            {
               
                // Atualiza
                _unitOfWork.BeginTransaction();

                _repository.Atualizar(dados);

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                return $"Erro ao atualizar, segue a exceção: {ex.Message}";
            }

            return string.Empty;
        }

        public string Excluir(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                _repository.Remove(id);

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                return $"Erro ao excluir, segue a exceção: {ex.Message}";
            }

            return string.Empty;
        }

        private string Validar(Usuario dados, bool insert = true)
        {
            var dadosDataIgual = insert 
                ? _repository.ListarPrimeiroPor(x => x.Cpf == dados.Cpf) 
                : _repository.ListarPrimeiroPor(x => x.Cpf == dados.Cpf && x.Id != dados.Id);

            return dadosDataIgual != null 
                ? "Existe um usuário com o mesmo CPF" : string.Empty;
        }

        #endregion
    }
}
