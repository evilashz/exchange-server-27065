using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008CC RID: 2252
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationTreeNode : ConversationTreeNodeBase
	{
		// Token: 0x060053CE RID: 21454 RVA: 0x0015C14F File Offset: 0x0015A34F
		internal ConversationTreeNode(PropertyDefinition indexPropertyDefinition, List<IStorePropertyBag> storePropertyBags, IConversationTreeNodeSorter conversationTreeNodeSorter) : base(conversationTreeNodeSorter)
		{
			ArgumentValidator.ThrowIfNull("storePropertyBags", storePropertyBags);
			this.indexPropertyDefinition = indexPropertyDefinition;
			this.storePropertyBags = storePropertyBags;
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x060053CF RID: 21455 RVA: 0x0015C171 File Offset: 0x0015A371
		public override List<IStorePropertyBag> StorePropertyBags
		{
			get
			{
				return this.storePropertyBags;
			}
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0015C17C File Offset: 0x0015A37C
		public override bool UpdatePropertyBag(StoreObjectId itemId, IStorePropertyBag bag)
		{
			for (int i = 0; i < this.storePropertyBags.Count; i++)
			{
				if (ConversationTreeNode.RetrieveStoreObjectId(this.storePropertyBags[i]).Equals(itemId))
				{
					this.storePropertyBags[i] = bag;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x0015C1C8 File Offset: 0x0015A3C8
		public static StoreObjectId RetrieveStoreObjectId(IStorePropertyBag bag)
		{
			return ((VersionedId)bag.TryGetProperty(ItemSchema.Id)).ObjectId;
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x060053D2 RID: 21458 RVA: 0x0015C1DF File Offset: 0x0015A3DF
		public override bool HasAttachments
		{
			get
			{
				return this.AnyPropertyValueIsTrue(ItemSchema.HasAttachment);
			}
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x060053D3 RID: 21459 RVA: 0x0015C1EC File Offset: 0x0015A3EC
		public override ConversationId ConversationThreadId
		{
			get
			{
				return this.GetValueOrDefault<ConversationId>(ItemSchema.ConversationFamilyId, null);
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x0015C1FA File Offset: 0x0015A3FA
		public override bool HasBeenSubmitted
		{
			get
			{
				return this.AnyPropertyValueIsTrue(MessageItemSchema.HasBeenSubmitted);
			}
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x060053D5 RID: 21461 RVA: 0x0015C207 File Offset: 0x0015A407
		public override bool IsSpecificMessageReplyStamped
		{
			get
			{
				return this.AnyPropertyValueIsTrue(MessageItemSchema.IsSpecificMessageReplyStamped);
			}
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x060053D6 RID: 21462 RVA: 0x0015C214 File Offset: 0x0015A414
		public override bool IsSpecificMessageReply
		{
			get
			{
				return this.AnyPropertyValueIsTrue(MessageItemSchema.IsSpecificMessageReply);
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x060053D7 RID: 21463 RVA: 0x0015C224 File Offset: 0x0015A424
		public override ConversationId ConversationId
		{
			get
			{
				byte[] valueOrDefault = this.GetValueOrDefault<byte[]>(ItemSchema.ConversationIndex, null);
				if (valueOrDefault != null)
				{
					return ConversationIndex.RetrieveConversationId(valueOrDefault);
				}
				return null;
			}
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x060053D8 RID: 21464 RVA: 0x0015C249 File Offset: 0x0015A449
		public override bool HasData
		{
			get
			{
				return this.StorePropertyBags.Count > 0;
			}
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x0015C259 File Offset: 0x0015A459
		public override byte[] Index
		{
			get
			{
				if (this.index == null)
				{
					this.index = this.GetValueOrDefault<byte[]>(this.indexPropertyDefinition, null);
				}
				return this.index;
			}
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x060053DA RID: 21466 RVA: 0x0015C27C File Offset: 0x0015A47C
		public override StoreObjectId MainStoreObjectId
		{
			get
			{
				List<StoreObjectId> list = this.ToListStoreObjectId();
				if (list.Count > 0)
				{
					return list[0];
				}
				return null;
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x0015C2A4 File Offset: 0x0015A4A4
		public override ExDateTime? ReceivedTime
		{
			get
			{
				return this.GetValueOrDefault<ExDateTime?>(ItemSchema.ReceivedTime, null);
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x060053DC RID: 21468 RVA: 0x0015C2C5 File Offset: 0x0015A4C5
		public override string ItemClass
		{
			get
			{
				return this.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			}
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0015C2D8 File Offset: 0x0015A4D8
		public override List<StoreObjectId> ToListStoreObjectId()
		{
			if (this.ids == null)
			{
				this.ids = new List<StoreObjectId>();
				if (this.HasData)
				{
					foreach (IStorePropertyBag bag in this.StorePropertyBags)
					{
						this.ids.Add(ConversationTreeNode.RetrieveStoreObjectId(bag));
					}
				}
			}
			return this.ids;
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x0015C358 File Offset: 0x0015A558
		public override T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue = default(T))
		{
			return this.GetValueOrDefault<T>(this.MainStoreObjectId, propertyDefinition, defaultValue);
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0015C368 File Offset: 0x0015A568
		public override T GetValueOrDefault<T>(StoreObjectId itemId, PropertyDefinition propertyDefinition, T defaultValue = default(T))
		{
			IStorePropertyBag storePropertyBag;
			if (this.TryGetPropertyBag(itemId, out storePropertyBag))
			{
				return storePropertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
			}
			return defaultValue;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0015C38C File Offset: 0x0015A58C
		public override bool TryGetPropertyBag(StoreObjectId itemId, out IStorePropertyBag bag)
		{
			bag = null;
			if (!this.HasData || itemId == null)
			{
				return false;
			}
			foreach (IStorePropertyBag storePropertyBag in this.StorePropertyBags)
			{
				if (itemId.Equals(ConversationTreeNode.RetrieveStoreObjectId(storePropertyBag)))
				{
					bag = storePropertyBag;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x060053E1 RID: 21473 RVA: 0x0015C400 File Offset: 0x0015A600
		public override IStorePropertyBag MainPropertyBag
		{
			get
			{
				IStorePropertyBag result;
				if (this.TryGetPropertyBag(this.MainStoreObjectId, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x0015C420 File Offset: 0x0015A620
		private bool AnyPropertyValueIsTrue(PropertyDefinition propertyDefinition)
		{
			if (!this.HasData)
			{
				return false;
			}
			foreach (IStorePropertyBag storePropertyBag in this.StorePropertyBags)
			{
				if (storePropertyBag.GetValueOrDefault<bool>(propertyDefinition, false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002D69 RID: 11625
		private readonly PropertyDefinition indexPropertyDefinition;

		// Token: 0x04002D6A RID: 11626
		private readonly List<IStorePropertyBag> storePropertyBags;

		// Token: 0x04002D6B RID: 11627
		private List<StoreObjectId> ids;

		// Token: 0x04002D6C RID: 11628
		private byte[] index;
	}
}
