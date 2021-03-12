using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005CD RID: 1485
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TenantRelocationConfigImpl : IDisposable, IDiagnosable
	{
		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x001003C5 File Offset: 0x000FE5C5
		public static IDiagnosable DiagnosableComponent
		{
			get
			{
				return TenantRelocationConfigImpl.Instance.Value;
			}
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x001003D1 File Offset: 0x000FE5D1
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "TenantRelocation_config";
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x001003D8 File Offset: 0x000FE5D8
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.provider.GetDiagnosticInfo(parameters);
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x001003E8 File Offset: 0x000FE5E8
		private TenantRelocationConfigImpl()
		{
			IConfigProvider configProvider = ConfigProvider.CreateADProvider(new TenantRelocationConfigSchema(), null);
			configProvider.Initialize();
			this.provider = configProvider;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x0010041C File Offset: 0x000FE61C
		public static T GetConfig<T>(string settingName)
		{
			return TenantRelocationConfigImpl.Instance.Value.GetConfigFromProvider<T>(settingName);
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x0010042E File Offset: 0x000FE62E
		private T GetConfigFromProvider<T>(string settingName)
		{
			return this.provider.GetConfig<T>(settingName);
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x0010043C File Offset: 0x000FE63C
		public void Dispose()
		{
			this.provider.Dispose();
		}

		// Token: 0x04002EBF RID: 11967
		private IConfigProvider provider;

		// Token: 0x04002EC0 RID: 11968
		private static readonly Lazy<TenantRelocationConfigImpl> Instance = new Lazy<TenantRelocationConfigImpl>(() => new TenantRelocationConfigImpl(), LazyThreadSafetyMode.PublicationOnly);
	}
}
