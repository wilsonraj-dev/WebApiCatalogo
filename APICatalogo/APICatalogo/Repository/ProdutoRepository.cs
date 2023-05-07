﻿using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext _context) : base(_context) { }

    public IEnumerable<Produto> GetProdutosPorPreco()
    {
        return Get().OrderBy(c => c.Preco).ToList();
    }
}
