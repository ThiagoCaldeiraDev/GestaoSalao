namespace GestaoSalao.Models.Entity
{
    public class Servico : BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Tempo { get; set; }
    }
}
