using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200009E RID: 158
	internal class ConversationNodeComparer : IComparer<ConversationNode>
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x00012AAD File Offset: 0x00010CAD
		public ConversationNodeComparer(ConversationNodeSortOrder sortOrder)
		{
			this.sortOrder = sortOrder;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012ADC File Offset: 0x00010CDC
		public int Compare(ConversationNode x, ConversationNode y)
		{
			string value = null;
			string value2 = null;
			switch (this.sortOrder)
			{
			case ConversationNodeSortOrder.DateOrderAscending:
				value = x.Items.OrderBy((ItemType i) => i.DateTimeReceived, ConversationHelper.DateTimeStringComparer).FirstOrDefault<ItemType>().DateTimeReceived;
				value2 = y.Items.OrderBy((ItemType i) => i.DateTimeReceived, ConversationHelper.DateTimeStringComparer).FirstOrDefault<ItemType>().DateTimeReceived;
				break;
			case ConversationNodeSortOrder.DateOrderDescending:
				value2 = x.Items.OrderBy((ItemType i) => i.DateTimeReceived, ConversationHelper.DateTimeStringComparer).LastOrDefault<ItemType>().DateTimeReceived;
				value = y.Items.OrderBy((ItemType i) => i.DateTimeReceived, ConversationHelper.DateTimeStringComparer).LastOrDefault<ItemType>().DateTimeReceived;
				break;
			}
			if (string.IsNullOrEmpty(value))
			{
				value = DateTime.MinValue.ToString();
			}
			if (string.IsNullOrEmpty(value2))
			{
				value2 = DateTime.MinValue.ToString();
			}
			return Convert.ToDateTime(value).CompareTo(Convert.ToDateTime(value2));
		}

		// Token: 0x04000610 RID: 1552
		private ConversationNodeSortOrder sortOrder;
	}
}
