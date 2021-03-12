using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Conversations;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x02000025 RID: 37
	internal static class ActionsProcessor
	{
		// Token: 0x0600010D RID: 269 RVA: 0x0000679C File Offset: 0x0000499C
		internal static bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (mapiEvent.ItemType != ObjectType.MAPI_MESSAGE)
			{
				return false;
			}
			if ((mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) == MapiEventFlags.None)
			{
				return false;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectModified) == (MapiEventTypeFlags)0 && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) == (MapiEventTypeFlags)0 && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectMoved) == (MapiEventTypeFlags)0)
			{
				return false;
			}
			if (string.Compare(mapiEvent.ObjectClass, "IPM.ConversationAction", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (mapiEvent.ItemEntryId == null || mapiEvent.ParentEntryId == null)
			{
				ActionsProcessor.Tracer.TraceError(0L, "{0}: Found item without entry Id or parent entry Id", new object[]
				{
					TraceContext.Get()
				});
				return false;
			}
			return mapiEvent.ClientType != MapiEventClientTypes.EventBasedAssistants && mapiEvent.ClientType != MapiEventClientTypes.Transport;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006844 File Offset: 0x00004A44
		internal static void HandleEventInternal(MapiEvent mapiEvent, MailboxSession session, StoreObject storeItem, MailboxData mailboxData)
		{
			ActionsProcessor.Tracer.TraceDebug(0L, "{0}: Calling ActionsProcessor.HandleEventInternal", new object[]
			{
				TraceContext.Get()
			});
			try
			{
				StoreObjectId conversationActionsFolderId = ConversationActionItem.GetConversationActionsFolderId(session, false);
				if (conversationActionsFolderId != null)
				{
					if (ArrayComparer<byte>.Comparer.Equals(mapiEvent.ParentEntryId, conversationActionsFolderId.ProviderLevelItemId))
					{
						ConversationActionItem conversationActionItem = storeItem as ConversationActionItem;
						if (conversationActionItem == null)
						{
							ActionsProcessor.Tracer.TraceError(0L, "{0}: HandleEventInternal received null item", new object[]
							{
								TraceContext.Get()
							});
						}
						else if (conversationActionItem.IsCorrectVersion())
						{
							bool flag = ActionsProcessor.MergeDuplicateActionItems(session, conversationActionItem, mailboxData);
							if (flag)
							{
								ActionsProcessor.Tracer.TraceDebug(0L, "{0}: Item being processed got deleted by merging duplicates", new object[]
								{
									TraceContext.Get()
								});
							}
							else
							{
								ActionsProcessor.Tracer.TraceDebug<object, ConversationId>(0L, "{0}: Processing conversation action item with conversation id = {1}", TraceContext.Get(), conversationActionItem.ConversationId);
								AggregateOperationResult result = conversationActionItem.ProcessItems(ConversationAction.AlwaysMove | ConversationAction.AlwaysDelete | ConversationAction.AlwaysCategorize);
								ActionsProcessor.TraceAggregateOperationResult(result);
								conversationActionItem.Save(SaveMode.ResolveConflicts);
							}
						}
					}
				}
			}
			catch (CorruptDataException)
			{
				ActionsProcessor.Tracer.TraceDebug(0L, "{0}: CorruptDataException thrown while processing action item", new object[]
				{
					TraceContext.Get()
				});
				byte[] valueOrDefault = storeItem.GetValueOrDefault<byte[]>(ItemSchema.ConversationIndex, new byte[0]);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_CorruptConversationActionItem, null, new object[]
				{
					session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					BitConverter.ToString(valueOrDefault)
				});
			}
			catch (ObjectNotFoundException)
			{
				ActionsProcessor.Tracer.TraceDebug(0L, "{0}: ObjectNotFoundException thrown while processing action item", new object[]
				{
					TraceContext.Get()
				});
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006A18 File Offset: 0x00004C18
		private static bool MergeDuplicateActionItems(MailboxSession session, ConversationActionItem actionItem, MailboxData mailboxData)
		{
			ActionsProcessor.Tracer.TraceDebug(0L, "{0}: Calling ActionsProcessor.MergeDuplicateActionItems", new object[]
			{
				TraceContext.Get()
			});
			ConversationId conversationId = actionItem.ConversationId;
			int totalActionItemCount;
			IList<StoreId> list = ConversationActionItem.QueryConversationActionsFolder(session, conversationId, 10, out totalActionItemCount);
			mailboxData.UpdateConversationActionsEnabledStatus(totalActionItemCount);
			if (list == null || list.Count <= 1)
			{
				ActionsProcessor.Tracer.TraceDebug<object, int>(0L, "{0}: No duplicates found.  Count = {1}", TraceContext.Get(), (list != null) ? list.Count : -1);
				return false;
			}
			StoreId[] array = new StoreId[list.Count - 1];
			bool result = false;
			for (int i = 1; i < list.Count; i++)
			{
				if (list[i].Equals(actionItem.Id))
				{
					result = true;
				}
				array[i - 1] = list[i];
			}
			ActionsProcessor.Tracer.TraceDebug<object, int>(0L, "{0}: Attempting to delete {1} duplicates...", TraceContext.Get(), array.Length);
			session.Delete(DeleteItemFlags.HardDelete, array);
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006B03 File Offset: 0x00004D03
		private static void TraceAggregateOperationResult(AggregateOperationResult result)
		{
		}

		// Token: 0x04000117 RID: 279
		private const int MaxDuplicatesToProcess = 10;

		// Token: 0x04000118 RID: 280
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;
	}
}
