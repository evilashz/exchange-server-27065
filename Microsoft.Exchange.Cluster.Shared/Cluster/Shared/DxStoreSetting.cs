using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200009F RID: 159
	public class DxStoreSetting
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x00016716 File Offset: 0x00014916
		private DxStoreSetting()
		{
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00016720 File Offset: 0x00014920
		public static DxStoreSetting Instance
		{
			get
			{
				if (DxStoreSetting.instance == null)
				{
					lock (DxStoreSetting.singletonLocker)
					{
						if (DxStoreSetting.instance == null)
						{
							DxStoreSetting dxStoreSetting = new DxStoreSetting();
							SettingOverrideSync.Instance.Start(true);
							DxStoreSetting.instance = dxStoreSetting;
						}
					}
				}
				return DxStoreSetting.instance;
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00016784 File Offset: 0x00014984
		public static void RegisterADPerfCounters(string instanceName)
		{
			Globals.InitializeMultiPerfCounterInstance(instanceName);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001678C File Offset: 0x0001498C
		public IActiveManagerSettings GetSettings()
		{
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).HighAvailability.ActiveManager;
		}

		// Token: 0x0400032B RID: 811
		private static DxStoreSetting instance;

		// Token: 0x0400032C RID: 812
		private static object singletonLocker = new object();
	}
}
