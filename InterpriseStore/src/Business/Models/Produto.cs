using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{

    public class Produto : Entity
    {
        public Produto()
        {
            DataCadastro = DateTime.Now;
        }


        public Guid CategoriaId { get; set; }

        public string Descricao { get; set; }

        public int Preco { get; set; }

        public DateTime DataCadastro { get; set; }

        
        public bool Ativo { get; set; }

        /* EF Relations */
        public Categoria Categoria { get; set; }

    }
}
