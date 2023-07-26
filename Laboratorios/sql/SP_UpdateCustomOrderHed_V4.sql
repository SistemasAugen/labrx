USE [LocalLab2000]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustomOrderHed_V4]    Script Date: 12/26/2012 16:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-------------------------------------------------------------------------------------------------------------
-- Ver 4 (Dic 15 2009) [RL]
-- Agregar la opcion Diseños Especiales (satpickup)
-------------------------------------------------------------------------------------------------------------
-- Ultima Actualizacion 3/6/2008 para Ver 3.6.3 en adelante
-- Actualizado por: Francico Garcia
-- Actualizado 12/26/2012 para Ver Lab Rx 3.8.3.6  Pedro Farías Lozano
-- campo Costcolente contiene caracteres alfanuméricos = código de paquete  PKG_ABCD_1234
-- Actualizado 2/6/2013 para Ver Lab Rx 3.8.3.9  Pedro Farías Lozano
-- campo CostcoLente de 40 caracteres
-- Actualizado 3/28/2013 se sustituye llamada a CrearOrdenTRECEUX_V4 por SP_OrderEntryTRECEUX_V4 y poder funcionar con el nuevo pa'ametro @DESCTO y @LADO
-------------------------------------------------------------------------------------------------------------
-- Stored Procedure para actualizar el encabezado de un Sales Order con la informacion de la receta
-------------------------------------------------------------------------------------------------------------


-- Ejemplo de uso:
-- SP_UpdateCustomOrderHed 'TRECEUX',15,2774469,446901,1,1,1,0,0,0,0,1,-4.5,22,0,1,-2,167,0,15,15,0,0,60,58,15,48.6,26.92,48.63,18.3,2,2,0,0,0,'x'

ALTER      PROCEDURE [dbo].[SP_UpdateCustomOrderHed_V4]
	@Company as varchar(15),
	@OrderNum as decimal,
	@CUSTNUM AS DECIMAL,
	@OrdenLab as varchar(30),
	@RefDigital as decimal,
	@TrazadoArmazon as bit,
	@TipoArmazon as decimal,
	@Biselado as bit,
	@Antireflejante as varchar(2),
	@Color as varchar(2),
	@Gradiente as bit,
	@Tono as varchar(2),
	@EsferaR as decimal(8,2),
	@CilindroR as decimal(8,2),
	@EjeR as decimal,
	@AdicionR as decimal(8,2),
	@EsferaL as decimal(8,2),
	@CilindroL as decimal(8,2),
	@EjeL as decimal,
	@AdicionL as decimal(8,2),
	@AlturaR as decimal,
	@AlturaL as decimal,
	@MonoR as decimal,
	@MonoL as decimal,
	@DIPle as decimal,
	@DIPce as decimal,
	@Altura as decimal,
	@A as varchar(8),
	@B as varchar(8),
	@ED as varchar(8),
	@Puente as varchar(8),
	@Material as varchar(2),
	@Diseno as varchar(2),
	@ArmazonID as int,		
	@Status as bit,
	@Foco as int,			
	@Coated as bit,
	@Abrillantado as bit,
	@Optimizado as bit,
	@PrismaR as varchar(8),
	@EjePrismaR as varchar(8),
	@PrismaL as varchar(8),
	@EjePrismaL as varchar(8),
	@CurrentLab as int,		-- Laboratorio que esta haciendo la modificacion
	@ARLab as varchar(10),		-- Laboratorio  de AR asignado [Solo si aplica]
	@OriginalRefDigital as int,	-- No. Ref. Digital Original de la Rx
	@ARFunction as int,		-- Si lleva AR, que se va a modificar con respecto a AR. [0:No op. | 1: Modifica Lab. Local | 2: Modifica Lab. AR | 3: Entrada a Lab AR]
	@CostcoLente as varchar(40),  -- Código del paquete = partnum
	@CostcoAR as varchar(8),
	@CostcoTinte as varchar(8),
	@CostcoPulido as varchar(8),
	@Comentarios as varchar(4000),
	@SeqID as int,
	@FechaEntradaWeb as datetime,
	@FechaEntradaFisica as datetime,
	@Sincronizada as bit,
	@date03 as datetime,
	@date04 as datetime,
	@date06 as datetime,
	@date07 as datetime,
	@LocalReceiveDate as datetime,	-- date05
	@ReceivedARDate as datetime,	-- date02
	@ReceivedAR as bit,
	@OutARLab as bit,
	@LocalReceive as bit,
	@Reproceso as bit,
	@FinishedQA as bit,
	@SpecialDesignID as tinyint,
	@Msg as varchar(15) output
