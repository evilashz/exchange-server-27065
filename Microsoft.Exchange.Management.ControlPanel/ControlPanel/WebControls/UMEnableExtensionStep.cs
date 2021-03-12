using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020004BD RID: 1213
	[ClientScriptResource("UMEnableExtensionStep", "Microsoft.Exchange.Management.ControlPanel.Client.UnifiedMessaging.js")]
	public class UMEnableExtensionStep : WizardStep
	{
		// Token: 0x06003BCA RID: 15306 RVA: 0x000B4417 File Offset: 0x000B2617
		public UMEnableExtensionStep()
		{
			base.ClientClassName = "UMEnableExtensionStep";
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x000B442C File Offset: 0x000B262C
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.AddComponentProperty("PropertyPage", base.FindPropertiesParent().ClientID);
			return scriptDescriptor;
		}
	}
}
