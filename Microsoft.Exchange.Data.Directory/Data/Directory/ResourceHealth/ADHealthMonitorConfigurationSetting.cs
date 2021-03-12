using System;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009E6 RID: 2534
	internal sealed class ADHealthMonitorConfigurationSetting : ResourceHealthMonitorConfigurationSetting
	{
		// Token: 0x17002A1C RID: 10780
		// (get) Token: 0x06007598 RID: 30104 RVA: 0x0018261A File Offset: 0x0018081A
		internal override string DisabledRegistryValueName
		{
			get
			{
				return "DisableADHealthCollection";
			}
		}

		// Token: 0x17002A1D RID: 10781
		// (get) Token: 0x06007599 RID: 30105 RVA: 0x00182621 File Offset: 0x00180821
		internal override string RefreshIntervalRegistryValueName
		{
			get
			{
				return "ADHealthRefreshInterval";
			}
		}

		// Token: 0x17002A1E RID: 10782
		// (get) Token: 0x0600759A RID: 30106 RVA: 0x00182628 File Offset: 0x00180828
		internal override string OverrideMetricValueRegistryValueName
		{
			get
			{
				return "ADMetricValue";
			}
		}

		// Token: 0x17002A1F RID: 10783
		// (get) Token: 0x0600759B RID: 30107 RVA: 0x0018262F File Offset: 0x0018082F
		internal string OverrideHealthMeasureRegistryValueName
		{
			get
			{
				return "ADHealthMeasure";
			}
		}

		// Token: 0x17002A20 RID: 10784
		// (get) Token: 0x0600759C RID: 30108 RVA: 0x00182636 File Offset: 0x00180836
		internal string HealthyCutoffRegistryValueName
		{
			get
			{
				return "ADHealthHealthyCutoff";
			}
		}

		// Token: 0x17002A21 RID: 10785
		// (get) Token: 0x0600759D RID: 30109 RVA: 0x0018263D File Offset: 0x0018083D
		internal string FairCutoffRegistryValueName
		{
			get
			{
				return "ADHealthFairCutoff";
			}
		}

		// Token: 0x17002A22 RID: 10786
		// (get) Token: 0x0600759E RID: 30110 RVA: 0x00182644 File Offset: 0x00180844
		internal override TimeSpan DefaultRefreshInterval
		{
			get
			{
				return ADHealthMonitorConfigurationSetting.refreshIntervalDefault;
			}
		}

		// Token: 0x17002A23 RID: 10787
		// (get) Token: 0x0600759F RID: 30111 RVA: 0x0018264B File Offset: 0x0018084B
		internal TimeSpan HealthyCutoffDefault
		{
			get
			{
				return ADHealthMonitorConfigurationSetting.healthyCutoffDefault;
			}
		}

		// Token: 0x17002A24 RID: 10788
		// (get) Token: 0x060075A0 RID: 30112 RVA: 0x00182652 File Offset: 0x00180852
		internal TimeSpan FairCutoffDefault
		{
			get
			{
				return ADHealthMonitorConfigurationSetting.fairCutoffDefault;
			}
		}

		// Token: 0x04004B67 RID: 19303
		private const string DisabledValueName = "DisableADHealthCollection";

		// Token: 0x04004B68 RID: 19304
		private const string RefreshIntervalValueName = "ADHealthRefreshInterval";

		// Token: 0x04004B69 RID: 19305
		private const string OverrideMetricValueName = "ADMetricValue";

		// Token: 0x04004B6A RID: 19306
		private const string OverrideHealthMeasureValueName = "ADHealthMeasure";

		// Token: 0x04004B6B RID: 19307
		private const string HealthyCutoffValueName = "ADHealthHealthyCutoff";

		// Token: 0x04004B6C RID: 19308
		private const string FairCutoffValueName = "ADHealthFairCutoff";

		// Token: 0x04004B6D RID: 19309
		private static readonly TimeSpan refreshIntervalDefault = TimeSpan.FromMinutes(3.0);

		// Token: 0x04004B6E RID: 19310
		private static readonly TimeSpan healthyCutoffDefault = TimeSpan.FromMinutes(15.0);

		// Token: 0x04004B6F RID: 19311
		private static readonly TimeSpan fairCutoffDefault = TimeSpan.FromMinutes(45.0);
	}
}
