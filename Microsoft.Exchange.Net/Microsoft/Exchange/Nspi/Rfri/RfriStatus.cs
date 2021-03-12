using System;

namespace Microsoft.Exchange.Nspi.Rfri
{
	// Token: 0x02000923 RID: 2339
	public enum RfriStatus
	{
		// Token: 0x04002B8A RID: 11146
		Success,
		// Token: 0x04002B8B RID: 11147
		GeneralFailure = -2147467259,
		// Token: 0x04002B8C RID: 11148
		InvalidObject = -2147221240,
		// Token: 0x04002B8D RID: 11149
		LogonFailed = -2147221231,
		// Token: 0x04002B8E RID: 11150
		NoSuchObject = -2147219168,
		// Token: 0x04002B8F RID: 11151
		AccessDenied = -2147024891,
		// Token: 0x04002B90 RID: 11152
		InvalidParameter = -2147024809
	}
}
