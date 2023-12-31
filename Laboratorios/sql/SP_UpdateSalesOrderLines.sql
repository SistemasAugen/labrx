USE [LocalLab2000]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSalesOrderLines]    Script Date: 02/06/2013 16:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*-----------------------------------------------------------------------------------------------------------------------------------------------------------------
 Stored Procedure para actualizar el encabezado de un Sales Order con la informacion de la receta
-----------------------------------------------------------------------------------------------------------------------------------------------------------------*/

-- Ejemplo de uso:
-- SP_UpdateSalesOrderLines 'TRECEUX',15,'2105030007,1,0,2105022376,1,1','x'
-- Parametros se conforma por:  [NoParte,Cantidad,Lado]
--			donde lado => 0: Izquierdo[ST] / 1: Derecho[ST] / 2: Izquierdo[T] / 3:Derecho[T]

-- Update Ago 30 2012 Pedro Farias
-- Agregar precios especial en AR usando @Diseno
-------------------------------------------------------------------------------------------------------------
-- Update Sep 14 2012 Pedro Farías
-- Arreglado precio en 0 cuando es orden de cadena=2 Sanborns y no se encuentra el prodcode o partnum con el HDRx indicado
-- Update Sep 25 2012 Pedro Farías
-- Arreglado precios en 0 cuando es una cadena. No se aplica precio especial para Trivex, CR-39 o Parasol

-- Pedro Farías L. Nov 28 2012
-- Pedro Farías L. Feb  6 2012
-- Pedro Farías L. Mar 12 2013  -- Precios nuevos para terminados con AR especial

ALTER     PROCEDURE [dbo].[SP_UpdateSalesOrderLines] 
	@Company as varchar(15),
	@OrderNum as decimal,
	@parametros as text,
	@Msg as varchar(15) output
AS
begin transaction

declare @ptr  varbinary(16)
declare @length int
declare @pos as int
declare @par as int
declare @orderline as int
declare @dato as varchar(20)
declare @partnum as varchar(20)
declare @partqty as int
declare @auxprice as decimal(8,2)
declare @price as decimal(8,2)
declare @custnum as int
declare @descr as varchar(80)
declare @ium as varchar(5)
declare @typecode as varchar(15)
declare @pricexcode as varchar(5)
declare @expDiv as varchar(50)
declare @expChart as varchar(50)
declare @expDept as varchar(50)
declare @prodcode as varchar(50)
declare @costmethod as  varchar(1)
declare @salesum as varchar(2)
declare @sellingfactor as decimal(18,8)
declare @maxProg as int
declare @descuento as decimal(8,2)
declare @descto as decimal (8,2)
declare @maxOrden as int
declare @current_date as datetime
declare @fecenvio as datetime
declare @warehousecode as varchar(10)
declare @mensajeria as varchar(15)
declare @lado as bit
declare @clase as bit
declare @OrdenLab as varchar(30)
declare @listcode as varchar(10)
declare @compania as varchar(10)
declare @IsGratis as bit
declare @IsGarantia as bit
declare @TaxCatID as varchar(4)			-- [NUEVO]
declare @CostcoLente as varchar(40)

declare @HDRxPrice as decimal(8,2)		-- [NUEVO]
declare @HDRxListcode as varchar(10)

declare @cadena as int
declare @Optimizado bit
declare @skusbn varchar(20)

declare @Material			varchar(2)			-- [NEW]
declare @sd_part			varchar(50)
declare @SpecialDesignID	varchar(5)

----- Mod para AR Trivex CR Parasol con AR Augen Matiz-e
-- Pedro Farías Lozano  Julio 23 2012
declare @Antireflejante as varchar(2)
declare @Diseno as varchar(8)
----- Termina Mod para AR Trivex CR Parasol con AR Augen Matiz-e

set @TaxCatID = 'MX10'					-- [NUEVO]


