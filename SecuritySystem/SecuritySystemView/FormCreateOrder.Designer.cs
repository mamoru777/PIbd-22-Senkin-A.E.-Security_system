
namespace SecuritySystemView
{
    partial class FormCreateOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxSecure = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.labelSecure = new System.Windows.Forms.Label();
            this.labelColvo = new System.Windows.Forms.Label();
            this.labelSum = new System.Windows.Forms.Label();
            this.comboBoxClient = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxSecure
            // 
            this.comboBoxSecure.FormattingEnabled = true;
            this.comboBoxSecure.Location = new System.Drawing.Point(138, 36);
            this.comboBoxSecure.Name = "comboBoxSecure";
            this.comboBoxSecure.Size = new System.Drawing.Size(273, 23);
            this.comboBoxSecure.TabIndex = 0;
            this.comboBoxSecure.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSecure_SelectedIndexChanged);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(138, 65);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(273, 23);
            this.textBoxCount.TabIndex = 1;
            this.textBoxCount.TextChanged += new System.EventHandler(this.TextBoxCount_TextChanged);
            // 
            // textBoxSum
            // 
            this.textBoxSum.Location = new System.Drawing.Point(138, 96);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.Size = new System.Drawing.Size(273, 23);
            this.textBoxSum.TabIndex = 2;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(328, 139);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(83, 31);
            this.ButtonCancel.TabIndex = 3;
            this.ButtonCancel.Text = "Отмена";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(240, 139);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(82, 31);
            this.ButtonSave.TabIndex = 4;
            this.ButtonSave.Text = "Сохранить";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelSecure
            // 
            this.labelSecure.AutoSize = true;
            this.labelSecure.Location = new System.Drawing.Point(28, 36);
            this.labelSecure.Name = "labelSecure";
            this.labelSecure.Size = new System.Drawing.Size(56, 15);
            this.labelSecure.TabIndex = 5;
            this.labelSecure.Text = "Изделие:";
            // 
            // labelColvo
            // 
            this.labelColvo.AutoSize = true;
            this.labelColvo.Location = new System.Drawing.Point(28, 65);
            this.labelColvo.Name = "labelColvo";
            this.labelColvo.Size = new System.Drawing.Size(79, 15);
            this.labelColvo.TabIndex = 6;
            this.labelColvo.Text = "Колличетсво";
            // 
            // labelSum
            // 
            this.labelSum.AutoSize = true;
            this.labelSum.Location = new System.Drawing.Point(28, 96);
            this.labelSum.Name = "labelSum";
            this.labelSum.Size = new System.Drawing.Size(45, 15);
            this.labelSum.TabIndex = 7;
            this.labelSum.Text = "Сумма";
            // 
            // comboBoxClient
            // 
            this.comboBoxClient.FormattingEnabled = true;
            this.comboBoxClient.Location = new System.Drawing.Point(138, 7);
            this.comboBoxClient.Name = "comboBoxClient";
            this.comboBoxClient.Size = new System.Drawing.Size(273, 23);
            this.comboBoxClient.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Клиент";
            // 
            // FormCreateOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 182);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxClient);
            this.Controls.Add(this.labelSum);
            this.Controls.Add(this.labelColvo);
            this.Controls.Add(this.labelSecure);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.textBoxSum);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxSecure);
            this.Name = "FormCreateOrder";
            this.Text = "FormCreateOrder";
            this.Load += new System.EventHandler(this.FormCreateOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSecure;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Label labelSecure;
        private System.Windows.Forms.Label labelColvo;
        private System.Windows.Forms.Label labelSum;
        private System.Windows.Forms.ComboBox comboBoxClient;
        private System.Windows.Forms.Label label1;
    }
}