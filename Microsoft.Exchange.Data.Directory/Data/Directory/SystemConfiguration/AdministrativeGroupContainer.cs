using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000351 RID: 849
	[Serializable]
	public class AdministrativeGroupContainer : ADConfigurationObject
	{
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x000A596A File Offset: 0x000A3B6A
		internal override ADObjectSchema Schema
		{
			get
			{
				return AdministrativeGroupContainer.schema;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000A5971 File Offset: 0x000A3B71
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AdministrativeGroupContainer.mostDerivedClass;
			}
		}

		// Token: 0x040017E8 RID: 6120
		public const string DefaultName = "Administrative Groups";

		// Token: 0x040017E9 RID: 6121
		private static AdministrativeGroupContainerSchema schema = ObjectSchema.GetInstance<AdministrativeGroupContainerSchema>();

		// Token: 0x040017EA RID: 6122
		private static string mostDerivedClass = "msExchAdminGroupContainer";
	}
}
