using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D6 RID: 214
	public interface IDirectory
	{
		// Token: 0x06000743 RID: 1859
		ADSite[] GetADSites();

		// Token: 0x06000744 RID: 1860
		ClientAccessArray[] GetClientAccessArrays();

		// Token: 0x06000745 RID: 1861
		Server[] GetServers();
	}
}
