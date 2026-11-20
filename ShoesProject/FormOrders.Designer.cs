namespace ShoesProject
{
    partial class FormOrders
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabCart = new TabPage();
            dgvCart = new DataGridView();
            pnCartBottom = new Panel();
            cmbDeliveryPoint = new ComboBox();
            lblDelivery = new Label();
            lblTotalSum = new Label();
            btnCreateOrder = new Button();
            btnRemoveItem = new Button();

            tabOrdersList = new TabPage();
            dgvOrders = new DataGridView();
            pnOrdersBottom = new Panel();
            cmbStatus = new ComboBox();
            btnChangeStatus = new Button();
            btnDeleteOrder = new Button();

            tabControl.SuspendLayout();
            tabCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            pnCartBottom.SuspendLayout();
            tabOrdersList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            pnOrdersBottom.SuspendLayout();
            SuspendLayout();

            tabControl.Dock = DockStyle.Fill;
            tabControl.Controls.Add(tabCart);
            tabControl.Controls.Add(tabOrdersList);

            //Оформление заказа
            tabCart.Text = "Текущий заказ (Корзина)";
            tabCart.BackColor = Color.White;
            tabCart.Controls.Add(dgvCart);
            tabCart.Controls.Add(pnCartBottom);

            dgvCart.Dock = DockStyle.Fill;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.ReadOnly = true;

            pnCartBottom.Dock = DockStyle.Bottom;
            pnCartBottom.Height = 90;
            pnCartBottom.BackColor = ColorTranslator.FromHtml("#D2F6E7");
            pnCartBottom.Controls.Add(lblDelivery);
            pnCartBottom.Controls.Add(cmbDeliveryPoint);
            pnCartBottom.Controls.Add(lblTotalSum);
            pnCartBottom.Controls.Add(btnCreateOrder);
            pnCartBottom.Controls.Add(btnRemoveItem);

            lblDelivery.AutoSize = true;
            lblDelivery.Location = new Point(15, 15);
            lblDelivery.Text = "Пункт выдачи:";

            cmbDeliveryPoint.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeliveryPoint.Location = new Point(130, 12);
            cmbDeliveryPoint.Size = new Size(350, 27);

            lblTotalSum.AutoSize = true;
            lblTotalSum.Font = new Font("Calibri", 13F, FontStyle.Bold);
            lblTotalSum.Location = new Point(15, 52);
            lblTotalSum.Text = "Итого: 0.00 руб.";

            btnRemoveItem.BackColor = Color.White;
            btnRemoveItem.FlatStyle = FlatStyle.Flat;
            btnRemoveItem.Location = new Point(500, 12);
            btnRemoveItem.Size = new Size(160, 32);
            btnRemoveItem.Text = "Удалить позицию";
            btnRemoveItem.Click += BtnRemoveItem_Click;

            btnCreateOrder.BackColor = ColorTranslator.FromHtml("#70B2AF");
            btnCreateOrder.FlatStyle = FlatStyle.Flat;
            btnCreateOrder.ForeColor = Color.White;
            btnCreateOrder.Font = new Font("Calibri", 12F, FontStyle.Bold);
            btnCreateOrder.Location = new Point(680, 12);
            btnCreateOrder.Size = new Size(180, 60);
            btnCreateOrder.Text = "Оформить заказ";
            btnCreateOrder.Click += BtnCreateOrder_Click;

            //Список заказов
            tabOrdersList.Text = "Список заказов";
            tabOrdersList.BackColor = Color.White;
            tabOrdersList.Controls.Add(dgvOrders);
            tabOrdersList.Controls.Add(pnOrdersBottom);

            dgvOrders.Dock = DockStyle.Fill;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.ReadOnly = true;

            pnOrdersBottom.Dock = DockStyle.Bottom;
            pnOrdersBottom.Height = 60;
            pnOrdersBottom.BackColor = ColorTranslator.FromHtml("#D2F6E7");
            pnOrdersBottom.Controls.Add(cmbStatus);
            pnOrdersBottom.Controls.Add(btnChangeStatus);
            pnOrdersBottom.Controls.Add(btnDeleteOrder);

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(15, 16);
            cmbStatus.Size = new Size(200, 27);

            btnChangeStatus.BackColor = ColorTranslator.FromHtml("#70B2AF");
            btnChangeStatus.FlatStyle = FlatStyle.Flat;
            btnChangeStatus.ForeColor = Color.White;
            btnChangeStatus.Location = new Point(230, 14);
            btnChangeStatus.Size = new Size(180, 32);
            btnChangeStatus.Text = "Изменить статус";
            btnChangeStatus.Click += BtnChangeStatus_Click;

            btnDeleteOrder.BackColor = ColorTranslator.FromHtml("#FF8080");
            btnDeleteOrder.FlatStyle = FlatStyle.Flat;
            btnDeleteOrder.Location = new Point(680, 14);
            btnDeleteOrder.Size = new Size(180, 32);
            btnDeleteOrder.Text = "Удалить заказ";
            btnDeleteOrder.Click += BtnDeleteOrder_Click;

            // FormOrders
            ClientSize = new Size(884, 511);
            Controls.Add(tabControl);
            Font = new Font("Calibri", 12F, FontStyle.Regular);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Чудо Обувь — Управление заказами";

            tabControl.ResumeLayout(false);
            tabCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            pnCartBottom.ResumeLayout(false);
            pnCartBottom.PerformLayout();
            tabOrdersList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            pnOrdersBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TabControl tabControl;
        private TabPage tabCart, tabOrdersList;
        private DataGridView dgvCart, dgvOrders;
        private Panel pnCartBottom, pnOrdersBottom;
        private ComboBox cmbDeliveryPoint, cmbStatus;
        private Label lblDelivery, lblTotalSum;
        private Button btnCreateOrder, btnRemoveItem, btnChangeStatus, btnDeleteOrder;
    }
}
