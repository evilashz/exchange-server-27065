using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public class InformationStoreIdParameter : ADIdParameter
	{
		// Token: 0x06000A0A RID: 2570 RVA: 0x000218CD File Offset: 0x0001FACD
		public InformationStoreIdParameter()
		{
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000218D5 File Offset: 0x0001FAD5
		public InformationStoreIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000218DE File Offset: 0x0001FADE
		public InformationStoreIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000218E7 File Offset: 0x0001FAE7
		public InformationStoreIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000218F0 File Offset: 0x0001FAF0
		public static InformationStoreIdParameter Parse(string identity)
		{
			return new InformationStoreIdParameter(identity);
		}
	}
}
