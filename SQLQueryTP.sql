
USE [GD1C2017]
IF NOT EXISTS (
SELECT schema_name
FROM information_schema.SCHEMATA
WHERE schema_name = 'DESCONOCIDOS4' )
BEGIN
EXEC sp_executesql N'CREATE SCHEMA DESCONOCIDOS4'
END
GO

/*---------Limpieza de Tablas-------------*/
IF OBJECT_ID('DESCONOCIDOS4.ITEM_RENDICION') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.ITEM_RENDICION ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.ITEM_FACTURA') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.ITEM_FACTURA ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.CABECERO_RENDICION') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.CABECERO_RENDICION ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.CABECERO_FACTURA') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.CABECERO_FACTURA ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.VIAJE') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.VIAJE ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.UNIDAD_DISPONIBLE') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.UNIDAD_DISPONIBLE ;
END
GO
IF OBJECT_ID('DESCONOCIDOS4.USUARIO') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.USUARIO ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.CHOFER') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.CHOFER ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.CLIENTE') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.CLIENTE ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.PERSONA') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.PERSONA ;
END;
GO

IF OBJECT_ID('DESCONOCIDOS4.TURNO') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.TURNO ;
END
GO
IF OBJECT_ID('DESCONOCIDOS4.MARCA_MODELO') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.MARCA_MODELO ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.MARCA') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.MARCA ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.MODELO') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.MODELO ;
END;
GO


IF OBJECT_ID('DESCONOCIDOS4.AUTO') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.AUTO ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.USUARIO_ROL') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.USUARIO_ROL ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.FUNCIONALIDADXROL') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.FUNCIONALIDADXROL ;
END;
GO
IF OBJECT_ID('DESCONOCIDOS4.ROL') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.ROL ;
END;
GO

IF OBJECT_ID('DESCONOCIDOS4.FUNCIONALIDAD') IS NOT NULL
BEGIN
DROP TABLE DESCONOCIDOS4.FUNCIONALIDAD ;
END;
GO

/*---------Definiciones de Tabla-------------*/
CREATE TABLE [DESCONOCIDOS4].PERSONA(
Persona_Id INT IDENTITY(1,1) NOT NULL,
Persona_Dni NUMERIC(18,0) NOT NULL,
Persona_Nombre VARCHAR(255)NOT NULL,
Persona_Apellido VARCHAR(255) NOT NULL,
Persona_Direccion VARCHAR(255) NOT NULL,
Persona_Piso SMALLINT NOT NULL,
Persona_Dartamento  VARCHAR(255) NOT NULL,
Persona_Localidad  VARCHAR(255) NOT NULL,
Persona_Telefono NUMERIC(18,0) NOT NULL,
Persona_Mail VARCHAR(255) NOT NULL,
Persona_Fecha_Nac DATETIME NOT NULL,
PRIMARY KEY (Persona_Id)
);
GO
CREATE TABLE [DESCONOCIDOS4].USUARIO(
Usu_Id INT IDENTITY(1,1) NOT NULL,
Usu_Per_Id INT  NOT NULL,
Usu_Nombre_Usuario VARCHAR (255) NOT NULL,
Usu_Password VARCHAR(255) NOT NULL,
Usu_cantIntentosLoginFallidos SMALLINT DEFAULT 0,
Usu_Habilitado BIT DEFAULT 1,
PRIMARY KEY (Usu_Id)
);
GO

CREATE TABLE [DESCONOCIDOS4].CHOFER(
Chofer_Id INT NOT NULL IDENTITY(1,1),
Chofer_Per_Id INT REFERENCES [DESCONOCIDOS4].PERSONA NOT NULL,
Chofer_Habilitado BIT DEFAULT 1,
PRIMARY KEY(Chofer_Id)
);
GO
CREATE TABLE [DESCONOCIDOS4].CLIENTE(
Cliente_Id INT NOT NULL IDENTITY(1,1),
Cliente_Per_ID INT REFERENCES [DESCONOCIDOS4].PERSONA NOT NULL, 
Cliente_Habilitado BIT DEFAULT 1,
PRIMARY KEY(Cliente_Id)
);
GO

