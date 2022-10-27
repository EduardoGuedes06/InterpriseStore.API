using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InterpriseStore.ViewModels;
using Business.Intefaces;
using Business.Interfaces;
using Business.Models;
using InterpriseStore.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace InterpriseStore.V1.Controllers
{

    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categorias")]
    public class CategoriaController : MainController
    {
        private readonly ICategoriaRepository _Categoriarepository;
        private readonly ICategoriaService _Categoriaservice;
        private IMapper _mapper;

        public CategoriaController(ICategoriaRepository categoriaRepository, ICategoriaService categoriaService,
            IMapper mapper, INotificador notificador, IUser user) : base(notificador, user)
        {
            _Categoriarepository = categoriaRepository;
            _Categoriaservice = categoriaService;
            _mapper = mapper;
        }

       // [Authorize]
        [HttpGet]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            var categoria = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _Categoriarepository.ObterTodos());
            return categoria;
        }


        [HttpGet("{id:guid}")]
        //[Authorize]
        public async Task<ActionResult<CategoriaViewModel>> ObterPorId(Guid id)
        {
            var categoria = _mapper.Map<CategoriaViewModel>(await _Categoriarepository.ObterCategoriaProduto(id));

            if (categoria == null) return NotFound();

            await _Categoriarepository.ObterPorId(id);

            return CostumResponse(categoria);
        }


       
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return CostumResponse(ModelState);

            await _Categoriaservice.Adicionar(_mapper.Map<Categoria>(categoriaViewModel));

            return CostumResponse(categoriaViewModel);
        }

        
        [HttpPut("{id:guid}")]
        //[Authorize]
        public async Task<ActionResult<ProdutoViewModel>> Atualizar(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CostumResponse(categoriaViewModel);
            }

            if (!ModelState.IsValid) return CostumResponse(ModelState);

            await _Categoriaservice.Atualizar(_mapper.Map<Categoria>(categoriaViewModel));

            return CostumResponse(categoriaViewModel);
        }

       
        [HttpDelete("{id:guid}")]
        //[Authorize]
        public async Task<ActionResult<CategoriaViewModel>> Excluir(Guid id)
        {
            var categoriaViewModel = _mapper.Map<CategoriaViewModel>(await _Categoriarepository.ObterCategoriaProduto(id));

            if (categoriaViewModel == null) return NotFound();

            await _Categoriaservice.Remover(id);

            return CostumResponse(categoriaViewModel);
        }

    }
}

