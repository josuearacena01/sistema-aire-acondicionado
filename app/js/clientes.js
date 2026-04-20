// ===== CLIENTES PAGE =====
async function renderClientes() {
    const content = document.getElementById('content');
    content.innerHTML = `
        <div class="page-enter">
            <div class="page-header">
                <h1>Clientes</h1>
                <div class="page-header-actions">
                    <button class="btn btn-accent" id="btn-new-cliente">
                        <span class="material-icons-round">person_add</span> Nuevo Cliente
                    </button>
                </div>
            </div>
            <div class="table-container">
                <div class="table-toolbar">
                    <div class="search-box">
                        <span class="material-icons-round">search</span>
                        <input type="text" id="search-clientes" placeholder="Buscar por nombre o cédula...">
                    </div>
                </div>
                <div id="clientes-table">${showLoading()}</div>
            </div>
        </div>`;

    const clientes = await Api.getClientes();
    renderClientesTable(Array.isArray(clientes) ? clientes : []);

    document.getElementById('btn-new-cliente').addEventListener('click', () => openClienteForm());
    document.getElementById('search-clientes').addEventListener('input', (e) => {
        const q = e.target.value.toLowerCase();
        const rows = document.querySelectorAll('#clientes-table tbody tr');
        rows.forEach(r => { r.style.display = r.textContent.toLowerCase().includes(q) ? '' : 'none'; });
    });
}

function renderClientesTable(clientes) {
    const container = document.getElementById('clientes-table');
    if (!clientes.length) { container.innerHTML = showEmpty('people', 'No hay clientes registrados'); return; }
    container.innerHTML = `
        <table>
            <thead><tr>
                <th>Cédula</th><th>Nombre</th><th>Teléfono</th><th>Correo</th><th>Registro</th><th>Acciones</th>
            </tr></thead>
            <tbody>
                ${clientes.map(c => `
                    <tr>
                        <td style="font-weight:600;color:var(--text-primary)">${c.cedula}</td>
                        <td>${c.nombres} ${c.apellidos}</td>
                        <td>${c.telefono || '—'}</td>
                        <td>${c.correo || '—'}</td>
                        <td>${formatDate(c.fechaRegistro)}</td>
                        <td class="actions-cell">
                            <button class="btn-icon" onclick="openClienteForm(${c.idCliente})" title="Editar">
                                <span class="material-icons-round">edit</span>
                            </button>
                        </td>
                    </tr>
                `).join('')}
            </tbody>
        </table>`;
}

async function openClienteForm(id = null) {
    await loadCatalogos();
    let c = null;
    if (id) c = await Api.getCliente(id);

    const isEdit = !!c && !c.error;
    const title = isEdit ? 'Editar Cliente' : 'Nuevo Cliente';

    openModal(title, `
        <form id="cliente-form">
            <div class="form-row">
                <div class="form-group">
                    <label>Cédula</label>
                    <input type="text" id="cf-cedula" value="${isEdit ? c.cedula : ''}" maxlength="11" 
                        placeholder="00000000000" ${isEdit ? 'readonly style="opacity:0.6"' : 'required'}>
                </div>
                <div class="form-group">
                    <label>Provincia</label>
                    <select id="cf-provincia">
                        <option value="">— Seleccionar —</option>
                        ${selectOptions(App.cache.provincias, 'idProvincia', 'nombre', isEdit ? c.idProvincia : null)}
                    </select>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group">
                    <label>Nombres</label>
                    <input type="text" id="cf-nombres" value="${isEdit ? c.nombres : ''}" required>
                </div>
                <div class="form-group">
                    <label>Apellidos</label>
                    <input type="text" id="cf-apellidos" value="${isEdit ? c.apellidos : ''}" required>
                </div>
            </div>
            <div class="form-row">
                <div class="form-group">
                    <label>Teléfono</label>
                    <input type="text" id="cf-telefono" value="${isEdit ? (c.telefono || '') : ''}" maxlength="10" placeholder="8091234567">
                </div>
                <div class="form-group">
                    <label>Correo</label>
                    <input type="email" id="cf-correo" value="${isEdit ? (c.correo || '') : ''}">
                </div>
            </div>
            <div class="form-row">
                <div class="form-group">
                    <label>Ciudad</label>
                    <input type="text" id="cf-ciudad" value="${isEdit ? (c.ciudad || '') : ''}">
                </div>
                <div class="form-group">
                    <label>Sector</label>
                    <input type="text" id="cf-sector" value="${isEdit ? (c.sector || '') : ''}">
                </div>
            </div>
            <div class="form-group">
                <label>Calle</label>
                <input type="text" id="cf-calle" value="${isEdit ? (c.calle || '') : ''}">
            </div>
            ${!isEdit ? `
                <div class="form-row">
                    <div class="form-group">
                        <label>Username</label>
                        <input type="text" id="cf-username" required minlength="4">
                    </div>
                    <div class="form-group">
                        <label>Contraseña</label>
                        <input type="password" id="cf-password" required minlength="6">
                    </div>
                </div>
            ` : ''}
            <div class="modal-actions">
                <button type="button" class="btn btn-outline" onclick="closeModal()">Cancelar</button>
                <button type="submit" class="btn btn-accent">${isEdit ? 'Guardar Cambios' : 'Crear Cliente'}</button>
            </div>
        </form>
    `);

    document.getElementById('cliente-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        const prov = document.getElementById('cf-provincia').value;
        if (isEdit) {
            const data = {
                idProvincia: prov ? parseInt(prov) : null,
                nombres: document.getElementById('cf-nombres').value,
                apellidos: document.getElementById('cf-apellidos').value,
                telefono: document.getElementById('cf-telefono').value || null,
                correo: document.getElementById('cf-correo').value || null,
                ciudad: document.getElementById('cf-ciudad').value || null,
                sector: document.getElementById('cf-sector').value || null,
                calle: document.getElementById('cf-calle').value || null,
            };
            const res = await Api.updateCliente(id, data);
            if (res === true) { showToast('Cliente actualizado', 'success'); closeModal(); renderClientes(); }
            else showToast(res.error || 'Error', 'error');
        } else {
            const data = {
                cedula: document.getElementById('cf-cedula').value,
                idProvincia: prov ? parseInt(prov) : null,
                nombres: document.getElementById('cf-nombres').value,
                apellidos: document.getElementById('cf-apellidos').value,
                telefono: document.getElementById('cf-telefono').value || null,
                correo: document.getElementById('cf-correo').value || null,
                ciudad: document.getElementById('cf-ciudad').value || null,
                sector: document.getElementById('cf-sector').value || null,
                calle: document.getElementById('cf-calle').value || null,
                username: document.getElementById('cf-username').value,
                password: document.getElementById('cf-password').value,
            };
            const res = await Api.createCliente(data);
            if (res && !res.error) { showToast('Cliente creado', 'success'); closeModal(); renderClientes(); }
            else showToast(res.error || 'Error', 'error');
        }
    });
}
