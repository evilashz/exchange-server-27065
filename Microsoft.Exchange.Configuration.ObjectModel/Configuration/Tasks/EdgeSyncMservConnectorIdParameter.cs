using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public class EdgeSyncMservConnectorIdParameter : ADIdParameter
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x00020D9D File Offset: 0x0001EF9D
		public EdgeSyncMservConnectorIdParameter()
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00020DA5 File Offset: 0x0001EFA5
		public EdgeSyncMservConnectorIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00020DAE File Offset: 0x0001EFAE
		public EdgeSyncMservConnectorIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00020DB7 File Offset: 0x0001EFB7
		public EdgeSyncMservConnectorIdParameter(EdgeSyncMservConnector connector) : base(connector.Id)
		{
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00020DC5 File Offset: 0x0001EFC5
		public EdgeSyncMservConnectorIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00020DCE File Offset: 0x0001EFCE
		public static EdgeSyncMservConnectorIdParameter Parse(string identity)
		{
			return new EdgeSyncMservConnectorIdParameter(identity);
		}
	}
}
