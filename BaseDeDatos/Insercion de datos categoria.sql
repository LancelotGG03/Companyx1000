
select * from Categoria

create proc sp_RegistrarCategoria(
@Descripcion varchar (100),
@Activo bit,
@Mensaje varchar (500) output,
@Resultado int output
)

as
begin 
	SET @Resultado = 0
	if not exists (select * from Categoria where Descripcion = @Descripcion)
	begin
		insert into Categoria (Descripcion, Activo) values
		(@Descripcion, @Activo)

		set @Resultado = SCOPE_IDENTITY()
		end
		else
		set @Mensaje = 'La categoria ya existe'
end


create proc sp_EditarCategoria(
@IdCategoria int,
@Descripcion varchar (100),
@Activo bit,
@Mensaje varchar (500) output,
@Resultado int output
)

as
begin 
	SET @Resultado = 0
	if not exists (select * from Categoria where Descripcion = @Descripcion and IdCategoria != @IdCategoria)
	begin
		
		update top (1) Categoria set
		Descripcion = @Descripcion,
		Activo = @Activo
		where IdCategoria = @IdCategoria

		set @Resultado = 1
		end
		else
		set @Mensaje = 'La categoria ya existe'
end

create proc sp_EliminarCategoria(
@IdCategoria int,
@Mensaje varchar (500) output,
@Resultado bit output
)

as
begin 
	SET @Resultado = 0
	if not exists (select * from Plato p 
	inner join Categoria c on c.IdCategoria = p.IdCat 
	where p.IdCat = @IdCategoria)
	begin
		
		delete top (1) from Categoria where IdCategoria = @IdCategoria
		
		set @Resultado = 1
		end
		else
		set @Mensaje = 'La categoria se encuentra relacionada a un producto'
end


