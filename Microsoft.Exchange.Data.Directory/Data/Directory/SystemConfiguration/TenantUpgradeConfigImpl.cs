using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005CA RID: 1482
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TenantUpgradeConfigImpl : IDisposable, IDiagnosable
	{
		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x0010006D File Offset: 0x000FE26D
		public static IDiagnosable DiagnosableComponent
		{
			get
			{
				return TenantUpgradeConfigImpl.Instance.Value;
			}
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x00100079 File Offset: 0x000FE279
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "TenantUpgrade_config";
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x00100080 File Offset: 0x000FE280
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.provider.GetDiagnosticInfo(parameters);
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x00100090 File Offset: 0x000FE290
		private TenantUpgradeConfigImpl()
		{
			IConfigProvider configProvider = ConfigProvider.CreateADProvider(new TenantUpgradeConfigSchema(), null);
			configProvider.Initialize();
			this.provider = configProvider;
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x001000C4 File Offset: 0x000FE2C4
		public static T GetConfig<T>(string settingName)
		{
			return TenantUpgradeConfigImpl.Instance.Value.GetConfigFromProvider<T>(settingName);
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x001000D6 File Offset: 0x000FE2D6
		private T GetConfigFromProvider<T>(string settingName)
		{
			return this.provider.GetConfig<T>(settingName);
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x001000E4 File Offset: 0x000FE2E4
		public void Dispose()
		{
			this.provider.Dispose();
		}

		// Token: 0x04002EAB RID: 11947
		private IConfigProvider provider;

		// Token: 0x04002EAC RID: 11948
		private static readonly Lazy<TenantUpgradeConfigImpl> Instance = new Lazy<TenantUpgradeConfigImpl>(() => new TenantUpgradeConfigImpl(), LazyThreadSafetyMode.PublicationOnly);
	}
}
