using System;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000668 RID: 1640
	internal sealed class RefreshCompletedEventArgs : EventArgs
	{
		// Token: 0x06004CAB RID: 19627 RVA: 0x0011B227 File Offset: 0x00119427
		public RefreshCompletedEventArgs(bool changed, VariantConfigurationOverride[] overrides)
		{
			this.IsChanged = changed;
			this.Overrides = (overrides ?? new VariantConfigurationOverride[0]);
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06004CAC RID: 19628 RVA: 0x0011B247 File Offset: 0x00119447
		// (set) Token: 0x06004CAD RID: 19629 RVA: 0x0011B24F File Offset: 0x0011944F
		public bool IsChanged { get; private set; }

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x0011B258 File Offset: 0x00119458
		// (set) Token: 0x06004CAF RID: 19631 RVA: 0x0011B260 File Offset: 0x00119460
		public VariantConfigurationOverride[] Overrides { get; private set; }
	}
}
