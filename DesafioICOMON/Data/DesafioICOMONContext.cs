using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioICOMON.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioICOMON.Data
{
    public class DesafioICOMONContext : DbContext
    {
        public DesafioICOMONContext (DbContextOptions<DesafioICOMONContext> options)
            : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }




    }
}
