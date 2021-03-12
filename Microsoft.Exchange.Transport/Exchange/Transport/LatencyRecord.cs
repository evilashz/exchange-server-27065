using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200017F RID: 383
	internal struct LatencyRecord
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x00042E10 File Offset: 0x00041010
		public LatencyRecord(ushort componentId, TimeSpan latency)
		{
			this.componentId = componentId;
			this.latency = (uint)LatencyRecord.ConstrainToLimits(latency).TotalMilliseconds;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00042E39 File Offset: 0x00041039
		public ushort ComponentId
		{
			get
			{
				return this.componentId;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00042E41 File Offset: 0x00041041
		public string ComponentShortName
		{
			get
			{
				return LatencyTracker.GetShortName(this.componentId);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00042E4E File Offset: 0x0004104E
		public TimeSpan Latency
		{
			get
			{
				return TimeSpan.FromMilliseconds(this.latency);
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00042E5D File Offset: 0x0004105D
		private static TimeSpan ConstrainToLimits(TimeSpan latency)
		{
			if (latency < TimeSpan.Zero)
			{
				return TimeSpan.Zero;
			}
			if (latency > LatencyRecord.MaxLatency)
			{
				return LatencyRecord.MaxLatency;
			}
			return latency;
		}

		// Token: 0x040008F2 RID: 2290
		public static readonly LatencyRecord Empty = new LatencyRecord(0, TimeSpan.Zero);

		// Token: 0x040008F3 RID: 2291
		private static readonly TimeSpan MaxLatency = TransportAppConfig.LatencyTrackerConfig.MaxLatency;

		// Token: 0x040008F4 RID: 2292
		private readonly ushort componentId;

		// Token: 0x040008F5 RID: 2293
		private readonly uint latency;
	}
}
