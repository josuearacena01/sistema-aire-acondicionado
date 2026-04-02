CREATE TRIGGER aire.TR_DetalleVentas_ActualizarStock
ON aire.DetalleVentas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar stock suficiente
    IF EXISTS (
        SELECT 1
        FROM aire.Productos p
        INNER JOIN inserted i ON p.IdProducto = i.IdProducto
        WHERE p.Stock < i.Cantidad
    )
    BEGIN
        RAISERROR('Stock insuficiente', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    -- Restar stock
    UPDATE p
    SET p.Stock = p.Stock - i.Cantidad
    FROM aire.Productos p
    INNER JOIN inserted i ON p.IdProducto = i.IdProducto;

    -- Registrar movimiento
    INSERT INTO aire.MovimientosInventario (IdProducto, TipoMovimiento, Cantidad)
    SELECT 
        i.IdProducto,
        'Salida',
        i.Cantidad
    FROM inserted i;
END;
GO

CREATE TRIGGER aire.TR_Productos_EstadoStock
ON aire.Productos
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Si stock es 0 o menor → Inactivo
    UPDATE p
    SET Estado = 'Inactivo'
    FROM aire.Productos p
    INNER JOIN inserted i ON p.IdProducto = i.IdProducto
    WHERE i.Stock <= 0;

    -- Si stock vuelve a ser mayor que 0 → Activo
    UPDATE p
    SET Estado = 'Activo'
    FROM aire.Productos p
    INNER JOIN inserted i ON p.IdProducto = i.IdProducto
    WHERE i.Stock > 0;
END;
GO