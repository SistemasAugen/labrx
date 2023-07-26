USE [LocalLab2000]
GO

/****** Object:  View [dbo].[VwRecetasTRECEUX]    Script Date: 12/04/2012 16:48:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*    
Actualización 12/04/2012 PEDRO FARIAS L.

Ultima Actualizacion por RL para Laboratorios Ver 3.2.4 (Ene 26' 2007)
WHERE     (dbo.orderhed.company = 'TRECEUX') AND (DATEPART(dayofyear, dbo.orderhed.orderdate) = DATEPART(dayofyear, GETDATE())) AND (DATEPART(year, 
                      dbo.orderhed.orderdate) = DATEPART(year, GETDATE()))
 ALTER  VIEW dbo.VwRecetasTRECEUX AS SELECT dbo.orderhed.ordernum, dbo.VwPartTRECEUX.Clase, dbo.VwPartTRECEUX.Material, dbo.VwPartTRECEUX.Diseño, dbo.VwPartTRECEUX.Lado, dbo.orderhed.shortchar01 AS OrdenLab, CAST(dbo.orderhed.number01 AS decimal) AS RefDigital, CAST(ISNULL(dbo.orderhed.number20, 0) AS decimal) AS Armazon, CAST(dbo.orderhed.number19 AS decimal) AS TipoArmazon, dbo.orderhed.checkbox01 AS Biselado, ISNULL(dbo.orderhed.shortchar06, 0) AS Antireflejante, ISNULL(dbo.orderhed.shortchar09, 0) AS Tinte, dbo.orderhed.checkbox02 AS Gradiente, ISNULL(dbo.orderhed.shortchar10, 0) AS Tono, CAST(dbo.orderhed.number02 AS decimal(8, 2)) AS EsferaR, CAST(dbo.orderhed.number06 AS decimal(8, 2)) AS EsferaL, CAST(dbo.orderhed.number03 AS decimal(8, 2)) AS CilindroR, CAST(dbo.orderhed.number07 AS decimal(8, 2)) AS CilindroL, CAST(dbo.orderhed.number04 AS decimal) AS EjeR, CAST(dbo.orderhed.number08 AS decimal) AS EjeL, CAST(dbo.orderhed.number05 AS decimal(8, 2)) AS AdicionR, CAST(dbo.orderhed.number09 AS decimal(8, 2)) AS AdicionL, CAST(dbo.orderhed.number11 AS decimal) AS AlturaR, CAST(dbo.orderhed.number13 AS decimal) AS AlturaL, CAST(dbo.orderhed.number12 AS decimal) AS MonoR, CAST(dbo.orderhed.number14 AS decimal) AS MonoL, CAST(dbo.orderhed.number16 AS decimal) AS DIPle, CAST(dbo.orderhed.number15 AS DECIMAL) AS DIPce, CAST(dbo.orderhed.number17 AS DECIMAL) AS Altura, ISNULL(dbo.orderhed.shortchar02, 0) AS A, ISNULL(dbo.orderhed.shortchar03, 0) AS B, ISNULL(dbo.orderhed.shortchar04, 0) AS ED, ISNULL(dbo.orderhed.shortchar05, 0) AS Puente, ISNULL(dbo.orderhed.shortchar07, 0) AS MaterialID, ISNULL(dbo.orderhed.shortchar08, 0) AS DiseñoID, ISNULL(CAST(dbo.orderhed.number18 AS decimal), 0) AS ArmazonID, dbo.orderhed.checkbox03 AS Status, CAST(dbo.orderhed.number10 AS decimal) AS Foco, dbo.orderhed.orderdate, dbo.orderdtl.checkbox01 AS RxLado, dbo.VwPartTRECEUX.partnum, dbo.VwPartTRECEUX.partdescription, dbo.orderdtl.checkbox02 AS RxClase, dbo.customer.custnum, dbo.orderhed.checkbox04, dbo.orderhed.checkbox05, dbo.orderhed.userchar1 as PrismaR, dbo.orderhed.userchar2 as EjePrismaR, dbo.orderhed.userchar3 as PrismaL, dbo.orderhed.userchar4 as EjePrismaL FROM dbo.customer INNER JOIN dbo.orderhed ON dbo.customer.custnum = dbo.orderhed.custnum AND dbo.customer.company = dbo.orderhed.company LEFT OUTER JOIN dbo.VwPartTRECEUX INNER JOIN dbo.orderdtl ON dbo.VwPartTRECEUX.company = dbo.orderdtl.company AND dbo.VwPartTRECEUX.partnum = dbo.orderdtl.partnum ON dbo.orderhed.ordernum = dbo.orderdtl.ordernum AND dbo.orderhed.company = dbo.orderdtl.company WHERE (dbo.orderhed.company = 'TRECEUX') AND (DATEPART(dayofyear, dbo.orderhed.orderdate) = DATEPART(dayofyear, GETDATE())) AND (DATEPART(year, dbo.orderhed.orderdate) = DATEPART(year, GETDATE())) 

WHERE     (dbo.orderhed.company = 'TRECEUX') AND (dbo.orderhed.sincronizada = 0)
*/
ALTER VIEW [dbo].[VwRecetasTRECEUX]
AS
SELECT     dbo.orderhed.ordernum, dbo.VwPartTRECEUX.Clase, dbo.VwPartTRECEUX.Material, dbo.VwPartTRECEUX.Diseño, dbo.VwPartTRECEUX.Lado, 
                      dbo.orderhed.shortchar01 AS OrdenLab, CAST(dbo.orderhed.number01 AS decimal) AS RefDigital, CAST(ISNULL(dbo.orderhed.number20, 0) AS decimal) AS Armazon, 
                      CAST(dbo.orderhed.number19 AS decimal) AS TipoArmazon, dbo.orderhed.checkbox01 AS Biselado, ISNULL(dbo.orderhed.shortchar06, 0) AS Antireflejante, 
                      ISNULL(dbo.orderhed.shortchar09, 0) AS Tinte, dbo.orderhed.checkbox02 AS Gradiente, ISNULL(dbo.orderhed.shortchar10, 0) AS Tono, 
                      CAST(dbo.orderhed.number02 AS decimal(8, 2)) AS EsferaR, CAST(dbo.orderhed.number06 AS decimal(8, 2)) AS EsferaL, CAST(dbo.orderhed.number03 AS decimal(8, 
                      2)) AS CilindroR, CAST(dbo.orderhed.number07 AS decimal(8, 2)) AS CilindroL, CAST(dbo.orderhed.number04 AS decimal) AS EjeR, 
                      CAST(dbo.orderhed.number08 AS decimal) AS EjeL, CAST(dbo.orderhed.number05 AS decimal(8, 2)) AS AdicionR, CAST(dbo.orderhed.number09 AS decimal(8, 2)) 
                      AS AdicionL, CAST(dbo.orderhed.number11 AS decimal) AS AlturaR, CAST(dbo.orderhed.number13 AS decimal) AS AlturaL, CAST(dbo.orderhed.number12 AS decimal) 
                      AS MonoR, CAST(dbo.orderhed.number14 AS decimal) AS MonoL, CAST(dbo.orderhed.number16 AS decimal) AS DIPle, CAST(dbo.orderhed.number15 AS DECIMAL) 
                      AS DIPce, CAST(dbo.orderhed.number17 AS DECIMAL) AS Altura, ISNULL(dbo.orderhed.shortchar02, 0) AS A, ISNULL(dbo.orderhed.shortchar03, 0) AS B, 
                      ISNULL(dbo.orderhed.shortchar04, 0) AS ED, ISNULL(dbo.orderhed.shortchar05, 0) AS Puente, ISNULL(dbo.orderhed.shortchar07, 0) AS MaterialID, 
                      ISNULL(dbo.orderhed.shortchar08, 0) AS DiseñoID, ISNULL(CAST(dbo.orderhed.number18 AS decimal), 0) AS ArmazonID, dbo.orderhed.checkbox03 AS Status, 
                      CAST(dbo.orderhed.number10 AS decimal) AS Foco, dbo.orderhed.orderdate, dbo.orderdtl.checkbox01 AS RxLado, dbo.VwPartTRECEUX.partnum, 
                      dbo.VwPartTRECEUX.partdescription, dbo.orderdtl.checkbox02 AS RxClase, dbo.orderhed.custnum, dbo.orderhed.checkbox04 AS Coated, 
                      dbo.orderhed.checkbox05 AS Abrillantado, dbo.orderhed.userchar1 AS PrismaR, dbo.orderhed.userchar2 AS EjePrismaR, dbo.orderhed.userchar3 AS PrismaL, 
                      dbo.orderhed.userchar4 AS EjePrismaL, dbo.orderhed.userdate1 AS FechaInicial, dbo.orderhed.date01 AS FechaSalida, dbo.orderhed.checkbox08 AS Retenida, 
                      CAST(dbo.orderhed.ordercomment AS varchar(150)) AS Comentarios, COALESCE (dbo.customer.number01, CAST(dbo.orderhed.character01 AS varchar(5))) AS Plant, 
                      COALESCE (dbo.customer.number02, CAST(dbo.orderhed.character01 AS varchar(5))) AS Plant2, COALESCE (dbo.customer.name, 'Lab. Virtual') AS CustName, 
                      dbo.orderhed.checkbox09 AS IsVirtualRx, CAST(dbo.orderhed.character01 AS varchar(10)) AS ARLab, dbo.orderhed.checkbox10 AS InARLab, 
                      dbo.orderhed.checkbox11 AS OutARLab, COALESCE (dbo.TblLaboratorios.Nombre, 'NINGUNO') AS ARLabName, dbo.orderhed.checkbox12 AS ReceivedInLocalLab, 
                      ISNULL(dbo.orderhed.date05, '') AS FechaARInLocalLab, dbo.orderhed.checkbox14 AS CheckLeftEye, dbo.orderhed.checkbox15 AS CheckRightEye, 
                      dbo.orderhed.checkbox16 AS RxInspected, CAST(dbo.orderhed.character05 AS varchar(150)) AS ComentariosLab, dbo.orderhed.checkbox17 AS IsGratis, 
                      dbo.orderhed.checkbox18 AS IsGarantia, COALESCE (dbo.orderhed.character06, '0') AS CostcoLente, COALESCE (dbo.orderhed.character07, '0') AS CostcoAR, 
                      COALESCE (dbo.orderhed.character08, '0') AS CostcoTinte, COALESCE (dbo.orderhed.character09, '0') AS CostcoPulido, dbo.orderdtl.orderline, 
                      dbo.orderhed.weborder AS IsWebRx, dbo.orderhed.date10 AS FechaEntradaWeb, dbo.orderhed.date11 AS FechaEntradaFisica, dbo.orderhed.ccfreight AS SeqID, 
                      COALESCE (dbo.orderhed.date02, '') AS date02, COALESCE (dbo.orderhed.date03, '') AS date03, COALESCE (dbo.orderhed.date04, '') AS date04, 
                      COALESCE (dbo.orderhed.date06, '') AS date06, COALESCE (dbo.orderhed.date07, '') AS date07, dbo.orderhed.checkbox11, dbo.orderhed.checkbox13, 
                      dbo.orderhed.checkbox20 AS Optimizado, CAST(dbo.customer.number06 AS int) AS TipoClte
FROM         dbo.VwPartTRECEUX WITH (NOLOCK) INNER JOIN
                      dbo.orderdtl WITH (NOLOCK) ON dbo.VwPartTRECEUX.company = dbo.orderdtl.company AND dbo.VwPartTRECEUX.partnum = dbo.orderdtl.partnum RIGHT OUTER JOIN
                      dbo.TblLaboratorios WITH (nolock) RIGHT OUTER JOIN
                      dbo.customer WITH (NOLOCK) RIGHT OUTER JOIN
                      dbo.orderhed WITH (NOLOCK) ON dbo.customer.custnum = dbo.orderhed.custnum AND dbo.customer.company = dbo.orderhed.company ON 
                      dbo.TblLaboratorios.cl_lab LIKE dbo.orderhed.character01 ON dbo.orderdtl.ordernum = dbo.orderhed.ordernum AND 
                      dbo.orderdtl.company = dbo.orderhed.company 
WHERE     (dbo.orderhed.company = 'TRECEUX') AND (dbo.orderhed.openorder = 1) AND (LEN(dbo.orderdtl.partnum) = 10)

GO

