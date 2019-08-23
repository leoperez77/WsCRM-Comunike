

CREATE table cm_logcambios
(
	id int identity not null primary key,
	tabla varchar(40),
	idvalor int,
	estado tinyint,
	ultcambio datetime not null default getdate()
)

drop table cm_Logcambios

SELECT
    OBJECT_NAME(parent_id) AS [Table],
    OBJECT_NAME(object_id) AS TriggerName
FROM
    sys.triggers
WHERE
    object_id = @@PROCID
