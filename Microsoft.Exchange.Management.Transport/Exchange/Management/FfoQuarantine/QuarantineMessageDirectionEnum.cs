using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x02000024 RID: 36
	public enum QuarantineMessageDirectionEnum
	{
		// Token: 0x0400003F RID: 63
		[LocDescription(CoreStrings.IDs.QuarantineInbound)]
		Inbound,
		// Token: 0x04000040 RID: 64
		[LocDescription(CoreStrings.IDs.QuarantineOutbound)]
		Outbound
	}
}
