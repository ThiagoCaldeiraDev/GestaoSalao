using GestaoSalao.Data.Context;
using GestaoSalao.Models.Intefaces.Data;
using System.Collections;

namespace GestaoSalao.Data.BaseRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Propriedades e construtor

        private readonly ApplicationContext _context;
        private Hashtable _repositories = new Hashtable();

        public UnitOfWork()
        {
            _context = new ApplicationContext();
        }

        #endregion

        public IRepository<T> GetRepository<T>() where T : class
        {
            //if (!_repositories.Contains(typeof(T)))
            //{
            //    _repositories.Add(typeof(T), new Repository<T>(_context));
            //}

            return (IRepository<T>)new Repository<T>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Commit();
        }

        public void RollBackTransaction()
        {
            _context.Rollback();
        }

        public void FinalizeTransaction()
        {
            _context.FinalizeTransaction();
        }

        //private bool disposed = false;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
