using System;
using Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000007 RID: 7
	internal interface IDataProviderFactory
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30
		IADDataProvider ADDataProvider { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31
		IManagedMethodProvider ManagedMethodProvider { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32
		IMonadDataProvider MonadDataProvider { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33
		INativeMethodProvider NativeMethodProvider { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34
		IRegistryDataProvider RegistryDataProvider { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35
		IWMIDataProvider WMIDataProvider { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36
		IWebAdminDataProvider WebAdminDataProvider { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37
		IHybridConfigurationDetection HybridConfigurationDetectionProvider { get; }
	}
}
