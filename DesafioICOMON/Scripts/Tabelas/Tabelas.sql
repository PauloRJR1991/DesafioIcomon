--Aplicar antes de executar o projeto
--drop table DEPARTAMENTO
--drop table CARGO
--drop table FUNCIONARIO

--Aplicar antes de executar o projeto
CREATE TABLE DEPARTAMENTO
( Id     INT IDENTITY(1,1) PRIMARY KEY
, Nome   VARCHAR(200)  NOT NULL
, Codigo DECIMAL(10,0) NOT NULL
, Ativo  BIT           NOT NULL
, CONSTRAINT U_Dpt_COD UNIQUE (Codigo)
)

INSERT INTO DEPARTAMENTO ( Nome, Codigo, Ativo) VALUES ('RH', 10, 1);
INSERT INTO DEPARTAMENTO ( Nome, Codigo, Ativo) VALUES ('TI', 1, 1);
INSERT INTO DEPARTAMENTO ( Nome, Codigo, Ativo) VALUES ('Financeiro', 5, 1);
INSERT INTO DEPARTAMENTO ( Nome, Codigo, Ativo) VALUES ('Diretoria', 1001, 1);
INSERT INTO DEPARTAMENTO ( Nome, Codigo, Ativo) VALUES ('Financeiro', 1002, 1);
INSERT INTO DEPARTAMENTO ( Nome, Codigo, Ativo) VALUES ('Recursos Humanos', 1003, 1);
INSERT INTO DEPARTAMENTO ( Nome, Codigo, Ativo) VALUES ('Tecnologia da Informação', 1004, 1);
INSERT INTO DEPARTAMENTO ( Nome, Codigo, Ativo) VALUES ('Marketing', 1005, 1);

CREATE TABLE CARGO
( Id           INT IDENTITY(1,1) PRIMARY KEY
, Nome         VARCHAR(200) NOT NULL
, Nivel        VARCHAR(2)   NOT NULL
, Ativo        BIT          NOT NULL
)

INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ('Assistente de TI', '3', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ('Analista de sistemas Pleno', '3 ', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ('Analista de sistemas Estagiario', '1 ', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ('Analista de sistemas Senior', '4 ', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ('Coordenador TI', '5 ', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ('Gerente TI', '6 ', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ('Diretor TI', '7 ', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ( 'CEO', '8', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ( 'Diretor Financeiro', '7', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ( 'Gerente RH', '6', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ( 'Analista TI', '2', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ( 'Desenvolvedor', '3', 1);
INSERT INTO CARGO ( Nome, Nivel, Ativo) VALUES ( 'Analista Marketing', '3', 1);

CREATE TABLE FUNCIONARIO
( Id             INT IDENTITY(1,1) PRIMARY KEY
, Nome           VARCHAR(200) NOT NULL
, Email          VARCHAR(100) NOT NULL 
, DepartamentoId INT          NOT NULL
, CargoId        INT          NOT NULL
, DataAdmissao   DATETIME     NOT NULL
, ManagerId      INT          NULL
, CONSTRAINT U_Dpt_Email UNIQUE (Email)
, CONSTRAINT FK_Funcionario_Departamento FOREIGN KEY (DepartamentoId) REFERENCES DEPARTAMENTO(Id)
, CONSTRAINT FK_Funcionario_Cargo FOREIGN KEY (CargoId) REFERENCES Cargo(Id)
)

INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ('Paulo Junior', 'Paulo@Paulo', 2, 3, '20260201 00:00:00', 2);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ('Leandro', 'L@L', 2, 6, '20260201 00:00:00', 3);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ('Toro', 'T@T', 2, 7, '20260201 00:00:00', 4);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ('Pilla', 'P@P.com', 2, 8, '20260201 00:00:00', NULL);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ('henry', 'H@H.com', 2, 4, '20260101 00:00:00', 2);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ( 'Sergio', 'pilla@empresa.com', 1, 1, '20200110 00:00:00', NULL);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ( 'Ana', 'leandro@empresa.com', 3, 3, '20220520 00:00:00', 1);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ( 'Junior', 'paulo@empresa.com', 4, 4, '20230701 00:00:00', 2);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ( 'Maria', 'maria@empresa.com', 4, 5, '20240212 00:00:00', 4);
INSERT INTO Funcionario ( Nome, Email, DepartamentoId, CargoId, DataAdmissao, ManagerId) VALUES ( 'João Santos', 'joao@empresa.com', 5, 6, '20240930 00:00:00', 1);




