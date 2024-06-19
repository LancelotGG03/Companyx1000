

use DatabaseCompanyJLS

GO

create table Categoria(
IdCategoria int primary key identity,
Descripcion varchar (100),
Activo bit default 1,
FechaRegistro datetime default getdate ()
)
go
create table Plato(
IdPlato int primary key identity,
Nombreplato varchar (45),
Ingredientes nvarchar (150),
Descripcion varchar (50),
IdCat int references Categoria (IdCategoria),
Precio decimal (10,0) default 0,
Rutaimagen varchar (100),
Nombreimagen varchar (100),
Activo bit default 1,
FechaRegistro datetime default getdate ()
)
go
create table Cliente (
IdCliente int primary key identity,
Nombre varchar (100),
Apellidos varchar (100),
Correo varchar (100),
Contraseña varchar (150),
Reestablecer bit default 0,
FechaRegistro datetime default getdate ()
)
go
Create table Carrito(
IdCarrito int primary key identity,
IdCliente int references Cliente (IdCliente),
IdProducto int references Plato (IdPlato),
Cantidad int
)
go
create table Factura(
IdVenta int primary key identity,
IdCliente int references Cliente (IdCliente),
Totalproducto int,
ValorTotal decimal (10,0),
Telefono varchar (50),
Direccion varchar (100),
Fechaventa datetime default getdate ()
)
go

create table DetalleFactura(
IdDetalleVenta int primary key identity,
IdVenta int references Factura (IdVenta),
IdProducto int references Plato (IdPlato),
Cantidad int,
Total decimal (10,0)
)
go

Create table Usuario(
IdUsuario int primary key identity,
Nombres varchar (100),
Apellidos varchar (100),
Correo varchar (100),
Contraseña varchar (150),
Reestablecer bit default 1,
Activo bit default 1,
FechaRegistro datetime default getdate ()
)

go

