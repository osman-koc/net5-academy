using AutoMapper;
using MongoDB.Driver;
using NET5Academy.Services.Catalog.Data.Entities;
using NET5Academy.Services.Catalog.Dtos;
using NET5Academy.Services.Catalog.Settings;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Services
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

        public async Task<OkResponse<CategoryDto>> CreateAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return OkResponse<CategoryDto>.Error(HttpStatusCode.BadRequest, "Name cannot be empty!");
            }

            var category = new Category { Name = name };
            await _categoryCollection.InsertOneAsync(category);
            
            var mapDto = _mapper.Map<CategoryDto>(category);
            return OkResponse<CategoryDto>.Success(HttpStatusCode.OK, mapDto);
        }
    }
}
