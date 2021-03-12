using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000180 RID: 384
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAttachment : IDisposable
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600078D RID: 1933
		IPropertyBag PropertyBag { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600078E RID: 1934
		bool IsEmbeddedMessage { get; }

		// Token: 0x0600078F RID: 1935
		IMessage GetEmbeddedMessage();

		// Token: 0x06000790 RID: 1936
		void Save();

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000791 RID: 1937
		int AttachmentNumber { get; }
	}
}
