namespace 杜德平的数据库编程
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labStuId = new System.Windows.Forms.Label();
            this.labName = new System.Windows.Forms.Label();
            this.butView = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.butTo = new System.Windows.Forms.Button();
            this.butNoTo = new System.Windows.Forms.Button();
            this.pic = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labNum = new System.Windows.Forms.Label();
            this.labFoot = new System.Windows.Forms.Label();
            this.comBoxSelectDepart = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.txtStatistic = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labdeal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // labStuId
            // 
            this.labStuId.AutoSize = true;
            this.labStuId.Location = new System.Drawing.Point(88, 242);
            this.labStuId.Name = "labStuId";
            this.labStuId.Size = new System.Drawing.Size(29, 12);
            this.labStuId.TabIndex = 1;
            this.labStuId.Text = "学号";
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(88, 269);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(29, 12);
            this.labName.TabIndex = 2;
            this.labName.Text = "姓名";
            // 
            // butView
            // 
            this.butView.Location = new System.Drawing.Point(176, 12);
            this.butView.Name = "butView";
            this.butView.Size = new System.Drawing.Size(266, 23);
            this.butView.TabIndex = 3;
            this.butView.Text = "查看点名记录";
            this.butView.UseVisualStyleBackColor = true;
            this.butView.Click += new System.EventHandler(this.butView_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(176, 49);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(266, 298);
            this.txtLog.TabIndex = 5;
            // 
            // butTo
            // 
            this.butTo.Location = new System.Drawing.Point(12, 293);
            this.butTo.Name = "butTo";
            this.butTo.Size = new System.Drawing.Size(63, 23);
            this.butTo.TabIndex = 6;
            this.butTo.Text = "到勤";
            this.butTo.UseVisualStyleBackColor = true;
            this.butTo.Click += new System.EventHandler(this.butTo_Click);
            // 
            // butNoTo
            // 
            this.butNoTo.Location = new System.Drawing.Point(90, 293);
            this.butNoTo.Name = "butNoTo";
            this.butNoTo.Size = new System.Drawing.Size(66, 23);
            this.butNoTo.TabIndex = 7;
            this.butNoTo.Text = "缺席";
            this.butNoTo.UseVisualStyleBackColor = true;
            this.butNoTo.Click += new System.EventHandler(this.butNoTo_Click);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(12, 49);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(144, 174);
            this.pic.TabIndex = 8;
            this.pic.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "学号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "姓名：";
            // 
            // labNum
            // 
            this.labNum.AutoSize = true;
            this.labNum.Location = new System.Drawing.Point(107, 335);
            this.labNum.Name = "labNum";
            this.labNum.Size = new System.Drawing.Size(35, 12);
            this.labNum.TabIndex = 11;
            this.labNum.Text = "0 / 0";
            // 
            // labFoot
            // 
            this.labFoot.AutoSize = true;
            this.labFoot.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFoot.Location = new System.Drawing.Point(174, 362);
            this.labFoot.Name = "labFoot";
            this.labFoot.Size = new System.Drawing.Size(167, 12);
            this.labFoot.TabIndex = 12;
            this.labFoot.Text = "  模拟随机点名小程序 杜德平";
            // 
            // comBoxSelectDepart
            // 
            this.comBoxSelectDepart.FormattingEnabled = true;
            this.comBoxSelectDepart.Location = new System.Drawing.Point(71, 14);
            this.comBoxSelectDepart.Name = "comBoxSelectDepart";
            this.comBoxSelectDepart.Size = new System.Drawing.Size(83, 20);
            this.comBoxSelectDepart.TabIndex = 17;
            this.comBoxSelectDepart.Text = "--请选择--";
            this.comBoxSelectDepart.SelectedIndexChanged += new System.EventHandler(this.comBoxSelectDepart_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "选择系别";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(458, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "选择统计类别";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "全部",
            "全勤",
            "缺席一次",
            "缺席两次",
            "缺席三次及以上"});
            this.comboBoxType.Location = new System.Drawing.Point(541, 14);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(150, 20);
            this.comboBoxType.TabIndex = 20;
            this.comboBoxType.Text = "-------请选择-------";
            this.comboBoxType.SelectionChangeCommitted += new System.EventHandler(this.comboBoxType_SelectionChangeCommitted);
            // 
            // txtStatistic
            // 
            this.txtStatistic.Location = new System.Drawing.Point(460, 49);
            this.txtStatistic.Multiline = true;
            this.txtStatistic.Name = "txtStatistic";
            this.txtStatistic.ReadOnly = true;
            this.txtStatistic.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatistic.Size = new System.Drawing.Size(231, 298);
            this.txtStatistic.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 335);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "已点人数：";
            // 
            // labdeal
            // 
            this.labdeal.AutoSize = true;
            this.labdeal.ForeColor = System.Drawing.Color.Green;
            this.labdeal.Location = new System.Drawing.Point(3, 369);
            this.labdeal.Name = "labdeal";
            this.labdeal.Size = new System.Drawing.Size(89, 12);
            this.labdeal.TabIndex = 23;
            this.labdeal.Text = "正在处理......";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 385);
            this.Controls.Add(this.labdeal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStatistic);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comBoxSelectDepart);
            this.Controls.Add(this.labFoot);
            this.Controls.Add(this.labNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.butNoTo);
            this.Controls.Add(this.butTo);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.butView);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.labStuId);
            this.Name = "Form1";
            this.Text = "杜德平的数据库编程";
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labStuId;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Button butView;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button butTo;
        private System.Windows.Forms.Button butNoTo;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labNum;
        private System.Windows.Forms.Label labFoot;
        private System.Windows.Forms.ComboBox comBoxSelectDepart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.TextBox txtStatistic;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labdeal;
    }
}

