using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002CB RID: 715
	public interface IListViewDataSource
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001BBD RID: 7101
		string ContainerId { get; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001BBE RID: 7102
		int StartRange { get; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001BBF RID: 7103
		int EndRange { get; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001BC0 RID: 7104
		int RangeCount { get; }

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001BC1 RID: 7105
		int TotalCount { get; }

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001BC2 RID: 7106
		int TotalItemCount { get; }

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001BC3 RID: 7107
		int UnreadCount { get; }

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001BC4 RID: 7108
		bool UserHasRightToLoad { get; }

		// Token: 0x06001BC5 RID: 7109
		void Load(string seekValue, int itemCount);

		// Token: 0x06001BC6 RID: 7110
		void Load(int startRange, int itemCount);

		// Token: 0x06001BC7 RID: 7111
		void Load(ObjectId seekToObjectId, SeekDirection seekDirection, int itemCount);

		// Token: 0x06001BC8 RID: 7112
		bool LoadAdjacent(ObjectId adjacentObjectId, SeekDirection seekDirection, int itemCount);

		// Token: 0x06001BC9 RID: 7113
		bool MoveNext();

		// Token: 0x06001BCA RID: 7114
		void MoveToItem(int itemIndex);

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001BCB RID: 7115
		int CurrentItem { get; }

		// Token: 0x06001BCC RID: 7116
		string GetItemId();

		// Token: 0x06001BCD RID: 7117
		string GetItemClass();

		// Token: 0x06001BCE RID: 7118
		T GetItemProperty<T>(PropertyDefinition propertyDefinition) where T : class;

		// Token: 0x06001BCF RID: 7119
		T GetItemProperty<T>(PropertyDefinition propertyDefinition, T defaultValue);

		// Token: 0x06001BD0 RID: 7120
		object GetCurrentItem();
	}
}
