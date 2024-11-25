using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
			//Quando inicializar a api no servidor vai criar a ainstancia do servi�o quando for solicitado pela primeira vez.
			// reutiliza a mesma instancia em todos os locais em que esse servi�o � necessario 
			// isso pode ser um problema danado porque esta compartilhando a mesma memoria e as mesmas informa��es
			//services.AddSingleton<IRepository, Repository>();

			//AddTransient
			// n�o importa de onde venha a requisi��o, se uma mesma requisi��o dentro do repository tiverem necessitando de outras classes
			// cada uma dessas classes vai utilizar o mesmo status, vai criar instancias para todoas elas
			// ele nunca vai reutilizar mesma requisi��o ou em requisi��es novas, ele nunca vai utilizar a mesma instancia
			// por exemplo se eu utilizar a instancia do aluna e la tambem tiver um servi�o de professores
			// ela vai criatr uma nova instancia de aluno para depois criar um a instancia de professores
			// vai sempre gerar uma nova instancia para cada intem encontrado capara cada dependencia, ou seja se encontrar 5
			// ser�o 5 instancias diferentes
			//services.AddTransient<IRepository, Repository>();

			//AddScoped
			// ele � difetente da transiente que garante que uma requisi��o seja criada uma instancia de uma classe onde se houver outras dependencias
			// seja utilizada essa unica instancia para todas, renovando soemnte nas requisi��es subsquentes, mas, mantendo essa obrigatoriedade
			//services.AddScoped<IRepository,Repository>();

			//services.AddSingleton<IRepository, Repository>();
			//services.AddTransient<IRepository, Repository>();
			
          
            services.AddControllers()
                    .AddNewtonsoftJson(
                                        opt => opt.SerializerSettings.ReferenceLoopHandling =
                                               Newtonsoft.Json.ReferenceLoopHandling.Ignore); // para ignorar o loop infnito do json
			
            services.AddScoped<IRepository, Repository>();

			// passando quais s�o as aplica��es de dominio dos eus assembles
            // auto mapper vai procurar dentro dos seus assembles que esta herdando de profille 
            // busca toda classe que herda de profille
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
