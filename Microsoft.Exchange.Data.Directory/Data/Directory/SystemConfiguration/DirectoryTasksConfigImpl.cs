using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000408 RID: 1032
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DirectoryTasksConfigImpl : IDisposable, IDiagnosable
	{
		// Token: 0x06002EBC RID: 11964 RVA: 0x000BE08C File Offset: 0x000BC28C
		private DirectoryTasksConfigImpl()
		{
			IConfigProvider configProvider = ConfigProvider.CreateADProvider(new DirectoryTasksConfigSchema(), null);
			configProvider.Initialize();
			this.provider = configProvider;
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000BE0C0 File Offset: 0x000BC2C0
		public static IDiagnosable DiagnosableComponent
		{
			get
			{
				return DirectoryTasksConfigImpl.Instance.Value;
			}
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000BE0CC File Offset: 0x000BC2CC
		public void Dispose()
		{
			this.provider.Dispose();
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000BE0D9 File Offset: 0x000BC2D9
		public static T GetConfig<T>(string settingName)
		{
			return DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<T>(settingName);
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x000BE0EB File Offset: 0x000BC2EB
		public static bool IsDirectoryTaskProcessingEnabled
		{
			get
			{
				return DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("IsDirectoryTaskProcessingEnabled");
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000BE101 File Offset: 0x000BC301
		public static int MaxConcurrentNonRecurringTasks
		{
			get
			{
				return (int)DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<uint>("MaxConcurrentNonRecurringTasks");
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000BE117 File Offset: 0x000BC317
		public static string[] OffersRequiringSCT
		{
			get
			{
				return DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<string[]>("OffersRequiringSCT");
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000BE12D File Offset: 0x000BC32D
		public static int DelayBetweenSCTChecksInMinutes
		{
			get
			{
				return (int)DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<uint>("DelayBetweenSCTChecksInMinutes");
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000BE143 File Offset: 0x000BC343
		public static int SCTTaskMaxStartDelayInMinutes
		{
			get
			{
				return (int)DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<uint>("SCTTaskMaxRandomStartDelayInMinutes");
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x000BE159 File Offset: 0x000BC359
		public static int SCTCreateNumberOfRetries
		{
			get
			{
				return (int)DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<uint>("SCTCreateNumberOfRetries");
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002EC6 RID: 11974 RVA: 0x000BE16F File Offset: 0x000BC36F
		public static int SCTCreateDelayBetweenRetriesInSeconds
		{
			get
			{
				return (int)DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<uint>("SCTCreateDelayBetweenRetriesInSeconds");
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000BE185 File Offset: 0x000BC385
		public static bool SCTCreateUseADHealthMonitor
		{
			get
			{
				return DirectoryTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("SCTCreateUseADHealthMonitor");
			}
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000BE19B File Offset: 0x000BC39B
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "DirectoryTasks_config";
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000BE1A2 File Offset: 0x000BC3A2
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.provider.GetDiagnosticInfo(parameters);
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000BE1B0 File Offset: 0x000BC3B0
		private T GetConfigFromProvider<T>(string settingName)
		{
			return this.provider.GetConfig<T>(settingName);
		}

		// Token: 0x04001F7C RID: 8060
		private IConfigProvider provider;

		// Token: 0x04001F7D RID: 8061
		private static readonly Lazy<DirectoryTasksConfigImpl> Instance = new Lazy<DirectoryTasksConfigImpl>(() => new DirectoryTasksConfigImpl(), LazyThreadSafetyMode.PublicationOnly);
	}
}
