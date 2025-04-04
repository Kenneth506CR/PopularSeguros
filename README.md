# API en ASP.NET con Entity Framework, SQL Server y JWT

## Descripción

Este proyecto es una API RESTful desarrollada en ASP.NET Core que permite realizar operaciones CRUD sobre una base de datos SQL Server utilizando Entity Framework. Además, se utiliza JWT (JSON Web Token) para la autenticación y autorización de los usuarios.

## Tecnologías utilizadas

- **ASP.NET Core**: Framework principal para el desarrollo de la API.
- **Entity Framework Core**: ORM para interactuar con SQL Server.
- **SQL Server**: Base de datos relacional.
- **JWT (JSON Web Token)**: Mecanismo de autenticación y autorización.
- **Swagger**: Herramienta para documentar y probar la API.

## Requisitos

- **.NET 8**
- **SQL Server** 
- **Postman o cualquier cliente HTTP** para probar la API.
- **Visual Studio o Visual Studio Code** para desarrollo.

## Script de Base de datos

CREATE DATABASE db_polizas;
GO

USE db_polizas;
GO


CREATE TABLE tipoPoliza (
    idTipoPoliza INTEGER PRIMARY KEY IDENTITY(1,1),  
    tipoPoliza VARCHAR(100) NOT NULL UNIQUE          
);
GO

CREATE TABLE coberturas (
    idCobertura INTEGER PRIMARY KEY IDENTITY(1,1), 
    cobertura VARCHAR(100) NOT NULL UNIQUE           
);
GO

CREATE TABLE estadoPoliza (
    idEstadoPoliza INTEGER PRIMARY KEY IDENTITY(1,1),  
    estadoPoliza VARCHAR(100) NOT NULL UNIQUE          
);
GO

CREATE TABLE aseguradoras (
    idAseguradora INTEGER PRIMARY KEY IDENTITY(1,1),  
    aseguradora VARCHAR(100) NOT NULL UNIQUE           
);
GO

CREATE TABLE cliente (
    cedulaAsegurado VARCHAR(20) PRIMARY KEY,   
    nombre VARCHAR(100) NOT NULL,                
    primerApellido VARCHAR(50) NOT NULL,      
    segundoApellido VARCHAR(50),              
    tipoPersona VARCHAR(20),                 
    fechaNacimiento DATE NOT NULL              
);
GO

CREATE TABLE polizas (
    numeroPoliza VARCHAR(50) PRIMARY KEY,                
    idTipoPoliza INTEGER,                         
    cedulaAsegurado VARCHAR(20) NOT NULL,                
    montoAsegurado NUMERIC NOT NULL,              
    fechaVencimiento DATE NOT NULL,                
    fechaEmision DATE NOT NULL,                    
    prima NUMERIC NOT NULL,                         
    periodo DATE NOT NULL,                          
    fechaInclusion DATE NOT NULL,                   
    idAseguradora INTEGER,                          
    idCobertura INTEGER,                            
    idEstadoPoliza INTEGER,                        
    FOREIGN KEY (cedulaAsegurado) REFERENCES cliente(cedulaAsegurado) ON DELETE CASCADE,
    FOREIGN KEY (idTipoPoliza) REFERENCES tipoPoliza(idTipoPoliza),
    FOREIGN KEY (idAseguradora) REFERENCES aseguradoras(idAseguradora),
    FOREIGN KEY (idCobertura) REFERENCES coberturas(idCobertura),
    FOREIGN KEY (idEstadoPoliza) REFERENCES estadoPoliza(idEstadoPoliza)
);
GO


CREATE TABLE usuarios (
    idUsuario INTEGER PRIMARY KEY IDENTITY(1,1),  
    usuario VARCHAR(50) NOT NULL UNIQUE,                   
    contrasena VARCHAR(255) NOT NULL,                       
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE()  
);
GO


INSERT INTO usuarios (usuario, contrasena)
VALUES ('admin', 'admin');
GO


INSERT INTO aseguradoras (aseguradora)
VALUES 
    ('Popular Seguros'),
    ('INS'),
    ('CCSS');
GO


INSERT INTO tipoPoliza (tipoPoliza)
VALUES 
    ('Vida'),
    ('Automóvil'),
    ('Hogar');
GO


INSERT INTO coberturas (cobertura)
VALUES 
    ('Accidente'),
    ('Robo'),
    ('Incendio');
GO


INSERT INTO estadoPoliza (estadoPoliza)
VALUES 
    ('Activo'),
    ('Cancelado'),
    ('Vencido');
GO

INSERT INTO cliente (cedulaAsegurado, nombre, primerApellido, segundoApellido, tipoPersona, fechaNacimiento)
VALUES 
    ('101230456', 'Juan', 'Pérez', 'Gómez', 'Natural', '1985-06-15'),
    ('202340789', 'María', 'Fernández', 'Rodríguez', 'Natural', '1990-09-21'),
    ('303450123', 'Carlos', 'Gutiérrez', 'López', 'Natural', '1978-04-10');
GO


INSERT INTO polizas (numeroPoliza, idTipoPoliza, cedulaAsegurado, montoAsegurado, fechaVencimiento, fechaEmision, prima, periodo, fechaInclusion, idAseguradora, idCobertura, idEstadoPoliza)
VALUES 
    ('POL12345', 1, '101230456', 50000, '2026-06-15', '2024-06-15', 500, '2024-06-15', '2024-06-15', 1, 1, 1), 
    ('POL67890', 2, '202340789', 30000, '2025-09-21', '2023-09-21', 350, '2023-09-21', '2023-09-21', 2, 2, 1),
    ('POL54321', 3, '303450123', 80000, '2027-04-10', '2025-04-10', 700, '2025-04-10', '2025-04-10', 3, 3, 2);
GO

SELECT *
FROM Cliente

SELECT * 
FROM polizas p
LEFT JOIN aseguradoras a ON p.idAseguradora = a.idAseguradora
LEFT JOIN tipoPoliza t ON p.idTipoPoliza = t.idTipoPoliza
LEFT JOIN estadoPoliza e ON p.idEstadoPoliza = e.idEstadoPoliza
LEFT JOIN coberturas c ON p.idCobertura = c.idCobertura
WHERE p.numeroPoliza = 'POL123456'


SELECT p.numeroPoliza, t.tipoPoliza, a.aseguradora, c.cobertura, e.estadoPoliza
FROM polizas p
LEFT JOIN tipoPoliza t ON p.idTipoPoliza = t.idTipoPoliza
LEFT JOIN aseguradoras a ON p.idAseguradora = a.idAseguradora
LEFT JOIN coberturas c ON p.idCobertura = c.idCobertura
LEFT JOIN estadoPoliza e ON p.idEstadoPoliza = e.idEstadoPoliza;

INSERT INTO cliente (cedulaAsegurado, nombre, primerApellido, segundoApellido, tipoPersona, fechaNacimiento)
VALUES 
    ('504260921', 'Kenneth', 'Rodríguez', 'Carvajal', 'Física', '1999-04-27');
GO

SELECT NEWID();

GO
INSERT INTO usuarios (usuario, contrasena, fechaRegistro)
VALUES ('ADMIN', 'Popular2024@' , GETDATE())

SELECT * FROM Usuarios WHERE Usuario = 'ADMIN';
