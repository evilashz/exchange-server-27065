using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000153 RID: 339
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFastTransferProcessor<TContext> : IDisposable where TContext : BaseObject
	{
		// Token: 0x06000637 RID: 1591
		IEnumerator<FastTransferStateMachine?> Process(TContext context);
	}
}
