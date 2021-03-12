using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000ED RID: 237
	[Serializable]
	public class ApprovalApplicationIdParameter : ADIdParameter
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0001E8AC File Offset: 0x0001CAAC
		public ApprovalApplicationIdParameter()
		{
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
		public ApprovalApplicationIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001E8BD File Offset: 0x0001CABD
		public ApprovalApplicationIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001E8C6 File Offset: 0x0001CAC6
		public ApprovalApplicationIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001E8CF File Offset: 0x0001CACF
		public static ApprovalApplicationIdParameter Parse(string identity)
		{
			return new ApprovalApplicationIdParameter(identity);
		}
	}
}