CREATE TABLE [DESCONOCIDOS4].TURNO(
Turno_Id INT NOT NULL IDENTITY(1,1), 
Turno_Hora_Inicio NUMERIC(18,0),
Turno_Hora_Fin NUMERIC(18,0),
Turno_Descripcion VARCHAR(255),
Turno_Valor_Kilometro NUMERIC(18,2),
Turno_Precio_Base NUMERIC(18,2),
PRIMARY KEY(Turno_Id));
GO

CREATE TABLE [DESCONOCIDOS4].MODELO (
Modelo_Id INT NOT NULL IDENTITY(1,1),
Modelo_Nombre VARCHAR(255),
Modelo_Rodado VARCHAR(10),
PRIMARY KEY(Modelo_id)
);
GO
CREATE TABLE [DESCONOCIDOS4].MARCA (
Marca_Id INT NOT NULL IDENTITY(1,1),
Marca_Nombre  VARCHAR(255),
PRIMARY KEY(Marca_Id)
);
GO
CREATE TABLE [DESCONOCIDOS4].MARCA_MODELO (
TAuto_Marca INT REFERENCES [DESCONOCIDOS4].MARCA NOT NULL,
TAuto_Modelo INT REFERENCES [DESCONOCIDOS4].MODELO NOT NULL,
PRIMARY KEY(TAuto_Marca,TAuto_Modelo)
);
GO
CREATE TABLE [DESCONOCIDOS4].AUTO(
Auto_Patente VARCHAR(10) NOT NULL,
Auto_Detalle VARCHAR(26),
Auto_Marca_Modelo INT,
Auto_Licencia VARCHAR(26),
Auto_Habilitado BIT DEFAULT 1,
PRIMARY KEY(Auto_Patente)
);
GO
CREATE TABLE [DESCONOCIDOS4].UNIDAD_DISPONIBLE(
Uni_Dis_Auto VARCHAR(10) NOT NULL,
Uni_Dis_Chofer INT REFERENCES [DESCONOCIDOS4].CHOFER NOT NULL,
Uni_Dis_Turno INT REFERENCES [DESCONOCIDOS4].TURNO NOT NULL,
PRIMARY KEY(Uni_Dis_Auto,Uni_Dis_Chofer,Uni_Dis_Turno)
);
GO


