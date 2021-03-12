using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000007 RID: 7
	public class CustomCheckbox : UserControl
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600005F RID: 95 RVA: 0x00003BCC File Offset: 0x00001DCC
		// (remove) Token: 0x06000060 RID: 96 RVA: 0x00003C04 File Offset: 0x00001E04
		public event EventHandler CheckedChanged;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003C39 File Offset: 0x00001E39
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003C41 File Offset: 0x00001E41
		public int TextGap { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003C4A File Offset: 0x00001E4A
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003C52 File Offset: 0x00001E52
		public Color NormalColor { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003C5B File Offset: 0x00001E5B
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003C63 File Offset: 0x00001E63
		public Color DisabledColor { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003C6C File Offset: 0x00001E6C
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003C74 File Offset: 0x00001E74
		public Color HighlightedColor { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003C7D File Offset: 0x00001E7D
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003C8A File Offset: 0x00001E8A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public override string Text
		{
			get
			{
				return this.label.Text;
			}
			set
			{
				this.label.Text = value;
				this.OnResize(this, null);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003CA0 File Offset: 0x00001EA0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003CA8 File Offset: 0x00001EA8
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003CD7 File Offset: 0x00001ED7
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003CE4 File Offset: 0x00001EE4
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

		// Token: 0x0600006F RID: 111 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public CustomCheckbox()
		{
			this.TextGap = 10;
			this.NormalColor = Color.FromArgb(152, 163, 166);
			this.HighlightedColor = Color.FromArgb(125, 125, 125);
			this.DisabledColor = Color.FromArgb(221, 221, 221);
			this.InitializeComponent();
			this.OnResize(this, null);
			this.SetForegroundColor();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003D68 File Offset: 0x00001F68
		private void OnResize(object sender, EventArgs e)
		{
			this.glyph.Top = (base.Height - this.glyph.Height) / 2;
			this.label.Left = this.glyph.Right + this.TextGap;
			this.label.Width = base.Width - this.label.Left - 1;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003DD0 File Offset: 0x00001FD0
		private void GlyphCheckedChanged(object sender, EventArgs e)
		{
			EventHandler checkedChanged = this.CheckedChanged;
			if (checkedChanged != null)
			{
				checkedChanged(this, e);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003DF0 File Offset: 0x00001FF0
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

		// Token: 0x06000073 RID: 115 RVA: 0x00003E47 File Offset: 0x00002047
		private void OnStateChanged(object sender, EventArgs e)
		{
			this.SetForegroundColor();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003E4F File Offset: 0x0000204F
		private void OnEnter(object sender, EventArgs e)
		{
			this.Highligted = true;
			this.glyph.Highligted = true;
			this.SetForegroundColor();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E6A File Offset: 0x0000206A
		private void OnLeave(object sender, EventArgs e)
		{
			this.Highligted = false;
			this.glyph.Highligted = false;
			this.SetForegroundColor();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E85 File Offset: 0x00002085
		private void OnClick(object sender, EventArgs e)
		{
			this.glyph.Checked = !this.glyph.Checked;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003EA0 File Offset: 0x000020A0
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				this.glyph.Checked = !this.glyph.Checked;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003EC5 File Offset: 0x000020C5
		protected override bool ProcessMnemonic(char charCode)
		{
			if (base.CanSelect && Control.IsMnemonic(charCode, this.Text))
			{
				this.glyph.Checked = !this.glyph.Checked;
				return true;
			}
			return false;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003EF9 File Offset: 0x000020F9
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003F18 File Offset: 0x00002118
		private void InitializeComponent()
		{
			this.label = new FontScalingLabel();
			this.glyph = new CustomGlyph();
			base.SuspendLayout();
			this.label.AutoSize = true;
			this.label.BackColor = Color.Transparent;
			this.label.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.label.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.label.Location = new Point(25, 0);
			this.label.Margin = new Padding(3, 0, 3, 0);
			this.label.Name = "label";
			this.label.PreferredFontSize = 12f;
			this.label.Size = new Size(133, 19);
			this.label.TabIndex = 2;
			this.label.TabStop = false;
			this.label.Text = "Add some text";
			this.label.TextAlign = ContentAlignment.TopLeft;
			this.label.Click += this.OnClick;
			this.label.KeyDown += this.OnKeyDown;
			this.label.Resize += this.OnResize;
			this.glyph.BackColor = Color.Transparent;
			this.glyph.Checked = false;
			this.glyph.Deselectable = true;
			this.glyph.DisabledGlyphColor = SetupWizardPage.DefaultDisabledColor;
			this.glyph.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.glyph.HighlightedGlyphColor = SetupWizardPage.DefaultHighlightColor;
			this.glyph.Highligted = false;
			this.glyph.Location = new Point(0, 0);
			this.glyph.Margin = new Padding(3, 0, 3, 0);
			this.glyph.MaximumSize = new Size(16, 16);
			this.glyph.MinimumSize = new Size(16, 16);
			this.glyph.Name = "glyph";
			this.glyph.NormalGlyphColor = SetupWizardPage.DefaultNormalColor;
			this.glyph.Size = new Size(16, 16);
			this.glyph.Style = CustomGlyph.GlyphStyle.CheckBox;
			this.glyph.TabIndex = 1;
			this.glyph.CheckedChanged += this.GlyphCheckedChanged;
			this.glyph.KeyDown += this.OnKeyDown;
			this.glyph.Resize += this.OnResize;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.label);
			base.Controls.Add(this.glyph);
			base.Name = "CustomCheckbox";
			base.Size = new Size(162, 19);
			base.EnabledChanged += this.OnStateChanged;
			base.Click += this.OnClick;
			base.Enter += this.OnEnter;
			base.KeyDown += this.OnKeyDown;
			base.Leave += this.OnLeave;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000025 RID: 37
		private bool highlighted;

		// Token: 0x04000026 RID: 38
		private IContainer components;

		// Token: 0x04000027 RID: 39
		private CustomGlyph glyph;

		// Token: 0x04000028 RID: 40
		private FontScalingLabel label;
	}
}
