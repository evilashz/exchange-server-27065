using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public class CountryListIdParameter : ADIdParameter
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x0001FC17 File Offset: 0x0001DE17
		public CountryListIdParameter()
		{
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001FC1F File Offset: 0x0001DE1F
		public CountryListIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001FC28 File Offset: 0x0001DE28
		public CountryListIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001FC31 File Offset: 0x0001DE31
		public CountryListIdParameter(CountryList list) : base(list.Id)
		{
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001FC3F File Offset: 0x0001DE3F
		public CountryListIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001FC48 File Offset: 0x0001DE48
		public static CountryListIdParameter Parse(string identity)
		{
			return new CountryListIdParameter(identity);
		}
	}
}
