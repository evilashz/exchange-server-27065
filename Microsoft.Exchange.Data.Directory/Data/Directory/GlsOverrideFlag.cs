using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000139 RID: 313
	[Flags]
	internal enum GlsOverrideFlag
	{
		// Token: 0x040006C7 RID: 1735
		None = 0,
		// Token: 0x040006C8 RID: 1736
		OverrideIsSet = 1,
		// Token: 0x040006C9 RID: 1737
		GlsRecordMismatch = 2,
		// Token: 0x040006CA RID: 1738
		ResourceForestMismatch = 4,
		// Token: 0x040006CB RID: 1739
		AccountForestMismatch = 8
	}
}
