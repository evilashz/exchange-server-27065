using System;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.DataProvider
{
	// Token: 0x020000B5 RID: 181
	internal abstract class ConfigDataProviderFactory
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00013D7C File Offset: 0x00011F7C
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00013D83 File Offset: 0x00011F83
		internal static EnvironmentStrategy Environment
		{
			get
			{
				return ConfigDataProviderFactory.environment;
			}
			set
			{
				ConfigDataProviderFactory.environment = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00013D8C File Offset: 0x00011F8C
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00013DF1 File Offset: 0x00011FF1
		internal static ConfigDataProviderFactory CacheDefault
		{
			get
			{
				if (ConfigDataProviderFactory.cacheInstance == null)
				{
					if (!ConfigDataProviderFactory.environment.IsForefrontForOffice() && !ConfigDataProviderFactory.environment.IsForefrontDALOverrideUseSQL())
					{
						throw new NotImplementedException("On-premise provider factory not yet implemented");
					}
					Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.CacheDataProvider");
					Type type = assembly.GetType("Microsoft.Exchange.Hygiene.Cache.DataProvider.CacheDataProvider+Factory");
					ConfigDataProviderFactory.cacheInstance = (ConfigDataProviderFactory)Activator.CreateInstance(type);
				}
				return ConfigDataProviderFactory.cacheInstance;
			}
			set
			{
				ConfigDataProviderFactory.cacheInstance = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00013DFC File Offset: 0x00011FFC
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00013E61 File Offset: 0x00012061
		internal static ConfigDataProviderFactory CacheFallbackDefault
		{
			get
			{
				if (ConfigDataProviderFactory.compositeInstance == null)
				{
					if (!ConfigDataProviderFactory.environment.IsForefrontForOffice() && !ConfigDataProviderFactory.environment.IsForefrontDALOverrideUseSQL())
					{
						throw new NotImplementedException("On-premise provider factory not yet implemented");
					}
					Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.CacheDataProvider");
					Type type = assembly.GetType("Microsoft.Exchange.Hygiene.Cache.DataProvider.CompositeDataProvider+Factory");
					ConfigDataProviderFactory.compositeInstance = (ConfigDataProviderFactory)Activator.CreateInstance(type);
				}
				return ConfigDataProviderFactory.compositeInstance;
			}
			set
			{
				ConfigDataProviderFactory.compositeInstance = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00013E6C File Offset: 0x0001206C
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00013ED0 File Offset: 0x000120D0
		internal static ConfigDataProviderFactory OpticsDefault
		{
			get
			{
				if (ConfigDataProviderFactory.opticsInstance == null)
				{
					if (ConfigDataProviderFactory.environment.IsForefrontForOffice() || ConfigDataProviderFactory.environment.IsForefrontDALOverrideUseSQL())
					{
						Assembly assembly = Assembly.Load("Microsoft.Exchange.Data.StreamingOptics.Apps");
						Type type = assembly.GetType("Microsoft.Exchange.Data.StreamingOptics.Apps.OpticsDataProvider+Factory");
						ConfigDataProviderFactory.opticsInstance = (ConfigDataProviderFactory)Activator.CreateInstance(type);
					}
					else
					{
						ConfigDataProviderFactory.opticsInstance = ConfigDataProviderFactory.CreateFactoryInstanceUsingDALWebService();
					}
				}
				return ConfigDataProviderFactory.opticsInstance;
			}
			set
			{
				ConfigDataProviderFactory.opticsInstance = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00013ED8 File Offset: 0x000120D8
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00013F3C File Offset: 0x0001213C
		internal static ConfigDataProviderFactory KEStoreDefault
		{
			get
			{
				if (ConfigDataProviderFactory.keStoreInstance == null)
				{
					if (ConfigDataProviderFactory.environment.IsForefrontForOffice() || ConfigDataProviderFactory.environment.IsForefrontDALOverrideUseSQL())
					{
						Assembly assembly = Assembly.Load("Microsoft.Exchange.AntiSpam.KEStore.Core");
						Type type = assembly.GetType("Microsoft.Exchange.AntiSpam.KEStore.Core.KEStoreDataProvider+DataProviderFactory");
						ConfigDataProviderFactory.keStoreInstance = (ConfigDataProviderFactory)Activator.CreateInstance(type);
					}
					else
					{
						ConfigDataProviderFactory.keStoreInstance = ConfigDataProviderFactory.CreateFactoryInstanceUsingDALWebService();
					}
				}
				return ConfigDataProviderFactory.keStoreInstance;
			}
			set
			{
				ConfigDataProviderFactory.keStoreInstance = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00013F44 File Offset: 0x00012144
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x00013F80 File Offset: 0x00012180
		internal static ConfigDataProviderFactory Default
		{
			get
			{
				if (ConfigDataProviderFactory.defaultInstance == null)
				{
					if (ConfigDataProviderFactory.environment.IsForefrontForOffice() || ConfigDataProviderFactory.environment.IsForefrontDALOverrideUseSQL())
					{
						ConfigDataProviderFactory.defaultInstance = ConfigDataProviderFactory.CreateFactoryInstanceUsingDALSQL();
					}
					else
					{
						ConfigDataProviderFactory.defaultInstance = ConfigDataProviderFactory.CreateFactoryInstanceUsingDALWebService();
					}
				}
				return ConfigDataProviderFactory.defaultInstance;
			}
			set
			{
				ConfigDataProviderFactory.defaultInstance = value;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00013F88 File Offset: 0x00012188
		internal static IConfigDataProvider CreateDataProvider(DatabaseType store)
		{
			switch (store)
			{
			case DatabaseType.Optics:
				return ConfigDataProviderFactory.OpticsDefault.Create(store);
			case DatabaseType.KEStore:
				return ConfigDataProviderFactory.KEStoreDefault.Create(store);
			default:
				return ConfigDataProviderFactory.Default.Create(store);
			}
		}

		// Token: 0x06000600 RID: 1536
		internal abstract IConfigDataProvider Create(DatabaseType store);

		// Token: 0x06000601 RID: 1537 RVA: 0x00013FD0 File Offset: 0x000121D0
		private static ConfigDataProviderFactory CreateFactoryInstanceUsingDALSQL()
		{
			Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.WebStoreDataProvider");
			Type type = assembly.GetType("Microsoft.Exchange.Hygiene.WebStoreDataProvider.WstDataProviderFactory");
			return (ConfigDataProviderFactory)Activator.CreateInstance(type);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00014000 File Offset: 0x00012200
		private static ConfigDataProviderFactory CreateFactoryInstanceUsingDALWebService()
		{
			Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.WebserviceDataProvider");
			Type type = assembly.GetType("Microsoft.Exchange.Hygiene.WebserviceDataProvider.WebserviceDataProviderFactory");
			return (ConfigDataProviderFactory)Activator.CreateInstance(type);
		}

		// Token: 0x040003AD RID: 941
		private static ConfigDataProviderFactory cacheInstance;

		// Token: 0x040003AE RID: 942
		private static ConfigDataProviderFactory compositeInstance;

		// Token: 0x040003AF RID: 943
		private static ConfigDataProviderFactory opticsInstance;

		// Token: 0x040003B0 RID: 944
		private static ConfigDataProviderFactory keStoreInstance;

		// Token: 0x040003B1 RID: 945
		private static ConfigDataProviderFactory defaultInstance;

		// Token: 0x040003B2 RID: 946
		private static EnvironmentStrategy environment = new EnvironmentStrategy();
	}
}
