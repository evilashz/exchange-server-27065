using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C6 RID: 710
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarItem : ICalendarItemInstance, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001E87 RID: 7815
		string InternetMessageId { get; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001E88 RID: 7816
		// (set) Token: 0x06001E89 RID: 7817
		int InstanceCreationIndex { get; set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001E8A RID: 7818
		// (set) Token: 0x06001E8B RID: 7819
		bool HasExceptionalInboxReminders { get; set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001E8C RID: 7820
		// (set) Token: 0x06001E8D RID: 7821
		Recurrence Recurrence { get; set; }

		// Token: 0x06001E8E RID: 7822
		CalendarItemOccurrence OpenOccurrence(StoreObjectId id, params PropertyDefinition[] prefetchPropertyDefinitions);
	}
}
