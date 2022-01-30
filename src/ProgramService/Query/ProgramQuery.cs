using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProgramService.Database;
using ProgramService.Type;

namespace ProgramService.Query
{
    [ExtendObjectType("Query")]
    public class ProgramQuery
    {
        //[UseSelection] - Documentation states to use it, but it actually prevents the schema from being created
        [HotChocolate.Data.UseFiltering]
        public async Task<IQueryable<Type.Program>> Program([Service] ProgramDbContext context, [Service] IMapper mapper, string name)
            => context.Programs.Where(w => w.Name.Equals(name)).ProjectTo<Type.Program>(mapper.ConfigurationProvider);

        //[UseSelection] - Documentation states to use it, but it actually prevents the schema from being created
        [HotChocolate.Data.UseFiltering]
        public async Task<IQueryable<Type.Program>> Programs([Service] ProgramDbContext context, [Service] IMapper mapper)
            => context.Programs.ProjectTo<Type.Program>(mapper.ConfigurationProvider);
    }
}

