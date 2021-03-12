using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning
{
	// Token: 0x02000404 RID: 1028
	public class FowardSyncEventRecord
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0008DF23 File Offset: 0x0008C123
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x0008DF2B File Offset: 0x0008C12B
		public string Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x0008DF34 File Offset: 0x0008C134
		// (set) Token: 0x06001A18 RID: 6680 RVA: 0x0008DF3C File Offset: 0x0008C13C
		public string ServiceInstanceName
		{
			get
			{
				return this.serviceInstanceName;
			}
			set
			{
				this.serviceInstanceName = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0008DF45 File Offset: 0x0008C145
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x0008DF4D File Offset: 0x0008C14D
		public DateTime? TimeCreated
		{
			get
			{
				return this.timeCreated;
			}
			set
			{
				this.timeCreated = value;
			}
		}

		// Token: 0x040011C3 RID: 4547
		private string status;

		// Token: 0x040011C4 RID: 4548
		private string serviceInstanceName;

		// Token: 0x040011C5 RID: 4549
		private DateTime? timeCreated;
	}
}