set @compania = @company
--SET IDENTITY_INSERT PartWhse ON
---- Creo un dataset para guardar los rows del select
---------------------------------------------------------------------
--DECLARE dataset CURSOR FOR
--	SELECT     orderrel.warehousecode, orderdtl.orderqty, orderrel.partnum
--	FROM         orderrel with (nolock) INNER JOIN
--	                      orderdtl ON orderrel.company = orderdtl.company AND orderrel.ordernum = orderdtl.ordernum AND orderrel.orderline = orderdtl.orderline
--	WHERE     (orderrel.company = @Company) AND (orderrel.ordernum = @OrderNum)		
--
--OPEN dataset
--
--	FETCH NEXT FROM dataset INTO
--	@warehousecode, @partqty, @partnum
--	
--	-- Recorro cada uno de los registros del Select
--	-------------------------------------------------------------------
--	WHILE @@FETCH_STATUS = 0
--	BEGIN
--		UPDATE PartWhse
--		SET allocqty = allocqty - @partqty, salesallocqty = salesallocqty - @partqty
--		WHERE (company = @company) AND (partnum = @partnum) AND (warehousecode = @warehousecode)			 
--		if @@error <> 0
--		begin
--			ROLLBACK TRAN
--			SET IDENTITY_INSERT PartWhse OFF
--			select @Msg = 'No se pudo actualizar la tabla PartWhse'
--			RETURN
--		end
--		FETCH NEXT FROM dataset INTO
--		@warehousecode, @partqty, @partnum
--	END
--CLOSE dataset
--DEALLOCATE dataset
--
--SET IDENTITY_INSERT PartWhse OFF

-- Borro los registros que haya en las tablas con respecto a las pastillas
---------------------------------------------------------------------------------------------------------------------------
-- Borro registros de tabla OrderDtl
----------------------------------------------------------
set IDENTITY_INSERT  OrderDtl ON
DELETE FROM OrderDtl
WHERE     (company = @Company) AND (ordernum = @OrderNum)
if @@error > 0
begin
	rollback tran
	set IDENTITY_INSERT  OrderDtl OFF
	select @Msg = 'No se pudo eliminar registros de la tabla OrderDtl para la orden ' + @OrderNum
	return
end
set IDENTITY_INSERT  OrderDtl OFF

-- Borro registros de tabla OrderRel
----------------------------------------------------------
set IDENTITY_INSERT  OrderRel ON
DELETE FROM OrderRel
WHERE     (company = @Company) AND (ordernum = @OrderNum)
if @@error > 0
begin
	rollback tran
	set IDENTITY_INSERT  orderdtl OFF
	select @Msg = 'No se pudo eliminar registros de la tabla OrderRel para la orden ' + @OrderNum
	return
end
set IDENTITY_INSERT  OrderRel OFF

-- Borro registros de tabla PartDtl
------------------------------------------------------------
set IDENTITY_INSERT  PartDtl ON
DELETE FROM PartDtl
WHERE     (company = @Company) AND (ordernum = @OrderNum)
if @@error > 0
begin
	rollback tran
	set IDENTITY_INSERT  PartDtl OFF
	select @Msg = 'No se pudo eliminar registros de la tabla PartDtl para la orden ' + @OrderNum
	return
end
set IDENTITY_INSERT  PartDtl OFF
---------------------------------------------------------------------------------------------------------------------------

select  @ptr = TEXTPTR(a) from tblText
writetext tblText.a @ptr @parametros
select @length = datalength(a) from tblText
updatetext tblText.a @ptr @length 0 ','


select @pos = charindex(',',a) from tblText
set @par=0
set @orderline = 1
if @Company = 'TRECEUX'
begin
	set @warehousecode = 'MP01'
end
if @Company = 'AUGEN'
begin
	set @warehousecode = 'PT01'
end

-- Verifica el numero de parte del diseño Free Form si es que la receta lo lleva.
----- Mod para AR Trivex CR Parasol con AR Augen Matiz-e
-- Pedro Farías Lozano  Julio 23 2012
SELECT @SpecialDesignID=cast(character07 as varchar(5)),@AntiReflejante = shortchar06,@Diseno=shortchar08,@Material = shortchar07, @IsGratis = checkbox17, @IsGarantia = checkbox18, @custnum = custnum, @OrdenLab = shortchar01, @CostcoLente=character06 FROM orderhed WHERE ordernum=@OrderNum 
----- Termina Mod para AR Trivex CR Parasol con AR Augen Matiz-e

