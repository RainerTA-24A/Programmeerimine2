namespace KooliProjekt.WindowsForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.panel1 = new System.Windows.Forms.FlowLayoutPanel();
        this.btnTooted = new System.Windows.Forms.Button();
        this.btnKliendid = new System.Windows.Forms.Button();
        this.btnTellimused = new System.Windows.Forms.Button();
        this.btnArved = new System.Windows.Forms.Button();
        this.btnTellimuseRead = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.btnTooted);
        this.panel1.Controls.Add(this.btnKliendid);
        this.panel1.Controls.Add(this.btnTellimused);
        this.panel1.Controls.Add(this.btnArved);
        this.panel1.Controls.Add(this.btnTellimuseRead);
        this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel1.Location = new System.Drawing.Point(0, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(800, 40);
        this.panel1.TabIndex = 1;
        // 
        // btnTooted
        // 
        this.btnTooted.Location = new System.Drawing.Point(3, 3);
        this.btnTooted.Name = "btnTooted";
        this.btnTooted.Size = new System.Drawing.Size(100, 30);
        this.btnTooted.TabIndex = 0;
        this.btnTooted.Text = "Tooted";
        this.btnTooted.UseVisualStyleBackColor = true;
        this.btnTooted.Click += new System.EventHandler(this.BtnTooted_Click);
        // 
        // btnKliendid
        // 
        this.btnKliendid.Location = new System.Drawing.Point(109, 3);
        this.btnKliendid.Name = "btnKliendid";
        this.btnKliendid.Size = new System.Drawing.Size(100, 30);
        this.btnKliendid.TabIndex = 1;
        this.btnKliendid.Text = "Kliendid";
        this.btnKliendid.UseVisualStyleBackColor = true;
        this.btnKliendid.Click += new System.EventHandler(this.BtnKliendid_Click);
        // 
        // btnTellimused
        // 
        this.btnTellimused.Location = new System.Drawing.Point(215, 3);
        this.btnTellimused.Name = "btnTellimused";
        this.btnTellimused.Size = new System.Drawing.Size(100, 30);
        this.btnTellimused.TabIndex = 2;
        this.btnTellimused.Text = "Tellimused";
        this.btnTellimused.UseVisualStyleBackColor = true;
        this.btnTellimused.Click += new System.EventHandler(this.BtnTellimused_Click);
        // 
        // btnArved
        // 
        this.btnArved.Location = new System.Drawing.Point(321, 3);
        this.btnArved.Name = "btnArved";
        this.btnArved.Size = new System.Drawing.Size(100, 30);
        this.btnArved.TabIndex = 3;
        this.btnArved.Text = "Arved";
        this.btnArved.UseVisualStyleBackColor = true;
        this.btnArved.Click += new System.EventHandler(this.BtnArved_Click);
        // 
        // btnTellimuseRead
        // 
        this.btnTellimuseRead.Location = new System.Drawing.Point(427, 3);
        this.btnTellimuseRead.Name = "btnTellimuseRead";
        this.btnTellimuseRead.Size = new System.Drawing.Size(120, 30);
        this.btnTellimuseRead.TabIndex = 4;
        this.btnTellimuseRead.Text = "Tellimuse Read";
        this.btnTellimuseRead.UseVisualStyleBackColor = true;
        this.btnTellimuseRead.Click += new System.EventHandler(this.BtnTellimuseRead_Click);
        // 
        // dataGridView1
        // 
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dataGridView1.Location = new System.Drawing.Point(0, 40);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.Size = new System.Drawing.Size(800, 410);
        this.dataGridView1.TabIndex = 0;
        // 
        // Form1
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.dataGridView1);
        this.Controls.Add(this.panel1);
        this.Text = "KooliProjekt - Haldus";
        this.Load += new System.EventHandler(this.Form1_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.FlowLayoutPanel panel1;
    private System.Windows.Forms.Button btnTooted;
    private System.Windows.Forms.Button btnKliendid;
    private System.Windows.Forms.Button btnTellimused;
    private System.Windows.Forms.Button btnArved;
    private System.Windows.Forms.Button btnTellimuseRead;
}
