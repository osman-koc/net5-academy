using AutoMapper;
using System;

namespace NET5Academy.Services.Order.Application.Mapping
{
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile<OkAutoMapping>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => _lazy.Value;
    }
}
