-- Base De Datos:
Create Database Practica02NARABD;

-- Usar BD:
Use Practica02NARABD;

-- Tabla 1:
Create Table Directores
(
IdDirector int Identity Primary Key,
NombreDirector Varchar(200) not null
);

-- Tabla 2:
Create Table Peliculas
(
Id int Identity Primary Key,
NombrePelicula Varchar(200) not null,
Genero Varchar(200) not null,
AñoPublicada Varchar(200) not null,
ImagenPelicula image,
DirectorPelicula int not null,
Foreign Key(DirectorPelicula) References Directores(IdDirector) 
);
