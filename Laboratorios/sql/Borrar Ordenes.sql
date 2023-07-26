select * from customer where name like '%pruebas%'
select * from orderhed where custnum=5703

select partnum,linedesc from orderdtl where ordernum in (260136460,260136461,260136462,260136469,260136474,260136475,260136476,260136481,260136484,260136485)

Declare @order as integer
set @order=360127555
delete from orderdtl where   ordernum in (@order)
delete from comxref  where   key1     in (cast(@order as varchar))
delete from orderrel where   ordernum in (@order)
delete from orderhed where   ordernum in (@order)
delete from ordermsc where  ordernum in (@order)



-- Regresa el consumo de una orden
DECLARE @qty as numeric
SELECT @qty=onhandqty+2 FROM partbin where partnum='2105024786'
update partbin  set [onhandqty] = @qty where partnum='2105024786'
SELECT @qty=onhandqty+2 FROM partwhse where partnum='2105024786'
update partwhse set onhandqty=    @qty where partnum='2105024786'
