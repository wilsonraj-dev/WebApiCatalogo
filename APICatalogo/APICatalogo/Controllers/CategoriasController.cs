using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[ApiConventionType(typeof(DefaultApiConventions))]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public CategoriasController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
    {
        var categorias = await _uof.CategoriaRepository.GetCategorias(categoriasParameters);

        var metaData = new
        {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

        var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

        return categoriasDTO;
    }

    [HttpGet("produtos")]
    [EnableCors("PermitirApiRequest")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
    {
        var categorias = await _uof.CategoriaRepository.GetCategoriasProdutos();
        var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

        return categoriasDTO;
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    [EnableCors("PermitirApiRequest")]
    public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetById(x => x.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada.");
        }

        var categoriaDTO = _mapper.Map<List<CategoriaDTO>>(categoria);

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CategoriaDTO categoriaDto)
    {
        var categoria = _mapper.Map<Categoria>(categoriaDto);

        if (categoriaDto is null)
            return BadRequest("Informações inválidas inseridas para a categoria!");

        _uof.CategoriaRepository.Add(categoria);
        await _uof.Commit();

        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoriaDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            return BadRequest("Id inválido");
        }

        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _uof.CategoriaRepository.Update(categoria);
        await _uof.Commit();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetById(x => x.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada.");
        }

        _uof.CategoriaRepository.Delete(categoria);
        await _uof.Commit();

        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return categoriaDTO;
    }
}
