using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext _context) : base(_context) { }

    public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
    {
        return await Get().OrderBy(c => c.Preco).ToListAsync();
    }

    public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
    {
        return await PagedList<Produto>.ToPagedList(Get().OrderBy(p => p.Nome)
                                            , produtosParameters.PageNumber
                                            , produtosParameters.PageSize);
    }
}
