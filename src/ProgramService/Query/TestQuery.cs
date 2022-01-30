using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProgramService.Database;

namespace ProgramService.Query
{
    [ExtendObjectType("Query")]
    public class TestQuery
	{
        //[UseSelection] - Documentation states to use it, but it actually prevents the schema from being created
        [HotChocolate.Data.UseFiltering]
        public async Task<IQueryable<Type.Test>> Test([Service] ProgramDbContext context, [Service] IMapper mapper, string pk1, string pk2)
            => context.Tests.Where(w => w.TestPk1.Equals(pk1) && w.TestPk2.Equals(pk2)).ProjectTo<Type.Test>(mapper.ConfigurationProvider);

        //[UseSelection] - Documentation states to use it, but it actually prevents the schema from being created
        [HotChocolate.Data.UseFiltering]
        public async Task<IQueryable<Type.Test>> Tests([Service] ProgramDbContext context, [Service] IMapper mapper)
            => context.Tests.ProjectTo<Type.Test>(mapper.ConfigurationProvider);
    }
}

