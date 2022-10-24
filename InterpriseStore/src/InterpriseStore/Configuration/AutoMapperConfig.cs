using AutoMapper;
using InterpriseStore.ViewModels;
using Business.Models;

namespace InterpriseStore.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<CategoriaViewModel, Categoria>().ReverseMap();
            CreateMap<ProdutoViewModel, Produto>().ReverseMap();




        }
    }
}
