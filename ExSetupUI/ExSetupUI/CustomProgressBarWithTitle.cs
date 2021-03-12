using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200000B RID: 11
	public class CustomProgressBarWithTitle : UserControl
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x0000497A File Offset: 0x00002B7A
		public CustomProgressBarWithTitle()
		{
			this.InitializeComponent();
			this.TitleBarGap = 20;
			this.TitlePercentageGap = 20;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004998 File Offset: 0x00002B98
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000049A5 File Offset: 0x00002BA5
		public int Value
		{
			get
			{
				return this.customProgressBar.Value;
			}
			set
			{
				this.customProgressBar.Value = value;
				this.percentage.Text = string.Format("{0}%", value);
				this.percentage.Refresh();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000049D9 File Offset: 0x00002BD9
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000049E6 File Offset: 0x00002BE6
		public string Title
		{
			get
			{
				return this.title.Text;
			}
			set
			{
				this.title.Text = value;
				this.title.Refresh();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000049FF File Offset: 0x00002BFF
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004A07 File Offset: 0x00002C07
		public int TitleBarGap
		{
			get
			{
				return this.titleBarGap;
			}
			set
			{
				this.titleBarGap = value;
				this.OnSizeChanged(null);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004A17 File Offset: 0x00002C17
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004A1F File Offset: 0x00002C1F
		public int TitlePercentageGap
		{
			get
			{
				return this.titlePercentageGap;
			}
			set
			{
				this.titlePercentageGap = value;
				this.OnSizeChanged(null);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004A2F File Offset: 0x00002C2F
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004A3C File Offset: 0x00002C3C
		public bool TitleOnly
		{
			get
			{
				return this.customProgressBar.Visible;
			}
			set
			{
				this.customProgressBar.Visible = !value;
				this.percentage.Visible = !value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004A5C File Offset: 0x00002C5C
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004A69 File Offset: 0x00002C69
		public Color BarBackColor
		{
			get
			{
				return this.customProgressBar.BackColor;
			}
			set
			{
				this.customProgressBar.BackColor = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004A77 File Offset: 0x00002C77
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004A84 File Offset: 0x00002C84
		public Color BarForeColor
		{
			get
			{
				return this.customProgressBar.ForeColor;
			}
			set
			{
				this.customProgressBar.ForeColor = value;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004A92 File Offset: 0x00002C92
		protected override void OnSizeChanged(EventArgs e)
		{
			this.OnResize(null, e);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004A9C File Offset: 0x00002C9C
		private void OnResize(object sender, EventArgs e)
		{
			this.customProgressBar.Top = base.Height - this.customProgressBar.Height - 1;
			this.customProgressBar.Left = 0;
			this.customProgressBar.Width = base.Width;
			this.percentage.Top = this.customProgressBar.Top - this.TitleBarGap - this.percentage.Height - 1;
			this.percentage.Left = base.Width - this.percentage.Width - 1;
			this.title.Top = this.percentage.Top;
			this.title.Left = 0;
			this.title.Width = this.percentage.Left - this.TitlePercentageGap;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004B6E File Offset: 0x00002D6E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004B90 File Offset: 0x00002D90
		private void InitializeComponent()
		{
			this.percentage = new FontScalingLabel();
			this.title = new FontScalingLabel();
			this.customProgressBar = new CustomProgressBar();
			base.SuspendLayout();
			this.percentage.BackColor = Color.Transparent;
			this.percentage.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.percentage.Location = new Point(670, 0);
			this.percentage.Margin = new Padding(0);
			this.percentage.Name = "percentage";
			this.percentage.PreferredFontSize = 12f;
			this.percentage.Size = new Size(51, 23);
			this.percentage.TabIndex = 2;
			this.percentage.Text = "100%";
			this.percentage.TextAlign = ContentAlignment.TopRight;
			this.percentage.Resize += this.OnResize;
			this.title.BackColor = Color.Transparent;
			this.title.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.title.Location = new Point(0, 0);
			this.title.Margin = new Padding(0);
			this.title.Name = "title";
			this.title.PreferredFontSize = 12f;
			this.title.Size = new Size(508, 46);
			this.title.TabIndex = 1;
			this.title.Text = "ProgressBar Title";
			this.title.TextAlign = ContentAlignment.TopLeft;
			this.customProgressBar.BackColor = Color.FromArgb(198, 198, 198);
			this.customProgressBar.BackgroundImageLayout = ImageLayout.Zoom;
			this.customProgressBar.ForeColor = Color.FromArgb(0, 114, 198);
			this.customProgressBar.Location = new Point(0, 50);
			this.customProgressBar.Margin = new Padding(0);
			this.customProgressBar.Name = "customProgressBar";
			this.customProgressBar.Size = new Size(721, 25);
			this.customProgressBar.TabIndex = 3;
			this.customProgressBar.Value = 0;
			this.customProgressBar.Load += this.OnResize;
			base.AutoScaleDimensions = new SizeF(8f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.percentage);
			base.Controls.Add(this.title);
			base.Controls.Add(this.customProgressBar);
			base.Margin = new Padding(0);
			base.Name = "CustomProgressBarWithTitle";
			base.Size = new Size(721, 76);
			base.ResumeLayout(false);
		}

		// Token: 0x04000042 RID: 66
		private int titleBarGap;

		// Token: 0x04000043 RID: 67
		private int titlePercentageGap;

		// Token: 0x04000044 RID: 68
		private IContainer components;

		// Token: 0x04000045 RID: 69
		private CustomProgressBar customProgressBar;

		// Token: 0x04000046 RID: 70
		private FontScalingLabel title;

		// Token: 0x04000047 RID: 71
		private FontScalingLabel percentage;
	}
}
