
select count(*) from Cliente

select isnull (sum (Cantidad),0) from DetalleFactura

select count(*) from Plato

create proc sp_ReporteDashboard
as
begin

select

(select count(*) from Cliente) [TotalCliente],
(select isnull (sum (Cantidad),0) from DetalleFactura) [TotalVenta],
(select count(*) from Plato) [TotalPlato]

end

exec sp_ReporteDashboard

select * from Factura