CREATE TABLE [DESCONOCIDOS4].VIAJE(
Viaje_Nro INT NOT NULL IDENTITY(1,1),
Viaje_Chofer INT REFERENCES [DESCONOCIDOS4].CHOFER NOT NULL,
Viaje_Cliente INT REFERENCES [DESCONOCIDOS4].CLIENTE NOT NULL,
Viaje_Automovil VARCHAR(10) REFERENCES [DESCONOCIDOS4].AUTO NOT NULL,
Viaje_Turno INT REFERENCES [DESCONOCIDOS4].TURNO NOT NULL,
Viaje_Importe NUMERIC(18,2) NOT NULL,
Viaje_Cantidad_Km NUMERIC(18,0),
Viaje_Fecha_Hora_Inicio DATETIME,
Viaje_Fecha_Hora_Fin DATETIME,
PRIMARY KEY (Viaje_Nro)
);
GO
CREATE TABLE [DESCONOCIDOS4].CABECERO_FACTURA(
Cab_Fac_Nro NUMERIC(18,0) NOT NULL,
Cab_Fac_Fecha DATETIME NOT NULL,
Cab_Fac_Cliente INT  NOT NULL,
Cab_Fac_Fecha_Inicio DATETIME,
Cab_Fac_Fecha_Fin DATETIME,
Cab_Fac_Total_Fac NUMERIC(18,2),
PRIMARY KEY (Cab_Fac_Nro,Cab_Fac_Fecha,Cab_Fac_Cliente),
FOREIGN KEY (Cab_Fac_Cliente) REFERENCES [DESCONOCIDOS4].CLIENTE,
);
GO
CREATE TABLE [DESCONOCIDOS4].ITEM_FACTURA(
Item_Fac_Nro_Fac NUMERIC(18,0)  NOT NULL,
Item_Fac_Item INT NOT NULL,
Item_Fac_Id_Viaje int  REFERENCES [DESCONOCIDOS4].VIAJE NOT NULL,
PRIMARY KEY (Item_Fac_Nro_Fac,Item_Fac_Item) 
);
GO
CREATE TABLE [DESCONOCIDOS4].CABECERO_RENDICION(
Cab_Rend_Nro NUMERIC(18,0) NOT NULL IDENTITY(1,1),
Cab_Rend_Turno INT REFERENCES [DESCONOCIDOS4].TURNO NOT NULL,
Cab_Rend_Chofer INT REFERENCES [DESCONOCIDOS4].CHOFER NOT NULL,
Cab_Rend_Fecha DATETIME NOT NULL,
Cab_Rend_Importe NUMERIC(18,2),
PRIMARY KEY (Cab_Rend_Nro,Cab_Rend_Turno,Cab_Rend_Chofer)
);
GO
CREATE TABLE [DESCONOCIDOS4].ITEM_RENDICION(
Item_Rend_NroRend NUMERIC(18,0)  NOT NULL,
Item_Rend_Pos SMALLINT NOT NULL,
Item_Rend_Viaje INT REFERENCES [DESCONOCIDOS4].VIAJE NOT NULL
PRIMARY KEY (Item_Rend_NroRend,Item_Rend_Pos),
);
GO
CREATE TABLE [DESCONOCIDOS4].ROL(
Rol_Id SMALLINT NOT NULL IDENTITY(1,1), 
Rol_Nombre VARCHAR(255) NOT NULL,
Rol_Habilitado BIT DEFAULT 1,
PRIMARY KEY(Rol_Id)
);
GO
CREATE TABLE [DESCONOCIDOS4].USUARIO_ROL(
UsuRol_Usu_Id NVARCHAR(255),
UsuRol_Rol_Id SMALLINT,
);
GO
CREATE TABLE [DESCONOCIDOS4].FUNCIONALIDAD(
Func_Id INT IDENTITY(1,1),
Func_Nombre VARCHAR(255),
Func_Descripcion VARCHAR(255),
PRIMARY KEY(Func_Id)
);
GO
CREATE TABLE [DESCONOCIDOS4].FUNCIONALIDADXROL(
FuncRol_Rol_Id SMALLINT REFERENCES  [DESCONOCIDOS4].ROL(Rol_Id) NOT NULL,
FunRol_Func_Id INT REFERENCES [DESCONOCIDOS4].FUNCIONALIDAD(Func_Id )
NOT NULL,
);
GO

-- // PROGRAMACION DE LA MIGRACION
IF OBJECT_ID (N'[DESCONOCIDOS4].PRC_MIGRA_PERSONA_CLIENTE', N'P') IS NOT NULL
		DROP PROCEDURE  [DESCONOCIDOS4].PRC_MIGRA_PERSONA_CLIENTE;
GO
-- Se puebla la tabla PERSONA Y CLIENTE
CREATE PROCEDURE [DESCONOCIDOS4].PRC_MIGRA_PERSONA_CLIENTE
AS
BEGIN

INSERT INTO [DESCONOCIDOS4].PERSONA (Persona_Dni,Persona_Nombre
      ,Persona_Apellido,Persona_Direccion
      ,Persona_Piso,Persona_Dartamento,Persona_Localidad
      ,Persona_Telefono,Persona_Mail
      ,Persona_Fecha_Nac) 
	  SELECT DISTINCT Cliente_Dni,Cliente_Nombre,Cliente_Apellido,Cliente_Direccion,'0','-','-',Cliente_Telefono,Cliente_Mail,Cliente_Fecha_Nac FROM gd_esquema.Maestra
	  INSERT INTO  [DESCONOCIDOS4].CLIENTE (Cliente_Per_ID)
	  SELECT Persona_Id FROM [DESCONOCIDOS4].PERSONA
END
GO

IF OBJECT_ID (N'[DESCONOCIDOS4].PRC_MIGRA_PERSONA_CHOFER', N'P') IS NOT NULL
		DROP PROCEDURE  [DESCONOCIDOS4].PRC_MIGRA_PERSONA_CHOFER;
GO
-- Se puebla la tabla PERSONA Y CHOFER
CREATE PROCEDURE [DESCONOCIDOS4].PRC_MIGRA_PERSONA_CHOFER
AS
BEGIN

