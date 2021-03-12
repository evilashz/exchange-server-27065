using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000708 RID: 1800
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ItemCountAdvisor : AdvisorBase
	{
		// Token: 0x06004746 RID: 18246 RVA: 0x0012F489 File Offset: 0x0012D689
		private ItemCountAdvisor(Guid mailboxGuid, EventCondition condition, EventWatermark watermark) : base(mailboxGuid, false, condition, watermark)
		{
			if (condition.ObjectType != EventObjectType.Folder || condition.EventType != EventType.ObjectModified)
			{
				throw new ArgumentException(ServerStrings.ExInvalidItemCountAdvisorCondition);
			}
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x0012F4C2 File Offset: 0x0012D6C2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ItemCountAdvisor>(this);
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x0012F4EC File Offset: 0x0012D6EC
		public static ItemCountAdvisor Create(MailboxSession session, EventCondition condition)
		{
			return EventSink.InternalCreateEventSink<ItemCountAdvisor>(session, null, () => new ItemCountAdvisor(session.MailboxGuid, condition, null));
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x0012F54C File Offset: 0x0012D74C
		public static ItemCountAdvisor Create(MailboxSession session, EventCondition condition, EventWatermark watermark)
		{
			if (watermark == null)
			{
				throw new ArgumentNullException("watermark");
			}
			return EventSink.InternalCreateEventSink<ItemCountAdvisor>(session, watermark, () => new ItemCountAdvisor(session.MailboxGuid, condition, watermark));
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x0012F5A4 File Offset: 0x0012D7A4
		public Dictionary<StoreObjectId, ItemCountPair> GetItemCounts()
		{
			this.CheckDisposed(null);
			base.CheckException();
			Dictionary<StoreObjectId, ItemCountPair> dictionary = new Dictionary<StoreObjectId, ItemCountPair>();
			Dictionary<StoreObjectId, ItemCountPair> result = null;
			lock (base.ThisLock)
			{
				if (base.UseRecoveryValues)
				{
					result = this.recoveryItemCounts;
					this.recoveryItemCounts = dictionary;
					this.firstWatermarkInRecoveryDictionary = 0L;
				}
				else
				{
					result = this.itemCounts;
					this.itemCounts = dictionary;
					this.firstWatermarkInDictionary = 0L;
				}
			}
			return result;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x0012F62C File Offset: 0x0012D82C
		protected override void InternalStartRecovery()
		{
			base.InternalStartRecovery();
			Dictionary<StoreObjectId, ItemCountPair> dictionary = new Dictionary<StoreObjectId, ItemCountPair>();
			lock (base.ThisLock)
			{
				this.recoveryItemCounts = dictionary;
			}
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x0012F67C File Offset: 0x0012D87C
		protected override void InternalEndRecovery()
		{
			foreach (KeyValuePair<StoreObjectId, ItemCountPair> keyValuePair in this.itemCounts)
			{
				ItemCountAdvisor.UpdateItemCounts(this.recoveryItemCounts, keyValuePair.Key, keyValuePair.Value.ItemCount, keyValuePair.Value.UnreadItemCount);
			}
			this.itemCounts = this.recoveryItemCounts;
			this.recoveryItemCounts = null;
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x0012F708 File Offset: 0x0012D908
		protected override void InternalRecoveryConsumeRelevantEvent(MapiEvent mapiEvent)
		{
			string text = null;
			StoreObjectId storeObjectId = Event.GetStoreObjectId(mapiEvent, out text);
			if (this.recoveryItemCounts.Count == 0)
			{
				this.firstWatermarkInRecoveryDictionary = mapiEvent.Watermark.EventCounter;
			}
			ItemCountAdvisor.UpdateItemCounts(this.recoveryItemCounts, storeObjectId, mapiEvent.ItemCount, mapiEvent.UnreadItemCount);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x0012F758 File Offset: 0x0012D958
		protected override bool TryGetCurrentWatermark(bool isRecoveryWatermark, out EventWatermark watermark)
		{
			watermark = null;
			if (isRecoveryWatermark)
			{
				if (this.recoveryItemCounts.Count != 0)
				{
					watermark = new EventWatermark(base.MdbGuid, this.firstWatermarkInRecoveryDictionary, false);
				}
			}
			else if (this.itemCounts.Count != 0)
			{
				watermark = new EventWatermark(base.MdbGuid, this.firstWatermarkInDictionary, false);
			}
			return watermark != null;
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x0012F7B8 File Offset: 0x0012D9B8
		protected override void AdvisorInternalConsumeRelevantEvent(MapiEvent mapiEvent)
		{
			string text = null;
			StoreObjectId storeObjectId = Event.GetStoreObjectId(mapiEvent, out text);
			if (this.itemCounts.Count == 0)
			{
				this.firstWatermarkInDictionary = mapiEvent.Watermark.EventCounter;
			}
			ItemCountAdvisor.UpdateItemCounts(this.itemCounts, storeObjectId, mapiEvent.ItemCount, mapiEvent.UnreadItemCount);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x0012F806 File Offset: 0x0012DA06
		protected override bool ShouldIgnoreRecoveryEventsAfterConsume()
		{
			return false;
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x0012F80C File Offset: 0x0012DA0C
		private static void UpdateItemCounts(Dictionary<StoreObjectId, ItemCountPair> itemCountDictionary, StoreObjectId objectId, long itemCount, long unreadItemCount)
		{
			ItemCountPair itemCountPair;
			if ((itemCount < 0L || unreadItemCount < 0L) && itemCountDictionary.TryGetValue(objectId, out itemCountPair))
			{
				if (itemCount < 0L)
				{
					itemCount = itemCountPair.ItemCount;
				}
				if (unreadItemCount < 0L)
				{
					unreadItemCount = itemCountPair.UnreadItemCount;
				}
			}
			itemCountDictionary[objectId] = new ItemCountPair(itemCount, unreadItemCount);
		}

		// Token: 0x040026FD RID: 9981
		private Dictionary<StoreObjectId, ItemCountPair> itemCounts = new Dictionary<StoreObjectId, ItemCountPair>();

		// Token: 0x040026FE RID: 9982
		private long firstWatermarkInDictionary;

		// Token: 0x040026FF RID: 9983
		private long firstWatermarkInRecoveryDictionary;

		// Token: 0x04002700 RID: 9984
		private Dictionary<StoreObjectId, ItemCountPair> recoveryItemCounts;
	}
}
