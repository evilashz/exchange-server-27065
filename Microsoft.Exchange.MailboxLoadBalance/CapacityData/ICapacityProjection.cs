using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.CapacityData
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ICapacityProjection
	{
		// Token: 0x0600011D RID: 285
		BatchCapacityDatum GetCapacity();
	}
}