AS
--@ReceivedAR	=	checkbox10
--@OutARLab		=	checkbox11
--@LocalReceive =	checkbox12
--@Reproceso	=	checkbox13
--@FinishedQA	=	checkbox16

begin transaction
	declare @ProcesoQA		int
	declare @TiempoDeRuta	int
	declare @IsVirtualRx as bit
	declare @IsModifying as bit
	declare @PlantID as varchar(15)
--	declare @ReceivedAR as bit
--	declare @OutARLab as bit
--	declare	@LocalReceive as bit
--	declare @Reproceso as bit
--	declare @FinishedQA as bit
--  declare @date05 as datetime
	declare @IsGratis as bit
	declare @IsGarantia as bit
	declare @index as int
	declare @GeneratedYear as varchar(4)
	declare @IsWebRx as bit
	
	--------------------------------------------------------
	-- Checar si es RxVirtual y a que Lab se mandara
	--------------------------------------------------------
	set @IsModifying = 0
--	set @ReceivedAR = 0
	set @IsVirtualRx = 0
--	set @LocalReceive = 0
--	set @Reproceso = 0
--	set @OutARLab = 0

	--------------------------------------------------------
	-- Checa si la Rx es Garantia
	--------------------------------------------------------
	set @IsGarantia = 0
	set @index = charindex('[GARANTIA_RX]', @Comentarios)
	if @index > 0
	begin
		set @IsGarantia = 1
		set @Comentarios = substring(@Comentarios,1,@index-1)
	end
	--------------------------------------------------------
	-- Checa si la Rx sera gratis por el Programa Cliente Distinguido
	--------------------------------------------------------
	set @IsGratis = 0
	set @index = charindex('[FREE_RX]', @Comentarios)
	if @index > 0
	begin
		set @IsGratis = 1
		set @Comentarios = substring(@Comentarios,1,@index-1)
	end
	--------------------------------------------------------
	-- Checa si la Rx se metio por Web
	--------------------------------------------------------
	set @IsWebRx = 0
	set @index = charindex('[WEB_RX]', @Comentarios)
	if @index > 0
	begin
		set @IsWebRx = 1
		set @Comentarios = substring(@Comentarios,1,@index-1)
	end

	--------------------------------------------------------
	-- Checo cuando se genero la Rx, el año, y si es garantia
	-- se busca cuando fue la ultima buena que se genero
	--------------------------------------------------------
	if @IsGarantia = 1
	begin
		select @GeneratedYear = shipcomment from orderhed with(nolock) where company = 'TRECEUX' and shortchar01 like @OrdenLab+'%' order by convert(varchar(4),shipcomment) desc
		if @GeneratedYear = ''
		begin
			set @GeneratedYear = convert(varchar(4),year(getdate()))
		end
	end
	else
	begin
		set @GeneratedYear = convert(varchar(4),year(getdate()))
	end	
	if @ARLab > 0 -- and @Antireflejante not like '0'
	begin
		set @IsVirtualRx = 1
	end

	if (@ReceivedARDate < '1/1/1901') set @ReceivedARDate=NULL		--DATE02
	if (@date03 < '1/1/1901') set @date03=NULL
	if (@date04 < '1/1/1901') set @date04=NULL
	if (@LocalReceiveDate < '1/1/1901') set @LocalReceiveDate=NULL	--DATE05
	if (@date06 < '1/1/1901') set @date06=NULL
	if (@date07 < '1/1/1901') set @date07=NULL

	if @IsVirtualRx = 1
	begin
		--select @IsModifying = checkbox10 from orderhed with(nolock) where ordernum = @OrderNum

		--if exists(select * from customer where custnum = @CUSTNUM and number01 = @PlantID)
		--begin
		--	set @IsModifying = 1
		--end


		if @ARFunction = 5
		begin
			set @date03 = null
			set @date04 = null
			set @date06 = null
			set @date07 = null
			set @ReceivedARDate = getdate()
			set @FinishedQA = 0	
			set @ProcesoQA = 0
		end


