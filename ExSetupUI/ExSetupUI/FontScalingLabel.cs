using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200001C RID: 28
	public class FontScalingLabel : UserControl
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00007D35 File Offset: 0x00005F35
		public FontScalingLabel()
		{
			this.InitializeComponent();
			this.PreferredFontSize = 12f;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.TextAlign = ContentAlignment.TopLeft;
		}

		// Token: 0x17000037 RID: 55
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00007D5E File Offset: 0x00005F5E
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			set
			{
				base.Text = value;
				this.FindBestFitFont();
				base.Invalidate();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007D73 File Offset: 0x00005F73
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00007D7B File Offset: 0x00005F7B
		public ContentAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				this.textAlign = value;
				this.FindBestFitFont();
				base.Invalidate();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007D90 File Offset: 0x00005F90
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00007D98 File Offset: 0x00005F98
		public float PreferredFontSize { get; set; }

		// Token: 0x0600015B RID: 347 RVA: 0x00007DA1 File Offset: 0x00005FA1
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007DC0 File Offset: 0x00005FC0
		private void OnClientSizeChanged(object sender, EventArgs e)
		{
			this.FindBestFitFont();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007DC8 File Offset: 0x00005FC8
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			TextRenderer.DrawText(e.Graphics, this.Text, this.Font, base.ClientRectangle, this.ForeColor, this.GetTextFormatFlag());
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007DFC File Offset: 0x00005FFC
		private void FindBestFitFont()
		{
			Font font = (this.Font.Size == this.PreferredFontSize) ? this.Font : new Font(this.Font.FontFamily, this.PreferredFontSize, this.Font.Style, GraphicsUnit.Pixel);
			Font font2 = font;
			Size size = TextRenderer.MeasureText(this.Text, font);
			while (size.Width > base.Size.Width || size.Height > base.Size.Height)
			{
				float num = (float)base.Size.Height / (float)size.Height;
				float num2 = (float)base.Size.Width / (float)size.Width;
				float num3 = (num < num2) ? num : num2;
				font2 = font;
				font = new Font(font.Name, font.Size * num3, font.Style);
				size = TextRenderer.MeasureText(this.Text, font, base.Size, this.GetTextFormatFlag());
			}
			this.Font = font2;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007F0C File Offset: 0x0000610C
		private TextFormatFlags GetTextFormatFlag()
		{
			TextFormatFlags textFormatFlags;
			if (this.textAlign == ContentAlignment.TopCenter)
			{
				textFormatFlags = TextFormatFlags.HorizontalCenter;
			}
			else if (this.textAlign == ContentAlignment.TopRight)
			{
				textFormatFlags = TextFormatFlags.Right;
			}
			else if (this.textAlign == ContentAlignment.MiddleLeft)
			{
				textFormatFlags = TextFormatFlags.VerticalCenter;
			}
			else if (this.textAlign == ContentAlignment.MiddleCenter)
			{
				textFormatFlags = (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			}
			else if (this.textAlign == ContentAlignment.MiddleRight)
			{
				textFormatFlags = (TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
			}
			else if (this.textAlign == ContentAlignment.BottomLeft)
			{
				textFormatFlags = TextFormatFlags.Bottom;
			}
			else if (this.textAlign == ContentAlignment.BottomCenter)
			{
				textFormatFlags = (TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter);
			}
			else if (this.textAlign == ContentAlignment.BottomRight)
			{
				textFormatFlags = (TextFormatFlags.Bottom | TextFormatFlags.Right);
			}
			else
			{
				textFormatFlags = TextFormatFlags.Default;
			}
			return textFormatFlags | TextFormatFlags.WordBreak;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007F9C File Offset: 0x0000619C
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			this.DoubleBuffered = true;
			this.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			base.Name = "FontScalingLabel";
			base.Size = new Size(215, 46);
			base.ClientSizeChanged += this.OnClientSizeChanged;
			base.ResumeLayout(false);
		}

		// Token: 0x040000B0 RID: 176
		private IContainer components;

		// Token: 0x040000B1 RID: 177
		private ContentAlignment textAlign;
	}
}
