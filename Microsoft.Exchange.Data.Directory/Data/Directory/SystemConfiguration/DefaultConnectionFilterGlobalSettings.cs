using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000407 RID: 1031
	[Serializable]
	public class DefaultConnectionFilterGlobalSettings : ADConfigurationObject
	{
		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x000BE05C File Offset: 0x000BC25C
		internal override ADObjectSchema Schema
		{
			get
			{
				return DefaultConnectionFilterGlobalSettings.schema;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002EBA RID: 11962 RVA: 0x000BE063 File Offset: 0x000BC263
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DefaultConnectionFilterGlobalSettings.mostDerivedClass;
			}
		}

		// Token: 0x04001F79 RID: 8057
		private static DefaultConnectionFilterGlobalSettingsSchema schema = ObjectSchema.GetInstance<DefaultConnectionFilterGlobalSettingsSchema>();

		// Token: 0x04001F7A RID: 8058
		private static string mostDerivedClass = "msExchSmtpConnectionTurfList";

		// Token: 0x04001F7B RID: 8059
		public static readonly string DefaultName = "Default Connection Filter";
	}
}
