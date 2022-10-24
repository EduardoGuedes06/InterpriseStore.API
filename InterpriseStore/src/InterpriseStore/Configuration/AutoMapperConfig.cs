using AutoMapper;
using InterpriseStore.ViewModels;
using Business.Models;

namespace InterpriseStore.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<ProdutoViewModel, Produto>();

            

            
        }
    }
}
