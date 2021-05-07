using AutoMapper;
using MongoDB.Driver;
using NET5Academy.Services.Catalog.Data.Entities;
using NET5Academy.Services.Catalog.Dtos;
using NET5Academy.Services.Catalog.Settings;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Services
{
    internal class CategoryService
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
            return OkResponse<List<CategoryDto>>.Success(200, mapDtos);
        }
    }
}
