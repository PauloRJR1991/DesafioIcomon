CREATE PROCEDURE dbo.ListaDepartamento
( @Id DECIMAL(10,0) = NULL
)
AS
BEGIN
    SELECT Id
         , Nome
         , Codigo
         , Ativo  
      FROM dbo.DEPARTAMENTO
	 WHERE dbo.DEPARTAMENTO.Id = @Id
	    OR (@Id IS NULL)
END