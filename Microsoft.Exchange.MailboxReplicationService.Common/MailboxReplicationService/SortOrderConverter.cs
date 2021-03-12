using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000232 RID: 562
	internal class SortOrderConverter : IDataConverter<SortOrder, SortOrderData>
	{
		// Token: 0x06001DF2 RID: 7666 RVA: 0x0003DB28 File Offset: 0x0003BD28
		SortOrder IDataConverter<SortOrder, SortOrderData>.GetNativeRepresentation(SortOrderData sod)
		{
			SortOrder sortOrder = new SortOrder();
			foreach (SortOrderMember sortOrderMember in sod.Members)
			{
				if (sortOrderMember.IsCategory)
				{
					sortOrder.AddCategory((PropTag)sortOrderMember.PropTag, (SortFlags)sortOrderMember.Flags);
				}
				else
				{
					sortOrder.Add((PropTag)sortOrderMember.PropTag, (SortFlags)sortOrderMember.Flags);
				}
			}
			return sortOrder;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0003DBC0 File Offset: 0x0003BDC0
		SortOrderData IDataConverter<SortOrder, SortOrderData>.GetDataRepresentation(SortOrder so)
		{
			SortOrderData sortOrderData = new SortOrderData();
			List<SortOrderMember> soList = new List<SortOrderMember>();
			so.EnumerateSortOrder(delegate(PropTag ptag, SortFlags flags, bool isCategory, object ctx)
			{
				SortOrderMember sortOrderMember = new SortOrderMember();
				sortOrderMember.PropTag = (int)ptag;
				sortOrderMember.Flags = (int)flags;
				sortOrderMember.IsCategory = isCategory;
				soList.Add(sortOrderMember);
			}, null);
			sortOrderData.Members = soList.ToArray();
			return sortOrderData;
		}
	}
}
