using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000181 RID: 385
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAttachmentHandle
	{
		// Token: 0x06000792 RID: 1938
		IAttachment GetAttachment();
	}
}
