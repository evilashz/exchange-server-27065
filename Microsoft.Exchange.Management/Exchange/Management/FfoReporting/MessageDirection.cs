using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003EC RID: 1004
	public enum MessageDirection
	{
		// Token: 0x04001C42 RID: 7234
		[LocDescription(CoreStrings.IDs.MessageDirectionAll)]
		All,
		// Token: 0x04001C43 RID: 7235
		[LocDescription(CoreStrings.IDs.MessageDirectionSent)]
		Sent,
		// Token: 0x04001C44 RID: 7236
		[LocDescription(CoreStrings.IDs.MessageDirectionReceived)]
		Received
	}
}
