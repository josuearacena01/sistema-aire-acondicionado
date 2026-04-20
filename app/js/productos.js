// ===== PRODUCTOS PAGE =====
async function renderProductos() {
    const content = document.getElementById('content');
    content.innerHTML = `
        <div class="page-enter">
            <div class="page-header">
                <h1>Productos</h1>
                <div class="page-header-actions">
                    <button class="btn btn-accent" id="btn-new-producto">
                        <span class="material-icons-round">add</span> Nuevo Producto
                    </button>
                </div>
            </div>
            <div class="table-container">
                <div class="table-toolbar">
                    <div class="search-box">
                        <span class="material-icons-round">search</span>
                        <input type="text" id="search-productos" placeholder="Buscar por nombre o categoría...">
                    </div>
                </div>
                <div id="productos-table">${showLoading()}</div>
            </div>
        </div>`;

    const productos = await Api.getProductos();
    renderProductosTable(Array.isArray(productos) ? productos : []);

    document.getElementById('btn-new-producto').addEventListener('click', () => openProductoForm());
    document.getElementById('search-productos').addEventListener('input', (e) => {
        const q = e.target.value.toLowerCase();
        document.querySelectorAll('#productos-table tbody tr').forEach(r => {
            r.style.display = r.textContent.toLowerCase().includes(q) ? '' : 'none';
        });
    });
}

function renderProductosTable(productos) {
    const container = document.getElementById('productos-table');
    if (!productos.length) { container.innerHTML = showEmpty('inventory_2', 'No hay productos registrados'); return; }
    container.innerHTML = `
        <table>
            <thead><tr>
                <th>ID</th><th>Nombre</th><th>Categoría</th><th>Precio</th><th>Stock</th><th>Estado</th><th>Acciones</th>
            </tr></thead>
            <tbody>
                ${productos.map(p => `
                    <tr>
                        <td style="font-weight:600;color:var(--text-primary)">#${p.idProducto}</td>
                        <td>${p.nombre}</td>
                        <td><span class="badge badge-info">${p.categoria}</span></td>
                        <td style="font-weight:600;color:var(--success)">${formatMoney(p.precio)}</td>
                        <td>
                            <span style="font-weight:600;color:${p.stock <= 5 ? 'var(--danger)' : 'var(--text-primary)'}">${p.stock}</span>
                            ${p.stock <= 5 ? ' <span style="color:var(--danger);font-size:11px">⚠ Bajo</span>' : ''}
                        </td>
                        <td>${estadoBadge(p.estado)}</td>
                        <td class="actions-cell">
                            <button class="btn-icon" onclick="openProductoForm(${p.idProducto})" title="Editar">
                                <span class="material-icons-round">edit</span>
                            </button>
                        </td>
                    </tr>
                `).join('')}
            </tbody>
        </table>`;
}

async function openProductoForm(id = null) {
    await loadCatalogos();
    let p = null;
    if (id) p = await Api.getProducto(id);
    const isEdit = !!p && !p.error;

    openModal(isEdit ? 'Editar Producto' : 'Nuevo Producto', `
        <form id="producto-form">
            <div class="form-group">
                <label>Categoría</label>
                <select id="pf-categoria" required>
                    <option value="">— Seleccionar —</option>
                    ${selectOptions(App.cache.categorias, 'idCategoria', 'nombre', isEdit ? p.idCategoria : null)}
                </select>
            </div>
            <div class="form-group">
                <label>Nombre</label>
                <input type="text" id="pf-nombre" value="${isEdit ? p.nombre : ''}" required minlength="2" maxlength="150">
            </div>
            <div class="form-row">
                <div class="form-group">
                    <label>Precio (RD$)</label>
                    <input type="number" id="pf-precio" step="0.01" min="0.01" value="${isEdit ? p.precio : ''}" required>
                </div>
                <div class="form-group">
                    <label>Stock</label>
                    <input type="number" id="pf-stock" min="0" value="${isEdit ? p.stock : ''}" required>
                </div>
            </div>
            <div class="modal-actions">
                <button type="button" class="btn btn-outline" onclick="closeModal()">Cancelar</button>
                <button type="submit" class="btn btn-accent">${isEdit ? 'Guardar' : 'Crear Producto'}</button>
            </div>
        </form>
    `);

    document.getElementById('producto-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        if (isEdit) {
            const data = {
                idCategoria: parseInt(document.getElementById('pf-categoria').value),
                nombre: document.getElementById('pf-nombre').value,
                precio: parseFloat(document.getElementById('pf-precio').value),
                stock: parseInt(document.getElementById('pf-stock').value),
            };
            const res = await Api.updateProducto(id, data);
            if (res === true) { showToast('Producto actualizado', 'success'); closeModal(); renderProductos(); }
            else showToast(res.error || 'Error', 'error');
        } else {
            const data = {
                idCategoria: parseInt(document.getElementById('pf-categoria').value),
                nombre: document.getElementById('pf-nombre').value,
                precio: parseFloat(document.getElementById('pf-precio').value),
                stock: parseInt(document.getElementById('pf-stock').value),
            };
            const res = await Api.createProducto(data);
            if (res && !res.error) { showToast('Producto creado', 'success'); closeModal(); renderProductos(); }
            else showToast(res.error || 'Error', 'error');
        }
    });
}
