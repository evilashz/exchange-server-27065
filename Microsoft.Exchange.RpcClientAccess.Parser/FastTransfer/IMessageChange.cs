using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200018F RID: 399
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessageChange : IDisposable
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060007DC RID: 2012
		IMessage Message { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060007DD RID: 2013
		int MessageSize { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060007DE RID: 2014
		bool IsAssociatedMessage { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060007DF RID: 2015
		IPropertyBag MessageHeaderPropertyBag { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060007E0 RID: 2016
		IMessageChangePartial PartialChange { get; }
	}
}
