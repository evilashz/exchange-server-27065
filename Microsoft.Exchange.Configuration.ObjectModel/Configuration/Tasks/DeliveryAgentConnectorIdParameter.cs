using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000103 RID: 259
	[Serializable]
	public class DeliveryAgentConnectorIdParameter : ADIdParameter
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x000207C7 File Offset: 0x0001E9C7
		public DeliveryAgentConnectorIdParameter()
		{
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000207CF File Offset: 0x0001E9CF
		public DeliveryAgentConnectorIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000207D8 File Offset: 0x0001E9D8
		public DeliveryAgentConnectorIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000207E1 File Offset: 0x0001E9E1
		public DeliveryAgentConnectorIdParameter(DeliveryAgentConnector connector) : base(connector.Id)
		{
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000207EF File Offset: 0x0001E9EF
		public static DeliveryAgentConnectorIdParameter Parse(string identity)
		{
			return new DeliveryAgentConnectorIdParameter(identity);
		}
	}
}
