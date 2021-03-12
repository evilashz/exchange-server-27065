using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x0200065E RID: 1630
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigProvider : ConfigProviderBase
	{
		// Token: 0x06004C47 RID: 19527 RVA: 0x0011A1E8 File Offset: 0x001183E8
		protected ConfigProvider(IConfigSchema schema) : base(schema)
		{
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0011A1F1 File Offset: 0x001183F1
		public static IConfigProvider CreateProvider(IConfigSchema schema)
		{
			return ConfigProvider.CreateProvider(schema, new TimeSpan?(ConfigDriverBase.DefaultErrorThresholdInterval));
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0011A204 File Offset: 0x00118404
		public static IConfigProvider CreateProvider(IConfigSchema schema, TimeSpan? errorThresholdInterval)
		{
			ConfigFlags overrideFlags = ConfigProviderBase.OverrideFlags;
			if ((overrideFlags & ConfigFlags.DisallowADConfig) == ConfigFlags.DisallowADConfig)
			{
				return ConfigProvider.CreateAppProvider(schema);
			}
			if ((overrideFlags & ConfigFlags.DisallowAppConfig) == ConfigFlags.DisallowAppConfig)
			{
				return ConfigProvider.CreateADProvider(schema);
			}
			ConfigProvider configProvider = new ConfigProvider(schema);
			ConfigDriverBase configDriver = new ADConfigDriver(schema, errorThresholdInterval);
			ConfigDriverBase configDriver2 = new AppConfigDriver(schema, errorThresholdInterval);
			if ((overrideFlags & ConfigFlags.LowADConfigPriority) == ConfigFlags.LowADConfigPriority)
			{
				configProvider.AddConfigDriver(configDriver2);
				configProvider.AddConfigDriver(configDriver);
			}
			else
			{
				configProvider.AddConfigDriver(configDriver);
				configProvider.AddConfigDriver(configDriver2);
			}
			return configProvider;
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0011A26D File Offset: 0x0011846D
		public static IConfigProvider CreateADProvider(IConfigSchema schema)
		{
			return ConfigProvider.CreateADProvider(schema, new TimeSpan?(ConfigDriverBase.DefaultErrorThresholdInterval));
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0011A280 File Offset: 0x00118480
		public static IConfigProvider CreateADProvider(IConfigSchema schema, TimeSpan? errorThresholdInterval)
		{
			ConfigFlags overrideFlags = ConfigProviderBase.OverrideFlags;
			if ((overrideFlags & ConfigFlags.DisallowADConfig) == ConfigFlags.DisallowADConfig)
			{
				return ConfigProvider.CreateDefaultValueProvider(schema);
			}
			ConfigProvider configProvider = new ConfigProvider(schema);
			configProvider.AddConfigDriver(new ADConfigDriver(schema, errorThresholdInterval));
			return configProvider;
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0011A2B5 File Offset: 0x001184B5
		public static IConfigProvider CreateAppProvider(IConfigSchema schema)
		{
			return ConfigProvider.CreateAppProvider(schema, new TimeSpan?(ConfigDriverBase.DefaultErrorThresholdInterval));
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0011A2C8 File Offset: 0x001184C8
		public static IConfigProvider CreateAppProvider(IConfigSchema schema, TimeSpan? errorThresholdInterval)
		{
			ConfigFlags overrideFlags = ConfigProviderBase.OverrideFlags;
			if ((overrideFlags & ConfigFlags.DisallowAppConfig) == ConfigFlags.DisallowAppConfig)
			{
				return ConfigProvider.CreateDefaultValueProvider(schema);
			}
			ConfigProvider configProvider = new ConfigProvider(schema);
			configProvider.AddConfigDriver(new AppConfigDriver(schema, errorThresholdInterval));
			return configProvider;
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0011A2FD File Offset: 0x001184FD
		public static IConfigProvider CreateDefaultValueProvider(IConfigSchema schema)
		{
			return new ConfigProvider(schema);
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0011A305 File Offset: 0x00118505
		public override T GetConfig<T>(string settingName)
		{
			return base.GetConfig<T>(SettingsContextBase.EffectiveContext, settingName);
		}
	}
}
