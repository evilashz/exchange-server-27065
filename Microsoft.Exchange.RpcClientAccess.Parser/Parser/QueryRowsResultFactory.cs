using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000110 RID: 272
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class QueryRowsResultFactory : StandardResultFactory
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x00010432 File Offset: 0x0000E632
		internal QueryRowsResultFactory(int availableBufferSize, Encoding string8Encoding) : base(RopId.QueryRows)
		{
			this.availableBufferSize = availableBufferSize;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001044C File Offset: 0x0000E64C
		public RowCollector CreateRowCollector()
		{
			int num = 7;
			return new RowCollector(this.availableBufferSize - num, true, this.string8Encoding);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001046F File Offset: 0x0000E66F
		public RopResult CreateSuccessfulResult(BookmarkOrigin bookmarkOrigin, RowCollector rowCollector)
		{
			return new SuccessfulQueryRowsResult(bookmarkOrigin, rowCollector);
		}

		// Token: 0x04000311 RID: 785
		private readonly int availableBufferSize;

		// Token: 0x04000312 RID: 786
		private readonly Encoding string8Encoding;
	}
}
