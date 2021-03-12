using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200004A RID: 74
	internal class ProbeRunSequence
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		public static long GetProbeRunSequenceNumber(string probeId, out bool firstRun)
		{
			long num;
			long probeRunSequenceNumber = ProbeRunSequence.GetProbeRunSequenceNumber(probeId, out num);
			firstRun = (num == 1L);
			return probeRunSequenceNumber;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C420 File Offset: 0x0000A620
		public static long GetProbeRunSequenceNumber(string probeId, out long count)
		{
			ProbeRunSequence.SequenceInfo orAdd = ProbeRunSequence.dictProbeRunSeqNumber.GetOrAdd(probeId, (string key) => new ProbeRunSequence.SequenceInfo(DateTime.UtcNow.Ticks));
			return orAdd.GetNextSequence(out count);
		}

		// Token: 0x0400013E RID: 318
		private static ConcurrentDictionary<string, ProbeRunSequence.SequenceInfo> dictProbeRunSeqNumber = new ConcurrentDictionary<string, ProbeRunSequence.SequenceInfo>();

		// Token: 0x0200004B RID: 75
		private class SequenceInfo
		{
			// Token: 0x060001E0 RID: 480 RVA: 0x0000C471 File Offset: 0x0000A671
			public SequenceInfo(long value)
			{
				this.currentValue = value;
				this.StartValue = value;
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000C487 File Offset: 0x0000A687
			// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000C48F File Offset: 0x0000A68F
			public long StartValue { get; private set; }

			// Token: 0x060001E3 RID: 483 RVA: 0x0000C498 File Offset: 0x0000A698
			public long GetNextSequence(out long count)
			{
				long num = Interlocked.Increment(ref this.currentValue);
				count = num - this.StartValue;
				return num;
			}

			// Token: 0x04000140 RID: 320
			private long currentValue;
		}
	}
}
