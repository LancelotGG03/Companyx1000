declare @idcategoria int = 1

select distinct c.Descripcion from Plato p
inner join Categoria c on c.IdCategoria = p.IdCat
where c.IdCategoria = iif (@idcategoria = 0, c.IdCategoria,@idcategoria)
