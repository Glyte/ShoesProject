using Microsoft.EntityFrameworkCore;
using ShoesProject.Models;

namespace ShoesProject
{
    public partial class FormOrders : Form
    {
        private User _currentUser;
        private Dictionary<int, int> _cart;

        public FormOrders(User user, Dictionary<int, int> cart)
        {
            InitializeComponent();
            _currentUser = user;
            _cart = cart;

            SetupRoleAccess();
            LoadLists();
            RefreshCartGrid();
            LoadOrdersGrid();
        }

        private void SetupRoleAccess()
        {
            string role = _currentUser.Role.RoleName.ToLower();
            bool isManagerOrAdmin = role.Contains("менеджер") || role.Contains("администратор");

            //Менеджер и Администратор могут удалять и управлять заказами
            btnDeleteOrder.Visible = isManagerOrAdmin;
            // Администратор может редактировать заказ
            cmbStatus.Visible = role.Contains("администратор");
            btnChangeStatus.Visible = role.Contains("администратор");
        }

        private void LoadLists()
        {
            using (var db = new ShopDbContext())
            {
                cmbDeliveryPoint.DataSource = db.DeliveryPoints.ToList();
                cmbDeliveryPoint.DisplayMember = "DeliveryAddress";
                cmbDeliveryPoint.ValueMember = "Id";

                cmbStatus.DataSource = db.Statuses.ToList();
                cmbStatus.DisplayMember = "StatusName";
                cmbStatus.ValueMember = "Id";
            }
        }

        private void RefreshCartGrid()
        {
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add("Id", "ID");
            dgvCart.Columns["Id"].Visible = false;
            dgvCart.Columns.Add("Name", "Товар");
            dgvCart.Columns.Add("Price", "Цена за ед. (руб.)");
            dgvCart.Columns.Add("Qty", "Количество");
            dgvCart.Columns.Add("Total", "Сумма (руб.)");

            decimal totalOrderSum = 0;

            using (var db = new ShopDbContext())
            {
                foreach (var item in _cart)
                {
                    var p = db.Products.Include(x => x.ProductType).FirstOrDefault(x => x.Id == item.Key);
                    if (p == null) continue;

                    decimal finalPrice = p.Price * (100 - p.Discount) / 100;
                    decimal rowSum = finalPrice * item.Value;
                    totalOrderSum += rowSum;

                    dgvCart.Rows.Add(p.Id, $"{p.ProductType.ProdType} ({p.Art})", finalPrice.ToString("N2"), item.Value, rowSum.ToString("N2"));
                }
            }

            lblTotalSum.Text = $"Итоговая сумма по заказу: {totalOrderSum:N2} руб.";
        }

        private void BtnRemoveItem_Click(object? sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0) return;
            int prodId = (int)dgvCart.SelectedRows[0].Cells["Id"].Value;
            _cart.Remove(prodId);
            RefreshCartGrid();
        }

        private void BtnCreateOrder_Click(object? sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbDeliveryPoint.SelectedValue == null)
            {
                MessageBox.Show("Выберите пункт выдачи!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new ShopDbContext())
            {
                var firstStatus = db.Statuses.FirstOrDefault();
                if (firstStatus == null)
                {
                    MessageBox.Show("В БД отсутствуют статусы заказов!", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newOrder = new Order
                {
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    DeliveryDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                    IdDeliveryPoint = (int)cmbDeliveryPoint.SelectedValue,
                    IdUser = _currentUser.Id,
                    Code = new Random().Next(100, 999),
                    IdStatuses = firstStatus.Id
                };

                db.Orders.Add(newOrder);
                db.SaveChanges();

                foreach (var item in _cart)
                {
                    var product = db.Products.Find(item.Key)!;
                    decimal finalPrice = product.Price * (100 - product.Discount) / 100;

                    db.ProductOrders.Add(new ProductOrder
                    {
                        IdOrder = newOrder.Id,
                        IdProduct = product.Id,
                        Quantity = item.Value,
                        PriceAtOrder = finalPrice
                    });

                    // Уменьшаем остаток на складе
                    product.CountInStock -= item.Value;
                }

                db.SaveChanges();
            }

            _cart.Clear();
            RefreshCartGrid();
            LoadOrdersGrid();
            MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadOrdersGrid()
        {
            dgvOrders.Columns.Clear();
            dgvOrders.Columns.Add("Id", "№ Заказа");
            dgvOrders.Columns.Add("Date", "Дата заказа");
            dgvOrders.Columns.Add("Client", "ФИО клиента");
            dgvOrders.Columns.Add("Status", "Статус");
            dgvOrders.Columns.Add("Sum", "Итоговая сумма (руб.)");

            using (var db = new ShopDbContext())
            {
                var query = db.Orders
                    .Include(o => o.User)
                    .Include(o => o.Status)
                    .Include(o => o.ProductOrders)
                    .AsQueryable();

                // Обычный клиент видит только свои заказы, менеджер и админ — все
                string role = _currentUser.Role.RoleName.ToLower();
                if (!role.Contains("менеджер") && !role.Contains("администратор"))
                {
                    query = query.Where(o => o.IdUser == _currentUser.Id);
                }

                foreach (var o in query.ToList())
                {
                    decimal sum = o.ProductOrders.Sum(po => po.Quantity * po.PriceAtOrder);
                    dgvOrders.Rows.Add(o.Id, o.OrderDate.ToString("dd.MM.yyyy"), o.User.FullName, o.Status.StatusName, sum.ToString("N2"));
                }
            }
        }

        private void BtnChangeStatus_Click(object? sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0 || cmbStatus.SelectedValue == null) return;
            int orderId = (int)dgvOrders.SelectedRows[0].Cells["Id"].Value;

            using (var db = new ShopDbContext())
            {
                var order = db.Orders.Find(orderId);
                if (order != null)
                {
                    order.IdStatuses = (int)cmbStatus.SelectedValue;
                    db.SaveChanges();
                    LoadOrdersGrid();
                }
            }
        }

        private void BtnDeleteOrder_Click(object? sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0) return;
            int orderId = (int)dgvOrders.SelectedRows[0].Cells["Id"].Value;

            if (MessageBox.Show($"Удалить заказ №{orderId}?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var db = new ShopDbContext())
                {
                    var order = db.Orders.Include(o => o.ProductOrders).FirstOrDefault(o => o.Id == orderId);
                    if (order != null)
                    {
                        db.ProductOrders.RemoveRange(order.ProductOrders);
                        db.Orders.Remove(order);
                        db.SaveChanges();
                        LoadOrdersGrid();
                    }
                }
            }
        }
    }
}
