CREATE PROCEDURE aire.SP_RegistrarVenta
    @IdCliente      INT,
    @IdUsuario      INT,
    @Productos      NVARCHAR(MAX),
    @MetodoPago     VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON
    SET XACT_ABORT ON

    BEGIN TRY
        BEGIN TRANSACTION

        -- JSON vacío
        IF @Productos IS NULL OR LEN(@Productos) = 0
        BEGIN
            RAISERROR('Debe enviar productos', 16, 1)
            RETURN
        END

        -- Método de pago válido
        IF @MetodoPago NOT IN ('Efectivo', 'Tarjeta', 'Transferencia')
        BEGIN
            RAISERROR('Método de pago inválido', 16, 1)
            RETURN
        END
        -- Productos existentes
        IF EXISTS (
            SELECT 1
            FROM OPENJSON(@Productos)
            WITH (IdProducto INT '$.IdProducto') j
            LEFT JOIN aire.Productos p ON j.IdProducto = p.IdProducto
            WHERE p.IdProducto IS NULL
        )
        BEGIN
            RAISERROR('Uno o más productos no existen', 16, 1);
            RETURN
        END

        DECLARE @IdVenta        INT
        DECLARE @IdFactura      INT
        DECLARE @TotalVenta     DECIMAL(10,2)

        INSERT INTO aire.Ventas (IdCliente, IdUsuario)
        VALUES (@IdCliente, @IdUsuario)

        SET @IdVenta = SCOPE_IDENTITY()

        INSERT INTO aire.DetalleVentas 
            (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
        SELECT
            @IdVenta,
            j.IdProducto,
            j.Cantidad,
            j.PrecioUnitario,
            j.Cantidad * j.PrecioUnitario
        FROM OPENJSON(@Productos)
        WITH (
            IdProducto      INT             '$.IdProducto',
            Cantidad        INT             '$.Cantidad',
            PrecioUnitario  DECIMAL(10,2)   '$.PrecioUnitario'
        ) j

        SELECT @TotalVenta = SUM(Subtotal)
        FROM aire.DetalleVentas
        WHERE IdVenta = @IdVenta

        UPDATE aire.Ventas
        SET Total_Venta = @TotalVenta
        WHERE IdVenta = @IdVenta

        INSERT INTO aire.Facturas (IdCliente, IdEstadoFactura, Total_Factura)
        VALUES (@IdCliente, 1, @TotalVenta)

        SET @IdFactura = SCOPE_IDENTITY()

        INSERT INTO aire.FacturaDetalle 
            (IdFactura, IdVenta, DescripcionDetalle, Cantidad, PrecioUnitario, Subtotal)
        SELECT
            @IdFactura,
            @IdVenta,
            p.Nombre,
            dv.Cantidad,
            dv.PrecioUnitario,
            dv.Subtotal
        FROM aire.DetalleVentas dv
        INNER JOIN aire.Productos p ON dv.IdProducto = p.IdProducto
        WHERE dv.IdVenta = @IdVenta

        INSERT INTO aire.Pagos 
            (IdFactura, IdUsuario, MetodoPago, Monto)
        VALUES 
            (@IdFactura, @IdUsuario, @MetodoPago, @TotalVenta);

        UPDATE aire.Facturas
        SET IdEstadoFactura = 2
        WHERE IdFactura = @IdFactura

        COMMIT TRANSACTION

        SELECT 
            @IdVenta AS IdVenta, 
            @IdFactura AS IdFactura, 
            @TotalVenta AS Total

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW
    END CATCH
END
GO