------  COMENTADO POR FRANCISCO GARCIA EL 3/6/2008 PORQUE ESTABA PONIENDO EL CHECKBOX16 EN 0 Y ESTE PARAMETRO YA SE RECIBE
------		if @ARFunction < 5
------		begin
------			select @FinishedQA = checkbox16, @ReceivedARDate = date02  from orderhed with(nolock) where ordernum = @OrderNum
------		end
------		else if @ARFunction = 5
------		begin
------			set @date03 = null
------			set @date04 = null
------			set @date06 = null
------			set @date07 = null
------			set @ReceivedARDate = getdate()
------			set @FinishedQA = 0	
------		end

		-- Hay una modificacion en laboratorio local que no consume pastilla
		--------------------------------------------------------------------
		if @ARFunction = 1
		begin
			declare @AR as varchar(10)
			--select  @OriginalRefDigital = userinteger2, @Reproceso = checkbox13, @OutARLab = checkbox11, @LocalReceive = checkbox12, @LocalReceiveDate = date05, @ReceivedARDate = date02, @ReceivedAR = checkbox10 from orderhed with(nolock) where ordernum = @OrderNum
--			SELECT @OriginalRefDigital = userinteger2, @ReceivedAR = checkbox10, @OutARLab = checkbox11, @LocalReceive = checkbox12, @Reproceso = checkbox13, 
--				   @ReceivedARDate = date02, @LocalReceiveDate = date05 FROM orderhed WITH(NOLOCK) WHERE ordernum = @OrderNum
			SELECT @OriginalRefDigital = userinteger2, @ProcesoQA = cctax, @date03=date03, @date04=date04, @OutARLab=checkbox11 FROM orderhed WITH(NOLOCK) WHERE ordernum = @OrderNum
			
			-- Si estoy en local y el laboratorio es el mismo, entonces marcar como que ya entro al laboratorio
			if @CurrentLab = @ARLab
			begin
				set @ReceivedAR = 1
				set @OriginalRefDigital = @RefDigital
			end
			else if @LocalReceive = 0
			begin
				set @ReceivedAR = 0
				set @ReceivedARDate = getdate()
			end

			-- COMENTADO POR FCO 7/27/2009
--			if @LocalReceive = 0
--			begin
--				set @FinishedQA = 0
--				set @OutARLab = 0
--			end			
		end

--checkbox09 = @IsVirtualRx, checkbox10 = @ReceivedAR, checkbox11 = @OutARLab, checkbox12 = @LocalReceive, checkbox16 = @FinishedQA
--character01 = @ARLab, userinteger2 = @OriginalRefDigital, 


