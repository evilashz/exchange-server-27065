using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D0 RID: 208
	internal sealed class WebAppMonitoringNotification : WebAppNotification
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00015B31 File Offset: 0x00013D31
		public WebAppMonitoringNotification(string appId, string deviceId) : base(appId, OrganizationId.ForestWideOrgId, "PublishO365Notification", new O365Notification("::AE82E53440744F2798C276818CE8BD5C::", deviceId).ToJson())
		{
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00015B54 File Offset: 0x00013D54
		public override bool IsMonitoring
		{
			get
			{
				return true;
			}
		}
	}
}
