using AutoMapper;
using OrdersProcessingService.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersProcessingService.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // API to Domain
            CreateMap<Order, Model.Order>();
            CreateMap<OrdersByUser, Model.OrdersByUser>();

            // Domain to API
            CreateMap<Model.Order, Order>();
            CreateMap<Model.OrdersByUser, OrdersByUser>();
        }    
    }
}
