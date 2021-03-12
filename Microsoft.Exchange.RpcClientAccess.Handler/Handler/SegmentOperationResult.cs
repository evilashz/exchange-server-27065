using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SegmentOperationResult
	{
		// Token: 0x040000D8 RID: 216
		internal static readonly OperationResult NeutralOperationResult;

		// Token: 0x040000D9 RID: 217
		internal OperationResult OperationResult;

		// Token: 0x040000DA RID: 218
		internal LocalizedException Exception;

		// Token: 0x040000DB RID: 219
		internal int CompletedWork;

		// Token: 0x040000DC RID: 220
		internal bool IsCompleted;
	}
}
