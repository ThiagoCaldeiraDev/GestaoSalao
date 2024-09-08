using System.Linq.Expressions;

namespace GestaoSalao.Models.Intefaces.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Inserir(TEntity obj);
        void Inserir(IEnumerable<TEntity> entity);

        TEntity ListarPorId(int id);
        ICollection<TEntity> ListarPor(Expression<Func<TEntity, bool>> expressao);
        TEntity ListarPrimeiroPor(Expression<Func<TEntity, bool>> expressao);
        TEntity ListarUltimoPor(Expression<Func<TEntity, bool>> expressao);
        TEntity ListarPrimeiro();
        TEntity ListarUltimo();
        ICollection<TEntity> ListarTodos();

        void Atualizar(TEntity obj);
        void Atualizar(IEnumerable<TEntity> entity);

        void Remove(int id);
        void Remove(IEnumerable<TEntity> entity);
    }
}
