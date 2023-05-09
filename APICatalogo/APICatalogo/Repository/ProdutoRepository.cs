using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext _context) : base(_context) { }

    public IEnumerable<Produto> GetProdutosPorPreco()
    {
        return Get().OrderBy(c => c.Preco).ToList();
    }

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        return PagedList<Produto>.ToPagedList(Get().OrderBy(p => p.Nome)
                                            , produtosParameters.PageNumber
                                            , produtosParameters.PageSize);
    }
}
