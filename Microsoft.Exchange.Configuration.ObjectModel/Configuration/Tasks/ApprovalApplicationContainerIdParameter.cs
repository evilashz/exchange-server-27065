using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public class ApprovalApplicationContainerIdParameter : ADIdParameter
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x0001E881 File Offset: 0x0001CA81
		public ApprovalApplicationContainerIdParameter()
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001E889 File Offset: 0x0001CA89
		public ApprovalApplicationContainerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001E892 File Offset: 0x0001CA92
		public ApprovalApplicationContainerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001E89B File Offset: 0x0001CA9B
		public ApprovalApplicationContainerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		public static ApprovalApplicationContainerIdParameter Parse(string identity)
		{
			return new ApprovalApplicationContainerIdParameter(identity);
		}
	}
}
