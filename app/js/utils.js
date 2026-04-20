// ===== UTILITIES =====
const App = {
    user: null,
    currentPage: 'dashboard',
    cache: {},
};

function showToast(message, type = 'info') {
    const container = document.getElementById('toast-container');
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    const icons = { success: 'check_circle', error: 'error', info: 'info' };
    toast.innerHTML = `<span class="material-icons-round">${icons[type]}</span>${message}`;
    container.appendChild(toast);
    setTimeout(() => { toast.style.opacity = '0'; toast.style.transform = 'translateX(30px)'; setTimeout(() => toast.remove(), 300); }, 3500);
}

function openModal(title, bodyHtml) {
    document.getElementById('modal-title').textContent = title;
    document.getElementById('modal-body').innerHTML = bodyHtml;
    document.getElementById('modal-overlay').style.display = 'flex';
}

function closeModal() {
    document.getElementById('modal-overlay').style.display = 'none';
}

function showLoading() {
    return '<div class="loading-spinner"><div class="spinner"></div></div>';
}

function showEmpty(icon, msg) {
    return `<div class="empty-state"><span class="material-icons-round">${icon}</span><p>${msg}</p></div>`;
}

function formatDate(d) {
    if (!d) return '—';
    return new Date(d).toLocaleDateString('es-DO', { day: '2-digit', month: '2-digit', year: 'numeric' });
}

function formatMoney(n) {
    if (n == null) return 'RD$0.00';
    return 'RD$' + Number(n).toLocaleString('es-DO', { minimumFractionDigits: 2 });
}

function estadoBadge(estado) {
    if (!estado) return '';
    const s = estado.toLowerCase();
    let cls = 'badge-info';
    if (s.includes('pendiente')) cls = 'badge-pending';
    else if (s.includes('proceso') || s.includes('progreso')) cls = 'badge-progress';
    else if (s.includes('completado') || s.includes('finalizado') || s.includes('pagada')) cls = 'badge-completed';
    else if (s.includes('cancelado') || s.includes('anulada')) cls = 'badge-cancelled';
    else if (s.includes('activo')) cls = 'badge-active';
    return `<span class="badge ${cls}">${estado}</span>`;
}

async function loadCatalogos() {
    if (App.cache.loaded) return;
    const [provincias, marcas, categorias, tiposServ, estadosServ] = await Promise.all([
        Api.getProvincias(), Api.getMarcas(), Api.getCategorias(),
        Api.getTiposServicio(), Api.getEstadosServicio()
    ]);
    App.cache.provincias = Array.isArray(provincias) ? provincias : [];
    App.cache.marcas = Array.isArray(marcas) ? marcas : [];
    App.cache.categorias = Array.isArray(categorias) ? categorias : [];
    App.cache.tiposServicio = Array.isArray(tiposServ) ? tiposServ : [];
    App.cache.estadosServicio = Array.isArray(estadosServ) ? estadosServ : [];
    App.cache.loaded = true;
}

function selectOptions(list, valKey, textKey, selected = null) {
    return list.map(i => `<option value="${i[valKey]}" ${i[valKey] == selected ? 'selected' : ''}>${i[textKey]}</option>`).join('');
}

// Modal close handlers
document.getElementById('modal-close').addEventListener('click', closeModal);
document.getElementById('modal-overlay').addEventListener('click', (e) => {
    if (e.target === e.currentTarget) closeModal();
});
