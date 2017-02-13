
----------------------------------------------------------------------------
-- Procedimiento de lectura de registos por sincronizar tabla cot_cliente
----------------------------------------------------------------------------
ALTER PROC CMGetcot_clientes
	@id_emp int
AS

select top 5 
	campo_1 = ltrim(rtrim(convert(varchar,id_emp))),
	campo_2 = ltrim(rtrim(convert(varchar,id))),
	campo_3 = ltrim(rtrim(convert(varchar,id_cot_zona_sub))),
	campo_4 = ltrim(rtrim(convert(varchar,razon_social))),
	campo_5 = ltrim(rtrim(convert(varchar,tel_1))),
	campo_6 = ltrim(rtrim(convert(varchar,tel_2))),
	campo_7 = ltrim(rtrim(convert(varchar,direccion))),
	campo_8 = ltrim(rtrim(convert(varchar,url))),
	campo_9 = ltrim(rtrim(convert(varchar,id_cot_estado))),
	campo_10 = ltrim(rtrim(convert(varchar,id_usuario_vendedor))),
	campo_11 = ltrim(rtrim(convert(varchar,id_cot_cliente_actividad))),
	campo_12 = ltrim(rtrim(convert(varchar,id_cot_cliente_origen))),
	campo_13 = ltrim(rtrim(convert(varchar,nit))),
	campo_14 = ltrim(rtrim(convert(varchar,notas))),
	campo_15 = ltrim(rtrim(convert(varchar,privado))),
	campo_16 = ltrim(rtrim(convert(varchar,ventas1))),
	campo_17 = ltrim(rtrim(convert(varchar,ventas2))),
	campo_18 = ltrim(rtrim(convert(varchar,utilidad1))),
	campo_19 = ltrim(rtrim(convert(varchar,utilidad2))),
	campo_20 = ltrim(rtrim(convert(varchar,id_cot_cliente_tipo))),
	campo_21 = ltrim(rtrim(convert(varchar,id_cot_cliente_perfil))),
	campo_22 = ltrim(rtrim(convert(varchar,cupo_credito))),
	campo_23 = ltrim(rtrim(convert(varchar,precio_costo))),
	campo_24 = ltrim(rtrim(convert(varchar,id_tipo_tributario))),
	campo_25 = ltrim(rtrim(convert(varchar,digito))),
	campo_26 = ltrim(rtrim(convert(varchar,id_cot_cliente_contacto))),
	campo_27 = ltrim(rtrim(convert(varchar,id_cot_forma_pago))),
	campo_28 = ltrim(rtrim(convert(varchar,id_cot_item_listas))),
	campo_29 = ltrim(rtrim(convert(varchar,id_cot_tipo))),
	campo_30 = ltrim(rtrim(convert(varchar,id_cot_cotizacion_formatos))),
	campo_31 = ltrim(rtrim(convert(varchar,id_con_cco))),
	campo_32 = ltrim(rtrim(convert(varchar,permite_controlados))),
	campo_33 = ltrim(rtrim(convert(varchar,id_cot_cliente_pais))),
	campo_34 = ltrim(rtrim(convert(varchar,fecha_modificacion))),
	campo_35 = ltrim(rtrim(convert(varchar,clave_web))),
	campo_36 = ltrim(rtrim(convert(varchar,recibir_mail))),
	campo_37 = ltrim(rtrim(convert(varchar,impedir_descuentos_pie))),
	campo_38 = ltrim(rtrim(convert(varchar,idv))),
	campo_39 = ltrim(rtrim(convert(varchar,fecha_creacion))),
	campo_40 = ltrim(rtrim(convert(varchar,id_cot_bodega))),
	campo_41 = ltrim(rtrim(convert(varchar,fletes))),
	campo_42 = ltrim(rtrim(convert(varchar,id_emp_orig))),
	campo_43 = ltrim(rtrim(convert(varchar,dias_gracia))),
	campo_44 = ltrim(rtrim(convert(varchar,id_tipo_tributario2))),
	campo_45 = ltrim(rtrim(convert(varchar,max_dcto))),
	campo_46 = ltrim(rtrim(convert(varchar,id_cot_bodega_recep))),
	campo_47 = ltrim(rtrim(convert(varchar,descu_escal))),
	campo_48 = ltrim(rtrim(convert(varchar,dia_max))),
	campo_49 = ltrim(rtrim(convert(varchar,control_unidades))),
	campo_50 = ltrim(rtrim(convert(varchar,codigo))),
	campo_51 = ltrim(rtrim(convert(varchar,no_iva))),
	campo_52 = ltrim(rtrim(convert(varchar,no_sismed))),
	campo_53 = ltrim(rtrim(convert(varchar,cupo_credito_tal))),
	campo_54 = ltrim(rtrim(convert(varchar,cupo_credito_veh))),
	campo_55 = ltrim(rtrim(convert(varchar,ppal))),
	campo_56 = ltrim(rtrim(convert(varchar,def_taller))),
	campo_57 = '',
	campo_58 = '',
	campo_59 = '',
	campo_60 = ''

 FROM	cot_cliente
 WHERE id_emp = @id_emp and isnull(SyncStatus,0) in (0,2)
GO

alter procedure CMPutcot_cliente
	@id int
as
	update cot_cliente
	set SyncStatus = 2
	where id = @id
go

