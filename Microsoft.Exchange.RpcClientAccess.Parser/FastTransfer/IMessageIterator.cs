using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000192 RID: 402
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessageIterator : IDisposable
	{
		// Token: 0x060007EA RID: 2026
		IEnumerator<IMessage> GetMessages();
	}
}
