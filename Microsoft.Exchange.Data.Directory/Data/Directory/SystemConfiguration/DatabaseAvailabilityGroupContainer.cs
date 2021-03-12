using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003E7 RID: 999
	[Serializable]
	public class DatabaseAvailabilityGroupContainer : ADConfigurationObject
	{
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000BB2EB File Offset: 0x000B94EB
		internal override ADObjectSchema Schema
		{
			get
			{
				return DatabaseAvailabilityGroupContainer.schema;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x000BB2F2 File Offset: 0x000B94F2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DatabaseAvailabilityGroupContainer.mostDerivedClass;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000BB2F9 File Offset: 0x000B94F9
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04001EBA RID: 7866
		public static readonly string DefaultName = "Database Availability Groups";

		// Token: 0x04001EBB RID: 7867
		private static DatabaseAvailabilityGroupContainerSchema schema = ObjectSchema.GetInstance<DatabaseAvailabilityGroupContainerSchema>();

		// Token: 0x04001EBC RID: 7868
		private static string mostDerivedClass = "msExchMDBAvailabilityGroupContainer";
	}
}
