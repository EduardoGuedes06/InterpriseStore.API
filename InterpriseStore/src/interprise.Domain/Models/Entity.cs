using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();

            DataCadastro = DateTime.Now;

        }


        public DateTime DataCadastro { get; set; }
        public Guid Id { get; set; }
    }
}
