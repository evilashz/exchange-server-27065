using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000D7 RID: 215
	internal enum TransactionCommitMode
	{
		// Token: 0x040003DD RID: 989
		Lazy,
		// Token: 0x040003DE RID: 990
		ShortLatencyLazy,
		// Token: 0x040003DF RID: 991
		MediumLatencyLazy,
		// Token: 0x040003E0 RID: 992
		Immediate
	}
}
