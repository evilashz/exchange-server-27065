using System;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000008 RID: 8
	public interface IServerVersionLookup
	{
		// Token: 0x06000012 RID: 18
		int? LookupVersion(string server);
	}
}
