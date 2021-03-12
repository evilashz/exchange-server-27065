using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F9 RID: 1017
	public enum ReportDirection
	{
		// Token: 0x04001CAA RID: 7338
		[LocDescription(CoreStrings.IDs.MessageDirectionAll)]
		All,
		// Token: 0x04001CAB RID: 7339
		[LocDescription(CoreStrings.IDs.MessageDirectionSent)]
		Sent,
		// Token: 0x04001CAC RID: 7340
		[LocDescription(CoreStrings.IDs.MessageDirectionReceived)]
		Received
	}
}
