using System;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200066C RID: 1644
	public class SubscriptionResultWizardStep : ShowResultWizardStep
	{
		// Token: 0x06004752 RID: 18258 RVA: 0x000D84F0 File Offset: 0x000D66F0
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "SubscriptionResultWizardStep";
			scriptDescriptor.AddProperty("FailedHelpLink", HelpUtil.BuildEhcHref(OptionsHelpId.AutoProvisionFailed.ToString()));
			scriptDescriptor.AddProperty("NewPopAccessible", LoginUtil.CheckUrlAccess("NewPopSubscription.aspx"));
			scriptDescriptor.AddProperty("NewImapAccessible", LoginUtil.CheckUrlAccess("NewImapSubscription.aspx"));
			return scriptDescriptor;
		}
	}
}
