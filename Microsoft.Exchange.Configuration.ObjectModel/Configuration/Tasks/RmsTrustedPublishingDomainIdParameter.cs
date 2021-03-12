using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	public class RmsTrustedPublishingDomainIdParameter : ADIdParameter
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x000247A2 File Offset: 0x000229A2
		public RmsTrustedPublishingDomainIdParameter()
		{
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000247AA File Offset: 0x000229AA
		public RmsTrustedPublishingDomainIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000247B3 File Offset: 0x000229B3
		public RmsTrustedPublishingDomainIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000247BC File Offset: 0x000229BC
		public RmsTrustedPublishingDomainIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000247C5 File Offset: 0x000229C5
		public static RmsTrustedPublishingDomainIdParameter Parse(string identity)
		{
			return new RmsTrustedPublishingDomainIdParameter(identity);
		}
	}
}
