using System.ComponentModel.DataAnnotations;

namespace InterpriseStore.ViewModels
{
    public class CategoriaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public IEnumerable<ProdutoViewModel>? Produtos { get; set; }
    }
}