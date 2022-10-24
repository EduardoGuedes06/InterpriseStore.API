using Microsoft.EntityFrameworkCore;
using Business.Interfaces;
using Business.Models;
using Data.context;

namespace Data.repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MeuDbContext context) : base(context)
        {
        }

        public async Task<Categoria> ObterCategoriaProduto(Guid id)
        {
            return await Db.Categorias.AsNoTracking()
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categoria> ObterSomenteCategoria(Guid id)
        {
            return await Db.Categorias.AsNoTracking()
                //.Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
