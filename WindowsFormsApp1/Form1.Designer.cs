namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_curDir = new System.Windows.Forms.TextBox();
            this.textBox_ouput = new System.Windows.Forms.TextBox();
            this.text_Error_file = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(609, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "開始歸類";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "當前目錄";
            // 
            // textBox_curDir
            // 
            this.textBox_curDir.Location = new System.Drawing.Point(113, 34);
            this.textBox_curDir.Name = "textBox_curDir";
            this.textBox_curDir.Size = new System.Drawing.Size(540, 22);
            this.textBox_curDir.TabIndex = 3;
            // 
            // textBox_ouput
            // 
            this.textBox_ouput.Location = new System.Drawing.Point(39, 470);
            this.textBox_ouput.Name = "textBox_ouput";
            this.textBox_ouput.Size = new System.Drawing.Size(573, 22);
            this.textBox_ouput.TabIndex = 4;
            // 
            // text_Error_file
            // 
            this.text_Error_file.Location = new System.Drawing.Point(65, 169);
            this.text_Error_file.Multiline = true;
            this.text_Error_file.Name = "text_Error_file";
            this.text_Error_file.Size = new System.Drawing.Size(453, 241);
            this.text_Error_file.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 543);
            this.Controls.Add(this.text_Error_file);
            this.Controls.Add(this.textBox_ouput);
            this.Controls.Add(this.textBox_curDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_curDir;
        private System.Windows.Forms.TextBox textBox_ouput;
        private System.Windows.Forms.TextBox text_Error_file;
    }
}

