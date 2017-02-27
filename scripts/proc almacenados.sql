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
	campo_25 = ltrim(rtrim(convert(varchar, c.fecha_creacion))),				-- Fecha en la que se creo el tercero  
	campo_26 = ltrim(rtrim(convert(varchar, c.fecha_modificacion))),			-- Fecha en la que se modifico el tercero
	campo_27 = ltrim(rtrim(convert(varchar,
		case when o.ano_cumple is not null and mes_dia_cumple is not null then
		cast(cast(ano_cumple as varchar) + 
			cast(right(mes_dia_cumple,2) as varchar) +
			replicate('0',2-len(left(mes_dia_cumple,len(mes_dia_cumple)-2))) + 
			left(mes_dia_cumple,len(mes_dia_cumple)-2) as date)
		else '' end))),															-- fecha cumpleaños
	campo_28 = ltrim(rtrim(c.notas)),											-- Notas del tercero
	campo_29 = ltrim(rtrim(convert(varchar, 
	case when o.ano_cumple is not null and mes_dia_cumple is not null then	
		DATEDIFF( YY, cast(cast(ano_cumple as varchar) + 
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
	campo_33 = ltrim(rtrim(convert(varchar, 0))),								-- pos_num no se usa
	campo_34 = ltrim(rtrim(convert(varchar, id_cot_estado))),					-- tabla cot_estado
	campo_35 = ltrim(rtrim(convert(varchar, isnull(l.estado,1)))),				-- Indica el estado del tercero para el CRM (bandera)		
	campo_36 = ltrim(rtrim(n.nom1)),											-- nombre 1
	campo_37 = ltrim(rtrim(n.nom2)),											-- nombre 2
	campo_38 = ltrim(rtrim(n.nom3)),											-- nombre 3
	campo_39 = ltrim(rtrim(n.ape1)),											-- nombre 4
	campo_40 = ltrim(rtrim(n.ape2)),											-- nombre 5
	campo_41 = ltrim(rtrim(convert(varchar, c.id_emp))),						-- id empresa	
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
 FROM	cot_cliente c
	join tipo_tributario t on t.id = c.id_tipo_tributario
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

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynccot_clientes')
	drop procedure CMGetcot_clientes
go

create procedure CMSynccot_clientes
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
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMGetcot_items')
	DROP PROC CMGetcot_items
go

----------------------------------------------------------------------------
-- Procedimiento de lectura de registros por sincronizar tabla cot_item
----------------------------------------------------------------------------
CREATE PROC CMGetcot_items @id_emp int
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
	campo_10 = ltrim(rtrim(notas)),
	campo_11 = ltrim(rtrim(convert(varchar,numero_decimales))),
	campo_12 = ltrim(rtrim(convert(varchar,fecha_creacion))),	
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
 FROM	cot_item i
	join cot_grupo_sub s on i.id_cot_grupo_sub = s.id
	join cot_iva v on i.id_cot_iva = v.id
	left join cot_item_talla t on t.id = i.id_cot_item_talla
	left join cot_item_color c on c.id = i.id_cot_item_color
	left join cm_logcambios l on l.tabla = 'cot_item' and l.idvalor = c.id
 WHERE i.id_emp = @id_emp 
	and id_veh_ano is null
GO

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'CMSynccot_items')
	drop procedure CMSynccot_items
go

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
	join tipo_tributario_emp e on e.id_tipo_tributario = t.id
where e.id_emp = @id_emp
GO


----  
--MakeSelectAllSP tipo_tributario

