using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public class HybridConfigurationIdParameter : ADIdParameter
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0001F404 File Offset: 0x0001D604
		public HybridConfigurationIdParameter()
		{
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001F40C File Offset: 0x0001D60C
		public HybridConfigurationIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001F415 File Offset: 0x0001D615
		public HybridConfigurationIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001F41E File Offset: 0x0001D61E
		public HybridConfigurationIdParameter(HybridConfiguration hybridRelationship) : base(hybridRelationship.Id)
		{
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001F42C File Offset: 0x0001D62C
		public HybridConfigurationIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001F435 File Offset: 0x0001D635
		public static HybridConfigurationIdParameter Parse(string identity)
		{
			return new HybridConfigurationIdParameter(identity);
		}

		// Token: 0x0400025E RID: 606
		internal const string FixedValue = "Hybrid Configuration";
	}
}
