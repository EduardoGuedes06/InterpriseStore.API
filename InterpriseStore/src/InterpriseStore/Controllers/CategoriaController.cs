using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InterpriseStore.ViewModels;
using Business.Intefaces;
using Business.Interfaces;
using Business.Models;
using InterpriseStore.Controllers;

namespace Revisao.Api.Controllers
{

    [Route("api/categoria")]
    public class CategoriaController : MainController
    {
        private readonly ICategoriaRepository _repository;
        private readonly ICategoriaService _service;
        private IMapper _mapper;
        
        public CategoriaController(ICategoriaRepository categoriaRepository, ICategoriaService categoriaService, 
            IMapper mapper, INotificador notificador, IUser user) : base(notificador, user)
        {
            _repository = categoriaRepository;
            _service = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            var categoria = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _repository.ObterTodos());
            return categoria;
        }


        [HttpGet("{id:guid}")]
        // [Authorize]
        public async Task<ActionResult<ProdutoViewModel>> ObterpPorId(Guid id)
        {
            var categoria = _mapper.Map<ProdutoViewModel>(await _repository.ObterCategoriaProduto(id));

            if (categoria == null) return NotFound();

            return categoria;
        }


        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return CostumResponse(ModelState);


            return CostumResponse(categoriaViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Atualizar(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CostumResponse(categoriaViewModel);
            }

            if (!ModelState.IsValid) return CostumResponse(ModelState);

            await _service.Atualizar(_mapper.Map<Categoria>(categoriaViewModel));

            return CostumResponse(categoriaViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            var categoriaViewModel = _mapper.Map<ProdutoViewModel>(await _repository.ObterCategoriaProduto(id));

            if (categoriaViewModel == null) return NotFound();

            await _repository.Remover(id);

            return CostumResponse(categoriaViewModel);
        }

        




    }
}

