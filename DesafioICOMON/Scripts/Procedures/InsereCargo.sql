CREATE PROCEDURE dbo.InsereCargo 
( @Nome         VARCHAR(200) 
, @Nivel        CHAR(2)      
, @Ativo        BIT          
)
AS
BEGIN 
  INSERT INTO dbo.CARGO 
            ( Nome 
            , Nivel
            , Ativo
            )
     VALUES ( @Nome       
            , @Nivel      
            , @Ativo      
            )
END