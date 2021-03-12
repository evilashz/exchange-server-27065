using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D6 RID: 214
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FindRowResultFactory : StandardResultFactory
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000F064 File Offset: 0x0000D264
		internal FindRowResultFactory(int availableBufferSize, Encoding string8Encoding) : base(RopId.FindRow)
		{
			this.availableBufferSize = availableBufferSize;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000F07C File Offset: 0x0000D27C
		public RowCollector CreateRowCollector()
		{
			int num = 8;
			return new RowCollector(this.availableBufferSize - num, false, this.string8Encoding);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000F09F File Offset: 0x0000D29F
		public RopResult CreateSuccessfulResult(bool positionChanged, RowCollector rowCollector)
		{
			if (rowCollector.RowCount > 1)
			{
				throw new ArgumentException("FindRow only accepts one row");
			}
			return new SuccessfulFindRowResult(positionChanged, rowCollector);
		}

		// Token: 0x040002E5 RID: 741
		private readonly int availableBufferSize;

		// Token: 0x040002E6 RID: 742
		private readonly Encoding string8Encoding;
	}
}
