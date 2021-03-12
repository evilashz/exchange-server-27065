using System;
using System.Net;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000377 RID: 887
	internal static class CustomHttpStatusCode
	{
		// Token: 0x04002354 RID: 9044
		public const HttpStatusCode IdentityAccepted = (HttpStatusCode)241;

		// Token: 0x04002355 RID: 9045
		public const HttpStatusCode NeedIdentity = (HttpStatusCode)441;

		// Token: 0x04002356 RID: 9046
		public const HttpStatusCode DelegatedSecurityTokenExpired = (HttpStatusCode)443;
	}
}
