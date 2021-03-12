using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000135 RID: 309
	[Serializable]
	public class OutboundConnectorIdParameter : ADIdParameter
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x00023B0C File Offset: 0x00021D0C
		public OutboundConnectorIdParameter()
		{
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00023B14 File Offset: 0x00021D14
		public OutboundConnectorIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00023B1D File Offset: 0x00021D1D
		public OutboundConnectorIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00023B26 File Offset: 0x00021D26
		protected OutboundConnectorIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00023B2F File Offset: 0x00021D2F
		public static OutboundConnectorIdParameter Parse(string identity)
		{
			return new OutboundConnectorIdParameter(identity);
		}
	}
}
