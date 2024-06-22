select * from Plato

create proc sp_RegistrarPlato(
@Nombre varchar (100),
@Ingredientes varchar (100),
@Descripcion varchar (100),
@IdCat varchar (100),
@Precio decimal (10,0),
@Activo bit,
@Mensaje varchar (500) output,
@Resultado int output
)
as
begin
	set @Resultado = 0
	if not exists (select * from Plato where Nombreplato = @Nombre)
	begin
		insert into Plato (Nombreplato, Ingredientes,Descripcion,IdCat,Precio,Activo) values 
		(@Nombre,@Ingredientes,@Descripcion,@IdCat,@Precio,@Activo)

		set @Resultado = SCOPE_IDENTITY ()
		end
	else
	set @Mensaje = 'El plato ya existe'
end

create proc sp_EditarPlato(
@IdPlato int,
@Nombre varchar (100),
@Ingredientes varchar (100),
@Descripcion varchar (100),
@IdCat varchar (100),
@Precio decimal (10,0),
@Activo bit,
@Mensaje varchar (500) output,
@Resultado int output
)

as
begin 
	set @Resultado = 0
	if not exists (select * from Plato where Nombreplato = @Nombre and IdPlato != @IdPlato)
	begin

		update Plato set
		Nombreplato = @Nombre,
		Ingredientes = @Ingredientes,
		Descripcion = @Descripcion,
		IdCat = @IdCat,
		Precio = @Precio,
		Activo = @Activo
		where IdPlato = @IdPlato

		set @Resultado = 1
	end
	else
		set @Mensaje = 'El plato ya existe'
end

create proc sp_EliminarPlato(
@IdPlato int, 
@Mensaje varchar (500) output,
@Resultado bit output
)
as
begin
	set @Resultado = 0
	if not exists (select * from DetalleFactura dv
	inner join Plato p on p.IdPlato = dv.IdProducto
	where p.IdPlato = @IdPlato)
	begin
		delete top (1) Plato where IdPlato = @IdPlato
		set @Resultado = 1
	end
	else
	set @Mensaje = 'El producto se encuentra relacionado a una venta'
end

select p.IdPlato, p.Nombreplato, p.Ingredientes, p.Descripcion,
c.IdCategoria, c.Descripcion[DesCategoria],
p.Precio, p.Rutaimagen, p.Nombreimagen, p.Activo
from Plato p
inner join Categoria c on c.IdCategoria = p.IdCat