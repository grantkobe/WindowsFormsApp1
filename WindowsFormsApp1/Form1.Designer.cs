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
            this.radioButton_month = new System.Windows.Forms.RadioButton();
            this.radioButton_day = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 53);
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
            // radioButton_month
            // 
            this.radioButton_month.AutoSize = true;
            this.radioButton_month.Location = new System.Drawing.Point(33, 18);
            this.radioButton_month.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton_month.Name = "radioButton_month";
            this.radioButton_month.Size = new System.Drawing.Size(59, 16);
            this.radioButton_month.TabIndex = 6;
            this.radioButton_month.Text = "按月份";
            this.radioButton_month.UseVisualStyleBackColor = true;
            // 
            // radioButton_day
            // 
            this.radioButton_day.AutoSize = true;
            this.radioButton_day.Checked = true;
            this.radioButton_day.Location = new System.Drawing.Point(92, 18);
            this.radioButton_day.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton_day.Name = "radioButton_day";
            this.radioButton_day.Size = new System.Drawing.Size(59, 16);
            this.radioButton_day.TabIndex = 7;
            this.radioButton_day.TabStop = true;
            this.radioButton_day.Text = "按日期";
            this.radioButton_day.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_day);
            this.groupBox1.Controls.Add(this.radioButton_month);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(556, 72);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(158, 90);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 543);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.text_Error_file);
            this.Controls.Add(this.textBox_ouput);
            this.Controls.Add(this.textBox_curDir);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "拍照日期或月份排列 2.1_20180620";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_curDir;
        private System.Windows.Forms.TextBox textBox_ouput;
        private System.Windows.Forms.TextBox text_Error_file;
        private System.Windows.Forms.RadioButton radioButton_month;
        private System.Windows.Forms.RadioButton radioButton_day;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

