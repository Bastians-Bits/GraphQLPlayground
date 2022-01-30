using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ProgramService.Database;
using ProgramService.Mapper;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services
    .AddGraphQLServer()
    .AddQueryType(t => t.Name("Query"))
        .AddType<ProgramService.Query.ProgramQuery>()
        .AddType<ProgramService.Query.TestQuery>()
    .AddMutationType(t => t.Name("Mutation"))
        .AddType<ProgramService.Query.ProgramMutation>()
        .AddType<ProgramService.Query.TestMutation>()
    .AddFiltering();

builder.Services.AddAutoMapper(typeof(Mapping));

builder.Services.AddDbContext<ProgramDbContext>();

var app = builder.Build();

app.MapGraphQL();

using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<ProgramDbContext>();
    if (app.Environment.IsDevelopment())
        context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    context.Programs.AddRange(
        new ProgramService.Entity.ProgramEntity()
        {
            Name = "MyProgram1",
            Version = new List<ProgramService.Entity.VersionEntity>
            {
                new ProgramService.Entity.VersionEntity()
                {
                    VersionTag = "1.0"
                },
                new ProgramService.Entity.VersionEntity()
                {
                    VersionTag = "2.0"
                }
            }
        },
        new ProgramService.Entity.ProgramEntity()
        {
            Name = "MyProgram2",
            Version = new List<ProgramService.Entity.VersionEntity>
            {
                new ProgramService.Entity.VersionEntity()
                {
                    VersionTag = "3.0"
                },
                new ProgramService.Entity.VersionEntity()
                {
                    VersionTag = "4.0"
                }
            }
        },
        new ProgramService.Entity.ProgramEntity()
        {
            Name = "MyProgram3",
            Version = new List<ProgramService.Entity.VersionEntity>
            {
               new ProgramService.Entity.VersionEntity()
               {
                   VersionTag = "5.0"
               },
               new ProgramService.Entity.VersionEntity()
               {
                    VersionTag = "6.0"
               }
            }
        }
    );
    context.Tests.Add(new ProgramService.Entity.TestEntity()
    {
        Field = "Field",
        TestPk1 = "Pk1.1",
        TestPk2 = "Pk1.2"
    });
    context.SaveChanges();
}

app.Run();