SET @sd_part = ''
SELECT @sd_part=partnum FROM TblSpecialDesigns WITH(NOLOCK) WHERE ID=@SpecialDesignID 

---------------------------------------------------------------------
-- Se agrego @Material = shortchar07 [RL - NEW]
---------------------------------------------------------------------

declare @ST as int
declare @AR as int
declare @Type as varchar(5)
declare @HasAR as int

set @ST = 0
set @AR = 0

select @length = datalength(a) from tblText
if @length > 0
begin
	while(@pos > 0 )
	begin
		select @dato = substring(a,0,@pos) from tblText

		if ( @dato <> '')
		begin
			if( @par = 0 )
			begin
				-- Aqui obtengo numero de Parte
				-----------------------------------------------------------------------------------
				set @partnum=@dato
				select @Type = Clase, @HasAR = AR from VwPartTreceux where partnum = @partnum and company = @compania
				if @Type = 'S'
				begin
					set @ST = 1
				end
				if @HasAR = 1
				begin
					set @AR = 1
				end
				set @par = 1
			end
			else if @par = 1
			begin
				-- Aqui obtengo Cantidad 
				-----------------------------------------------------------------------------------
				set @partqty = @dato  
				set @par = 2
			end
			else
			begin
				if (@dato = 0)
				begin
					set @lado = 0		-- Lado = Izquierdo
					set @clase = 0		-- Clase = ST
				end
				if (@dato = 1)
				begin
					set @lado = 1		-- Lado = Derecho
					set @clase = 0		-- Clase = ST
				end
				if (@dato = 2)
				begin
					set @lado = 0		-- Lado = Izquierdo
					set @clase = 1		-- Clase = T
				end
				if (@dato = 3)
				begin
					set @lado = 1		-- Lado = Derecho
					set @clase = 1		-- Clase = T
				end




				--set @lado = cast(@dato as bit)
				-- Obtenemos precio para cada cliente 
				-----------------------------------------------------------------------------------

			          -- Lista de Precios
				--------------------------------------
				declare @orderdate as datetime
				select @orderdate = orderdate from orderhed with(nolock) where ordernum = @OrderNum 
				select @price = 0,@listcode = ''
				select @prodcode = prodcode from part with(nolock) where partnum = @partnum 

				if exists(select * from VwPriceLstParts with(nolock) where custnum = @custnum and partnum = @partnum and @orderdate between startdate and enddate)
				select top 1 @price = baseprice, @listcode = listcode from VwPriceLstParts with(nolock) where custnum = @custnum and partnum = @partnum and @orderdate between startdate and enddate order by seqnum  
				else if exists(select * from VwPriceLstGroups with(nolock) where custnum = @custnum and prodcode = @prodcode and @orderdate between startdate and enddate)
				select top 1 @price = baseprice, @listcode = listcode from VwPriceLstGroups with(nolock) where custnum = @custnum and prodcode = @prodcode and @orderdate between startdate and enddate order by seqnum 
				else if exists(select * from part with(nolock) where partnum = @partnum )
				select top 1 @price = unitprice, @listcode = '' from part with(nolock) where partnum = @partnum 
				else
				select @price = 0, @listcode = ''   
				
				--------------------------------------------------------------------------------------------------------------
				-- Aqui se obtiene el precio del HDRx dependiendo el prodcode y lo almacena en @HDRxPrice
				--------------------------------------------------------------------------------------------------------------
				select distinct @HDRxListcode = listcode from VwPriceLstGroups with(nolock) where @orderdate between startdate and enddate and custnum = @custnum and seqnum = 1
				if exists(select unitprice from tblhdrx with(nolock) where prodcode = @prodcode and listcode = @HDRxListcode)
					select @HDRxPrice = unitprice from tblhdrx with(nolock) where prodcode = @prodcode and listcode = @HDRxListcode
				--------------------------------------------------------------------------------------------------------------
				-- Aqui asigna el precio al detalle del HDRx en caso que exista, si no, no hace nada con el precio obtenido antes
				--------------------------------------------------------------------------------------------------------------
				--if @partnum like 'HDRx'
				--	set @price = @HDRxPrice
				
				if @partnum like 'HDRx'
					set @price = @HDRxPrice

				
				select @cadena = number06 from customer with (nolock) where custnum = @custnum and company = @compania
				select @Optimizado = checkbox20 from orderhed with(nolock) where ordernum=@OrderNum 

				if (@cadena=2)
				begin
					Declare @Xprice as money
					set @Xprice = 0
					set @skusbn = ''
					select @skusbn='SNB' + sku from tblsanbornspricelistcodes with(nolock) where (Prodcode=@prodcode or Prodcode=@partnum) and Hdrx=@Optimizado

					select top 1 @Xprice = baseprice, @listcode = listcode from VwPriceLstParts with(nolock) where custnum = @custnum and partnum = @skusbn and @orderdate between startdate and enddate order by seqnum  
					
					-- Si obtuvimos un precio de la VwPriceLstParts con el SKU de Sanborns usamos ese precio
					if @Xprice>0  
						Set @price=@Xprice
					
				end
		
				--------------------------------------------------------------------------------------------------------------
				--------------------------------------------------------------------------------------------------------------
				-- Aqui se obtiene el precio de terminado en caso que entre en el rango y se haya surtido con semiterminados
				--------------------------------------------------------------------------------------------------------------
				if (len(@partnum) = 10) and (@cadena<>2)
				begin
					declare @seqnum int
					select distinct top 1 @price = a.baseprice, @listcode = b.listcode, @seqnum=b.seqnum
						from VwPriceListByGroup a WITH(NOLOCK) inner join 
							customerpricelst b with(nolock) on a.listcode = b.listcode and a.custnum = b.custnum inner join 
							vwprodcoderangosterminados c with(nolock) on a.prodcode = c.prodcode
					where a.custnum = @custnum and c.partnum = @partnum and c.ordernum = @OrderNum and c.lado = @lado
					order by b.seqnum
				end
				--------------------------------------------------------------------------------------------------------------

				--------------------------------------------------------------------------------------------------------------
				-- Aqui se verifica si el trabajo lleva diseño Free Form distinto a HDRX para poner en ceros el precio de las pastillas
				--------------------------------------------------------------------------------------------------------------
				if (@SpecialDesignID > 0) AND (@sd_part <> 'HDRX') AND (LEN(@partnum) = 10) AND (ISNUMERIC(@partnum) = 1)
				begin
					set @price = 0
				end
				
				SELECT @LISTCODE,@PARTNUM,@PRICE

				set @auxprice = @price


				-- Tabla Part
				----------------------------------------------------------------
				select @descr=partdescription, @ium=ium, @typecode=typecode, @auxprice=unitprice, 
					@pricexcode=pricepercode, @expDiv=expensediv, @expChart=expensechart, @expDept= expensedept, 
					@prodcode=prodcode, @costmethod=costmethod, @salesum=salesum, @sellingfactor=sellingfactor 
				from part with (nolock)  where  partnum=@partnum and company=@company

			--------------------------------------------------------------------------------------------------------------

			/* VERIFICA SI EL CAMPO COSTCOLENTE SE ENCUENTRA EN LA TABLA TBLCADENASPRICELST, LO INDICA QUE ES UN PAQUETE */
			-- Pedro Farías L. Nov 28 2012
			-- Si traemos un paquete en la orden olvida cualquier precio y solo coloca el precio en el código del paquete
			-- los demás van en 0

			if (len(@CostcoLente)>1 AND @CostcoLente<>@partnum)
			begin
				set @price=0
			end
			---------------------------------------------------------------------------------------------------------------

			--------------------------------------------------------------------------------------------------------------
			-- [Ricardo Lozano - 4/21/2010]
			-- Aqui se pone el precio de las dioptrias en 0 si el material es MR-10 (cl_mat = 7)
			--------------------------------------------------------------------------------------------------------------
			if @Material = '7'
			begin
				if @partnum like 'DIOPT'
				begin
					set @price = 0
				end
			end		
			--------------------------------------------------------------------------------------------------------------
						--------------------------------------------------------------------------------------------------------------
			----- Mod para AR Trivex CR Parasol  -- el AR Augen Matiz-e cuesta $ 122.50
			-- Pedro Farías Lozano  Julio 23 2012
			
			
			--------------------------------------------------------------------------------------------------------------
			----- Mod para AR Trivex CR Parasol  -- el AR Augen Matiz-e cuesta $ 122.50
			-- Pedro Farías Lozano  Julio 17 2012
			-- Pedro Farías Lozano  Septiembre 25 2012
			
			if (@cadena=0)
			begin
				if ((@Antireflejante='1' AND @Diseno='1') AND 
													--@typecode='P')	-- Si piden Antireflejante y NO ES UN PAQUETE (los paquetes son typecode=M
					exists(select partnum from VwPartTRECEUX with(nolock) where partnum = @partnum and company = @compania) )
				begin
					if (@Material='8')		SET @price = 0 -- Augen Parasol $ 440 por par
					if (@Material='4')		SET @price = 0 -- CR $ 220 por par
					if (@Material='1')		SET @price = 0 -- Trivex $ 390 por par
				end
				

				if ((@Antireflejante='1') AND @partnum='ARAUG' AND @Diseno='1' AND len(@CostcoLente)<2) -- Es un código de Antireflejante y NO ES UN PAQUETE (CostoLente vacío)
			
				begin
					if (@Material='8')		SET @price = 190 -- Augen Parasol $ 440 por par
					if (@Material='4')		SET @price = 110 -- CR $ 220 por par
					if (@Material='1')		SET @price = 195 -- Trivex $ 390 por par
				end
			end
			---------------------------------------------------------------------------------
			

				if @price is null
				begin
					if @auxprice is null
					begin
						set @price = 0
					end
					else
					begin
						set @price = @auxprice
					end
				end

				if @IsGratis = 1 or @IsGarantia = 1
				begin
					set @price = 0
				end

				/*
				if @auxprice > 0 
				begin
					set @price = @auxprice
				end
				*/

				-- Tabla OrderDtl 
				----------------------------------------------------------------
