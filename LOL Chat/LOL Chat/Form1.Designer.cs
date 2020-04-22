namespace LOL_Chat
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.users_b = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // users_b
            // 
            this.users_b.Location = new System.Drawing.Point(12, 98);
            this.users_b.Name = "users_b";
            this.users_b.Size = new System.Drawing.Size(75, 23);
            this.users_b.TabIndex = 0;
            this.users_b.Text = "Users";
            this.users_b.UseVisualStyleBackColor = true;
            this.users_b.Click += new System.EventHandler(this.users_b_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.users_b);
            this.Name = "FormMain";
            this.Text = "LOL_CHAT";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button users_b;
    }
}

