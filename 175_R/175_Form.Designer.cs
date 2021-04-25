namespace _175_R
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
    private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.StartButton = new MetroFramework.Controls.MetroButton();
            this.BrandItemsView = new System.Windows.Forms.DataGridView();
            this.ProcessTimer = new System.Windows.Forms.Timer(this.components);
            this.Loading = new System.Windows.Forms.PictureBox();
            this.LastUpdate = new System.Windows.Forms.Label();
            this.column_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_continue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.up_ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BrandItemsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.White;
            this.StartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartButton.ForeColor = System.Drawing.Color.Black;
            this.StartButton.Location = new System.Drawing.Point(273, 26);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(133, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Let\'s Start Jumping!";
            this.StartButton.UseSelectable = true;
            this.StartButton.Click += new System.EventHandler(this.StartProcess);
            // 
            // BrandItemsView
            // 
            this.BrandItemsView.AllowUserToAddRows = false;
            this.BrandItemsView.AllowUserToDeleteRows = false;
            this.BrandItemsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BrandItemsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column_brand,
            this.column_price,
            this.column_continue,
            this.up_ratio});
            this.BrandItemsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrandItemsView.Location = new System.Drawing.Point(20, 60);
            this.BrandItemsView.Name = "BrandItemsView";
            this.BrandItemsView.RowHeadersVisible = false;
            this.BrandItemsView.RowTemplate.Height = 21;
            this.BrandItemsView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BrandItemsView.Size = new System.Drawing.Size(645, 370);
            this.BrandItemsView.TabIndex = 1;
            // 
            // ProcessTimer
            // 
            this.ProcessTimer.Enabled = true;
            this.ProcessTimer.Interval = 300000;
            this.ProcessTimer.Tick += new System.EventHandler(this.MainProcess);
            // 
            // Loading
            // 
            this.Loading.Image = ((System.Drawing.Image)(resources.GetObject("Loading.Image")));
            this.Loading.Location = new System.Drawing.Point(639, 30);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(27, 25);
            this.Loading.TabIndex = 2;
            this.Loading.TabStop = false;
            this.Loading.Visible = false;
            // 
            // LastUpdate
            // 
            this.LastUpdate.AutoSize = true;
            this.LastUpdate.Location = new System.Drawing.Point(556, 41);
            this.LastUpdate.Name = "LastUpdate";
            this.LastUpdate.Size = new System.Drawing.Size(105, 12);
            this.LastUpdate.TabIndex = 3;
            this.LastUpdate.Text = "Last Update:         ";
            // 
            // column_brand
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.column_brand.DefaultCellStyle = dataGridViewCellStyle1;
            this.column_brand.HeaderText = "銘柄";
            this.column_brand.Name = "column_brand";
            this.column_brand.ReadOnly = true;
            this.column_brand.Width = 120;
            // 
            // column_price
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.column_price.DefaultCellStyle = dataGridViewCellStyle2;
            this.column_price.HeaderText = "価格";
            this.column_price.Name = "column_price";
            this.column_price.ReadOnly = true;
            this.column_price.Width = 200;
            // 
            // column_continue
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.column_continue.DefaultCellStyle = dataGridViewCellStyle3;
            this.column_continue.HeaderText = "連続上昇時間";
            this.column_continue.Name = "column_continue";
            this.column_continue.ReadOnly = true;
            this.column_continue.Width = 150;
            // 
            // up_ratio
            // 
            this.up_ratio.HeaderText = "直近5分上昇率";
            this.up_ratio.Name = "up_ratio";
            this.up_ratio.ReadOnly = true;
            this.up_ratio.Width = 200;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 450);
            this.Controls.Add(this.LastUpdate);
            this.Controls.Add(this.Loading);
            this.Controls.Add(this.BrandItemsView);
            this.Controls.Add(this.StartButton);
            this.Name = "Form1";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "175 Rider";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseProcess);
            ((System.ComponentModel.ISupportInitialize)(this.BrandItemsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton StartButton;
        private System.Windows.Forms.DataGridView BrandItemsView;
        private System.Windows.Forms.Timer ProcessTimer;
        private System.Windows.Forms.PictureBox Loading;
        private System.Windows.Forms.Label LastUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_continue;
        private System.Windows.Forms.DataGridViewTextBoxColumn up_ratio;
    }
}

