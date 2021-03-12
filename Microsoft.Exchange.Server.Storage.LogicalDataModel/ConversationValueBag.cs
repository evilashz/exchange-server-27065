using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000027 RID: 39
	public class ConversationValueBag : IColumnValueBag
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00034544 File Offset: 0x00032744
		public ConversationValueBag(ConversationItem conversationItem, ExchangeId folderId, ConversationMembers conversationMembers)
		{
			this.conversationItem = conversationItem;
			this.folderId = folderId;
			this.conversationMembers = conversationMembers;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00034561 File Offset: 0x00032761
		public ConversationValueBag(ConversationItem conversationItem, ICollection<FidMid> originalMembersFilterList, ICollection<FidMid> membersFilterList, ConversationMembers conversationMembers)
		{
			this.conversationItem = conversationItem;
			this.originalMembersFilterList = originalMembersFilterList;
			this.membersFilterList = membersFilterList;
			this.conversationMembers = conversationMembers;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00034588 File Offset: 0x00032788
		public object GetColumnValue(Context context, Column column)
		{
			object result;
			if (column is PropertyColumn && ConversationMembers.IsAggregateProperty(((PropertyColumn)column).StorePropTag))
			{
				StorePropTag storePropTag = ((PropertyColumn)column).StorePropTag;
				result = this.GetAggregateProperty(context, storePropTag);
			}
			else
			{
				result = this.conversationItem.GetColumnValue(context, column);
			}
			return result;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000345D8 File Offset: 0x000327D8
		public object GetOriginalColumnValue(Context context, Column column)
		{
			object result;
			if (column is PropertyColumn && ConversationMembers.IsAggregateProperty(((PropertyColumn)column).StorePropTag))
			{
				StorePropTag storePropTag = ((PropertyColumn)column).StorePropTag;
				result = this.GetOriginalAggregateProperty(context, storePropTag);
			}
			else
			{
				result = this.conversationItem.GetOriginalColumnValue(context, column);
			}
			return result;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00034625 File Offset: 0x00032825
		public bool IsColumnChanged(Context context, Column column)
		{
			return 0 != ValueHelper.ValuesCompare(this.GetColumnValue(context, column), this.GetOriginalColumnValue(context, column));
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00034642 File Offset: 0x00032842
		public void SetInstanceNumber(Context context, object instanceNumber)
		{
			this.conversationItem.SetInstanceNumber(context, instanceNumber);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00034654 File Offset: 0x00032854
		private object GetAggregateProperty(Context context, StorePropTag proptag)
		{
			object aggregateProperty;
			if (this.cachedAggregateProperties == null || !this.cachedAggregateProperties.TryGetValue(proptag, out aggregateProperty))
			{
				if (this.folderId.IsValid)
				{
					aggregateProperty = this.conversationMembers.GetAggregateProperty(context, proptag, this.folderId, false);
				}
				else
				{
					aggregateProperty = this.conversationMembers.GetAggregateProperty(context, proptag, this.membersFilterList, false);
				}
				if (this.cachedAggregateProperties == null)
				{
					this.cachedAggregateProperties = new Dictionary<StorePropTag, object>(30);
				}
				this.cachedAggregateProperties.Add(proptag, aggregateProperty);
			}
			return aggregateProperty;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000346D8 File Offset: 0x000328D8
		private object GetOriginalAggregateProperty(Context context, StorePropTag proptag)
		{
			object aggregateProperty;
			if (this.cachedOriginalAggregateProperties == null || !this.cachedOriginalAggregateProperties.TryGetValue(proptag, out aggregateProperty))
			{
				if (this.folderId.IsValid)
				{
					aggregateProperty = this.conversationMembers.GetAggregateProperty(context, proptag, this.folderId, true);
				}
				else
				{
					aggregateProperty = this.conversationMembers.GetAggregateProperty(context, proptag, this.originalMembersFilterList, true);
				}
				if (this.cachedOriginalAggregateProperties == null)
				{
					this.cachedOriginalAggregateProperties = new Dictionary<StorePropTag, object>(30);
				}
				this.cachedOriginalAggregateProperties.Add(proptag, aggregateProperty);
			}
			return aggregateProperty;
		}

		// Token: 0x0400026A RID: 618
		private readonly ConversationItem conversationItem;

		// Token: 0x0400026B RID: 619
		private readonly ExchangeId folderId;

		// Token: 0x0400026C RID: 620
		private readonly ICollection<FidMid> originalMembersFilterList;

		// Token: 0x0400026D RID: 621
		private readonly ICollection<FidMid> membersFilterList;

		// Token: 0x0400026E RID: 622
		private readonly ConversationMembers conversationMembers;

		// Token: 0x0400026F RID: 623
		private Dictionary<StorePropTag, object> cachedAggregateProperties;

		// Token: 0x04000270 RID: 624
		private Dictionary<StorePropTag, object> cachedOriginalAggregateProperties;
	}
}
