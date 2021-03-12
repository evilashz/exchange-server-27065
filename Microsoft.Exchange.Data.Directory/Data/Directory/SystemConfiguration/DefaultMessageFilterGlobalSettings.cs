using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000405 RID: 1029
	[Serializable]
	public class DefaultMessageFilterGlobalSettings : ADConfigurationObject
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000BDFDE File Offset: 0x000BC1DE
		internal override ADObjectSchema Schema
		{
			get
			{
				return DefaultMessageFilterGlobalSettings.schema;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000BDFE5 File Offset: 0x000BC1E5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DefaultMessageFilterGlobalSettings.mostDerivedClass;
			}
		}

		// Token: 0x04001F76 RID: 8054
		private static DefaultMessageFilterGlobalSettingsSchema schema = ObjectSchema.GetInstance<DefaultMessageFilterGlobalSettingsSchema>();

		// Token: 0x04001F77 RID: 8055
		private static string mostDerivedClass = "msExchSMTPTurfList";

		// Token: 0x04001F78 RID: 8056
		public static readonly string DefaultName = "Default Message Filter";
	}
}
