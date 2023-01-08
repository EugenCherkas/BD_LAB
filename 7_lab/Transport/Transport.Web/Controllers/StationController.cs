using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;
using Transport.Web.Controllers.Abstractions;

namespace Transport.Web.Controllers;

public class StationController : BaseController<IStationRepository, Station, int>
{
    public StationController(IStationRepository repository) : base(repository)
    {
    }

    protected override Expression<Func<Station, bool>> SearchExpression(string searchString)
    {
        return x => x.Name.ToLower().StartsWith(searchString.ToLower());
    }

    [HttpGet]
    public async Task<IActionResult> Index(string search, int? page)
    {
        ViewBag.Total = await Repository.QueryEntities()
            .CountAsync(x => x.WithControlRoom);
        var items = await GetSearchQuery(search)
            .Include(x => x.RouteStations)
            .ToListAsync();

        return View(ToPagedList(items, page));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await Repository.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var item = await Repository.GetById(id);

        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Station item)
    {
        await Repository.Update(item);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Station item)
    {
        await Repository.Create(item);

        return RedirectToAction(nameof(Index));
    }
}