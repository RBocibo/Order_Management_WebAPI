using AutoMapper;
using OrderManagement.Core.Mapper;

namespace Tests.Mocks
{
    public class MapperMock
    {
        private IMapper _mapper;

        public IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            _mapper = mappingConfig.CreateMapper();

            return _mapper;
        }
    }
}