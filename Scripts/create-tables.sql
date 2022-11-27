CREATE DATABASE [pub-dev];
GO

USE [pub-dev];
GO

CREATE TABLE tblUsuarioStatus
(
	UsuarioStatusID INT IDENTITY(1,1) NOT NULL,
	Nome NVARCHAR(50) NOT NULL,
	DataCriacao DATETIME DEFAULT(GETDATE()),
	CONSTRAINT [PK_tblUsuarioStatus_UsuarioStatusID] PRIMARY KEY(UsuarioStatusID)
)

CREATE TABLE tblUsuario
(
	UsuarioID INT IDENTITY(1,1) NOT NULL,
	Nome NVARCHAR(200) NOT NULL,
	Sobrenome NVARCHAR(200) NOT NULL,
	Email NVARCHAR(200),
	DataCriacao DATETIME DEFAULT(GETDATE()),
	UsuarioStatusID INT NOT NULL,
	CONSTRAINT [PK_tblUsuario_UsuarioID] PRIMARY KEY(UsuarioID),
	CONSTRAINT [FK_tblUsuario_tblUsuarioStatus_UsuarioStatusID] FOREIGN KEY(UsuarioStatusID) REFERENCES tblUsuarioStatus(UsuarioStatusID)
)

INSERT INTO tblUsuarioStatus(Nome)
VALUES
	('Ativo'),
	('Bloqueado'),
	('Removido')

INSERT INTO tblUsuario(Nome, Sobrenome, UsuarioStatusID, Email)
VALUES
	('Mayara', 'Toku', 1, 'mayara.toku@pubdev.com'),
	(N'João', 'Augusto', 2, 'joao.augusto@pubdev.com'),
	('Alef', 'Carlos', 3, 'alef.carlos@pubdev.com')

CREATE TABLE tblCor
(
	CorID INT IDENTITY(1,1),
	Nome NVARCHAR(50) NOT NULL,	
	DataCriacao DATETIME DEFAULT(GETDATE()),
	CONSTRAINT [PK_tblCor_CorID] PRIMARY KEY(CorID)
)

CREATE TABLE tblVeiculoTipo
(
	VeiculoTipoID INT IDENTITY(1,1),
	Nome NVARCHAR(200) NOT NULL,	
	DataCriacao DATETIME DEFAULT(GETDATE()),
	CONSTRAINT [PK_tblVeiculoTipo_VeiculoTipoID] PRIMARY KEY(VeiculoTipoID)
)

CREATE TABLE tblVeiculo
(
	VeiculoID INT IDENTITY(1,1),
	Placa NVARCHAR(20) NOT NULL,
	CorID INT NOT NULL,
	VeiculoTipoID INT NOT NULL,
	DataCriacao DATETIME DEFAULT(GETDATE()),
	CONSTRAINT [PK_tblVeiculo_VeiculoID] PRIMARY KEY(VeiculoID),
	CONSTRAINT [UK_tblVeiculo_Placa] UNIQUE(Placa),
	CONSTRAINT [FK_tblVeiculo_tblCor_CorID] FOREIGN KEY(CorID) REFERENCES tblCor(CorID),
	CONSTRAINT [FK_tblVeiculo_tblVeiculoTipo_VeiculoTipoID] FOREIGN KEY(VeiculoTipoID) REFERENCES tblVeiculoTipo(VeiculoTipoID)
)

INSERT INTO tblCor(Nome)
VALUES
	('Vermelho'),
	('Azul'),
	('Preto'),
	('Cinza')

INSERT INTO tblVeiculoTipo(Nome)
VALUES
	(N'Carro'),
	(N'Moto'),
	(N'Caminhão')

INSERT INTO tblVeiculo(Placa,CorID,VeiculoTipoID)
VALUES
	('QTP5F71',1,1),
	('BQZ5Z48',2,2),
	('QAA5T47',4,3)

CREATE TABLE tblPedido
(
	PedidoID INT IDENTITY(1,1) NOT NULL,
	Pago BIT DEFAULT(0) NOT NULL,
	DataCriacao DATETIME DEFAULT(GETDATE()),
	CONSTRAINT [PK_tblPedido_PedidoID] PRIMARY KEY(PedidoID)
)

CREATE TABLE tblPedidoItem
(
	PedidoItemID INT IDENTITY(1,1) NOT NULL,
	PedidoID INT NOT NULL,
	Item NVARCHAR(500) NOT NULL,
	ValorUnitario DECIMAL(18,2) NOT NULL,
	Quantidade INT NOT NULL,
	DataCriacao DATETIME DEFAULT(GETDATE()),
	CONSTRAINT [PK_tblPedidoItem_PedidoItemID] PRIMARY KEY(PedidoItemID),
	CONSTRAINT [FK_tblPedidoItem_tblPedido_PedidoID] FOREIGN KEY(PedidoID) REFERENCES tblPedido(PedidoID)
)

INSERT INTO tblPedido(Pago)
VALUES
	(1),
	(0)

INSERT INTO tblPedidoItem(PedidoID,Item,ValorUnitario,Quantidade)
VALUES
	(1, 'Camiseta PubDev - Deve ser CACHE', 69.99, 1),
	(1, 'Camiseta PubDev - Tá Pronto! Só Falta Testar...', 69.99, 3),
	(1, 'Camiseta PubDev - O meu código esta compilando', 69.99, 1),
	(2, 'Camiseta PubDev - CSS é Incrivel', 69.99, 20),
	(2, 'Camiseta PubDev - Update Sem Where', 69.99, 5)