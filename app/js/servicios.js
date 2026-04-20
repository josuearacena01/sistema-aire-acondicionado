// ===== SERVICIOS PAGE =====
async function renderServicios() {
    const content = document.getElementById('content');
    content.innerHTML = `
        <div class="page-enter">
            <div class="page-header">
                <h1>Servicios</h1>
                <div class="page-header-actions">
                    <button class="btn btn-accent" id="btn-new-servicio">
                        <span class="material-icons-round">add</span> Nuevo Servicio
                    </button>
                </div>
            </div>
            <div class="table-container">
                <div class="table-toolbar">
                    <div class="search-box">
                        <span class="material-icons-round">search</span>
                        <input type="text" id="search-servicios" placeholder="Buscar por cliente, placa o estado...">
                    </div>
                </div>
                <div id="servicios-table">${showLoading()}</div>
            </div>
        </div>`;

    const servicios = await Api.getServicios();
    renderServiciosTable(Array.isArray(servicios) ? servicios : []);

    document.getElementById('btn-new-servicio').addEventListener('click', () => openServicioForm());
    document.getElementById('search-servicios').addEventListener('input', (e) => {
        const q = e.target.value.toLowerCase();
        document.querySelectorAll('#servicios-table tbody tr').forEach(r => {
            r.style.display = r.textContent.toLowerCase().includes(q) ? '' : 'none';
        });
    });
}

function renderServiciosTable(servicios) {
    const container = document.getElementById('servicios-table');
    if (!servicios.length) { container.innerHTML = showEmpty('build', 'No hay servicios registrados'); return; }
    container.innerHTML = `
        <table>
            <thead><tr>
                <th>ID</th><th>Fecha</th><th>Cliente</th><th>Vehículo</th><th>Placa</th><th>Total</th><th>Estado</th><th>Acciones</th>
            </tr></thead>
            <tbody>
                ${servicios.map(s => `
                    <tr>
                        <td style="font-weight:600;color:var(--text-primary)">#${s.idServicio}</td>
                        <td>${formatDate(s.fechaIngreso)}</td>
                        <td>${s.cliente || '—'}</td>
                        <td>${s.marca} ${s.modelo} ${s.anio}</td>
                        <td style="font-weight:600">${s.placa}</td>
                        <td style="font-weight:600;color:var(--success)">${formatMoney(s.totalServicio)}</td>
                        <td>${estadoBadge(s.estadoServicio)}</td>
                        <td class="actions-cell">
                            <button class="btn-icon" onclick="viewServicioDetail(${s.idServicio})" title="Ver detalles">
                                <span class="material-icons-round">visibility</span>
                            </button>
                            <button class="btn-icon" onclick="openCambiarEstado(${s.idServicio})" title="Cambiar estado">
                                <span class="material-icons-round">swap_horiz</span>
                            </button>
                            ${s.estadoServicio && s.estadoServicio.toLowerCase().includes('pendiente') ? `
                                <button class="btn-icon danger" onclick="eliminarServicio(${s.idServicio})" title="Eliminar">
                                    <span class="material-icons-round">delete</span>
                                </button>
                            ` : ''}
                        </td>
                    </tr>
                `).join('')}
            </tbody>
        </table>`;
}

