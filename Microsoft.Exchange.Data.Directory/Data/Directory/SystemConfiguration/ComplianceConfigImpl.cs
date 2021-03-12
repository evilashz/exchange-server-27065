using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003BE RID: 958
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ComplianceConfigImpl : IDisposable, IDiagnosable
	{
		// Token: 0x06002BFA RID: 11258 RVA: 0x000B5D58 File Offset: 0x000B3F58
		private ComplianceConfigImpl()
		{
			IConfigProvider configProvider = ConfigProvider.CreateADProvider(new ComplianceConfigSchema(), null);
			this.provider = configProvider;
			configProvider.Initialize();
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000B5D8C File Offset: 0x000B3F8C
		public void Dispose()
		{
			this.provider.Dispose();
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000B5D99 File Offset: 0x000B3F99
		public static T GetConfig<T>(string settingName)
		{
			return ComplianceConfigImpl.Instance.Value.GetConfigFromProvider<T>(settingName);
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000B5DAB File Offset: 0x000B3FAB
		public static bool JournalArchivingHardeningEnabled
		{
			get
			{
				return ComplianceConfigImpl.Instance.Value.GetConfigFromProvider<bool>("JournalArchivingHardeningEnabled");
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000B5DC1 File Offset: 0x000B3FC1
		public static bool ArchivePropertiesHardeningEnabled
		{
			get
			{
				return ComplianceConfigImpl.Instance.Value.GetConfigFromProvider<bool>("ArchivePropertiesHardeningEnabled");
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000B5DD7 File Offset: 0x000B3FD7
		public static IDiagnosable DiagnosableComponent
		{
			get
			{
				return ComplianceConfigImpl.Instance.Value;
			}
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000B5DE3 File Offset: 0x000B3FE3
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "Compliance Config";
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000B5DEA File Offset: 0x000B3FEA
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.provider.GetDiagnosticInfo(parameters);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000B5DF8 File Offset: 0x000B3FF8
		private T GetConfigFromProvider<T>(string settingName)
		{
			return this.provider.GetConfig<T>(settingName);
		}

		// Token: 0x04001A5C RID: 6748
		private readonly IConfigProvider provider;

		// Token: 0x04001A5D RID: 6749
		private static readonly Lazy<ComplianceConfigImpl> Instance = new Lazy<ComplianceConfigImpl>(() => new ComplianceConfigImpl(), LazyThreadSafetyMode.PublicationOnly);
	}
}
