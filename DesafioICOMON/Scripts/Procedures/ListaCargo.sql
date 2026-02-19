CREATE PROCEDURE dbo.ListaCargo
( @Id DECIMAL(10,0) = NULL
)
AS
BEGIN
    SELECT Id        
         , Nome      
         , Nivel     
         , Ativo     
      FROM dbo.CARGO
	 WHERE dbo.CARGO.Id = @Id
	    OR (@Id IS NULL)
END

