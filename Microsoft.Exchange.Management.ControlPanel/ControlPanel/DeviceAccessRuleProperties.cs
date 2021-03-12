using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000302 RID: 770
	[ClientScriptResource("DeviceAccessRuleProperties", "Microsoft.Exchange.Management.ControlPanel.Client.DeviceAccessRule.js")]
	public sealed class DeviceAccessRuleProperties : Properties
	{
		// Token: 0x06002E2F RID: 11823 RVA: 0x0008C6A8 File Offset: 0x0008A8A8
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "DeviceAccessRuleProperties";
			scriptDescriptor.AddComponentProperty("DeviceTypeSelector", base.ContentContainer.FindControl("pkrDeviceFamily").ClientID);
			scriptDescriptor.AddComponentProperty("DeviceModelSelector", base.ContentContainer.FindControl("pkrDeviceModel").ClientID);
			return scriptDescriptor;
		}
	}
}
