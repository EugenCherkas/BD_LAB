using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Repositories.Abstractions;
using Transport.Web.Models;

namespace Transport.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRouteRepository _routeRepository;

    public HomeController(ILogger<HomeController> logger, IRouteRepository routeRepository)
    {
        _logger = logger;
        _routeRepository = routeRepository;
    }

    [HttpGet("RouteEmployees")]
    public async Task<IActionResult> RouteEmployees(Guid routeId)
    {
        var employees = await _routeRepository.QueryEntities()
            .Where(x => x.Id == routeId)
            .SelectMany(x => x.Employees)
            .Include(x => x.Rank)
            .ToListAsync();

        return PartialView(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var routes = await _routeRepository
            .QueryEntities()
            .Include(x => x.Employees)
            .ThenInclude(x => x.Rank)
            .ToListAsync();

        return View(routes);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}