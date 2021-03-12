using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001AB RID: 427
	internal struct LatencyRecordPlus
	{
		// Token: 0x06001383 RID: 4995 RVA: 0x0004D8B9 File Offset: 0x0004BAB9
		public LatencyRecordPlus(ushort componentId, long startTime)
		{
			this = default(LatencyRecordPlus);
			this.latency = 0U;
			this.ComponentId = componentId;
			this.StartTime = startTime;
			this.IsComplete = false;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004D8E0 File Offset: 0x0004BAE0
		public LatencyRecordPlus(ushort componentId, TimeSpan latency)
		{
			this = default(LatencyRecordPlus);
			this.latency = (uint)LatencyRecordPlus.ConstrainToLimits(latency).TotalMilliseconds;
			this.ComponentId = componentId;
			this.StartTime = 0L;
			this.IsComplete = true;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0004D91F File Offset: 0x0004BB1F
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x0004D927 File Offset: 0x0004BB27
		public long StartTime { get; private set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0004D930 File Offset: 0x0004BB30
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x0004D938 File Offset: 0x0004BB38
		public ushort ComponentId { get; private set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0004D941 File Offset: 0x0004BB41
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x0004D949 File Offset: 0x0004BB49
		public bool IsComplete { get; private set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0004D952 File Offset: 0x0004BB52
		public bool IsPending
		{
			get
			{
				return !this.IsComplete;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0004D95D File Offset: 0x0004BB5D
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x0004D965 File Offset: 0x0004BB65
		public bool IsImplicitlyComplete { get; private set; }

		// Token: 0x0600138E RID: 5006 RVA: 0x0004D96E File Offset: 0x0004BB6E
		public TimeSpan Complete(long endTime, bool implictCompletion = false)
		{
			return this.Complete(endTime, this.ComponentId, implictCompletion);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0004D980 File Offset: 0x0004BB80
		public TimeSpan Complete(long endTime, ushort newComponentId, bool implicitCompletion = false)
		{
			TimeSpan result = LatencyRecordPlus.ConstrainToLimits(LatencyTracker.TimeSpanFromTicks(this.StartTime, endTime));
			this.latency = (uint)result.TotalMilliseconds;
			this.ComponentId = newComponentId;
			this.IsComplete = true;
			this.IsImplicitlyComplete = implicitCompletion;
			return result;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0004D9C3 File Offset: 0x0004BBC3
		public TimeSpan CalculateLatency(long currentTime)
		{
			if (!this.IsComplete)
			{
				return LatencyTracker.TimeSpanFromTicks(this.StartTime, currentTime);
			}
			return TimeSpan.FromMilliseconds(this.latency);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0004D9E7 File Offset: 0x0004BBE7
		public TimeSpan CalculateLatency()
		{
			return this.CalculateLatency(LatencyTracker.StopwatchProvider());
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004D9F9 File Offset: 0x0004BBF9
		internal LatencyRecord AsCompletedRecord()
		{
			return new LatencyRecord(this.ComponentId, this.CalculateLatency());
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004DA0C File Offset: 0x0004BC0C
		internal PendingLatencyRecord AsPendingRecord()
		{
			return new PendingLatencyRecord(this.ComponentId, this.StartTime);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004DA1F File Offset: 0x0004BC1F
		private static TimeSpan ConstrainToLimits(TimeSpan latency)
		{
			if (latency < TimeSpan.Zero)
			{
				return TimeSpan.Zero;
			}
			if (latency > LatencyRecordPlus.MaxLatency)
			{
				return LatencyRecordPlus.MaxLatency;
			}
			return latency;
		}

		// Token: 0x04000A0A RID: 2570
		private static readonly TimeSpan MaxLatency = TransportAppConfig.LatencyTrackerConfig.MaxLatency;

		// Token: 0x04000A0B RID: 2571
		private uint latency;
	}
}
