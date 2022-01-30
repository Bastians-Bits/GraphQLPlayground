using System;
using AutoMapper;

namespace ProgramService.Mapper
{
	public class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<Entity.ProgramEntity, Type.Program>().ReverseMap();
			CreateMap<Entity.VersionEntity, Type.Version>().ReverseMap();
			CreateMap<Entity.TestEntity, Type.Test>().ReverseMap();
		}
	}
}

