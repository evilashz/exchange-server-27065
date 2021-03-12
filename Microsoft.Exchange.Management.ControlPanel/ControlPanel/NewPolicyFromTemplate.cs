using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001C8 RID: 456
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(WizardForm))]
	[ClientScriptResource("NewPolicyFromTemplate", "Microsoft.Exchange.Management.ControlPanel.Client.DLPPolicy.js")]
	public class NewPolicyFromTemplate : BaseForm
	{
		// Token: 0x060024EA RID: 9450 RVA: 0x00071558 File Offset: 0x0006F758
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddComponentProperty("ListView", this.complianceProgramListView.ClientID, true);
			descriptor.AddElementProperty("DistributionGroupPanel", this.pnlDistributionGroup.ClientID);
			descriptor.AddElementProperty("IncidentManagementPanel", this.pnlIncidentMailbox.ClientID);
			EcpSingleSelect ecpSingleSelect = (EcpSingleSelect)this.pnlIncidentMailbox.FindControl("chooseIncidentManagementBox");
			EcpSingleSelect ecpSingleSelect2 = (EcpSingleSelect)this.pnlDistributionGroup.FindControl("chooseDistributionGroup");
			descriptor.AddComponentProperty("IncidentMailboxPicker", ecpSingleSelect.ClientID, true);
			descriptor.AddComponentProperty("DistibutionGroupPicker", ecpSingleSelect2.ClientID, true);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04001EAF RID: 7855
		protected Microsoft.Exchange.Management.ControlPanel.WebControls.ListView complianceProgramListView;

		// Token: 0x04001EB0 RID: 7856
		protected TextBox templateId;

		// Token: 0x04001EB1 RID: 7857
		protected Panel pnlDistributionGroup;

		// Token: 0x04001EB2 RID: 7858
		protected Panel pnlIncidentMailbox;
	}
}
