IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMFormatoFecha')
	drop function CMFormatoFecha
go

create function dbo.CMFormatoFecha (@fecha datetime) returns varchar(20) as
begin
	declare @salida varchar(30)

	select @salida = format(@fecha, 'yyyy-MM-dd hh:mm', 'en-US')
	return @salida
end
go


----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente
----------------------------------------------------------------------------

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_clientes')
	drop procedure CMGetcot_clientes
go

create proc CMGetcot_clientes
	@id_emp int
AS

select top 5 
	campo_1 = ltrim(rtrim(convert(varchar,c.id))),								-- id del tercero
	campo_2 = ltrim(rtrim(convert(varchar,nit))),								-- nit real
	campo_3 = ltrim(rtrim(convert(varchar,c.codigo))),							-- nit / codigo
	campo_4 = ltrim(rtrim(convert(varchar,digito))),							-- dígito de verificación
	campo_5 = ltrim(rtrim(convert(varchar,t.tipo_identificacion))),				-- viene de la tabla tipo_tributario
	campo_6 = ltrim(rtrim(convert(varchar,id_tipo_tributario))),				-- tipo tributario viene de tabla tipo_tributario
	campo_7 = ltrim(rtrim(convert(varchar, ''))),								-- regimen va en blanco, no exite en advance
	campo_8 = ltrim(rtrim(isnull(o.nombre,razon_social))),						-- nombres
	campo_9 = ltrim(rtrim(convert(varchar,c.tel_1))),							-- teléfono 1
	campo_10 = ltrim(rtrim(convert(varchar,tel_2))),							-- teléfono 2
	campo_11 = ltrim(rtrim(convert(varchar, ''))),								-- Extension, no existe
	campo_12 = ltrim(rtrim(convert(varchar, o.tel_1))),							-- Celular, asumiendo que está asociado al contacto
	campo_13 = ltrim(rtrim(convert(varchar, ''))),								-- Fax, no existe
	campo_14 = ltrim(rtrim(c.direccion)),										-- dirección
	campo_15 = ltrim(rtrim(o.email)),											-- mail
	campo_16 = ltrim(rtrim(convert(varchar, pais.id))),							-- id del pais como codigo
	campo_17 = ltrim(rtrim(pais.descripcion)),									-- Nombre del pais
	campo_18 = ltrim(rtrim(convert(varchar, departamento.id))),					-- id del departamento como código
	campo_19 = ltrim(rtrim(convert(varchar, ciudad.id))),						-- id de la ciudad como código
	campo_20 = ltrim(rtrim(ciudad.descripcion)),								-- nombre de la ciudad
	campo_21 = ltrim(rtrim(convert(varchar, c.id_cot_cliente_barrio))),			-- barrio, tabla cot_cliente_barrio
	campo_22 = ltrim(rtrim(convert(varchar, c.id_cot_cliente_tipo))),			-- id tipo de cliente viene de la tabla cot_cliente_tipo equivale concepto_1
	campo_23 = ltrim(rtrim(convert(varchar, c.id_usuario_vendedor))),			-- Vendedor nuevo, requiere maestro. No hay asociación con tabla de terceros
	campo_24 = ltrim(rtrim(convert(varchar, c.id_usuario_vendedor))),			-- Vendedor usado	
	campo_25 = format(c.fecha_creacion, 'yyyy-MM-dd hh:mm', 'en-US'),			-- Fecha en la que se creo el tercero
	campo_26 = format(c.fecha_modificacion, 'yyyy-MM-dd hh:mm', 'en-US'),		-- Fecha en la que se modifico el tercero
	campo_27 = ltrim(rtrim(convert(varchar,
		case when o.ano_cumple is not null and mes_dia_cumple is not null then
		try_cast(cast(ano_cumple as varchar) + 
			cast(right(mes_dia_cumple,2) as varchar) +
			replicate('0',2-len(left(mes_dia_cumple,len(mes_dia_cumple)-2))) + 
			left(mes_dia_cumple,len(mes_dia_cumple)-2) as date)
		else '' end))),															-- fecha cumpleaños
	campo_28 = ltrim(rtrim(convert(varchar(max),c.notas))),						-- Notas del tercero
	campo_29 = ltrim(rtrim(convert(varchar, 
	case when o.ano_cumple is not null and mes_dia_cumple is not null then	
		DATEDIFF( YY, try_cast(cast(ano_cumple as varchar) + 
			cast(right(mes_dia_cumple,2) as varchar) +
			replicate('0',2-len(left(mes_dia_cumple,len(mes_dia_cumple)-2))) + 
			left(mes_dia_cumple,len(mes_dia_cumple)-2) as date), GETDATE() ) 
	else 0 end))),																-- Edad del tercero  
	campo_30 = ltrim(rtrim(convert(varchar, case 
		when o.sexo =  1 then 'M'
		when o.sexo = 2 then 'F'
		else null end
	))),																		-- Sexo tercero 
	campo_31 = ltrim(rtrim(c.razon_social)),									-- Razon comercial 
	campo_32 = ltrim(rtrim(convert(varchar, ''))),								-- Usuario que creo o modificó, no disponible
	campo_33 = ltrim(rtrim(convert(varchar, cupo_credito))),					-- cupo de crédito
	campo_34 = ltrim(rtrim(convert(varchar, id_cot_estado))),					-- tabla cot_estado
	campo_35 = ltrim(rtrim(convert(varchar, isnull(l.estado,1)))),				-- Indica el estado del tercero para el CRM (bandera)		
	campo_36 = ltrim(rtrim(n.nom1)),											-- nombre 1
	campo_37 = ltrim(rtrim(n.nom2)),											-- nombre 2
	campo_38 = ltrim(rtrim(n.nom3)),											-- nombre 3
	campo_39 = ltrim(rtrim(n.ape1)),											-- nombre 4
	campo_40 = ltrim(rtrim(n.ape2)),											-- nombre 5
	campo_41 = ltrim(rtrim(convert(varchar, c.id_emp))),						-- id empresa	
	campo_42 = ltrim(rtrim(convert(varchar, id_cot_forma_pago))),				-- id forma de pago, se relaciona con la tabla cot_forma_pago
	campo_43 = ltrim(rtrim(convert(varchar, id_cot_zona_sub))),					-- zona ( id de la subzona asociada )
	campo_44 = ltrim(rtrim(convert(varchar, id_cot_cliente_perfil))),			-- perfil del cliente tabla cot_cliente_perfil
	campo_45 = ltrim(rtrim(convert(varchar, id_cot_cliente_contacto))),			-- id del contacto principal
	campo_46 = ltrim(rtrim(convert(varchar, url))),								-- url ó correo del proveedor
	campo_47 = ltrim(rtrim(convert(varchar, id_cot_cliente_actividad))),		-- actividad mercantil del cliente
	campo_48 = ltrim(rtrim(convert(varchar, id_cot_cliente_origen))),			-- origen del cliente
	campo_49 = ltrim(rtrim(convert(varchar, id_tipo_tributario2))),				-- actividad económica
	campo_50 = '',
	campo_51 = '',
	campo_52 = '',
	campo_53 = '',
	campo_54 = '',
	campo_55 = '',
	campo_56 = '',
	campo_57 = '',
	campo_58 = '',
	campo_59 = '',
	campo_60 = ''
 FROM	cot_cliente c
	left join tipo_tributario t on t.id = c.id_tipo_tributario
	left join cot_cliente_contacto o on o.id = c.id_cot_cliente_contacto
	left join cot_cliente_pais as ciudad on ciudad.id = c.id_cot_cliente_pais
	left join cot_cliente_pais as departamento on Departamento.id = ciudad.id_cot_cliente_pais
	left join cot_cliente_pais as Pais on Pais.id = Departamento.id_cot_cliente_pais
	left join cot_cliente_tipo i on i.id = c.id_cot_cliente_tipo
	left join cot_cliente_nom n on n.id_cot_cliente = c.id
	left join cm_logcambios l on l.tabla = 'cot_cliente' and l.idvalor = c.id
 WHERE c.id_emp = @id_emp and isnull(l.estado,0) in (0,2)
 ORDER BY c.id
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynccot_cliente')
	drop procedure CMSynccot_cliente
