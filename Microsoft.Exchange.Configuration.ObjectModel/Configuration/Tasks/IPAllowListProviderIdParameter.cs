using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000160 RID: 352
	[Serializable]
	public class IPAllowListProviderIdParameter : ADIdParameter
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x00027BD7 File Offset: 0x00025DD7
		public IPAllowListProviderIdParameter()
		{
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00027BDF File Offset: 0x00025DDF
		public IPAllowListProviderIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00027BE8 File Offset: 0x00025DE8
		public IPAllowListProviderIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00027BF1 File Offset: 0x00025DF1
		public IPAllowListProviderIdParameter(IPAllowListProvider provider) : base(provider.Id)
		{
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00027BFF File Offset: 0x00025DFF
		public IPAllowListProviderIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00027C08 File Offset: 0x00025E08
		public static IPAllowListProviderIdParameter Parse(string identity)
		{
			return new IPAllowListProviderIdParameter(identity);
		}
	}
}
