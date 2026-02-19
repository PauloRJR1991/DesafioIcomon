CREATE PROCEDURE dbo.AtualizaDepartamento 
( @Nome         VARCHAR(200)   
, @Codigo       DECIMAL(10,0)        
, @Ativo        BIT      
, @Id           INT        
)  
AS  
BEGIN   
  UPDATE dbo.DEPARTAMENTO  
     SET DEPARTAMENTO.Nome   = @Nome         
       , DEPARTAMENTO.Codigo = @Codigo        
       , DEPARTAMENTO.Ativo  = @Ativo        
   WHERE DEPARTAMENTO.Id     = @Id       
END