go

create procedure CMSynccot_cliente
	@id int
as
begin
	if not exists(select id from cm_logcambios where tabla = 'cot_cliente' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('cot_cliente', @id, 99)
	else
		update cm_logcambios
		set estado = 99
		where tabla = 'cot_cliente' and idvalor = @id
end 
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetoperarios')
	DROP PROC CMGetoperarios
go
--
--------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla operarios
----------------------------------------------------------------------------

CREATE PROC CMGetoperarios @id_emp int
AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,u.id))),					-- id del usuario
	campo_2 = ltrim(rtrim(convert(varchar,id_usuario_subgrupo))),	-- grupo al que pertenece el usuario
	campo_3 = ltrim(rtrim(convert(varchar,codigo_usuario))),		-- código del usuario
	campo_4 = ltrim(rtrim(convert(varchar,nombre))),				-- nombre usuario
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = ''
 FROM usuario u
	join usuario_subgrupo s on u.id_usuario_subgrupo = s.id
	join usuario_grupo g on g.id = s.id_usuario_grupo
 WHERE es_operario = 1 and id_emp = @id_emp
	and isnull(anulado,0) = 0
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_items')
	DROP PROC CMGetcot_items
go

----------------------------------------------------------------------------
-- Procedimiento de lectura de registros por sincronizar tabla cot_item
----------------------------------------------------------------------------
CREATE PROC CMGetcot_items @id_emp int, @vehiculos int
AS

select top 5 
	campo_1 = ltrim(rtrim(convert(varchar,i.id_emp))),						-- id de la empresa
	campo_2 = ltrim(rtrim(convert(varchar,i.id))),							-- id del item
	campo_3 = ltrim(rtrim(convert(varchar,codigo))),						-- código del item
	campo_4 = ltrim(rtrim(i.descripcion)),									-- descripcion
	campo_5 = ltrim(rtrim(convert(varchar,precio))),						-- valor unitario
	campo_6 = ltrim(rtrim(convert(varchar,id_cot_grupo_sub))),				-- subgrupo
	campo_7 = ltrim(rtrim(convert(varchar,v.porcentaje))),					-- porcentaje iva
	campo_8 = ltrim(rtrim(convert(varchar,max_dcto))),						-- porcentaje máximo de descuento
	campo_9 = ltrim(rtrim(convert(varchar,anulado))),
	campo_10 = ltrim(rtrim(convert(varchar(max),notas))),
	campo_11 = ltrim(rtrim(convert(varchar,numero_decimales))),
	campo_12 = format(fecha_creacion, 'yyyy-MM-dd hh:mm', 'en-US'),	
	campo_13 = ltrim(rtrim(convert(varchar,promocion))),
	campo_14 = ltrim(rtrim(convert(varchar,mostrar_precio_con_iva))),
	campo_15 = ltrim(rtrim(convert(varchar,maneja_stock))),
	campo_16 = ltrim(rtrim(convert(varchar,id_cot_bodega))),
	campo_17 = ltrim(rtrim(convert(varchar,maneja_lotes))),
	campo_18 = ltrim(rtrim(convert(varchar,id_cot_item_talla))),
	campo_19 = ltrim(rtrim(convert(varchar,id_cot_item_color))),
	campo_20 = ltrim(rtrim(convert(varchar,und))),
	campo_21= ltrim(rtrim(convert(varchar,und_cant))),
	campo_22 = ltrim(rtrim(convert(varchar,id_cot_cliente))),
	campo_23 = ltrim(rtrim(convert(varchar,costo_compra))),
	campo_24 = ltrim(rtrim(convert(varchar,id_cot_item_categoria))),
	campo_25 = ltrim(rtrim(convert(varchar,usar_precio_fijo))),
	campo_26 = ltrim(rtrim(convert(varchar,controlado))),
	campo_27 = ltrim(rtrim(convert(varchar,web))),
	campo_28 = ltrim(rtrim(convert(varchar,factor_costo))),
	campo_29 = ltrim(rtrim(convert(varchar,id_cot_unidades1))),
	campo_30 = ltrim(rtrim(convert(varchar,id_cot_unidades2))),
	campo_31 = ltrim(rtrim(convert(varchar,id_cot_unidades3))),
	campo_32 = ltrim(rtrim(convert(varchar,id_cot_unidades4))),
	campo_33 = ltrim(rtrim(convert(varchar,importado))),
	campo_34 = ltrim(rtrim(convert(varchar,item_produccion))),
	campo_35 = ltrim(rtrim(convert(varchar,id_cot_grupo_sub5))),
	campo_36 = ltrim(rtrim(convert(varchar,precio_und3))),
	campo_37 = ltrim(rtrim(convert(varchar,precio_und4))),
	campo_38 = ltrim(rtrim(convert(varchar,precio_und2))),
	campo_39 = ltrim(rtrim(convert(varchar,id_veh_ano))),					-- Indica el modelo al que pertenece el item cuando es vehículo
	campo_40 = ltrim(rtrim(convert(varchar,calificacion_abc))),				-- si es 'O' es una operación de taller
	campo_41 = ltrim(rtrim(convert(varchar,id_veh_linea))),
	campo_42 = ltrim(rtrim(convert(varchar,id_veh_linea_modelo))),
	campo_43 = '',
	campo_44 = '',
	campo_45 = '',
	campo_46 = '',
	campo_47 = '',
	campo_48 = '',
	campo_49 = '',
	campo_50 = '',
	campo_51 = '',
	campo_52 = '',
	campo_53 = '',
	campo_54 = '',
	campo_55 = '',
	campo_56 = '',
	campo_57 = '',
	campo_58 = '',
	campo_59 = '',
	campo_60 = ''
 FROM	cot_item i
	join cot_grupo_sub s on i.id_cot_grupo_sub = s.id
	left join cot_iva v on i.id_cot_iva = v.id
	left join cot_item_talla t on t.id = i.id_cot_item_talla
	left join cot_item_color c on c.id = i.id_cot_item_color
	left join cm_logcambios l on l.tabla = 'cot_item' and l.idvalor = i.id
 WHERE i.id_emp = @id_emp 
	and ((@vehiculos = 0 and id_veh_ano is null) or (@vehiculos = 1 and id_veh_ano is not null))
	and isnull(l.estado,0) in (0,2)
order by i.id
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynccot_item')
	drop procedure CMSynccot_item
go

create procedure CMSynccot_item
	@id int
as
begin
	if not exists(select id from cm_logcambios where tabla = 'cot_item' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('cot_item', @id, 99)
	else
		update cm_logcambios
		set estado = 99
		where tabla = 'cot_item' and idvalor = @id
end 
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGettipo_tributarios')
	drop procedure CMGettipo_tributarios
go


CREATE PROC CMGettipo_tributarios @id_emp int
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,t.id))),						-- Id del tipo
	campo_2 = ltrim(rtrim(convert(varchar,id_pais))),					-- Id del pais donde aplica
	campo_3 = ltrim(rtrim(descripcion)),								-- descripción del tipo
	campo_4 = ltrim(rtrim(convert(varchar,tipo_identificacion))),		-- Tipo de identificación
	campo_5 = ltrim(rtrim(convert(varchar,usar_nombre_apellido))),		-- indica si se debe usar nombre y apellido en la captura
	campo_6 = ltrim(rtrim(convert(varchar,tipo_doc_dian))),
	campo_7 = ltrim(rtrim(convert(varchar,exigir_digito))),				-- indica si es obligatorio capturar el dígito de verificación
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
FROM	tipo_tributario t
	--join tipo_tributario_emp e on e.id_tipo_tributario = t.id
where id_pais = 2 and tipo_identificacion = 'N' --e.id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_cliente_paises')
	DROP PROC CMGetcot_cliente_paises
go

