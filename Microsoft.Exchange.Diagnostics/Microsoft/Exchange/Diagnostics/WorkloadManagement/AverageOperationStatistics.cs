using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001F6 RID: 502
	internal class AverageOperationStatistics : OperationStatistics
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x0003CCDA File Offset: 0x0003AEDA
		internal AverageOperationStatistics()
		{
			this.total = 0.0;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0003CCF1 File Offset: 0x0003AEF1
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0003CCF9 File Offset: 0x0003AEF9
		public float CumulativeAverage
		{
			get
			{
				if (this.count == 0)
				{
					return 0f;
				}
				return (float)(this.total / (double)this.count);
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003CD18 File Offset: 0x0003AF18
		internal override void AppendStatistics(OperationKey operationKey, List<KeyValuePair<string, object>> customData)
		{
			customData.Add(new KeyValuePair<string, object>(base.ToCountKey(operationKey), this.Count));
			customData.Add(new KeyValuePair<string, object>(this.ToAverageKey(operationKey), this.CumulativeAverage));
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003CD54 File Offset: 0x0003AF54
		internal override void AddCall(float value = 0f, int count = 1)
		{
			if (count < 1)
			{
				Guid? localId = SingleContext.Singleton.LocalId;
				ExTraceGlobals.ActivityContextTracer.TraceDebug<Guid?, int>((long)localId.GetHashCode(), "OperationStatistics.AddCall - failed to update the Average for ActivityId {0}, count was less than 1: {1}.", (localId != null) ? localId : new Guid?(Guid.Empty), count);
				return;
			}
			Interlocked.Add(ref this.count, count);
			double num = 0.0;
			double num2;
			do
			{
				num2 = num;
				double value2 = num2 + (double)value;
				num = Interlocked.CompareExchange(ref this.total, value2, num2);
			}
			while (num != num2);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003CDD8 File Offset: 0x0003AFD8
		internal override void Merge(OperationStatistics s2)
		{
			AverageOperationStatistics averageOperationStatistics = s2 as AverageOperationStatistics;
			if (averageOperationStatistics != null)
			{
				float value = averageOperationStatistics.CumulativeAverage * (float)averageOperationStatistics.Count;
				this.AddCall(value, averageOperationStatistics.Count);
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003CE0C File Offset: 0x0003B00C
		private string ToAverageKey(OperationKey operationKey)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			stringBuilder.Append(DisplayNameAttribute.GetEnumName(operationKey.ActivityOperationType));
			stringBuilder.Append(".AL[");
			if (!string.IsNullOrWhiteSpace(operationKey.Instance))
			{
				base.AppendValidChars(stringBuilder, operationKey.Instance);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000A94 RID: 2708
		private double total;

		// Token: 0x04000A95 RID: 2709
		private int count;
	}
}