--				select @maxProg=max(progress_recid) + 1 from orderdtl 
		
				set @descuento = 0
				set @descto = 0
				set @descuento =  ( @price * @partqty ) * ( @descto  /100) 
				
--				set IDENTITY_INSERT  orderdtl on

				INSERT INTO orderdtl
					(openline, company, ordernum, orderline, linetype, partnum, linedesc, ium, revisionnum, 
					commissionable,discountpercent, unitprice, docunitprice, orderqty,requestdate, prodcode, pricepercode, needbydate, custnum, 
					basepartnum,warranty,salesum, sellingfactor, sellingquantity, listprice, doclistprice,ordbasedprice,docordbasedprice, baserevisionnum, 
					discount, docdiscount, checkbox01,checkbox02, shortchar01,
					reference, xpartnum, XREVISIONNUM, ORDERCOMMENT, SHIPCOMMENT, INVOICECOMMENT, PICKLISTCOMMENT, ORIGWHYNOTAX, CONTRACTCODE, WARRANTYCODE, MATERIALMOD, LABORMOD, WARRANTYCOMMENT, MKTGCAMPAIGNID, EXTCOMPANY, LASTCONFIGUSERID, PRICEGROUPCODE,
					taxcatid )
				VALUES
					(1, @company, @OrderNum, @orderline, 'PART', @partnum, @descr, @ium, 'A', 
					1, @descto,@price, @price, @partqty,getdate(),@prodcode, @pricexcode,getdate(), @custnum, 
					@partnum, 1, @salesum, 1, @partqty, @price, @price, @price, @price,@costmethod,
					@descuento, @descuento, @lado,@clase, @OrdenLab,
					'','','','','','','','','','','','','','','','','',
					@TaxCatID)

				if @@error <> 0
				begin
					ROLLBACK TRAN
					select @Msg = 'No se pudo insertar registro en tabla OrderDtl'