----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente_pais
----------------------------------------------------------------------------
CREATE PROC CMGetcot_cliente_paises @id_emp int
AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,codigo))),
	campo_4 = ltrim(rtrim(descripcion)),
	campo_5 = ltrim(rtrim(convert(varchar,id_cot_cliente_pais))),		-- relaciona el nivel con el padre anterior pais - dpto - ciudad	
	campo_6 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_cliente_pais
 WHERE id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_cliente_barrios')
	DROP PROC CMGetcot_cliente_barrios
go
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente_barrio
----------------------------------------------------------------------------
CREATE PROC CMGetcot_cliente_barrios @id_emp int
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,b.id))),
	campo_2 = ltrim(rtrim(convert(varchar,id_cot_cliente_ciudad))),		-- id de la ciudad asociada viene de la tabla cot_cliente_pais
	campo_3 = ltrim(rtrim(convert(varchar,b.codigo))),
	campo_4 = ltrim(rtrim(b.descripcion)),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''	
 FROM	cot_cliente_barrio b
	join cot_cliente_pais p on p.id = b.id_cot_cliente_ciudad
 where p.id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_estados')
	DROP PROC CMGetcot_estados
GO

----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_estado
----------------------------------------------------------------------------
CREATE PROC CMGetcot_estados @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = ltrim(rtrim(convert(varchar,idv))),
	campo_5 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_estado
 WHERE id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_grupos')
	DROP PROC CMGetcot_grupos
GO

----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_grupo
----------------------------------------------------------------------------
CREATE PROC CMGetcot_grupos @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(descripcion)),
	campo_4 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_grupo
 WHERE id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_grupo_subs')
	DROP PROC CMGetcot_grupo_subs
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_grupo_sub
----------------------------------------------------------------------------
CREATE PROC CMGetcot_grupo_subs @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_cot_grupo))),			-- grupo al que pertenece el subgrupo
	campo_2 = ltrim(rtrim(convert(varchar,s.id))),					-- id del subgrupo
	campo_3 = ltrim(rtrim(s.descripcion)),							-- subgrupo
	campo_4 = ltrim(rtrim(convert(varchar,s.fecha_modif))),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_grupo_sub s
	join cot_grupo g on g.id = s.id_cot_grupo
 WHERE g.id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_bodegas')
	DROP PROC CMGetcot_bodegas
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_bodega
----------------------------------------------------------------------------
CREATE PROC CMGetcot_bodegas @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(descripcion)),
	campo_4 = ltrim(rtrim(direccion)),
	campo_5 = ltrim(rtrim(convert(varchar,telefonos))),
	campo_6 = ltrim(rtrim(publicidad)),										-- contiene mensaje publicitario asociado a la bodega
	campo_7 = ltrim(rtrim(convert(varchar,anulada))),
	campo_8 = ltrim(rtrim(convert(varchar,tipo_bodega))),
	campo_9 = ltrim(rtrim(convert(varchar,valor_hora))),					-- valor de la hora para taller
	campo_10 = ltrim(rtrim(convert(varchar,id_usuario_jefe))),
	campo_11 = ltrim(rtrim(convert(varchar,id_cot_cliente_pais))),			-- ubicación
	campo_12 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_bodega
 WHERE id_Emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_item_tallas')
	DROP PROC CMGetcot_item_tallas
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_item_talla - puede llevar marca
----------------------------------------------------------------------------
CREATE PROC CMGetcot_item_tallas @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_3 = ltrim(rtrim(convert(varchar,codigo_talla))),
	campo_4 = ltrim(rtrim(descripcion)),
	campo_5 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM cot_item_talla
 WHERE id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_item_categorias')
	DROP PROC CMGetcot_item_categorias
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_item_categoria
----------------------------------------------------------------------------
CREATE PROC CMGetcot_item_categorias @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_3 = ltrim(rtrim(descripcion)),
	campo_4 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_item_categoria
 where id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_unidades')
	DROP PROC CMGetcot_unidades
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_unidades
----------------------------------------------------------------------------
CREATE PROC CMGetcot_unidades @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),							
	campo_2 = ltrim(rtrim(convert(varchar,abreviacion))),
	campo_3 = ltrim(rtrim(descripcion)),
	campo_4 = ltrim(rtrim(convert(varchar,fecha))),
	campo_5 = ltrim(rtrim(des2)),
	campo_6 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_unidades

GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_cliente_contactos')
	DROP PROC CMGetcot_cliente_contactos
GO

CREATE PROC CMGetcot_cliente_contactos @id_emp int 
AS
select top 5 
	campo_1 = ltrim(rtrim(convert(varchar,id_cot_cliente))),				-- id del cliente al que pertenece el contacto
	campo_2 = ltrim(rtrim(convert(varchar,o.id))),							
	campo_3 = ltrim(rtrim(convert(varchar,nombre))),
	campo_4 = ltrim(rtrim(convert(varchar,o.tel_1))),
	campo_5 = ltrim(rtrim(convert(varchar,reemplazo))),						-- nombre del contacto que reemplaza
	campo_6 = ltrim(rtrim(convert(varchar,id_cot_cliente_cargo))),			-- id del cargo, se relaciona con la tabla cot_cliente_cargo
	campo_7 = ltrim(rtrim(convert(varchar,email))),
	campo_8 = ltrim(rtrim(convert(varchar,id_cot_cliente_contacto_jefe))),	-- id del contacto que es jefe de este contacto
	campo_9 = ltrim(rtrim(convert(varchar,id_cot_profesion))),				-- id de la progrsión, viene de la tabla cot_profesion
	campo_10 = ltrim(rtrim(convert(varchar,sexo))),
	campo_11 = ltrim(rtrim(convert(varchar,mes_dia_cumple))),
	campo_12 = ltrim(rtrim(convert(varchar,estado_civil))),
	campo_13 = ltrim(rtrim(convert(varchar,cantidad_hijos))),
	campo_14 = ltrim(rtrim(convert(varchar,anulado))),
	campo_15 = ltrim(rtrim(convert(varchar,o.notas))),
	campo_16 = ltrim(rtrim(convert(varchar,o.direccion))),
	campo_17 = ltrim(rtrim(convert(varchar,cedula))),
	campo_18 = ltrim(rtrim(convert(varchar,numero_cuenta))),				-- número de cuenta bancaria
	campo_19 = ltrim(rtrim(convert(varchar,tipo_cuenta))),
	campo_20 = ltrim(rtrim(convert(varchar,id_tes_bancos))),				-- id del banco al que pertenece la cuenta
	campo_21 = ltrim(rtrim(convert(varchar,o.id_cot_cliente_pais))),
	campo_22 = ltrim(rtrim(convert(varchar,estrato))),
	campo_23 = ltrim(rtrim(convert(varchar,ano_cumple))),
	campo_24 = dbo.CMFormatoFecha(fecha_modif),
	campo_25 = ltrim(rtrim(convert(varchar,unsuscribe))),					-- indica si el cliente se ha retirado de los servicios de envío de correo
	campo_26 = '',
	campo_27 = '',
	campo_28 = '',
	campo_29 = '',
	campo_30 = '',
	campo_31 = '',
	campo_32 = '',
	campo_33 = '',
	campo_34 = '',
	campo_35 = '',
	campo_36 = '',
	campo_37 = '',
	campo_38 = '',
	campo_39 = '',
	campo_40 = '',
	campo_41 = '',
	campo_42 = '',
	campo_43 = '',
	campo_44 = '',
	campo_45 = '',
	campo_46 = '',
	campo_47 = '',
	campo_48 = '',
	campo_49 = '',
	campo_50 = '',
	campo_51 = '',
	campo_52 = '',
	campo_53 = '',
	campo_54 = '',
	campo_55 = '',
	campo_56 = '',
	campo_57 = '',
	campo_58 = '',
	campo_59 = '',
	campo_60 = ''
 FROM	cot_cliente_contacto o
	join cot_cliente c on o.id_cot_cliente = c.id
	left join cm_logcambios l on l.tabla = 'cot_cliente_contacto' and l.idvalor = o.id
 WHERE c.id_emp = @id_emp and isnull(l.estado,0) in (0,2)
 ORDER BY o.id
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynccot_contacto')
	DROP PROC CMSynccot_contacto