INSERT INTO [DESCONOCIDOS4].PERSONA (Persona_Dni,Persona_Nombre
      ,Persona_Apellido,Persona_Direccion
      ,Persona_Piso,Persona_Dartamento,Persona_Localidad
      ,Persona_Telefono,Persona_Mail
      ,Persona_Fecha_Nac) 
	  SELECT DISTINCT Chofer_Dni,Chofer_Nombre,Chofer_Apellido,Chofer_Direccion,'0','-','-',Chofer_Telefono,Chofer_Mail,Chofer_Fecha_Nac FROM gd_esquema.Maestra
	  INSERT INTO  [DESCONOCIDOS4].CHOFER (Chofer_Per_Id)
	  SELECT Persona_Id FROM [DESCONOCIDOS4].PERSONA WHERE PERSONA.Persona_Id NOT IN (SELECT Cliente_Per_ID FROM [DESCONOCIDOS4].CLIENTE )
END
GO
--USUARIO AUTOMATICO AL INSERTAR UN REGISTRO EN PERSONA
IF OBJECT_ID (N'[DESCONOCIDOS4].TR_USUARIO_AUTOMATICO', N'TR') IS NOT NULL
		DROP TRIGGER  [DESCONOCIDOS4].TR_USUARIO_AUTOMATICO;
GO

CREATE TRIGGER  [DESCONOCIDOS4].TR_USUARIO_AUTOMATICO ON [DESCONOCIDOS4].PERSONA
FOR INSERT
AS
BEGIN	
	INSERT INTO [DESCONOCIDOS4].USUARIO (Usu_Per_Id,Usu_Nombre_Usuario,Usu_Password)
	SELECT I.Persona_Id,
	CONCAT(LEFT(I.Persona_Apellido,4),LEFT(I.Persona_Nombre,3)),
	HashBytes('SHA2_256',convert(varchar(255), 'Inicio2017')) 
	FROM INSERTED I 

END
GO
IF OBJECT_ID (N'[DESCONOCIDOS4].PRC_MIGRA_TURNO', N'P') IS NOT NULL
		DROP PROCEDURE  [DESCONOCIDOS4].PRC_MIGRA_TURNO;
GO
-- Se puebla la tabla TURNO
CREATE PROCEDURE [DESCONOCIDOS4].PRC_MIGRA_TURNO
AS
BEGIN
	  INSERT INTO [DESCONOCIDOS4].TURNO (Turno_Hora_Inicio,Turno_Hora_Fin,Turno_Descripcion,Turno_Valor_Kilometro,Turno_Precio_Base) 
	  SELECT DISTINCT Turno_Hora_Inicio,Turno_Hora_Fin,Turno_Descripcion,Turno_Valor_Kilometro,Turno_Precio_Base FROM gd_esquema.Maestra
END
GO

IF OBJECT_ID (N'[DESCONOCIDOS4].PRC_MIGRA_CAB_FACTURA', N'P') IS NOT NULL
		DROP PROCEDURE  [DESCONOCIDOS4].PRC_MIGRA_CAB_FACTURA;
GO
-- Se puebla la tabla CABECERO_FACTURA
CREATE PROCEDURE [DESCONOCIDOS4].PRC_MIGRA_CAB_FACTURA
AS
BEGIN
	  INSERT INTO [DESCONOCIDOS4].CABECERO_FACTURA (Cab_Fac_Nro,Cab_Fac_Fecha,Cab_Fac_Cliente,Cab_Fac_Fecha_Inicio,Cab_Fac_Fecha_Fin) 
	  SELECT 
		  DISTINCT
		  Factura_Nro,
		  Factura_Fecha,
		  (SELECT Cliente_Id FROM [DESCONOCIDOS4].CLIENTE LEFT JOIN  [DESCONOCIDOS4].PERSONA ON  Cliente_Per_ID=Persona_Id WHERE Persona_Dni= M.Cliente_Dni),
		  Factura_Fecha_Inicio,
		  Factura_Fecha_Fin 
	  FROM gd_esquema.Maestra M WHERE Factura_Nro>0
END
GO
IF OBJECT_ID (N'[DESCONOCIDOS4].PRC_MIGRA_VIAJE', N'P') IS NOT NULL
		DROP PROCEDURE  [DESCONOCIDOS4].PRC_MIGRA_VIAJE;
