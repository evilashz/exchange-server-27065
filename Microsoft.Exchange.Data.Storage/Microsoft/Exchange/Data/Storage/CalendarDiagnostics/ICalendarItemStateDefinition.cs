using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x0200035E RID: 862
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarItemStateDefinition : IEquatable<ICalendarItemStateDefinition>, IEqualityComparer<CalendarItemState>
	{
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600266C RID: 9836
		bool IsRecurringMasterSpecific { get; }

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600266D RID: 9837
		string SchemaKey { get; }

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600266E RID: 9838
		StorePropertyDefinition[] RequiredProperties { get; }

		// Token: 0x0600266F RID: 9839
		bool Evaluate(CalendarItemState state, PropertyBag propertyBag, MailboxSession session);
	}
}