async function openServicioForm() {
    await loadCatalogos();
    const clientes = await Api.getClientes();
    const vehiculos = await Api.getVehiculos();
    const cList = Array.isArray(clientes) ? clientes : [];
    const vList = Array.isArray(vehiculos) ? vehiculos : [];

    openModal('Nuevo Servicio', `
        <form id="servicio-form">
            <div class="form-group">
                <label>Cliente</label>
                <select id="sf-cliente" required>
                    <option value="">— Seleccionar —</option>
                    ${cList.map(c => `<option value="${c.idCliente}">${c.nombres} ${c.apellidos} (${c.cedula})</option>`).join('')}
                </select>
            </div>
            <div class="form-group">
                <label>Vehículo</label>
                <select id="sf-vehiculo" required>
                    <option value="">— Seleccionar —</option>
                    ${vList.map(v => `<option value="${v.idVehiculo}">#${v.idVehiculo} — ${v.anio}</option>`).join('')}
                </select>
            </div>
            <div class="form-group">
                <label>Placa del Vehículo</label>
                <input type="text" id="sf-placa" required minlength="6" maxlength="10" placeholder="Ej: A123456">
            </div>
            <div class="form-group">
                <label>Diagnóstico</label>
                <textarea id="sf-diagnostico" maxlength="500" placeholder="Descripción del problema..."></textarea>
            </div>
            <div class="modal-actions">
                <button type="button" class="btn btn-outline" onclick="closeModal()">Cancelar</button>
                <button type="submit" class="btn btn-accent">Crear Servicio</button>
            </div>
        </form>
    `);

    document.getElementById('servicio-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        const data = {
            idCliente: parseInt(document.getElementById('sf-cliente').value),
            idUsuario: App.user.id,
            idVehiculo: parseInt(document.getElementById('sf-vehiculo').value),
            placa: document.getElementById('sf-placa').value,
            diagnostico: document.getElementById('sf-diagnostico').value || null,
        };
        const res = await Api.createServicio(data);
        if (res && !res.error) {
            showToast('Servicio creado exitosamente', 'success');
            closeModal(); renderServicios();
        } else showToast(res.error || 'Error al crear servicio', 'error');
    });
}

async function openCambiarEstado(idServicio) {
    await loadCatalogos();
    openModal('Cambiar Estado', `
        <form id="estado-form">
            <div class="form-group">
                <label>Nuevo Estado</label>
                <select id="ef-estado" required>
                    <option value="">— Seleccionar —</option>
                    ${App.cache.estadosServicio.map(e => `<option value="${e.idEstadoServicio}">${e.nombre}</option>`).join('')}
                </select>
            </div>
            <div class="modal-actions">
                <button type="button" class="btn btn-outline" onclick="closeModal()">Cancelar</button>
                <button type="submit" class="btn btn-accent">Actualizar</button>
            </div>
        </form>
    `);

    document.getElementById('estado-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        const est = parseInt(document.getElementById('ef-estado').value);
        const res = await Api.updateEstadoServicio(idServicio, est);
        if (res === true) { showToast('Estado actualizado', 'success'); closeModal(); renderServicios(); }
        else showToast(res.error || 'Error', 'error');
    });
}

async function eliminarServicio(id) {
    if (!confirm('¿Eliminar este servicio pendiente?')) return;
    const res = await Api.deleteServicio(id);
    if (res === true) { showToast('Servicio eliminado', 'success'); renderServicios(); }
    else showToast(res.error || 'Solo se pueden eliminar servicios pendientes', 'error');
}

async function viewServicioDetail(id) {
    const content = document.getElementById('content');
    content.innerHTML = `<div class="page-enter">${showLoading()}</div>`;

    await loadCatalogos();
    const servicio = await Api.getServicio(id);
    const detalles = await Api.getDetallesByServicio(id);

    if (!servicio || servicio.error) {
        content.innerHTML = showEmpty('error', 'Servicio no encontrado');
        return;
    }

    const detList = Array.isArray(detalles) ? detalles : [];
    const tiposMap = {};
    App.cache.tiposServicio.forEach(t => tiposMap[t.idTipoServicio] = t.nombre);

    content.innerHTML = `
        <div class="page-enter">
            <div class="detail-header">
                <button class="btn-back" onclick="renderServicios()">
                    <span class="material-icons-round">arrow_back</span>
                </button>
                <h1>Servicio #${servicio.idServicio}</h1>
                ${estadoBadge(servicio.estadoServicio)}
            </div>

            <div class="detail-grid">
                <div class="detail-item"><span class="label">Cliente</span><div class="value">${servicio.cliente}</div></div>
                <div class="detail-item"><span class="label">Cédula</span><div class="value">${servicio.cedula}</div></div>
                <div class="detail-item"><span class="label">Vehículo</span><div class="value">${servicio.marca} ${servicio.modelo} ${servicio.anio}</div></div>
                <div class="detail-item"><span class="label">Placa</span><div class="value">${servicio.placa}</div></div>
                <div class="detail-item"><span class="label">Fecha Ingreso</span><div class="value">${formatDate(servicio.fechaIngreso)}</div></div>
                <div class="detail-item"><span class="label">Total</span><div class="value" style="color:var(--success);font-size:18px">${formatMoney(servicio.totalServicio)}</div></div>
            </div>

            ${servicio.diagnostico ? `
                <div class="detail-item" style="margin-bottom:24px">
                    <span class="label">Diagnóstico</span>
                    <div class="value">${servicio.diagnostico}</div>
                </div>
            ` : ''}

            <div class="table-container">
                <div class="table-toolbar">
                    <h3 style="font-size:15px;font-weight:600">Detalle de Servicios</h3>
                    <button class="btn btn-accent btn-sm" onclick="openAddDetalle(${id})">
                        <span class="material-icons-round">add</span> Agregar
                    </button>
                </div>
                <div id="detalles-table">
                    ${detList.length ? `
                        <table>
                            <thead><tr><th>ID</th><th>Tipo de Servicio</th><th>Precio</th><th>Acciones</th></tr></thead>
                            <tbody>
                                ${detList.map(d => `
                                    <tr>
                                        <td>#${d.idDetalleServicio}</td>
                                        <td>${tiposMap[d.idTipoServicio] || 'Tipo ' + d.idTipoServicio}</td>
                                        <td style="font-weight:600;color:var(--success)">${formatMoney(d.precioServicio)}</td>
                                        <td class="actions-cell">
                                            <button class="btn-icon danger" onclick="eliminarDetalle(${d.idDetalleServicio}, ${id})" title="Eliminar">
                                                <span class="material-icons-round">delete</span>
                                            </button>
                                        </td>
                                    </tr>
                                `).join('')}
                            </tbody>
                        </table>
                    ` : showEmpty('list', 'No hay detalles agregados')}
                </div>
            </div>
        </div>`;
}

async function openAddDetalle(idServicio) {
    await loadCatalogos();
    openModal('Agregar Detalle de Servicio', `
        <form id="detalle-form">
            <div class="form-group">
                <label>Tipo de Servicio</label>
                <select id="df-tipo" required>
                    <option value="">— Seleccionar —</option>
                    ${App.cache.tiposServicio.map(t => `<option value="${t.idTipoServicio}">${t.nombre}</option>`).join('')}
                </select>
            </div>
            <div class="form-group">
                <label>Precio (RD$)</label>
                <input type="number" id="df-precio" step="0.01" min="0" required placeholder="0.00">
            </div>
            <div class="modal-actions">
                <button type="button" class="btn btn-outline" onclick="closeModal()">Cancelar</button>
                <button type="submit" class="btn btn-accent">Agregar</button>
            </div>
        </form>
    `);

    document.getElementById('detalle-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        const data = {
            idServicio: idServicio,
            idTipoServicio: parseInt(document.getElementById('df-tipo').value),
            precioServicio: parseFloat(document.getElementById('df-precio').value),
        };
        const res = await Api.createDetalle(data);
        if (res && !res.error) {
            showToast('Detalle agregado', 'success');
            closeModal(); viewServicioDetail(idServicio);
        } else showToast(res.error || 'Error', 'error');
    });
}

async function eliminarDetalle(idDetalle, idServicio) {
    if (!confirm('¿Eliminar este detalle?')) return;
    const res = await Api.deleteDetalle(idDetalle, idServicio);
    if (res === true) { showToast('Detalle eliminado', 'success'); viewServicioDetail(idServicio); }
    else showToast(res.error || 'Error', 'error');
}
