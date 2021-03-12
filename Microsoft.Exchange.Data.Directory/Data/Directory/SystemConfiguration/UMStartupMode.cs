using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000580 RID: 1408
	public enum UMStartupMode
	{
		// Token: 0x04002C6F RID: 11375
		[LocDescription(DirectoryStrings.IDs.TCP)]
		TCP,
		// Token: 0x04002C70 RID: 11376
		[LocDescription(DirectoryStrings.IDs.TLS)]
		TLS,
		// Token: 0x04002C71 RID: 11377
		[LocDescription(DirectoryStrings.IDs.Dual)]
		Dual
	}
}