----		if @ARFunction = 1
----		begin
----			declare @AR as varchar(10)
----			--select @date03 = date03, @date04 = date04, @date05 = date05, @date06 = date06 from orderhed with(nolock) where ordernum = @OrderNum
----			select  @AR = character01, @OriginalRefDigital = userinteger2, @Reproceso = checkbox13, @OutARLab = checkbox11, @LocalReceive = checkbox12, @date05 = date05, @date02 = date02, @ReceivedAR = checkbox10 from orderhed with(nolock) where ordernum = @OrderNum
----			
----			-- Si estoy en local y el laboratorio es el mismo, entonces marcar como que ya entro al laboratorio
----
----			if @CurrentLab = @ARLab
----			begin
----				set @ReceivedAR = 1
----				set @OriginalRefDigital = @RefDigital
----			end
----			else if @AR <> @ARLab
----			begin
----				set @ReceivedAR = 0
----			end
----		end


		-- Hay una modificacion en laboratorio remoto
		--------------------------------------------------------------------
		else if  @ARFunction = 2
		begin
			--select  @Reproceso = checkbox13, @LocalReceive = checkbox12, @LocalReceiveDate = date05, @ReceivedARDate = date02 from orderhed with(nolock) where ordernum = @OrderNum
			SELECT @ProcesoQA = cctax from orderhed with(nolock)  where ordernum = @OrderNum

			set @OriginalRefDigital = @RefDigital
			set @ReceivedAR = 1
			set @FinishedQA = 0
			set @OutARLab = 0
		end
		-- Se le da entrada al laboratorio remoto
		--------------------------------------------------------------------
		else if @ARFunction = 3
		begin
			--select  @Reproceso = checkbox13, @LocalReceiveDate = date05, @ReceivedARDate = date02, @OriginalRefDigital = userinteger2 from orderhed with(nolock) where ordernum = @OrderNum
			set @date07 = getdate()
			set @ReceivedAR = 1
			set @LocalReceive = 0
			set @FinishedQA = 0
			set @OutARLab = 0
			set @ProcesoQA = 0
		end
		-- Se le da entrada al laboratorio local cuando el remoto ya la libero
		--------------------------------------------------------------------
		else if @ARFunction = 4
		begin
			set @LocalReceive = 1
			set @ReceivedAR = 1
			set @FinishedQA = 1
			set @OutARLab = 1
			set @LocalReceiveDate = getdate()
		end
		-- Se marca una receta en reproceso pues la pastilla se echo a perder
		--------------------------------------------------------------------
		else if @ARFunction = 5
		begin

			set @LocalReceive = 0
			set @OriginalRefDigital = @RefDigital
			set @ReceivedAR = 0	
			set @Reproceso = 1	
			set @OutARLab = 0	
			set @FinishedQA = 0
			set @ProcesoQA = 0
			set @date04 = null
			set @ReceivedARDate = getdate()

			if @CurrentLab = @ARLab
			begin
				set @ReceivedAR = 1
				set @date07 = getdate()
			end
			else
			begin
				set @ReceivedAR = 0
			end
			set @LocalReceiveDate = null

		end
	end
	else
	begin

		-- Agregado por Fco Garcia Enero 23, 2010.
		-- Cuando la rx no llevaba AR y se hacia una modificacion por recaptura SP se borraban los registros
		-- de QA
		if @ARFunction = 1
		begin
			SELECT @FinishedQA=checkbox16, @date06=date06, @ProcesoQA = cctax FROM orderhed WITH(NOLOCK) WHERE ordernum = @OrderNum
		end
		else
		begin		-- Solo estaba esto antes del cambio hecho por Fco 
			--select @ReceivedARDate = date02 from orderhed with(nolock) where ordernum = @OrderNum
			set @ProcesoQA=0
			set @FinishedQA=0
			set @ReceivedARDate = null
			set @date03 = null
			set @date04 = null
	--		set @date05 = null
			set @date06 = null
			set @ARLab = ''
			set @IsVirtualRx = 0
			set @OriginalRefDigital = @RefDigital
		end

	end

	--------------------------------------------------------
	-- Checar el tiempo que tardara en la ruta
	--------------------------------------------------------

--	if @Antireflejante <> 0
--	begin
--		set @TiempoDeRuta = 3
--		if @ARFunction = 4
--		begin
--			set @TiempoDeRuta = 1
--		end
--
--	end
--	else

	set @TiempoDeRuta = 3
 	if @Biselado = 0 
	begin
		set @TiempoDeRuta = 3
	end
	else if @TipoArmazon = 1 or @TipoArmazon = 2
	begin
		if (@ARFunction = 1 or @ARFunction = 4) and @LocalReceiveDate is not null
		begin
			set @TiempoDeRuta = 1
		end
		if @IsVirtualRx = 0
		begin
			set @TiempoDeRuta = 4
		end
	end
	else
	begin
		if (@ARFunction = 1 or @ARFunction = 4) and @LocalReceiveDate is not null
		begin
			set @TiempoDeRuta = 2
		end
		if @IsVirtualRx = 0
		begin
			set @TiempoDeRuta = 5
		end
	end


	declare @startdate datetime
	declare @finaldate datetime
	declare @day integer
	declare @sdate varchar(10)
	declare @nextday as varchar(10)
	declare @starthour as integer
	declare @inithour as integer
	declare @endhour as integer
	declare @dayhours as integer
	declare @dy as integer
	declare @dw as integer
	declare @LaborCode as varchar(5)
	declare @initdate as datetime
	declare @TotalTime integer
	declare @mins as integer
	declare @Plant varchar(10)
	declare @LocalReceived as bit

	
	set @Plant = substring(cast(@ordernum as varchar(10)),1,2)
	set @TotalTime = @TiempoDeRuta
	if @ARFunction = 4		-- Si se esta recibiendo en lab. local, se toma como fecha de inicio la actual
	begin
		set @startdate = getdate()
	end
	else				-- De otra manera, se toma la inicial.
	begin
		select @startdate = userdate1 from orderhed with(nolock) where ordernum = @ordernum and company = @Company
	end
	if @ARFunction = 1		-- Si es modificacion local
	begin
		select @LocalReceived = checkbox12 from orderhed with(nolock) where ordernum = @ordernum and company = @Company
		if @LocalReceived = 1	-- Y ya fue recibida, se toma la fecha de inicio de la fecha en que se recibio el el lab. local
		begin
			select @startdate = date05 from orderhed with(nolock) where ordernum = @ordernum and company = @Company
		end
	end
	set @initdate = @startdate
	set @sdate = convert(varchar(10),@startdate,101)
	set @day = datepart(dw,@startdate)
	
	set datefirst 7
	
	set @nextday = convert(varchar(10),@startdate,101)
	set @starthour = datepart(hh,@startdate)
	set @mins = datepart(mi,@startdate)
	
	select @startdate as [Fecha Inicial], @TiempoDeRuta as [Duracion]
	
