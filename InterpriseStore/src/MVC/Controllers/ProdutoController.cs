using AutoMapper;
using Business.Interfaces;
using Business.Models;
using InterpriseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class ProdutoController : BaseController
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;

    public ProdutoController(IProdutoRepository produtoRepository,
        IProdutoService produtoService,
        IMapper mapper,
        INotificador notificador) : base(notificador)
    {
        _produtoRepository = produtoRepository;
        _produtoService = produtoService;
        _mapper = mapper;
    }
    //[AllowAnonymous]
    [Route("lista-de-produtos")]
    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos()));
    }

    //[AllowAnonymous]
    [Route("dados-do-produto/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var produtoViewModel = await ObterProdutoPorId(id);

        if (produtoViewModel == null)
        {
            return NotFound();
        }

        return View(produtoViewModel);
    }

    [Route("novo-produto")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("novo-produto")]
    [HttpPost]
    public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
    {
        if (!ModelState.IsValid) return View(produtoViewModel);

        var produto = _mapper.Map<Produto>(produtoViewModel);
        await _produtoService.Adicionar(produto);

        if (!OperacaoValida()) return View(produtoViewModel);

        return RedirectToAction("Index");
    }

    [Route("editar-produto/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var produtoViewModel = await ObterProdutoPorId(id);

        if (produtoViewModel == null)
        {
            return NotFound();
        }

        return View(produtoViewModel);
    }

    [Route("editar-produto/{id:guid}")]
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
    {
        if (id != produtoViewModel.Id) return NotFound();

        if (!ModelState.IsValid) return View(produtoViewModel);

        var produto = _mapper.Map<Produto>(await ObterProdutoPorId(id));
        await _produtoService.Adicionar(produto);

        if (!OperacaoValida()) return View(await ObterProdutoPorId(id));

        return RedirectToAction("Index");
    }


    [Route("excluir-produto/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var produtoViewModel = await ObterProdutoPorId(id);

        if (produtoViewModel == null)
        {
            return NotFound();
        }
        await _produtoService.Remover(id);

        return View(produtoViewModel);

    }

    [Route("excluir-produto/{id:guid}")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var produto = await ObterProdutoPorId(id);

        if (produto == null) return NotFound();

        await _produtoService.Remover(id);

        if (!OperacaoValida()) return View(produto);

        return RedirectToAction("Index");
    }



    private async Task<ProdutoViewModel> ObterProdutoPorId(Guid id)
    {
        return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
    }
}