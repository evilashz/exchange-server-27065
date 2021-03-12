using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E2 RID: 226
	public class RbacPermissionLockResultPane : ResultPane
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x0001C7A8 File Offset: 0x0001A9A8
		public RbacPermissionLockResultPane()
		{
			base.ViewModeCommands.Add(Theme.VisualEffectsCommands);
			base.EnableVisualEffects = true;
			this.InitializeComponent();
			this.titleImage.Image = IconLibrary.ToBitmap(Icons.LockIcon, this.titleImage.Size);
			this.labelTitle.Text = Strings.NotPermittedByRbac;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001C810 File Offset: 0x0001AA10
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.labelTitle = new Label();
			this.titleImage = new Label();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.BackColor = Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.labelTitle, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.titleImage, 0, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Top;
			this.tableLayoutPanel1.Location = new Point(12, 5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new Padding(0, 0, 0, 12);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.Size = new Size(126, 60);
			this.tableLayoutPanel1.TabIndex = 2;
			this.labelTitle.AutoSize = true;
			this.labelTitle.Dock = DockStyle.Fill;
			this.labelTitle.Location = new Point(57, 0);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new Size(66, 48);
			this.labelTitle.TabIndex = 1;
			this.labelTitle.TextAlign = ContentAlignment.MiddleLeft;
			this.titleImage.Location = new Point(3, 0);
			this.titleImage.Name = "titleImage";
			this.titleImage.Size = new Size(32, 32);
			this.titleImage.TabIndex = 2;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "RbacPermissionLockResultPane";
			base.Padding = new Padding(12, 5, 12, 5);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001CA66 File Offset: 0x0001AC66
		public override string SelectionHelpTopic
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040003E4 RID: 996
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040003E5 RID: 997
		private Label titleImage;

		// Token: 0x040003E6 RID: 998
		private Label labelTitle;
	}
}
