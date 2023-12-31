USE [taller]
GO
/****** Object:  Table [dbo].[vehiculo]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vehiculo](
	[ID_VEHICULO] [int] IDENTITY(1,1) NOT NULL,
	[VEHICULO_MARCA] [varchar](150) NOT NULL,
	[VEHICULO_MODELO] [varchar](150) NOT NULL,
	[VEHICULO_PATENTE] [varchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_VEHICULO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[automovil]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[automovil](
	[ID_AUTO] [int] IDENTITY(1,1) NOT NULL,
	[RELA_VEHICULO] [int] NOT NULL,
	[AUTO_TIPO] [int] NOT NULL,
	[AUTO_CANPUERTAS] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_AUTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SoloAutos]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[SoloAutos] as
select v.VEHICULO_MARCA Marca
, v.VEHICULO_MODELO Modelo
, v.VEHICULO_PATENTE Patente
,CASE a.AUTO_TIPO
        WHEN 0 THEN 'compacto'
        WHEN 1 THEN 'sedan'
        WHEN 2 THEN 'monovolumen'
        WHEN 3 THEN 'utilitario'
        WHEN 4 THEN 'lujo'
        ELSE 'Desconocido'
  END AS Automovil_Tipo
, a.AUTO_CANPUERTAS Nro_de_Puertas
from vehiculo v join automovil a on v.ID_VEHICULO = a.RELA_VEHICULO ;
GO
/****** Object:  Table [dbo].[moto]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[moto](
	[ID_MOTO] [int] IDENTITY(1,1) NOT NULL,
	[RELA_VEHICULO] [int] NOT NULL,
	[MOTO_CILINDRADA] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_MOTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SoloMotos]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SoloMotos]
AS
SELECT        v.VEHICULO_MARCA AS Marca, v.VEHICULO_MODELO AS Modelo, v.VEHICULO_PATENTE AS Patente, m.MOTO_CILINDRADA AS Cilindrada
FROM            dbo.vehiculo AS v INNER JOIN
                         dbo.moto AS m ON v.ID_VEHICULO = m.RELA_VEHICULO
GO
/****** Object:  View [dbo].[VehiAutoMoto]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VehiAutoMoto]
AS
SELECT        v.ID_VEHICULO AS Nro_Vehiculo, v.VEHICULO_MARCA, v.VEHICULO_MODELO, v.VEHICULO_PATENTE, a.ID_AUTO AS Nro_Automovil, 
                         CASE a.AUTO_TIPO WHEN 0 THEN 'compacto' WHEN 1 THEN 'sedan' WHEN 2 THEN 'monovolumen' WHEN 3 THEN 'utilitario' WHEN 4 THEN 'lujo' ELSE ' ' END AS Automovil_Tipo, 
                         a.AUTO_CANPUERTAS AS Automovil_Puertas, m.ID_MOTO AS Nro_Moto, m.MOTO_CILINDRADA
FROM            dbo.vehiculo AS v LEFT OUTER JOIN
                         dbo.automovil AS a ON v.ID_VEHICULO = a.RELA_VEHICULO LEFT OUTER JOIN
                         dbo.moto AS m ON v.ID_VEHICULO = m.RELA_VEHICULO
GO
/****** Object:  Table [dbo].[arreglo]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[arreglo](
	[ID_ARREGLO] [int] IDENTITY(1,1) NOT NULL,
	[RELA_DESPERFECTO] [int] NOT NULL,
	[RELA_REPUESTO] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_ARREGLO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[desperfecto]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[desperfecto](
	[ID_DESPERFECTO] [int] IDENTITY(1,1) NOT NULL,
	[RELA_PRESUPUESTO] [int] NOT NULL,
	[DESPERFECTO_DESCRI] [varchar](150) NOT NULL,
	[DESPERFECTO_MANODEOBRA] [decimal](10, 2) NOT NULL,
	[DESPERFECTO_TIEMPO] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_DESPERFECTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[presupuesto]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[presupuesto](
	[ID_PRESUPUESTO] [int] IDENTITY(1,1) NOT NULL,
	[RELA_CLIENTES] [int] NOT NULL,
	[RELA_VEHICULOS] [int] NOT NULL,
	[PRESUPUESTO_TOTAL] [int] NOT NULL,
	[PRESUPUESTO_FECHA] [date] NOT NULL,
	[PRESUPUESTO_NRO] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_PRESUPUESTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[repuestos]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[repuestos](
	[ID_REPUESTO] [int] IDENTITY(1,1) NOT NULL,
	[REPUESTO_NOMBRE] [varchar](150) NOT NULL,
	[repuesto_precio] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_REPUESTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_repuesto_nombre] UNIQUE NONCLUSTERED 
(
	[REPUESTO_NOMBRE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Taller_full]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create view [dbo].[Taller_full] as 
--Repuesto más utilizado por Marca/Modelo en las reparaciones realizadas (Mostrar Descripción del Repuesto y cantidad de veces usado)
select *
from vehiculo a 
join presupuesto b on a.ID_VEHICULO = b.RELA_VEHICULOS
join desperfecto c on c.RELA_PRESUPUESTO = b.ID_PRESUPUESTO
join arreglo d on d.RELA_DESPERFECTO = c.ID_DESPERFECTO
join repuestos e on e.ID_REPUESTO = d.RELA_REPUESTO;
GO
/****** Object:  View [dbo].[Repuesto_Mas_Usado]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[Repuesto_Mas_Usado] as
SELECT  --vinculo arreglos con repuestos
        d.RELA_PRESUPUESTO,
        ar.RELA_REPUESTO as RELA_REPUESTO,
        r.REPUESTO_NOMBRE as Repuesto,
        COUNT(*) AS CantidadUsos
    FROM desperfecto d
    INNER JOIN arreglo ar ON d.ID_DESPERFECTO = ar.RELA_DESPERFECTO
    INNER JOIN repuestos r ON ar.RELA_REPUESTO = r.ID_REPUESTO
    GROUP BY d.RELA_PRESUPUESTO, ar.RELA_REPUESTO, r.REPUESTO_NOMBRE ; 
GO
/****** Object:  View [dbo].[RepMasUsado_xMarcaModelo]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[RepMasUsado_xMarcaModelo] as
select 
a.VEHICULO_MARCA
, a.VEHICULO_MODELO
, b.REPUESTO
, max(b.CANTIDADUSOS) as MasVecesUsado
from
Taller_full a, Repuesto_Mas_Usado b
where a.ID_PRESUPUESTO = b.RELA_PRESUPUESTO
group by a.VEHICULO_MARCA, a.VEHICULO_MODELO, b.REPUESTO;
GO
/****** Object:  View [dbo].[PromTotal_xMarcaModelo]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[PromTotal_xMarcaModelo] as 
SELECT v.VEHICULO_MARCA
, v.VEHICULO_MODELO
, AVG(v.PRESUPUESTO_TOTAL) PromTotal_Agrup
 FROM Taller_full v
 group by v.VEHICULO_MARCA, v.VEHICULO_MODELO;
GO
/****** Object:  View [dbo].[Totales_xAuto_Moto]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Totales_xAuto_Moto]
AS
SELECT        'Auto' AS Vehiculo, sum(t .presupuesto_total) AS total
FROM            Taller_full t INNER JOIN
                         automovil a ON t .ID_VEHICULO = a.RELA_VEHICULO
UNION ALL
SELECT        'Moto' AS Vehiculo, sum(y.PRESUPUESTO_TOTAL)
FROM            Taller_full y INNER JOIN
                         moto b ON y.ID_VEHICULO = b.RELA_VEHICULO;
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[ID_CLIENTES] [int] IDENTITY(1,1) NOT NULL,
	[CLIENTE_NOMBRE] [varchar](150) NOT NULL,
	[CLIENTE_APELLIDO] [varchar](150) NOT NULL,
	[CLIENTE_EMAIL] [varchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_CLIENTES] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[arreglo]  WITH CHECK ADD FOREIGN KEY([RELA_DESPERFECTO])
REFERENCES [dbo].[desperfecto] ([ID_DESPERFECTO])
GO
ALTER TABLE [dbo].[arreglo]  WITH CHECK ADD FOREIGN KEY([RELA_REPUESTO])
REFERENCES [dbo].[repuestos] ([ID_REPUESTO])
GO
ALTER TABLE [dbo].[automovil]  WITH CHECK ADD FOREIGN KEY([RELA_VEHICULO])
REFERENCES [dbo].[vehiculo] ([ID_VEHICULO])
GO
ALTER TABLE [dbo].[desperfecto]  WITH CHECK ADD FOREIGN KEY([RELA_PRESUPUESTO])
REFERENCES [dbo].[presupuesto] ([ID_PRESUPUESTO])
GO
ALTER TABLE [dbo].[moto]  WITH CHECK ADD FOREIGN KEY([RELA_VEHICULO])
REFERENCES [dbo].[vehiculo] ([ID_VEHICULO])
GO
ALTER TABLE [dbo].[presupuesto]  WITH CHECK ADD FOREIGN KEY([RELA_CLIENTES])
REFERENCES [dbo].[clientes] ([ID_CLIENTES])
GO
ALTER TABLE [dbo].[presupuesto]  WITH CHECK ADD FOREIGN KEY([RELA_VEHICULOS])
REFERENCES [dbo].[vehiculo] ([ID_VEHICULO])
GO
/****** Object:  StoredProcedure [dbo].[MassiveCharge]    Script Date: 30/10/2023 15:42:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[MassiveCharge] AS
BEGIN

/*+ Creación de la tabla Temporal que contendrá los Repuestos con sus precios*/

    CREATE TABLE #TMP_RESPUESTO (Nombre VARCHAR(100),
                                 Precio DECIMAL(18,6))

