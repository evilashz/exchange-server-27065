using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200009F RID: 159
	internal sealed class GcmMonitoringNotification : GcmNotification
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00012B30 File Offset: 0x00010D30
		public GcmMonitoringNotification(string appId, string deviceId) : base(appId, OrganizationId.ForestWideOrgId, deviceId, new GcmPayload(new int?(5), null, null, BackgroundSyncType.None), "c", null, null)
		{
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00012B6F File Offset: 0x00010D6F
		public override bool IsMonitoring
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00012B72 File Offset: 0x00010D72
		public override bool DryRun
		{
			get
			{
				return true;
			}
		}
	}
}
