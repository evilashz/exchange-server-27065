using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000104 RID: 260
	internal enum DispatchStepResult
	{
		// Token: 0x04000685 RID: 1669
		RedirectToUrl,
		// Token: 0x04000686 RID: 1670
		RewritePath,
		// Token: 0x04000687 RID: 1671
		RewritePathToError,
		// Token: 0x04000688 RID: 1672
		EndResponse,
		// Token: 0x04000689 RID: 1673
		EndResponseWithPrivateCaching,
		// Token: 0x0400068A RID: 1674
		Stop,
		// Token: 0x0400068B RID: 1675
		Continue
	}
}
