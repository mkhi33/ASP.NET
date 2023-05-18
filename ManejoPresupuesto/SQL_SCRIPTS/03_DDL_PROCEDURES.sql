CREATE PROCEDURE dbo.CrearDatosUsuarioNuevo
	@UsuarioId INT
AS 
BEGIN
	DECLARE @Efectivo nvarchar(50) = 'Efectivo';
	DECLARE @CuentasDeBancos nvarchar(50) = 'Cuentas de Banco';
	DECLARE @Tarjetas nvarchar(50) = 'Tarjetas';

	INSERT INTO TiposCuentas(Nombre, UsuarioId, Orden) VALUES 
		(@Efectivo, @UsuarioId, 1),
		(@CuentasDeBancos, @UsuarioId, 2),
		(@Tarjetas, @UsuarioId, 3);

	INSERT INTO Cuentas(Nombre, Balance, TipoCuentaId)
	SELECT Nombre, 0, Id FROM TiposCuentas WHERE UsuarioId = @UsuarioId;

	INSERT INTO Categorias(Nombre, TipoOperacionId, UsuarioId) VALUES
		('Libros', 2, @UsuarioId),
		('Salario', 2, @UsuarioId),
		('Mesada', 2, @UsuarioId),
		('Comida', 2, @UsuarioId);
		
END;

CREATE PROCEDURE dbo.TiposCuentas_Insertar
	@Nombre nvarchar(50),
	@UsuarioId int
AS

BEGIN
	DECLARE @Orden int;
	SELECT @Orden = COALESCE(MAX(Orden),0)+ 1 FROM TiposCuentas WHERE UsuarioId = @UsuarioId;
	
	INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden) VALUES(@Nombre, @UsuarioId, @Orden);
	SELECT SCOPE_IDENTITY();
END;

CREATE PROCEDURE dbo.Transacciones_Actualizar
	@Id int,
	@FechaTransaccion DATETIME,
	@Monto decimal(18,2),
	@MontoAnterior decimal(18,2),
	@CuentaId int,
	@CuentaAnteriorId int,
	@CategoriaId int,
	@Nota nvarchar(1000) = NULL
AS 
	BEGIN
		-- Revertir transaccón anterior
		UPDATE Cuentas
			SET Balance -= @MontoAnterior
		WHERE Id = @CuentaAnteriorId;
	
		-- Realizar la nueva transacción
		UPDATE Cuentas
			SET Balance += @Monto
		WHERE Id = @CuentaId;
	
		UPDATE Transacciones
			SET Monto = ABS(@Monto), FechaTransaccion = @FechaTransaccion, CategoriaId = @CategoriaId, CuentaId = @CuentaId, Nota = @Nota
		WHERE Id = @Id;
	END
	
SELECT 1;

CREATE PROCEDURE dbo.Transacciones_Borrar
	@Id int
AS 
	BEGIN
		DECLARE @Monto DECIMAL(18,2);
		DECLARE @CuentaId int;
		DECLARE @TipoOperacionId int;
			
		SELECT @Monto = Monto, @CuentaId = CuentaId, @TipoOperacionId = cat.TipoOperacionId FROM Transacciones
			INNER JOIN Categorias cat
				ON cat.Id = Transacciones.CategoriaId
			WHERE Transacciones.Id = @Id;
		
		DECLARE @FactorMultiplicativo int = 1;
		
		IF(@TipoOperacionId = 2)
			SET @FactorMultiplicativo = -1;
		SET @Monto = @Monto * @FactorMultiplicativo;
	
		UPDATE Cuentas
			SET Balance -= @Monto 
		WHERE Id = @CuentaId;
	
		DELETE Transacciones WHERE Id = @Id;
			
	END;

CREATE PROCEDURE dbo.Transacciones_Insertar
	@UsuarioId int,
	@FechaTransaccion date,
	@Monto decimal(18,2),
	@CategoriaId int,
	@CuentaId int,
	@Nota nvarchar(1000) = NULL
AS 
BEGIN
	
	INSERT INTO Transacciones(UsuarioId,FechaTransaccion, Monto, CategoriaId, CuentaId, Nota ) VALUES
	(@UsuarioId, @FechaTransaccion, ABS(@Monto), @CategoriaId, @CuentaId, @Nota);

	UPDATE Cuentas
		SET BALANCE += @Monto
	WHERE Id = @CuentaId;

	SELECT SCOPE_IDENTITY();
END;
