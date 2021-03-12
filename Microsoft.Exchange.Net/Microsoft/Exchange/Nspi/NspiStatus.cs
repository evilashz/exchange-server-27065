using System;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x02000919 RID: 2329
	public enum NspiStatus
	{
		// Token: 0x04002B55 RID: 11093
		Success,
		// Token: 0x04002B56 RID: 11094
		UnbindSuccess,
		// Token: 0x04002B57 RID: 11095
		UnbindFailure,
		// Token: 0x04002B58 RID: 11096
		ErrorsReturned = 263040,
		// Token: 0x04002B59 RID: 11097
		GeneralFailure = -2147467259,
		// Token: 0x04002B5A RID: 11098
		NotSupported = -2147221246,
		// Token: 0x04002B5B RID: 11099
		NotFound = -2147221233,
		// Token: 0x04002B5C RID: 11100
		LogonFailed = -2147221231,
		// Token: 0x04002B5D RID: 11101
		TooComplex = -2147221225,
		// Token: 0x04002B5E RID: 11102
		InvalidCodePage = -2147221218,
		// Token: 0x04002B5F RID: 11103
		InvalidLocale,
		// Token: 0x04002B60 RID: 11104
		TooBig = -2147220731,
		// Token: 0x04002B61 RID: 11105
		TableTooBig = -2147220477,
		// Token: 0x04002B62 RID: 11106
		InvalidBookmark = -2147220475,
		// Token: 0x04002B63 RID: 11107
		AccessDenied = -2147024891,
		// Token: 0x04002B64 RID: 11108
		InvalidParameter = -2147024809
	}
}
