--CREATE TABLE CM_OrigenOrdenes
--(
--	Id INT IDENTITY  NOT NULL PRIMARY KEY,
--	IdCotizacion INT,
--	IdPedido INT,
--	IdOrden INT, 
--	Origen CHAR(1)
--)
--GO

--USE dms_smd3pruebas

IF EXISTS (   SELECT *
              FROM   sysobjects
              WHERE  name = 'PutCM_OrigenOrdenes' )
    DROP PROC PutCM_OrigenOrdenes;
GO

----------------------------------------------------------------------------
-- Insert a single record into CM_OrigenOrdenes
----------------------------------------------------------------------------
CREATE PROC PutCM_OrigenOrdenes
    @IdCotizacion INT = 0 ,
    @IdPedido INT = 0 ,
    @IdOrden INT = 0 ,
    @Origen CHAR(1)
AS
    IF @IdCotizacion > 0
        BEGIN
            IF NOT EXISTS (   SELECT id
                              FROM   CM_OrigenOrdenes
                              WHERE  ISNULL(@IdCotizacion, 0) = @IdCotizacion )
                INSERT INTO cm_origenordenes ( idcotizacion ,
                                               origen )
                VALUES ( @IdCotizacion, @Origen );
            ELSE
                UPDATE Cm_OrigenOrdenes
                SET    Origen = @Origen
                WHERE  IdCotizacion = @IdCotizacion;
        END;

    IF @IdPedido > 0
        BEGIN
            IF NOT EXISTS (   SELECT id
                              FROM   CM_OrigenOrdenes
                              WHERE  ISNULL(@IdPedido, 0) = @IdPedido )
                INSERT INTO cm_origenordenes ( IdPedido ,
                                               origen )
                VALUES ( @IdPedido, @Origen );
            ELSE
                UPDATE Cm_OrigenOrdenes
                SET    Origen = @Origen
                WHERE  IdPedido = @IdPedido;
        END;


    IF @IdOrden > 0
        BEGIN
            IF NOT EXISTS (   SELECT id
                              FROM   CM_OrigenOrdenes
                              WHERE  ISNULL(@IdOrden, 0) = @IdOrden )
                INSERT INTO cm_origenordenes ( IdOrden ,
                                               origen )
                VALUES ( @IdOrden, @Origen );
            ELSE
                UPDATE Cm_OrigenOrdenes
                SET    Origen = @Origen
                WHERE  IdOrden = @IdOrden;
        END;
GO



ALTER PROCEDURE dbo.CMGet_cot_pedido
    @idPedido INT
AS
    SELECT c.id ,
           id_emp AS IdEmpresa ,
           id_usuario_vende AS usuario ,
           id_cot_bodega AS bodega ,
           dbo.CMFormatoFecha(fecha) AS fecha ,
           id_cot_tipo AS TipoDocumento ,
           id_cot_cliente AS cliente ,
           id_usuario_vende AS vendedor ,
           id_cot_forma_pago AS FormaPago ,
           id_cot_cliente_contacto AS Contacto ,
           dias_validez AS dias ,
           total_sub AS subtotal ,
           ISNULL(total_descuento, 0) AS descuento ,
           ISNULL(total_iva, 0) AS Iva ,
           total_total AS Total ,
           id_cot_moneda AS Moneda ,
           tasa ,
           dbo.CMFormatoFecha(fecha_estimada) AS FechaEstimada ,
           notas_internas AS NotasInternas ,
           notas ,
           docref_tipo AS TipoReferencia ,
           docref_numero AS NumeroReferencia ,
           numero_cotizacion AS Numero ,
           '' AS Estado ,
           0 AS Factibilidad ,
           0 AS idorden ,
           ISNULL(o.origen, '') AS Origen
    FROM   cot_pedido c
           LEFT JOIN Cm_OrigenOrdenes o ON o.idPedido = c.id
    WHERE  c.id = @idPedido;
GO


ALTER PROCEDURE dbo.CMGet_cot_cotizacion
    @idcotizacion INT
AS
    SELECT c.id ,
           id_emp AS IdEmpresa ,
           id_usuario_vende AS usuario ,
           id_cot_bodega AS bodega ,
           dbo.CMFormatoFecha(fecha) AS fecha ,
           id_cot_tipo AS TipoDocumento ,
           id_cot_cliente AS cliente ,
           id_usuario_vende AS vendedor ,
           id_cot_forma_pago AS FormaPago ,
           id_cot_cliente_contacto AS Contacto ,
           dias_validez AS dias ,
           total_sub AS subtotal ,
           ISNULL(total_descuento, 0) AS descuento ,
           ISNULL(total_iva, 0) AS Iva ,
           total_total AS Total ,
           id_cot_moneda AS Moneda ,
           tasa ,
           dbo.CMFormatoFecha(fecha_estimada) AS FechaEstimada ,
           notas_internas AS NotasInternas ,
           notas ,
           docref_tipo AS TipoReferencia ,
           docref_numero AS NumeroReferencia ,
           numero_cotizacion AS Numero ,
           id_cot_cotizacion_estado AS Estado ,
           factibilidad ,
           id_cot_item_lote ,
           deducible ,
           deducible_minimo ,
           ISNULL(o.origen, '') AS Origen
    FROM   cot_cotizacion c
           LEFT JOIN Cm_OrigenOrdenes o ON o.idCotizacion = c.id
    WHERE  c.id = @idcotizacion;
GO

IF EXISTS (   SELECT *
              FROM   sysobjects
              WHERE  name = 'CM_DelItemOrden' )
    DROP PROC CM_DelItemOrden;
GO

CREATE PROCEDURE CM_DelItemOrden @IdLinea INT
AS
BEGIN
	DELETE dbo.cot_cotizacion_item
	WHERE id = @idlinea
END
GO

IF EXISTS (   SELECT *
              FROM   sysobjects
              WHERE  name = 'CM_DelItemPedido' )
    DROP PROC CM_DelItemPedido;
GO

CREATE PROCEDURE CM_DelItemPedido @IdLinea INT
AS
BEGIN
	DELETE dbo.cot_pedido_item
	WHERE id = @idlinea
END

CMGet_cot_cotizacion 