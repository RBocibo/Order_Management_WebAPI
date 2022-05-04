using System;

public class MapperMock
{
    private IMapper _mapper;

    public IMapper GetMapper()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapperProfile());
        });

        _mapper = mappingConfig.CreateMapper();

        return _mapper;
    }
}
