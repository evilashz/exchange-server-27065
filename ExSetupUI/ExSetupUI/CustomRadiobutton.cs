using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200000F RID: 15
	public class CustomRadiobutton : UserControl
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000BF RID: 191 RVA: 0x000056CC File Offset: 0x000038CC
		// (remove) Token: 0x060000C0 RID: 192 RVA: 0x00005704 File Offset: 0x00003904
		public event EventHandler CheckedChanged;

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005739 File Offset: 0x00003939
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00005741 File Offset: 0x00003941
		public int TextGap { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000574A File Offset: 0x0000394A
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00005752 File Offset: 0x00003952
		public Color NormalColor { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000575B File Offset: 0x0000395B
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005763 File Offset: 0x00003963
		public Color DisabledColor { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000576C File Offset: 0x0000396C
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00005774 File Offset: 0x00003974
		public Color HighlightedColor { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000577D File Offset: 0x0000397D
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000578A File Offset: 0x0000398A
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get
			{
				return this.label.Text;
			}
			set
			{
				this.label.Text = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005798 File Offset: 0x00003998
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000057A0 File Offset: 0x000039A0
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
					this.glyph.Highligted = this.highlighted;
					this.SetForegroundColor();
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000057CF File Offset: 0x000039CF
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000057DC File Offset: 0x000039DC
		public bool Checked
		{
			get
			{
				return this.glyph.Checked;
			}
			set
			{
				this.glyph.Checked = value;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000057EC File Offset: 0x000039EC
		public CustomRadiobutton()
		{
			this.TextGap = 10;
			this.NormalColor = SetupWizardPage.DefaultNormalColor;
			this.HighlightedColor = SetupWizardPage.DefaultHighlightColor;
			this.DisabledColor = SetupWizardPage.DefaultDisabledColor;
			this.InitializeComponent();
			this.glyph.Deselectable = false;
			this.OnResize(null, null);
			this.SetForegroundColor();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005848 File Offset: 0x00003A48
		private void GlyphCheckedChanged(object sender, EventArgs e)
		{
			EventHandler checkedChanged = this.CheckedChanged;
			if (checkedChanged != null)
			{
				checkedChanged(this, e);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005868 File Offset: 0x00003A68
		private void OnResize(object sender, EventArgs e)
		{
			this.glyph.Top = (base.Height - this.glyph.Height) / 2;
			this.label.Left = this.glyph.Right + this.TextGap;
			this.label.Width = base.Width - this.label.Left - 1;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000058D0 File Offset: 0x00003AD0
		protected void SetForegroundColor()
		{
			if (!base.Enabled)
			{
				this.ForeColor = this.DisabledColor;
			}
			else
			{
				this.ForeColor = (this.Highligted ? this.HighlightedColor : this.NormalColor);
			}
			this.label.ForeColor = this.ForeColor;
			base.Invalidate(true);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005927 File Offset: 0x00003B27
		private void OnStateChanged(object sender, EventArgs e)
		{
			this.SetForegroundColor();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000592F File Offset: 0x00003B2F
		private void OnEnter(object sender, EventArgs e)
		{
			this.Highligted = true;
			this.glyph.Highligted = true;
			this.SetForegroundColor();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000594A File Offset: 0x00003B4A
		private void OnLeave(object sender, EventArgs e)
		{
			this.Highligted = false;
			this.glyph.Highligted = false;
			this.SetForegroundColor();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005965 File Offset: 0x00003B65
		private void OnClick(object sender, EventArgs e)
		{
			this.glyph.Checked = true;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005973 File Offset: 0x00003B73
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				this.glyph.Checked = true;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000598B File Offset: 0x00003B8B
		protected override bool ProcessMnemonic(char charCode)
		{
			if (base.CanSelect && Control.IsMnemonic(charCode, this.Text))
			{
				this.glyph.Checked = true;
				return true;
			}
			return false;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000059B2 File Offset: 0x00003BB2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000059D4 File Offset: 0x00003BD4
		private void InitializeComponent()
		{
			this.label = new FontScalingLabel();
			this.glyph = new CustomGlyph();
			base.SuspendLayout();
			this.label.BackColor = Color.Transparent;
			this.label.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.label.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.label.Location = new Point(25, 0);
			this.label.Margin = new Padding(3, 0, 3, 0);
			this.label.Name = "label";
			this.label.PreferredFontSize = 12f;
			this.label.Size = new Size(213, 19);
			this.label.TabIndex = 1;
			this.label.TabStop = false;
			this.label.Text = "CustomRadiobutton Text";
			this.label.TextAlign = ContentAlignment.TopLeft;
			this.label.Click += this.OnClick;
			this.label.KeyDown += this.OnKeyDown;
			this.glyph.BackColor = Color.Transparent;
			this.glyph.Checked = false;
			this.glyph.Deselectable = true;
			this.glyph.DisabledGlyphColor = SetupWizardPage.DefaultDisabledColor;
			this.glyph.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.glyph.HighlightedGlyphColor = SetupWizardPage.DefaultHighlightColor;
			this.glyph.Highligted = false;
			this.glyph.Location = new Point(0, 0);
			this.glyph.Margin = new Padding(3, 0, 3, 0);
			this.glyph.MaximumSize = new Size(16, 16);
			this.glyph.MinimumSize = new Size(16, 16);
			this.glyph.Name = "glyph";
			this.glyph.NormalGlyphColor = SetupWizardPage.DefaultNormalColor;
			this.glyph.Size = new Size(16, 16);
			this.glyph.Style = CustomGlyph.GlyphStyle.RadioButton;
			this.glyph.TabIndex = 0;
			this.glyph.CheckedChanged += this.GlyphCheckedChanged;
			this.glyph.KeyDown += this.OnKeyDown;
			base.AutoScaleMode = AutoScaleMode.None;
			this.AutoSize = true;
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.label);
			base.Controls.Add(this.glyph);
			this.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			base.Name = "CustomRadiobutton";
			base.Size = new Size(340, 19);
			base.EnabledChanged += this.OnStateChanged;
			base.Click += this.OnClick;
			base.Enter += this.OnEnter;
			base.KeyDown += this.OnKeyDown;
			base.Leave += this.OnLeave;
			base.Resize += this.OnResize;
			base.ResumeLayout(false);
		}

		// Token: 0x0400005C RID: 92
		private bool highlighted;

		// Token: 0x0400005D RID: 93
		private IContainer components;

		// Token: 0x0400005E RID: 94
		private CustomGlyph glyph;

		// Token: 0x0400005F RID: 95
		private FontScalingLabel label;
	}
}
