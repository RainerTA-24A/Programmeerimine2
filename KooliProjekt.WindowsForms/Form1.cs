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

    public string CurrentFotoUrl
    {
        get { return fotoUrlField?.Text ?? string.Empty; }
        set { if (fotoUrlField != null) fotoUrlField.Text = value; }
    }

    public decimal CurrentPrice
    {
        get { return decimal.TryParse(priceField?.Text, out decimal price) ? price : 0; }
        set { if (priceField != null) priceField.Text = value.ToString("0.00"); }
    }

    public decimal CurrentStockQuantity
    {
        get { return decimal.TryParse(stockQuantityField?.Text, out decimal result) ? result : 0; }
        set { if (stockQuantityField != null) stockQuantityField.Text = value.ToString("0.00"); }
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
    private TextBox fotoUrlField = null!;
    private TextBox priceField = null!;
    private TextBox stockQuantityField = null!;
    private Button saveCommand = null!;
    private Button addCommand = null!;
    private Button deleteCommand = null!;

    private void InitializeCustomControls()
    {
        var panel2 = new FlowLayoutPanel { 
            Dock = DockStyle.Right, 
            Width = 220,
            FlowDirection = FlowDirection.TopDown,
            Padding = new Padding(10) 
        };

        panel2.Controls.Add(new Label { Text = "ID:", AutoSize = true, Margin = new Padding(3, 5, 3, 0) });
        idField = new TextBox { Width = 180, ReadOnly = true, Text = "0" };
        panel2.Controls.Add(idField);

        panel2.Controls.Add(new Label { Text = "Name:", AutoSize = true, Margin = new Padding(3, 7, 3, 0) });
        titleField = new TextBox { Width = 180 };
        panel2.Controls.Add(titleField);

        panel2.Controls.Add(new Label { Text = "URL:", AutoSize = true, Margin = new Padding(3, 7, 3, 0) });
        fotoUrlField = new TextBox { Width = 180 };
        panel2.Controls.Add(fotoUrlField);

        panel2.Controls.Add(new Label { Text = "Price:", AutoSize = true, Margin = new Padding(3, 7, 3, 0) });
        priceField = new TextBox { Width = 180 };
        panel2.Controls.Add(priceField);

        panel2.Controls.Add(new Label { Text = "Stock:", AutoSize = true, Margin = new Padding(3, 7, 3, 0) });
        stockQuantityField = new TextBox { Width = 180 };
        panel2.Controls.Add(stockQuantityField);

        saveCommand = new Button { Text = "Salvesta", AutoSize = true, MinimumSize = new Size(180, 0), Margin = new Padding(3, 20, 3, 3) };
        saveCommand.Click += async (s, e) => { if (_presenter != null) await _presenter.Save(); };
        panel2.Controls.Add(saveCommand);

        addCommand = new Button { Text = "Lisa uus", AutoSize = true, MinimumSize = new Size(180, 0), Margin = new Padding(3, 5, 3, 3) };
        addCommand.Click += (s, e) => AddNewToode();
        panel2.Controls.Add(addCommand);

        deleteCommand = new Button { Text = "Kustuta", AutoSize = true, MinimumSize = new Size(180, 0), Margin = new Padding(3, 5, 3, 3) };
        deleteCommand.Click += async (s, e) => { if (_presenter != null) await _presenter.Delete(); };
        panel2.Controls.Add(deleteCommand);

        Controls.Add(panel2);
        panel2.BringToFront();

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
        if (_presenter != null)
        {
            _presenter.SetSelection(null);
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

    public bool ConfirmDelete()
    {
        var result = MessageBox.Show(
            "Kas olete kindel, et soovite kirje kustutada?",
            "Kustutamine",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );
        return result == DialogResult.Yes;
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
