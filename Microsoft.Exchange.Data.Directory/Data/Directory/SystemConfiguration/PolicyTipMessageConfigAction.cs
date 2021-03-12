using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006A5 RID: 1701
	public enum PolicyTipMessageConfigAction
	{
		// Token: 0x040035AE RID: 13742
		[LocDescription(CoreStrings.IDs.PolicyTipNotifyOnly)]
		NotifyOnly,
		// Token: 0x040035AF RID: 13743
		[LocDescription(CoreStrings.IDs.PolicyTipRejectOverride)]
		RejectOverride,
		// Token: 0x040035B0 RID: 13744
		[LocDescription(CoreStrings.IDs.PolicyTipReject)]
		Reject,
		// Token: 0x040035B1 RID: 13745
		[LocDescription(CoreStrings.IDs.PolicyTipUrl)]
		Url
	}
}
