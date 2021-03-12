using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003ED RID: 1005
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("ListSearchReport", "Microsoft.Exchange.Management.ControlPanel.Client.AuditReports.js")]
	public class NonOwnerAccessReport : AuditReportDataPage, IScriptControl
	{
		// Token: 0x0600336A RID: 13162 RVA: 0x0009FE04 File Offset: 0x0009E004
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.IsPostBack)
			{
				Control contentPanel = base.ContentPanel;
				this.logonType = (DropDownList)contentPanel.FindControl("logonType");
				this.SetupFilterBindings();
			}
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x0009FE44 File Offset: 0x0009E044
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("LogonTypes", this.logonType.ClientID, true);
			descriptor.AddProperty("DefaultSelectedLogonTypeIndex", this.logonType.SelectedIndex.ToString());
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x0009FE90 File Offset: 0x0009E090
		private void SetupFilterBindings()
		{
			ClientControlBinding clientControlBinding = new ClientControlBinding(this.logonType, "value");
			clientControlBinding.Name = "LogonTypes";
			this.dataSource.FilterParameters.Add(clientControlBinding);
		}

		// Token: 0x040024EA RID: 9450
		private DropDownList logonType;
	}
}
