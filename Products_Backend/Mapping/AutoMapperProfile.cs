using AutoMapper;
using ProductService.DTOs;
using ProductService.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductService.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
            // both now include ImageUrl automatically

        }
    }
}