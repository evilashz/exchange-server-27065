using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000060 RID: 96
	internal class DeviceAutoBlockThreshold
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x0001EA88 File Offset: 0x0001CC88
		public DeviceAutoBlockThreshold(ActiveSyncDeviceAutoblockThreshold threshold)
		{
			this.BehaviorType = threshold.BehaviorType;
			if (threshold.WhenChanged != null)
			{
				this.LastChangeTime = (ExDateTime)threshold.WhenChanged.Value.ToUniversalTime();
			}
			else
			{
				this.LastChangeTime = ExDateTime.UtcNow;
			}
			this.BehaviorTypeIncidenceLimit = threshold.BehaviorTypeIncidenceLimit;
			this.BehaviorTypeIncidenceDuration = threshold.BehaviorTypeIncidenceDuration;
			this.DeviceBlockDuration = threshold.DeviceBlockDuration;
			this.AdminEmailInsert = threshold.AdminEmailInsert;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001EB15 File Offset: 0x0001CD15
		public DeviceAutoBlockThreshold(AutoblockThresholdType type)
		{
			this.BehaviorType = type;
			this.LastChangeTime = ExDateTime.UtcNow;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001EB2F File Offset: 0x0001CD2F
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0001EB37 File Offset: 0x0001CD37
		public AutoblockThresholdType BehaviorType { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001EB40 File Offset: 0x0001CD40
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0001EB48 File Offset: 0x0001CD48
		public ExDateTime LastChangeTime { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001EB51 File Offset: 0x0001CD51
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0001EB59 File Offset: 0x0001CD59
		public int BehaviorTypeIncidenceLimit { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001EB62 File Offset: 0x0001CD62
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001EB6A File Offset: 0x0001CD6A
		public EnhancedTimeSpan BehaviorTypeIncidenceDuration { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001EB73 File Offset: 0x0001CD73
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001EB7B File Offset: 0x0001CD7B
		public EnhancedTimeSpan DeviceBlockDuration { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001EB84 File Offset: 0x0001CD84
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001EB8C File Offset: 0x0001CD8C
		public string AdminEmailInsert { get; set; }

		// Token: 0x040003B9 RID: 953
		public const DeviceAccessStateReason FirstAutoBlockReason = DeviceAccessStateReason.UserAgentsChanges;

		// Token: 0x040003BA RID: 954
		public const DeviceAccessStateReason LastAutoBlockReason = DeviceAccessStateReason.CommandFrequency;
	}
}
