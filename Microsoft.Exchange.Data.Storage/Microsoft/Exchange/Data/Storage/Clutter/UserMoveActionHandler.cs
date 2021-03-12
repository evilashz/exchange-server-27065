using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x02000444 RID: 1092
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserMoveActionHandler
	{
		// Token: 0x060030C1 RID: 12481 RVA: 0x000C7EB4 File Offset: 0x000C60B4
		private UserMoveActionHandler(MailboxSession mailboxSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, IList<StoreObjectId> itemIds, bool isUserInitiatedMove)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("sourceFolderId", sourceFolderId);
			ArgumentValidator.ThrowIfNull("destinationFolderId", destinationFolderId);
			ArgumentValidator.ThrowIfNull("itemIds", itemIds);
			if (sourceFolderId.Equals(destinationFolderId))
			{
				throw new ArgumentException("sourceFolderId should not equal destinationFolderId");
			}
			this.mailboxSession = mailboxSession;
			this.sourceFolderId = sourceFolderId;
			this.destinationFolderId = destinationFolderId;
			this.itemIds = (from id in itemIds
			where id != null
			select id).Distinct<StoreObjectId>().ToList<StoreObjectId>();
			this.isUserInitiatedMove = isUserInitiatedMove;
			this.interpretedClutterAction = this.InterpretClutterAction();
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000C7F68 File Offset: 0x000C6168
		public static bool TryCreate(MailboxSession mailboxSession, FolderChangeOperation operation, FolderChangeOperationFlags flags, GroupOperationResult result, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, out UserMoveActionHandler handler)
		{
			handler = null;
			if (operation == FolderChangeOperation.Move && result != null && result.OperationResult != OperationResult.Failed && result.ResultObjectIds != null && result.ResultObjectIds.Count > 0 && ClutterUtilities.IsClutterEnabled(mailboxSession, mailboxSession.MailboxOwner.GetConfiguration()) && sourceFolderId != null && destinationFolderId != null && !sourceFolderId.Equals(destinationFolderId))
			{
				bool flag = mailboxSession.LogonType == LogonType.Owner || flags.HasFlag(FolderChangeOperationFlags.ClutterActionByUserOverride);
				handler = new UserMoveActionHandler(mailboxSession, sourceFolderId, destinationFolderId, result.ResultObjectIds, flag);
				return true;
			}
			return false;
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000C801C File Offset: 0x000C621C
		public void HandleUserMoves()
		{
			if (this.interpretedClutterAction == null)
			{
				return;
			}
			List<StoreObjectId> list = new List<StoreObjectId>();
			HashSet<ConversationId> hashSet = new HashSet<ConversationId>();
			foreach (StoreObjectId storeObjectId in this.itemIds)
			{
				using (Item item = Item.Bind(this.mailboxSession, storeObjectId, UserMoveActionHandler.itemPropertiesToLoad))
				{
					if (this.IsProcessingNecessary(item))
					{
						item.OpenAsReadWrite();
						item[InternalSchema.IsClutter] = this.interpretedClutterAction.Value;
						if (this.isUserInitiatedMove)
						{
							item[InternalSchema.IsClutterOverridden] = true;
							ConversationId valueOrDefault = item.GetValueOrDefault<ConversationId>(ItemSchema.ConversationId);
							if (valueOrDefault != null && !hashSet.Contains(valueOrDefault))
							{
								hashSet.Add(valueOrDefault);
							}
							list.Add(storeObjectId);
						}
						item.Save(SaveMode.ResolveConflicts);
					}
				}
			}
			if (this.mailboxSession.ActivitySession != null)
			{
				this.mailboxSession.ActivitySession.CaptureMarkAsClutterOrNotClutter(list.ToDictionary((StoreObjectId id) => id, (StoreObjectId id) => this.interpretedClutterAction.Value));
			}
			this.HandleFutureMessages(hashSet);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000C8190 File Offset: 0x000C6390
		private bool? InterpretClutterAction()
		{
			StoreObjectId defaultFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Clutter);
			if (this.destinationFolderId.Equals(defaultFolderId))
			{
				return new bool?(true);
			}
			if (!this.IsFolderClutternessAmbiguous(this.destinationFolderId) && (this.sourceFolderId.Equals(defaultFolderId) || this.IsFolderClutternessAmbiguous(this.sourceFolderId)))
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000C81FC File Offset: 0x000C63FC
		private bool IsProcessingNecessary(Item item)
		{
			return item.GetValueOrDefault<bool>(ItemSchema.IsClutter, false) ^ this.interpretedClutterAction.Value;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000C8224 File Offset: 0x000C6424
		private void HandleFutureMessages(HashSet<ConversationId> affectedConversations)
		{
			foreach (ConversationId conversationId in affectedConversations)
			{
				Conversation conversation = Conversation.Load(this.mailboxSession, conversationId, UserMoveActionHandler.conversationPropertiesToLoad);
				if (!this.interpretedClutterAction.Value)
				{
					conversation.AlwaysClutterOrUnclutter(new bool?(false), false);
				}
				else if (this.IsAllConversationClutter(conversation))
				{
					conversation.AlwaysClutterOrUnclutter(new bool?(true), false);
				}
			}
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000C82B4 File Offset: 0x000C64B4
		private bool IsAllConversationClutter(Conversation conversation)
		{
			if (this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Clutter) == null)
			{
				return false;
			}
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				for (int i = 0; i < conversationTreeNode.StorePropertyBags.Count; i++)
				{
					if (!conversationTreeNode.StorePropertyBags[i].GetValueOrDefault<bool>(ItemSchema.IsClutter, false))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000C8348 File Offset: 0x000C6548
		private bool IsFolderClutternessAmbiguous(StoreObjectId folderId)
		{
			ArgumentValidator.ThrowIfNull("folderId", folderId);
			return folderId.Equals(this.mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems)) || folderId.Equals(this.mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions));
		}

		// Token: 0x04001A7A RID: 6778
		private static readonly PropertyDefinition[] itemPropertiesToLoad = new PropertyDefinition[]
		{
			ItemSchema.OriginalDeliveryFolderInfo
		};

		// Token: 0x04001A7B RID: 6779
		private static readonly PropertyDefinition[] conversationPropertiesToLoad = new PropertyDefinition[]
		{
			ItemSchema.IsClutter,
			MessageItemSchema.IsClutterOverridden
		};

		// Token: 0x04001A7C RID: 6780
		private readonly MailboxSession mailboxSession;

		// Token: 0x04001A7D RID: 6781
		private readonly StoreObjectId sourceFolderId;

		// Token: 0x04001A7E RID: 6782
		private readonly StoreObjectId destinationFolderId;

		// Token: 0x04001A7F RID: 6783
		private readonly IList<StoreObjectId> itemIds;

		// Token: 0x04001A80 RID: 6784
		private readonly bool? interpretedClutterAction;

		// Token: 0x04001A81 RID: 6785
		private readonly bool isUserInitiatedMove;
	}
}
