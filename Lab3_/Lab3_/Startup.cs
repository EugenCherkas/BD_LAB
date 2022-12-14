using System;
using System.Collections.Generic;
using System.Text;
using Lab3_.Data;
using Lab3_.Middleware;
using Lab3_.Services;
using Lab3_.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Transport.Infrastructure.Data;
using Transport.Infrastructure.Data.Entities;

namespace Lab3_
{
    public class Startup
    {
        private static readonly string CacheKey = "Transport20";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // внедрение зависимости для доступа к БД с использованием EF
            var connection = Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<TransportContext>(options => options.UseSqlServer(connection));
            // внедрение зависимости OperationService
            services.AddTransient<ITransportService, TransportService>();
            // добавление кэширования
            services.AddMemoryCache();
            // добавление поддержки сессии
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            // добавляем поддержку статических файлов
            app.UseStaticFiles();

            // добавляем поддержку сессий
            app.UseSession();

            app.UseMiddleware<InfoMiddleware>();

            // добавляем компонент middleware по инициализации базы данных и производим инициализацию базы
            app.UseDbInitializer();

            // добавляем компонент middleware для реализации кэширования и записывем данные в кэш
            app.UseOperatinCache(CacheKey);

            app.MapWhen(ctx => ctx.Request.Path == "/", Index);
            app.MapWhen(ctx => ctx.Request.Path == "/searchform1", CookieSearch);
            app.MapWhen(ctx => ctx.Request.Path == "/search1", CookieSearchHandler);
            app.MapWhen(ctx => ctx.Request.Path == "/searchform2", SessionSearch);
            app.MapWhen(ctx => ctx.Request.Path == "/search2", SessionSearchHandler);

            app.UseRouting();
        }

        private static string AppendEmployeesTable(string htmlString, IEnumerable<EmployeeViewModel> employees)
        {
            var stringBuilder = new StringBuilder(htmlString);

            stringBuilder.Append("<div style=\"display: inline-block;\"><H1>сотрудники</H1>");
            stringBuilder.Append("<TABLE BORDER=1>");
            stringBuilder.Append("<TH>");
            stringBuilder.Append("<TD>#</TD>");
            stringBuilder.Append("<TD>Имя</TD>");
            stringBuilder.Append("<TD>фамилия</TD>");
            stringBuilder.Append("<TD>смена</TD>");
            stringBuilder.Append("<TD>должность</TD>");
            stringBuilder.Append("</TH>");

            foreach (var model in employees)
            {
                var shift = model.IsSecondShift ? "вторая" : "первая";
                stringBuilder.Append("<TR>");
                stringBuilder.Append("<TD>-</TD>");
                stringBuilder.Append($"<TD>{model.Id}</TD>");
                stringBuilder.Append($"<TD>{model.FirstName}</TD>");
                stringBuilder.Append($"<TD>{model.LastName}</TD>");
                stringBuilder.Append($"<TD>{shift}</TD>");
                stringBuilder.Append($"<TD>{model.Rank}</TD>");
                stringBuilder.Append("</TR>");
            }

            stringBuilder.Append("</table>");
            stringBuilder.Append("</div>");

            return stringBuilder.ToString();
        }

        private static string AppendRanksTable(string htmlString, IEnumerable<Rank> ranks)
        {
            var stringBuilder = new StringBuilder(htmlString);

            stringBuilder.Append("<div style=\"display: inline-block; margin-left: 35px;\"><H1>должности</H1>");
            stringBuilder.Append("<TABLE BORDER=1>");
            stringBuilder.Append("<TH>");
            stringBuilder.Append("<TD>#</TD>");
            stringBuilder.Append("<TD>Наименование</TD>");
            stringBuilder.Append("</TH>");

            foreach (var rank in ranks)
            {
                stringBuilder.Append("<TR>");
                stringBuilder.Append("<TD>-</TD>");
                stringBuilder.Append($"<TD>{rank.Id}</TD>");
                stringBuilder.Append($"<TD>{rank.Name}</TD>");
                stringBuilder.Append("</TR>");
            }

            stringBuilder.Append("</table>");
            stringBuilder.Append("</div>");

            return stringBuilder.ToString();
        }

