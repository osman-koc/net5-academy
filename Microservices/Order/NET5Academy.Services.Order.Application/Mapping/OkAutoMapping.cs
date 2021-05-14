using AutoMapper;
using NET5Academy.Services.Order.Application.Dtos;

namespace NET5Academy.Services.Order.Application.Mapping
{
    public class OkAutoMapping : Profile
    {
        public OkAutoMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.Address, AddressDto>().ReverseMap();
        }
    }
}
