using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Conversations;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x02000026 RID: 38
	internal static class SentItemsProcessor
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00006B14 File Offset: 0x00004D14
		internal static bool IsEventInteresting(MapiEvent mapiEvent, MailboxData mailboxData)
		{
			if (mapiEvent.ItemType != ObjectType.MAPI_MESSAGE)
			{
				return false;
			}
			if ((mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) != MapiEventFlags.None)
			{
				return false;
			}
			if (mapiEvent.ItemEntryId == null || mapiEvent.ParentEntryId == null)
			{
				SentItemsProcessor.Tracer.TraceError(0L, "{0}: Found item without entry Id or parent entry Id", new object[]
				{
					TraceContext.Get()
				});
				return false;
			}
			bool flag = ObjectClass.IsSmsMessage(mapiEvent.ObjectClass);
			if (mapiEvent.ClientType == MapiEventClientTypes.Transport || (mapiEvent.ClientType == MapiEventClientTypes.AirSync && flag))
			{
				if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) != (MapiEventTypeFlags)0)
				{
					if (mailboxData == null)
					{
						return true;
					}
					DefaultFolderType defaultFolderType = mailboxData.MatchCachedDefaultFolderType(mapiEvent.ParentEntryId);
					if (defaultFolderType == DefaultFolderType.SentItems)
					{
						SentItemsProcessor.Tracer.TraceDebug(0L, "{0}: Found item created in sent items by Transport", new object[]
						{
							TraceContext.Get()
						});
						return flag || mailboxData.ConversationActionsEnabled;
					}
				}
			}
			else if ((mapiEvent.ClientType == MapiEventClientTypes.MOMT || mapiEvent.ClientType == MapiEventClientTypes.User || mapiEvent.ClientType == MapiEventClientTypes.RpcHttp) && flag && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectMoved) != (MapiEventTypeFlags)0)
			{
				if (mailboxData == null)
				{
					return true;
				}
				DefaultFolderType defaultFolderType2 = mailboxData.MatchCachedDefaultFolderType(mapiEvent.ParentEntryId);
				DefaultFolderType defaultFolderType3 = mailboxData.MatchCachedDefaultFolderType(mapiEvent.OldParentEntryId);
				if (defaultFolderType2 == DefaultFolderType.SentItems && defaultFolderType3 == DefaultFolderType.Outbox)
				{
					SentItemsProcessor.Tracer.TraceDebug(0L, "{0}: Found item moved to sent items by Outlook", new object[]
					{
						TraceContext.Get()
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006C64 File Offset: 0x00004E64
		internal static void HandleEventInternal(MapiEvent mapiEvent, MailboxSession session, StoreObject storeItem, MailboxData mailboxData)
		{
			SentItemsProcessor.Tracer.TraceDebug(0L, "{0}: Calling SentItemsProcessor.HandleEventInternal", new object[]
			{
				TraceContext.Get()
			});
			Item item = (Item)storeItem;
			if (item == null)
			{
				SentItemsProcessor.Tracer.TraceError(0L, "{0}: HandleEventInternal received null item", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			if (ObjectClass.IsSmsMessage(item.ClassName))
			{
				try
				{
					ConversationIndexTrackingEx conversationIndexTrackingEx = ConversationIndexTrackingEx.Create();
					SentItemsProcessor.HandleSmsMessage(session, item, mailboxData, conversationIndexTrackingEx);
					SentItemsProcessor.Tracer.TraceDebug<object, ConversationIndexTrackingEx>(0L, "{0}: ChunkSmsConversation traces: {1}", TraceContext.Get(), conversationIndexTrackingEx);
				}
				catch (ObjectNotFoundException)
				{
					SentItemsProcessor.Tracer.TraceDebug(0L, "{0}: ObjectNotFoundException thrown while processing SMS item in Sent Items folder", new object[]
					{
						TraceContext.Get()
					});
				}
				return;
			}
			try
			{
				ConversationId valueOrDefault = item.GetValueOrDefault<ConversationId>(ItemSchema.ConversationId);
				if (valueOrDefault == null)
				{
					SentItemsProcessor.Tracer.TraceError(0L, "{0}: Found item without conversation id", new object[]
					{
						TraceContext.Get()
					});
				}
				else
				{
					int totalActionItemCount;
					IList<StoreId> list = ConversationActionItem.QueryConversationActionsFolder(session, valueOrDefault, 1, out totalActionItemCount);
					StoreId storeId = (list != null) ? list[0] : null;
					mailboxData.UpdateConversationActionsEnabledStatus(totalActionItemCount);
					if (storeId == null)
					{
						SentItemsProcessor.Tracer.TraceDebug<object, ConversationId>(0L, "{0}: No action item associated found for message with conversation Id = {1}", TraceContext.Get(), valueOrDefault);
					}
					else
					{
						using (ConversationActionItem conversationActionItem = ConversationActionItem.Bind(session, storeId))
						{
							if (conversationActionItem.IsCorrectVersion())
							{
								if (conversationActionItem.AlwaysCategorizeValue != null)
								{
									List<string> categoriesForItem = conversationActionItem.GetCategoriesForItem(item.TryGetProperty(ItemSchema.Categories) as string[]);
									if (categoriesForItem != null)
									{
										SentItemsProcessor.Tracer.TraceDebug(0L, "{0}: Processed categories on item", new object[]
										{
											TraceContext.Get()
										});
										item.SafeSetProperty(ItemSchema.Categories, categoriesForItem.ToArray());
										item.Save(SaveMode.ResolveConflicts);
										item.Load();
									}
								}
								conversationActionItem.Save(SaveMode.ResolveConflicts);
							}
						}
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				SentItemsProcessor.Tracer.TraceDebug(0L, "{0}: ObjectNotFoundException thrown while processing action item", new object[]
				{
					TraceContext.Get()
				});
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006EAC File Offset: 0x000050AC
		private static void HandleSmsMessage(MailboxSession session, Item item, MailboxData mailboxData, ConversationIndexTrackingEx indexTrackingEx)
		{
			MessageItem messageItem = item as MessageItem;
			if (messageItem == null)
			{
				SentItemsProcessor.Tracer.TraceDebug(0L, "{0}: the SMS message is not MessageItem", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			RecipientCollection recipients = messageItem.Recipients;
			if (recipients.Count == 0)
			{
				return;
			}
			using (SmsRecipientInfoCache smsRecipientInfoCache = SmsRecipientInfoCache.Create(session, SentItemsProcessor.Tracer))
			{
				Dictionary<ConversationIndex, Recipient> dictionary = new Dictionary<ConversationIndex, Recipient>(recipients.Count);
				for (int i = recipients.Count - 1; i >= 0; i--)
				{
					Recipient recipient = recipients[i];
					Participant participant = recipient.Participant;
					if (!(participant == null) && !string.IsNullOrEmpty(participant.EmailAddress))
					{
						string text = null;
						if (string.Equals(participant.RoutingType, "MOBILE", StringComparison.OrdinalIgnoreCase))
						{
							text = participant.EmailAddress;
							smsRecipientInfoCache.AddRecipient(participant);
						}
						else if (string.Equals(participant.RoutingType, "SMTP", StringComparison.OrdinalIgnoreCase))
						{
							ProxyAddress proxyAddress;
							if (SmtpProxyAddress.TryDeencapsulate(participant.EmailAddress, out proxyAddress) && string.Equals(proxyAddress.PrefixString, "MOBILE", StringComparison.OrdinalIgnoreCase))
							{
								text = proxyAddress.AddressString;
							}
							smsRecipientInfoCache.AddRecipient(new Participant(participant.DisplayName, text, "MOBILE"));
						}
						if (text != null)
						{
							ConversationIndex conversationIndex = ConversationIndex.GenerateFromPhoneNumber(text);
							if (!(conversationIndex == ConversationIndex.Empty))
							{
								recipients.RemoveAt(i);
								if (!dictionary.ContainsKey(conversationIndex))
								{
									dictionary.Add(conversationIndex, recipient);
								}
							}
						}
					}
				}
				if (recipients.Count > 0)
				{
					messageItem.Save(SaveMode.ResolveConflicts);
					messageItem.Load();
				}
				int num = 0;
				foreach (KeyValuePair<ConversationIndex, Recipient> keyValuePair in dictionary)
				{
					num++;
					AggregationBySmsItemClassProcessor.ChunkSmsConversation(XSOFactory.Default, session, keyValuePair.Key, indexTrackingEx);
					if (num < dictionary.Count || recipients.Count > 0)
					{
						SentItemsProcessor.CloneSmsItem(session, messageItem, mailboxData, keyValuePair.Value, keyValuePair.Key);
					}
					else
					{
						recipients.Add(keyValuePair.Value);
						SentItemsProcessor.SaveSmsItem(messageItem, keyValuePair.Key);
						messageItem.Load();
					}
				}
				smsRecipientInfoCache.Commit();
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000710C File Offset: 0x0000530C
		private static void CloneSmsItem(MailboxSession session, MessageItem source, MailboxData mailboxData, Recipient recipient, ConversationIndex conversationIndex)
		{
			using (MessageItem messageItem = MessageItem.CloneMessage(session, StoreObjectId.FromProviderSpecificId(mailboxData.SentItemsFolderId), source))
			{
				RecipientCollection recipients = messageItem.Recipients;
				recipients.Clear();
				recipients.Add(recipient);
				SentItemsProcessor.SaveSmsItem(messageItem, conversationIndex);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007164 File Offset: 0x00005364
		private static void SaveSmsItem(MessageItem message, ConversationIndex conversationIndex)
		{
			try
			{
				message[ItemSchema.ConversationIndex] = conversationIndex.ToByteArray();
				message[ItemSchema.ConversationIndexTracking] = true;
				message.Save(SaveMode.ResolveConflicts);
			}
			catch (QuotaExceededException ex)
			{
				SentItemsProcessor.Tracer.TraceDebug<string>(0L, "Saving failed because quota exceeded. Exception: {0}", ex.Message);
			}
		}

		// Token: 0x04000119 RID: 281
		private const string MobileRoutingType = "MOBILE";

		// Token: 0x0400011A RID: 282
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;
	}
}
