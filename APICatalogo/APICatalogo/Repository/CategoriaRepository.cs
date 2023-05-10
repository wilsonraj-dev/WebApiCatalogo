using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext _context) : base(_context) { }

    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return Get().Include(x => x.Produtos);
    }

    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
    {
        return PagedList<Categoria>.ToPagedList(Get().OrderBy(c => c.Nome)
                                              , categoriasParameters.PageNumber
                                              , categoriasParameters.PageSize);
    }
}
