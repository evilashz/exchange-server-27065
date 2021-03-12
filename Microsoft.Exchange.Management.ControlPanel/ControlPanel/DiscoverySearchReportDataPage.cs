using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003C1 RID: 961
	[ClientScriptResource("ListSearchReport", "Microsoft.Exchange.Management.ControlPanel.Client.AuditReports.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class DiscoverySearchReportDataPage : AuditReportPage, IScriptControl
	{
		// Token: 0x0600320E RID: 12814 RVA: 0x0009B964 File Offset: 0x00099B64
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			base.CommitButton.Visible = false;
			if (!base.IsPostBack)
			{
				Panel contentPanel = base.ContentPanel;
				this.SetupFilterBindings();
			}
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x0009B990 File Offset: 0x00099B90
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("StartDate", this.dcStartDate.ClientID, true);
			descriptor.AddComponentProperty("EndDate", this.dcEndDate.ClientID, true);
			descriptor.AddProperty("DefaultStartDate", this.dcStartDate.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture));
			descriptor.AddProperty("DefaultEndDate", this.dcEndDate.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture));
			descriptor.AddElementProperty("SearchButton", this.searchButton.ClientID, true);
			descriptor.AddElementProperty("ClearButton", this.clearButton.ClientID, true);
			descriptor.AddComponentProperty("ListViewDataSource", this.dataSource.ClientID, true);
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x0009BA68 File Offset: 0x00099C68
		private void SetupFilterBindings()
		{
			BindingCollection filterParameters = this.dataSource.FilterParameters;
			ClientControlBinding clientControlBinding = new ComponentBinding(this.dcStartDate, "value");
			clientControlBinding.Name = "StartDate";
			ClientControlBinding clientControlBinding2 = new ComponentBinding(this.dcEndDate, "value");
			clientControlBinding2.Name = "EndDate";
			filterParameters.Add(clientControlBinding);
			filterParameters.Add(clientControlBinding2);
		}

		// Token: 0x0400246A RID: 9322
		protected WebServiceListSource dataSource;

		// Token: 0x0400246B RID: 9323
		protected HtmlButton searchButton;

		// Token: 0x0400246C RID: 9324
		protected HtmlButton clearButton;

		// Token: 0x0400246D RID: 9325
		protected Microsoft.Exchange.Management.ControlPanel.WebControls.ListView listViewSearchResults;
	}
}
