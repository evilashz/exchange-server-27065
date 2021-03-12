using System;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200002C RID: 44
	internal struct ProgressInfo
	{
		// Token: 0x040000C3 RID: 195
		public bool IsCompleted;

		// Token: 0x040000C4 RID: 196
		public uint CompletedTaskCount;

		// Token: 0x040000C5 RID: 197
		public uint TotalTaskCount;

		// Token: 0x040000C6 RID: 198
		public Func<object, IProgressResultFactory, RopResult> CreateCompleteResult;

		// Token: 0x040000C7 RID: 199
		public Func<object, ProgressResultFactory, RopResult> CreateCompleteResultForProgress;
	}
}
