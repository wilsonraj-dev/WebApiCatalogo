using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorPreco();
    IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
}
