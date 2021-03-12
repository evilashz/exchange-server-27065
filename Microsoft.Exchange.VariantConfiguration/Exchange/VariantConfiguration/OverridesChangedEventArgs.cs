using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200012C RID: 300
	public class OverridesChangedEventArgs : EventArgs
	{
		// Token: 0x06000E4B RID: 3659 RVA: 0x00022890 File Offset: 0x00020A90
		public OverridesChangedEventArgs(VariantConfigurationOverride[] newOverrides)
		{
			this.NewOverrides = (newOverrides ?? new VariantConfigurationOverride[0]);
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x000228A9 File Offset: 0x00020AA9
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x000228B1 File Offset: 0x00020AB1
		public VariantConfigurationOverride[] NewOverrides { get; private set; }
	}
}
