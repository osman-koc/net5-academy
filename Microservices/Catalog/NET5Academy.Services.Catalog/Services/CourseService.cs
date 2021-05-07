using AutoMapper;
using MongoDB.Driver;
using NET5Academy.Services.Catalog.Data.Entities;
using NET5Academy.Services.Catalog.Dtos;
using NET5Academy.Services.Catalog.Settings;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Services
{
    internal class CourseService : ICourseService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IMongoSettings mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(mongoSettings.CategoryCollectionName);
            _courseCollection = database.GetCollection<Course>(mongoSettings.CourseCollectionName);
            _mapper = mapper;
        }

        public async Task<OkResponse<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(x => true).ToListAsync();
            if (courses.Any())
            {
                var categoryIds = courses.Select(x => x.CategoryId).ToList();
                var categories = await _categoryCollection.Find(x => categoryIds.Contains(x.Id)).ToListAsync();
                if (categoryIds.Any())
                {
                    courses.ForEach(item => item.Category = categories.FirstOrDefault(x => x.Id == item.CategoryId));
                }
            }
            else
            {
                courses = new List<Course>();
            }

            var mapDtos = _mapper.Map<List<CourseDto>>(courses);
            return OkResponse<List<CourseDto>>.Success((int)HttpStatusCode.OK, mapDtos);
        }

        public async Task<OkResponse<CourseDto>> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return OkResponse<CourseDto>.Error((int)HttpStatusCode.BadRequest, "Id cannot be empty!");
            }

            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return OkResponse<CourseDto>.Error((int)HttpStatusCode.NotFound, "Course not found!");
            }

            if (!string.IsNullOrEmpty(course.CategoryId))
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            }

            var mapDto = _mapper.Map<CourseDto>(course);
            return OkResponse<CourseDto>.Success((int)HttpStatusCode.OK, mapDto);
        }
    }
}
