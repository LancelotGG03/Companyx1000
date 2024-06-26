
create proc sp_RegistrarCliente(
@Nombre varchar (100),
@Apellidos varchar (100),
@Correo varchar (100),
@Contraseņa varchar (100),
@Mensaje varchar (500) output,
@Resultado int output
)
as
begin
	set @Resultado = 0
	if not exists (select * from Cliente where Correo = @Correo)
		begin
			insert into Cliente (Nombre,Apellidos,Correo,Contraseņa,Reestablecer) values
			(@Nombre,@Apellidos,@Correo,@Contraseņa,0)

			set @Resultado = SCOPE_IDENTITY()
			end
			else
			set @Mensaje = 'El correo del usuario ya existe'
end

select * from Cliente