// ===== VEHICULOS PAGE =====
async function renderVehiculos() {
    const content = document.getElementById('content');
    content.innerHTML = `
        <div class="page-enter">
            <div class="page-header">
                <h1>Vehículos</h1>
                <div class="page-header-actions">
                    <button class="btn btn-accent" id="btn-new-vehiculo">
                        <span class="material-icons-round">add</span> Nuevo Vehículo
                    </button>
                </div>
            </div>
            <div class="table-container">
                <div class="table-toolbar">
                    <div class="search-box">
                        <span class="material-icons-round">search</span>
                        <input type="text" id="search-vehiculos" placeholder="Buscar por marca o modelo...">
                    </div>
                </div>
                <div id="vehiculos-table">${showLoading()}</div>
            </div>
        </div>`;

    await loadCatalogos();
    const vehiculos = await Api.getVehiculos();
    renderVehiculosTable(Array.isArray(vehiculos) ? vehiculos : []);

    document.getElementById('btn-new-vehiculo').addEventListener('click', () => openVehiculoForm());
    document.getElementById('search-vehiculos').addEventListener('input', (e) => {
        const q = e.target.value.toLowerCase();
        const rows = document.querySelectorAll('#vehiculos-table tbody tr');
        rows.forEach(r => { r.style.display = r.textContent.toLowerCase().includes(q) ? '' : 'none'; });
    });
}

function renderVehiculosTable(vehiculos) {
    const container = document.getElementById('vehiculos-table');
    if (!vehiculos.length) { container.innerHTML = showEmpty('directions_car', 'No hay vehículos registrados'); return; }

    const marcasMap = {};
    (App.cache.marcas || []).forEach(m => marcasMap[m.idMarca] = m.nombre);

    container.innerHTML = `
        <table>
            <thead><tr>
                <th>ID</th><th>Marca</th><th>Modelo</th><th>Año</th><th>Acciones</th>
            </tr></thead>
            <tbody>
                ${vehiculos.map(v => `
                    <tr>
                        <td style="font-weight:600;color:var(--text-primary)">#${v.idVehiculo}</td>
                        <td>${marcasMap[v.idMarca] || `Marca ${v.idMarca}`}</td>
                        <td>${v.idModelo}</td>
                        <td>${v.anio}</td>
                        <td class="actions-cell">
                            <button class="btn-icon" onclick="openVehiculoForm(${v.idVehiculo})" title="Editar">
                                <span class="material-icons-round">edit</span>
                            </button>
                        </td>
                    </tr>
                `).join('')}
            </tbody>
        </table>`;
}

async function openVehiculoForm(id = null) {
    await loadCatalogos();
    let v = null;
    if (id) v = await Api.getVehiculo(id);
    const isEdit = !!v && !v.error;

    openModal(isEdit ? 'Editar Vehículo' : 'Nuevo Vehículo', `
        <form id="vehiculo-form">
            <div class="form-group">
                <label>Marca</label>
                <select id="vf-marca" required>
                    <option value="">— Seleccionar —</option>
                    ${selectOptions(App.cache.marcas, 'idMarca', 'nombre', isEdit ? v.idMarca : null)}
                </select>
            </div>
            <div class="form-group">
                <label>Modelo</label>
                <select id="vf-modelo" required>
                    <option value="">— Seleccione marca primero —</option>
                </select>
            </div>
            <div class="form-group">
                <label>Año</label>
                <input type="number" id="vf-anio" min="1950" max="2100" value="${isEdit ? v.anio : new Date().getFullYear()}" required>
            </div>
            <div class="modal-actions">
                <button type="button" class="btn btn-outline" onclick="closeModal()">Cancelar</button>
                <button type="submit" class="btn btn-accent">${isEdit ? 'Guardar' : 'Crear Vehículo'}</button>
            </div>
        </form>
    `);

    // Load models when brand changes
    const marcaSelect = document.getElementById('vf-marca');
    const modeloSelect = document.getElementById('vf-modelo');

    async function loadModelos(idMarca, selectedModelo = null) {
        if (!idMarca) { modeloSelect.innerHTML = '<option value="">— Seleccione marca —</option>'; return; }
        const modelos = await Api.getModelos(idMarca);
        if (Array.isArray(modelos)) {
            modeloSelect.innerHTML = '<option value="">— Seleccionar —</option>' +
                selectOptions(modelos, 'idModelo', 'nombre', selectedModelo);
        }
    }

    marcaSelect.addEventListener('change', () => loadModelos(marcaSelect.value));
    if (isEdit && v.idMarca) loadModelos(v.idMarca, v.idModelo);

    document.getElementById('vehiculo-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        const data = {
            idMarca: parseInt(marcaSelect.value),
            idModelo: parseInt(modeloSelect.value),
            anio: parseInt(document.getElementById('vf-anio').value),
        };
        let res;
        if (isEdit) res = await Api.updateVehiculo(id, data);
        else res = await Api.createVehiculo(data);

        if (res === true || (res && !res.error)) {
            showToast(isEdit ? 'Vehículo actualizado' : 'Vehículo creado', 'success');
            closeModal(); renderVehiculos();
        } else showToast(res.error || 'Error', 'error');
    });
}
