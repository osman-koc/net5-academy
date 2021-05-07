using AutoMapper;
using NET5Academy.Services.Catalog.Data.Entities;
using NET5Academy.Services.Catalog.Dtos;

namespace NET5Academy.Services.Catalog.Mapping
{
    public class OkAutoMapping : Profile
    {
        public OkAutoMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
