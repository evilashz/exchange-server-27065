using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000146 RID: 326
	[Serializable]
	public class SendConnectorIdParameter : ADIdParameter
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002501E File Offset: 0x0002321E
		public SendConnectorIdParameter()
		{
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00025026 File Offset: 0x00023226
		public SendConnectorIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002502F File Offset: 0x0002322F
		public SendConnectorIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00025038 File Offset: 0x00023238
		public SendConnectorIdParameter(SendConnector connector) : base(connector.Id)
		{
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00025046 File Offset: 0x00023246
		public SendConnectorIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002504F File Offset: 0x0002324F
		public static SendConnectorIdParameter Parse(string identity)
		{
			return new SendConnectorIdParameter(identity);
		}
	}
}
