using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000030 RID: 48
	public interface IContextProvider : IConnectionProvider
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001DA RID: 474
		Context CurrentContext { get; }
	}
}
