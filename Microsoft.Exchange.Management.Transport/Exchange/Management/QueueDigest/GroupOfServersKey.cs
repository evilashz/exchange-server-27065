using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x0200006D RID: 109
	internal abstract class GroupOfServersKey
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000F708 File Offset: 0x0000D908
		public static GroupOfServersKey CreateFromDag(ADObjectId dagId)
		{
			return new DagGroupOfServersKey(dagId);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000F710 File Offset: 0x0000D910
		public static GroupOfServersKey CreateFromSite(ADObjectId siteId, int majorVersion)
		{
			return new SiteGroupOfServersKey(siteId, majorVersion);
		}
	}
}
