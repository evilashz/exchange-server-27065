using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001EE RID: 494
	public class FooteredFeatureLauncherPropertyControl : FeatureLauncherPropertyControl
	{
		// Token: 0x0600163F RID: 5695 RVA: 0x0005C098 File Offset: 0x0005A298
		public FooteredFeatureLauncherPropertyControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0005C0A8 File Offset: 0x0005A2A8
		protected override void UpdateStatusWhenSelectionChanged()
		{
			base.UpdateStatusWhenSelectionChanged();
			FooteredFeatureLauncherListViewItem footeredFeatureLauncherListViewItem = base.CurrentFeature as FooteredFeatureLauncherListViewItem;
			if (footeredFeatureLauncherListViewItem != null)
			{
				this.iconedInfoControl.Text = footeredFeatureLauncherListViewItem.FooterDescription;
				this.iconedInfoControl.IconImage = footeredFeatureLauncherListViewItem.FooterBitmap;
				this.enterpriseTableLayoutPanel.Visible = true;
				return;
			}
			this.enterpriseTableLayoutPanel.Visible = false;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0005C108 File Offset: 0x0005A308
		public override Size GetPreferredSize(Size proposedSize)
		{
			Size preferredSize = base.GetPreferredSize(proposedSize);
			Size preferredSize2 = this.enterpriseTableLayoutPanel.GetPreferredSize(proposedSize);
			return new Size(preferredSize.Width, preferredSize.Height + preferredSize2.Height);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0005C148 File Offset: 0x0005A348
		private void InitializeComponent()
		{
			this.enterpriseTableLayoutPanel = new AutoTableLayoutPanel();
			this.iconedInfoControl = new IconedInfoControl();
			this.enterpriseTableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.enterpriseTableLayoutPanel.AutoLayout = true;
			this.enterpriseTableLayoutPanel.AutoSize = true;
			this.enterpriseTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.enterpriseTableLayoutPanel.ColumnCount = 1;
			this.enterpriseTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.enterpriseTableLayoutPanel.Controls.Add(this.iconedInfoControl, 0, 0);
			this.enterpriseTableLayoutPanel.Dock = DockStyle.Top;
			this.enterpriseTableLayoutPanel.Location = new Point(0, 259);
			this.enterpriseTableLayoutPanel.Margin = new Padding(0);
			this.enterpriseTableLayoutPanel.Name = "enterpriseTableLayoutPanel";
			this.enterpriseTableLayoutPanel.Padding = new Padding(0, 12, 0, 0);
			this.enterpriseTableLayoutPanel.RowCount = 1;
			this.enterpriseTableLayoutPanel.RowStyles.Add(new RowStyle());
			this.enterpriseTableLayoutPanel.Size = new Size(418, 34);
			this.enterpriseTableLayoutPanel.TabIndex = 1;
			this.enterpriseTableLayoutPanel.Visible = false;
			this.iconedInfoControl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.iconedInfoControl.Location = new Point(0, 12);
			this.iconedInfoControl.Margin = new Padding(0);
			this.iconedInfoControl.Name = "iconedInfoControl";
			this.iconedInfoControl.Size = new Size(418, 22);
			this.iconedInfoControl.TabIndex = 0;
			base.Controls.Add(this.enterpriseTableLayoutPanel);
			base.Name = "FooteredFeatureLauncherPropertyControl";
			base.Size = new Size(418, 363);
			base.Controls.SetChildIndex(this.enterpriseTableLayoutPanel, 0);
			this.enterpriseTableLayoutPanel.ResumeLayout(false);
			this.enterpriseTableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400081B RID: 2075
		private AutoTableLayoutPanel enterpriseTableLayoutPanel;

		// Token: 0x0400081C RID: 2076
		private IconedInfoControl iconedInfoControl;
	}
}
