namespace FTPApplication_WHU
{
    partial class MainWindow
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
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.line1 = new System.Windows.Forms.Label();
            this.uploadBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkLengthBtn = new System.Windows.Forms.Button();
            this.renameBtn = new System.Windows.Forms.Button();
            this.downloadBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.getFileBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fileListBox
            // 
            this.fileListBox.BackColor = System.Drawing.SystemColors.Control;
            this.fileListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fileListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.fileListBox.Font = new System.Drawing.Font("等线", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.ItemHeight = 19;
            this.fileListBox.Items.AddRange(new object[] {
            "戴季成",
            "韩金伟",
            "杨捷",
            "林博韬"});
            this.fileListBox.Location = new System.Drawing.Point(0, 45);
            this.fileListBox.Margin = new System.Windows.Forms.Padding(10);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(150, 418);
            this.fileListBox.TabIndex = 1;
            this.fileListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.fileListBox_DrawItem);
            this.fileListBox.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.fileListBox_MeasureItem);
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.line1.Location = new System.Drawing.Point(155, 49);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(540, 1);
            this.line1.TabIndex = 2;
            // 
            // uploadBtn
            // 
            this.uploadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(255)))));
            this.uploadBtn.FlatAppearance.BorderSize = 0;
            this.uploadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uploadBtn.Font = new System.Drawing.Font("等线", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.uploadBtn.Location = new System.Drawing.Point(160, 10);
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Size = new System.Drawing.Size(90, 30);
            this.uploadBtn.TabIndex = 7;
            this.uploadBtn.Text = "上传文件";
            this.uploadBtn.UseVisualStyleBackColor = false;
            this.uploadBtn.Click += new System.EventHandler(this.uploadBtn_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(157, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(533, 269);
            this.label1.TabIndex = 8;
            this.label1.Text = "显示文件细节\r\n。。。\r\n。。。\r\n。。。\r\n";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("等线 Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(155, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(351, 28);
            this.label2.TabIndex = 9;
            this.label2.Text = "显示文件长度";
            // 
            // checkLengthBtn
            // 
            this.checkLengthBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(255)))));
            this.checkLengthBtn.FlatAppearance.BorderSize = 0;
            this.checkLengthBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkLengthBtn.Font = new System.Drawing.Font("等线", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkLengthBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.checkLengthBtn.Location = new System.Drawing.Point(600, 363);
            this.checkLengthBtn.Name = "checkLengthBtn";
            this.checkLengthBtn.Size = new System.Drawing.Size(90, 28);
            this.checkLengthBtn.TabIndex = 10;
            this.checkLengthBtn.Text = "显示长度";
            this.checkLengthBtn.UseVisualStyleBackColor = false;
            // 
            // renameBtn
            // 
            this.renameBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(255)))));
            this.renameBtn.FlatAppearance.BorderSize = 0;
            this.renameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renameBtn.Font = new System.Drawing.Font("等线", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.renameBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.renameBtn.Location = new System.Drawing.Point(600, 396);
            this.renameBtn.Name = "renameBtn";
            this.renameBtn.Size = new System.Drawing.Size(90, 28);
            this.renameBtn.TabIndex = 12;
            this.renameBtn.Text = "重命名";
            this.renameBtn.UseVisualStyleBackColor = false;
            // 
            // downloadBtn
            // 
            this.downloadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(255)))));
            this.downloadBtn.FlatAppearance.BorderSize = 0;
            this.downloadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadBtn.Font = new System.Drawing.Font("等线", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downloadBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.downloadBtn.Location = new System.Drawing.Point(15, 408);
            this.downloadBtn.Name = "downloadBtn";
            this.downloadBtn.Size = new System.Drawing.Size(50, 30);
            this.downloadBtn.TabIndex = 13;
            this.downloadBtn.Text = "下载";
            this.downloadBtn.UseVisualStyleBackColor = false;
            this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(255)))));
            this.deleteBtn.FlatAppearance.BorderSize = 0;
            this.deleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteBtn.Font = new System.Drawing.Font("等线", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deleteBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.deleteBtn.Location = new System.Drawing.Point(85, 408);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(50, 30);
            this.deleteBtn.TabIndex = 14;
            this.deleteBtn.Text = "删除";
            this.deleteBtn.UseVisualStyleBackColor = false;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Enabled = false;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 45);
            this.label4.TabIndex = 15;
            this.label4.Text = "我的文件";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // passwordBox
            // 
            this.passwordBox.BackColor = System.Drawing.Color.White;
            this.passwordBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passwordBox.Font = new System.Drawing.Font("等线 Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordBox.Location = new System.Drawing.Point(158, 396);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(348, 20);
            this.passwordBox.TabIndex = 16;
            this.passwordBox.Text = "文件重命名";
            // 
            // getFileBtn
            // 
            this.getFileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(255)))));
            this.getFileBtn.FlatAppearance.BorderSize = 0;
            this.getFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.getFileBtn.Font = new System.Drawing.Font("等线", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.getFileBtn.ForeColor = System.Drawing.SystemColors.Window;
            this.getFileBtn.Location = new System.Drawing.Point(256, 10);
            this.getFileBtn.Name = "getFileBtn";
            this.getFileBtn.Size = new System.Drawing.Size(90, 30);
            this.getFileBtn.TabIndex = 17;
            this.getFileBtn.Text = "查看所有";
            this.getFileBtn.UseVisualStyleBackColor = false;
            this.getFileBtn.Click += new System.EventHandler(this.getFileBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.closeBtn.BackgroundImage = global::FTPApplication_WHU.Properties.Resources.close;
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Location = new System.Drawing.Point(670, 10);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(20, 20);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Controls.Add(this.getFileBtn);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.downloadBtn);
            this.Controls.Add(this.renameBtn);
            this.Controls.Add(this.checkLengthBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uploadBtn);
            this.Controls.Add(this.fileListBox);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Label line1;
        private System.Windows.Forms.Button uploadBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button checkLengthBtn;
        private System.Windows.Forms.Button renameBtn;
        private System.Windows.Forms.Button downloadBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button getFileBtn;
    }

}