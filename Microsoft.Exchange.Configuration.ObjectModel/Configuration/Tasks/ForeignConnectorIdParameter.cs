using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000123 RID: 291
	[Serializable]
	public class ForeignConnectorIdParameter : ADIdParameter
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x00022B85 File Offset: 0x00020D85
		public ForeignConnectorIdParameter()
		{
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00022B8D File Offset: 0x00020D8D
		public ForeignConnectorIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00022B96 File Offset: 0x00020D96
		public ForeignConnectorIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00022B9F File Offset: 0x00020D9F
		public ForeignConnectorIdParameter(ForeignConnector connector) : base(connector.Id)
		{
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00022BAD File Offset: 0x00020DAD
		public ForeignConnectorIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00022BB6 File Offset: 0x00020DB6
		public static ForeignConnectorIdParameter Parse(string identity)
		{
			return new ForeignConnectorIdParameter(identity);
		}
	}
}
