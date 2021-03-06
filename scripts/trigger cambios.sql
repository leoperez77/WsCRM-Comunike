IF EXISTS(SELECT * FROM sysobjects WHERE name = 'cm_cot_cliente_update')
	drop trigger cm_cot_cliente_update
GO

create trigger cm_cot_cliente_update on cot_cliente for insert, update as
begin
	declare @id int 
	declare @tname varchar(30)
	
	select @tname = OBJECT_NAME(parent_id)
	FROM sys.triggers
	WHERE object_id = @@PROCID

	select @id = id
	from inserted

	if not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values(@tname, @id, 0)
	else
	begin
		-- validar si el registro ya est� actualizado en la tabla lo que indicar�a que lleg� desde el CRM
		-- en cuyo caso no se hace actualizaci�n para evitar una actualizaci�n circular
		-- datediff(minute,ultcambio, getdate())>1
		if(not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id and estado=1 ))
			update cm_logcambios 
			set estado = 2, ultcambio = getdate()
			where tabla = @tname and idvalor = @id 
		else
			update cm_logcambios 
			set estado = 99
			where tabla = @tname and idvalor = @id 
	end 
end
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'cm_cot_item_update')
	drop trigger cm_cot_item_update
GO

create trigger cm_cot_item_update on cot_item for insert, update as
begin
	declare @id int 
	declare @tname varchar(30)
	
	select @tname = OBJECT_NAME(parent_id)
	FROM sys.triggers
	WHERE object_id = @@PROCID

	select @id = id
	from inserted

	if not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values(@tname, @id, 0)
	else
	begin
		-- validar si el registro ya est� actualizado en la tabla lo que indicar�a que lleg� desde el CRM
		-- en cuyo caso no se hace actualizaci�n para evitar una actualizaci�n circular
		-- datediff(minute,ultcambio, getdate())>1
		if(not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id and estado=1 ))
			update cm_logcambios 
			set estado = 2, ultcambio = getdate()
			where tabla = @tname and idvalor = @id 
		else
			update cm_logcambios 
			set estado = 99
			where tabla = @tname and idvalor = @id 
	end 
end
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'cm_cot_cliente_contacto_update')
	drop trigger cm_cot_cliente_contacto_update
GO

create trigger cm_cot_cliente_contacto_update on cot_cliente_contacto for insert, update as
begin
	declare @id int 
	declare @tname varchar(30)
	
	select @tname = OBJECT_NAME(parent_id)
	FROM sys.triggers
	WHERE object_id = @@PROCID

	select @id = id
	from inserted

	if not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values(@tname, @id, 0)
	else
	begin
		-- validar si el registro ya est� actualizado en la tabla lo que indicar�a que lleg� desde el CRM
		-- en cuyo caso no se hace actualizaci�n para evitar una actualizaci�n circular
		-- datediff(minute,ultcambio, getdate())>1
		if(not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id and estado=1 ))
			update cm_logcambios 
			set estado = 2, ultcambio = getdate()
			where tabla = @tname and idvalor = @id 
		else
			update cm_logcambios 
			set estado = 99
			where tabla = @tname and idvalor = @id 
	end 
end
go

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'cm_cot_item_lote_update')
	drop trigger cm_cot_item_lote_update
GO

create trigger cm_cot_item_lote_update on cot_cliente_contacto for insert, update as
begin
	declare @id int 
	declare @tname varchar(30)
	
	select @tname = OBJECT_NAME(parent_id)
	FROM sys.triggers
	WHERE object_id = @@PROCID

	select @id = id
	from inserted

	if not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values(@tname, @id, 0)
	else
	begin
		-- validar si el registro ya est� actualizado en la tabla lo que indicar�a que lleg� desde el CRM
		-- en cuyo caso no se hace actualizaci�n para evitar una actualizaci�n circular
		-- datediff(minute,ultcambio, getdate())>1
		if(not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id and estado=1 ))
			update cm_logcambios 
			set estado = 2, ultcambio = getdate()
			where tabla = @tname and idvalor = @id 
		else
			update cm_logcambios 
			set estado = 99
			where tabla = @tname and idvalor = @id 
	end 
end
go


IF EXISTS(SELECT * FROM sysobjects WHERE name = 'cm_tal_camp_enc_update')
	drop trigger cm_tal_camp_enc_update
GO

create trigger cm_tal_camp_enc_update on cot_cliente_contacto for insert, update as
begin
	declare @id int 
	declare @tname varchar(30)
	
	select @tname = OBJECT_NAME(parent_id)
	FROM sys.triggers
	WHERE object_id = @@PROCID

	select @id = id
	from inserted

	if not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values(@tname, @id, 0)
	else
	begin
		-- validar si el registro ya est� actualizado en la tabla lo que indicar�a que lleg� desde el CRM
		-- en cuyo caso no se hace actualizaci�n para evitar una actualizaci�n circular
		-- datediff(minute,ultcambio, getdate())>1
		if(not exists(select id from cm_logcambios where tabla = @tname and idvalor = @id and estado=1 ))
			update cm_logcambios 
			set estado = 2, ultcambio = getdate()
			where tabla = @tname and idvalor = @id 
		else
			update cm_logcambios 
			set estado = 99
			where tabla = @tname and idvalor = @id 
	end 
end
go

