using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000020 RID: 32
	public class AssistantActivityState
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00010574 File Offset: 0x0000E774
		internal AssistantActivityState(RequiredMaintenanceResourceType requiredMaintenanceResourceType)
		{
			this.requiredMaintenanceResourceType = requiredMaintenanceResourceType;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00010583 File Offset: 0x0000E783
		public RequiredMaintenanceResourceType RequiredMaintenanceResourceType
		{
			get
			{
				return this.requiredMaintenanceResourceType;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0001058B File Offset: 0x0000E78B
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00010593 File Offset: 0x0000E793
		public DateTime LastTimeRequested
		{
			get
			{
				return this.lastTimeRequested;
			}
			set
			{
				this.lastTimeRequested = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0001059C File Offset: 0x0000E79C
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000105A4 File Offset: 0x0000E7A4
		public DateTime LastTimePerformed
		{
			get
			{
				return this.lastTimePerformed;
			}
			set
			{
				this.lastTimePerformed = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000105AD File Offset: 0x0000E7AD
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000105B5 File Offset: 0x0000E7B5
		public bool AssistantIsActiveInLastMonitoringPeriod
		{
			get
			{
				return this.assistantIsActiveInLastMonitoringPeriod;
			}
			set
			{
				this.assistantIsActiveInLastMonitoringPeriod = value;
			}
		}

		// Token: 0x040001DD RID: 477
		private readonly RequiredMaintenanceResourceType requiredMaintenanceResourceType;

		// Token: 0x040001DE RID: 478
		private DateTime lastTimeRequested;

		// Token: 0x040001DF RID: 479
		private DateTime lastTimePerformed;

		// Token: 0x040001E0 RID: 480
		private bool assistantIsActiveInLastMonitoringPeriod;
	}
}
