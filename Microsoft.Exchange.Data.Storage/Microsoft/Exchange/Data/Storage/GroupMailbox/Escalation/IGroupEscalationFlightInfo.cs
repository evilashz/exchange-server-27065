using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox.Escalation
{
	// Token: 0x02000807 RID: 2055
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IGroupEscalationFlightInfo
	{
		// Token: 0x06004C99 RID: 19609
		bool IsGroupEscalationFooterEnabled();
	}
}
