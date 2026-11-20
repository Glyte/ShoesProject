using ShoesProject.Properties;

namespace ShoesProject
{
    partial class FormProducts
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnHeader = new Panel();
            pbLogo = new PictureBox();
            lblTitle = new Label();
            lblUserName = new Label();
            btnLogout = new Button();
            pnFilters = new Panel();
            txtSearch = new TextBox();
            cmbSort = new ComboBox();
            cmbCategory = new ComboBox();
            dgvProducts = new DataGridView();
            pnFooter = new Panel();
            btnAddToOrder = new Button();
            btnOrders = new Button();
            lblCartCount = new Label();

            pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            pnFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            pnFooter.SuspendLayout();
            SuspendLayout();

            // pnHeader (#D2F6E7)
            pnHeader.BackColor = ColorTranslator.FromHtml("#D2F6E7");
            pnHeader.Dock = DockStyle.Top;
            pnHeader.Height = 70;
            pnHeader.Controls.Add(pbLogo);
            pnHeader.Controls.Add(lblTitle);
            pnHeader.Controls.Add(lblUserName);
            pnHeader.Controls.Add(btnLogout);

            pbLogo.Image = Resources.logo;
            pbLogo.Location = new Point(15, 5);
            pbLogo.Size = new Size(60, 60);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Calibri", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(85, 22);
            lblTitle.Text = "Чудо Обувь — Каталог товаров";

            lblUserName.AutoSize = true;
            lblUserName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserName.Location = new Point(620, 25);
            lblUserName.Text = "Гость";

            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Location = new Point(860, 18);
            btnLogout.Size = new Size(110, 34);
            btnLogout.Text = "Выход";
            btnLogout.Click += BtnLogout_Click;

            // pnFilters
            pnFilters.BackColor = Color.White;
            pnFilters.Dock = DockStyle.Top;
            pnFilters.Height = 50;
            pnFilters.Controls.Add(txtSearch);
            pnFilters.Controls.Add(cmbSort);
            pnFilters.Controls.Add(cmbCategory);

            txtSearch.Location = new Point(15, 12);
            txtSearch.PlaceholderText = "Поиск по описанию или артикулу...";
            txtSearch.Size = new Size(300, 27);
            txtSearch.TextChanged += FilterChanged;

            cmbSort.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSort.Location = new Point(330, 12);
            cmbSort.Size = new Size(220, 27);
            cmbSort.Items.AddRange(new object[] { "Без сортировки", "Цена: по возрастанию", "Цена: по убыванию" });
            cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += FilterChanged;

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Location = new Point(565, 12);
            cmbCategory.Size = new Size(220, 27);
            cmbCategory.SelectedIndexChanged += FilterChanged;

            // dgvProducts
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.ColumnHeadersVisible = false;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // pnFooter (#D2F6E7)
            pnFooter.BackColor = ColorTranslator.FromHtml("#D2F6E7");
            pnFooter.Dock = DockStyle.Bottom;
            pnFooter.Height = 60;
            pnFooter.Controls.Add(btnAddToOrder);
            pnFooter.Controls.Add(lblCartCount);
            pnFooter.Controls.Add(btnOrders);

            btnAddToOrder.BackColor = ColorTranslator.FromHtml("#70B2AF");
            btnAddToOrder.FlatAppearance.BorderSize = 0;
            btnAddToOrder.FlatStyle = FlatStyle.Flat;
            btnAddToOrder.ForeColor = Color.White;
            btnAddToOrder.Font = new Font("Calibri", 12F, FontStyle.Bold);
            btnAddToOrder.Location = new Point(15, 12);
            btnAddToOrder.Size = new Size(210, 36);
            btnAddToOrder.Text = "Добавить в заказ";
            btnAddToOrder.Click += BtnAddToOrder_Click;

            lblCartCount.AutoSize = true;
            lblCartCount.Location = new Point(240, 20);
            lblCartCount.Text = "В текущем заказе: 0 шт.";

            btnOrders.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOrders.BackColor = ColorTranslator.FromHtml("#70B2AF");
            btnOrders.FlatAppearance.BorderSize = 0;
            btnOrders.FlatStyle = FlatStyle.Flat;
            btnOrders.ForeColor = Color.White;
            btnOrders.Font = new Font("Calibri", 12F, FontStyle.Bold);
            btnOrders.Location = new Point(760, 12);
            btnOrders.Size = new Size(210, 36);
            btnOrders.Text = "Заказы / Оформление";
            btnOrders.Click += BtnOrders_Click;

            // FormProducts
            BackColor = Color.White;
            ClientSize = new Size(984, 661);
            Controls.Add(dgvProducts);
            Controls.Add(pnFilters);
            Controls.Add(pnHeader);
            Controls.Add(pnFooter);
            Font = new Font("Calibri", 12F, FontStyle.Regular);
            Name = "FormProducts";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Чудо Обувь — Каталог товаров";

            pnHeader.ResumeLayout(false);
            pnHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            pnFilters.ResumeLayout(false);
            pnFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            pnFooter.ResumeLayout(false);
            pnFooter.PerformLayout();
            ResumeLayout(false);
        }

        private Panel pnHeader, pnFilters, pnFooter;
        private PictureBox pbLogo;
        private Label lblTitle, lblUserName, lblCartCount;
        private Button btnLogout, btnAddToOrder, btnOrders;
        private TextBox txtSearch;
        private ComboBox cmbSort, cmbCategory;
        private DataGridView dgvProducts;
    }
}
