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
