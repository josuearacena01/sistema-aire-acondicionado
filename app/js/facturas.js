// ===== FACTURAS PAGE =====
async function renderFacturas() {
    const content = document.getElementById('content');
    content.innerHTML = `
        <div class="page-enter">
            <div class="page-header">
                <h1>Facturas</h1>
            </div>
            <div class="table-container">
                <div class="table-toolbar">
                    <div class="search-box">
                        <span class="material-icons-round">search</span>
                        <input type="text" id="search-facturas" placeholder="Buscar por cliente o estado...">
                    </div>
                </div>
                <div id="facturas-table">${showLoading()}</div>
            </div>
        </div>`;

    const facturas = await Api.getFacturas();
    renderFacturasTable(Array.isArray(facturas) ? facturas : []);

    document.getElementById('search-facturas').addEventListener('input', (e) => {
        const q = e.target.value.toLowerCase();
        document.querySelectorAll('#facturas-table tbody tr').forEach(r => {
            r.style.display = r.textContent.toLowerCase().includes(q) ? '' : 'none';
        });
    });
}

function renderFacturasTable(facturas) {
    const container = document.getElementById('facturas-table');
    if (!facturas.length) { container.innerHTML = showEmpty('receipt_long', 'No hay facturas registradas'); return; }
    container.innerHTML = `
        <table>
            <thead><tr>
                <th>ID</th><th>Fecha</th><th>Cliente</th><th>Cédula</th><th>Total</th><th>Estado</th><th>Acciones</th>
            </tr></thead>
            <tbody>
                ${facturas.map(f => `
                    <tr>
                        <td style="font-weight:600;color:var(--text-primary)">#${f.idFactura}</td>
                        <td>${formatDate(f.fechaFactura)}</td>
                        <td>${f.cliente}</td>
                        <td>${f.cedula}</td>
                        <td style="font-weight:600;color:var(--success)">${formatMoney(f.totalFactura)}</td>
                        <td>${estadoBadge(f.estadoFactura)}</td>
                        <td class="actions-cell">
                            ${!f.estadoFactura?.toLowerCase().includes('anulada') ? `
                                <button class="btn-icon danger" onclick="anularFactura(${f.idFactura})" title="Anular factura">
                                    <span class="material-icons-round">block</span>
                                </button>
                            ` : ''}
                        </td>
                    </tr>
                `).join('')}
            </tbody>
        </table>`;
}

async function anularFactura(id) {
    if (!confirm('¿Está seguro de que desea anular esta factura? Esta acción no se puede deshacer.')) return;
    const res = await Api.anularFactura(id);
    if (res === true) { showToast('Factura anulada', 'success'); renderFacturas(); }
    else showToast(res.error || 'Error al anular', 'error');
}
