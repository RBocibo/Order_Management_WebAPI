using AutoMapper;
using OrderManagement.Contracts.DTO.OrderDTOs;
using OrderManagement.Contracts.DTO.ProductD;
using OrderManagement.Contracts.DTO.StockDTOs;
using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Stock, StockDTO>();
        }
    }
}
