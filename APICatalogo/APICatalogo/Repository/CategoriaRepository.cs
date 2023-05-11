﻿using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext _context) : base(_context) { }

    public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return await Get().Include(x => x.Produtos).ToListAsync();
    }

    public async Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters)
    {
        return await PagedList<Categoria>.ToPagedList(Get().OrderBy(c => c.Nome)
                                              , categoriasParameters.PageNumber
                                              , categoriasParameters.PageSize);
    }
}
