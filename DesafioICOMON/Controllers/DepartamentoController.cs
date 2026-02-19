using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DesafioICOMON.Models;
using Microsoft.EntityFrameworkCore;
using DesafioICOMON.Data;
using Microsoft.EntityFrameworkCore.Internal;
namespace DesafioICOMON.Controllers
{

  namespace MeuProjetoMVC.Controllers
  {
    public class DepartamentoController : Controller
    {
      private static List<Departamento> departamentos = new List<Departamento>();
      private DesafioICOMONContext _context;
      public DepartamentoController(DesafioICOMONContext context) { _context = context; }

      public IActionResult Index()
      {
        var departamento = _context.Departamentos.FromSqlRaw("EXEC ListaDepartamento").ToList();
        return View(departamento);
      }

      public IActionResult Create()
      {
        return View();
      }

      [HttpPost]
      public IActionResult Create(Departamento departamento)
      {
        if (ModelState.IsValid)
        {
          if (departamentos.Any(d => d.Codigo == departamento.Codigo))
          {
            ModelState.AddModelError("Codigo", "Este código já está cadastrado.");
            return View(departamento);
          }

          _context.Database.ExecuteSqlRaw("EXEC InsereDepartamento" +
             "  @Nome = {0}" +
             ", @Codigo = {1}" +
             ", @Ativo = {2}"
             , departamento.Nome
             , departamento.Codigo
             , departamento.Ativo
            );

          departamentos.Add(departamento);


          return RedirectToAction("Index");
        }
        return View(departamento);
      }

      public IActionResult Edit(int id)
      {
        var departamento = _context.Departamentos.FromSqlRaw("EXEC ListaDepartamento @Id = {0}", id).AsEnumerable().FirstOrDefault();
        return View(departamento);
      }

      [HttpPost]
      public IActionResult Edit(Departamento departamento)
      {
        if (ModelState.IsValid)
        {
          var d = departamentos.FirstOrDefault(x => x.Id == departamento.Id);
          if (d != null)
          {
            if (departamentos.Any(x => x.Codigo == departamento.Codigo && x.Id != departamento.Id))
            {
              ModelState.AddModelError("Codigo", "Este código já está cadastrado.");
              return View(departamento);
            }

            _context.Database.ExecuteSqlRaw("EXEC AtualizaCargo" +
                                            "  @Nome = {0}" +
                                            ", @Codigo = {1}" +
                                            ", @Ativo = {2}" +
                                            ", @Id = {3}"
                                            , departamento.Nome
                                            , departamento.Codigo
                                            , departamento.Ativo
                                            , departamento.Id
                                           );
          }
          return RedirectToAction("Index");
        }
        return View(departamento);
      }

      public IActionResult Delete(int id)
      {
        var departamento = _context.Departamentos.FromSqlRaw("EXEC ListaDepartamento @Id = {0}", id).AsEnumerable().FirstOrDefault();
        return View(departamento);
      }

      public IActionResult DeleteDept(int id)
      {
        _context.Database.ExecuteSqlRaw("EXEC ExcluiDepartamento @Id = {0}", id);
        return RedirectToAction("Index");
      }
    }
  }
}

