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
       

        public Guid CategoriaId { get; set; }

        public string Descricao { get; set; }

        public int Preco { get; set; }

        public bool Ativo { get; set; }

        //public DateTime DataCadastro = DateTime.Now;

        /* EF Relations */
        public Categoria Categoria { get; set; }

    }
}
