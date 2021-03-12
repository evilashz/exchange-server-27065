using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003B5 RID: 949
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("ListSearchReport", "Microsoft.Exchange.Management.ControlPanel.Client.AuditReports.js")]
	public class AuditReportDataPage : AuditReportPage, IScriptControl
	{
		// Token: 0x060031B8 RID: 12728 RVA: 0x000999C8 File Offset: 0x00097BC8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			base.CommitButton.Visible = false;
			if (!base.IsPostBack)
			{
				Control contentPanel = base.ContentPanel;
				this.objectIds = (PickerControl)contentPanel.FindControl("objectIds");
				this.SetupFilterBindings();
			}
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00099A14 File Offset: 0x00097C14
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
			if (this.objectIds is RoleGroupPickerControl)
			{
				descriptor.AddComponentProperty("RoleGroupPicker", this.objectIds.ClientID, true);
				return;
			}
			descriptor.AddComponentProperty("RecipientPicker", this.objectIds.ClientID, true);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x00099B28 File Offset: 0x00097D28
		private void SetupFilterBindings()
		{
			BindingCollection filterParameters = this.dataSource.FilterParameters;
			ClientControlBinding clientControlBinding = new ComponentBinding(this.dcStartDate, "value");
			clientControlBinding.Name = "StartDate";
			ClientControlBinding clientControlBinding2 = new ComponentBinding(this.dcEndDate, "value");
			clientControlBinding2.Name = "EndDate";
			ClientControlBinding clientControlBinding3 = new ComponentBinding(this.objectIds, "value");
			clientControlBinding3.Name = "ObjectIds";
			filterParameters.Add(clientControlBinding);
			filterParameters.Add(clientControlBinding2);
			filterParameters.Add(clientControlBinding3);
		}

		// Token: 0x0400241D RID: 9245
		protected WebServiceListSource dataSource;

		// Token: 0x0400241E RID: 9246
		protected HtmlButton searchButton;

		// Token: 0x0400241F RID: 9247
		protected HtmlButton clearButton;

		// Token: 0x04002420 RID: 9248
		protected ListView listViewSearchResults;

		// Token: 0x04002421 RID: 9249
		protected PickerControl objectIds;
	}
}