--					set IDENTITY_INSERT  orderdtl off
					RETURN
				end
--				set IDENTITY_INSERT  orderdtl off

				-- Tabla Orderrel 
				----------------------------------------------------------------
				--select @maxProg=max(progress_recid) + 1 from orderrel  with (nolock)

				--set IDENTITY_INSERT  orderrel on  
				INSERT INTO orderrel
					(company, ordernum, orderline, orderrelnum, linetype, reqdate, ourreqqty, shipviacode, 
					openrelease, firmrelease, ourstockqty, ourstockshippedqty, warehousecode, partnum, revisionnum, needbydate, plant, 
					sellingreqqty, sellingstockqty, sellingstockshippedqty)
				VALUES
					(@company, @OrderNum, @orderline, 1, 'PART',getdate(), @partqty, 'MULP', 
					1, 1, @partqty, 0, @warehousecode, @partnum, 'A', getdate(), 'Mfgsys', @partqty, @partqty,
					@partqty)
				if @@error <> 0
				begin
					ROLLBACK TRAN
					select @Msg = 'No se pudo insertar registro en la tabla OrderRel'
					--set IDENTITY_INSERT  orderrel off 
					RETURN
				end
				--set IDENTITY_INSERT  orderrel off 

				-- Tabla PartDtl
				--------------------------------------------------------------
				--select @maxProg= max(progress_recid) + 1 from partdtl with (nolock)

				--set IDENTITY_INSERT  partdtl on  
				INSERT INTO partdtl
					(company, type, partnum, duedate, requirementflag, quantity, ordernum, orderline, orderrelnum, 
					partdescription, ium, sourcefile, custnum, stocktrans, firmrelease, revisionnum, plant)
				VALUES     
					(@company, 'Mtl', @partnum, getdate(), 1, @partqty, @OrderNum, @orderline, 1, @descr,
					@ium, 'OR', @custnum, 1, 1, 'A','MfgSys')
				if @@error <> 0
				begin
					ROLLBACK TRAN
					--set IDENTITY_INSERT  partdtl off
					select @Msg = 'No se pudo insertar registro en la tabla PartDtl'
					RETURN
				end
				--set IDENTITY_INSERT  partdtl off