/*+ Se generan los registros en la tabla temporal que posteriormente se evaluarán para ver si procede su carga en la tabla definitiva de Repuestos*/

    BEGIN /*+ BEGIN INSERT EN LA TEMPORAL DE RESPUESTOS*/
        INSERT INTO #TMP_RESPUESTO VALUES ('B356963821', 17.61)
        INSERT INTO #TMP_RESPUESTO VALUES ('B881468337', 40.88)
        INSERT INTO #TMP_RESPUESTO VALUES ('B867719836', 87.76)
        INSERT INTO #TMP_RESPUESTO VALUES ('B397571688', 13.97)
        INSERT INTO #TMP_RESPUESTO VALUES ('B852883143', 47.97)
        INSERT INTO #TMP_RESPUESTO VALUES ('B461882670', 22.68)
        INSERT INTO #TMP_RESPUESTO VALUES ('B333520964', 82.28)
        INSERT INTO #TMP_RESPUESTO VALUES ('B388445039', 50.71)
        INSERT INTO #TMP_RESPUESTO VALUES ('B648201513', 21.83)
        INSERT INTO #TMP_RESPUESTO VALUES ('B436759416', 35.39)
        INSERT INTO #TMP_RESPUESTO VALUES ('B317533243', 22.84)
        INSERT INTO #TMP_RESPUESTO VALUES ('B666592414', 58.67)
        INSERT INTO #TMP_RESPUESTO VALUES ('B443568817', 53.83)
        INSERT INTO #TMP_RESPUESTO VALUES ('B316416378', 17.74)
        INSERT INTO #TMP_RESPUESTO VALUES ('B252543362', 16.98)
        INSERT INTO #TMP_RESPUESTO VALUES ('B453148609', 14.23)
        INSERT INTO #TMP_RESPUESTO VALUES ('B254958806', 41.19)
        INSERT INTO #TMP_RESPUESTO VALUES ('B356963821', 62.58)
        INSERT INTO #TMP_RESPUESTO VALUES ('B846487171', 92.91)
        INSERT INTO #TMP_RESPUESTO VALUES ('B397571688', 1.04)
        INSERT INTO #TMP_RESPUESTO VALUES ('B535169105', 90.14)
        INSERT INTO #TMP_RESPUESTO VALUES ('B628263302', 78.64)
        INSERT INTO #TMP_RESPUESTO VALUES ('B608816685', 93.73)
        INSERT INTO #TMP_RESPUESTO VALUES ('B660755442', 43.62)
        INSERT INTO #TMP_RESPUESTO VALUES ('B659053715', 90.59)
        INSERT INTO #TMP_RESPUESTO VALUES ('B556344166', 71.62)
        INSERT INTO #TMP_RESPUESTO VALUES ('B216140665', 93.15)
        INSERT INTO #TMP_RESPUESTO VALUES ('B843858581', 66.52)
        INSERT INTO #TMP_RESPUESTO VALUES ('B790077756', 8.91)
        INSERT INTO #TMP_RESPUESTO VALUES ('B916071768', 85.46)
        INSERT INTO #TMP_RESPUESTO VALUES ('B317533243', 7.97)
        INSERT INTO #TMP_RESPUESTO VALUES ('B343454513', 22.91)
        INSERT INTO #TMP_RESPUESTO VALUES ('B986574036', 65.10)
        INSERT INTO #TMP_RESPUESTO VALUES ('B662139869', 3.50)
        INSERT INTO #TMP_RESPUESTO VALUES ('B618792223', 6.87)
        INSERT INTO #TMP_RESPUESTO VALUES ('B578485476', 49.70)
        INSERT INTO #TMP_RESPUESTO VALUES ('B132813434', 32.58)
        INSERT INTO #TMP_RESPUESTO VALUES ('B776163235', 73.64)
        INSERT INTO #TMP_RESPUESTO VALUES ('B215908676', 92.83)
        INSERT INTO #TMP_RESPUESTO VALUES ('B871139440', 31.83)
        INSERT INTO #TMP_RESPUESTO VALUES ('B564893705', 18.91)
        INSERT INTO #TMP_RESPUESTO VALUES ('B634131771', 70.35)
        INSERT INTO #TMP_RESPUESTO VALUES ('B321187273', 91.96)
        INSERT INTO #TMP_RESPUESTO VALUES ('B444737823', 78.73)
        INSERT INTO #TMP_RESPUESTO VALUES ('B413525993', 9.93)
        INSERT INTO #TMP_RESPUESTO VALUES ('B229547877', 97.08)
        INSERT INTO #TMP_RESPUESTO VALUES ('B545788950', 11.84)
        INSERT INTO #TMP_RESPUESTO VALUES ('B658514562', 8.84)
        INSERT INTO #TMP_RESPUESTO VALUES ('B736313138', 78.47)
        INSERT INTO #TMP_RESPUESTO VALUES ('B840888802', 93.85)
        INSERT INTO #TMP_RESPUESTO VALUES ('B883572821', 21.57)
        INSERT INTO #TMP_RESPUESTO VALUES ('B493478663', 76.98)
        INSERT INTO #TMP_RESPUESTO VALUES ('B718838840', 7.41)
        INSERT INTO #TMP_RESPUESTO VALUES ('B183671709', 45.53)
        INSERT INTO #TMP_RESPUESTO VALUES ('B908384721', 14.73)
        INSERT INTO #TMP_RESPUESTO VALUES ('B566417680', 44.04)
        INSERT INTO #TMP_RESPUESTO VALUES ('B633833113', 33.28)
        INSERT INTO #TMP_RESPUESTO VALUES ('B829258206', 41.74)
        INSERT INTO #TMP_RESPUESTO VALUES ('B350041352', 85.13)
        INSERT INTO #TMP_RESPUESTO VALUES ('B548168477', 7.44)
        INSERT INTO #TMP_RESPUESTO VALUES ('B765657146', 89.79)
        INSERT INTO #TMP_RESPUESTO VALUES ('B830231322', 81.42)
        INSERT INTO #TMP_RESPUESTO VALUES ('B816385774', 9.30)
        INSERT INTO #TMP_RESPUESTO VALUES ('B857448796', 77.36)
        INSERT INTO #TMP_RESPUESTO VALUES ('B302875266', 54.89)
        INSERT INTO #TMP_RESPUESTO VALUES ('B790507487', 50.41)
        INSERT INTO #TMP_RESPUESTO VALUES ('B723629401', 65.36)
        INSERT INTO #TMP_RESPUESTO VALUES ('B595728629', 19.94)
        INSERT INTO #TMP_RESPUESTO VALUES ('B472436824', 65.69)
        INSERT INTO #TMP_RESPUESTO VALUES ('B235859870', 66.44)
        INSERT INTO #TMP_RESPUESTO VALUES ('B874178252', 42.38)
        INSERT INTO #TMP_RESPUESTO VALUES ('B777713850', 40.26)
        INSERT INTO #TMP_RESPUESTO VALUES ('B550221285', 8.72)
        INSERT INTO #TMP_RESPUESTO VALUES ('B816043247', 73.97)
        INSERT INTO #TMP_RESPUESTO VALUES ('B607313788', 15.95)
        INSERT INTO #TMP_RESPUESTO VALUES ('B396482694', 45.17)
        INSERT INTO #TMP_RESPUESTO VALUES ('B504021331', 24.52)
        INSERT INTO #TMP_RESPUESTO VALUES ('B651475349', 86.77)
        INSERT INTO #TMP_RESPUESTO VALUES ('B470409863', 11.81)
        INSERT INTO #TMP_RESPUESTO VALUES ('B264135435', 62.58)
        INSERT INTO #TMP_RESPUESTO VALUES ('B755636151', 33.88)
        INSERT INTO #TMP_RESPUESTO VALUES ('B382183955', 0.92)
        INSERT INTO #TMP_RESPUESTO VALUES ('B667316286', 0.29)
        INSERT INTO #TMP_RESPUESTO VALUES ('B783117048', 41.57)
        INSERT INTO #TMP_RESPUESTO VALUES ('B812952354', 86.25)
        INSERT INTO #TMP_RESPUESTO VALUES ('B621838237', 80.54)
        INSERT INTO #TMP_RESPUESTO VALUES ('B665465223', 53.69)
        INSERT INTO #TMP_RESPUESTO VALUES ('B881682635', 64.78)
        INSERT INTO #TMP_RESPUESTO VALUES ('B646289861', 72.01)
        INSERT INTO #TMP_RESPUESTO VALUES ('B852115667', 48.73)
        INSERT INTO #TMP_RESPUESTO VALUES ('B144635415', 34.23)
        INSERT INTO #TMP_RESPUESTO VALUES ('B874863828', 24.70)
        INSERT INTO #TMP_RESPUESTO VALUES ('B333841476', 41.57)
        INSERT INTO #TMP_RESPUESTO VALUES ('B587386017', 45.27)
        INSERT INTO #TMP_RESPUESTO VALUES ('B874270576', 42.38)
        INSERT INTO #TMP_RESPUESTO VALUES ('B300733136', 25.55)
        INSERT INTO #TMP_RESPUESTO VALUES ('B611446656', 60.12)
        INSERT INTO #TMP_RESPUESTO VALUES ('B801300387', 61.04)
        INSERT INTO #TMP_RESPUESTO VALUES ('B845153562', 60.09)
        INSERT INTO #TMP_RESPUESTO VALUES ('B943846621', 37.05)
    END /*+ END INSERT EN LA TEMPORAL DE RESPUESTOS*/
	


	
