namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000024 RID: 36
	internal partial class SetupFormBase : global::System.Windows.Forms.Form
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00009436 File Offset: 0x00007636
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009A50 File Offset: 0x00007C50
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.basePanel = new global::System.Windows.Forms.Panel();
			this.footerPanel = new global::System.Windows.Forms.Panel();
			this.buttonTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.btnPrevious = new global::System.Windows.Forms.Button();
			this.btnNext = new global::System.Windows.Forms.Button();
			this.logoBox = new global::System.Windows.Forms.PictureBox();
			this.headerPanel = new global::System.Windows.Forms.Panel();
			this.btnPrint = new global::System.Windows.Forms.Button();
			this.exchangeServerLabel = new global::System.Windows.Forms.Label();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.setupFormBaseImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.btnHelp = new global::System.Windows.Forms.Button();
			this.displayPanel = new global::System.Windows.Forms.Panel();
			this.titlePanel = new global::System.Windows.Forms.Panel();
			this.pageTitle = new global::System.Windows.Forms.Label();
			this.pagePanel = new global::System.Windows.Forms.Panel();
			this.nextButtonTimer = new global::System.Windows.Forms.Timer(this.components);
			this.checkLoadedTimer = new global::System.Windows.Forms.Timer(this.components);
			this.printDialog = new global::System.Windows.Forms.PrintDialog();
			this.printDocument = new global::System.Drawing.Printing.PrintDocument();
			this.basePanel.SuspendLayout();
			this.footerPanel.SuspendLayout();
			this.buttonTableLayoutPanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.logoBox).BeginInit();
			this.headerPanel.SuspendLayout();
			this.displayPanel.SuspendLayout();
			this.titlePanel.SuspendLayout();
			base.SuspendLayout();
			this.basePanel.BackColor = global::System.Drawing.Color.White;
			this.basePanel.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.basePanel.Controls.Add(this.footerPanel);
			this.basePanel.Controls.Add(this.headerPanel);
			this.basePanel.Controls.Add(this.displayPanel);
			this.basePanel.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel, 0);
			this.basePanel.ForeColor = global::System.Drawing.Color.FromArgb(51, 51, 51);
			this.basePanel.Location = new global::System.Drawing.Point(0, 0);
			this.basePanel.Margin = new global::System.Windows.Forms.Padding(3, 3, 0, 0);
			this.basePanel.Name = "basePanel";
			this.basePanel.Size = new global::System.Drawing.Size(800, 700);
			this.basePanel.TabIndex = 0;
			this.basePanel.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.SetupFormBase_MouseDown);
			this.footerPanel.BackColor = global::System.Drawing.Color.Transparent;
			this.footerPanel.Controls.Add(this.buttonTableLayoutPanel);
			this.footerPanel.Controls.Add(this.logoBox);
			this.footerPanel.Location = new global::System.Drawing.Point(40, 601);
			this.footerPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.footerPanel.Name = "footerPanel";
			this.footerPanel.Size = new global::System.Drawing.Size(721, 60);
			this.footerPanel.TabIndex = 0;
			this.buttonTableLayoutPanel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonTableLayoutPanel.AutoSize = true;
			this.buttonTableLayoutPanel.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonTableLayoutPanel.ColumnCount = 2;
			this.buttonTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.buttonTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.buttonTableLayoutPanel.Controls.Add(this.btnPrevious, 0, 0);
			this.buttonTableLayoutPanel.Controls.Add(this.btnNext, 1, 0);
			this.buttonTableLayoutPanel.Location = new global::System.Drawing.Point(487, 22);
			this.buttonTableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
			this.buttonTableLayoutPanel.RowCount = 1;
			this.buttonTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.buttonTableLayoutPanel.Size = new global::System.Drawing.Size(234, 31);
			this.buttonTableLayoutPanel.TabIndex = 103;
			this.btnPrevious.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnPrevious.AutoSize = true;
			this.btnPrevious.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnPrevious.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnPrevious.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnPrevious.FlatAppearance.BorderSize = 2;
			this.btnPrevious.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.FromArgb(0, 114, 198);
			this.btnPrevious.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Transparent;
			this.btnPrevious.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnPrevious.ForeColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnPrevious.Location = new global::System.Drawing.Point(27, 0);
			this.btnPrevious.Margin = new global::System.Windows.Forms.Padding(27, 0, 0, 0);
			this.btnPrevious.MinimumSize = new global::System.Drawing.Size(90, 31);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new global::System.Drawing.Size(90, 31);
			this.btnPrevious.TabIndex = 102;
			this.btnPrevious.Text = "[btnBack]";
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.EnabledChanged += new global::System.EventHandler(this.PreviousButtonEnabledChanged);
			this.btnPrevious.Click += new global::System.EventHandler(this.BtnPrevious_Click);
			this.btnNext.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnNext.AutoSize = true;
			this.btnNext.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnNext.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnNext.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnNext.FlatAppearance.BorderSize = 2;
			this.btnNext.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.FromArgb(0, 114, 198);
			this.btnNext.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Transparent;
			this.btnNext.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnNext.ForeColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnNext.Location = new global::System.Drawing.Point(144, 0);
			this.btnNext.Margin = new global::System.Windows.Forms.Padding(27, 0, 0, 0);
			this.btnNext.MinimumSize = new global::System.Drawing.Size(90, 31);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new global::System.Drawing.Size(90, 31);
			this.btnNext.TabIndex = 100;
			this.btnNext.Text = "[btnNext]";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.EnabledChanged += new global::System.EventHandler(this.NextButtonEnabledChanged);
			this.btnNext.Click += new global::System.EventHandler(this.BtnNext_Click);
			this.logoBox.Image = global::Microsoft.Exchange.Setup.ExSetupUI.SetupFormBase.Images[6];
			this.logoBox.Location = new global::System.Drawing.Point(0, 35);
			this.logoBox.Name = "logoBox";
			this.logoBox.Size = new global::System.Drawing.Size(107, 23);
			this.logoBox.TabIndex = 4;
			this.logoBox.TabStop = false;
			this.headerPanel.BackColor = global::System.Drawing.Color.Transparent;
			this.headerPanel.Controls.Add(this.btnPrint);
			this.headerPanel.Controls.Add(this.exchangeServerLabel);
			this.headerPanel.Controls.Add(this.btnCancel);
			this.headerPanel.Controls.Add(this.btnHelp);
			this.headerPanel.Location = new global::System.Drawing.Point(40, 40);
			this.headerPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.Size = new global::System.Drawing.Size(721, 55);
			this.headerPanel.TabIndex = 0;
			this.btnPrint.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnPrint.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnPrint.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.Window;
			this.btnPrint.FlatAppearance.BorderSize = 0;
			this.btnPrint.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Transparent;
			this.btnPrint.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Transparent;
			this.btnPrint.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnPrint.ForeColor = global::System.Drawing.SystemColors.Window;
			this.btnPrint.ImageList = this.setupFormBaseImageList;
			this.btnPrint.Location = new global::System.Drawing.Point(630, 0);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new global::System.Drawing.Size(16, 16);
			this.btnPrint.TabIndex = 13;
			this.btnPrint.UseVisualStyleBackColor = true;
			this.exchangeServerLabel.AutoSize = true;
			this.exchangeServerLabel.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.exchangeServerLabel.Font = new global::System.Drawing.Font("Segoe UI", 13.8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel, 0);
			this.exchangeServerLabel.ForeColor = global::System.Drawing.Color.FromArgb(0, 114, 198);
			this.exchangeServerLabel.Location = new global::System.Drawing.Point(0, 0);
			this.exchangeServerLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.exchangeServerLabel.Name = "exchangeServerLabel";
			this.exchangeServerLabel.Size = new global::System.Drawing.Size(171, 19);
			this.exchangeServerLabel.TabIndex = 1;
			this.exchangeServerLabel.Text = "[EXCHANGESERVERLABEL]";
			this.btnCancel.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnCancel.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.Window;
			this.btnCancel.FlatAppearance.BorderSize = 0;
			this.btnCancel.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Transparent;
			this.btnCancel.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Transparent;
			this.btnCancel.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.ForeColor = global::System.Drawing.Color.Transparent;
			this.btnCancel.ImageList = this.setupFormBaseImageList;
			this.btnCancel.Location = new global::System.Drawing.Point(702, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(16, 16);
			this.btnCancel.TabIndex = 101;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.BtnCancel_Click);
			this.setupFormBaseImageList.ColorDepth = global::System.Windows.Forms.ColorDepth.Depth8Bit;
			this.setupFormBaseImageList.ImageSize = new global::System.Drawing.Size(16, 16);
			this.setupFormBaseImageList.TransparentColor = global::System.Drawing.Color.Transparent;
			this.btnHelp.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnHelp.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnHelp.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.Window;
			this.btnHelp.FlatAppearance.BorderSize = 0;
			this.btnHelp.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Transparent;
			this.btnHelp.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Transparent;
			this.btnHelp.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnHelp.ForeColor = global::System.Drawing.SystemColors.Window;
			this.btnHelp.ImageList = this.setupFormBaseImageList;
			this.btnHelp.Location = new global::System.Drawing.Point(666, 0);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new global::System.Drawing.Size(16, 16);
			this.btnHelp.TabIndex = 103;
			this.btnHelp.UseVisualStyleBackColor = true;
			this.btnHelp.Click += new global::System.EventHandler(this.BtnHelp_Click);
			this.displayPanel.BackColor = global::System.Drawing.Color.Transparent;
			this.displayPanel.Controls.Add(this.titlePanel);
			this.displayPanel.Controls.Add(this.pagePanel);
			this.displayPanel.Location = new global::System.Drawing.Point(40, 40);
			this.displayPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.displayPanel.Name = "displayPanel";
			this.displayPanel.Size = new global::System.Drawing.Size(721, 621);
			this.displayPanel.TabIndex = 0;
			this.titlePanel.Controls.Add(this.pageTitle);
			this.titlePanel.Location = new global::System.Drawing.Point(0, 56);
			this.titlePanel.Name = "titlePanel";
			this.titlePanel.Size = new global::System.Drawing.Size(721, 69);
			this.titlePanel.TabIndex = 2;
			this.pageTitle.AutoSize = true;
			this.pageTitle.BackColor = global::System.Drawing.Color.Transparent;
			this.pageTitle.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.pageTitle.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.pageTitle.Font = new global::System.Drawing.Font("Segoe UI Light", 22.2f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.pageTitle.ForeColor = global::System.Drawing.Color.FromArgb(51, 51, 51);
			this.pageTitle.Location = new global::System.Drawing.Point(0, 0);
			this.pageTitle.Margin = new global::System.Windows.Forms.Padding(0);
			this.pageTitle.Name = "pageTitle";
			this.pageTitle.Size = new global::System.Drawing.Size(189, 51);
			this.pageTitle.TabIndex = 1;
			this.pageTitle.Text = "[pageTitle]";
			this.pagePanel.BackColor = global::System.Drawing.Color.Transparent;
			this.pagePanel.Location = new global::System.Drawing.Point(0, 126);
			this.pagePanel.Name = "pagePanel";
			this.pagePanel.Size = new global::System.Drawing.Size(721, 435);
			this.pagePanel.TabIndex = 0;
			this.nextButtonTimer.Tick += new global::System.EventHandler(this.NextButtonTimer_Tick);
			this.checkLoadedTimer.Tick += new global::System.EventHandler(this.CheckLoadedTimer_Tick);
			this.printDialog.UseEXDialog = true;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			base.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.AutoValidate = global::System.Windows.Forms.AutoValidate.Disable;
			this.BackColor = global::System.Drawing.Color.White;
			base.ClientSize = new global::System.Drawing.Size(800, 700);
			base.Controls.Add(this.basePanel);
			this.DoubleBuffered = true;
			this.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel, 0);
			this.ForeColor = global::System.Drawing.SystemColors.Info;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SetupFormBase";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Closing += new global::System.ComponentModel.CancelEventHandler(this.SetupFormBase_Closing);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.SetupFormBase_FormClosed);
			base.Load += new global::System.EventHandler(this.SetupFormBase_Load);
			base.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.SetupFormBase_MouseDown);
			this.basePanel.ResumeLayout(false);
			this.footerPanel.ResumeLayout(false);
			this.footerPanel.PerformLayout();
			this.buttonTableLayoutPanel.ResumeLayout(false);
			this.buttonTableLayoutPanel.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.logoBox).EndInit();
			this.headerPanel.ResumeLayout(false);
			this.headerPanel.PerformLayout();
			this.displayPanel.ResumeLayout(false);
			this.titlePanel.ResumeLayout(false);
			this.titlePanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040000DB RID: 219
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000DC RID: 220
		private global::System.Windows.Forms.Panel basePanel;

		// Token: 0x040000DD RID: 221
		private global::System.Windows.Forms.Panel headerPanel;

		// Token: 0x040000DE RID: 222
		private global::System.Windows.Forms.Panel displayPanel;

		// Token: 0x040000DF RID: 223
		private global::System.Windows.Forms.Panel footerPanel;

		// Token: 0x040000E0 RID: 224
		private global::System.Windows.Forms.Button btnHelp;

		// Token: 0x040000E1 RID: 225
		private global::System.Windows.Forms.Button btnNext;

		// Token: 0x040000E2 RID: 226
		private global::System.Windows.Forms.Button btnPrevious;

		// Token: 0x040000E3 RID: 227
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x040000E4 RID: 228
		private global::System.Windows.Forms.Panel pagePanel;

		// Token: 0x040000E5 RID: 229
		private global::System.Windows.Forms.Label pageTitle;

		// Token: 0x040000EA RID: 234
		private global::System.Windows.Forms.Timer nextButtonTimer;

		// Token: 0x040000EB RID: 235
		private global::System.Windows.Forms.Label exchangeServerLabel;

		// Token: 0x040000EC RID: 236
		private global::System.Windows.Forms.ImageList setupFormBaseImageList;

		// Token: 0x040000ED RID: 237
		private global::System.Windows.Forms.PictureBox logoBox;

		// Token: 0x040000EE RID: 238
		private global::System.Windows.Forms.Button btnPrint;

		// Token: 0x040000EF RID: 239
		private global::System.Windows.Forms.PrintDialog printDialog;

		// Token: 0x040000F1 RID: 241
		private global::System.Drawing.Printing.PrintDocument printDocument;

		// Token: 0x040000F4 RID: 244
		private global::System.Windows.Forms.Panel titlePanel;

		// Token: 0x040000F5 RID: 245
		private global::System.Windows.Forms.TableLayoutPanel buttonTableLayoutPanel;

		// Token: 0x040000F6 RID: 246
		private global::System.Windows.Forms.Timer checkLoadedTimer;

		// Token: 0x040000F7 RID: 247
		internal static global::System.Collections.Generic.List<global::System.Drawing.Image> Images;
	}
}
