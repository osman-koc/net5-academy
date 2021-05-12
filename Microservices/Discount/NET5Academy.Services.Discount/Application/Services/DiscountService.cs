using AutoMapper;
using NET5Academy.Services.Discount.Application.Dtos;
using NET5Academy.Services.Discount.Data.Repositories;
using NET5Academy.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NET5Academy.Services.Discount.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<OkResponse<IEnumerable<DiscountDto>>> GetAll()
        {
            var entities = await _discountRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<DiscountDto>>(entities);

            return OkResponse<IEnumerable<DiscountDto>>.Success(HttpStatusCode.OK, dtos);
        }

        public async Task<OkResponse<DiscountDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return OkResponse<DiscountDto>.Error(HttpStatusCode.BadRequest, "Discount id is not valid");
            }

            var entity = await _discountRepository.GetById(id);
            if(entity == null)
            {
                return OkResponse<DiscountDto>.Error(HttpStatusCode.NotFound, "Discount not found!");
            }

            var dto = _mapper.Map<DiscountDto>(entity);
            return OkResponse<DiscountDto>.Success(HttpStatusCode.OK, dto);
        }

        public async Task<OkResponse<DiscountDto>> GetByCodeAndUserId(string code, string userId)
        {
            if(string.IsNullOrEmpty(code) || string.IsNullOrEmpty(userId))
            {
                return OkResponse<DiscountDto>.Error(HttpStatusCode.BadRequest, "Discount code or UserId is not valid.");
            }

            var entity = await _discountRepository.GetByCodeAndUserId(code, userId);
            if(entity == null)
            {
                return OkResponse<DiscountDto>.Error(HttpStatusCode.NotFound, "Discount not found!");
            }

            var dto = _mapper.Map<DiscountDto>(entity);
            return OkResponse<DiscountDto>.Success(HttpStatusCode.OK, dto);
        }

        public async Task<OkResponse<DiscountDto>> Create(DiscountCreateDto dto)
        {
            if(dto == null || string.IsNullOrEmpty(dto.Code) || string.IsNullOrEmpty(dto.UserId) || dto.EndDate < DateTime.Now || dto.StartDate > dto.EndDate)
            {
                return OkResponse<DiscountDto>.Error(HttpStatusCode.BadRequest, "Model is not valid");
            }

            var entity = _mapper.Map<Data.Entities.Discount>(dto);
            var id = await _discountRepository.CreateAndGetId(entity);
            if(id <= 0)
            {
                return OkResponse<DiscountDto>.Error(HttpStatusCode.InternalServerError, "Discount could not be added.");
            }

            var discountDto = _mapper.Map<DiscountDto>(dto);
            discountDto.Id = id;
            return OkResponse<DiscountDto>.Success(HttpStatusCode.Created, discountDto);
        }

        public async Task<OkResponse<DiscountDto>> Update(DiscountUpdateDto dto)
        {
            if (dto == null || dto.Id <= 0 || string.IsNullOrEmpty(dto.Code) || string.IsNullOrEmpty(dto.UserId) || dto.EndDate < DateTime.Now || dto.StartDate > dto.EndDate)
            {
                return OkResponse<DiscountDto>.Error(HttpStatusCode.BadRequest, "Model is not valid");
            }

            var entity = _mapper.Map<Data.Entities.Discount>(dto);
            var isSuccess = await _discountRepository.Update(entity);
            if (!isSuccess)
            {
                return OkResponse<DiscountDto>.Error(HttpStatusCode.InternalServerError, "Discount could not be updated.");
            }

            var discountDto = _mapper.Map<DiscountDto>(dto);
            return OkResponse<DiscountDto>.Success(HttpStatusCode.OK, discountDto);
        }

        public async Task<OkResponse<object>> DeleteById(int id)
        {
            if (id <= 0)
            {
                return OkResponse<object>.Error(HttpStatusCode.BadRequest, "Discount id is not valid");
            }

            var isSuccess = await _discountRepository.DeleteById(id);
            if (!isSuccess)
            {
                return OkResponse<object>.Error(HttpStatusCode.InternalServerError, "Discount could not be deleted.");
            }

            return OkResponse<object>.Success(HttpStatusCode.NoContent);
        }
    }
}
