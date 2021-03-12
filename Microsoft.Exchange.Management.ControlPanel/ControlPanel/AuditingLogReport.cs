using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003B2 RID: 946
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("ListSearchReport", "Microsoft.Exchange.Management.ControlPanel.Client.AuditReports.js")]
	public class AuditingLogReport : AuditReportPage, IScriptControl
	{
		// Token: 0x060031A3 RID: 12707 RVA: 0x000992B2 File Offset: 0x000974B2
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			base.CommitButton.Visible = false;
			if (!base.IsPostBack)
			{
				this.SetupFilterBindings();
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000992D8 File Offset: 0x000974D8
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

		// Token: 0x060031A5 RID: 12709 RVA: 0x000993B0 File Offset: 0x000975B0
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

		// Token: 0x04002414 RID: 9236
		protected WebServiceListSource dataSource;

		// Token: 0x04002415 RID: 9237
		protected HtmlButton searchButton;

		// Token: 0x04002416 RID: 9238
		protected HtmlButton clearButton;

		// Token: 0x04002417 RID: 9239
		protected ListView listViewSearchResults;
	}
}
