using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000481 RID: 1153
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.SMSProperties.js")]
	public class RegionCarrierStep : WizardStep
	{
		// Token: 0x060039C7 RID: 14791 RVA: 0x000AF6A8 File Offset: 0x000AD8A8
		public RegionCarrierStep()
		{
			base.ClientClassName = "RegionCarrierStep";
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000AF6BC File Offset: 0x000AD8BC
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.AddComponentProperty("Properties", base.FindPropertiesParent().ClientID);
			return scriptDescriptor;
		}
	}
}
