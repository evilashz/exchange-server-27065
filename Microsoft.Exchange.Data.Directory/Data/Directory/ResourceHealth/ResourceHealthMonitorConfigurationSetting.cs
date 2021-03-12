using System;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009E5 RID: 2533
	internal abstract class ResourceHealthMonitorConfigurationSetting
	{
		// Token: 0x17002A18 RID: 10776
		// (get) Token: 0x06007593 RID: 30099
		internal abstract string DisabledRegistryValueName { get; }

		// Token: 0x17002A19 RID: 10777
		// (get) Token: 0x06007594 RID: 30100
		internal abstract string RefreshIntervalRegistryValueName { get; }

		// Token: 0x17002A1A RID: 10778
		// (get) Token: 0x06007595 RID: 30101
		internal abstract string OverrideMetricValueRegistryValueName { get; }

		// Token: 0x17002A1B RID: 10779
		// (get) Token: 0x06007596 RID: 30102
		internal abstract TimeSpan DefaultRefreshInterval { get; }
	}
}
