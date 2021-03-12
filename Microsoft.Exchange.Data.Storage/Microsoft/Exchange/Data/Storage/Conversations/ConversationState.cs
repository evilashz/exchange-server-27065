using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008CE RID: 2254
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversationState
	{
		// Token: 0x060053F6 RID: 21494 RVA: 0x0015C4EB File Offset: 0x0015A6EB
		internal ConversationState(IMailboxSession session, IConversationTree conversationTree, ICollection<IConversationTreeNode> nodesToExclude)
		{
			this.conversationTree = conversationTree;
			this.session = session;
			this.treeNode = new MiniStateTreeNode(conversationTree, new Func<StoreObjectId, long>(this.StoreIdToIdHash), nodesToExclude);
			this.InitializeSerializedState();
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x0015C52B File Offset: 0x0015A72B
		internal byte[] SerializedState
		{
			get
			{
				return this.serializedState;
			}
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x0015C534 File Offset: 0x0015A734
		internal KeyValuePair<List<StoreObjectId>, List<StoreObjectId>> CalculateChanges(byte[] otherSerializedState)
		{
			Util.ThrowOnNullOrEmptyArgument(otherSerializedState, "otherSerializedState");
			MiniStateTreeNode right = MiniStateTreeNode.DeSerialize(otherSerializedState);
			KeyValuePair<List<long>, List<long>> affectedIds = this.treeNode.GetAffectedIds(right);
			KeyValuePair<List<StoreObjectId>, List<StoreObjectId>> result = new KeyValuePair<List<StoreObjectId>, List<StoreObjectId>>(new List<StoreObjectId>(affectedIds.Key.Count), new List<StoreObjectId>(affectedIds.Value.Count));
			for (int i = 0; i < affectedIds.Key.Count; i++)
			{
				result.Key.Add(this.hashToIdMap[affectedIds.Key[i]]);
			}
			for (int j = 0; j < affectedIds.Value.Count; j++)
			{
				result.Value.Add(this.hashToIdMap[affectedIds.Value[j]]);
			}
			foreach (IConversationTreeNode conversationTreeNode in this.conversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				foreach (StoreObjectId storeObjectId in conversationTreeNode2.ToListStoreObjectId())
				{
					bool valueOrDefault = conversationTreeNode2.GetValueOrDefault<bool>(storeObjectId, MessageItemSchema.IsDraft, false);
					if (valueOrDefault && !result.Key.Contains(storeObjectId) && !result.Value.Contains(storeObjectId))
					{
						result.Value.Add(storeObjectId);
					}
				}
			}
			return result;
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x0015C6D0 File Offset: 0x0015A8D0
		private long StoreIdToIdHash(StoreObjectId id)
		{
			long midFromMessageId = this.session.IdConverter.GetMidFromMessageId(id);
			try
			{
				this.hashToIdMap.Add(midFromMessageId, id);
			}
			catch (ArgumentException)
			{
				throw new StoragePermanentException(ServerStrings.ConversationContainsDuplicateMids(this.session.MailboxOwner.LegacyDn, midFromMessageId));
			}
			return midFromMessageId;
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x0015C72C File Offset: 0x0015A92C
		private void InitializeSerializedState()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.treeNode.Serialize(binaryWriter, 0);
					this.serializedState = memoryStream.ToArray();
				}
			}
		}

		// Token: 0x04002D6D RID: 11629
		private readonly IMailboxSession session;

		// Token: 0x04002D6E RID: 11630
		private IConversationTree conversationTree;

		// Token: 0x04002D6F RID: 11631
		private MiniStateTreeNode treeNode;

		// Token: 0x04002D70 RID: 11632
		private Dictionary<long, StoreObjectId> hashToIdMap = new Dictionary<long, StoreObjectId>();

		// Token: 0x04002D71 RID: 11633
		private byte[] serializedState;
	}
}
