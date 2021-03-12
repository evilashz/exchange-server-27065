using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003B3 RID: 947
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarItemSeries : ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06002B39 RID: 11065
		// (set) Token: 0x06002B3A RID: 11066
		int SeriesSequenceNumber { get; set; }
	}
}
