CREATE PROCEDURE dbo.InsereDepartamento 
( @Nome         VARCHAR(200) 
, @Codigo       DECIMAL(10,0)     
, @Ativo        BIT          
)
AS
BEGIN 
  INSERT INTO dbo.DEPARTAMENTO 
            ( Nome 
            , Codigo
            , Ativo
            )
     values ( @Nome       
            , @Codigo      
            , @Ativo      
            )
END