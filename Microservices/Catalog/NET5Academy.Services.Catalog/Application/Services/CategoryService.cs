using AutoMapper;
using MongoDB.Driver;
using NET5Academy.Services.Catalog.Data.Entities;
using NET5Academy.Services.Catalog.Application.Dtos;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NET5Academy.Shared.Config;

namespace NET5Academy.Services.Catalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IMongoSettings mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(mongoSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<OkResponse<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(x => true).ToListAsync();
            var mapDtos = _mapper.Map<List<CategoryDto>>(categories);
            return OkResponse<List<CategoryDto>>.Success(HttpStatusCode.OK, mapDtos);
        }

        public async Task<OkResponse<CategoryDto>> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return OkResponse<CategoryDto>.Error(HttpStatusCode.BadRequest, "Id cannot be empty!");
            }

            var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if(category == null)
            {
                return OkResponse<CategoryDto>.Error(HttpStatusCode.NotFound, "Category not found!");
            }

            var mapDto = _mapper.Map<CategoryDto>(category);
            return OkResponse<CategoryDto>.Success(HttpStatusCode.OK, mapDto);
        }

        public async Task<OkResponse<CategoryDto>> CreateAsync(CategoryCreateDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Name))
            {
                return OkResponse<CategoryDto>.Error(HttpStatusCode.BadRequest, "Model is not valid.");
            }

            var category = _mapper.Map<Category>(dto);
            await _categoryCollection.InsertOneAsync(category);
            
            var mapDto = _mapper.Map<CategoryDto>(category);
            return OkResponse<CategoryDto>.Success(HttpStatusCode.OK, mapDto);
        }

        public async Task<OkResponse<CategoryDto>> UpdateAsync(CategoryUpdateDto dto)
        {

            if (dto == null || string.IsNullOrEmpty(dto.Id) || string.IsNullOrEmpty(dto.Name))
                return OkResponse<CategoryDto>.Error(HttpStatusCode.BadRequest, "Model is not valid!");

            var updateCategory = _mapper.Map<Category>(dto);
            var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.Id == updateCategory.Id, updateCategory);
            if (result == null)
            {
                return OkResponse<CategoryDto>.Error(HttpStatusCode.NotFound, "Category is not found.");
            }

            var mapDto = _mapper.Map<CategoryDto>(result);
            return OkResponse<CategoryDto>.Success(HttpStatusCode.OK, mapDto);
        }

        public async Task<OkResponse<object>> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return OkResponse<object>.Error(HttpStatusCode.BadRequest, "Id cannot be empty!");

            var result = await _categoryCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return OkResponse<object>.Success(HttpStatusCode.NoContent);
            }
            else
            {
                return OkResponse<object>.Error(HttpStatusCode.NotFound, "Category is not found.");
            }
        }
    }
}
