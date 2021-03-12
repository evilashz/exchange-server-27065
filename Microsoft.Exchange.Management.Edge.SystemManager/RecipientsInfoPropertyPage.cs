using System;
using System.ComponentModel;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000018 RID: 24
	public class RecipientsInfoPropertyPage : DataListPropertyPage
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00006668 File Offset: 0x00004868
		public RecipientsInfoPropertyPage()
		{
			this.InitializeComponent();
			base.DataListControl.DataListView.AutoGenerateColumns = false;
			base.DataListControl.DataListView.AvailableColumns.Add("Address", Strings.RecipientInfoAddressColumn, true);
			base.DataListControl.DataListView.AvailableColumns.Add("Status", Strings.StatusColumn, true);
			base.DataListControl.DataListView.AvailableColumns.Add("LastError", Strings.LastErrorColumn, true);
			base.DataListControl.DataListView.SortProperty = "Address";
			this.Text = Strings.RecipientInfoPageTitle;
			base.BindingSource.DataSource = typeof(MessageInfo);
			base.DataListControl.DataBindings.Add("DataSource", base.BindingSource, "Recipients");
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006760 File Offset: 0x00004960
		private void InitializeComponent()
		{
			((ISupportInitialize)base.BindingSource).BeginInit();
			base.SuspendLayout();
			base.InputValidationProvider.SetEnabled(base.BindingSource, true);
			base.Name = "RecipientsInfoPropertyPage";
			((ISupportInitialize)base.BindingSource).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
