using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200023B RID: 571
	internal class VoiceMessageList
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x0004B098 File Offset: 0x00049298
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x0004B0A0 File Offset: 0x000492A0
		internal StoreObjectId CurrentMessageBeingRead { get; private set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0004B0A9 File Offset: 0x000492A9
		internal bool MessageListIsNull
		{
			get
			{
				return this.currentPagerList == null;
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004B0B4 File Offset: 0x000492B4
		internal void InitializeCurrentPagerList(MessageItemList itemList)
		{
			this.currentPagerList = itemList;
			this.currentPagerList.Start();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004B0C8 File Offset: 0x000492C8
		internal bool GetNextMessageToRead(bool readingSavedMessages)
		{
			bool flag = true;
			StoreObjectId next = this.readMessageList.GetNext();
			if (next == null)
			{
				while (this.currentPagerList.Next(!readingSavedMessages))
				{
					if (!this.ShouldWeLoopAndReadNextMessage(readingSavedMessages))
					{
						this.CurrentMessageBeingRead = this.currentPagerList.CurrentStoreObjectId;
						flag = false;
						goto IL_49;
					}
				}
				return false;
			}
			this.CurrentMessageBeingRead = next;
			IL_49:
			if (!flag)
			{
				this.readMessageList.AddToList(this.CurrentMessageBeingRead);
			}
			return true;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0004B134 File Offset: 0x00049334
		internal bool GetPreviousMessageToRead()
		{
			StoreObjectId previous = this.readMessageList.GetPrevious();
			if (previous == null)
			{
				return false;
			}
			this.CurrentMessageBeingRead = previous;
			return true;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0004B15A File Offset: 0x0004935A
		internal void DeleteMessage(StoreObjectId element)
		{
			if (!this.readMessageList.CheckIfPresentAndIgnore(element))
			{
				this.currentPagerList.Ignore(element);
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004B176 File Offset: 0x00049376
		internal void UnDeleteMessage(StoreObjectId element)
		{
			if (!this.readMessageList.CheckIfPresentAndUnIgnore(element))
			{
				this.currentPagerList.UnIgnore(element);
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004B194 File Offset: 0x00049394
		private bool ShouldWeLoopAndReadNextMessage(bool readingSavedMessages)
		{
			bool flag = this.currentPagerList.SafeGetProperty<bool>(MessageItemSchema.IsRead, false);
			return (!readingSavedMessages && flag != readingSavedMessages) || (readingSavedMessages && (flag != readingSavedMessages || this.readMessageList.Contains(this.currentPagerList.CurrentStoreObjectId)));
		}

		// Token: 0x04000B9D RID: 2973
		private MessageItemList currentPagerList;

		// Token: 0x04000B9E RID: 2974
		private VoiceMessageList.ListOfMessagesReadSoFar readMessageList = new VoiceMessageList.ListOfMessagesReadSoFar();

		// Token: 0x0200023C RID: 572
		private class ListOfMessagesReadSoFar
		{
			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0004B1F1 File Offset: 0x000493F1
			internal int Count
			{
				get
				{
					return this.mylist.Count;
				}
			}

			// Token: 0x060010BA RID: 4282 RVA: 0x0004B1FE File Offset: 0x000493FE
			internal bool Contains(StoreObjectId element)
			{
				return this.mylist.Contains(element);
			}

			// Token: 0x060010BB RID: 4283 RVA: 0x0004B20C File Offset: 0x0004940C
			internal void AddToList(StoreObjectId element)
			{
				this.mylist.Add(element);
				this.currentIndex++;
			}

			// Token: 0x060010BC RID: 4284 RVA: 0x0004B228 File Offset: 0x00049428
			internal StoreObjectId GetNext()
			{
				while (this.currentIndex >= 0 && this.currentIndex < this.mylist.Count - 1)
				{
					StoreObjectId storeObjectId = this.mylist[++this.currentIndex];
					if (!this.ignoreTable.ContainsKey(storeObjectId))
					{
						return storeObjectId;
					}
				}
				return null;
			}

			// Token: 0x060010BD RID: 4285 RVA: 0x0004B284 File Offset: 0x00049484
			internal StoreObjectId GetPrevious()
			{
				StoreObjectId storeObjectId = null;
				if (this.currentIndex <= 0)
				{
					return null;
				}
				int i = this.currentIndex;
				while (i > 0)
				{
					storeObjectId = this.mylist[--i];
					if (!this.ignoreTable.ContainsKey(storeObjectId))
					{
						this.currentIndex = i;
						break;
					}
					storeObjectId = null;
				}
				return storeObjectId;
			}

			// Token: 0x060010BE RID: 4286 RVA: 0x0004B2D6 File Offset: 0x000494D6
			internal bool CheckIfPresentAndIgnore(StoreObjectId element)
			{
				if (this.Contains(element))
				{
					this.ignoreTable.Add(element, true);
					return true;
				}
				return false;
			}

			// Token: 0x060010BF RID: 4287 RVA: 0x0004B2F1 File Offset: 0x000494F1
			internal bool CheckIfPresentAndUnIgnore(StoreObjectId element)
			{
				if (this.ignoreTable.ContainsKey(element))
				{
					this.ignoreTable.Remove(element);
					return true;
				}
				return false;
			}

			// Token: 0x04000BA0 RID: 2976
			private List<StoreObjectId> mylist = new List<StoreObjectId>(100);

			// Token: 0x04000BA1 RID: 2977
			private Dictionary<StoreObjectId, bool> ignoreTable = new Dictionary<StoreObjectId, bool>();

			// Token: 0x04000BA2 RID: 2978
			private int currentIndex = -1;
		}
	}
}
