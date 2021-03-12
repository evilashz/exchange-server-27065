using System;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x02000889 RID: 2185
	internal enum TopLevelStatusCode
	{
		// Token: 0x04002875 RID: 10357
		Success = 1,
		// Token: 0x04002876 RID: 10358
		RequestIPNotFoundInACL = 3101,
		// Token: 0x04002877 RID: 10359
		HttpsRequired,
		// Token: 0x04002878 RID: 10360
		NotAuthorized,
		// Token: 0x04002879 RID: 10361
		UnknownCertificate,
		// Token: 0x0400287A RID: 10362
		RequestSyntaxError = 4102,
		// Token: 0x0400287B RID: 10363
		ConcurrentOperation = 4108,
		// Token: 0x0400287C RID: 10364
		TooManyCommands = 4301
	}
}
