using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000480 RID: 1152
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.SMSProperties.js")]
	public sealed class CalendarNotificationSlabProperties : SmsSlabProperties
	{
		// Token: 0x060039C5 RID: 14789 RVA: 0x000AF677 File Offset: 0x000AD877
		public CalendarNotificationSlabProperties() : base(null, "../sms/EditNotification.aspx")
		{
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000AF688 File Offset: 0x000AD888
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "CalendarNotificationSlabProperties";
			return scriptDescriptor;
		}
	}
}
