using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001F7 RID: 503
	public class ImagedCheckBox : ExchangeUserControl
	{
		// Token: 0x060016C5 RID: 5829 RVA: 0x0005F9BC File Offset: 0x0005DBBC
		public ImagedCheckBox()
		{
			this.InitializeComponent();
			this.checkBox.CheckedChanged += delegate(object sender, EventArgs e)
			{
				this.OnCheckedChanged(e);
			};
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0005F9F4 File Offset: 0x0005DBF4
		private void InitializeComponent()
		{
			this.pictureBox = new ExchangePictureBox();
			this.checkBox = new AutoHeightCheckBox();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			((ISupportInitialize)this.pictureBox).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.pictureBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.pictureBox.Location = new Point(3, 0);
			this.pictureBox.Margin = new Padding(3, 0, 0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new Size(16, 16);
			this.pictureBox.TabIndex = 1;
			this.pictureBox.TabStop = false;
			this.checkBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.checkBox.CheckAlign = ContentAlignment.MiddleLeft;
			this.checkBox.Location = new Point(22, 1);
			this.checkBox.Margin = new Padding(3, 0, 0, 0);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new Size(300, 14);
			this.checkBox.TabIndex = 2;
			this.checkBox.TextAlign = ContentAlignment.MiddleLeft;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 19f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.pictureBox, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.checkBox, 1, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Top;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.Size = new Size(322, 16);
			this.tableLayoutPanel1.TabIndex = 3;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "ImagedCheckBox";
			base.Size = new Size(322, 17);
			((ISupportInitialize)this.pictureBox).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0005FC9B File Offset: 0x0005DE9B
		protected CheckBox CheckBoxControl
		{
			get
			{
				return this.checkBox;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0005FCA3 File Offset: 0x0005DEA3
		protected PictureBox PictureBoxControl
		{
			get
			{
				return this.pictureBox;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0005FCAB File Offset: 0x0005DEAB
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0005FCB8 File Offset: 0x0005DEB8
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(null)]
		public Image Image
		{
			get
			{
				return this.PictureBoxControl.Image;
			}
			set
			{
				this.PictureBoxControl.Image = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0005FCC6 File Offset: 0x0005DEC6
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0005FCD3 File Offset: 0x0005DED3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Browsable(true)]
		public string Description
		{
			get
			{
				return this.CheckBoxControl.Text;
			}
			set
			{
				if (value != this.Description)
				{
					this.CheckBoxControl.Text = (value ?? string.Empty);
				}
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0005FCF8 File Offset: 0x0005DEF8
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0005FD05 File Offset: 0x0005DF05
		[DefaultValue(false)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Checked
		{
			get
			{
				return this.CheckBoxControl.Checked;
			}
			set
			{
				this.CheckBoxControl.Checked = value;
			}
		}

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x060016CF RID: 5839 RVA: 0x0005FD14 File Offset: 0x0005DF14
		// (remove) Token: 0x060016D0 RID: 5840 RVA: 0x0005FD4C File Offset: 0x0005DF4C
		public event EventHandler CheckedChanged;

		// Token: 0x060016D1 RID: 5841 RVA: 0x0005FD81 File Offset: 0x0005DF81
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			if (this.CheckedChanged != null)
			{
				this.CheckedChanged(this, e);
			}
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0005FD98 File Offset: 0x0005DF98
		public override Size GetPreferredSize(Size proposedSize)
		{
			return new Size(proposedSize.Width, this.checkBox.Height);
		}

		// Token: 0x0400086A RID: 2154
		private ExchangePictureBox pictureBox;

		// Token: 0x0400086B RID: 2155
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x0400086C RID: 2156
		private AutoHeightCheckBox checkBox;
	}
}
