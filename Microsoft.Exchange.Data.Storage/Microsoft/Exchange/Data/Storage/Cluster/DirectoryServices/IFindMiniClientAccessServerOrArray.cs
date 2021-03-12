using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x02000430 RID: 1072
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFindMiniClientAccessServerOrArray
	{
		// Token: 0x06002FFB RID: 12283
		void Clear();

		// Token: 0x06002FFC RID: 12284
		IADMiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayByFqdn(string serverFqdn);

		// Token: 0x06002FFD RID: 12285
		IADMiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayByLegdn(string serverLegdn);

		// Token: 0x06002FFE RID: 12286
		IADMiniClientAccessServerOrArray ReadMiniClientAccessServerOrArrayByObjectId(ADObjectId serverId);

		// Token: 0x06002FFF RID: 12287
		IADMiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayWithClientAccess(ADObjectId siteId, ADObjectId preferredServerId);
	}
}
