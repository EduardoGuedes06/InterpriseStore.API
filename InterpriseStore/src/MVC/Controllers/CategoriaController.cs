using AutoMapper;
using Business.Interfaces;
using Business.Models;
using InterpriseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class CategoriaController : BaseController
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly ICategoriaService _categoriaService;
    private readonly IMapper _mapper;

    public CategoriaController(ICategoriaRepository categoriaRepository,
        ICategoriaService categoriaService,
        IMapper mapper,
        INotificador notificador) : base(notificador)
    {
        _categoriaRepository = categoriaRepository;
        _categoriaService = categoriaService;
        _mapper = mapper;
    }
    //[AllowAnonymous]
    [Route("lista-de-categorias")]
    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos()));
    }

    //[AllowAnonymous]
    [Route("dados-da-categoria/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var categoriaViewModel = await ObterCategoriaPorId(id);

        if (categoriaViewModel == null)
        {
            return NotFound();
        }

        return View(categoriaViewModel);
    }

    [Route("nova-categoria")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("nova-categoria")]
    [HttpPost]
    public async Task<IActionResult> Create(CategoriaViewModel categoriaViewModel)
    {
        if (!ModelState.IsValid) return View(categoriaViewModel);

        var categoria = _mapper.Map<Categoria>(categoriaViewModel);
        await _categoriaService.Adicionar(categoria);

        if (!OperacaoValida()) return View(categoriaViewModel);

        return RedirectToAction("Index");
    }

    [Route("editar-categoria/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var categoriaViewModel = await ObterCategoriaPorId(id);

        if(categoriaViewModel == null)
        {
            return NotFound();
        }

        return View(categoriaViewModel);
    }

    [Route("editar-categoria/{id:guid}")]
    [HttpPut]
    public async Task<IActionResult> Edit(Guid id, CategoriaViewModel categoriaViewModel)
    {
        if (id != categoriaViewModel.Id) return NotFound();

        if (!ModelState.IsValid) return View(categoriaViewModel);

        var categoria = _mapper.Map<Categoria>(await ObterCategoriaPorId(id));
        await _categoriaService.Adicionar(categoria);

        if (!OperacaoValida()) return View(await ObterCategoriaPorId(id));

        return RedirectToAction("Index");
    }


    [Route("excluir-categoria/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var categoriaViewModel = await ObterCategoriaPorId(id);

        if (categoriaViewModel == null)
        {
            return NotFound();
        }
        await _categoriaService.Remover(id);

        return View(categoriaViewModel);

    }

    [Route("excluir-categoria/{id:guid}")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var categoria = await ObterCategoriaPorId(id);

        if (categoria == null) return NotFound();

        await _categoriaService.Remover(id);

        if (!OperacaoValida()) return View(categoria);

        return RedirectToAction("Index");
    }



    private async Task<CategoriaViewModel> ObterCategoriaPorId(Guid id)
    {
        return _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorId(id));
    }
}