using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200020B RID: 523
	public class ResultPaneCaption : ExchangeUserControl
	{
		// Token: 0x060017B2 RID: 6066 RVA: 0x00063856 File Offset: 0x00061A56
		public ResultPaneCaption()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00063864 File Offset: 0x00061A64
		protected override Size DefaultSize
		{
			get
			{
				return new Size(387, 22);
			}
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00063872 File Offset: 0x00061A72
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00063884 File Offset: 0x00061A84
		private void UpdateFonts()
		{
			base.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			if (this.BaseFont != null)
			{
				this.labelDescription.Font = new Font(this.BaseFont.FontFamily, this.BaseFont.Size * FontHelper.GetScaleFactor(), this.BaseFont.Style);
				this.labelStatus.Font = new Font(this.labelDescription.Font.FontFamily, this.labelDescription.Font.Size);
			}
			else
			{
				this.labelDescription.Font = null;
				this.labelStatus.Font = null;
			}
			this.tableLayoutPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0006393E File Offset: 0x00061B3E
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x00063946 File Offset: 0x00061B46
		[DefaultValue(null)]
		public Font BaseFont
		{
			get
			{
				return this.baseFont;
			}
			set
			{
				if (this.baseFont != value)
				{
					this.baseFont = value;
					this.UpdateFonts();
				}
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0006395E File Offset: 0x00061B5E
		[Obsolete("Use BaseFont instead.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Font Font
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00063965 File Offset: 0x00061B65
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x00063970 File Offset: 0x00061B70
		[DefaultValue(null)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (this.Icon != value)
				{
					Bitmap image = IconLibrary.ToBitmap(value, this.pictureBox.Size);
					if (this.pictureBox.Image != null)
					{
						this.pictureBox.Image.Dispose();
					}
					this.pictureBox.Image = image;
					this.icon = value;
				}
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000639C8 File Offset: 0x00061BC8
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x000639D0 File Offset: 0x00061BD0
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				this.labelDescription.Text = value;
				base.Text = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000639F2 File Offset: 0x00061BF2
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x000639FF File Offset: 0x00061BFF
		[DefaultValue("")]
		public string Status
		{
			get
			{
				return this.labelStatus.Text;
			}
			set
			{
				this.labelStatus.Text = value;
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00063A10 File Offset: 0x00061C10
		private void InitializeComponent()
		{
			this.labelStatus = new Label();
			this.labelDescription = new Label();
			this.pictureBox = new ExchangePictureBox();
			this.tableLayoutPanel = new TableLayoutPanel();
			((ISupportInitialize)this.pictureBox).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.labelStatus.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.labelStatus.AutoSize = true;
			this.labelStatus.Location = new Point(384, 4);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new Size(1, 13);
			this.labelStatus.TabIndex = 2;
			this.labelStatus.TextAlign = ContentAlignment.MiddleRight;
			this.labelStatus.UseMnemonic = false;
			this.labelDescription.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.labelDescription.AutoEllipsis = true;
			this.labelDescription.ImageAlign = ContentAlignment.MiddleLeft;
			this.labelDescription.Location = new Point(22, 0);
			this.labelDescription.Margin = new Padding(0);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new Size(359, 22);
			this.labelDescription.TabIndex = 1;
			this.labelDescription.TextAlign = ContentAlignment.MiddleLeft;
			this.labelDescription.UseMnemonic = false;
			this.pictureBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.pictureBox.Location = new Point(3, 3);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new Size(16, 16);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel.Controls.Add(this.pictureBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelDescription, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.labelStatus, 2, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Size = new Size(387, 22);
			this.tableLayoutPanel.TabIndex = 3;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "ResultPaneCaption";
			base.Size = new Size(387, 22);
			((ISupportInitialize)this.pictureBox).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040008DB RID: 2267
		private Label labelStatus;

		// Token: 0x040008DC RID: 2268
		private Label labelDescription;

		// Token: 0x040008DD RID: 2269
		private ExchangePictureBox pictureBox;

		// Token: 0x040008DE RID: 2270
		private TableLayoutPanel tableLayoutPanel;

		// Token: 0x040008DF RID: 2271
		private Font baseFont;

		// Token: 0x040008E0 RID: 2272
		private Icon icon;
	}
}
