using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartSchool.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<SmartContext>
				(
				   contexto => contexto.UseSqlite(Configuration.GetConnectionString("default"))

				);

			//AddSingletonAddScoped
			//Quando inicializar a api no servidor vai criar a ainstancia do serviço quando for solicitado pela primeira vez.
			// reutiliza a mesma instancia em todos os locais em que esse serviço é necessario 
			// isso pode ser um problema danado porque esta compartilhando a mesma memoria e as mesmas informações
			//services.AddSingleton<IRepository, Repository>();

			//AddTransient
			// não importa de onde venha a requisição, se uma mesma requisição dentro do repository tiverem necessitando de outras classes
			// cada uma dessas classes vai utilizar o mesmo status, vai criar instancias para todoas elas
			// ele nunca vai reutilizar mesma requisição ou em requisições novas, ele nunca vai utilizar a mesma instancia
			// por exemplo se eu utilizar a instancia do aluna e la tambem tiver um serviço de professores
			// ela vai criatr uma nova instancia de aluno para depois criar um a instancia de professores
			// vai sempre gerar uma nova instancia para cada intem encontrado capara cada dependencia, ou seja se encontrar 5
			// serão 5 instancias diferentes
			//services.AddTransient<IRepository, Repository>();

			//AddScoped
			// ele é difetente da transiente que garante que uma requisição seja criada uma instancia de uma classe onde se houver outras dependencias
			// seja utilizada essa unica instancia para todas, renovando soemnte nas requisições subsquentes, mas, mantendo essa obrigatoriedade
			//services.AddScoped<IRepository,Repository>();

			//services.AddSingleton<IRepository, Repository>();
			//services.AddTransient<IRepository, Repository>();


			services.AddControllers()
					.AddNewtonsoftJson(
										opt => opt.SerializerSettings.ReferenceLoopHandling =
											   Newtonsoft.Json.ReferenceLoopHandling.Ignore); // para ignorar o loop infnito do json

			services.AddScoped<IRepository, Repository>();

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			})
			.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
			});

			var apiProviderDescription = services.BuildServiceProvider()
												 .GetService<IApiVersionDescriptionProvider>();


			services.AddSwaggerGen(options =>
			{
				foreach (var description in apiProviderDescription.ApiVersionDescriptions)
				{
					options.SwaggerDoc(
						description.GroupName,
						new Microsoft.OpenApi.Models.OpenApiInfo()
						{
							Title = "SmartSchool API",
							Version = description.ApiVersion.ToString(),
							TermsOfService = new Uri("http://SeusTermosDeUso.com"),
							Description = "A descrição da WebAPI do SmartSchool",
							License = new Microsoft.OpenApi.Models.OpenApiLicense
							{
								Name = "SmartSchool License",
								Url = new Uri("http://mit.com")
							},
							Contact = new Microsoft.OpenApi.Models.OpenApiContact
							{
								Name = "Fabiano da Silva",
								Email = "fabiano451@live.com",
								Url = new Uri("https://github.com/fabiano451")
							}
						}
					);
				}

				var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

				options.IncludeXmlComments(xmlCommentsFullPath);
			});
			services.AddCors();
		}
		
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		
		public void Configure(IApplicationBuilder app,
							  IWebHostEnvironment env,
							  IApiVersionDescriptionProvider apiProviderDescription)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseSwagger()
			   .UseSwaggerUI(options =>
			   {
				   foreach (var description in apiProviderDescription.ApiVersionDescriptions)
				   {
					   options.SwaggerEndpoint(
						   $"/swagger/{description.GroupName}/swagger.json",
						   description.GroupName.ToUpperInvariant());
				   }
				   options.RoutePrefix = "";
			   });

			// app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}