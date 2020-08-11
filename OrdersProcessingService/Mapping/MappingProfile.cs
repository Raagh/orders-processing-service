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
            // Domain to Resource
            CreateMap<Order, Model.Order>();
            CreateMap<OrdersByUser, Model.OrdersByUser>();

            // Resource to Domain
            CreateMap<Model.Order, Order>();
            CreateMap<Model.OrdersByUser, OrdersByUser>();
        }    
    }
}
