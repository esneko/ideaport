using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ideaport.Models;

namespace ideaport
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.Authority = $"{Configuration["AzureAdB2C:Instance"]}/{Configuration["AzureAdB2C:Domain"]}/{Configuration["AzureAdB2C:SignUpSignInPolicyId"]}/v2.0/";
                jwtOptions.Audience = Configuration["AzureAdB2C:ClientId"];
                jwtOptions.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        var _system = ctx.HttpContext.RequestServices.GetRequiredService<SystemContext>();

                        ClaimsIdentity identity = ctx.Principal.Identity as ClaimsIdentity;
                        Claim userEmail = identity.Claims.FirstOrDefault(x => x.Type == "emails");
                        User user = _system.User.FirstOrDefault(u => u.Email == userEmail.Value);

                        if (user == null)
                        {
                            // issue an id to the user at first login
                            _system.User.Add(new User()
                            {
                                Email = userEmail.Value
                            });

                            await _system.SaveChangesAsync();
                        }
                        else {
                            // identity.AddClaim(new Claim(ClaimTypes.Role, user.Id));
                        }
                    },
                    // OnAuthenticationFailed = AuthenticationFailed
                };
            });

            services.AddAutoMapper();

            services.AddDbContext<SystemContext>(options => options.UseInMemoryDatabase("System"));

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        // private Task AuthenticationFailed(AuthenticationFailedContext arg)
        // {
        //     var s = $"AuthenticationFailed: {arg.Exception.Message}";
        //     arg.Response.ContentLength = s.Length;
        //     arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
            
        //     return Task.FromResult(0);
        // }
    }
}