GO

create procedure CMSynccot_contacto
	@id int
as
begin
	if not exists(select id from cm_logcambios where tabla = 'cot_cliente_contacto' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('cot_cliente_contacto', @id, 99)
	else
		update cm_logcambios
		set estado = 99
		where tabla = 'cot_cliente_contacto' and idvalor = @id
end 
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGettes_bancos')
	DROP PROC CMGettes_bancos
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla tes_bancos
----------------------------------------------------------------------------
CREATE PROC CMGettes_bancos @id_emp int 
AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,cuenta_bancaria))),
	campo_3 = ltrim(rtrim(convert(varchar,codigo))),
	campo_4 = ltrim(rtrim(convert(varchar,nombre))),
	campo_5 = ltrim(rtrim(convert(varchar,direccion))),
	campo_6 = ltrim(rtrim(convert(varchar,telefono))),
	campo_7 = ltrim(rtrim(convert(varchar,contacto))),
	campo_8 = ltrim(rtrim(convert(varchar,notas))),
	campo_9 = '',	
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''	
 FROM	tes_bancos
 where id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_profesiones')
	DROP PROC CMGetcot_profesiones
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_profesion
----------------------------------------------------------------------------
CREATE PROC CMGetcot_profesiones @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = ltrim(rtrim(convert(varchar,idv))),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',	
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''	
 FROM	cot_profesion
 where id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_cliente_cargos')
	DROP PROC CMGetcot_cliente_cargos
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente_cargo
----------------------------------------------------------------------------
CREATE PROC CMGetcot_cliente_cargos @id_emp int 
AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = '',
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_cliente_cargo
 where id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_cliente_actividades')
	DROP PROC CMGetcot_cliente_actividades
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente_actividad
----------------------------------------------------------------------------
CREATE PROC CMGetcot_cliente_actividades @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = dbo.CMFormatoFecha(fecha_modif),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_cliente_actividad
  where id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_formas_pago')
	DROP PROC CMGetcot_formas_pago
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_forma_pago
----------------------------------------------------------------------------
CREATE PROC CMGetcot_formas_pago @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = ltrim(rtrim(convert(varchar,explicacion))),
	campo_5 = ltrim(rtrim(convert(varchar,dias_credito))),
	campo_6 = ltrim(rtrim(convert(varchar,tipo))),
	campo_7 = ltrim(rtrim(convert(varchar,dias_gracia))),
	campo_8 = dbo.CMFormatoFecha(fecha_modif),
	campo_9 = ltrim(rtrim(convert(varchar,descuento_pie))),
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_forma_pago
 where id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_cliente_tipos')
	DROP PROC CMGetcot_cliente_tipos
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente_tipo
----------------------------------------------------------------------------
CREATE PROC CMGetcot_cliente_tipos @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = dbo.CMFormatoFecha(fecha_modif),	
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_cliente_tipo
  where id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_cliente_perfiles')
	DROP PROC CMGetcot_cliente_perfiles
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente_perfil
----------------------------------------------------------------------------
CREATE PROC CMGetcot_cliente_perfiles @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = dbo.CMFormatoFecha(fecha_modif),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''	
 FROM	cot_cliente_perfil
 where id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_grupo_subgrupos3')
	DROP PROC CMGetcot_grupo_subgrupos3
GO

----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_grupo_sub3
----------------------------------------------------------------------------
CREATE PROC CMGetcot_grupo_subgrupos3 @id_emp int 
AS

select id_emp,
	campo_1 = ltrim(rtrim(convert(varchar,id_cot_grupo_sub))),				-- id del subgrupo padre
	campo_2 = ltrim(rtrim(convert(varchar,s3.id))),
	campo_3 = ltrim(rtrim(convert(varchar,s3.descripcion))),
	campo_4 = dbo.CMFormatoFecha(s3.fecha_modif),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_grupo_sub3 s3
	join cot_grupo_sub s on s.id = s3.id_cot_grupo_sub
	join cot_grupo g on g.id = s.id_cot_grupo
WHERE g.id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_grupo_subgrupos4')
	DROP PROC CMGetcot_grupo_subgrupos4
GO

----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_grupo_sub3
----------------------------------------------------------------------------
CREATE PROC CMGetcot_grupo_subgrupos4 @id_emp int 
AS

select id_emp,
	campo_1 = ltrim(rtrim(convert(varchar,id_cot_grupo_sub3))),				-- id del subgrupo padre
	campo_2 = ltrim(rtrim(convert(varchar,s4.id))),
	campo_3 = ltrim(rtrim(convert(varchar,s4.descripcion))),
	campo_4 = dbo.CMFormatoFecha(s4.fecha_modif),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_grupo_sub4 s4
	join cot_grupo_sub3 s3 on s4.id_cot_grupo_sub3 = s3.id
	join cot_grupo_sub s on s.id = s3.id_cot_grupo_sub
	join cot_grupo g on g.id = s.id_cot_grupo
WHERE g.id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_grupo_subgrupos5')
	DROP PROC CMGetcot_grupo_subgrupos5
GO

----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_grupo_sub3
----------------------------------------------------------------------------
CREATE PROC CMGetcot_grupo_subgrupos5 @id_emp int 
AS

select id_emp,
	campo_1 = ltrim(rtrim(convert(varchar,s5.id_cot_grupo_sub4))),				-- id del subgrupo padre
	campo_2 = ltrim(rtrim(convert(varchar,s5.id))),
	campo_3 = ltrim(rtrim(convert(varchar,s5.descripcion))),
	campo_4 = dbo.CMFormatoFecha(s5.fecha_modif),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_grupo_sub5 s5
	join cot_grupo_sub4 s4 on s5.id_cot_grupo_sub4 = s4.id
	join cot_grupo_sub3 s3 on s4.id_cot_grupo_sub3 = s3.id
	join cot_grupo_sub s on s.id = s3.id_cot_grupo_sub
	join cot_grupo g on g.id = s.id_cot_grupo
WHERE g.id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetveh_marcas')
	DROP PROC CMGetveh_marcas
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla veh_marca
----------------------------------------------------------------------------
CREATE PROC CMGetveh_marcas @id_emp int 
AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_3 = dbo.CMFormatoFecha(fecha_modif),
	campo_4 = '',
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	veh_marca
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetveh_lineas')
	DROP PROC CMGetveh_lineas
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla veh_linea
----------------------------------------------------------------------------
CREATE PROC CMGetveh_lineas @id_emp int 
AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,id_veh_marca))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = dbo.CMFormatoFecha(fecha_creacion),
	campo_5 = ltrim(rtrim(convert(varchar,clase))),							-- Clase de vehículo, viene de la tabla veh_clase (automovil, campero.. )
	campo_6 = dbo.CMFormatoFecha(fecha_modif),
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	veh_linea
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetveh_clases')
	DROP PROC CMGetveh_clases
GO

----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla veh_clase
----------------------------------------------------------------------------
CREATE PROC CMGetveh_clases @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_3 = '',
	campo_4 = '',
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	veh_clase
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetveh_servicios')
	DROP PROC CMGetveh_servicios
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla veh_servicio
----------------------------------------------------------------------------
CREATE PROC CMGetveh_servicios @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_3 = '',
	campo_4 = '',
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	veh_servicio
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetveh_linea_modelos')
	DROP PROC CMGetveh_linea_modelos
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla veh_linea_modelo
----------------------------------------------------------------------------
CREATE PROC CMGetveh_linea_modelos @id_emp int 
AS

