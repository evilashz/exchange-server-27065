using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005BB RID: 1467
	[Flags]
	internal enum SecurityFlags
	{
		// Token: 0x04002DDA RID: 11738
		RequireTLS = 4,
		// Token: 0x04002DDB RID: 11739
		AuthSpNego = 128,
		// Token: 0x04002DDC RID: 11740
		AuthLogin = 256,
		// Token: 0x04002DDD RID: 11741
		AuthKerberos = 2097152,
		// Token: 0x04002DDE RID: 11742
		ForceHELO = 4194304,
		// Token: 0x04002DDF RID: 11743
		IgnoreSTARTTLS = 8388608
	}
}
