using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000295 RID: 661
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigBase<TSchema> where TSchema : IConfigSchema, new()
	{
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0008A967 File Offset: 0x00088B67
		public static IConfigProvider Provider
		{
			get
			{
				if (ConfigBase<TSchema>.provider == null)
				{
					ConfigBase<TSchema>.InitializeConfigProvider(new Func<IConfigSchema, IConfigProvider>(ConfigProvider.CreateADProvider));
				}
				return ConfigBase<TSchema>.provider;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0008A986 File Offset: 0x00088B86
		public static ISettingsContext CurrentContext
		{
			get
			{
				return SettingsContextBase.EffectiveContext;
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0008A990 File Offset: 0x00088B90
		public static void InitializeConfigProvider(Func<IConfigSchema, IConfigProvider> configCreator)
		{
			if (configCreator == null)
			{
				throw new ArgumentNullException("configCreator");
			}
			lock (ConfigBase<TSchema>.lockObj)
			{
				if (ConfigBase<TSchema>.provider == null)
				{
					ConfigBase<TSchema>.isInitializing = true;
					try
					{
						IConfigProvider configProvider = configCreator(ConfigBase<TSchema>.Schema);
						configProvider.Initialize();
						IDisposeTrackable disposeTrackable = configProvider as IDisposeTrackable;
						if (disposeTrackable != null)
						{
							disposeTrackable.SuppressDisposeTracker();
						}
						ConfigBase<TSchema>.provider = configProvider;
					}
					finally
					{
						ConfigBase<TSchema>.isInitializing = false;
					}
				}
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0008AA28 File Offset: 0x00088C28
		public static T GetConfig<T>(string settingName)
		{
			if (ConfigBase<TSchema>.isInitializing)
			{
				return ConfigSchemaBase.GetDefaultConfig<T>(ConfigBase<TSchema>.Schema, settingName);
			}
			return ConfigBase<TSchema>.Provider.GetConfig<T>(SettingsContextBase.EffectiveContext, settingName);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0008AA52 File Offset: 0x00088C52
		public static bool TryGetConfig<T>(string settingName, out T settingValue)
		{
			if (ConfigBase<TSchema>.isInitializing)
			{
				settingValue = default(T);
				return false;
			}
			return ConfigBase<TSchema>.Provider.TryGetConfig<T>(SettingsContextBase.EffectiveContext, settingName, out settingValue);
		}

		// Token: 0x04001282 RID: 4738
		private static object lockObj = new object();

		// Token: 0x04001283 RID: 4739
		private static IConfigProvider provider;

		// Token: 0x04001284 RID: 4740
		private static bool isInitializing = false;

		// Token: 0x04001285 RID: 4741
		public static readonly TSchema Schema = (default(TSchema) == null) ? Activator.CreateInstance<TSchema>() : default(TSchema);
	}
}
