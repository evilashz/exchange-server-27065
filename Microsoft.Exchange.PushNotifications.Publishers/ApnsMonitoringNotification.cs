using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000033 RID: 51
	internal sealed class ApnsMonitoringNotification : ApnsNotification
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00008112 File Offset: 0x00006312
		public ApnsMonitoringNotification(string appId, string deviceId) : base(appId, OrganizationId.ForestWideOrgId, deviceId, 1, DateTime.UtcNow)
		{
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008127 File Offset: 0x00006327
		public override bool IsMonitoring
		{
			get
			{
				return true;
			}
		}
	}
}
