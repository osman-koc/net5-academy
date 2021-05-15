using AutoMapper;
using NET5Academy.Services.Catalog.Data.Entities;
using NET5Academy.Services.Catalog.Application.Dtos;

namespace NET5Academy.Services.Catalog.Application.Mapping
{
    public class OkAutoMapping : Profile
    {
        public OkAutoMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