select top 5 
	campo_1 = ltrim(rtrim(convert(varchar,v.id))),
	campo_2 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_3 = dbo.CMFormatoFecha(fecha_creacion),
	campo_4 = ltrim(rtrim(convert(varchar,precio))),
	campo_5 = ltrim(rtrim(convert(varchar,moneda_precio))),
	campo_6 = ltrim(rtrim(convert(varchar,tipo))),
	campo_7 = ltrim(rtrim(convert(varchar,motor))),
	campo_8 = ltrim(rtrim(convert(varchar,cilindrada))),
	campo_9 = ltrim(rtrim(convert(varchar,potencia))),
	campo_10 = ltrim(rtrim(convert(varchar,velocidad_max))),
	campo_11 = ltrim(rtrim(convert(varchar,aceleracion))),
	campo_12 = ltrim(rtrim(convert(varchar,valvulas))),
	campo_13 = ltrim(rtrim(convert(varchar,caja))),
	campo_14 = ltrim(rtrim(convert(varchar,frenos))),
	campo_15 = ltrim(rtrim(convert(varchar,control_frenos))),
	campo_16 = ltrim(rtrim(convert(varchar,largo))),
	campo_17 = ltrim(rtrim(convert(varchar,ancho))),
	campo_18 = ltrim(rtrim(convert(varchar,baul))),
	campo_19 = ltrim(rtrim(convert(varchar,peso))),
	campo_20 = ltrim(rtrim(convert(varchar,aire_acondicionado))),
	campo_21 = ltrim(rtrim(convert(varchar,direccion_hidraulica))),
	campo_22 = ltrim(rtrim(convert(varchar,vidrios_electricos))),
	campo_23 = ltrim(rtrim(convert(varchar,rines))),
	campo_24 = ltrim(rtrim(convert(varchar,espejos))),
	campo_25 = ltrim(rtrim(convert(varchar,airbags))),
	campo_26 = ltrim(rtrim(convert(varchar,cd))),
	campo_27 = ltrim(rtrim(convert(varchar,mp3))),
	campo_28 = ltrim(rtrim(convert(varchar,dvd))),
	campo_29 = ltrim(rtrim(convert(varchar,camara))),
	campo_30 = ltrim(rtrim(convert(varchar,sensor_parqueo))),
	campo_31 = ltrim(rtrim(convert(varchar,tv))),
	campo_32 = ltrim(rtrim(convert(varchar,trasmision))),
	campo_33 = ltrim(rtrim(convert(varchar,combustible))),
	campo_34 = ltrim(rtrim(convert(varchar,gps))),
	campo_35 = ltrim(rtrim(convert(varchar,id_veh_linea))),					-- Línea a la que pertenece el modelo tabla veh_linea
	campo_36 = ltrim(rtrim(convert(varchar,clase))),						-- no está en uso a 20170321				
	campo_37 = dbo.CMFormatoFecha(fecha_actualizacion),
	campo_38 = dbo.CMFormatoFecha(fecha_modif),
	campo_39 = '',
	campo_40 = '',
	campo_41 = '',
	campo_42 = '',
	campo_43 = '',
	campo_44 = '',
	campo_45 = '',
	campo_46 = '',
	campo_47 = '',
	campo_48 = '',
	campo_49 = '',
	campo_50 = '',
	campo_51 = '',
	campo_52 = '',
	campo_53 = '',
	campo_54 = '',
	campo_55 = '',
	campo_56 = '',
	campo_57 = '',
	campo_58 = '',
	campo_59 = '',
	campo_60 = ''
 FROM	veh_linea_modelo v
	left join cm_logcambios l on l.tabla = 'veh_linea_modelo' and l.idvalor = v.id
 WHERE isnull(l.estado,0) in (0,2)
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSyncveh_linea_modelo')
	DROP PROC CMSyncveh_linea_modelo
GO

create procedure CMSyncveh_linea_modelo
	@id int
as
begin
	if not exists(select id from cm_logcambios where tabla = 'veh_linea_modelo' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('veh_linea_modelo', @id, 99)
	else
		update cm_logcambios
		set estado = 99
		where tabla = 'veh_linea_modelo' and idvalor = @id
end 
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_item_colores')
	DROP PROC CMGetcot_item_colores
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_item_color
----------------------------------------------------------------------------
CREATE PROC CMGetcot_item_colores @id_emp int 
AS
select top 5 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_3 = ltrim(rtrim(convert(varchar,codigo_color))),
	campo_4 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_5 = ltrim(rtrim(convert(varchar,idv))),
	campo_6 = dbo.CMFormatoFecha(fecha_modif),
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_item_color
 WHERE id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_zonas')
	DROP PROC CMGetcot_zonas
GO

----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_zona
----------------------------------------------------------------------------
CREATE PROC CMGetcot_zonas @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = dbo.CMFormatoFecha(fecha_modif),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_zona
 where id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_zona_subs')
	DROP PROC CMGetcot_zona_subs
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_zona_sub
----------------------------------------------------------------------------
CREATE PROC CMGetcot_zona_subs @id_emp int 
AS
select
	campo_1 = ltrim(rtrim(convert(varchar,id_cot_zona))),
	campo_2 = ltrim(rtrim(convert(varchar,s.id))),
	campo_3 = ltrim(rtrim(convert(varchar,s.descripcion))),
	campo_4 = dbo.CMFormatoFecha(s.fecha_modif),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_zona_sub s
	join cot_zona z on z.id = s.id_cot_zona
 where id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynccot_items')
	DROP PROC CMSynccot_items
GO
----------------------------------------------------------------------------
-- Procedimiento para marcar un item como sincronizado
----------------------------------------------------------------------------

create procedure CMSynccot_items
	@id int
as
begin
	if not exists(select id from cm_logcambios where tabla = 'cot_item' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('cot_item', @id, 99)
	else
		update cm_logcambios
		set estado = 99
		where tabla = 'cot_item' and idvalor = @id
end 
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_item_lotes')
	DROP PROC CMGetcot_item_lotes
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_item_lote
----------------------------------------------------------------------------
CREATE PROC CMGetcot_item_lotes @id_emp int 
AS

select top 5 
	campo_1 = ltrim(rtrim(convert(varchar,o.id))),							-- identificadór único del lote (vehículo ó maquinaria)
	campo_2 = ltrim(rtrim(convert(varchar,id_cot_item))),					-- id del modelo_año al que pertenece
	campo_3 = ltrim(rtrim(convert(varchar,lote))),							-- Serial
	campo_4 = dbo.CMFormatoFecha(o.fecha_creacion),
	campo_5 = dbo.CMFormatoFecha(o.fecha_vencimiento),
	campo_6 = ltrim(rtrim(convert(varchar,o.notas))),
	campo_7 =	'',
	campo_8 = dbo.CMFormatoFecha(fecha_modif),
	campo_9 = ltrim(rtrim(convert(varchar,vin))),
	campo_10 = ltrim(rtrim(convert(varchar,o.motor))),
	campo_11 = ltrim(rtrim(convert(varchar,placa))),
	campo_12 = ltrim(rtrim(convert(varchar,o.chasis))),
	campo_13 = ltrim(rtrim(convert(varchar,o.km))),
	campo_14 = ltrim(rtrim(convert(varchar,id_veh_color_int))),				-- id del color interno
	campo_15 = ltrim(rtrim(convert(varchar,o.id_veh_color))),				-- id del color externo
	campo_16 = ltrim(rtrim(convert(varchar,o.id_cot_cliente_contacto))),	-- id del contacto que tiene asociado el vh
	campo_17 = ltrim(rtrim(convert(varchar,o.licencia_transito))),
	campo_18 = ltrim(rtrim(convert(varchar,o.seguro_obligatorio))),
	campo_19 = ltrim(rtrim(convert(varchar,id_cot_cliente_pais))),
	campo_20 = ltrim(rtrim(convert(varchar,id_cot_cliente_aseguradora))),	-- id de la aseguradora
	campo_21 = '',
	campo_22 = '',
	campo_23 = '',
	campo_24 = '',
	campo_25 = '',
	campo_26 = '',
	campo_27 = '',
	campo_28 = '',
	campo_29 = '',
	campo_30 = '',
	campo_31 = '',
	campo_32 = '',
	campo_33 = '',
	campo_34 = '',
	campo_35 = '',
	campo_36 = '',
	campo_37 = '',
	campo_38 = '',
	campo_39 = '',
	campo_40 = '',
	campo_41 = '',
	campo_42 = '',
	campo_43 = '',
	campo_44 = '',
	campo_45 = '',
	campo_46 = '',
	campo_47 = '',
	campo_48 = '',
	campo_49 = '',
	campo_50 = '',
	campo_51 = '',
	campo_52 = '',
	campo_53 = '',
	campo_54 = '',
	campo_55 = '',
	campo_56 = '',
	campo_57 = '',
	campo_58 = '',
	campo_59 = '',
	campo_60 = ''
 FROM cot_item_lote o
	join cot_item i on i.id = o.id_cot_item
	left join cm_logcambios l on l.tabla = 'cot_item_lote' and l.idvalor = o.id
 WHERE i.id_emp = @id_emp 
	and id_veh_ano is not null
	and isnull(l.estado,0) in (0,2)
order by i.id

GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynccot_item_lote')
	DROP PROC CMSynccot_item_lote
GO

create procedure CMSynccot_item_lote
	@id int
as
begin
	if not exists(select id from cm_logcambios where tabla = 'cot_item_lote' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('cot_item_lote', @id, 99)
	else
		update cm_logcambios
		set estado = 99
		where tabla = 'cot_item_lote' and idvalor = @id
end 
go


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGettal_motivo_ingresos')
	DROP PROC CMGettal_motivo_ingresos
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla tal_motivo_ingreso
----------------------------------------------------------------------------
CREATE PROC CMGettal_motivo_ingresos @id_emp int 

AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = ltrim(rtrim(convert(varchar,anulado))),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	tal_motivo_ingreso
 WHERE id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetstock')
	DROP PROC CMGetstock
GO

create procedure dbo.CMGetstock @IdItem int, @IdBodega int 
as
	declare @manejalotes int 
	declare @esvehiculos int = 0
	
	select @manejalotes = isnull(maneja_lotes,0),
		@esvehiculos = case when id_veh_ano is not null then 1 else 0 end
	from cot_item 
	where id = @IdItem

	exec GetCotItemStockSimple4	@IdBodega, @IdItem, @manejalotes, 0, 0, 1, 0, 0, 0, 0, -1, @esvehiculos, 0
		
go


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGettal_planes')
	DROP PROC CMGettal_planes
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla tal_planes
----------------------------------------------------------------------------
CREATE PROC CMGettal_planes @id_emp int 

AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''	
 FROM	tal_planes
WHERE id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGettal_campañas')
	DROP PROC CMGettal_campañas
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla tal_camp_enc
----------------------------------------------------------------------------
CREATE PROC CMGettal_campañas @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,o.id))),
	campo_2 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_3 = ltrim(rtrim(convert(varchar,titulo))),						-- nombre de la campaña
	campo_4 = ltrim(rtrim(convert(varchar,fecha_ini))),						-- inicio vigencia
	campo_5 = ltrim(rtrim(convert(varchar,fecha_fin))),						-- fin vigencia
	campo_6 = ltrim(rtrim(convert(varchar,vin_ini))),
	campo_7 = ltrim(rtrim(convert(varchar,vin_fin))),
	campo_8 = ltrim(rtrim(convert(varchar,motor_ini))),
	campo_9 = ltrim(rtrim(convert(varchar,motor_fin))),
	campo_10 = ltrim(rtrim(convert(varchar,fecha_modif))),					-- ultimo cambio
	campo_11 = ltrim(rtrim(convert(varchar,anulada))),						-- inidica di está anulada
	campo_12 = ltrim(rtrim(convert(varchar,id_veh_linea))),					-- línea a la que aplica Ej Audi A4 , blanco para todos
	campo_13 = ltrim(rtrim(convert(varchar,id_veh_linea_modelo))),			-- linea modelo a la que aaplica ej A4 1.8 TP , blanco para todos
	campo_14 = '',
	campo_15 = ''	
 FROM	tal_camp_enc o
		left join cm_logcambios l on l.tabla = 'tal_camp_enc' and l.idvalor = o.id
 WHERE o.id_emp = @id_emp 
	and fecha_fin >= cast(getdate() as date)	
	and isnull(l.estado,0) in (0,2) 
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynctal_camp_enc')
	DROP PROC CMSynctal_camp_enc
