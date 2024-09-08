using GestaoSalao.Models.Dtos;
using GestaoSalao.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GestaoSalao.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        #region DbSet's

        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Venda> Venda { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationSettingsDTO.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Vai no dbcontext e busca todas as classes que implementem IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            // Desabilitar o cascate delete
            var relacionamentos = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relacionamento in relacionamentos)
            {
                relacionamento.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

        public IDbContextTransaction _transaction;
        private int _openedTransactions = 0;
        public bool HasTransactions => _openedTransactions > 0;

        public void BeginTransaction()
        {
            _transaction ??= this.Database.BeginTransaction();

            _openedTransactions++;
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            catch
            {
            }
            finally
            {
                FinalizeTransaction();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            FinalizeTransaction();
        }

        public void FinalizeTransaction()
        {
            _transaction?.Dispose();
        }
    }
}
