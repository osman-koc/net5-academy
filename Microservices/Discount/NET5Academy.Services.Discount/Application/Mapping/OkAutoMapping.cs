using AutoMapper;
using NET5Academy.Services.Discount.Application.Dtos;
using System.Collections.Generic;

namespace NET5Academy.Services.Discount.Application.Mapping
{
    public class OkAutoMapping : Profile
    {
        public OkAutoMapping()
        {
            CreateMap<Data.Entities.Discount, DiscountDto>().ReverseMap();
            CreateMap<Data.Entities.Discount, DiscountCreateDto>().ReverseMap();
            CreateMap<Data.Entities.Discount, DiscountUpdateDto>().ReverseMap();

            CreateMap<IEnumerable<Data.Entities.Discount>, IEnumerable<DiscountDto>>().ReverseMap();

            CreateMap<DiscountCreateDto, DiscountDto>().ReverseMap();
            CreateMap<DiscountUpdateDto, DiscountDto>().ReverseMap();
        }
    }
}
