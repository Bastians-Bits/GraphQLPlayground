using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProgramService.Database;

namespace ProgramService.Query
{
    [ExtendObjectType("Mutation")]
    public class ProgramMutation
        : Query.Base.AbstractMutation<Type.Program, Entity.ProgramEntity>
	{
        public async Task<IList<Type.Program>> AddPrograms(
            [Service] ProgramDbContext context,
            [Service] IMapper mapper,
            IList<Type.Program> programs)
        {
            return await Add(context, mapper, programs);
        }

        public async Task<IList<Type.Program>> EditPrograms(
            [Service] ProgramDbContext context,
            [Service] IMapper mapper,
            IList<Type.Program> programs)
        {
            return await Edit(context, mapper, programs);
        }
    }
}