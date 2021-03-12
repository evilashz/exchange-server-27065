using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001F3 RID: 499
	public class IconedInfoControl : ExchangeUserControl
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x0005E5AB File Offset: 0x0005C7AB
		public IconedInfoControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005E5B9 File Offset: 0x0005C7B9
		public IconedInfoControl(Icon icon, string description) : this()
		{
			this.pictureBox.Image = IconLibrary.ToBitmap(icon, this.pictureBox.Size);
			this.infoLabel.Text = description;
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0005E5E9 File Offset: 0x0005C7E9
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x0005E5F6 File Offset: 0x0005C7F6
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override string Text
		{
			get
			{
				return this.infoLabel.Text;
			}
			set
			{
				this.infoLabel.Text = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0005E604 File Offset: 0x0005C804
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x0005E611 File Offset: 0x0005C811
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(null)]
		public Image IconImage
		{
			get
			{
				return this.pictureBox.Image;
			}
			set
			{
				this.pictureBox.Image = value;
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005E61F File Offset: 0x0005C81F
		public override Size GetPreferredSize(Size proposedSize)
		{
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0005E630 File Offset: 0x0005C830
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new TableLayoutPanel();
			this.pictureBox = new ExchangePictureBox();
			this.infoLabel = new Label();
			this.tableLayoutPanel.SuspendLayout();
			((ISupportInitialize)this.pictureBox).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.pictureBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.infoLabel, 1, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(644, 22);
			this.tableLayoutPanel.TabIndex = 0;
			this.pictureBox.Dock = DockStyle.Top;
			this.pictureBox.Location = new Point(3, 3);
			this.pictureBox.MinimumSize = new Size(16, 16);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new Size(16, 16);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.infoLabel.AutoSize = true;
			this.infoLabel.Dock = DockStyle.Top;
			this.infoLabel.ImageAlign = ContentAlignment.TopLeft;
			this.infoLabel.Location = new Point(20, 3);
			this.infoLabel.Margin = new Padding(0, 3, 0, 0);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new Size(624, 13);
			this.infoLabel.TabIndex = 1;
			this.infoLabel.Text = "infoLabel";
			this.AutoSize = true;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "IconedInfoControl";
			base.Size = new Size(644, 22);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((ISupportInitialize)this.pictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400084C RID: 2124
		private ExchangePictureBox pictureBox;

		// Token: 0x0400084D RID: 2125
		private Label infoLabel;

		// Token: 0x0400084E RID: 2126
		private TableLayoutPanel tableLayoutPanel;
	}
}