        private static string AppendTransportTypesTable(string htmlString, IEnumerable<TransportType> transportTypes)
        {
            var stringBuilder = new StringBuilder(htmlString);

            stringBuilder.Append("<div style=\"display: inline-block; margin-left: 35px;\"><H1>типы транспорта</H1>");
            stringBuilder.Append("<TABLE BORDER=1>");
            stringBuilder.Append("<TH>");
            stringBuilder.Append("<TD>#</TD>");
            stringBuilder.Append("<TD>наименование</TD>");
            stringBuilder.Append("</TH>");

            foreach (var driver in transportTypes)
            {
                stringBuilder.Append("<TR>");
                stringBuilder.Append("<TD>-</TD>");
                stringBuilder.Append($"<TD>{driver.Id}</TD>");
                stringBuilder.Append($"<TD>{driver.Name}</TD>");
                stringBuilder.Append("</TR>");
            }

            stringBuilder.Append("</table>");
            stringBuilder.Append("</div>");

            return stringBuilder.ToString();
        }

        private static string AppendMenu(string htmlString)
        {
            return htmlString +
                   "<div style=\"margin-top: 15px; margin-bottom: 15px;\">" +
                        "<div style=\"display: inline-block; margin-right: 10px;\"><a href=\"/info\">Информация</a></div>" +
                        "<div style=\"display: inline-block; margin-right: 10px;\"><a href=\"/\">Таблица с кешированием</a></div>" +
                        "<div style=\"display: inline-block; margin-right: 10px;\"><a href=\"/searchform1\">Поиск (куки)</a></div>" +
                        "<div style=\"display: inline-block; margin-right: 10px;\"><a href=\"/searchform2\">Поиск (сессия)</a></div>" +
                   "</div>";
        }

