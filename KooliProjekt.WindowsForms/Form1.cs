using System.Net.Http.Json;
using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms;

public partial class Form1 : Form, IMainView
{
    private MainViewPresenter? _presenter;

    public int CurrentId
    {
        get { return idField?.Text != null && int.TryParse(idField.Text, out int id) ? id : -1; }
        set { if (idField != null) idField.Text = value.ToString(); }
    }

    public string CurrentTitle
    {
        get { return titleField?.Text ?? string.Empty; }
        set { if (titleField != null) titleField.Text = value; }
    }

    public IList<Toode>? DataSource
    {
        get { return dataGridView1.DataSource as IList<Toode>; }
        set { dataGridView1.DataSource = value; }
    }

    public Toode? SelectedItem
    {
        get { return dataGridView1.CurrentRow?.DataBoundItem as Toode; }
        set { }
    }

    public void SetPresenter(MainViewPresenter presenter)
    {
        _presenter = presenter;
    }

    private static readonly HttpClient client = new HttpClient();
    private readonly IApiClient _apiClient;

    public Form1(IApiClient apiClient)
    {
        InitializeComponent();
        _apiClient = apiClient;
        InitializeCustomControls();
    }

    private TextBox idField = null!;
    private TextBox titleField = null!;
    private Button saveCommand = null!;
    private Button addCommand = null!;
    private Button deleteCommand = null!;

    private void InitializeCustomControls()
    {
        var panel2 = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 40 };
        
        panel2.Controls.Add(new Label { Text = "ID:", Width = 30, TextAlign = ContentAlignment.MiddleRight });
        idField = new TextBox { Width = 50, ReadOnly = true, Text = "0" };
        panel2.Controls.Add(idField);
        
        panel2.Controls.Add(new Label { Text = "Name:", Width = 50, TextAlign = ContentAlignment.MiddleRight });
        titleField = new TextBox { Width = 100 };
        panel2.Controls.Add(titleField);

        saveCommand = new Button { Text = "Salvesta" };
        saveCommand.Click += async (s, e) => await SaveToodeAsync();
        panel2.Controls.Add(saveCommand);

        addCommand = new Button { Text = "Lisa uus" };
        addCommand.Click += (s, e) => AddNewToode();
        panel2.Controls.Add(addCommand);

        deleteCommand = new Button { Text = "Kustuta" };
        deleteCommand.Click += async (s, e) => await DeleteToodeAsync();
        panel2.Controls.Add(deleteCommand);

        Controls.Add(panel2);
        
        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
    }

    private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
    {
        if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem is Toode toode)
        {
            _presenter?.SetSelection(toode);
        }
        else
        {
            _presenter?.SetSelection(null);
        }
    }

    private void AddNewToode()
    {
        idField.Text = "0";
        titleField.Text = "";
    }

    private async Task SaveToodeAsync()
    {
        var toode = new Toode
        {
            Id = int.Parse(idField.Text),
            Name = titleField.Text,
            Price = 10,
            StockQuantity = 10
        };

        var result = await _apiClient.SaveToode(toode);
        if (result.HasErrors)
        {
            ShowError("Viga salvestamisel", result);
            return;
        }

        await LoadTootedAsync();
        AddNewToode();
    }

    private async Task DeleteToodeAsync()
    {
        if (int.TryParse(idField.Text, out int id) && id > 0)
        {
            var result = await _apiClient.DeleteToode(id);
            if (result.HasErrors)
            {
                ShowError("Viga kustutamisel", result);
                return;
            }
            await LoadTootedAsync();
            AddNewToode();
        }
    }

    public void ShowError(string message, OperationResult result)
    {
        var errors = string.Join("\n", result.Errors ?? new List<string>());
        if (result.PropertyErrors != null)
        {
            var propErrors = string.Join("\n", result.PropertyErrors.Select(pe => $"{pe.Key}: {pe.Value}"));
            errors += "\n" + propErrors;
        }
        MessageBox.Show($"{message}\n{errors}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private async void Form1_Load(object sender, EventArgs e)
    {
        await LoadTootedAsync();
    }

    private async Task LoadTootedAsync()
    {
        if (_presenter != null) 
        {
            await _presenter.LoadData();
        }
    }

    private void LoadKliendid()
    {
        try {
            var url = "http://localhost:5086/api/Kliendid/List?page=1&pageSize=100";
            var response = client.GetFromJsonAsync<OperationResult<PagedResult<Klient>>>(url).Result;
            dataGridView1.DataSource = response?.Value?.Results;
        } catch (Exception ex) {
            MessageBox.Show($"Viga: {ex.Message}");
        }
    }

    private void LoadTellimused()
    {
        try {
            var url = "http://localhost:5086/api/Tellimused/List?page=1&pageSize=100";
            var response = client.GetFromJsonAsync<OperationResult<PagedResult<Tellimus>>>(url).Result;
            dataGridView1.DataSource = response?.Value?.Results;
        } catch (Exception ex) {
            MessageBox.Show($"Viga: {ex.Message}");
        }
    }

    private void LoadArved()
    {
        try {
            var url = "http://localhost:5086/api/Arved/List?page=1&pageSize=100";
            var response = client.GetFromJsonAsync<OperationResult<PagedResult<Arve>>>(url).Result;
            dataGridView1.DataSource = response?.Value?.Results;
        } catch (Exception ex) {
            MessageBox.Show($"Viga: {ex.Message}");
        }
    }

    private void LoadTellimuseRead()
    {
        try {
            var url = "http://localhost:5086/api/TellimuseRead/List?page=1&pageSize=100";
            var response = client.GetFromJsonAsync<OperationResult<PagedResult<TellimuseRida>>>(url).Result;
            dataGridView1.DataSource = response?.Value?.Results;
        } catch (Exception ex) {
            MessageBox.Show($"Viga: {ex.Message}");
        }
    }

    private async void BtnTooted_Click(object sender, EventArgs e)
    {
        await LoadTootedAsync();
    }

    private void BtnKliendid_Click(object sender, EventArgs e)
    {
        LoadKliendid();
    }

    private void BtnTellimused_Click(object sender, EventArgs e)
    {
        LoadTellimused();
    }

    private void BtnArved_Click(object sender, EventArgs e)
    {
        LoadArved();
    }

    private void BtnTellimuseRead_Click(object sender, EventArgs e)
    {
        LoadTellimuseRead();
    }
}
