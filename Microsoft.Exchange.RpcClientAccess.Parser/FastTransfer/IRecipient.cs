using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200019A RID: 410
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRecipient
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000813 RID: 2067
		IPropertyBag PropertyBag { get; }

		// Token: 0x06000814 RID: 2068
		void Save();
	}
}
