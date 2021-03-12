using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D1 RID: 977
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("ListSearchReport", "Microsoft.Exchange.Management.ControlPanel.Client.AuditReports.js")]
	public class ExternalAccessReportDataPage : AuditReportPage, IScriptControl
	{
		// Token: 0x0600324B RID: 12875 RVA: 0x0009C438 File Offset: 0x0009A638
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

		// Token: 0x0600324C RID: 12876 RVA: 0x0009C464 File Offset: 0x0009A664
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

		// Token: 0x0600324D RID: 12877 RVA: 0x0009C53C File Offset: 0x0009A73C
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

		// Token: 0x04002481 RID: 9345
		protected WebServiceListSource dataSource;

		// Token: 0x04002482 RID: 9346
		protected HtmlButton searchButton;

		// Token: 0x04002483 RID: 9347
		protected HtmlButton clearButton;

		// Token: 0x04002484 RID: 9348
		protected Microsoft.Exchange.Management.ControlPanel.WebControls.ListView listViewSearchResults;
	}
}
