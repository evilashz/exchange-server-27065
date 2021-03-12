using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200006E RID: 110
	internal class JournalConfigSchema : ConfigSchemaBase
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000141BC File Offset: 0x000123BC
		public override string Name
		{
			get
			{
				return "Journal";
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000396 RID: 918 RVA: 0x000141C3 File Offset: 0x000123C3
		// (set) Token: 0x06000397 RID: 919 RVA: 0x000141D5 File Offset: 0x000123D5
		[ConfigurationProperty("LegalInterceptTenantName", DefaultValue = "")]
		public string LegalInterceptTenantName
		{
			get
			{
				return (string)base["LegalInterceptTenantName"];
			}
			set
			{
				base["LegalInterceptTenantName"] = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000141E3 File Offset: 0x000123E3
		internal static IConfigProvider Configuration
		{
			get
			{
				return JournalConfigSchema.configProvider.Value;
			}
		}

		// Token: 0x04000237 RID: 567
		private static Lazy<IConfigProvider> configProvider = new Lazy<IConfigProvider>(delegate()
		{
			IConfigProvider configProvider = ConfigProvider.CreateProvider(new JournalConfigSchema());
			configProvider.Initialize();
			return configProvider;
		}, true);

		// Token: 0x0200006F RID: 111
		public static class Setting
		{
			// Token: 0x04000239 RID: 569
			public const string LegalInterceptTenantName = "LegalInterceptTenantName";
		}
	}
}
