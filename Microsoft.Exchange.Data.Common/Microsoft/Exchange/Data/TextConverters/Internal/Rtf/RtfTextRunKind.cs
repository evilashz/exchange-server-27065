using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000269 RID: 617
	internal enum RtfTextRunKind : byte
	{
		// Token: 0x04001E49 RID: 7753
		Ltrch,
		// Token: 0x04001E4A RID: 7754
		Rtlch,
		// Token: 0x04001E4B RID: 7755
		Loch,
		// Token: 0x04001E4C RID: 7756
		Hich,
		// Token: 0x04001E4D RID: 7757
		Dbch,
		// Token: 0x04001E4E RID: 7758
		Max,
		// Token: 0x04001E4F RID: 7759
		None = 255
	}
}
