USE [LocalLab2000]
GO

/****** Object:  View [dbo].[VwWebOrders]    Script Date: 11/29/2012 16:07:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP VIEW [dbo].[VwWebOrders]
/****** Vista nueva que obtiene el valor de NumPaquete de la tabla TblWebOrders ******/
/******  Pedro Farías L.  Nov 28 2012 ******/
CREATE VIEW [dbo].[VwWebOrders]
AS
SELECT     TOP (100) PERCENT dbo.TblWebOrders.arrived_date AS Fecha, dbo.customer.custnum, dbo.customer.number01 AS LabID, dbo.TblWebOrders.ponumber AS OrdenLab,
                       dbo.TblWebOrders.a, dbo.TblWebOrders.b, dbo.TblWebOrders.ed, dbo.TblWebOrders.bridge AS Puente, dbo.TblWebOrders.antireflection AS Antireflejante, 
                      dbo.TblWebOrders.material AS MaterialID, dbo.TblWebOrders.design AS DisenoID, dbo.TblWebOrders.tint AS Tinte, dbo.TblWebOrders.rsphere AS EsferaR, 
                      dbo.TblWebOrders.rcylinder AS CilindroR, dbo.TblWebOrders.raxis AS EjeR, dbo.TblWebOrders.raddition AS AdicionR, dbo.TblWebOrders.lsphere AS EsferaL, 
                      dbo.TblWebOrders.lcylinder AS CilindroL, dbo.TblWebOrders.laxis AS EjeL, dbo.TblWebOrders.laddition AS AdicionL, dbo.TblWebOrders.focus AS Foco, 
                      dbo.TblWebOrders.rheight AS AlturaR, dbo.TblWebOrders.rdip AS MonoR, dbo.TblWebOrders.lheight AS AlturaL, dbo.TblWebOrders.ldip AS MonoL, 
                      dbo.TblWebOrders.rneardip + dbo.TblWebOrders.lneardip AS DIPce, dbo.TblWebOrders.fardip AS DIPle, dbo.TblWebOrders.lheight AS Altura, 
                      dbo.TblWebOrders.framenum AS NumArmazon, dbo.TblWebOrders.frameedge AS Contorno, 0 AS CapturaArmazon, dbo.TblWebOrders.beveled AS Biselado, 
                      dbo.TblWebOrders.tintgradient AS Gradiente, dbo.TblWebOrders.process_status AS Status, dbo.customer.name, dbo.TblWebOrders.pulished AS Abrillantado, 
                      dbo.TblWebOrders.notes AS ComentariosLab, dbo.TblWebOrders.NumPaquete AS CostcoLente, dbo.TblWebOrders.antireflection AS CostcoAR, 
                      COALESCE (dbo.TblWebOrders.isGratis, 0) AS IsGratis, CASE WHEN (dbo.TblWebOrders.pulished > 0) THEN 515764 ELSE 0 END AS CostcoPulido, 
                      dbo.TblWebOrders.tint AS CostcoTinte, dbo.customer.custid, dbo.TblWebOrders.arrived_date AS orderdate, dbo.GetLente(dbo.TblWebOrders.material, 
                      dbo.TblWebOrders.design) AS CostcoLente2, dbo.TblWebOrders.seqid, dbo.TblMaterials.material, dbo.TblDesigns.diseno, 
                      dbo.TblEntradaCOSTCO.Fecha AS FechaEntradaFisica, dbo.TblWebOrders.arrived_date AS FechaEntradaWeb, '' AS date02, '' AS date03, '' AS date04, 
                      '' AS FechaArInLocalLab, '' AS date06, '' AS date07, 0 AS Optimizado, dbo.TblWebOrders.NumPaquete AS Paquete, COALESCE (dbo.TblWebOrders.idFreeForm, 0) 
                      AS idFreeForm
FROM         dbo.TblMaterials INNER JOIN
                      dbo.customer WITH (NOLOCK) INNER JOIN
                      dbo.TblWebOrders WITH (NOLOCK) ON dbo.customer.custnum = dbo.TblWebOrders.custnum ON dbo.TblMaterials.cl_mat = dbo.TblWebOrders.material INNER JOIN
                      dbo.TblDesigns WITH (NOLOCK) ON dbo.TblWebOrders.design = dbo.TblDesigns.cl_diseno LEFT OUTER JOIN
                      dbo.TblEntradaCOSTCO WITH (NOLOCK) ON dbo.TblWebOrders.seqid = dbo.TblEntradaCOSTCO.SeqID AND 
                      dbo.TblWebOrders.ponumber = dbo.TblEntradaCOSTCO.PO
WHERE     (dbo.customer.company = 'TRECEUX') AND (dbo.TblWebOrders.process_status = 0)

GO
