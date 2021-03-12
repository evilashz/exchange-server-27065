using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	public class X400AuthoritativeDomainIdParameter : ADIdParameter
	{
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002B29F File Offset: 0x0002949F
		public X400AuthoritativeDomainIdParameter()
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0002B2A7 File Offset: 0x000294A7
		public X400AuthoritativeDomainIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0002B2B0 File Offset: 0x000294B0
		public X400AuthoritativeDomainIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0002B2B9 File Offset: 0x000294B9
		protected X400AuthoritativeDomainIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0002B2C2 File Offset: 0x000294C2
		public static X400AuthoritativeDomainIdParameter Parse(string identity)
		{
			return new X400AuthoritativeDomainIdParameter(identity);
		}
	}
}