GO

create procedure CMSynctal_camp_enc
	@id int
as
begin
	if not exists(select id from cm_logcambios where tabla = 'tal_camp_enc' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('tal_camp_enc', @id, 99)
	else
		update cm_logcambios
		set estado = 99
		where tabla = 'tal_camp_enc' and idvalor = @id
end 
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_estadistica_cliente')
	DROP PROC CMGet_estadistica_cliente
GO


CREATE procedure [dbo].[CMGet_estadistica_cliente]
(
	@id_cliente int,
	@fecinf DATETIME,
	@fecsup DATETIME
)
As
	IF @fecsup <> ''
		SET @fecsup=dbo.FormatoFechaSinHoraPlana(@fecsup) + ' 23:59:59'
	
	select	
			tipo=t.descripcion,
			numero = ltrim(rtrim(convert(varchar,c.numero_cotizacion))),
			id = ltrim(rtrim(convert(varchar,c.id))),
			fecha= dbo.CMFormatoFecha(isnull(c.fecha_cartera,c.fecha)),
			total_iva = ltrim(rtrim(convert(varchar,c.total_iva))),
			total = ltrim(rtrim(convert(varchar,c.total_total))),
			valor_aplicado = ltrim(rtrim(convert(varchar,isnull(s.valor_aplicado,0)))),
			saldo = ltrim(rtrim(convert(varchar,c.total_total-IsNull(s.valor_aplicado,0)))),		
			vencimiento=dbo.CMFormatoFecha(c.fecha_estimada),
			dias_vencido=ltrim(rtrim(convert(varchar,datediff(d,c.fecha_estimada,getdate())))),
			forma_pago=f.descripcion,
			bodega=b.descripcion,			
			sw = ltrim(rtrim(convert(varchar,t.sw))),							-- indica la naturaleza del documento. 1 factura (ver módulo en advance)
			notas = c.notas		
	from cot_cotizacion c 
		join cot_cliente cli2 on cli2.id=c.id_cot_cliente
		left join cot_forma_pago f on f.id=c.id_cot_forma_pago
		left join v_cot_cotizacion_cuotas_cuantas cu on cu.id_cot_cotizacion=c.id
		Join cot_bodega b on b.id=c.id_cot_bodega
		Join usuario u on u.id=c.id_usuario_vende
		Join cot_tipo t on t.id=c.id_cot_tipo
		Join v_cot_factura_saldo s on s.id_cot_cotizacion=c.id	
	where sw is not null  
		and sw not in (46,60)
		and c.id_cot_cliente = @id_cliente 
		and c.fecha between @fecinf and @fecsup
	union all
	select	
			tipo=t.descripcion,
			numero=ltrim(rtrim(convert(varchar,n.numero))),
			ltrim(rtrim(convert(varchar,n.id))),
			fecha=dbo.CMFormatoFecha(n.fecha),
			ltrim(rtrim(convert(varchar,n.total_iva))),
			ltrim(rtrim(convert(varchar,n.total_total))),
			valor_aplicado = ltrim(rtrim(convert(varchar,isnull(s.valor_aplicado,0)))),
			saldo= ltrim(rtrim(convert(varchar,n.total_total-IsNull(s.valor_aplicado,0)))),			
			vencimiento=null,
			dias_vencido=null,
			forma_pago=f.descripcion,
			bodega=b.descripcion,		
			ltrim(rtrim(convert(varchar,t.sw))),				
			n.notas					
	from cot_notas_deb_cre n 
		join cot_cliente cli2 on cli2.id=n.id_cot_cliente
		left join cot_forma_pago f on f.id=n.id_cot_forma_pago
		Join cot_bodega b on b.id=n.id_cot_bodega
		Join usuario u on u.id=n.id_usuario_vende
		Join cot_tipo t on t.id=n.id_cot_tipo
		left Join v_tes_nota_saldo s on s.id_cot_notas_deb_cre=n.id
	where n.id_cot_cliente = @id_cliente and n.fecha between @fecinf and @fecsup
	order by fecha

go


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_tal_citas_fecha')
	DROP PROC CMGet_tal_citas_fecha
GO


CREATE PROCEDURE CMGet_tal_citas_fecha
(
@emp int,
@bod INT,
@fecha DATETIME
)
AS
	DECLARE @ocu15 TINYINT=dbo.ReglaDeNegocio(@emp,174,'minuto15',0)
	DECLARE @ocu30 TINYINT=dbo.ReglaDeNegocio(@emp,174,'minuto30',0)
	DECLARE @ocu45 TINYINT=dbo.ReglaDeNegocio(@emp,174,'minuto45',0)

	DECLARE @hora_ini TINYINT=dbo.ReglaDeNegocio(@emp,174,'hora_ini'+CAST(@bod AS VARCHAR),8)
	DECLARE @min_ini TINYINT=dbo.ReglaDeNegocio(@emp,174,'min_ini'+CAST(@bod AS VARCHAR),0)

	DECLARE @hora_fin TINYINT=dbo.ReglaDeNegocio(@emp,174,'hora_fin'+CAST(@bod AS VARCHAR),16)
	DECLARE @min_fin TINYINT=dbo.ReglaDeNegocio(@emp,174,'min_fin'+CAST(@bod AS VARCHAR),0)

	--0) citas del dia
	SELECT	id = ltrim(rtrim(convert(varchar,c.id))),
			H=ltrim(rtrim(convert(varchar,t.hora))),
			M= ltrim(rtrim(convert(varchar,CASE WHEN t.minutos=0 THEN NULL ELSE t.minutos end))),
			l.placa,
			i.descripcion,
			c.responsable,
			c.telefono,
			c.notas,
			ltrim(rtrim(convert(varchar,c.id_cot_cotizacion))),				-- número de la OT asociada a la cita
			ltrim(rtrim(convert(varchar,c.id_tal_planes))),					-- plan de mtto programado, puede ir en blanco
			ltrim(rtrim(convert(varchar,c.id_tal_camp_enc)))				-- campaña asociada a la cita, no puede ser null
	FROM tal_citas_horas t
	LEFT JOIN tal_citas c ON c.id_cot_bodega=@bod
							 AND year(c.fecha_cita)=YEAR(@fecha)
							 AND MONTH(c.fecha_cita)=MONTH(@fecha)
							 AND DAY(c.fecha_cita)=DAY(@fecha)
							 AND DATEPART(HOUR,c.fecha_cita)=t.hora
							 AND DATEPART(MINUTE,c.fecha_cita)=t.minutos
	LEFT JOIN cot_item_lote l ON l.id=c.id_cot_item_lote
	LEFT JOIN v_cot_item_descripcion i ON i.id=l.id_cot_item
	WHERE t.hora>=@hora_ini AND (t.hora<>@hora_ini OR t.minutos>=@min_ini)
	      AND t.hora<=@hora_fin AND (t.hora<>@hora_fin OR t.minutos<=@min_fin)
		  AND (@ocu15=0 OR t.minutos<>15)
		  AND (@ocu30=0 OR t.minutos<>30)
		  AND (@ocu45=0 OR t.minutos<>45)
	ORDER BY t.hora,t.minutos

	--1) ver dias con citas
	--SELECT DISTINCT fecha=CAST(dbo.FormatoFechaSinHoraPlana(c.fecha_cita) AS DATE)
	--FROM tal_citas c
	--WHERE c.id_cot_bodega=@Bod
	--	  AND YEAR(c.fecha_cita)=YEAR(@fecha)
	--	  AND MONTH(c.fecha_cita)=MONTH(@fecha)

