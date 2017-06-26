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
	  DECLARE @NombreUsuario VARCHAR(50)
	  DECLARE @ApellidoUsuario VARCHAR(50)
	  SELECT @Usuario_No_Habilitado = -2
	  SELECT @Usuario_No_Existe = -1
	  SELECT @Usuario_Clave_Incorrecta = -3
	  SELECT @Habilitado = 1
	  SELECT @No_Habilitado = 0

      SELECT @Usu_Id = Usu_Id
		FROM [DESCONOCIDOS4].Usuario WHERE Usu_Nombre_Usuario = @Usuario

	  SELECT @NombreUsuario = Persona_Nombre, @ApellidoUsuario = Persona_Apellido
      FROM [DESCONOCIDOS4].Usuario U JOIN [DESCONOCIDOS4].PERSONA P ON U.Usu_Per_Id = P.Persona_Id WHERE U.Usu_Id = @Usu_Id 

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
					SELECT @Usu_Id [codigoUsuario], Rol_Id, Rol_Nombre,@NombreUsuario Nombre,@ApellidoUsuario Apellido FROM [DESCONOCIDOS4].USUARIO_ROL left join [DESCONOCIDOS4].ROL on UsuRol_Rol_Id=Rol_Id
                              WHERE Rol_Habilitado=@Habilitado and UsuRol_Usu_Id=@Usu_Id
				END
				ELSE
				BEGIN
					-- Usuario Existe, Clave Correcta y No Habilitado
					SELECT @Usuario_No_Habilitado [UserId], -1 Rol_Id, '' Rol_Nombre, NULL Nombre, NULL Apellido
				END
            END
            ELSE
            BEGIN
				-- Usuario Existe y Clave Incorrecta
				-- Actualiza Intentos Fallidos de ingreso
				UPDATE [DESCONOCIDOS4].USUARIO SET Usu_cantIntentosLoginFallidos=
													([DESCONOCIDOS4].FN_OBTENER_CANTIDAD_INTENTOS_FALLIDOS_DE_INGRESO(@Usu_Id)) + 1
												WHERE Usu_Id=@Usu_Id
				SELECT @Usuario_Clave_Incorrecta [UserId], -1 Rol_Id, '' Rol_Nombre, NULL Nombre, NULL Apellido
            END
      END
      ELSE
      BEGIN
		-- Usuario NO EXISTE
        SELECT @Usuario_No_Existe [UserId], -1 Rol_Id, '' Rol_Nombre, NULL Nombre, NULL Apellido
      END
END

IF OBJECT_ID (N'[DESCONOCIDOS4].PRC_OBTENER_DATOS_USUARIOS', N'P') IS NOT NULL
		DROP PROCEDURE  [DESCONOCIDOS4].PRC_OBTENER_DATOS_USUARIOS;
GO
CREATE PROC [DESCONOCIDOS4].PRC_OBTENER_DATOS_USUARIOS
(
	@TipoUsuario VARCHAR(10)
)
AS
BEGIN
IF (@TipoUsuario = 'Cliente')
	BEGIN
		SELECT USUARIO.Usu_Nombre_Usuario, CLIENTE.Cliente_Id, PERSONA.* FROM DESCONOCIDOS4.CLIENTE LEFT JOIN DESCONOCIDOS4.PERSONA ON Cliente_Per_ID=Persona_Id LEFT JOIN DESCONOCIDOS4.USUARIO ON Usu_Per_Id=Persona_Id
	END
ELSE
	BEGIN
		SELECT USUARIO.Usu_Nombre_Usuario, CHOFER.Chofer_Id, PERSONA.* FROM DESCONOCIDOS4.CHOFER LEFT JOIN DESCONOCIDOS4.PERSONA ON Chofer_Per_ID=Persona_Id LEFT JOIN DESCONOCIDOS4.USUARIO ON Usu_Per_Id=Persona_Id
	END
END