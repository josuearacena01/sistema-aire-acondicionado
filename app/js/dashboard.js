// ===== DASHBOARD PAGE =====
async function renderDashboard() {
    const content = document.getElementById('content');
    const today = new Date();
    const firstDay = new Date(today.getFullYear(), today.getMonth(), 1);
    const fi = firstDay.toISOString().split('T')[0];
    const ff = today.toISOString().split('T')[0];

    content.innerHTML = `
        <div class="page-enter">
            <div class="page-header">
                <h1>Dashboard</h1>
                <div class="date-filter">
                    <input type="date" id="dash-fi" value="${fi}">
                    <span style="color:var(--text-muted)">—</span>
                    <input type="date" id="dash-ff" value="${ff}">
                    <button class="btn btn-accent btn-sm" id="dash-filter-btn">
                        <span class="material-icons-round">filter_list</span> Filtrar
                    </button>
                </div>
            </div>
            <div id="dash-content">${showLoading()}</div>
        </div>`;

    loadDashboardData(fi, ff);

    document.getElementById('dash-filter-btn').addEventListener('click', () => {
        const f1 = document.getElementById('dash-fi').value;
        const f2 = document.getElementById('dash-ff').value;
        if (f1 && f2) {
            document.getElementById('dash-content').innerHTML = showLoading();
            loadDashboardData(f1, f2);
        }
    });
}

async function loadDashboardData(fi, ff) {
    const data = await Api.getDashboard(fi, ff);
    const container = document.getElementById('dash-content');
    if (!container) return;

    if (data.error) {
        container.innerHTML = showEmpty('error', 'Error al cargar dashboard: ' + data.error);
        return;
    }

    const maxProd = data.productosMasVendidos?.length ? Math.max(...data.productosMasVendidos.map(p => p.cantidadVendida)) : 1;
    const maxServ = data.serviciosMasSolicitados?.length ? Math.max(...data.serviciosMasSolicitados.map(s => s.vecesSolicitado)) : 1;

    container.innerHTML = `
        <div class="stats-grid">
            <div class="stat-card">
                <span class="stat-label">Total Ventas</span>
                <div class="stat-value accent">${data.totalVentas}</div>
                <span class="material-icons-round stat-icon">shopping_cart</span>
            </div>
            <div class="stat-card">
                <span class="stat-label">Ingresos</span>
                <div class="stat-value success">${formatMoney(data.totalIngresos)}</div>
                <span class="material-icons-round stat-icon">payments</span>
            </div>
            <div class="stat-card">
                <span class="stat-label">Servicios</span>
                <div class="stat-value accent">${data.totalServicios}</div>
                <span class="material-icons-round stat-icon">build</span>
            </div>
            <div class="stat-card">
                <span class="stat-label">Productos Vendidos</span>
                <div class="stat-value warning">${data.productosVendidos}</div>
                <span class="material-icons-round stat-icon">inventory_2</span>
            </div>
        </div>

        <div class="charts-grid">
            <div class="chart-card">
                <h3>📦 Productos Más Vendidos</h3>
                <div class="chart-bar-list">
                    ${(data.productosMasVendidos || []).slice(0, 6).map(p => `
                        <div class="chart-bar-item">
                            <div class="chart-bar-label">
                                <span>${p.producto}</span>
                                <span>${p.cantidadVendida} uds — ${formatMoney(p.totalGenerado)}</span>
                            </div>
                            <div class="chart-bar-track">
                                <div class="chart-bar-fill" style="width:${(p.cantidadVendida / maxProd * 100)}%"></div>
                            </div>
                        </div>
                    `).join('') || '<p style="color:var(--text-muted);font-size:13px">Sin datos en este período</p>'}
                </div>
            </div>
            <div class="chart-card">
                <h3>🔧 Servicios Más Solicitados</h3>
                <div class="chart-bar-list">
                    ${(data.serviciosMasSolicitados || []).slice(0, 6).map(s => `
                        <div class="chart-bar-item">
                            <div class="chart-bar-label">
                                <span>${s.servicio}</span>
                                <span>${s.vecesSolicitado} veces — ${formatMoney(s.totalGenerado)}</span>
                            </div>
                            <div class="chart-bar-track">
                                <div class="chart-bar-fill" style="width:${(s.vecesSolicitado / maxServ * 100)}%"></div>
                            </div>
                        </div>
                    `).join('') || '<p style="color:var(--text-muted);font-size:13px">Sin datos en este período</p>'}
                </div>
            </div>
        </div>`;
}
