using System;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001F6 RID: 502
	[Flags]
	[Serializable]
	public enum ConfigFlags
	{
		// Token: 0x04000AAF RID: 2735
		None = 0,
		// Token: 0x04000AB0 RID: 2736
		DisallowADConfig = 1,
		// Token: 0x04000AB1 RID: 2737
		DisallowAppConfig = 2,
		// Token: 0x04000AB2 RID: 2738
		LowADConfigPriority = 4
	}
}
