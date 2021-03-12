using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000219 RID: 537
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregateOperationResult
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x00078597 File Offset: 0x00076797
		public AggregateOperationResult(OperationResult operationResult, GroupOperationResult[] groupOperationResults)
		{
			EnumValidator.ThrowIfInvalid<OperationResult>(operationResult, "operationResult");
			this.OperationResult = operationResult;
			this.GroupOperationResults = groupOperationResults;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000785B8 File Offset: 0x000767B8
		public static AggregateOperationResult Merge(AggregateOperationResult first, AggregateOperationResult second)
		{
			OperationResult operationResult;
			if (first.OperationResult == OperationResult.Succeeded && second.OperationResult == OperationResult.Succeeded)
			{
				operationResult = OperationResult.Succeeded;
			}
			else if (first.OperationResult == OperationResult.Failed && second.OperationResult == OperationResult.Failed)
			{
				operationResult = OperationResult.Failed;
			}
			else
			{
				operationResult = OperationResult.PartiallySucceeded;
			}
			GroupOperationResult[] array = new GroupOperationResult[first.GroupOperationResults.Length + second.GroupOperationResults.Length];
			first.GroupOperationResults.Concat(second.GroupOperationResults).CopyTo(array, 0);
			return new AggregateOperationResult(operationResult, array);
		}

		// Token: 0x04000F7F RID: 3967
		public readonly OperationResult OperationResult;

		// Token: 0x04000F80 RID: 3968
		public readonly GroupOperationResult[] GroupOperationResults;
	}
}
