using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200017E RID: 382
	internal struct PendingLatencyRecord
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x00042DD3 File Offset: 0x00040FD3
		public PendingLatencyRecord(ushort componentId, long startTime)
		{
			this.componentId = componentId;
			this.startTime = startTime;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00042DE3 File Offset: 0x00040FE3
		public ushort ComponentId
		{
			get
			{
				return this.componentId;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00042DEB File Offset: 0x00040FEB
		public string ComponentShortName
		{
			get
			{
				return LatencyTracker.GetShortName(this.componentId);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00042DF8 File Offset: 0x00040FF8
		public long StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00042E00 File Offset: 0x00041000
		public TimeSpan CalculatePendingLatency(long currentTime)
		{
			return LatencyTracker.TimeSpanFromTicks(this.startTime, currentTime);
		}

		// Token: 0x040008F0 RID: 2288
		private ushort componentId;

		// Token: 0x040008F1 RID: 2289
		private long startTime;
	}
}
