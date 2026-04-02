CREATE VIEW aire.vw_Facturas AS
SELECT 
    f.IdFactura,
    f.Fecha_Factura AS FechaFactura,
    f.Total_Factura AS TotalFactura,
    ef.Nombre AS EstadoFactura,
    c.IdCliente,
    ISNULL(c.Nombres, '') + ' ' + ISNULL(c.Apellidos, '') AS Cliente,
    c.Cedula
FROM aire.Facturas f
INNER JOIN aire.Clientes c ON f.IdCliente = c.IdCliente
INNER JOIN aire.EstadosFactura ef ON f.IdEstadoFactura = ef.IdEstadoFactura
GO

CREATE VIEW aire.vw_Servicios AS
SELECT
    s.IdServicio,
    s.FechaIngreso,
    s.Diagnostico,
    s.Total_Servicio AS TotalServicio,
    es.Nombre AS EstadoServicio,
    c.IdCliente,
    ISNULL(c.Nombres, '') + ' ' + ISNULL(c.Apellidos, '') AS Cliente,
    c.Cedula,
    v.IdVehiculo,
    v.Placa,
    v.Anio,
    m.Nombre AS Marca,
    mo.Nombre AS Modelo
FROM aire.Servicios s
INNER JOIN aire.Clientes c ON s.IdCliente = c.IdCliente
INNER JOIN aire.EstadosServicio es ON s.IdEstadoServicio = es.IdEstadoServicio
INNER JOIN aire.Vehiculos v ON s.IdVehiculo = v.IdVehiculo
INNER JOIN aire.Marcas m ON v.IdMarca = m.IdMarca
INNER JOIN aire.Modelos mo ON v.IdModelo = mo.IdModelo
GO

CREATE VIEW aire.vw_Productos AS
SELECT
    p.IdProducto,
    p.Nombre,
    p.Precio,
    p.Stock,
    p.Estado,
    c.IdCategoria,
    c.Nombre AS Categoria,
    CASE 
        WHEN p.Stock <= 0 THEN 'Agotado'
        ELSE 'Disponible'
    END AS EstadoStock
FROM aire.Productos p
INNER JOIN aire.Categorias c ON p.IdCategoria = c.IdCategoria
GO