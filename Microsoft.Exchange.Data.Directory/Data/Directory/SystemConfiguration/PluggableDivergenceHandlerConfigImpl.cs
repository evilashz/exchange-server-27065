using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200040E RID: 1038
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PluggableDivergenceHandlerConfigImpl : IDisposable, IDiagnosable
	{
		// Token: 0x06002F0C RID: 12044 RVA: 0x000BE6F4 File Offset: 0x000BC8F4
		private PluggableDivergenceHandlerConfigImpl()
		{
			IConfigProvider configProvider = ConfigProvider.CreateADProvider(new PluggableDivergenceHandlerConfigSchema(), null);
			this.provider = configProvider;
			configProvider.Initialize();
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000BE728 File Offset: 0x000BC928
		public void Dispose()
		{
			this.provider.Dispose();
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000BE735 File Offset: 0x000BC935
		public static T GetConfig<T>(string settingName)
		{
			return PluggableDivergenceHandlerConfigImpl.Instance.Value.GetConfigFromProvider<T>(settingName);
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000BE747 File Offset: 0x000BC947
		public static string PluggableDivergenceHandlerConfig
		{
			get
			{
				return PluggableDivergenceHandlerConfigImpl.Instance.Value.GetConfigFromProvider<string>("ProvisioningDivergenceHandlerConfig");
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000BE75D File Offset: 0x000BC95D
		public static IDiagnosable DiagnosableComponent
		{
			get
			{
				return PluggableDivergenceHandlerConfigImpl.Instance.Value;
			}
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000BE769 File Offset: 0x000BC969
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "ProvisioningDivergenceHandler Config";
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000BE770 File Offset: 0x000BC970
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.provider.GetDiagnosticInfo(parameters);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000BE77E File Offset: 0x000BC97E
		private T GetConfigFromProvider<T>(string settingName)
		{
			return this.provider.GetConfig<T>(settingName);
		}

		// Token: 0x04001F94 RID: 8084
		private readonly IConfigProvider provider;

		// Token: 0x04001F95 RID: 8085
		private static readonly Lazy<PluggableDivergenceHandlerConfigImpl> Instance = new Lazy<PluggableDivergenceHandlerConfigImpl>(() => new PluggableDivergenceHandlerConfigImpl(), LazyThreadSafetyMode.PublicationOnly);
	}
}
