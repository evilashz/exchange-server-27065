using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Conversations
{
	// Token: 0x0200039F RID: 927
	internal sealed class ConversationRequestArguments
	{
		// Token: 0x06001A10 RID: 6672 RVA: 0x00096401 File Offset: 0x00094601
		public ConversationRequestArguments(int maxItemsToReturn, bool returnSubmittedItems, ConversationNodeSortOrder sortOrder)
		{
			this.SortOrder = ConversationRequestArguments.ConvertEwstoXsoSortOrder(sortOrder);
			this.ReturnSubmittedItems = returnSubmittedItems;
			this.MaxItemsToReturn = maxItemsToReturn;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x00096423 File Offset: 0x00094623
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0009642B File Offset: 0x0009462B
		public int MaxItemsToReturn { get; private set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x00096434 File Offset: 0x00094634
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x0009643C File Offset: 0x0009463C
		public bool ReturnSubmittedItems { get; private set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x00096445 File Offset: 0x00094645
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x0009644D File Offset: 0x0009464D
		public ConversationTreeSortOrder SortOrder { get; private set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x00096456 File Offset: 0x00094656
		public bool IsNewestOnTop
		{
			get
			{
				return this.SortOrder == ConversationTreeSortOrder.ChronologicalDescending || this.SortOrder == ConversationTreeSortOrder.DeepTraversalDescending || this.SortOrder == ConversationTreeSortOrder.TraversalChronologicalDescending;
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00096475 File Offset: 0x00094675
		private static ConversationTreeSortOrder ConvertEwstoXsoSortOrder(ConversationNodeSortOrder sortOrder)
		{
			if (sortOrder != ConversationNodeSortOrder.DateOrderDescending)
			{
				return ConversationTreeSortOrder.TraversalChronologicalAscending;
			}
			return ConversationTreeSortOrder.TraversalChronologicalDescending;
		}
	}
}
