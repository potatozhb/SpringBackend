
using AutoMapper;
using SpringBackend.Dtos;
using SpringBackend.Models;

namespace SpringBackend.Profiles
{
    public class ProductProile : Profile
    {
        public ProductProile()
        {
            //S -> D
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
        }
    }
}