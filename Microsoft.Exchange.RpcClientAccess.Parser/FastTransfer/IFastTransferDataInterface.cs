using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200016F RID: 367
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IFastTransferDataInterface : IDisposable
	{
		// Token: 0x06000710 RID: 1808
		void NotifyCanSplitBuffers();
	}
}
