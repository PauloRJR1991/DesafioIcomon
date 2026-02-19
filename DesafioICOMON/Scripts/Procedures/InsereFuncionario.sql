CREATE PROCEDURE dbo.InsereFuncionario
( @Nome             VARCHAR(200) 
, @Email            VARCHAR(100)  
, @DepartamentoId 	INT          
, @CargoId 			INT          
, @DataAdmissao 	DATETIME     
, @ManagerId 		BIT          = NULL
)	
AS 
BEGIN 				
  INSERT INTO dbo.FUNCIONARIO
              ( Nome          
              , Email         
              , DepartamentoId
              , CargoId       
              , DataAdmissao  
              , ManagerId     
              )
       VALUES ( @Nome          
              , @Email         
              , @DepartamentoId
              , @CargoId       
              , @DataAdmissao  
              , @ManagerId 
              )
END