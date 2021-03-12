using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x0200000E RID: 14
	public interface IFlightSettingRepository
	{
		// Token: 0x0600004B RID: 75
		string GetHostNameFromVdir(ADObjectId serverSiteId, string protocol);
	}
}
