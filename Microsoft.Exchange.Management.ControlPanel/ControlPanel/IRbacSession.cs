using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000372 RID: 882
	internal interface IRbacSession : IPrincipal, IIdentity
	{
		// Token: 0x06003029 RID: 12329
		void RequestReceived();

		// Token: 0x0600302A RID: 12330
		void RequestCompleted();

		// Token: 0x0600302B RID: 12331
		void SetCurrentThreadPrincipal();
	}
}
