using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext _context) : base(_context) { }

    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return Get().Include(x => x.Produtos);
    }
}
