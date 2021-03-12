using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200002D RID: 45
	internal class LogConfiguration : ConfigurationSection
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000B2B2 File Offset: 0x000094B2
		[ConfigurationProperty("Environments", IsRequired = true)]
		public ProcessingEnvironmentCollection Environments
		{
			get
			{
				return (ProcessingEnvironmentCollection)base["Environments"];
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000B2C4 File Offset: 0x000094C4
		[ConfigurationProperty("ConfigSettings", IsRequired = true)]
		public ConfigCollection ConfigSettings
		{
			get
			{
				return (ConfigCollection)base["ConfigSettings"];
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000B2D6 File Offset: 0x000094D6
		[ConfigurationProperty("LogManagerPlugin", IsRequired = true)]
		public LogManagerPluginCollection LogProcessingSchemas
		{
			get
			{
				return (LogManagerPluginCollection)base["LogManagerPlugin"];
			}
		}

		// Token: 0x0400015F RID: 351
		public const string EnvironmentsKey = "Environments";

		// Token: 0x04000160 RID: 352
		public const string ConfigSettingsKey = "ConfigSettings";
	}
}
