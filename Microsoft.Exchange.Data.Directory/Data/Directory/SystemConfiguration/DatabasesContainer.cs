using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003D6 RID: 982
	[Serializable]
	public class DatabasesContainer : ADConfigurationObject
	{
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000B9598 File Offset: 0x000B7798
		internal override ADObjectSchema Schema
		{
			get
			{
				return DatabasesContainer.schema;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x000B959F File Offset: 0x000B779F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DatabasesContainer.mostDerivedClass;
			}
		}

		// Token: 0x04001E3F RID: 7743
		public static readonly string DefaultName = "Databases";

		// Token: 0x04001E40 RID: 7744
		private static DatabasesContainerSchema schema = ObjectSchema.GetInstance<DatabasesContainerSchema>();

		// Token: 0x04001E41 RID: 7745
		private static string mostDerivedClass = "msExchMDBContainer";
	}
}
