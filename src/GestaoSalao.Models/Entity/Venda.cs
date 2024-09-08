using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoSalao.Models.Entity
{
    public class Venda : BaseEntity
    {
        public int? IdServico { get; set; }
        public int? IdProduto { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public int Tipo { get; set; }
        public DateTime DataOperacao { get; set; }
        public int IdVendedor { get; set; }

        [NotMapped]
        public string TipoFormatada => Tipo == 1 ? "Venda" : "Serviço";

        [NotMapped]
        public string? DataOperacaoFormatada => DataOperacao.ToString("dd/MM/yy HH:mm");

        [NotMapped]
        public string? NomeVendedor { get; set; }

        [NotMapped]
        public string? DescricaoOperacao { get; set; }
    }
}
