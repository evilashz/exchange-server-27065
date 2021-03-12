using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200003D RID: 61
	internal sealed class RpcFailureCounters : IRpcCounters
	{
		// Token: 0x06000251 RID: 593 RVA: 0x0000887C File Offset: 0x00006A7C
		public RpcFailureCounters()
		{
			this.failureCounters = new Dictionary<uint, int>();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008890 File Offset: 0x00006A90
		public void IncrementCounter(IRpcCounterData counterData)
		{
			FailureCounterData failureCounterData = counterData as FailureCounterData;
			int num = 0;
			if (!this.failureCounters.TryGetValue(failureCounterData.FailureCode, out num))
			{
				this.failureCounters.Add(failureCounterData.FailureCode, 1);
				return;
			}
			this.failureCounters[failureCounterData.FailureCode] = num + 1;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000088F4 File Offset: 0x00006AF4
		private IEnumerable<KeyValuePair<uint, int>> GetTopFailureCounters(out int othersCount)
		{
			IOrderedEnumerable<KeyValuePair<uint, int>> source = from entry in this.failureCounters
			orderby entry.Value descending
			select entry;
			othersCount = source.Skip(10).Sum((KeyValuePair<uint, int> entry) => entry.Value);
			return source.Take(10);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008984 File Offset: 0x00006B84
		public override string ToString()
		{
			int num = 0;
			IEnumerable<KeyValuePair<uint, int>> topFailureCounters = this.GetTopFailureCounters(out num);
			List<string> list = (from entry in topFailureCounters
			select string.Format("0x{0:X}={1}", entry.Key, entry.Value)).ToList<string>();
			if (num != 0)
			{
				list.Add(string.Format("O={0}", num));
			}
			return string.Join(";", list);
		}

		// Token: 0x040001DC RID: 476
		private const int NumberOfRelevantCounters = 10;

		// Token: 0x040001DD RID: 477
		private readonly IDictionary<uint, int> failureCounters;
	}
}
