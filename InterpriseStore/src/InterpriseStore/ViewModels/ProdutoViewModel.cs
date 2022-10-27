using System.ComponentModel.DataAnnotations;

namespace InterpriseStore.ViewModels
{
    public class ProdutoViewModel
    {
        //public ProdutoViewModel()
        //{
        //    DataCadastro = DateTime.Now;
        //}

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Preco { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        

        public bool Ativo { get; set; }

        public CategoriaViewModel? Categoria { get; set; }

        public IEnumerable<CategoriaViewModel>? Categorias { get; set; }


    }
}
