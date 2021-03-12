using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B66 RID: 2918
	internal class FailedToIssueTokenException : WSTrustException
	{
		// Token: 0x06003E79 RID: 15993 RVA: 0x000A307F File Offset: 0x000A127F
		public FailedToIssueTokenException(Exception innerException) : base(WSTrustStrings.FailedToIssueToken(innerException), innerException)
		{
		}
	}
}
