using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public class InboundConnectorIdParameter : ADIdParameter
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x000218A2 File Offset: 0x0001FAA2
		public InboundConnectorIdParameter()
		{
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000218AA File Offset: 0x0001FAAA
		public InboundConnectorIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000218B3 File Offset: 0x0001FAB3
		public InboundConnectorIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000218BC File Offset: 0x0001FABC
		protected InboundConnectorIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000218C5 File Offset: 0x0001FAC5
		public static InboundConnectorIdParameter Parse(string identity)
		{
			return new InboundConnectorIdParameter(identity);
		}
	}
}
