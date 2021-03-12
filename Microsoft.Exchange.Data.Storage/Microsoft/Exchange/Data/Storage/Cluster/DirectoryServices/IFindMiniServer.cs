using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x02000435 RID: 1077
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFindMiniServer
	{
		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003028 RID: 12328
		IADToplogyConfigurationSession AdSession { get; }

		// Token: 0x06003029 RID: 12329
		void Clear();

		// Token: 0x0600302A RID: 12330
		IADServer FindMiniServerByFqdn(string serverFqdn);

		// Token: 0x0600302B RID: 12331
		IADServer FindMiniServerByShortName(string shortName);

		// Token: 0x0600302C RID: 12332
		IADServer FindMiniServerByShortNameEx(string shortName, out Exception ex);

		// Token: 0x0600302D RID: 12333
		IADServer ReadMiniServerByObjectId(ADObjectId serverId);
	}
}
