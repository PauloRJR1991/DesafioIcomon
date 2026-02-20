using DesafioICOMON.Controllers.MeuProjetoMVC.Controllers;
using DesafioICOMON.Data;
using DesafioICOMON.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using System.Data;
using System.Text.Json;
using DocumentFormat.OpenXml.InkML;

namespace DesafioICOMON.Controllers
{
    public class FuncionarioController : Controller
    {
        private static List<Funcionario> funcionarios = new List<Funcionario>();
        private static List<Departamento> departamentos = new List<Departamento>();
        private static List<Cargo> cargos = new List<Cargo>();

        private DesafioICOMONContext _context;
        public FuncionarioController(DesafioICOMONContext context) { _context = context; }

        public IActionResult Index()
        {
            CarregarDropdowns();
            funcionarios = _context.Funcionarios
               .FromSqlRaw("EXEC ListaFuncionario")
               .ToList();
            return View(funcionarios);
        }

        public IActionResult Create()
        {
            CarregarDropdowns();
            return View();
        }

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
        }

        private bool ValidaManager(Funcionario funcionario)
        {
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

            var managerArea = _context.Funcionarios
                                  .FromSqlRaw("SELECT * " +
                                              "  FROM Funcionario " +
                                              " WHERE Id = {0}"
                                              , funcionario.ManagerId
                                              )
                                  .AsEnumerable()
                                  .FirstOrDefault();


            var func = _context.Cargos
                        .FromSqlRaw("SELECT Id, Nome, Nivel, Ativo FROM Cargo WHERE Id = {0}", funcionario.CargoId)
                        .AsEnumerable()
                        .FirstOrDefault();

            var funcArea = _context.Funcionarios
                                    .FromSqlRaw("SELECT * FROM Funcionario WHERE Id = {0}", funcionario.Id)
                                    .AsEnumerable()
                                    .FirstOrDefault();
            if (manager?.Nivel != null )
            {
                if (Convert.ToDecimal(manager?.Nivel) < Convert.ToDecimal(func?.Nivel))
                {
                    ModelState.AddModelError("ManagerId", "Este Funcionario não pode ser Manager, por ser de um Nível menor.");
                    return true;
                }
            }
            if (managerArea?.DepartamentoId != null)
            {
                if (Convert.ToDecimal(managerArea?.DepartamentoId) != Convert.ToDecimal(funcArea?.DepartamentoId))
                {
                    ModelState.AddModelError("ManagerId", "Funcionários não são do mesmo Departamento.");
                    return true;
                }
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

        public IActionResult Edit(int id)
        {
            var funcionario = _context.Funcionarios.FromSqlRaw("EXEC ListaFuncionario @Id = {0}", id).AsEnumerable().FirstOrDefault();

            if (funcionario == null)
                return NotFound();

            CarregarDropdowns(funcionario);
            return View(funcionario);
        }

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

        public IActionResult ExportarExcel()
        {

            var funcionarios = _context.Funcionarios
           .FromSqlRaw("EXEC ListaFuncionario")
           .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Funcionarios");

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nome";
                worksheet.Cell(1, 3).Value = "Email";
                worksheet.Cell(1, 4).Value = "Departamento";
                worksheet.Cell(1, 5).Value = "Cargo";
                worksheet.Cell(1, 6).Value = "Manager";
                worksheet.Cell(1, 7).Value = "Data Admissão";

                int row = 2;
                foreach (var f in funcionarios)
                {
                    worksheet.Cell(row, 1).Value = f.Id;
                    worksheet.Cell(row, 2).Value = f.Nome;
                    worksheet.Cell(row, 3).Value = f.Email;
                    worksheet.Cell(row, 4).Value = f.Departamento?.Nome;
                    worksheet.Cell(row, 5).Value = f.Cargo?.Nome;
                    worksheet.Cell(row, 6).Value = f.Manager?.Nome;
                    worksheet.Cell(row, 7).Value = f.DataAdmissao.ToString("dd/MM/yyyy");
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Funcionarios.xlsx");
                }
            }
        }

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

        public IActionResult Organograma()
        {
            var funcionarios = _context.Funcionarios
                .FromSqlRaw("EXEC ListaFuncionario")
                .AsEnumerable()
                .ToList();

            var dados = funcionarios.Select(f => new
            {
                id = f.Id,
                name = f.Nome,
                pid = f.ManagerId == 0 ? null : f.ManagerId
            }).ToList();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                WriteIndented = false
            };

            ViewBag.JsonData = funcionarios;

            return View(funcionarios);
        }
    }
}
