using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Mobility;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000049 RID: 73
	[Cmdlet("Disable", "PushNotificationProxy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	[OutputType(new Type[]
	{
		typeof(PushNotificationProxyPresentationObject)
	})]
	public sealed class DisablePushNotificationProxy : ProxyCmdletBaseClass
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000C3E5 File Offset: 0x0000A5E5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableProxy(this.Identity.ToString(), base.CurrentProxyStatus);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C400 File Offset: 0x0000A600
		protected override IConfigurable PrepareDataObject()
		{
			PushNotificationApp pushNotificationApp = (PushNotificationApp)base.PrepareDataObject();
			pushNotificationApp.Enabled = new bool?(false);
			return pushNotificationApp;
		}
	}
}
