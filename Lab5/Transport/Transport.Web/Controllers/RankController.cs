using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;
using Transport.Web.Controllers.Abstractions;

namespace Transport.Web.Controllers;

public class RankController : BaseController<IRankRepository, Rank, int>
{
    public RankController(IRankRepository repository) : base(repository)
    {
    }

    protected override Expression<Func<Rank, bool>> SearchExpression(string searchString)
    {
        return x => x.Name.ToLower().Contains(searchString.ToLower());
    }

    [HttpGet]
    public async Task<IActionResult> Index(string search, int? page)
    {
        var items = await GetSearchQuery(search)
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
    public async Task<IActionResult> Update(Rank item)
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
    public async Task<IActionResult> Create(Rank item)
    {
        await Repository.Create(item);

        return RedirectToAction(nameof(Index));
    }
}