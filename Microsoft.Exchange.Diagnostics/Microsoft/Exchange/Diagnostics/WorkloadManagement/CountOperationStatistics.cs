using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001F7 RID: 503
	internal class CountOperationStatistics : OperationStatistics
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003CE76 File Offset: 0x0003B076
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003CE7E File Offset: 0x0003B07E
		internal override void AddCall(float value = 0f, int count = 1)
		{
			Interlocked.Add(ref this.count, count);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003CE90 File Offset: 0x0003B090
		internal override void Merge(OperationStatistics s2)
		{
			CountOperationStatistics countOperationStatistics = s2 as CountOperationStatistics;
			if (countOperationStatistics != null)
			{
				this.count += countOperationStatistics.count;
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003CEBA File Offset: 0x0003B0BA
		internal override void AppendStatistics(OperationKey operationKey, List<KeyValuePair<string, object>> customData)
		{
			customData.Add(new KeyValuePair<string, object>(base.ToCountKey(operationKey), this.Count));
		}

		// Token: 0x04000A96 RID: 2710
		private int count;
	}
}
