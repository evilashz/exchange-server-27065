using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200008E RID: 142
	internal abstract class StreamSource : BaseObject
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060005BF RID: 1471
		public abstract ICorePropertyBag PropertyBag { get; }

		// Token: 0x060005C0 RID: 1472
		public abstract void OnAccess();

		// Token: 0x060005C1 RID: 1473
		public abstract StreamSource Duplicate();
	}
}
