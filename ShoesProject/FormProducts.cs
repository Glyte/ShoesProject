using Microsoft.EntityFrameworkCore;
using ShoesProject.Models;
using ShoesProject.Properties;

namespace ShoesProject
{
    public partial class FormProducts : Form
    {
        public User? CurrentUser { get; private set; }
        public bool IsGuest { get; private set; }

        // Корзина для оформления заказа (ID товара -> Количество)
        private Dictionary<int, int> _cart = new Dictionary<int, int>();

        public FormProducts(User? user, bool guest)
        {
            InitializeComponent();
            CurrentUser = user;
            IsGuest = guest;

            SetupGrid();
            SetupPermissions();
            LoadCategories();
            LoadProducts();
        }

        private void SetupGrid()
        {
            var colPhoto = new DataGridViewImageColumn
            {
                Name = "colPhoto",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                FillWeight = 25
            };

            var colInfo = new DataGridViewTextBoxColumn
            {
                Name = "colInfo",
                FillWeight = 65
            };
            colInfo.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            var colDiscount = new DataGridViewTextBoxColumn
            {
                Name = "colDiscount",
                FillWeight = 10
            };
            colDiscount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colPhoto, colInfo, colDiscount });
        }

        private void SetupPermissions()
        {
            if (IsGuest || CurrentUser == null)
            {
                lblUserName.Text = "Гость";
                // По ТЗ: неавторизованный пользователь просматривает без фильтрации и не оформляет заказы
                pnFilters.Visible = false;
                pnFooter.Visible = false;
            }
            else
            {
                lblUserName.Text = $"{CurrentUser.FullName} ({CurrentUser.Role.RoleName})";
                pnFilters.Visible = true;
                pnFooter.Visible = true;
            }
        }

        private void LoadCategories()
        {
            using (var db = new ShopDbContext())
            {
                var categories = db.Categories.Select(c => c.CategoryName).ToList();
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("Все категории");
                cmbCategory.Items.AddRange(categories.ToArray());
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void LoadProducts()
        {
            try
            {
                using (var db = new ShopDbContext())
                {
                    var query = db.Products
                        .Include(i => i.Category)
                        .Include(i => i.Manufacturer)
                        .Include(i => i.Supplier)
                        .Include(i => i.Measure)
                        .Include(i => i.ProductType)
                        .AsQueryable();

                    // Поиск
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        string search = txtSearch.Text.ToLower();
                        query = query.Where(p => (p.Description != null && p.Description.ToLower().Contains(search))
                                              || p.Art.ToLower().Contains(search));
                    }

                    // Фильтрация по категории
                    if (cmbCategory.SelectedIndex > 0)
                    {
                        string selectedCat = cmbCategory.SelectedItem!.ToString()!;
                        query = query.Where(p => p.Category.CategoryName == selectedCat);
                    }

                    // Сортировка
                    if (cmbSort.SelectedIndex == 1)
                        query = query.OrderBy(p => p.Price);
                    else if (cmbSort.SelectedIndex == 2)
                        query = query.OrderByDescending(p => p.Price);

                    var products = query.ToList();

                    dgvProducts.SuspendLayout();
                    dgvProducts.Rows.Clear();

                    foreach (var product in products)
                    {
                        int rowIndex = dgvProducts.Rows.Add();
                        var row = dgvProducts.Rows[rowIndex];
                        row.Tag = product; // Сохраняем объект товара в строку

                        row.Cells["colPhoto"].Value = LoadProductImage(product.PhotoUrl);
                        row.Cells["colInfo"].Value = FormatProductInfo(product);
                        row.Cells["colDiscount"].Value = $"{product.Discount}%";

                        ApplyRowStyles(row, product);
                    }

                    dgvProducts.ResumeLayout();
                    dgvProducts.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyRowStyles(DataGridViewRow row, Product product)
        {
            if (product.CountInStock <= 3)
            {
                row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FF8080");
            }

            if (product.Discount > 0)
            {
                row.Cells["colDiscount"].Style.ForeColor = Color.Red;
                row.Cells["colDiscount"].Style.Font = new Font("Calibri", 12, FontStyle.Bold);
            }
        }

        private string FormatProductInfo(Product product)
        {
            string priceText;
            if (product.Discount > 0)
            {
                decimal finalPrice = product.Price * (100 - product.Discount) / 100;
                priceText = $"{product.Price:N2} руб. -> {finalPrice:N2} руб.";
            }
            else
            {
                priceText = $"{product.Price:N2} руб.";
            }

            return $"{product.Category.CategoryName} | {product.ProductType.ProdType} (Арт: {product.Art})" + Environment.NewLine +
                   $"Описание: {product.Description}" + Environment.NewLine +
                   $"Производитель: {product.Manufacturer.ManufacturerName} | Поставщик: {product.Supplier.SupplierName}" + Environment.NewLine +
                   $"Цена: {priceText}" + Environment.NewLine +
                   $"На складе: {product.CountInStock} {product.Measure.MeasureName}";
        }

        private Image LoadProductImage(string? photoUrl)
        {
            if (!string.IsNullOrEmpty(photoUrl))
            {
                string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", photoUrl);
                if (File.Exists(path1)) return Image.FromFile(path1);
                if (File.Exists(photoUrl)) return Image.FromFile(photoUrl);
            }
            return Resources.picture;
        }

        private void FilterChanged(object? sender, EventArgs e)
        {
            LoadProducts();
        }

        private void BtnAddToOrder_Click(object? sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) return;

            var product = (Product)dgvProducts.SelectedRows[0].Tag!;
            if (product.CountInStock <= 0)
            {
                MessageBox.Show("Товара нет в наличии!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_cart.ContainsKey(product.Id))
            {
                if (_cart[product.Id] < product.CountInStock)
                    _cart[product.Id]++;
                else
                    MessageBox.Show("Больше нет на складе!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _cart[product.Id] = 1;
            }

            lblCartCount.Text = $"В текущем заказе: {_cart.Values.Sum()} шт.";
        }

        private void BtnOrders_Click(object? sender, EventArgs e)
        {
            using (var formOrders = new FormOrders(CurrentUser!, _cart))
            {
                formOrders.ShowDialog();
                lblCartCount.Text = $"В текущем заказе: {_cart.Values.Sum()} шт.";
                LoadProducts();
            }
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
