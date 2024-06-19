
select * from Usuario

create proc sp_RegistrarUsuario(
@Nombres varchar(100),
@Apellidos varchar (100),
@Correo varchar (100),
@Contraseña varchar (100),
@Activo bit,
@Mensaje varchar (500) output,
@Resultado int output
)

as
begin
	Set @Resultado = 0
	if not exists (select * from Usuario where Correo = @Correo)
	begin 
	insert into Usuario (Nombres,Apellidos,Correo,Contraseña,Activo) values
	(@Nombres, @Apellidos, @Correo, @Contraseña, @Activo)

	set @Resultado = SCOPE_IDENTITY()
	end
	else
	set @Mensaje = 'El correo de usuario ya existe'
	end


create proc sp_EditarUsuario(
@IdUsuario int,
@Nombres varchar(100),
@Apellidos varchar (100),
@Correo varchar (100),
@Activo bit,
@Mensaje varchar (500) output,
@Resultado int output
)

as
begin
	Set @Resultado = 0
	if not exists (select * from Usuario where Correo = @Correo and IdUsuario != @IdUsuario)
	begin 
	
	update top (1) Usuario set
	Nombres = @Nombres,
	Apellidos = @Apellidos,
	Correo = @Correo,
	Activo = @Activo
	where IdUsuario = @IdUsuario

	set @Resultado = 1
	end
	else
	set @Mensaje = 'El correo de usuario ya existe'
	end

