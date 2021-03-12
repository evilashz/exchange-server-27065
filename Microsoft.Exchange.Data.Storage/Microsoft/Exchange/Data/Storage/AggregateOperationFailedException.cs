using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000714 RID: 1812
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class AggregateOperationFailedException : StoragePermanentException
	{
		// Token: 0x0600478B RID: 18315 RVA: 0x001301CE File Offset: 0x0012E3CE
		public AggregateOperationFailedException(LocalizedString message, AggregateOperationResult aggregateOperationResult) : base(message)
		{
			this.aggregateOperationResult = aggregateOperationResult;
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x001301DE File Offset: 0x0012E3DE
		protected AggregateOperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.aggregateOperationResult = (AggregateOperationResult)info.GetValue("aggregateOperationResult", typeof(AggregateOperationResult));
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x00130208 File Offset: 0x0012E408
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("aggregateOperationResult", this.aggregateOperationResult);
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x00130223 File Offset: 0x0012E423
		public AggregateOperationResult AggregateOperationResult
		{
			get
			{
				return this.aggregateOperationResult;
			}
		}

		// Token: 0x04002722 RID: 10018
		private const string AggregateOperationResultLabel = "aggregateOperationResult";

		// Token: 0x04002723 RID: 10019
		private readonly AggregateOperationResult aggregateOperationResult;
	}
}
