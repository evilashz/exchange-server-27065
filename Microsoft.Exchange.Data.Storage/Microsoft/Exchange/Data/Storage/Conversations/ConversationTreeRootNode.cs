using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008CD RID: 2253
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationTreeRootNode : ConversationTreeNodeBase
	{
		// Token: 0x060053E3 RID: 21475 RVA: 0x0015C488 File Offset: 0x0015A688
		internal ConversationTreeRootNode(IConversationTreeNodeSorter conversationTreeNodeSorter) : base(conversationTreeNodeSorter)
		{
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x0015C491 File Offset: 0x0015A691
		public override bool HasData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x060053E5 RID: 21477 RVA: 0x0015C494 File Offset: 0x0015A694
		public override List<IStorePropertyBag> StorePropertyBags
		{
			get
			{
				return new List<IStorePropertyBag>();
			}
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x0015C49B File Offset: 0x0015A69B
		public override bool UpdatePropertyBag(StoreObjectId itemId, IStorePropertyBag bag)
		{
			return false;
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x0015C49E File Offset: 0x0015A69E
		public override bool HasAttachments
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x060053E8 RID: 21480 RVA: 0x0015C4A1 File Offset: 0x0015A6A1
		public override bool HasBeenSubmitted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x060053E9 RID: 21481 RVA: 0x0015C4A4 File Offset: 0x0015A6A4
		public override bool IsSpecificMessageReplyStamped
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x060053EA RID: 21482 RVA: 0x0015C4A7 File Offset: 0x0015A6A7
		public override bool IsSpecificMessageReply
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x0015C4AA File Offset: 0x0015A6AA
		public override ConversationId ConversationId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x060053EC RID: 21484 RVA: 0x0015C4AD File Offset: 0x0015A6AD
		public override ConversationId ConversationThreadId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x060053ED RID: 21485 RVA: 0x0015C4B0 File Offset: 0x0015A6B0
		public override byte[] Index
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x060053EE RID: 21486 RVA: 0x0015C4B3 File Offset: 0x0015A6B3
		public override StoreObjectId MainStoreObjectId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x0015C4B8 File Offset: 0x0015A6B8
		public override ExDateTime? ReceivedTime
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x0015C4CE File Offset: 0x0015A6CE
		public override string ItemClass
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x0015C4D5 File Offset: 0x0015A6D5
		public override List<StoreObjectId> ToListStoreObjectId()
		{
			return new List<StoreObjectId>();
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x0015C4DC File Offset: 0x0015A6DC
		public override T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue = default(T))
		{
			return defaultValue;
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x0015C4DF File Offset: 0x0015A6DF
		public override T GetValueOrDefault<T>(StoreObjectId itemId, PropertyDefinition propertyDefinition, T defaultValue = default(T))
		{
			return defaultValue;
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x0015C4E2 File Offset: 0x0015A6E2
		public override bool TryGetPropertyBag(StoreObjectId itemId, out IStorePropertyBag bag)
		{
			bag = null;
			return false;
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x060053F5 RID: 21493 RVA: 0x0015C4E8 File Offset: 0x0015A6E8
		public override IStorePropertyBag MainPropertyBag
		{
			get
			{
				return null;
			}
		}
	}
}
