using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001AF RID: 431
	public partial class BaseErrorDialog : ExchangeForm
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x00043A20 File Offset: 0x00041C20
		protected BaseErrorDialog()
		{
			this.InitializeComponent();
			this.iconBox.Image = IconLibrary.ToBitmap(Icons.Error, this.iconBox.Size);
			this.cancelButton.Text = Strings.Ok;
			this.messageLabel.UseMnemonic = false;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0004403A File Offset: 0x0004223A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys)131139)
			{
				this.CopyTechnicalDetails();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00044054 File Offset: 0x00042254
		private void CopyTechnicalDetails()
		{
			using (new ControlWaitCursor(this))
			{
				WinformsHelper.SetDataObjectToClipboard(this.TechnicalDetails, true);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00044094 File Offset: 0x00042294
		public virtual string TechnicalDetails
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0004409B File Offset: 0x0004229B
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x000440A8 File Offset: 0x000422A8
		public string Message
		{
			get
			{
				return this.messageLabel.Text;
			}
			set
			{
				this.messageLabel.Text = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x000440B6 File Offset: 0x000422B6
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x000440BE File Offset: 0x000422BE
		public bool IsWarningOnly
		{
			get
			{
				return this.isWarningOnly;
			}
			set
			{
				if (value != this.IsWarningOnly)
				{
					this.isWarningOnly = value;
					this.OnIsWarningOnlyChanged();
				}
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000440D6 File Offset: 0x000422D6
		protected virtual void OnIsWarningOnlyChanged()
		{
			this.iconBox.Image = (this.IsWarningOnly ? Icons.Warning : Icons.Error).ToBitmap();
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x000440FC File Offset: 0x000422FC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel ContentPanel
		{
			get
			{
				return this.contentPanel;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00044104 File Offset: 0x00042304
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableLayoutPanel ButtonsPanel
		{
			get
			{
				return this.buttonsPanel;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0004410C File Offset: 0x0004230C
		protected override string DefaultHelpTopic
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x040006AA RID: 1706
		private bool isWarningOnly;
	}
}
