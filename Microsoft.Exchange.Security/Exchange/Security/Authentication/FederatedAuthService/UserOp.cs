using System;
using System.Threading;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200008B RID: 139
	internal class UserOp
	{
		// Token: 0x04000534 RID: 1332
		public string User;

		// Token: 0x04000535 RID: 1333
		public ManualResetEvent HrdEvent;

		// Token: 0x04000536 RID: 1334
		public DomainConfig NamespaceInfo;

		// Token: 0x04000537 RID: 1335
		public ManualResetEvent StsEvent;

		// Token: 0x04000538 RID: 1336
		public int refCount;
	}
}
