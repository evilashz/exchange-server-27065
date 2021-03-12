using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IAsyncOperationExecutor
	{
		// Token: 0x060001DD RID: 477
		void BeginOperation(bool useSameThread);

		// Token: 0x060001DE RID: 478
		void EndOperation();

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001DF RID: 479
		bool IsCompleted { get; }

		// Token: 0x060001E0 RID: 480
		void WaitForStopped();

		// Token: 0x060001E1 RID: 481
		void GetProgressInfo(out object progressToken, out ProgressInfo progressInfo);
	}
}
