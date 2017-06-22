IF OBJECT_ID('[DESCONOCIDOS4].FN_OBTENER_CANTIDAD_INTENTOS_FALLIDOS_DE_INGRESO','FN') IS NOT NULL
	DROP FUNCTION [DESCONOCIDOS4].FN_OBTENER_CANTIDAD_INTENTOS_FALLIDOS_DE_INGRESO;
GO

CREATE FUNCTION [DESCONOCIDOS4].FN_OBTENER_CANTIDAD_INTENTOS_FALLIDOS_DE_INGRESO(@Usu_Id INT)
RETURNS INT
AS
BEGIN
RETURN (SELECT Usu_cantIntentosLoginFallidos FROM [DESCONOCIDOS4].USUARIO WHERE Usu_Id=@Usu_Id)
END
GO
IF OBJECT_ID (N'[DESCONOCIDOS4].TR_ACTUALIZAR_INTENTOS_FALLIDOS', N'TR') IS NOT NULL
		DROP TRIGGER  [DESCONOCIDOS4].TR_ACTUALIZAR_INTENTOS_FALLIDOS;
GO

CREATE TRIGGER  [DESCONOCIDOS4].TR_ACTUALIZAR_INTENTOS_FALLIDOS ON [DESCONOCIDOS4].USUARIO
AFTER UPDATE
AS
BEGIN
TRANSACTION
	DECLARE @Usu_Id INT
	SELECT @Usu_Id = (SELECT I.Usu_Id FROM INSERTED I)
	IF ([DESCONOCIDOS4].FN_OBTENER_CANTIDAD_INTENTOS_FALLIDOS_DE_INGRESO(@Usu_Id)) = 3
	BEGIN
		UPDATE [DESCONOCIDOS4].USUARIO SET Usu_cantIntentosLoginFallidos = 0, Usu_Habilitado = 0 WHERE Usu_Id=@Usu_Id
	END
COMMIT
GO

IF OBJECT_ID (N'[DESCONOCIDOS4].PRC_VALIDAR_USUARIO', N'P') IS NOT NULL
		DROP PROCEDURE  [DESCONOCIDOS4].PRC_VALIDAR_USUARIO;
GO
CREATE PROCEDURE [DESCONOCIDOS4].PRC_VALIDAR_USUARIO
      @Usuario NVARCHAR(255),
      @Clave NVARCHAR(255)
AS
BEGIN
      SET NOCOUNT ON;
	  DECLARE @Habilitado BIT
	  DECLARE @No_Habilitado BIT
      DECLARE @Usu_Id INT
	  DECLARE @Usuario_No_Existe INT
      DECLARE @Usuario_No_Habilitado INT
	  DECLARE @Usuario_Clave_Incorrecta INT
	  SELECT @Usuario_No_Habilitado = -2
	  SELECT @Usuario_No_Existe = -1
	  SELECT @Usuario_Clave_Incorrecta = -3
	  SELECT @Habilitado = 1
	  SELECT @No_Habilitado = 0

      SELECT @Usu_Id = Usu_Id
      FROM [DESCONOCIDOS4].Usuario WHERE Usu_Nombre_Usuario = @Usuario

      IF @Usu_Id IS NOT NULL
	  -- Usuario Existe
      BEGIN
			IF EXISTS(SELECT Usu_Id FROM [DESCONOCIDOS4].Usuario WHERE Usu_Nombre_Usuario = @Usuario AND Usu_Password = @Clave)
			-- Usuario Existe y Clave Correcta
            BEGIN
                IF EXISTS(SELECT Usu_Id FROM [DESCONOCIDOS4].Usuario WHERE Usu_Id = @Usu_Id AND Usu_Habilitado=@Habilitado)
                BEGIN
				-- Usuario Existe, Clave Correcta y Habilitado
					IF ([DESCONOCIDOS4].FN_OBTENER_CANTIDAD_INTENTOS_FALLIDOS_DE_INGRESO(@Usu_Id)) != 0
					BEGIN
						-- Actualiza Intentos Fallidos de ingreso, en caso que sea necesario
						UPDATE [DESCONOCIDOS4].USUARIO SET Usu_cantIntentosLoginFallidos=0 WHERE Usu_Id=@Usu_Id
					END
					SELECT @Usu_Id [codigoUsuario], Rol_Id, Rol_Nombre FROM [DESCONOCIDOS4].USUARIO_ROL left join [DESCONOCIDOS4].ROL on UsuRol_Rol_Id=Rol_Id
                              WHERE Rol_Habilitado=@Habilitado and UsuRol_Usu_Id=@Usu_Id
				END
				ELSE
				BEGIN
					-- Usuario Existe, Clave Correcta y No Habilitado
					SELECT @Usuario_No_Habilitado [UserId], -1 Rol_Id, '' Rol_Nombre
				END
            END
            ELSE
            BEGIN
				-- Usuario Existe y Clave Incorrecta
				-- Actualiza Intentos Fallidos de ingreso
				UPDATE [DESCONOCIDOS4].USUARIO SET Usu_cantIntentosLoginFallidos=
													([DESCONOCIDOS4].FN_OBTENER_CANTIDAD_INTENTOS_FALLIDOS_DE_INGRESO(@Usu_Id)) + 1
												WHERE Usu_Id=@Usu_Id
				SELECT @Usuario_Clave_Incorrecta [UserId], -1 Rol_Id, '' Rol_Nombre
            END
      END
      ELSE
      BEGIN
		-- Usuario NO EXISTE
        SELECT @Usuario_No_Existe [Us	erId], -1 Rol_Id, '' Rol_Nombre
      END
END






SELECT 12 [codigoUsuario], Rol_Id, Rol_Nombre FROM [DESCONOCIDOS4].USUARIO_ROL left join [DESCONOCIDOS4].ROL on UsuRol_Rol_Id=Rol_Id
                              WHERE Rol_Habilitado=1 and UsuRol_Usu_Id=12
							  go
							  select * from [DESCONOCIDOS4].usuario where Usu_Id=88
							  select * from [DESCONOCIDOS4].USUARIO_rol where UsuRol_Usu_Id=88
							  update [DESCONOCIDOS4].USUARIO set Usu_Habilitado=1 where Usu_Id=89
							  INSERT INTO [DESCONOCIDOS4].USUARIO_ROL(UsuRol_Usu_Id,UsuRol_Rol_Id) values(88,2)
							 select CONVERT(VARCHAR(256),HashBytes('SHA2_256', 'Inicio2017'),2)
EXEC [DESCONOCIDOS4].PRC_VALIDAR_USUARIO 'JiméZAH', '4EC9FABADDF3198934AE53B25B3EB01710809A301E4F5C9A3E5D54CB82286AB0'
EXEC [DESCONOCIDOS4].PRC_VALIDAR_USUARIO 'JiméZAH', 'Inicio2017'