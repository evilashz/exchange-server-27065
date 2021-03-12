using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200021F RID: 543
	[RequiredScript(typeof(WizardForm))]
	[ClientScriptResource("MessageTraceResults", "Microsoft.Exchange.Management.ControlPanel.Client.MessageTrace.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class MessageTraceResults : BaseForm
	{
		// Token: 0x0600275F RID: 10079 RVA: 0x0007BB90 File Offset: 0x00079D90
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ViewDetailsCommand viewDetailsCommand = (ViewDetailsCommand)this.listViewSearchResults.Commands.FindCommandByName("viewdetails");
			if (viewDetailsCommand != null)
			{
				viewDetailsCommand.NavigateUrl = EcpUrl.ProcessUrl(EcpUrl.EcpVDirForStaticResource + "MessageTrace/MessageTraceDetails.aspx");
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0007BBDC File Offset: 0x00079DDC
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddComponentProperty("DataSource", this.messageTraceDataSource);
			descriptor.AddComponentProperty("RefreshMethod", this.messageTraceDataSource.RefreshWebServiceMethod.ClientID, true);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04001FFA RID: 8186
		protected WebServiceListSource messageTraceDataSource;

		// Token: 0x04001FFB RID: 8187
		protected ListView listViewSearchResults;
	}
}