go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_item_listas')
	DROP PROC CMGetcot_item_listas
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_item_listas
----------------------------------------------------------------------------
CREATE PROC CMGetcot_item_listas @id_emp int 

AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_3 = ltrim(rtrim(convert(varchar,precio_nro))),
	campo_4 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_5 = ltrim(rtrim(convert(varchar,idv))),
	campo_6 = ltrim(rtrim(convert(varchar,fecha_modif))),
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_item_listas
 WHERE id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_item_prereq')
	DROP PROC CMGetcot_item_prereq
GO


CREATE PROC CMGetcot_item_prereq @id_emp int 

AS

select top 5
	campo_1 = ltrim(rtrim(convert(varchar,id_cot_item))),
	campo_2 = ltrim(rtrim(convert(varchar,id_cot_item_prereq))),
	campo_3 = ltrim(rtrim(convert(varchar,cantidad_obligada))),
	campo_4 = ltrim(rtrim(convert(varchar,id_tal_planes))),
	campo_5 = ltrim(rtrim(convert(varchar,id_crm))),					-- creado para control
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_item_prereq p
	join cot_item i on i.id = p.id_cot_item
	left join cm_logcambios l on l.tabla = 'cot_item_prereq' and l.idvalor = p.id_crm
 WHERE id_emp = @id_emp and isnull(l.estado,0) in (0,2)
 ORDER BY p.id_crm
GO

----

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynccot_item_prereq')
	drop procedure CMSynccot_item_prereq
go

create procedure CMSynccot_item_prereq
	@id int
as
begin
	if not exists(select id from cm_logcambios where tabla = 'cot_item_prereq' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('cot_item_prereq', @id, 99)
	else
		update cm_logcambios
		set estado = 99
		where tabla = 'cot_item_prereq' and idvalor = @id
end 
go


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_Stock')
	drop procedure CMGet_Stock
go

create procedure CMGet_Stock @id_emp int  as
select 
	Campo_1 = ltrim(rtrim(convert(varchar,s.id_cot_bodega))), 
	Campo_2 = ltrim(rtrim(convert(varchar,id_cot_item))), 
	Campo_3 = ltrim(rtrim(convert(varchar,id_cot_item_lote))), 
	Campo_4 = ltrim(rtrim(convert(varchar,stock))),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
from v_cot_item_stock_real s
	join cot_item i on i.id = s.id_cot_item
where id_Emp = @id_emp 
order by id_cot_item
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMPut_contacto')
	DROP PROC CMPut_contacto
GO


----------------------------------------------------------------------------
-- Insert a single record into cot_cliente_contacto
----------------------------------------------------------------------------
CREATE PROC CMPut_contacto
	@id_cot_cliente int,
	@id int,
	@email nvarchar(160) = NULL,	
	@fecha_cumple varchar(20)
AS
	declare @idreal int = 0
	declare @fecha date

	set @fecha = cast(@fecha_cumple as date)

	IF @id = 0 
	begin
		select @idreal = isnull(id_cot_cliente_contacto,0)
		from cot_cliente 
		where id = @id_cot_cliente

		if @idreal = 0
			select @idreal = max(id)
			from cot_cliente_contacto
			where id_cot_cliente = @id_cot_cliente
	end
	else
		set @idreal = @id

		select top 10 * from cot_cliente_contacto
		where mes_dia_cumple is not null

	UPDATE	cot_cliente_contacto
	SET
	email = @email,	
	mes_dia_cumple = cast(month(@fecha)as varchar) + replicate('0',2-len(cast(day(@fecha)as varchar))) + cast(day(@fecha)as varchar)	,
	ano_cumple = cast(year(@fecha)as varchar)
	WHERE id = @idreal
GO



IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_usuarios')
	DROP PROC CMGet_usuarios
GO


----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla usuario
----------------------------------------------------------------------------
CREATE PROC CMGet_usuarios @id_emp int 

AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,u.id))),
	campo_2 = ltrim(rtrim(convert(varchar,id_usuario_subgrupo))),
	campo_3 = ltrim(rtrim(convert(varchar,codigo_usuario))),
	campo_4 = ltrim(rtrim(convert(varchar,nombre))),
	campo_5 = ltrim(rtrim(convert(varchar,clave))),
	campo_7 = ltrim(rtrim(convert(varchar,admin))),
	campo_8 = ltrim(rtrim(convert(varchar,email))),
	campo_9 = ltrim(rtrim(convert(varchar,cedula_nit))),
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''	
 FROM	usuario u
	join usuario_subgrupo s on s.id = u.id_usuario_subgrupo
	join usuario_grupo g on g.id = s.id_usuario_grupo