        private static void Index(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var service = context.RequestServices.GetService<ITransportService>();

                if (service == null)
                {
                    throw new InvalidOperationException($"Unable to retrieve {nameof(ITransportService)} service");
                }

                var models = service.GetHomeViewModel(CacheKey);
                var htmlString = "<HTML><HEAD>" +
                                 "<TITLE>С кешированием</TITLE></HEAD>" +
                                 "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                                 "<BODY>";
                htmlString = AppendMenu(htmlString);
                htmlString = AppendEmployeesTable(htmlString, models.Employees);
                htmlString = AppendRanksTable(htmlString, models.Ranks);
                htmlString = AppendTransportTypesTable(htmlString, models.TransportTypes);

                htmlString += "</BODY></HTML>";

                return context.Response.WriteAsync(htmlString);
            });
        }

        private static void SessionSearchHandler(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var rankParam = context.Request.Query["rank"].ToString();
                var lastnameParam = context.Request.Query["lastname"].ToString();

                context.Session.Set("rank", Encoding.Default.GetBytes(rankParam));
                context.Session.Set("lastname", Encoding.Default.GetBytes(lastnameParam));

                var service = context.RequestServices.GetService<ITransportService>();

                if (service == null)
                {
                    throw new InvalidOperationException($"Unable to retrieve {nameof(ITransportService)} service");
                }

                var htmlString = "<HTML><HEAD>" +
                                 "<TITLE>Поиск (куки)</TITLE></HEAD>" +
                                 "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                                 "<BODY>";

                var cars = service.SearchEmployees(rankParam, lastnameParam);
                htmlString = AppendMenu(htmlString);
                htmlString = AppendEmployeesTable(htmlString, cars);

                return context.Response.WriteAsync(htmlString);
            });
        }

        private static void SessionSearch(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var htmlString = "<HTML><HEAD>" +
                                 "<TITLE>Поиск (сессия)</TITLE></HEAD>" +
                                 "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                                 "<BODY>";
                htmlString = AppendMenu(htmlString);

                if (!context.Session.TryGetValue("rank", out var rankArray))
                {
                    rankArray = null;
                }

                var carMark = rankArray == null
                    ? string.Empty
                    : Encoding.Default.GetString(rankArray);

                if (!context.Session.TryGetValue("lastname", out var lastnameArray))
                {
                    lastnameArray = null;
                }

                var carOrganization = lastnameArray == null
                    ? string.Empty
                    : Encoding.Default.GetString(lastnameArray);

                var service = context.RequestServices.GetService<ITransportService>();

                if (service == null)
                {
                    throw new InvalidOperationException($"Unable to retrieve {nameof(ITransportService)} service");
                }

                var ranks = service.GetRanks();

                var selectHtml = "<select name='rank'>";
                foreach (var mark in ranks)
                {
                    if (carMark != string.Empty && carMark == mark)
                    {
                        selectHtml += $"<option selected=\"selected\" value='{mark}'>" + mark + "</option>";
                    }

                    selectHtml += $"<option value='{mark}'>" + mark + "</option>";
                }

                selectHtml += "</select>";

                htmlString += "<form action = /search2 >" +
                "<br>должность: " + selectHtml +
                "<br>фамилия: " + $"<input type = 'text' name = 'lastname' value='{carOrganization}'>" +
                    "<br><input type = 'submit' value = 'Найти' ></form>";


                htmlString += "</BODY></HTML>";

                return context.Response.WriteAsync(htmlString);
            });
        }

        private static void CookieSearchHandler(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var rankParam = context.Request.Query["rank"].ToString();
                var lastnameParam = context.Request.Query["lastname"].ToString();

                context.Response.Cookies.Append("rank", rankParam);
                context.Response.Cookies.Append("lastname", lastnameParam);

                var service = context.RequestServices.GetService<ITransportService>();

                if (service == null)
                {
                    throw new InvalidOperationException($"Unable to retrieve {nameof(ITransportService)} service");
                }

                var htmlString = "<HTML><HEAD>" +
                                 "<TITLE>Поиск (куки)</TITLE></HEAD>" +
                                 "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                                 "<BODY>";

                var employees = service.SearchEmployees(rankParam, lastnameParam);
                htmlString = AppendMenu(htmlString);
                htmlString = AppendEmployeesTable(htmlString, employees);

                return context.Response.WriteAsync(htmlString);
            });
        }

        private static void CookieSearch(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var htmlString = "<HTML><HEAD>" +
                                 "<TITLE>Поиск (куки)</TITLE></HEAD>" +
                                 "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                                 "<BODY>";
                htmlString = AppendMenu(htmlString);

                if (!context.Request.Cookies.TryGetValue("rank", out var rank))
                {
                    rank = string.Empty;
                }

                if (!context.Request.Cookies.TryGetValue("lastname", out var lastname))
                {
                    lastname = string.Empty;
                }

                var service = context.RequestServices.GetService<ITransportService>();

                if (service == null)
                {
                    throw new InvalidOperationException($"Unable to retrieve {nameof(ITransportService)} service");
                }

                var ranks = service.GetRanks();

                var selectHtml = "<select name='rank'>";
                foreach (var mark in ranks)
                {
                    if (rank != string.Empty && rank == mark)
                    {
                        selectHtml += $"<option selected=\"selected\" value='{mark}'>" + mark + "</option>";
                    }

                    selectHtml += $"<option value='{mark}'>" + mark + "</option>";
                }

                selectHtml += "</select>";

                htmlString += "<form action = /search1 >" +
                "<br>должность: " + selectHtml +
                "<br>фамилия: " + $"<input type = 'text' name = 'lastname' value='{lastname}'>" +
                    "<br><input type = 'submit' value = 'Найти' ></form>";


                htmlString += "</BODY></HTML>";

                return context.Response.WriteAsync(htmlString);
            });
        }
    }
}