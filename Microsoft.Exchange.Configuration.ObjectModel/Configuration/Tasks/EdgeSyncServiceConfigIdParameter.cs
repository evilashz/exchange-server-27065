using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	public class EdgeSyncServiceConfigIdParameter : ADIdParameter
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x00020DD6 File Offset: 0x0001EFD6
		public EdgeSyncServiceConfigIdParameter()
		{
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00020DDE File Offset: 0x0001EFDE
		public EdgeSyncServiceConfigIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00020DE7 File Offset: 0x0001EFE7
		public EdgeSyncServiceConfigIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00020DF0 File Offset: 0x0001EFF0
		public EdgeSyncServiceConfigIdParameter(EdgeSyncServiceConfig edgeSyncServiceConfig) : base(edgeSyncServiceConfig.Id)
		{
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00020DFE File Offset: 0x0001EFFE
		public EdgeSyncServiceConfigIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00020E07 File Offset: 0x0001F007
		public static EdgeSyncServiceConfigIdParameter Parse(string identity)
		{
			return new EdgeSyncServiceConfigIdParameter(identity);
		}
	}
}
