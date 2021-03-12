using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200000A RID: 10
	public class CustomProgressBar : UserControl
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000047FB File Offset: 0x000029FB
		public CustomProgressBar()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004809 File Offset: 0x00002A09
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004811 File Offset: 0x00002A11
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (value < 0)
				{
					this.value = 0;
				}
				else if (value > 100)
				{
					this.value = 100;
				}
				else
				{
					this.value = value;
				}
				this.Refresh();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000483C File Offset: 0x00002A3C
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004844 File Offset: 0x00002A44
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.backBrush = new SolidBrush(value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004859 File Offset: 0x00002A59
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004861 File Offset: 0x00002A61
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				this.foreBrush = new SolidBrush(value);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004878 File Offset: 0x00002A78
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.FillRectangle(this.backBrush, 0, 0, base.Width, base.Height);
			graphics.FillRectangle(this.foreBrush, 0, 0, (int)((double)((float)this.Value) / 100.0 * (double)((float)base.Width)), base.Height);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000048DD File Offset: 0x00002ADD
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000048FC File Offset: 0x00002AFC
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleMode = AutoScaleMode.None;
			this.BackColor = Color.FromArgb(198, 198, 198);
			this.DoubleBuffered = true;
			this.ForeColor = Color.FromArgb(0, 114, 198);
			base.Margin = new Padding(0);
			base.Name = "CustomProgressBar";
			base.Size = new Size(350, 20);
			base.ResumeLayout(false);
		}

		// Token: 0x0400003E RID: 62
		private int value;

		// Token: 0x0400003F RID: 63
		private SolidBrush backBrush;

		// Token: 0x04000040 RID: 64
		private SolidBrush foreBrush;

		// Token: 0x04000041 RID: 65
		private IContainer components;
	}
}
