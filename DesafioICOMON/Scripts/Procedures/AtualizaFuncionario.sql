CREATE PROCEDURE dbo.AtualizaFuncionario 
( @Nome           VARCHAR(200)   
, @Email          VARCHAR(100)
, @DepartamentoId INT
, @CargoId        INT
, @DataAdmissao   DATETIME
, @ManagerId      INT
, @Id			  DECIMAL(10,0)
)  
AS  
BEGIN   
  UPDATE dbo.FUNCIONARIO  
     SET FUNCIONARIO.Nome           = @Nome         
       , FUNCIONARIO.Email          = @Email        
       , FUNCIONARIO.DepartamentoId = @DepartamentoId        
       , FUNCIONARIO.CargoId        = @CargoId        
       , FUNCIONARIO.DataAdmissao   = @DataAdmissao        
       , FUNCIONARIO.ManagerId      = @ManagerId         
   WHERE FUNCIONARIO.Id             = @Id       
END