using System.Net.Http.Json;

namespace KooliProjekt.WindowsForms;

public partial class Form1 : Form
{
    private static readonly HttpClient client = new HttpClient();

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        LoadTooted();
    }

    private void LoadTooted()
    {
        var url = "http://localhost:5086/api/Tooted/List?page=1&pageSize=100";
        var response = client.GetFromJsonAsync<OperationResult<PagedResult<Toode>>>(url).Result;
        dataGridView1.DataSource = response?.Value?.Results;
    }

    private void LoadKliendid()
    {
        var url = "http://localhost:5086/api/Kliendid/List?page=1&pageSize=100";
        var response = client.GetFromJsonAsync<OperationResult<PagedResult<Klient>>>(url).Result;
        dataGridView1.DataSource = response?.Value?.Results;
    }

    private void LoadTellimused()
    {
        var url = "http://localhost:5086/api/Tellimused/List?page=1&pageSize=100";
        var response = client.GetFromJsonAsync<OperationResult<PagedResult<Tellimus>>>(url).Result;
        dataGridView1.DataSource = response?.Value?.Results;
    }

    private void LoadArved()
    {
        var url = "http://localhost:5086/api/Arved/List?page=1&pageSize=100";
        var response = client.GetFromJsonAsync<OperationResult<PagedResult<Arve>>>(url).Result;
        dataGridView1.DataSource = response?.Value?.Results;
    }

    private void LoadTellimuseRead()
    {
        var url = "http://localhost:5086/api/TellimuseRead/List?page=1&pageSize=100";
        var response = client.GetFromJsonAsync<OperationResult<PagedResult<TellimuseRida>>>(url).Result;
        dataGridView1.DataSource = response?.Value?.Results;
    }

    private void btnTooted_Click(object sender, EventArgs e)
    {
        LoadTooted();
    }

    private void btnKliendid_Click(object sender, EventArgs e)
    {
        LoadKliendid();
    }

    private void btnTellimused_Click(object sender, EventArgs e)
    {
        LoadTellimused();
    }

    private void btnArved_Click(object sender, EventArgs e)
    {
        LoadArved();
    }

    private void btnTellimuseRead_Click(object sender, EventArgs e)
    {
        LoadTellimuseRead();
    }
}
