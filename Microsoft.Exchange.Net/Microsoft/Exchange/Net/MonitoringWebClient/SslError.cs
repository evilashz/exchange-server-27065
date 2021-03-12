using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007CC RID: 1996
	internal class SslError
	{
		// Token: 0x04002488 RID: 9352
		internal DateTime CreationTime = DateTime.UtcNow;

		// Token: 0x04002489 RID: 9353
		internal string Description;

		// Token: 0x0400248A RID: 9354
		internal SslErrorType SslErrorType;

		// Token: 0x0400248B RID: 9355
		internal SslPolicyErrors SslPolicyErrors;

		// Token: 0x0400248C RID: 9356
		internal X509ChainStatusFlags X509ChainStatusFlags;
	}
}
