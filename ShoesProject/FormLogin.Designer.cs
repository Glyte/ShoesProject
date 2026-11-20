using ShoesProject.Properties;

namespace ShoesProject
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pbLogo = new PictureBox();
            pnMain = new Panel();
            btnGuest = new Button();
            btnLogin = new Button();
            txtLogin = new TextBox();
            lbLogin = new Label();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            pnMain.SuspendLayout();
            SuspendLayout();

            // pbLogo (Логотип без искажения пропорций)
            pbLogo.Image = Resources.logo; // Убедитесь, что логотип добавлен в Resources под именем logo
            pbLogo.Location = new Point(132, 15);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(120, 120);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabStop = false;

            // pnMain (Дополнительный фон #D2F6E7)
            pnMain.BackColor = ColorTranslator.FromHtml("#D2F6E7");
            pnMain.Controls.Add(btnGuest);
            pnMain.Controls.Add(btnLogin);
            pnMain.Controls.Add(txtLogin);
            pnMain.Controls.Add(lbLogin);
            pnMain.Location = new Point(37, 150);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(310, 190);

            // lbLogin
            lbLogin.AutoSize = true;
            lbLogin.Location = new Point(30, 20);
            lbLogin.Text = "Введите логин:";

            // txtLogin
            txtLogin.Location = new Point(30, 48);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(250, 27);

            // btnLogin (Акцентирование внимания #70B2AF)
            btnLogin.BackColor = ColorTranslator.FromHtml("#70B2AF");
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Calibri", 12F, FontStyle.Bold);
            btnLogin.Location = new Point(30, 95);
            btnLogin.Size = new Size(250, 35);
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;

            // btnGuest
            btnGuest.BackColor = Color.White;
            btnGuest.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#70B2AF");
            btnGuest.FlatStyle = FlatStyle.Flat;
            btnGuest.Location = new Point(30, 140);
            btnGuest.Size = new Size(250, 32);
            btnGuest.Text = "Войти как гость";
            btnGuest.UseVisualStyleBackColor = false;
            btnGuest.Click += BtnGuest_Click;

            // FormLogin
            BackColor = Color.White;
            ClientSize = new Size(384, 360);
            Controls.Add(pnMain);
            Controls.Add(pbLogo);
            Font = new Font("Calibri", 12F, FontStyle.Regular);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Чудо Обувь — Авторизация";

            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            pnMain.ResumeLayout(false);
            pnMain.PerformLayout();
            ResumeLayout(false);
        }

        private PictureBox pbLogo;
        private Panel pnMain;
        private Button btnGuest;
        private Button btnLogin;
        private TextBox txtLogin;
        private Label lbLogin;
    }
}
