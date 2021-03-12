using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001BB RID: 443
	public class TitleWizardPage : WizardPage
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x00048EF2 File Offset: 0x000470F2
		public TitleWizardPage()
		{
			this.InitializeComponent();
			this.ShortDescription = "";
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00048F0C File Offset: 0x0004710C
		private void InitializeComponent()
		{
			this.shortDescriptionLabel = new AutoHeightLabel();
			this.contentPanel = new Panel();
			((ISupportInitialize)base.BindingSource).BeginInit();
			base.SuspendLayout();
			base.InputValidationProvider.SetEnabled(base.BindingSource, true);
			this.shortDescriptionLabel.Dock = DockStyle.Top;
			this.shortDescriptionLabel.Location = new Point(0, 0);
			this.shortDescriptionLabel.Margin = new Padding(3, 3, 16, 3);
			this.shortDescriptionLabel.Padding = new Padding(0, 0, 16, 0);
			this.shortDescriptionLabel.Name = "shortDescriptionLabel";
			this.shortDescriptionLabel.Size = new Size(456, 17);
			this.shortDescriptionLabel.TabIndex = 1;
			this.shortDescriptionLabel.Text = "[shortDescription]";
			this.contentPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.contentPanel.Dock = DockStyle.Fill;
			this.contentPanel.Location = new Point(0, 17);
			this.contentPanel.Margin = new Padding(0);
			this.contentPanel.Name = "contentPanel";
			this.contentPanel.Size = new Size(456, 385);
			this.contentPanel.TabIndex = 2;
			base.Controls.Add(this.contentPanel);
			base.Controls.Add(this.shortDescriptionLabel);
			base.Name = "TitleWizardPage";
			((ISupportInitialize)base.BindingSource).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00049094 File Offset: 0x00047294
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(0);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000490AA File Offset: 0x000472AA
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x0004909C File Offset: 0x0004729C
		[DefaultValue("")]
		public string ShortDescription
		{
			get
			{
				return this.shortDescriptionLabel.Text;
			}
			set
			{
				this.shortDescriptionLabel.Text = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x000490B7 File Offset: 0x000472B7
		public Panel ContentPanel
		{
			get
			{
				return this.contentPanel;
			}
		}

		// Token: 0x040006EC RID: 1772
		private AutoHeightLabel shortDescriptionLabel;

		// Token: 0x040006ED RID: 1773
		[AccessedThroughProperty("ContentPanel")]
		private Panel contentPanel;
	}
}
