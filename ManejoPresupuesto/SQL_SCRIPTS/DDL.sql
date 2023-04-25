-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- ManejoPresupuesto.dbo.TiposOperaciones definition

-- Drop table

-- DROP TABLE ManejoPresupuesto.dbo.TiposOperaciones;

CREATE TABLE TiposOperaciones (
	Id int IDENTITY(0,1) NOT NULL,
	Descripcion nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT TiposOperaciones_PK PRIMARY KEY (Id)
);


-- ManejoPresupuesto.dbo.Usuarios definition

-- Drop table

-- DROP TABLE ManejoPresupuesto.dbo.Usuarios;

CREATE TABLE Usuarios (
	Id int IDENTITY(1,1) NOT NULL,
	EmailNormalizado nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	PasswordHash nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT Usuarios_PK PRIMARY KEY (Id)
);


-- ManejoPresupuesto.dbo.Categorias definition

-- Drop table

-- DROP TABLE ManejoPresupuesto.dbo.Categorias;

CREATE TABLE Categorias (
	Id int IDENTITY(0,1) NOT NULL,
	TipoOperacionId int NOT NULL,
	UsuarioId int NOT NULL,
	CONSTRAINT Categorias_PK PRIMARY KEY (Id),
	CONSTRAINT Categorias_TipoOperaciones_FK FOREIGN KEY (TipoOperacionId) REFERENCES TiposOperaciones(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT Categorias_Usuarios_FK FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE ON UPDATE CASCADE
);


-- ManejoPresupuesto.dbo.TiposCuentas definition

-- Drop table

-- DROP TABLE ManejoPresupuesto.dbo.TiposCuentas;

CREATE TABLE TiposCuentas (
	Id int IDENTITY(0,1) NOT NULL,
	Nombre nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	UsuarioId int NOT NULL,
	Orden int NOT NULL,
	CONSTRAINT TiposCuentas_PK PRIMARY KEY (Id),
	CONSTRAINT TiposCuentas_FK FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE ON UPDATE CASCADE
);


-- ManejoPresupuesto.dbo.Cuentas definition

-- Drop table

-- DROP TABLE ManejoPresupuesto.dbo.Cuentas;

CREATE TABLE Cuentas (
	Id int IDENTITY(0,1) NOT NULL,
	Nombre nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	TipoCuentaId int NOT NULL,
	Balance decimal(18,2) NOT NULL,
	Descripcion nvarchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT Cuentas_PK PRIMARY KEY (Id),
	CONSTRAINT Cuentas_FK FOREIGN KEY (TipoCuentaId) REFERENCES TiposCuentas(Id) ON DELETE CASCADE ON UPDATE CASCADE
);


-- ManejoPresupuesto.dbo.Transacciones definition

-- Drop table

-- DROP TABLE ManejoPresupuesto.dbo.Transacciones;

CREATE TABLE Transacciones (
	Id int IDENTITY(0,1) NOT NULL,
	UsuarioId int NOT NULL,
	FechaTransaccion datetime NOT NULL,
	Monto decimal(18,2) NOT NULL,
	TipoTransaccionId int NOT NULL,
	Nota nvarchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CuentaId int NOT NULL,
	CategoriaId int NOT NULL,
	CONSTRAINT transacciones_PK PRIMARY KEY (Id),
	CONSTRAINT Transacciones_Categorias_FK FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id),
	CONSTRAINT Transacciones_Cuentas_FK FOREIGN KEY (CuentaId) REFERENCES Cuentas(Id),
	CONSTRAINT Transacciones_FK FOREIGN KEY (TipoTransaccionId) REFERENCES TiposOperaciones(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT Transacciones_Usuarios_FK FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE ON UPDATE CASCADE
);