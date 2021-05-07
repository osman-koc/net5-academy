using AutoMapper;
using MongoDB.Driver;
using NET5Academy.Services.Catalog.Data.Entities;
using NET5Academy.Services.Catalog.Dtos;
using NET5Academy.Services.Catalog.Settings;
using NET5Academy.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Services
{
    public class CourseService : ICourseService
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

        public async Task<OkResponse<List<CourseDto>>> GetAllAsync(string userId = null)
        {
            var courses = await _courseCollection.Find(x => string.IsNullOrEmpty(userId) || x.UserId == userId).ToListAsync();
            if (!courses.Any())
            {
                courses = new List<Course>();
            }
            else
            {
                var categoryIds = courses.Select(x => x.CategoryId).ToList();
                var categories = await _categoryCollection.Find(x => categoryIds.Contains(x.Id)).ToListAsync();
                if (categoryIds.Any())
                {
                    courses.ForEach(item => item.Category = categories.FirstOrDefault(x => x.Id == item.CategoryId));
                }
            }

            var mapDtos = _mapper.Map<List<CourseDto>>(courses);
            return OkResponse<List<CourseDto>>.Success(HttpStatusCode.OK, mapDtos);
        }

        public async Task<OkResponse<CourseDto>> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return OkResponse<CourseDto>.Error(HttpStatusCode.BadRequest, "Id cannot be empty!");

            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
                return OkResponse<CourseDto>.Error(HttpStatusCode.NotFound, "Course not found!");

            if (!string.IsNullOrEmpty(course.CategoryId))
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();

            var mapDto = _mapper.Map<CourseDto>(course);
            return OkResponse<CourseDto>.Success(HttpStatusCode.OK, mapDto);
        }

        public async Task<OkResponse<CourseDto>> CreateAsync(CourseCreateDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Name))
                return OkResponse<CourseDto>.Error(HttpStatusCode.BadRequest, "Model is not valid!");

            var newCourse = _mapper.Map<Course>(dto);
            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);

            var mapDto = _mapper.Map<CourseDto>(newCourse);
            return OkResponse<CourseDto>.Success(HttpStatusCode.OK, mapDto);
        }

        public async Task<OkResponse<CourseDto>> UpdateAsync(CourseUpdateDto dto)
        {

            if (dto == null || string.IsNullOrEmpty(dto.Id) || string.IsNullOrEmpty(dto.Name))
                return OkResponse<CourseDto>.Error(HttpStatusCode.BadRequest, "Model is not valid!");

            var updateCourse = _mapper.Map<Course>(dto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == updateCourse.Id, updateCourse);
            if (result == null)
            {
                return OkResponse<CourseDto>.Error(HttpStatusCode.NotFound, "Course is not found.");
            }

            var mapDto = _mapper.Map<CourseDto>(result);
            return OkResponse<CourseDto>.Success(HttpStatusCode.OK, mapDto);
        }

        public async Task<OkResponse<object>> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return OkResponse<object>.Error(HttpStatusCode.BadRequest, "Id cannot be empty!");

            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            if(result.DeletedCount > 0)
            {
                return OkResponse<object>.Success(HttpStatusCode.NoContent);
            }
            else
            {
                return OkResponse<object>.Error(HttpStatusCode.NotFound, "Course is not found.");
            }
        }
    }
}
