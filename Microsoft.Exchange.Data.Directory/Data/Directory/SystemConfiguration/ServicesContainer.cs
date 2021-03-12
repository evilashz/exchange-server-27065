using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200059A RID: 1434
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class ServicesContainer : ADNonExchangeObject
	{
		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x000FB3E3 File Offset: 0x000F95E3
		internal override ADObjectSchema Schema
		{
			get
			{
				return ServicesContainer.schema;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x000FB3EA File Offset: 0x000F95EA
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ServicesContainer.mostDerivedClass;
			}
		}

		// Token: 0x04002D59 RID: 11609
		internal static readonly string DefaultName = "Services";

		// Token: 0x04002D5A RID: 11610
		private static ServicesContainerSchema schema = ObjectSchema.GetInstance<ServicesContainerSchema>();

		// Token: 0x04002D5B RID: 11611
		private static string mostDerivedClass = "container";
	}
}
