using System;
using Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection;
using Microsoft.Exchange.Setup.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000008 RID: 8
	internal class DataProviderFactory : IDataProviderFactory
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023BB File Offset: 0x000005BB
		public IADDataProvider ADDataProvider
		{
			get
			{
				if (this.adProvider == null)
				{
					this.adProvider = new ADProvider(5, 5, new TimeSpan(0, 0, 2));
				}
				return this.adProvider;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023E0 File Offset: 0x000005E0
		public IManagedMethodProvider ManagedMethodProvider
		{
			get
			{
				if (this.managedMethodProvider == null)
				{
					this.managedMethodProvider = new ManagedMethodProvider();
				}
				return this.managedMethodProvider;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023FB File Offset: 0x000005FB
		public IMonadDataProvider MonadDataProvider
		{
			get
			{
				if (this.monadProvider == null)
				{
					this.monadProvider = new MonadProvider();
				}
				return this.monadProvider;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002416 File Offset: 0x00000616
		public INativeMethodProvider NativeMethodProvider
		{
			get
			{
				if (this.nativeMethodProvider == null)
				{
					this.nativeMethodProvider = new NativeMethodProvider();
				}
				return this.nativeMethodProvider;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002431 File Offset: 0x00000631
		public IRegistryDataProvider RegistryDataProvider
		{
			get
			{
				if (this.registryDataProvider == null)
				{
					this.registryDataProvider = new RegistryDataProvider();
				}
				return this.registryDataProvider;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000244C File Offset: 0x0000064C
		public IWMIDataProvider WMIDataProvider
		{
			get
			{
				if (this.wmiDataProvider == null)
				{
					this.wmiDataProvider = new WMIProvider();
				}
				return this.wmiDataProvider;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002467 File Offset: 0x00000667
		public IWebAdminDataProvider WebAdminDataProvider
		{
			get
			{
				if (this.webAdminDataProvider == null)
				{
					this.webAdminDataProvider = new WebAdminDataProvider();
				}
				return this.webAdminDataProvider;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002484 File Offset: 0x00000684
		public IHybridConfigurationDetection HybridConfigurationDetectionProvider
		{
			get
			{
				if (this.hybridConfigurationDetectionProvider == null)
				{
					BridgeLogger logger = new BridgeLogger(SetupLogger.Logger);
					this.hybridConfigurationDetectionProvider = new HybridConfigurationDetection(logger);
				}
				return this.hybridConfigurationDetectionProvider;
			}
		}

		// Token: 0x04000009 RID: 9
		private ADProvider adProvider;

		// Token: 0x0400000A RID: 10
		private MonadProvider monadProvider;

		// Token: 0x0400000B RID: 11
		private ManagedMethodProvider managedMethodProvider;

		// Token: 0x0400000C RID: 12
		private NativeMethodProvider nativeMethodProvider;

		// Token: 0x0400000D RID: 13
		private RegistryDataProvider registryDataProvider;

		// Token: 0x0400000E RID: 14
		private WMIProvider wmiDataProvider;

		// Token: 0x0400000F RID: 15
		private WebAdminDataProvider webAdminDataProvider;

		// Token: 0x04000010 RID: 16
		private HybridConfigurationDetection hybridConfigurationDetectionProvider;
	}
}