GO
-- Se puebla la tabla VIAJE
CREATE PROCEDURE [DESCONOCIDOS4].[PRC_MIGRA_VIAJE]
AS
BEGIN
	  INSERT INTO [DESCONOCIDOS4].VIAJE (Viaje_Chofer,Viaje_Cliente,Viaje_Automovil,Viaje_Turno,Viaje_Precio_Base,Viaje_Valor_km,Viaje_Importe,Viaje_Cantidad_Km,Viaje_Fecha_Hora_Inicio,Viaje_Fecha_Hora_Fin) 
	  SELECT 
		  DISTINCT
		  (SELECT Chofer_Id FROM CHOFER LEFT JOIN PERSONA ON Persona_Id=Chofer_Per_Id WHERE M.Chofer_Dni=Persona_Dni),	
		  (SELECT Cliente_Id FROM CLIENTE LEFT JOIN PERSONA ON Persona_Id=Cliente_Per_ID WHERE M.Cliente_Dni=Persona_Dni),	  
		  Auto_Patente,
		  (SELECT Turno_Id FROM TURNO T 
			WHERE CONCAT(M.Turno_Descripcion,M.Turno_Hora_Inicio,M.Turno_Hora_Fin,M.Turno_Precio_Base,M.Turno_Valor_Kilometro)=CONCAT(T.Turno_Descripcion,T.Turno_Hora_Inicio,T.Turno_Hora_Fin,T.Turno_Precio_Base,T.Turno_Valor_Kilometro) ),
		  M.Turno_Precio_Base,
		  M.Turno_Valor_Kilometro,
		  M.Turno_Precio_Base+(M.Turno_Valor_Kilometro*M.Viaje_Cant_Kilometros),
		  M.Viaje_Cant_Kilometros,
		  M.Viaje_Fecha,
		  DATEADD(SECOND,1,M.Viaje_Fecha)
	  FROM gd_esquema.Maestra M WHERE M.Factura_Nro>0 ORDER BY M.Viaje_Fecha ASC
END
GO
IF OBJECT_ID (N'[DESCONOCIDOS4].PRC_MIGRA_ITEM_FACTURA', N'P') IS NOT NULL
		DROP PROCEDURE  [DESCONOCIDOS4].PRC_MIGRA_ITEM_FACTURA;
GO
-- Se puebla la tabla PRC_MIGRA_ITEM_FACTURA
CREATE PROCEDURE [DESCONOCIDOS4].PRC_MIGRA_ITEM_FACTURA
AS
BEGIN
	  INSERT INTO [DESCONOCIDOS4].ITEM_FACTURA(Item_Fac_Nro_Fac,Item_Fac_Item,Item_Fac_Id_Viaje) 
	  SELECT 
	  DISTINCT 
		  M2.Factura_Nro, 
		  ROW_NUMBER() OVER (PARTITION BY M2.Factura_Nro ORDER BY M2.Factura_Nro),
		  Viaje_Nro 
	  FROM [DESCONOCIDOS4].VIAJE LEFT JOIN  [DESCONOCIDOS4].CLIENTE ON Viaje_Cliente= Cliente_Id LEFT JOIN [DESCONOCIDOS4].CHOFER ON Viaje_Chofer=Chofer_Id
	LEFT JOIN  [DESCONOCIDOS4].PERSONA P1 ON P1.Persona_Id= Cliente_Per_ID LEFT JOIN [DESCONOCIDOS4].PERSONA P2 ON P2.Persona_Id= Chofer_Per_Id
	LEFT JOIN gd_esquema.Maestra M2
	 ON  CONCAT(M2.Viaje_Fecha,M2.Viaje_Cant_Kilometros,M2.Cliente_Dni,M2.Chofer_Dni,M2.Auto_Patente)=CONCAT(Viaje_Fecha_Hora_Inicio,Viaje_Cantidad_Km,P1.Persona_Dni,P2.Persona_Dni,Viaje_Automovil) 
	 WHERE M2.Factura_Nro>0 AND M2.Rendicion_Nro IS NULL  GROUP BY M2.Factura_Nro,Viaje_Nro ORDER BY M2.Factura_Nro,Viaje_Nro
END
GO

