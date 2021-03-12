using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public class FederationTrustIdParameter : ADIdParameter
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x00021344 File Offset: 0x0001F544
		public FederationTrustIdParameter()
		{
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002134C File Offset: 0x0001F54C
		public FederationTrustIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00021355 File Offset: 0x0001F555
		public FederationTrustIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002135E File Offset: 0x0001F55E
		protected FederationTrustIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00021367 File Offset: 0x0001F567
		public static FederationTrustIdParameter Parse(string identity)
		{
			return new FederationTrustIdParameter(identity);
		}
	}
}
