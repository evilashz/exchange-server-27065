using System;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002AE RID: 686
	internal interface ICalendarDataSource
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001AC2 RID: 6850
		int Count { get; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001AC3 RID: 6851
		WorkingHours WorkingHours { get; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001AC4 RID: 6852
		bool UserCanReadItem { get; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001AC5 RID: 6853
		bool UserCanCreateItem { get; }

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001AC6 RID: 6854
		string FolderClassName { get; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001AC7 RID: 6855
		SharedType SharedType { get; }

		// Token: 0x06001AC8 RID: 6856
		OwaStoreObjectId GetItemId(int index);

		// Token: 0x06001AC9 RID: 6857
		string GetChangeKey(int index);

		// Token: 0x06001ACA RID: 6858
		ExDateTime GetStartTime(int index);

		// Token: 0x06001ACB RID: 6859
		ExDateTime GetEndTime(int index);

		// Token: 0x06001ACC RID: 6860
		string GetSubject(int index);

		// Token: 0x06001ACD RID: 6861
		string GetLocation(int index);

		// Token: 0x06001ACE RID: 6862
		bool IsMeeting(int index);

		// Token: 0x06001ACF RID: 6863
		bool IsCancelled(int index);

		// Token: 0x06001AD0 RID: 6864
		bool HasAttachment(int index);

		// Token: 0x06001AD1 RID: 6865
		bool IsPrivate(int index);

		// Token: 0x06001AD2 RID: 6866
		CalendarItemTypeWrapper GetWrappedItemType(int index);

		// Token: 0x06001AD3 RID: 6867
		string GetOrganizerDisplayName(int index);

		// Token: 0x06001AD4 RID: 6868
		BusyTypeWrapper GetWrappedBusyType(int index);

		// Token: 0x06001AD5 RID: 6869
		bool IsOrganizer(int index);

		// Token: 0x06001AD6 RID: 6870
		string[] GetCategories(int index);

		// Token: 0x06001AD7 RID: 6871
		string GetCssClassName(int index);

		// Token: 0x06001AD8 RID: 6872
		string GetInviteesDisplayNames(int index);
	}
}
