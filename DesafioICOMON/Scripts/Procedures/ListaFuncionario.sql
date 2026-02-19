CREATE PROCEDURE dbo.ListaFuncionario
( @Id DECIMAL(10,0) = NULL
)
AS
BEGIN
  SELECT Id
       , Nome
       , Email
       , DepartamentoId
       , CargoId
       , DataAdmissao
       , ManagerId 
    FROM dbo.FUNCIONARIO
   WHERE dbo.FUNCIONARIO.Id = @Id
	       OR (@Id IS NULL)
END
