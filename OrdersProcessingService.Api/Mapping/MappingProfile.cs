using AutoMapper;
using OrdersProcessingService.Core.Domain;

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