--	while  (@TiempoDeRuta > 0) or (@mins > 0)
	while  (@TiempoDeRuta > 0) or (@TiempoDeRuta = 0 and @mins > 0) 
	begin
	
		SELECT     @inithour = dbo.LaborCodes.InitHour, @endhour = dbo.LaborCodes.EndHour, 
		                      @dayhours = dbo.LaborCodes.Hours
		FROM         dbo.Calendar WITH(NOLOCK) INNER JOIN
		                      dbo.LaborCodes WITH(NOLOCK) ON dbo.Calendar.LaborCode = dbo.LaborCodes.LaborCode
		WHERE     (dbo.Calendar.[Date] = CONVERT(varchar(10), @nextday, 101)) AND (dbo.LaborCodes.Plant = @Plant)
	
		if @inithour is null
		begin
			select 'Error en el calendario local'
			break
		end

		if @inithour = 0
		begin
			set @starthour = 0
		end
		else if @starthour = 0
		begin
			set @starthour = @inithour
		end
		if (@TiempoDeRuta <= 0) and (@inithour > 0)
		begin
			set @finaldate = @nextday + ' ' + convert(varchar(10),@inithour) + ':' + convert(varchar(10),@mins)
			set @mins = 0
		end
		else
		begin
			set @TiempoDeRuta = @TiempoDeRuta - (@endhour - @starthour)
			set @starthour = @inithour
			if @TiempoDeRuta <= 0 and (@inithour > 0)
			begin
				set @finaldate = @nextday + ' ' + convert(varchar(10),@endhour + @TiempoDeRuta) + ':' + convert(varchar(10),@mins)
			end
		end
		set @nextday = convert(varchar(10),dateadd(dd,1,@nextday),101)
	end
	set @TiempoDeRuta = @TotalTime
	declare @retenida as bit
	declare @retdate as datetime
	select @retenida= checkbox08, @retdate = date01 from orderhed  WITH(NOLOCK) where ordernum = @OrderNum
	if @retenida = 1 
	begin
		set @finaldate = @retdate
	end

	--------------------------------------------------------
	if (@ARLab='0') 
		set @ARLab=''

	if not exists(select ordernum from orderhed with(nolock) where ordernum = @OrderNum and company = @Company)
	begin
		declare @Mensaje as int
		declare @EntryPerson as varchar(5)
		declare @date as datetime

		set @date = getdate()
		set @EntryPerson = 'LocalServer'

        EXEC SP_OrderEntryTRECEUX_V4 @Company,@Plant,@OrderNum ,@Custnum ,@EntryPerson,@OrdenLab 
			,0,0,@date,'','','', @Comentarios, @OrdenLab,@RefDigital ,@TrazadoArmazon,
			@TipoArmazon ,@Biselado,@Antireflejante ,@Color ,@Gradiente ,@Tono ,@EsferaR,
			@CilindroR ,@EjeR ,@AdicionR,@EsferaL ,@CilindroL,@EjeL ,@AdicionL ,@AlturaR,
			@AlturaL,@MonoR,@MonoL,@DIPLe,@DIPCe,@Altura ,@A ,@B ,@ED ,@Puente ,
			@Material,@Diseno, @ArmazonID, @Status, @Foco, @Coated, @Abrillantado, @Optimizado,	@PrismaR,
			@EjePrismaR,@PrismaL,@EjePrismaL,0,0,@CostcoLente,@CostcoAR,
			@CostcoTinte,@CostcoPulido,@SeqID,@FechaEntradaWeb,@FechaEntradaFisica,@Sincronizada,@SpecialDesignID,
			@Msg OUTPUT

	end
	UPDATE    orderhed
	SET		sincronizada = @Sincronizada, openorder = 1, ponum=@OrdenLab, shortchar01 = @OrdenLab, number01 = @RefDigital, number20 = @TrazadoArmazon, number19 = @TipoArmazon, 
			checkbox01 = @Biselado, shortchar06 = @Antireflejante, shortchar09 = @Color, checkbox02 = @Gradiente,
			shortchar10 = @Tono, number02 = @EsferaR, number03 = @CilindroR, number04 = @EjeR, number05 = @AdicionR,
			number06 = @EsferaL, number07 = @CilindroL, number08 = @EjeL, number09 = @AdicionL, number11 = @AlturaR,
			number12 = @MonoR, number13 = @AlturaL, number14 = @MonoL, number15 = @DIPce, number16 = @DIPle, 
			number17 = @Altura, shortchar02 = @A, shortchar03 = @B, shortchar04 = @ED, shortchar05 = @Puente, 
			shortchar07 = @Material, shortchar08 = @Diseno, number18 = @ArmazonID, checkbox03 = @Status, number10 = @Foco, 
			checkbox04 = @Coated, checkbox05 = @Abrillantado, checkbox20 = @Optimizado, userchar1 = @PrismaR, userchar2 = @EjePrismaR, 
			userchar3 = @PrismaL, userchar4 = @EjePrismaL,CUSTNUM = @CUSTNUM, userinteger1 = @TiempoDeRuta, date01 = @finaldate,
			checkbox09 = @IsVirtualRx, character01 = @ARLab, userinteger2 = @OriginalRefDigital, checkbox10 = @ReceivedAR,
			date02 = @ReceivedARDate, checkbox12 = @LocalReceive, date05 = @LocalReceiveDate, checkbox13 = @Reproceso,
			checkbox11 = @OutARLab,date03 = @date03, date04 = @date04, date06 = @date06, date07 = @date07, character05 = @Comentarios,
			checkbox17 = @IsGratis, checkbox18 = @IsGarantia, character06 = @CostcoLente, character07 = @CostcoAR, character08 = @CostcoTinte, character09 = @CostcoPulido,
			checkbox16 = @FinishedQA, shipcomment = @GeneratedYear, weborder = @IsWebRx, ccfreight = @SeqID,	date10 = @FechaEntradaWeb, date11 = @FechaEntradaFisica,
			cctax = @ProcesoQA, satpickup = @SpecialDesignID
	WHERE	(ordernum = @OrderNum) AND (company = @Company)
	if @@error > 0
	begin
		rollback tran
		select @Msg = @Msg +' Falló actualizacion de la orden ' + @OrderNum
		return 
	end
	UPDATE	orderdtl
	SET		shortchar01 = @OrdenLab,CUSTNUM = @CUSTNUM
	WHERE	(ordernum = @OrderNum) AND (company = @Company)
	--------------------------------------------------------
	-- Pongo la informacion a fuerzas
	--------------------------------------------------------
	--declare @seqid as int
	--declare @arrived_date as datetime
	--select top 1 @seqid = seqid, @arrived_date = arrived_date from TblWebOrders where ponumber = @OrdenLab and custnum = @custnum and ordernum is null order by seqid desc
	update TblWebOrders set process_status = 1, ordernum = @OrderNum where ponumber = @OrdenLab and custnum = @custnum and seqid = @SeqID

	select @Msg = 'OK'
commit transaction

set ANSI_NULLS OFF
set QUOTED_IDENTIFIER OFF






