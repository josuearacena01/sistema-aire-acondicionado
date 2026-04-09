using System.Net.Http.Json;

namespace CajaApp;

public partial class Form1 : Form
{
    private Button btnCargar;
    private DataGridView dgvProductos;
    private readonly HttpClient _httpClient;

    public Form1()
    {
        InitializeComponent();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
        };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost:7200")
        };

        btnCargar = new Button
        {
            Text = "Cargar Productos",
            Location = new System.Drawing.Point(10, 10),
            Size = new System.Drawing.Size(200, 40)
        };
        btnCargar.Click += BtnCargar_Click;
        Controls.Add(btnCargar);

        dgvProductos = new DataGridView
        {
            Location = new System.Drawing.Point(10, 60),
            Size = new System.Drawing.Size(760, 350),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ReadOnly = true
        };
        Controls.Add(dgvProductos);

        this.Text = "Caja - Productos";
        this.Size = new System.Drawing.Size(800, 450);
    }

    private async void BtnCargar_Click(object? sender, EventArgs e)
    {
        try
        {
            var productos = await _httpClient.GetFromJsonAsync<List<ProductoDto>>("/api/productosint");
            dgvProductos.DataSource = productos;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }
}

public class ProductoDto
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Estado { get; set; } = "";
    public string Categoria { get; set; } = "";
    public string EstadoStock { get; set; } = "";
}