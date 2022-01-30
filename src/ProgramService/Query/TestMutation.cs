using System;
using AutoMapper;
using ProgramService.Database;

namespace ProgramService.Query
{
	[ExtendObjectType("Mutation")]
	public class TestMutation : Base.AbstractMutation<Type.Test, Entity.TestEntity>
	{
		public async Task<IList<Type.Test>> AddTests(
            [Service] ProgramDbContext context,
            [Service] IMapper mapper,
            IList<Type.Test> tests)
        {
            return await Add(context, mapper, tests);
        }

        public async Task<IList<Type.Test>> EditTests(
            [Service] ProgramDbContext context,
            [Service] IMapper mapper,
            IList<Type.Test> tests)
        {
            return await Edit(context, mapper, tests);
        }
    }
}