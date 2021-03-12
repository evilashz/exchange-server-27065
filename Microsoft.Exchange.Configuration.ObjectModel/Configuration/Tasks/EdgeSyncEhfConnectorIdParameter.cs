using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public class EdgeSyncEhfConnectorIdParameter : ADIdParameter
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x00020D64 File Offset: 0x0001EF64
		public EdgeSyncEhfConnectorIdParameter()
		{
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00020D6C File Offset: 0x0001EF6C
		public EdgeSyncEhfConnectorIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00020D75 File Offset: 0x0001EF75
		public EdgeSyncEhfConnectorIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00020D7E File Offset: 0x0001EF7E
		public EdgeSyncEhfConnectorIdParameter(EdgeSyncEhfConnector connector) : base(connector.Id)
		{
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00020D8C File Offset: 0x0001EF8C
		public EdgeSyncEhfConnectorIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00020D95 File Offset: 0x0001EF95
		public static EdgeSyncEhfConnectorIdParameter Parse(string identity)
		{
			return new EdgeSyncEhfConnectorIdParameter(identity);
		}
	}
}
