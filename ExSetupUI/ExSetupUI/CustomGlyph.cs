using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000008 RID: 8
	public sealed class CustomGlyph : UserControl
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600007B RID: 123 RVA: 0x00004280 File Offset: 0x00002480
		// (remove) Token: 0x0600007C RID: 124 RVA: 0x000042B8 File Offset: 0x000024B8
		public event EventHandler HighlightedChanged;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600007D RID: 125 RVA: 0x000042F0 File Offset: 0x000024F0
		// (remove) Token: 0x0600007E RID: 126 RVA: 0x00004328 File Offset: 0x00002528
		public event EventHandler CheckedChanged;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000435D File Offset: 0x0000255D
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00004365 File Offset: 0x00002565
		public Color NormalGlyphColor { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000436E File Offset: 0x0000256E
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00004376 File Offset: 0x00002576
		public Color DisabledGlyphColor { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000437F File Offset: 0x0000257F
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004387 File Offset: 0x00002587
		public Color HighlightedGlyphColor { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004390 File Offset: 0x00002590
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00004398 File Offset: 0x00002598
		public bool Deselectable { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000043A1 File Offset: 0x000025A1
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000043A9 File Offset: 0x000025A9
		public CustomGlyph.GlyphStyle Style
		{
			get
			{
				return this.glyphStyle;
			}
			set
			{
				this.glyphStyle = value;
				this.DrawGlyph();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000043B8 File Offset: 0x000025B8
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000043C0 File Offset: 0x000025C0
		public bool Highligted
		{
			get
			{
				return this.highlighted;
			}
			set
			{
				if (value != this.highlighted)
				{
					this.highlighted = value;
					this.OnHighlightedChanged(new EventArgs());
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000043DD File Offset: 0x000025DD
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000043E5 File Offset: 0x000025E5
		public bool Checked
		{
			get
			{
				return this.checkedVal;
			}
			set
			{
				if (value != this.checkedVal)
				{
					this.checkedVal = value;
					this.OnCheckedChanged(new EventArgs());
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004402 File Offset: 0x00002602
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000440A File Offset: 0x0000260A
		public Pen CurrentGlyphPen { get; private set; }

		// Token: 0x17000014 RID: 20
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00004413 File Offset: 0x00002613
		public override Color BackColor
		{
			set
			{
				base.BackColor = value;
				this.DrawGlyph();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004424 File Offset: 0x00002624
		public CustomGlyph()
		{
			this.fullsizeGlyphImage = new Bitmap(64, 64);
			this.fullsizeImageRectangle = new Rectangle(0, 0, 64, 64);
			this.Highligted = false;
			this.NormalGlyphColor = SetupWizardPage.DefaultNormalColor;
			this.HighlightedGlyphColor = SetupWizardPage.DefaultHighlightColor;
			this.DisabledGlyphColor = SetupWizardPage.DefaultDisabledColor;
			this.checkPen = new Pen(SetupWizardPage.DefaultNormalColor, 4f);
			this.BackColor = Color.Transparent;
			this.Style = CustomGlyph.GlyphStyle.RadioButton;
			this.InitializeComponent();
			this.Deselectable = true;
			this.DrawGlyph();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000044BC File Offset: 0x000026BC
		private void OnHighlightedChanged(EventArgs e)
		{
			EventHandler highlightedChanged = this.HighlightedChanged;
			if (highlightedChanged != null)
			{
				highlightedChanged(this, e);
			}
			this.DrawGlyph();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000044E4 File Offset: 0x000026E4
		private void OnCheckedChanged(EventArgs e)
		{
			EventHandler checkedChanged = this.CheckedChanged;
			if (checkedChanged != null)
			{
				checkedChanged(this, e);
			}
			this.DrawGlyph();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000450C File Offset: 0x0000270C
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
			graphics.DrawImage(this.fullsizeGlyphImage, base.ClientRectangle, this.fullsizeImageRectangle, GraphicsUnit.Pixel);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004548 File Offset: 0x00002748
		private void SetCurentGlyphPen()
		{
			if (!base.Enabled)
			{
				this.CurrentGlyphPen = new Pen(this.DisabledGlyphColor, 4f);
				return;
			}
			this.CurrentGlyphPen = (this.Highligted ? new Pen(this.HighlightedGlyphColor, 4f) : new Pen(this.NormalGlyphColor, 4f));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000045A4 File Offset: 0x000027A4
		private void DrawGlyph()
		{
			Graphics graphics = Graphics.FromImage(this.fullsizeGlyphImage);
			graphics.FillRectangle(Brushes.White, this.fullsizeImageRectangle);
			this.SetCurentGlyphPen();
			if (this.Style == CustomGlyph.GlyphStyle.CheckBox)
			{
				graphics.DrawRectangle(this.CurrentGlyphPen, 1, 1, this.fullsizeImageRectangle.Width - 4, this.fullsizeImageRectangle.Height - 4);
				if (this.checkedVal)
				{
					Pen pen = base.Enabled ? this.checkPen : this.CurrentGlyphPen;
					graphics.DrawLine(pen, 16, 28, 28, 36);
					graphics.DrawLine(pen, 28, 36, 48, 16);
				}
			}
			else
			{
				graphics.DrawEllipse(this.CurrentGlyphPen, 1, 1, this.fullsizeImageRectangle.Width - 4, this.fullsizeImageRectangle.Height - 4);
				if (this.checkedVal)
				{
					SolidBrush brush = base.Enabled ? new SolidBrush(this.checkPen.Color) : new SolidBrush(this.CurrentGlyphPen.Color);
					graphics.FillEllipse(brush, 12, 12, this.fullsizeImageRectangle.Width - 28, this.fullsizeImageRectangle.Height - 28);
				}
			}
			graphics.Dispose();
			base.Invalidate();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000046EE File Offset: 0x000028EE
		private void OnClick(object sender, EventArgs e)
		{
			this.Checked = (!this.Deselectable || !this.Checked);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000470A File Offset: 0x0000290A
		private void OnEnabledChanged(object sender, EventArgs e)
		{
			this.DrawGlyph();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004712 File Offset: 0x00002912
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004734 File Offset: 0x00002934
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			this.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.MaximumSize = new Size(16, 16);
			this.MinimumSize = new Size(16, 16);
			base.Name = "CustomGlyph";
			base.Size = new Size(16, 16);
			base.EnabledChanged += this.OnEnabledChanged;
			base.Click += this.OnClick;
			base.DoubleClick += this.OnClick;
			base.ResumeLayout(false);
		}

		// Token: 0x0400002F RID: 47
		private bool highlighted;

		// Token: 0x04000030 RID: 48
		private bool checkedVal;

		// Token: 0x04000031 RID: 49
		private readonly Pen checkPen;

		// Token: 0x04000032 RID: 50
		private CustomGlyph.GlyphStyle glyphStyle;

		// Token: 0x04000033 RID: 51
		private readonly Image fullsizeGlyphImage;

		// Token: 0x04000034 RID: 52
		private readonly Rectangle fullsizeImageRectangle;

		// Token: 0x04000035 RID: 53
		private IContainer components;

		// Token: 0x02000009 RID: 9
		public enum GlyphStyle
		{
			// Token: 0x0400003C RID: 60
			CheckBox,
			// Token: 0x0400003D RID: 61
			RadioButton
		}
	}
}
