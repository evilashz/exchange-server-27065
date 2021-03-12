using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200035F RID: 863
	[Serializable]
	public class ADBuiltinDomain : ADConfigurationObject
	{
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x000A7313 File Offset: 0x000A5513
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADBuiltinDomain.schema;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x000A731A File Offset: 0x000A551A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADBuiltinDomain.MostDerivedClass;
			}
		}

		// Token: 0x04001835 RID: 6197
		private static ADBuiltinDomainSchema schema = ObjectSchema.GetInstance<ADBuiltinDomainSchema>();

		// Token: 0x04001836 RID: 6198
		internal static string MostDerivedClass = "builtinDomain";
	}
}
