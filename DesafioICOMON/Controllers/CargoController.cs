using DesafioICOMON.Data;
using DesafioICOMON.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
namespace DesafioICOMON.Controllers
{
  public class CargoController : Controller
  {
    private readonly DesafioICOMONContext _context;

    private static List<Cargo> cargos = new List<Cargo>();

    public CargoController(DesafioICOMONContext context) { _context = context; }

    //[HttpGet("Cargo/{id}")]
    //public IActionResult Index(int id)
    //{
    //    var cargo = _context.Cargo.FirstOrDefault(f => f.Id == id);
    //    return View(cargo);
    //}
    // LISTAR
    public IActionResult Index(string searchString)
    {
      cargos = _context.Cargos
         .FromSqlRaw("EXEC ListaCargo")
         .ToList();
            // mantém o valor digitado na View
            ViewBag.CurrentFilter = searchString;

            if (!string.IsNullOrEmpty(searchString)) 
            {
                cargos = cargos.Where(c => c.Nome.Contains(searchString)).ToList(); // garante que continua sendo lista
            }
            return View(cargos);
    }

    // CRIAR (GET)
    public IActionResult Create()
    {
      ViewBag.Departamentos = new SelectList(_context.Departamentos, "Id", "Nome");
      ViewBag.Cargos = new SelectList(_context.Cargos, "Id", "Nome");
      ViewBag.Managers = new SelectList(_context.Funcionarios, "Id", "Nome");
      return View();
    }

    // CRIAR (POST)
    [HttpPost]
    public IActionResult Create(Cargo cargo)
    {
      if (ModelState.IsValid)
      {
        cargo.Id = cargos.Count > 0 ? cargos.Max(c => c.Id) + 1 : 1;
        cargos.Add(cargo);

        _context.Database.ExecuteSqlRaw("EXEC InsereCargo" +
                                        "  @Nome = {0}" +
                                        ", @Nivel = {1}" +
                                        ", @Ativo = {2}"
                                        , cargo.Nome
                                        , cargo.Nivel
                                        , cargo.Ativo
                                       );

        return RedirectToAction("Index");
      }
      return View(cargo);
    }

    private string GetDisplayName(NivelCargo nivel)
    {
      var field = nivel.GetType().GetField(nivel.ToString());
      var attr = field.GetCustomAttribute<DisplayAttribute>();
      return attr?.Name ?? nivel.ToString();
    }

    // EDITAR (GET)
    public IActionResult Edit(int id)
    {
      var cargo = _context.Cargos.FromSqlRaw("EXEC ListaCargo @Id = {0}", id).AsEnumerable().FirstOrDefault();

      if (cargo == null) return NotFound();

      var items = Enum.GetValues(typeof(NivelCargo))
      .Cast<NivelCargo>()
      .Select(c => new SelectListItem
      {
        Value = ((int)c).ToString(),
        Text = GetDisplayName(c),
      }).ToList();

      ViewBag.NivelCargoList = items;
      return View(cargo);
    }

    // EDITAR (POST)
    [HttpPost]
    public IActionResult Edit(Cargo cargo)
    {
      if (ModelState.IsValid)
      {
        var c = cargos.FirstOrDefault(x => x.Id == cargo.Id);
        if (c != null)
        {
          _context.Database.ExecuteSqlRaw("EXEC AtualizaCargo" +
                      "  @Nome = {0}" +
                      ", @Nivel = {1}" +
                      ", @Ativo = {2}" +
                      ", @Id = {3}"
                      , cargo.Nome
                      , cargo.Nivel
                      , cargo.Ativo
                      , cargo.Id
                     );
        }
        return RedirectToAction("Index");
      }

      return View(cargo);
    }

    // DELETAR
    public IActionResult Delete(int id)
    {
      var cargo = _context.Cargos.FromSqlRaw("EXEC ListaCargo @Id = {0}", id).AsEnumerable().FirstOrDefault();
      return View(cargo);
    }

    public IActionResult DeleteCargo(int id)
    {
      _context.Database.ExecuteSqlRaw("EXEC ExcluiCargo @Id = {0}", id);
      return RedirectToAction("Index");
    }
  }
}



