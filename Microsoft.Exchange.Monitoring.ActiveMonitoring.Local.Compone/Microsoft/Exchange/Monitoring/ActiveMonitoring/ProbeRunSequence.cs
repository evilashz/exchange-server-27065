using System;
using System.Collections.Concurrent;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200012B RID: 299
	internal class ProbeRunSequence
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x00033E48 File Offset: 0x00032048
		public static long GetProbeRunSequenceNumber(string probeId, out bool firstRun)
		{
			firstRun = false;
			long num = 0L;
			if (!ProbeRunSequence.dictProbeRunSeqNumber.TryGetValue(probeId, out num))
			{
				firstRun = true;
				num = DateTime.UtcNow.Ticks;
			}
			num += 1L;
			ProbeRunSequence.dictProbeRunSeqNumber[probeId] = num;
			return num;
		}

		// Token: 0x04000611 RID: 1553
		private static ConcurrentDictionary<string, long> dictProbeRunSeqNumber = new ConcurrentDictionary<string, long>();
	}
}
