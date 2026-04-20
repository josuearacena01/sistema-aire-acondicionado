// ===== API SERVICE LAYER =====
const API_BASE = 'http://localhost:5152/api';

const Api = {
    async request(method, path, body = null) {
        const opts = {
            method,
            headers: { 'Content-Type': 'application/json' },
        };
        if (body) opts.body = JSON.stringify(body);
        const res = await fetch(`${API_BASE}${path}`, opts);
        if (res.status === 204) return true;
        if (res.status === 401) return { error: 'Credenciales inválidas' };
        if (!res.ok) {
            const text = await res.text();
            let msg;
            try {
                const j = JSON.parse(text);
                msg = j.errors ? Object.values(j.errors).flat().join(', ') : j.title || text;
            } catch { msg = text || `Error ${res.status}`; }
            return { error: msg };
        }
        const text = await res.text();
        return text ? JSON.parse(text) : true;
    },

    // Auth
    login: (data) => Api.request('POST', '/Auth/login', data),

    // Catalogos
    getProvincias: () => Api.request('GET', '/Catalogos/provincias'),
    getMarcas: () => Api.request('GET', '/Catalogos/marcas'),
    getModelos: (idMarca) => Api.request('GET', `/Catalogos/marcas/${idMarca}/modelos`),
    getCategorias: () => Api.request('GET', '/Catalogos/categorias'),
    getTiposServicio: () => Api.request('GET', '/Catalogos/tipos-servicio'),
    getEstadosServicio: () => Api.request('GET', '/Catalogos/estados-servicio'),

    // Clientes
    getClientes: () => Api.request('GET', '/Clientes'),
    getCliente: (id) => Api.request('GET', `/Clientes/${id}`),
    getClienteByCedula: (ced) => Api.request('GET', `/Clientes/cedula/${ced}`),
    createCliente: (d) => Api.request('POST', '/Clientes', d),
    updateCliente: (id, d) => Api.request('PUT', `/Clientes/${id}`, d),

    // Vehiculos
    getVehiculos: () => Api.request('GET', '/Vehiculos'),
    getVehiculo: (id) => Api.request('GET', `/Vehiculos/${id}`),
    createVehiculo: (d) => Api.request('POST', '/Vehiculos', d),
    updateVehiculo: (id, d) => Api.request('PUT', `/Vehiculos/${id}`, d),

    // Servicios
    getServicios: () => Api.request('GET', '/Servicios'),
    getServicio: (id) => Api.request('GET', `/Servicios/${id}`),
    getServiciosByCliente: (id) => Api.request('GET', `/Servicios/cliente/${id}`),
    createServicio: (d) => Api.request('POST', '/Servicios', d),
    updateEstadoServicio: (id, est) => Api.request('PUT', `/Servicios/${id}/estado/${est}`),
    deleteServicio: (id) => Api.request('DELETE', `/Servicios/${id}`),

    // Detalle Servicios
    getDetallesByServicio: (id) => Api.request('GET', `/DetalleServicios/servicio/${id}`),
    createDetalle: (d) => Api.request('POST', '/DetalleServicios', d),
    deleteDetalle: (id, idServ) => Api.request('DELETE', `/DetalleServicios/${id}/servicio/${idServ}`),

    // Productos
    getProductos: () => Api.request('GET', '/Productos'),
    getProducto: (id) => Api.request('GET', `/Productos/${id}`),
    createProducto: (d) => Api.request('POST', '/Productos', d),
    updateProducto: (id, d) => Api.request('PUT', `/Productos/${id}`, d),

    // Facturas
    getFacturas: () => Api.request('GET', '/Facturas'),
    getFactura: (id) => Api.request('GET', `/Facturas/${id}`),
    getFacturasByCliente: (id) => Api.request('GET', `/Facturas/cliente/${id}`),
    anularFactura: (id) => Api.request('PUT', `/Facturas/${id}/anular`),

    // Reportes
    getDashboard: (fi, ff) => Api.request('GET', `/Reportes/dashboard?fechaInicio=${fi}&fechaFin=${ff}`),
};
