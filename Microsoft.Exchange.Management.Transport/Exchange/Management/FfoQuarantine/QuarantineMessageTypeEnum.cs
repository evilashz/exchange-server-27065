using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x02000025 RID: 37
	public enum QuarantineMessageTypeEnum
	{
		// Token: 0x04000042 RID: 66
		[LocDescription(CoreStrings.IDs.QuarantineSpam)]
		Spam,
		// Token: 0x04000043 RID: 67
		[LocDescription(CoreStrings.IDs.QuarantineTransportRule)]
		TransportRule
	}
}
