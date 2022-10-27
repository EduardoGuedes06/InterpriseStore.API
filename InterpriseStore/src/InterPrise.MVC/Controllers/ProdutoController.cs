using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.repository;
using InterpriseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Interprise.MVC.Controllers
{

    public class ProdutoController : BaseController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository,
            IProdutoService produtoService,
            ICategoriaRepository categoriaRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
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
        public async Task<IActionResult> Create()
        {
            var produtoViewModel = await PopularCategoria(new ProdutoViewModel());
            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularCategoria(produtoViewModel);
            if (!ModelState.IsValid) return View(produtoViewModel);

            var produto = _mapper.Map<Produto>(produtoViewModel);
            await _produtoService.Adicionar(produto);



            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await PopularCategoria(new ProdutoViewModel());


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
            produtoViewModel = await PopularCategoria(produtoViewModel);

            if (!ModelState.IsValid) return View(produtoViewModel);

            var produto = _mapper.Map<Produto>(produtoViewModel);
            await _produtoService.Atualizar(produto);



            if (!OperacaoValida()) return View(produtoViewModel);

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


        private async Task<ProdutoViewModel> PopularCategoria(ProdutoViewModel produto)
        {
            produto.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return produto;
        }
    }
}