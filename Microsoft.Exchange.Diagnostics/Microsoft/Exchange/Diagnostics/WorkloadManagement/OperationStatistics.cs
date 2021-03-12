using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001F5 RID: 501
	internal abstract class OperationStatistics
	{
		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		public static OperationStatistics GetInstance(OperationKey operationKey)
		{
			switch (operationKey.ActivityOperationType)
			{
			case ActivityOperationType.StoreCall:
			case ActivityOperationType.StoreCpu:
			case ActivityOperationType.UserDelay:
			case ActivityOperationType.ResourceDelay:
			case ActivityOperationType.CustomCpu:
			case ActivityOperationType.MailboxLogBytes:
			case ActivityOperationType.MailboxMessagesCreated:
			case ActivityOperationType.BudgetUsed:
			case ActivityOperationType.RpcLatency:
			case ActivityOperationType.MapiLatency:
				return new TotalOperationStatistics();
			case ActivityOperationType.OverBudget:
			case ActivityOperationType.ResourceBlocked:
			case ActivityOperationType.RpcCount:
			case ActivityOperationType.Rop:
			case ActivityOperationType.MapiCount:
				return new CountOperationStatistics();
			}
			return new AverageOperationStatistics();
		}

		// Token: 0x06000EC6 RID: 3782
		internal abstract void AddCall(float value = 0f, int count = 1);

		// Token: 0x06000EC7 RID: 3783
		internal abstract void Merge(OperationStatistics s2);

		// Token: 0x06000EC8 RID: 3784
		internal abstract void AppendStatistics(OperationKey operationKey, List<KeyValuePair<string, object>> customData);

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003CC24 File Offset: 0x0003AE24
		protected void AppendValidChars(StringBuilder dest, string src)
		{
			foreach (char c in src)
			{
				if (SpecialCharacters.IsValidKeyChar(c))
				{
					dest.Append(c);
				}
				else
				{
					dest.Append('-');
				}
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003CC68 File Offset: 0x0003AE68
		protected string ToCountKey(OperationKey operationKey)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			stringBuilder.Append(DisplayNameAttribute.GetEnumName(operationKey.ActivityOperationType));
			stringBuilder.Append(".C[");
			if (!string.IsNullOrWhiteSpace(operationKey.Instance))
			{
				this.AppendValidChars(stringBuilder, operationKey.Instance);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
