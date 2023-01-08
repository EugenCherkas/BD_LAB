using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.Infrastructure.Data.Entities;
using Transport.Infrastructure.Repositories.Abstractions;
using Transport.Web.Controllers.Abstractions;
using Transport.Web.Models.Route;
using Route = Transport.Infrastructure.Data.Entities.Route;

namespace Transport.Web.Controllers;

public class RouteController : BaseController<IRouteRepository, Route, Guid>
{
    private readonly IStationRepository _stationRepository;
    private readonly ITransportTypeRepository _transportTypeRepository;

    public RouteController(
        IRouteRepository repository,
        IStationRepository stationRepository,
        ITransportTypeRepository transportTypeRepository) : base(repository)
    {
        _stationRepository = stationRepository;
        _transportTypeRepository = transportTypeRepository;
    }

    protected override Expression<Func<Route, bool>> SearchExpression(string searchString)
    {
        return x => x.Name.ToLower().StartsWith(searchString.ToLower());
    }

    [HttpDelete("Route/RouteStation")]
    public async Task<IActionResult> RouteStation(Guid routeId, int stationId)
    {
        await Repository.RemoveRouteStation(routeId, stationId);

        return NoContent();
    }

    [HttpPost("Route/RouteStation")]
    public async Task<IActionResult> RouteStation([FromBody] CreateRouteStationBody body)
    {
        await Repository.AppendRouteStation(new RouteStation
        {
            RouteId = body.RouteId,
            StationId = body.StationId,
            Hours = body.Hours,
            Minutes = body.Minutes
        });

        return NoContent();
    }

    [HttpGet("Route/Details/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var items = await Repository.GetRouteStations(id);
        var stations = await _stationRepository.GetEntities();

        var model = new RouteDetailsModel
        {
            RouteId = id,
            RouteStations = items,
            Stations = stations
                .Where(x => items.All(z => z.StationId != x.Id))
                .ToList()
        };

        return PartialView(model);
    }

    [HttpGet]
    public async Task<IActionResult> Index(string search, int? page)
    {
        var items = await GetSearchQuery(search)
            .Include(x => x.TransportType)
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
    public async Task<IActionResult> Create()
    {
        var transportTypes = await _transportTypeRepository.GetEntities();
        ViewBag.TransportTypes = transportTypes;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Route item)
    {
        await Repository.Create(item);

        return RedirectToAction(nameof(Index));
    }
}