Create database pruebaScisa;

go

use pruebaScisa;

CREATE TABLE Usuario(Id UNIQUEIDENTIFIER, Nombre varchar(60),
ApellidoPaterno varchar(60),ApellidoMaterno varchar(60), Contrase Varchar(MAX),
Activo bit,UsuarioNom Varchar(100),PRIMARY KEY (Id));

CREATE TABLE Doctor(Id UNIQUEIDENTIFIER, Nombre varchar(60),
ApellidoPaterno varchar(60),ApellidoMaterno varchar(60), Especialidad varchar(100),
Departamento varchar(100), NumCedula varchar(100),PRIMARY KEY (Id));

CREATE TABLE Paciente(Id UNIQUEIDENTIFIER, Nombre varchar(60),
ApellidoPaterno varchar(60),ApellidoMaterno varchar(60), Edad Integer,
Peso float, Sexo varchar(30),PRIMARY KEY (Id) );


CREATE TABLE Cita(Id UNIQUEIDENTIFIER, IdPaciente UNIQUEIDENTIFIER,
IdDoctor UNIQUEIDENTIFIER,FechaCita DATE,
Comentarios VARCHAR(MAX),Horario varchar(50),PRIMARY KEY (Id),
FOREIGN KEY (IdPaciente) REFERENCES Paciente(Id),
FOREIGN KEY (IdDoctor) REFERENCES Doctor(Id));



--Procedimientos

CREATE PROCEDURE ConsultaDoctores(@NUM_PAGINA integer, @TAM_PAGINA integer )
AS
BEGIN

	SET NOCOUNT ON;

   SELECT * FROM Doctor
ORDER BY Id
OFFSET (@NUM_PAGINA-1)*@TAM_PAGINA ROWS
FETCH NEXT @TAM_PAGINA ROWS ONLY
END
GO



CREATE PROCEDURE ConsultaCitas(@NUM_PAGINA integer, @TAM_PAGINA integer )
AS
BEGIN

	SET NOCOUNT ON;
	SELECT c.Id,p.Id,p.Nombre,d.Id,d.Nombre,c.Comentarios,c.FechaCita FROM Doctor d  inner join Cita c on c.IdDoctor = d.Id
inner join Paciente p on p.Id = c.IdPaciente ORDER BY c.FechaCita OFFSET (@NUM_PAGINA-1)*@TAM_PAGINA ROWS
FETCH NEXT @TAM_PAGINA ROWS ONLY
END
GO




CREATE PROCEDURE ConsultaPacientes(@NUM_PAGINA integer, @TAM_PAGINA integer )
AS
BEGIN

	SET NOCOUNT ON;

   SELECT * FROM Paciente
ORDER BY Id
OFFSET (@NUM_PAGINA-1)*@TAM_PAGINA ROWS
FETCH NEXT @TAM_PAGINA ROWS ONLY
END
GO


