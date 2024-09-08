using GestaoSalao.Data.Context;
using GestaoSalao.Models.Intefaces.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoSalao.Data.BaseRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Propriedades e Construtor

        protected readonly ApplicationContext _contexto;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationContext contexto)
        {
            _contexto = contexto;
            DbSet = _contexto.Set<TEntity>();
        }

        #endregion

        public virtual void Inserir(TEntity obj)
        {
            DbSet.Add(obj);
            _contexto.SaveChanges();
        }

        public virtual void Inserir(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
            _contexto.SaveChanges();

        }

        public virtual TEntity ListarPorId(int id)
        {
            var dados = DbSet.Find(id);
            if (dados != null)
            {
                // detach
                _contexto.Entry(dados).State = EntityState.Detached;
            }

            return dados;
        }

        public virtual TEntity ListarPrimeiroPor(Expression<Func<TEntity, bool>> expressao)
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(expressao);

            var dados = query.AsNoTracking().ToList().FirstOrDefault();
            if (dados != null)
            {
                // detach
                _contexto.Entry(dados).State = EntityState.Detached;
            }

            return dados;
        }

        public virtual TEntity ListarUltimoPor(Expression<Func<TEntity, bool>> expressao)
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(expressao);

            var dados = query.AsNoTracking().ToList().LastOrDefault();
            if (dados != null)
            {
                // detach
                _contexto.Entry(dados).State = EntityState.Detached;
            }

            return dados;
        }

        public virtual ICollection<TEntity> ListarPor(Expression<Func<TEntity, bool>> expressao)
        {
            IQueryable<TEntity> query = DbSet;

            query = query.AsNoTracking().Where(expressao);

            return query.ToList();
        }

        public virtual TEntity ListarPrimeiro()
        {
            var dados = DbSet.AsNoTracking().ToList().FirstOrDefault();
            if (dados != null)
            {
                // detach
                _contexto.Entry(dados).State = EntityState.Detached;
            }

            return dados;
        }

        public virtual TEntity ListarUltimo()
        {
            var dados = DbSet.ToList().LastOrDefault();
            if (dados != null)
            {
                // detach
                _contexto.Entry(dados).State = EntityState.Detached;
            }

            return dados;
        }

        public virtual ICollection<TEntity> ListarTodos()
        {
            return DbSet.ToList();
        }

        public virtual void Atualizar(TEntity obj)
        {
            _contexto.Entry(obj).State = EntityState.Modified;
            DbSet.Update(obj);

            _contexto.SaveChanges();
        }

        public virtual void Atualizar(IEnumerable<TEntity> entity)
        {
            DbSet.UpdateRange(entity);
        }

        public virtual void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));

        }

        public virtual void Remove(IEnumerable<TEntity> entity)
        {
            DbSet.RemoveRange(entity);
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
