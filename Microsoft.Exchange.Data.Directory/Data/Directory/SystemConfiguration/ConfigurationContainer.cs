using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003C7 RID: 967
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class ConfigurationContainer : ADContainer
	{
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002C2C RID: 11308 RVA: 0x000B62C3 File Offset: 0x000B44C3
		internal override ADObjectSchema Schema
		{
			get
			{
				return ConfigurationContainer.schema;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x000B62CA File Offset: 0x000B44CA
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ConfigurationContainer.mostDerivedClass;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000B62D1 File Offset: 0x000B44D1
		internal override QueryFilter VersioningFilter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001A74 RID: 6772
		private static ConfigurationContainerSchema schema = ObjectSchema.GetInstance<ConfigurationContainerSchema>();

		// Token: 0x04001A75 RID: 6773
		private static string mostDerivedClass = "configuration";
	}
}
