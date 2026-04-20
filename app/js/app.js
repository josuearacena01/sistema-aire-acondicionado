// ===== MAIN APP - ROUTER, LOGIN, INIT =====

// Page router
const pages = {
    dashboard: renderDashboard,
    clientes: renderClientes,
    vehiculos: renderVehiculos,
    servicios: renderServicios,
    productos: renderProductos,
    facturas: renderFacturas,
};

function navigateTo(page) {
    if (!pages[page]) return;
    App.currentPage = page;

    // Update active nav link
    document.querySelectorAll('.nav-link').forEach(link => {
        link.classList.toggle('active', link.dataset.page === page);
    });

    // Render page
    pages[page]();
}

// Nav link clicks
document.querySelectorAll('.nav-link').forEach(link => {
    link.addEventListener('click', (e) => {
        e.preventDefault();
        navigateTo(link.dataset.page);
    });
});

// Login
document.getElementById('login-form').addEventListener('submit', async (e) => {
    e.preventDefault();
    const btn = document.getElementById('login-btn');
    const errorEl = document.getElementById('login-error');
    btn.disabled = true;
    btn.querySelector('.btn-text').textContent = 'Ingresando...';
    errorEl.style.display = 'none';

    const data = {
        username: document.getElementById('login-username').value,
        password: document.getElementById('login-password').value,
    };

    try {
        const res = await Api.login(data);
        if (res && res.id && !res.error) {
            App.user = res;
            document.getElementById('user-name').textContent = res.nombre;
            document.getElementById('user-role').textContent = res.rol;
            document.getElementById('login-screen').style.display = 'none';
            document.getElementById('app').style.display = 'flex';
            navigateTo('dashboard');
            showToast(`Bienvenido, ${res.nombre}`, 'success');
        } else {
            errorEl.textContent = res.error || 'Credenciales inválidas';
            errorEl.style.display = 'block';
        }
    } catch (err) {
        errorEl.textContent = 'No se pudo conectar con el servidor. Verifica que la API esté corriendo.';
        errorEl.style.display = 'block';
    }

    btn.disabled = false;
    btn.querySelector('.btn-text').textContent = 'Iniciar Sesión';
});

// Logout
document.getElementById('btn-logout').addEventListener('click', () => {
    if (!confirm('¿Cerrar sesión?')) return;
    App.user = null;
    App.cache = {};
    document.getElementById('app').style.display = 'none';
    document.getElementById('login-screen').style.display = 'flex';
    document.getElementById('login-username').value = '';
    document.getElementById('login-password').value = '';
});

// Keyboard shortcuts
document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') closeModal();
});
