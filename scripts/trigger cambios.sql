alter trigger cm_cot_cliente_update on cot_cliente for insert, update as
begin
	declare @id int 
	
	select @id = id
	from inserted

	if not exists(select id from cm_logcambios where tabla = 'cot_cliente' and idvalor = @id)
		insert into cm_logcambios(tabla, idvalor, estado)
		values('cot_cliente', @id, 0)
	else
	begin
		-- validar si el registro ya está actualizado en la tabla lo que indicaría que llegó desde el CRM
		-- en cuyo caso no se hace actualización para evitar una actualización circular
		-- datediff(minute,ultcambio, getdate())>1
		if(not exists(select id from cm_logcambios where tabla = 'cot_cliente' and idvalor = @id and estado=1 ))
			update cm_logcambios 
			set estado = 2, ultcambio = getdate()
			where tabla = 'cot_cliente' and idvalor = @id 
		else
			update cm_logcambios 
			set estado = 99
			where tabla = 'cot_cliente' and idvalor = @id 
	end 
end
go


alter trigger cm_cot_cliente_update on cot_cliente for insert, update as
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
		-- validar si el registro ya está actualizado en la tabla lo que indicaría que llegó desde el CRM
		-- en cuyo caso no se hace actualización para evitar una actualización circular
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



select *
from cm_logcambios


