using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000010 RID: 16
	public class CustomRichTextBox : UserControl
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00005D18 File Offset: 0x00003F18
		public CustomRichTextBox()
		{
			this.InitializeComponent();
			this.customScrollbar.Left = this.richTextBox.Right - this.customScrollbar.Width;
			this.customScrollbar.Size = new Size(this.customScrollbar.Size.Width, this.richTextBox.Size.Height);
		}

		// Token: 0x17000029 RID: 41
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00005D89 File Offset: 0x00003F89
		public LinkClickedEventHandler LinkClicked
		{
			set
			{
				this.richTextBox.LinkClicked += value;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005D97 File Offset: 0x00003F97
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005DB6 File Offset: 0x00003FB6
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005DC3 File Offset: 0x00003FC3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public override string Text
		{
			get
			{
				return this.richTextBox.Text;
			}
			set
			{
				this.richTextBox.Text = value;
				this.Refresh();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005DD7 File Offset: 0x00003FD7
		public void LoadFile(string path)
		{
			this.richTextBox.LoadFile(path);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public void InitializeCustomScrollbar()
		{
			this.customScrollbar.InitializeScrollbar(this.richTextBox.Handle, this.richTextBox.Height, this.richTextBox.AutoScrollOffset.Y);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005E29 File Offset: 0x00004029
		private void CustomScrollbarScroll(object sender, EventArgs e)
		{
			this.customScrollbar.CustomScroll(this.richTextBox.Handle, this.customScrollbar.Value);
			this.customScrollbar.Invalidate();
			Application.DoEvents();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005E5C File Offset: 0x0000405C
		private void RichTextBoxContentsResized(object sender, ContentsResizedEventArgs e)
		{
			this.customScrollbar.Visible = (e.NewRectangle.Height > this.richTextBox.Height);
			if (this.customScrollbar.Visible)
			{
				this.customScrollbar.Maximum = e.NewRectangle.Height;
				this.customScrollbar.LargeChange = this.richTextBox.Height;
				this.customScrollbar.SmallChange = 12;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005ED8 File Offset: 0x000040D8
		private void RichTextBoxResize(object sender, EventArgs e)
		{
			this.customScrollbar.Left = this.richTextBox.Right - this.customScrollbar.Width;
			this.customScrollbar.Top = this.richTextBox.Top;
			this.customScrollbar.Size = new Size(this.customScrollbar.Size.Width, this.richTextBox.Size.Height);
			this.richTextBox.RightMargin = this.richTextBox.Width - 15;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005F6C File Offset: 0x0000416C
		private void RichTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			switch (keyCode)
			{
			case Keys.Prior:
				this.customScrollbar.MoveScrollbar(this.richTextBox.Handle, 2);
				return;
			case Keys.Next:
				this.customScrollbar.MoveScrollbar(this.richTextBox.Handle, 3);
				break;
			default:
				switch (keyCode)
				{
				case Keys.Up:
					this.customScrollbar.MoveScrollbar(this.richTextBox.Handle, 0);
					return;
				case Keys.Right:
					break;
				case Keys.Down:
					this.customScrollbar.MoveScrollbar(this.richTextBox.Handle, 1);
					return;
				default:
					return;
				}
				break;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006008 File Offset: 0x00004208
		private void InitializeComponent()
		{
			this.leftSpacerPanel = new Panel();
			this.richTextBox = new RichTextBox();
			this.customScrollbar = new CustomScrollbar();
			base.SuspendLayout();
			this.leftSpacerPanel.BackColor = Color.FromArgb(224, 224, 224);
			this.leftSpacerPanel.Dock = DockStyle.Left;
			this.leftSpacerPanel.Location = new Point(0, 0);
			this.leftSpacerPanel.Name = "leftSpacerPanel";
			this.leftSpacerPanel.Size = new Size(15, 300);
			this.leftSpacerPanel.TabIndex = 20;
			this.richTextBox.AcceptsTab = true;
			this.richTextBox.BackColor = Color.FromArgb(224, 224, 224);
			this.richTextBox.BorderStyle = BorderStyle.None;
			this.richTextBox.BulletIndent = 30;
			this.richTextBox.Dock = DockStyle.Fill;
			this.richTextBox.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.richTextBox.ForeColor = Color.FromArgb(51, 51, 51);
			this.richTextBox.Location = new Point(15, 0);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.ReadOnly = true;
			this.richTextBox.RightMargin = 695;
			this.richTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.richTextBox.Size = new Size(706, 300);
			this.richTextBox.TabIndex = 18;
			this.richTextBox.TabStop = false;
			this.richTextBox.Text = "[RichBoxText]";
			this.richTextBox.ContentsResized += this.RichTextBoxContentsResized;
			this.richTextBox.Resize += this.RichTextBoxResize;
			this.richTextBox.KeyDown += this.RichTextBoxKeyDown;
			this.customScrollbar.BackColor = Color.FromArgb(224, 224, 224);
			this.customScrollbar.ChannelColor = Color.Transparent;
			this.customScrollbar.ForeColor = SetupWizardPage.DefaultNormalColor;
			this.customScrollbar.LargeChange = 10;
			this.customScrollbar.Location = new Point(705, 92);
			this.customScrollbar.Maximum = 100;
			this.customScrollbar.Minimum = 0;
			this.customScrollbar.MinimumSize = new Size(16, 88);
			this.customScrollbar.Name = "customScrollbar";
			this.customScrollbar.Size = new Size(16, 208);
			this.customScrollbar.SmallChange = 1;
			this.customScrollbar.TabIndex = 19;
			this.customScrollbar.TabStop = false;
			this.customScrollbar.Value = 0;
			this.customScrollbar.Scroll += this.CustomScrollbarScroll;
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.customScrollbar);
			base.Controls.Add(this.richTextBox);
			base.Controls.Add(this.leftSpacerPanel);
			this.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			base.Margin = new Padding(0);
			base.Name = "CustomRichTextBox";
			base.Size = new Size(721, 300);
			base.ResumeLayout(false);
		}

		// Token: 0x04000064 RID: 100
		private Panel leftSpacerPanel;

		// Token: 0x04000065 RID: 101
		private CustomScrollbar customScrollbar;

		// Token: 0x04000066 RID: 102
		private RichTextBox richTextBox;

		// Token: 0x04000067 RID: 103
		private IContainer components;
	}
}
