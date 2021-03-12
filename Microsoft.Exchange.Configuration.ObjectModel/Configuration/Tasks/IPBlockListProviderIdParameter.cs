using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	public class IPBlockListProviderIdParameter : ADIdParameter
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x00027C10 File Offset: 0x00025E10
		public IPBlockListProviderIdParameter()
		{
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00027C18 File Offset: 0x00025E18
		public IPBlockListProviderIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00027C21 File Offset: 0x00025E21
		public IPBlockListProviderIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00027C2A File Offset: 0x00025E2A
		public IPBlockListProviderIdParameter(IPBlockListProvider provider) : base(provider.Id)
		{
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00027C38 File Offset: 0x00025E38
		public IPBlockListProviderIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00027C41 File Offset: 0x00025E41
		public static IPBlockListProviderIdParameter Parse(string identity)
		{
			return new IPBlockListProviderIdParameter(identity);
		}
	}
}
