USE [LocalLab2000]
GO
/****** Object:  StoredProcedure [dbo].[SP_TrazosManualesOMA]    Script Date: 08/01/2013 17:21:29 ******/
/* Bug Fix Pedro Farías Lozano  1 AGO 2013 */
/* El SP no proporcionaba el consecutivo porque tenía un error al momento de seleccionar los últimos dos dígitos de un jobnum existente */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER     PROCEDURE [dbo].[SP_TrazosManualesOMA] 
	@JobNum varchar(4),
	@JobUnico varchar(6) output
AS
declare @LastJob nvarchar(6)
declare @Incremento nvarchar(2)
declare @JobOK bit

--BUSCAMOS EL ULTIMO JOB GENERADO CON LA BASE DEL JOB (4 DIGITOS)
IF (SELECT max(jobnum) from OMATraces with(nolock) where jobnum like @jobnum + '%') is null
BEGIN

	set @JobUnico = @Jobnum + '00' 
	
END
ELSE
BEGIN
select 'si entro',@jobnum
	set @LastJob = (SELECT max(jobnum) from OMATraces with(nolock) where jobnum like @jobnum + '%')
	set @Incremento = cast(substring(@LastJob,5,2) as int)
	set @JobOK  = 0

	WHILE(@JobOK = 0)
	BEGIN
			IF (@incremento = '99') 
			BEGIN
				set @Incremento = '00'
			END
			ELSE
			BEGIN
				set @incremento  = cast(@incremento as int) + 1
			END

			IF (len(@Incremento) = 1)
				set @Incremento = '0' + @Incremento

			IF not exists(select jobnum from OMATraces with(nolock) where jobnum = @jobnum + @incremento)
			BEGIN
				set @JobUnico = @Jobnum + @Incremento
				SET @JobOK = 1
			END
		
	END --CIERRA EL BEGIN DEL WHILE
		
END --CIERRA EL ELSE DEL PRIMER IF



