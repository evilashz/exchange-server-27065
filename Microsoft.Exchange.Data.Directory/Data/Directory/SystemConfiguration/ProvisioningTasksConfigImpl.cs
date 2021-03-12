using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200040B RID: 1035
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ProvisioningTasksConfigImpl : IDisposable, IDiagnosable
	{
		// Token: 0x06002EE1 RID: 12001 RVA: 0x000BE348 File Offset: 0x000BC548
		private ProvisioningTasksConfigImpl()
		{
			IConfigProvider configProvider = ConfigProvider.CreateADProvider(new ProvisioningTasksConfigSchema(), null);
			this.provider = configProvider;
			configProvider.Initialize();
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000BE37C File Offset: 0x000BC57C
		public void Dispose()
		{
			this.provider.Dispose();
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000BE389 File Offset: 0x000BC589
		public static T GetConfig<T>(string settingName)
		{
			return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<T>(settingName);
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x000BE39B File Offset: 0x000BC59B
		public static bool IsOrganizationSoftDeletionEnabled
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("IsOrganizationSoftDeletionEnabled");
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x000BE3B1 File Offset: 0x000BC5B1
		public static bool IsFailedOrganizationCleanupEnabled
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("IsFailedOrganizationCleanupEnabled");
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000BE3C8 File Offset: 0x000BC5C8
		public static bool UseBecAPIsforLiveId
		{
			get
			{
				string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring";
				string name2 = "ProvisioningAPI";
				bool result = ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("UseBecAPIsforLiveId");
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
				{
					if (registryKey != null)
					{
						string strA = (string)registryKey.GetValue(name2);
						if (string.Compare(strA, "BEC", StringComparison.OrdinalIgnoreCase) == 0)
						{
							result = true;
						}
						else if (string.Compare(strA, "SAPI", StringComparison.OrdinalIgnoreCase) == 0)
						{
							result = false;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000BE454 File Offset: 0x000BC654
		public static int MaxObjectFullSyncRequestsPerServiceInstance
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<int>("MaxObjectFullSyncRequestsPerServiceInstance");
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x000BE46A File Offset: 0x000BC66A
		public static bool EnableAutomatedCleaningOfCnfRbacContainer
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("EnableAutomatedCleaningOfCnfRbacContainer");
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x000BE480 File Offset: 0x000BC680
		public static bool EnableAutomatedCleaningOfCnfSoftDeletedContainer
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("EnableAutomatedCleaningOfCnfSoftDeletedContainer");
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000BE496 File Offset: 0x000BC696
		public static bool EnableAutomatedCleaningOfCnfProvisioningPolicyContainer
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("EnableAutomatedCleaningOfCnfProvisioningPolicyContainer");
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x000BE4AC File Offset: 0x000BC6AC
		public static bool EnablePowershellBasedDivergenceProcessor
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("EnablePowershellBasedDivergenceProcessor");
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000BE4C2 File Offset: 0x000BC6C2
		public static bool EnableProcessingMissingLinksInGroupDivergences
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("EnableProcessingMissingLinksInGroupDivergences");
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000BE4D8 File Offset: 0x000BC6D8
		public static bool EnableProcessingValidationDivergences
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value.GetConfigFromProvider<bool>("EnableProcessingValidationDivergences");
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000BE4EE File Offset: 0x000BC6EE
		public static IDiagnosable DiagnosableComponent
		{
			get
			{
				return ProvisioningTasksConfigImpl.Instance.Value;
			}
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000BE4FA File Offset: 0x000BC6FA
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "ProvisioningTasks Config";
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000BE501 File Offset: 0x000BC701
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.provider.GetDiagnosticInfo(parameters);
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000BE50F File Offset: 0x000BC70F
		private T GetConfigFromProvider<T>(string settingName)
		{
			return this.provider.GetConfig<T>(settingName);
		}

		// Token: 0x04001F87 RID: 8071
		private readonly IConfigProvider provider;

		// Token: 0x04001F88 RID: 8072
		private static readonly Lazy<ProvisioningTasksConfigImpl> Instance = new Lazy<ProvisioningTasksConfigImpl>(() => new ProvisioningTasksConfigImpl(), LazyThreadSafetyMode.PublicationOnly);
	}
}
