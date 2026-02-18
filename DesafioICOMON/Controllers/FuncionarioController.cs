using DesafioICOMON.Controllers.MeuProjetoMVC.Controllers;
using DesafioICOMON.Data;
using DesafioICOMON.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DesafioICOMON.Controllers
{
  public class FuncionarioController : Controller
  {
    // Simulação de dados em memória (substituir por EF Core depois)
    private static List<Funcionario> funcionarios = new List<Funcionario>();
    private static List<Departamento> departamentos = new List<Departamento>();
    private static List<Cargo> cargos = new List<Cargo>();

    private DesafioICOMONContext _context;
    public FuncionarioController(DesafioICOMONContext context) { _context = context; }
    // LISTAR
    public IActionResult Index()
    {
      CarregarDropdowns();
      funcionarios = _context.Funcionarios
         .FromSqlRaw("EXEC ListaFuncionario")
         .ToList();
      return View(funcionarios);
    }

    // CRIAR (GET)
    public IActionResult Create()
    {
      CarregarDropdowns();
      return View();
    }

    // CRIAR (POST)
    [HttpPost]
    public IActionResult Create(Funcionario funcionario)
    {
      ;
      if (ValidaEmail(funcionario))
      {
        return View(funcionario);
      }

      if (ValidaManager(funcionario))
      {
        return View(funcionario);
      }

      funcionarios.Add(funcionario);

      CarregarDropdowns();
      _context.Database.ExecuteSqlRaw("EXEC InsereFuncionario" +
                      "  @Nome = {0}" +
                      ", @Email = {1}" +
                      ", @DepartamentoId = {2}" +
                      ", @CargoId = {3}" +
                      ", @DataAdmissao = {4}" +
                      ", @ManagerId = {5}"
                      , funcionario.Nome
                      , funcionario.Email
                      , funcionario.DepartamentoId
                      , funcionario.CargoId
                      , funcionario.DataAdmissao
                      , funcionario.ManagerId
                      );

      return RedirectToAction("Index");
      //}

      return View(funcionario);
    }

    private bool ValidaManager(Funcionario funcionario)
    {
      //List<Cargo> manager = new List<Cargo>();
      var manager = _context.Cargos
                            .FromSqlRaw("SELECT Id, " +
                                        "       Nome," +
                                        "       Nivel, " +
                                        "       Ativo " +
                                        "  FROM Cargo " +
                                        " WHERE EXISTS( SELECT 1" +
                                        "                 FROM Funcionario FManager " +
                                        "                WHERE FManager.Id = {0} " +
                                        "                  AND FManager.CargoId = Cargo.Id)"
                                        , funcionario.ManagerId
                                        )
                            .AsEnumerable()
                            .FirstOrDefault();

      var func = _context.Cargos
                        .FromSqlRaw("SELECT Id, Nome, Nivel, Ativo FROM Cargo WHERE Id = {0}", funcionario.CargoId)
                        .AsEnumerable()
                        .FirstOrDefault();

      if (Convert.ToDecimal(manager?.Nivel) < Convert.ToDecimal(func?.Nivel))
      {
        ModelState.AddModelError("ManagerId", "Este Funcionario não pode ser Manager, por ser de um Nível menor.");
        return true;
      }

      CarregarDropdowns();
      return false;
    }

    private bool ValidaEmail(Funcionario funcionario)
    {
      var iExiste = _context.Funcionarios.FromSqlRaw("SELECT *" +
                                                     "  FROM FUNCIONARIO" +
                                                     " WHERE FUNCIONARIO.Email = {0}", funcionario.Email +
                                                     "   AND FUNCIONARIO.Id <> {0}", funcionario.Id
                                                     ).AsEnumerable().Count();
      if (iExiste > 0)
      {
        ModelState.AddModelError("Email", "Este email já está cadastrado.");
      }

      CarregarDropdowns();

      return iExiste > 0;
    }

    // EDITAR (GET)
    public IActionResult Edit(int id)
    {
      var funcionario = _context.Funcionarios.FromSqlRaw("EXEC ListaFuncionario @Id = {0}", id).AsEnumerable().FirstOrDefault();

      if (funcionario == null)
        return NotFound();

      CarregarDropdowns(funcionario);
      return View(funcionario);
    }

    // EDITAR (POST)
    [HttpPost]
    public IActionResult Edit(Funcionario funcionario)
    {

      if (ValidaEmail(funcionario))
      {
        return View(funcionario);
      }

      if (ValidaManager(funcionario))
      {
        return View(funcionario);
      }


      _context.Database.ExecuteSqlRaw("EXEC AtualizaFuncionario" +
           "  @Nome = {0}" +
           ", @Email = {1}" +
           ", @DepartamentoId = {2}" +
           ", @CargoId = {3}" +
           ", @DataAdmissao = {4}" +
           ", @ManagerId = {5}" +
           ", @Id = {6}"
          , funcionario.Nome
          , funcionario.Email
          , funcionario.DepartamentoId
          , funcionario.CargoId
          , funcionario.DataAdmissao
          , funcionario.ManagerId
          , funcionario.Id
          );


      return RedirectToAction("Index");
    }

    // DELETAR
    public IActionResult Delete(int id)
    {
      var departamento = _context.Cargos.FromSqlRaw("EXEC ListaFuncionario @Id = {0}", id).AsEnumerable().FirstOrDefault();
      return View(departamento);
    }

    public IActionResult DeleteFunc(int id)
    {
      _context.Database.ExecuteSqlRaw("EXEC ExcluiFuncionario @Id = {0}", id);
      return RedirectToAction("Index");
    }

    // Método auxiliar para carregar dropdowns
    private void CarregarDropdowns(Funcionario funcionario)
    {
      departamentos = _context.Departamentos.FromSqlRaw("EXEC ListaDepartamento").ToList();
      cargos = _context.Cargos.FromSqlRaw("EXEC ListaCargo").ToList();
      ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nome");
      ViewBag.Cargos = new SelectList(cargos, "Id", "Nome");
      ViewBag.Managers = new SelectList(funcionarios.Where(m => m.Id != funcionario.Id), "Id", "Nome");
    }

    private void CarregarDropdowns()
    {
      departamentos = _context.Departamentos.FromSqlRaw("EXEC ListaDepartamento").ToList();
      cargos = _context.Cargos.FromSqlRaw("EXEC ListaCargo").ToList();
      ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nome");
      ViewBag.Cargos = new SelectList(cargos, "Id", "Nome");
      ViewBag.Managers = new SelectList(funcionarios, "Id", "Nome");
    }
  }
}

