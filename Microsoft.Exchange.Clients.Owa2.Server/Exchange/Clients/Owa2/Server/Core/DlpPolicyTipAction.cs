using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C9 RID: 969
	public enum DlpPolicyTipAction
	{
		// Token: 0x04001196 RID: 4502
		NotifyOnly = 1,
		// Token: 0x04001197 RID: 4503
		RejectUnlessSilentOverride,
		// Token: 0x04001198 RID: 4504
		RejectUnlessExplicitOverride,
		// Token: 0x04001199 RID: 4505
		RejectUnlessFalsePositiveOverride,
		// Token: 0x0400119A RID: 4506
		RejectMessage
	}
}
