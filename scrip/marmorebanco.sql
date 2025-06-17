CREATE DATABASE MarmorariaDB;
USE MarmorariaDB;

-- Tabela de usuários
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioNome VARCHAR(50) NOT NULL,
    Senha VARCHAR(50) NOT NULL
);

-- Inserção de usuário padrão
INSERT INTO Usuarios (UsuarioNome, Senha)
VALUES ('admin', 'admin123');

-- Tabela de blocos
CREATE TABLE Blocos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Codigo VARCHAR(20) NOT NULL,
    PedreiraOrigem VARCHAR(50),
    MedidaM3 FLOAT,
    TipoMaterial VARCHAR(50),
    ValorCompra DECIMAL(10,2),
    NotaFiscal VARCHAR(20)
);

-- Tabela de chapas
CREATE TABLE Chapas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BlocoId INT FOREIGN KEY REFERENCES Blocos(Id),
    TipoMaterial VARCHAR(50),
    MedidaLargura FLOAT,
    MedidaAltura FLOAT,
    Valor DECIMAL(10,2)
);
