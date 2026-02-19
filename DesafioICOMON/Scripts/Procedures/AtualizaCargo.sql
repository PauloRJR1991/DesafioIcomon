CREATE PROCEDURE dbo.AtualizaCargo 
( @Nome         VARCHAR(200) 
, @Nivel        CHAR(2)      
, @Ativo        BIT    
, @Id           INT      
)
AS
BEGIN 
  UPDATE CARGO
     SET CARGO.Nome  = @Nome       
       , CARGO.Nivel = @Nivel      
       , CARGO.Ativo = @Ativo      
   WHERE CARGO.Id    = @Id     
END