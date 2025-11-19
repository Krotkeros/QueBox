-- Crear base de datos
CREATE DATABASE QueBox;
GO

USE QueBox;
-- Crear tabla Usuario
CREATE TABLE Usuario (
    Id_Usuario INT PRIMARY KEY IDENTITY(1,1),  -- IDENTITY para auto incremento
    Nombre VARCHAR(100) NOT NULL,
    Clave VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) NOT NULL UNIQUE
);
GO

-- Crear tabla Diseno
CREATE TABLE Diseno (
    Id_Diseno INT PRIMARY KEY IDENTITY(1,1),  -- IDENTITY para auto incremento
    Id_Usuario INT,
    Largo FLOAT NOT NULL,
    Alto FLOAT NOT NULL,
    Ancho FLOAT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id_Usuario)
);
GO

-- Crear tabla Capa
CREATE TABLE Capa (
    Id_Capa INT PRIMARY KEY IDENTITY(1,1),  -- IDENTITY para auto incremento
    Id_Diseno INT,
    Numero INT NOT NULL,
    FOREIGN KEY (Id_Diseno) REFERENCES Diseno(Id_Diseno)
);
GO

-- Crear tabla ImagenDecorativa
CREATE TABLE ImagenDecorativa (
    Id_Img INT PRIMARY KEY IDENTITY(1,1),  -- IDENTITY para auto incremento
    Id_Capa INT,
    Ancho FLOAT,
    Alto FLOAT,
    Url VARCHAR(200) NOT NULL UNIQUE,
    FOREIGN KEY (Id_Capa) REFERENCES Capa(Id_Capa)
);
GO

DELIMITER //

CREATE TRIGGER after_diseno_insert
AFTER INSERT ON Diseno
FOR EACH ROW
BEGIN
    INSERT INTO Capa (Id_Diseno, Numero)
    VALUES (NEW.column_name_in_table1);
END;
//

DELIMITER ;


CREATE TRIGGER TR_Diseno_AfterInsert
ON Diseno
AFTER INSERT
AS
BEGIN
    -- Insert data into YourSecondaryTable using values from the 'inserted' table
    -- Capa 1 
    INSERT INTO Capa (Id_Diseno, Numero)
    SELECT Id_Diseno, 1
    FROM inserted;

    -- Capa 2
    INSERT INTO Capa (Id_Diseno, Numero)
    SELECT Id_Diseno, 2
    FROM inserted;

    -- Capa 3
    INSERT INTO Capa (Id_Diseno, Numero)
    SELECT Id_Diseno, 3
    FROM inserted;

    -- Capa 4
    INSERT INTO Capa (Id_Diseno, Numero)
    SELECT Id_Diseno, 4
    FROM inserted;
END;