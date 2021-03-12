using System;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000009 RID: 9
	internal interface IRopHandlerWithContext : IRopHandler, IDisposable
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42
		// (set) Token: 0x0600002B RID: 43
		MapiContext MapiContext { get; set; }
	}
}