----				-- Tabla Partwhse 
----				---------------------------------------------------------------
----				UPDATE    partwhse
----				SET   allocqty = allocqty + @partqty, salesallocqty = salesallocqty + @partqty
----				WHERE (company = @company) AND (partnum = @partnum) AND (warehousecode = @warehousecode)
----				if @@error <> 0
----				begin
----					ROLLBACK TRAN
----					select @Msg = 'No se pudo actualizar la tabla PartWhse'
----					RETURN
----				end

				set @orderline = @orderline + 1			
				set @par =0
			end
		end /* end @dato */
		updatetext tblText.a @ptr 0 @pos
		select @pos = charindex(',',a,1) from tblText
	end /* end While */
end /* end replace */

if @AR = 0
begin
	--select @AR = shortchar06 from orderhed with(nolock) where ordernum = @OrderNum
	-- Se comenta la línea de arriba porque ya tenemos el valor de shortchart06
	if @AntiReflejante > 0
	begin
		set @AR = 0
	end
	else
	begin
		set @AR = 1
	end
end
if @ST = 0	-- Entra solo si los dos lentes son Terminados
begin
	if @AR = 1
	begin
		update orderhed set date03 = date02, date04 = date02 where ordernum = @OrderNum
		if @@error <> 0
		begin
			ROLLBACK TRAN
			RETURN
		end
	end
	else
	begin
		update orderhed set date03 = date02, date04 = null where ordernum = @OrderNum
		if @@error <> 0
		begin
			ROLLBACK TRAN
			RETURN
		end
	end	
end
--else
--begin		-- Entra cuando los alguno de los dos es semiterminado
/*
	if @AR = 1
	begin
		update orderhed set date04 = date02 where ordernum = @OrderNum
		if @@error <> 0
		begin
			ROLLBACK TRAN
			RETURN
		end		
	end
*/
--	update orderhed set date03 = null,  date04 = null where ordernum = @OrderNum
--	if @@error <> 0
--	begin
--		ROLLBACK TRAN
--		RETURN
--	end		
--end
update orderhed set character03 = cast(@ST as varchar(2)), character04 = cast(@AR as varchar(2)) where ordernum = @OrderNum


select @Msg = 'OK'

commit transaction
/******** Termina StoredProcedure [dbo].[SP_UpdateSalesOrderLines] *************/



