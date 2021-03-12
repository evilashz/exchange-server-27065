using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000193 RID: 403
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessageIteratorClient : IDisposable
	{
		// Token: 0x060007EB RID: 2027
		IMessage UploadMessage(bool isAssociatedMessage);
	}
}
