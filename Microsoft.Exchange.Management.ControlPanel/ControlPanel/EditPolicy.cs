using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001C2 RID: 450
	[RequiredScript(typeof(WizardForm))]
	[ClientScriptResource("EditPolicy", "Microsoft.Exchange.Management.ControlPanel.Client.DLPPolicy.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class EditPolicy : BaseForm
	{
		// Token: 0x0600245D RID: 9309 RVA: 0x0006FD14 File Offset: 0x0006DF14
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			PropertyPageSheet propertyPageSheet = (PropertyPageSheet)base.ContentPanel.FindControl("policyPropertyPage");
			ListView listView = (ListView)propertyPageSheet.Sections["rules"].FindControl("rulesListView");
			descriptor.AddComponentProperty("RulesListView", listView.ClientID, true);
			descriptor.AddComponentProperty("RulesDataSourceRefreshMethod", this.rulesdatasource.RefreshWebServiceMethod.ClientID);
			descriptor.AddProperty("DLPPolicyParameterName", "DlpPolicy");
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04001E70 RID: 7792
		protected WebServiceListSource rulesdatasource;
	}
}
