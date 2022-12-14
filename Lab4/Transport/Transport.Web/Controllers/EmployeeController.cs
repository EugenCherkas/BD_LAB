using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;
using Transport.Web.Controllers.Abstractions;

namespace Transport.Web.Controllers;

public class EmployeeController : BaseController<IEmployeeRepository, Employee, Guid>
{
    private readonly IRankRepository _rankRepository;
    private readonly IRouteRepository _routeRepository;

    public EmployeeController(
        IEmployeeRepository repository,
        IRankRepository rankRepository,
        IRouteRepository routeRepository) : base(repository)
    {
        _rankRepository = rankRepository;
        _routeRepository = routeRepository;
    }

    protected override Expression<Func<Employee, bool>> SearchExpression(string searchString)
    {
        return x => x.SecondName.ToLower().StartsWith(searchString.ToLower());
    }

    [HttpGet]
    public async Task<IActionResult> Index(string search, int? page)
    {
        var items = await GetSearchQuery(search)
            .Include(x => x.Rank)
            .Include(x => x.Route)
            .ToListAsync();

        return View(ToPagedList(items, page));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Repository.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var item = await Repository.GetById(id);

        var routes = await _routeRepository.GetEntities();
        var ranks = await _rankRepository.GetEntities();

        ViewBag.Routes = routes;
        ViewBag.Ranks = ranks;

        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Employee item)
    {
        await Repository.Update(item);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var ranks = await _rankRepository.GetEntities();

        ViewBag.Ranks = ranks;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee item)
    {
        await Repository.Create(item);

        return RedirectToAction(nameof(Index));
    }
}