using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200018D RID: 397
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IIcsState : IDisposable
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060007D1 RID: 2001
		IPropertyBag PropertyBag { get; }

		// Token: 0x060007D2 RID: 2002
		void Flush();
	}
}
