using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000CA RID: 202
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExpandRowResultFactory : StandardResultFactory
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0000EE5F File Offset: 0x0000D05F
		internal ExpandRowResultFactory(int availableBufferSize, Encoding string8Encoding) : base(RopId.ExpandRow)
		{
			this.availableBufferSize = availableBufferSize;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000EE78 File Offset: 0x0000D078
		public RowCollector CreateRowCollector()
		{
			int num = 6;
			return new RowCollector(this.availableBufferSize - num, false, this.string8Encoding);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000EE9B File Offset: 0x0000D09B
		public RopResult CreateSuccessfulResult(int expandedRowCount, RowCollector rowCollector)
		{
			return new SuccessfulExpandRowResult(expandedRowCount, rowCollector);
		}

		// Token: 0x040002DF RID: 735
		private readonly int availableBufferSize;

		// Token: 0x040002E0 RID: 736
		private readonly Encoding string8Encoding;
	}
}
