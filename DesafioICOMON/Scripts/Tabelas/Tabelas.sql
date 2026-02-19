--Aplicar antes de executar o projeto
CREATE TABLE DEPARTAMENTO
( Id     INT IDENTITY(1,1) PRIMARY KEY
, Nome   VARCHAR(200)  NOT NULL
, Codigo DECIMAL(10,0) NOT NULL
, Ativo  BIT           NOT NULL
, CONSTRAINT U_Dpt_CPF UNIQUE (Codigo)
)

CREATE TABLE CARGO
( Id           INT IDENTITY(1,1) PRIMARY KEY
, Nome         VARCHAR(200) NOT NULL
, Nivel        VARCHAR(2)     NOT NULL
, Ativo        BIT          NOT NULL
)

CREATE TABLE FUNCIONARIO
( Id             INT IDENTITY(1,1) PRIMARY KEY
, Nome           VARCHAR(200) NOT NULL
, Email          VARCHAR(100) NOT NULL 
, DepartamentoId INT          NOT NULL
, CargoId        INT          NOT NULL
, DataAdmissao   DATETIME     NOT NULL
, ManagerId      INT          NULL
  CONSTRAINT FK_Funcionario_Departamento FOREIGN KEY (DepartamentoId) REFERENCES DEPARTAMENTO(Id)
, CONSTRAINT FK_Funcionario_Cargo FOREIGN KEY (CargoId) REFERENCES Cargo(Id)
, CONSTRAINT FK_Funcionario_Manager FOREIGN KEY (ManagerId) REFERENCES FUNCIONARIO(Id)
)