WHERE g.id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_item_sus')
	DROP PROC CMGetcot_item_sus
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_item_sus
----------------------------------------------------------------------------
CREATE PROC CMGetcot_item_sus @id_emp int 
AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id_cot_item))),
	campo_2 = ltrim(rtrim(convert(varchar,id_cot_item_sus))),
	campo_3 = ltrim(rtrim(convert(varchar,cantidad))),
	campo_4 = ltrim(rtrim(convert(varchar,id_crm))),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_item_sus s
	join cot_item i on i.id = s.id_cot_item
WHERE id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_iva')
	DROP PROC CMGetcot_iva
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_item_sus
----------------------------------------------------------------------------
CREATE PROC CMGetcot_iva @id_emp int 
AS

select 
	campo_1 = ltrim(rtrim(convert(varchar,id))),
	campo_2 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_3 = ltrim(rtrim(convert(varchar,porcentaje))),
	campo_4 = '',
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_iva 
WHERE id_emp = @id_emp
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMPut_EstadoOperacion')
	DROP PROC CMPut_EstadoOperacion
GO

CREATE PROCEDURE CMPut_EstadoOperacion 
	@id_cot_cotizacion_item int, 
	@id_usuario_autorizo int, 
	@autorizo int, 
	@id_con_cco int = 0
As
	if exists(select id_cot_cotizacion_item from cot_cotizacion_item_mas where id_cot_cotizacion_item = @id_cot_cotizacion_item)
		update cot_cotizacion_item_mas
		set autorizo = @autorizo, 
			id_usuario_autorizo = case when @id_usuario_autorizo > 0 then @id_usuario_autorizo else null end
		where id_cot_cotizacion_item = @id_cot_cotizacion_item
	else
		insert into cot_cotizacion_item_mas
		(
			id_cot_cotizacion_item, 
			id_usuario_autorizo, 
			id_con_cco, 
			autorizo
		)
		values 
		(
			@id_cot_cotizacion_item, 
			case when @id_usuario_autorizo > 0 then @id_usuario_autorizo else null end, 
			case when @id_con_cco > 0 then @id_con_cco else null end,
			case when @autorizo > 0 then @autorizo else null end
		)
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_cliente_origenes')
	DROP PROC CMGetcot_cliente_origenes
GO
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente_actividad
----------------------------------------------------------------------------
CREATE PROC CMGetcot_cliente_origenes @id_emp int 
AS
select 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,descripcion))),
	campo_4 = dbo.CMFormatoFecha(fecha_modif),
	campo_5 = '',
	campo_6 = '',
	campo_7 = '',
	campo_8 = '',
	campo_9 = '',
	campo_10 = '',
	campo_11 = '',
	campo_12 = '',
	campo_13 = '',
	campo_14 = '',
	campo_15 = ''
 FROM	cot_cliente_origen
  where id_emp = @id_emp
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMPut_EjecucionOperacion')
	DROP PROC CMPut_EjecucionOperacion
GO

CREATE PROCEDURE CMPut_EjecucionOperacion
	@id_cot_cotizacion_item int, 
	@que int	
As	
	insert into tal_operaciones_tiempo
	(
		id_cot_cotizacion_item, 
		que, 
		fecha_hora
	)
	values 
	(
		@id_cot_cotizacion_item, 
		@que, 
		getdate()
	)
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_cot_pedido')
	DROP PROC CMGet_cot_pedido
GO

create procedure dbo.CMGet_cot_pedido  @idPedido int
as
select Id, Id_emp as IdEmpresa, id_usuario_vende as usuario, id_cot_bodega as bodega, 
	dbo.CMFormatoFecha(fecha) as fecha, 
	id_cot_tipo as TipoDocumento,
		id_cot_cliente as cliente, id_usuario_vende as vendedor, id_cot_forma_pago as FormaPago, id_cot_cliente_contacto as Contacto,
		dias_validez as dias, total_sub as subtotal, isnull(total_descuento,0) as descuento, isnull(total_iva,0) as Iva,
		total_total as Total, id_cot_moneda as Moneda, tasa, 
		dbo.CMFormatoFecha(fecha_estimada) as FechaEstimada, notas_internas as NotasInternas,
		Notas, docref_tipo as TipoReferencia, docref_numero as NumeroReferencia, numero_cotizacion as Numero,
		'' as Estado, 0 as Factibilidad, 0 as idorden
from cot_pedido
where id = @idPedido
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_cot_pedido_item')
	DROP PROC CMGet_cot_pedido_item
GO

create procedure dbo.CMGet_cot_pedido_item @idPedido int
as
select id, renglon, id_cot_pedido as idcotizacion, id_cot_item as IdItem, cantidad, precio_lista as Precio,
	precio_cotizado as PrecioCotizado, porcentaje_iva as iva, notas, isnull(porcentaje_descuento,0) as Descuento,
	id_cot_item_lote as IdLote, id_cot_unidades as Und, isnull(conversion,1) as Conversion,
	 precio_cotizado * cantidad_und as subtotal,
	 facturar_a
from cot_pedido_item
where id_cot_pedido = @idPedido
order by renglon
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'PutFacturarCotPedidoItems')
	DROP PROC PutFacturarCotPedidoItems
GO

create procedure PutFacturarCotPedidoItems
(
	@renglon int, 
	@idcot int, 
	@iditem int, 
	@fac char(1)	
)
as
	if @fac<> ''
		update cot_pedido_item
		set facturar_a = @fac
		where renglon = @renglon and id_cot_pedido_item = @iditem and id_cot_pedido = @idcot
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_cot_cotizacion')
	DROP PROC CMGet_cot_cotizacion
GO

create procedure dbo.CMGet_cot_cotizacion  @idcotizacion int
as
select Id, 
	Id_emp as IdEmpresa, 
	id_usuario_vende as usuario, 
	id_cot_bodega as bodega, 
	dbo.CMFormatoFecha(fecha) as fecha, 
	id_cot_tipo as TipoDocumento,
	id_cot_cliente as cliente, 
	id_usuario_vende as vendedor, 
	id_cot_forma_pago as FormaPago, 
	id_cot_cliente_contacto as Contacto,
	dias_validez as dias, 
	total_sub as subtotal, 
	isnull(total_descuento,0) as descuento, 
	isnull(total_iva,0) as Iva,
	total_total as Total, 
	id_cot_moneda as Moneda, 
	tasa, 
	dbo.CMFormatoFecha(fecha_estimada) as FechaEstimada, 
	notas_internas as NotasInternas,
	Notas, 
	docref_tipo as TipoReferencia, 
	docref_numero as NumeroReferencia, 
	numero_cotizacion as Numero,
	id_cot_cotizacion_estado as Estado, 
	Factibilidad, 
	id_cot_item_lote,
	deducible,
	deducible_minimo
from cot_cotizacion
where id = @idcotizacion
go


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_cot_cotizacion_item')
	DROP PROC CMGet_cot_cotizacion_item
GO

create procedure dbo.CMGet_cot_cotizacion_item @idCotizacion int
as
select id, 
	renglon, 
	id_cot_cotizacion as idcotizacion, 
	id_cot_item as IdItem, 
	cantidad, 
	precio_lista as Precio,
	precio_cotizado as PrecioCotizado, 
	porcentaje_iva as iva, 
	notas, 
	isnull(porcentaje_descuento,0) as Descuento,
	id_cot_item_lote as IdLote,
	id_cot_unidades as Und, 
	isnull(conversion,1) as Conversion,
	 precio_cotizado * cantidad_und as subtotal,
	 descu_escal, -- valor_hora
	can_tot_dis, -- valor operación
	facturar_a, 
	id_operario
from cot_cotizacion_item
where id_cot_cotizacion = @idCotizacion
order by renglon
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGet_cot_item')
	DROP PROC CMGet_cot_item
GO

create procedure CMGet_cot_item @id int
as
	select isnull(id_veh_ano,0) as id_veh_ano
	from cot_item
	where id = @id
GO