BEGIN
	DECLARE @TOTAL INT;
	DECLARE @EXCLUIDOS VARCHAR(2000);
	DECLARE @COMPLETO INT;
	DECLARE @ACTUALIZADO INT;
	DECLARE @NO_INCLUIDO INT;
	DEClARE @AGREGADO VARCHAR(50);
	-- asigno
	SET @AGREGADO  = '';
	SET @EXCLUIDOS = 'nombre --> precio' + char(13) + char(10) ;
	SET @COMPLETO = 0;
	SET @NO_INCLUIDO = 0;
	SET @ACTUALIZADO = 0;
	
	DECLARE E CURSOR FOR
	SELECT NOMBRE, PRECIO
	FROM #TMP_RESPUESTO 
	ORDER BY NOMBRE ASC, PRECIO ASC;

	OPEN E;

	DECLARE @nomb varchar(50);
	DECLARE @prec decimal(10,2);
	--comienza la magia
	FETCH NEXT FROM E INTO @nomb, @prec;
WHILE @@FETCH_STATUS = 0
BEGIN

    IF @prec < 20 
		BEGIN
		--controlo que no se duplique 
		SELECT @AGREGADO = a.REPUESTO_NOMBRE FROM repuestos a WHERE a.REPUESTO_NOMBRE = @nomb;
		IF @nomb = @AGREGADO 
			BEGIN
				UPDATE repuestos SET REPUESTO_PRECIO = REPUESTO_PRECIO + @prec WHERE REPUESTO_NOMBRE = @nomb ;
				SET @ACTUALIZADO = @ACTUALIZADO +1 ;
			END;
		ELSE
			BEGIN
				INSERT INTO REPUESTOS (REPUESTO_NOMBRE , REPUESTO_PRECIO) 
				values(@nomb, @prec);
				SET @COMPLETO = @COMPLETO +1 ;
			END;
		End;		
	ELSE		-- son = o > que 20
		BEGIN		--me fijo si ya esta el valor
			SELECT @AGREGADO = REPUESTO_NOMBRE FROM REPUESTOS WHERE REPUESTO_NOMBRE = @nomb;
		IF @nomb = @AGREGADO 
			BEGIN
				UPDATE REPUESTOS SET REPUESTO_PRECIO = REPUESTO_PRECIO + @prec WHERE REPUESTO_NOMBRE = @nomb ;
				SET @ACTUALIZADO = @ACTUALIZADO +1 ;
			END;
		ELSE	-- es mayor o igual a 20 y no está agregado
			BEGIN
				set @EXCLUIDOS = @EXCLUIDOS + @nomb + ' -> ' +CAST(@prec as nvarchar(20)) + char(13) + char(10) ;
				SET @NO_INCLUIDO = @NO_INCLUIDO + 1;
			END;
		END;
	FETCH NEXT FROM E INTO @nomb, @prec;
END;
	--libero cursor
	CLOSE E;
	DEALLOCATE E; 
	
	SELECT @TOTAL = COUNT(*) FROM #TMP_RESPUESTO;
	
	-- MUESTRO RESULTADOS
	PRINT 'DEL TOTAL DE (' + cast(@TOTAL as nvarchar) + ') DE REGISTROS DE LA TABLA TEMPORAL:';
	print 'Se han agregado ' + cast(@COMPLETO as nvarchar) + ' y se actualizaron ' + cast(@ACTUALIZADO as nvarchar) +' exitosamente ';
	print 'se detallan los (' + cast(@NO_INCLUIDO as nvarchar(10)) +') casos excluidos: '  +  @EXCLUIDOS ;
END;

END;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 - Compacto / 1 - Sedán / 2 - Monovolumen / 3 - Utilitario / 4 - Lujo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'automovil', @level2type=N'COLUMN',@level2name=N'AUTO_TIPO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "v"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SoloMotos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SoloMotos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Totales_xAuto_Moto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Totales_xAuto_Moto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "v"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 383
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VehiAutoMoto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VehiAutoMoto'
GO
