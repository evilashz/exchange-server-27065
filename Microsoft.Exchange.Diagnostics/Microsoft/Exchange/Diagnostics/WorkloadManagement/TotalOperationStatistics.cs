using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x02000203 RID: 515
	internal class TotalOperationStatistics : OperationStatistics
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x0003D750 File Offset: 0x0003B950
		internal TotalOperationStatistics()
		{
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0003D758 File Offset: 0x0003B958
		public double Total
		{
			get
			{
				return this.total;
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003D760 File Offset: 0x0003B960
		internal override void AddCall(float value = 0f, int count = 1)
		{
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

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003D790 File Offset: 0x0003B990
		internal override void Merge(OperationStatistics s2)
		{
			TotalOperationStatistics totalOperationStatistics = s2 as TotalOperationStatistics;
			if (totalOperationStatistics != null)
			{
				this.total += totalOperationStatistics.total;
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003D7BB File Offset: 0x0003B9BB
		internal override void AppendStatistics(OperationKey operationKey, List<KeyValuePair<string, object>> customData)
		{
			customData.Add(new KeyValuePair<string, object>(this.ToTotalKey(operationKey), this.Total));
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003D7DC File Offset: 0x0003B9DC
		private string ToTotalKey(OperationKey operationKey)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			stringBuilder.Append(DisplayNameAttribute.GetEnumName(operationKey.ActivityOperationType));
			stringBuilder.Append(".T[");
			if (!string.IsNullOrWhiteSpace(operationKey.Instance))
			{
				base.AppendValidChars(stringBuilder, operationKey.Instance);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000AB8 RID: 2744
		private double total;
	}
}
