/****** Script for SelectTopNRows command from SSMS  ******/
Declare @ordernum as int
set @ordernum=260115892
set @ordernum=260095594

SELECT [ordernum],[orderline],misccode as partnum,description as linedesc,'EA' as ium,miscamt as unitprice,1 as orderqty
FROM [ordermsc]
where company='TRECEUX' and ordernum=@ordernum

UNION

SELECT [ordernum],[orderline],[partnum],cast([linedesc] as varchar),[ium],[unitprice],[orderqty]
FROM [orderdtl]
where company='TRECEUX' and voidline=0 and ordernum=@ordernum

select * from tbllaboratorios



DECLARE	@return_value int

EXEC	@return_value = [dbo].[spObtieneRxInfo]
		@ordernum = 260115895,
		@labid = 26,
		@vblabid = 0

SELECT	'Return Value' = @return_value

GO
