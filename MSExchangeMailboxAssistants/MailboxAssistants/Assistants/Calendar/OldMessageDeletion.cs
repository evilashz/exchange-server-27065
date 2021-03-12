using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Calendar;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Calendar
{
	// Token: 0x020000B7 RID: 183
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OldMessageDeletion
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x00036880 File Offset: 0x00034A80
		public void PerformCleanUp(MailboxSession itemStore, StoreObject item, CalendarConfiguration mailboxConfig, CalendarItemBase originalCalItem, IEnumerable<VersionedId> duplicates)
		{
			bool flag = false;
			if (mailboxConfig.RemoveOldMeetingMessages && !(item is MeetingForwardNotification))
			{
				this.DeleteOldMessages(itemStore, (MeetingMessage)item, originalCalItem, out flag);
			}
			if (item is MeetingMessage)
			{
				bool flag2 = false;
				MeetingMessage meetingMessage = (MeetingMessage)item;
				if (meetingMessage.IsRepairUpdateMessage)
				{
					flag2 = (!flag || (!(meetingMessage is MeetingRequest) && !(meetingMessage is MeetingCancellation)));
					if (flag2)
					{
						meetingMessage.MarkAsOutOfDate();
						meetingMessage.IsRead = true;
						try
						{
							int num = 3;
							ConflictResolutionResult saveResults;
							do
							{
								saveResults = meetingMessage.Save(SaveMode.ResolveConflicts);
								meetingMessage.Load();
								num--;
							}
							while (num > 0 && !CalendarProcessing.CheckSaveResults(meetingMessage, saveResults, meetingMessage.InternetMessageId));
						}
						catch (ObjectNotFoundException arg)
						{
							flag2 = false;
							OldMessageDeletion.Tracer.TraceWarning<object, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: Tried to mark a RUM as Outdated, which does not exist anymore. Exception: {1}", TraceContext.Get(), arg);
						}
					}
				}
				if (!flag2 && item is MeetingForwardNotification)
				{
					bool flag3 = itemStore.IsGroupMailbox();
					string text = null;
					flag2 = (mailboxConfig.RemoveForwardedMeetingNotifications || !meetingMessage.IsResponseRequested || flag3);
					if (!flag2)
					{
						Participant participant = null;
						flag2 = OldMessageDeletion.IsSenderSame(meetingMessage.From, itemStore, out participant);
						if (participant != null)
						{
							text = participant.EmailAddress;
						}
					}
					string text2;
					if (flag2)
					{
						text2 = string.Format("Deleting MFN message as it was forwarded by the organizer. ParticipantFromEmail: {0}, Message From: {1},  From RoutingType: {2},  Mailbox owner : {3}, Mailboxconfig.RemoveMFN setting {4}, IsResponseRequested {5}, Subject: {6}, IsGroupMailbox: {7} ", new object[]
						{
							text ?? "unspecified",
							meetingMessage.From,
							(meetingMessage.From != null) ? meetingMessage.From.RoutingType : "unspecified",
							itemStore.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
							mailboxConfig.RemoveForwardedMeetingNotifications,
							meetingMessage.IsResponseRequested,
							meetingMessage.Subject,
							flag3
						});
					}
					else
					{
						text2 = string.Format("Not deleting MFN message as it was forwarded by someone other than the organizer. ParticipantFromEmail {0}, Message From: {1}, From RoutingType: {2},  Mailbox owner : {3}, Mailboxconfig.RemoveMFN setting {4}, IsResponseRequested {5}, Subject: {6} ", new object[]
						{
							text ?? "unspecified",
							meetingMessage.From,
							(meetingMessage.From != null) ? meetingMessage.From.RoutingType : "unspecified",
							itemStore.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
							mailboxConfig.RemoveForwardedMeetingNotifications,
							meetingMessage.IsResponseRequested,
							meetingMessage.Subject
						});
					}
					OldMessageDeletion.Tracer.TraceDebug((long)this.GetHashCode(), text2);
					MfnLog.LogEntry(itemStore, text2);
				}
				if (!flag2 && OldMessageDeletion.IsSelfForwardedEventAndAccepted(meetingMessage, itemStore, originalCalItem))
				{
					flag2 = true;
					OldMessageDeletion.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Marked the meeting request for deletion as sent to self", new object[]
					{
						TraceContext.Get()
					});
				}
				if (flag2)
				{
					this.DeleteMessage(itemStore, item);
				}
			}
			if (duplicates != null)
			{
				foreach (VersionedId versionedId in duplicates)
				{
					itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
					{
						versionedId
					});
					OldMessageDeletion.Tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: Deleted a duplicate item ID: {1}.", TraceContext.Get(), versionedId);
				}
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00036BD8 File Offset: 0x00034DD8
		internal void DeleteMessage(StoreSession itemStore, StoreObject item)
		{
			bool flag = false;
			string arg = null;
			if (item is MeetingForwardNotification)
			{
				flag = true;
				arg = ((MeetingMessage)item).Subject;
			}
			AggregateOperationResult aggregateOperationResult = itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
			{
				item.Id
			});
			if (aggregateOperationResult.OperationResult == OperationResult.Succeeded)
			{
				CalendarAssistantPerformanceCounters.MeetingMessagesDeleted.Increment();
				if (flag)
				{
					string info = string.Format("MFN with subject {0} was moved to deleted items.", arg);
					MfnLog.LogEntry((MailboxSession)itemStore, info);
					return;
				}
			}
			else if (flag)
			{
				string info = string.Format("MFN with subject {0} failed to be moved to deleted items. Error = {1}", arg, aggregateOperationResult);
				MfnLog.LogEntry((MailboxSession)itemStore, info);
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00036C68 File Offset: 0x00034E68
		private void DeleteOldMessages(MailboxSession itemStore, MeetingMessage item, CalendarItemBase originalCalItem, out bool fullUpdateDeleted)
		{
			fullUpdateDeleted = false;
			string text = (string)Utils.SafeGetProperty(item, ItemSchema.InternetMessageId, "<null>");
			if (item is MeetingRequest || item is MeetingCancellation)
			{
				bool flag = (bool)Utils.SafeGetProperty(item, MeetingMessageSchema.HijackedMeeting, false);
				if (flag)
				{
					CalendarAssistantLog.LogEntry(itemStore, "Message {0} has been hijacked, skipping deletion of old messages.", new object[]
					{
						text
					});
					return;
				}
			}
			int num = (int)Utils.SafeGetProperty(item, CalendarItemBaseSchema.AppointmentSequenceNumber, -1);
			if (num < 0)
			{
				OldMessageDeletion.Tracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: Message does not have a sequence number, skipping {1}", TraceContext.Get(), text);
				CalendarAssistantLog.LogEntry(itemStore, "Message does not have a sequence number, skipping {0}", new object[]
				{
					text
				});
				return;
			}
			if (OldMessageDeletion.ShouldIgnoreFolder(itemStore, item.ParentId))
			{
				return;
			}
			string text2 = (string)Utils.SafeGetProperty(item, MessageItemSchema.ReceivedRepresentingEmailAddress, string.Empty);
			string text3 = (string)Utils.SafeGetProperty(item, CalendarItemBaseSchema.OrganizerEmailAddress, string.Empty);
			byte[] array = (byte[])Utils.SafeGetProperty(item, CalendarItemBaseSchema.GlobalObjectId, null);
			if (array == null)
			{
				OldMessageDeletion.Tracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: Message {1} does not have a globalObjectId, skipping the message.", TraceContext.Get(), text);
				CalendarAssistantLog.LogEntry(itemStore, "Message {0} does not have a globalObjectId, skipping the message.", new object[]
				{
					text
				});
				return;
			}
			OldMessageDeletion.Tracer.TraceDebug<object, string, string>((long)item.GetHashCode(), "{0}: Received representing={1} sent representing={2}", TraceContext.Get(), text2, text3);
			CalendarAssistant.TracerPfd.TracePfd((long)item.GetHashCode(), "PFD IWC {0} {1}: Received representing={2} sent representing={3}", new object[]
			{
				22679,
				TraceContext.Get(),
				text2,
				text3
			});
			if (!itemStore.IsGroupMailbox() && string.Compare(text2, text3, true) == 0)
			{
				Participant participant = null;
				if (!OldMessageDeletion.IsSenderSame(item.Sender, itemStore, out participant))
				{
					return;
				}
				AggregateOperationResult aggregateOperationResult = itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
				{
					item.Id
				});
				OldMessageDeletion.Tracer.TraceDebug<object, string, OperationResult>((long)this.GetHashCode(), "{0}: Moving the item {1} to the Deleted Items folder returned:{2}", TraceContext.Get(), text, aggregateOperationResult.OperationResult);
				return;
			}
			else
			{
				if (item.IsRepairUpdateMessage)
				{
					RUMOldMessageDeletion.CleanUp(itemStore, item, originalCalItem, OldMessageDeletion.Tracer, out fullUpdateDeleted);
					return;
				}
				OldMessageDeletion.LatestItemInfo latestItemInfo;
				latestItemInfo.FullUpdateDeleted = fullUpdateDeleted;
				latestItemInfo.RollingHighlight = (int)Utils.SafeGetProperty(item, CalendarItemBaseSchema.ChangeHighlight, 0);
				latestItemInfo.LatestOldStartTime = ExDateTime.MinValue;
				latestItemInfo.LatestOldEndTime = ExDateTime.MinValue;
				latestItemInfo.LatestOldLocationStr = string.Empty;
				latestItemInfo.LatestSequenceNumber = -1;
				latestItemInfo.LatestItemId = item.Id;
				latestItemInfo.LatestClientSubmitTime = (ExDateTime)Utils.SafeGetProperty(item, ItemSchema.SentTime, ExDateTime.MinValue);
				this.QueryAndDeleteMatchingItems(itemStore, item, originalCalItem, array, text, text3, num, ref latestItemInfo);
				fullUpdateDeleted = latestItemInfo.FullUpdateDeleted;
				return;
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00036F2C File Offset: 0x0003512C
		internal static bool IsSenderSame(Participant sender, MailboxSession session, out Participant participant)
		{
			participant = null;
			if (sender == null || session == null)
			{
				return false;
			}
			bool result = false;
			participant = Participant.TryConvertTo(sender, "SMTP", session);
			if (participant != null)
			{
				result = string.Equals(session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), participant.EmailAddress, StringComparison.OrdinalIgnoreCase);
			}
			else if (string.Equals(sender.RoutingType, "EX", StringComparison.OrdinalIgnoreCase))
			{
				result = string.Equals(sender.EmailAddress, session.MailboxOwner.LegacyDn, StringComparison.OrdinalIgnoreCase);
			}
			return result;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00036FC0 File Offset: 0x000351C0
		internal static bool IsSelfForwardedEvent(MeetingMessage mtgMessage, MailboxSession session)
		{
			if (mtgMessage == null || mtgMessage.IsExternalMessage)
			{
				return false;
			}
			Participant participant = null;
			if (OldMessageDeletion.IsSenderSame(mtgMessage.Sender, session, out participant))
			{
				string text = (string)Utils.SafeGetProperty(mtgMessage, MessageItemSchema.ReceivedRepresentingEmailAddress, string.Empty);
				if (text.Equals(session.MailboxOwner.LegacyDn, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0003701C File Offset: 0x0003521C
		private static bool ShouldIgnoreFolder(MailboxSession itemStore, StoreObjectId folderId)
		{
			foreach (DefaultFolderType defaultFolderType in OldMessageDeletion.FoldersToIgnore)
			{
				StoreObjectId defaultFolderId = itemStore.GetDefaultFolderId(defaultFolderType);
				if (defaultFolderId != null && defaultFolderId.Equals(folderId))
				{
					return true;
				}
			}
			return CalendarAssistant.IsDumpsterFolder(itemStore, folderId);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00037067 File Offset: 0x00035267
		private static bool IsSelfForwardedEventAndAccepted(MeetingMessage mtgMessage, MailboxSession session, CalendarItemBase calendarItem)
		{
			return calendarItem != null && mtgMessage is MeetingRequest && (OldMessageDeletion.IsSelfForwardedEvent(mtgMessage, session) && calendarItem.ResponseType == ResponseType.Accept);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0003708C File Offset: 0x0003528C
		private void QueryAndDeleteMatchingItems(MailboxSession itemStore, MeetingMessage item, CalendarItemBase originalCalendarItem, byte[] itemGloblObjectId, string internetMessageId, string sentRepresenting, int itemSequenceNumber, ref OldMessageDeletion.LatestItemInfo latestInfo)
		{
			try
			{
				List<VersionedId> list = this.QueryMatchingItems(itemStore, item, itemGloblObjectId, sentRepresenting, itemSequenceNumber, ref latestInfo);
				if (item is MeetingRequest && itemSequenceNumber >= latestInfo.LatestSequenceNumber)
				{
					OldMessageDeletion.ApplyRollingHighlight((MeetingRequest)item, internetMessageId, originalCalendarItem, latestInfo);
				}
				if (list != null && list.Count != 0)
				{
					StoreId[] array = list.ToArray();
					OldMessageDeletion.Tracer.TraceDebug<object, ArrayTracer<StoreId>>((long)this.GetHashCode(), "{0}: Deleting message: {1}", TraceContext.Get(), new ArrayTracer<StoreId>(array));
					CalendarAssistant.TracerPfd.TracePfd<int, object, ArrayTracer<StoreId>>((long)this.GetHashCode(), "PFD IWC {0} {1}: Deleting message: {2}", 29847, TraceContext.Get(), new ArrayTracer<StoreId>(array));
					AggregateOperationResult aggregateOperationResult = itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, array);
					OldMessageDeletion.Tracer.TraceDebug<object, OperationResult>((long)this.GetHashCode(), "{0}: Deleting items returned: {1}", TraceContext.Get(), aggregateOperationResult.OperationResult);
					CalendarAssistantPerformanceCounters.MeetingMessagesDeleted.IncrementBy((long)list.Count);
				}
			}
			catch (ObjectNotFoundException arg)
			{
				OldMessageDeletion.Tracer.TraceDebug<object, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: Stopped OMD because we encountered an exception: {1}", TraceContext.Get(), arg);
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000371A0 File Offset: 0x000353A0
		private List<VersionedId> QueryMatchingItems(MailboxSession itemStore, MeetingMessage item, byte[] itemGlobalObjId, string sentRepresenting, int itemSequenceNumber, ref OldMessageDeletion.LatestItemInfo latestInfo)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			List<VersionedId> list = new List<VersionedId>();
			bool flag = true;
			bool flag2 = (bool)Utils.SafeGetProperty(item, MessageItemSchema.MapiHasAttachment, false);
			bool flag3 = item.Body != null && item.Body.Size > 0L;
			bool flag4 = item is MeetingRequest;
			bool flag5 = item is MeetingResponse;
			bool flag6 = item is MeetingCancellation;
			SortBy[] sortColumns = new SortBy[]
			{
				new SortBy(CalendarItemBaseSchema.GlobalObjectId, SortOrder.Ascending),
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Descending)
			};
			ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, CalendarItemBaseSchema.GlobalObjectId, itemGlobalObjId);
			using (Folder folder = Folder.Bind(itemStore, item.ParentId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, sortColumns, OldMessageDeletion.OMDColumnsToQuery))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
					while (flag)
					{
						object[][] rows = queryResult.GetRows(25);
						if (rows.Length < 1)
						{
							break;
						}
						ExDateTime itemOwnerChangeTime = (ExDateTime)Utils.SafeGetProperty(item, CalendarItemBaseSchema.OwnerCriticalChangeTime, utcNow);
						foreach (object[] array in rows)
						{
							string text = array[0] as string;
							if (!string.IsNullOrEmpty(text))
							{
								if (!itemStore.IsGroupMailbox())
								{
									if ((flag4 || flag6) && !ObjectClass.IsMeetingRequest(text) && !ObjectClass.IsMeetingCancellation(text))
									{
										flag = false;
										break;
									}
									if (flag5 && !ObjectClass.IsMeetingResponse(text))
									{
										goto IL_186;
									}
								}
								byte[] rowGlobalObjectId = (array[2] is byte[]) ? (array[2] as byte[]) : null;
								if (!OldMessageDeletion.GlobalObjectIdMatches(itemGlobalObjId, rowGlobalObjectId))
								{
									flag = false;
									break;
								}
								this.DetermineIfWeDelete(list, item, array, itemOwnerChangeTime, sentRepresenting, itemSequenceNumber, ref latestInfo, ref flag2, ref flag3);
							}
							IL_186:;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0003739C File Offset: 0x0003559C
		private void DetermineIfWeDelete(List<VersionedId> itemsToDelete, MeetingMessage item, object[] rowProperties, ExDateTime itemOwnerChangeTime, string sentRepresenting, int itemSequenceNumber, ref OldMessageDeletion.LatestItemInfo latestInfo, ref bool hasAttachments, ref bool hasBody)
		{
			bool flag = item is MeetingRequest;
			bool flag2 = item is MeetingResponse;
			object obj = rowProperties[0];
			MailboxSession itemStore = item.Session as MailboxSession;
			if (rowProperties[1] != null && !(rowProperties[1] is PropertyError))
			{
				VersionedId versionedId = (VersionedId)rowProperties[1];
				if (versionedId.Equals(latestInfo.LatestItemId))
				{
					return;
				}
				int num = 0;
				bool flag3 = false;
				if (!(rowProperties[3] is PropertyError) && rowProperties[3] is int)
				{
					num = (int)rowProperties[3];
					flag3 = true;
					if (latestInfo.LatestSequenceNumber < num)
					{
						if (rowProperties[9] is ExDateTime)
						{
							latestInfo.LatestOldStartTime = (ExDateTime)rowProperties[9];
							latestInfo.LatestSequenceNumber = num;
						}
						if (rowProperties[10] is ExDateTime)
						{
							latestInfo.LatestOldEndTime = (ExDateTime)rowProperties[10];
							latestInfo.LatestSequenceNumber = num;
						}
						if (rowProperties[11] is string && !string.IsNullOrEmpty((string)rowProperties[11]))
						{
							latestInfo.LatestOldLocationStr = (string)rowProperties[11];
							latestInfo.LatestSequenceNumber = num;
						}
					}
				}
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				if (flag3)
				{
					flag4 = (num == itemSequenceNumber);
					flag5 = (num < itemSequenceNumber);
				}
				else if (!(rowProperties[6] is PropertyError) && rowProperties[6] is ExDateTime && latestInfo.LatestClientSubmitTime >= (ExDateTime)rowProperties[6])
				{
					flag6 = true;
				}
				if (flag5 || flag6)
				{
					if (!flag2)
					{
						if (flag && !(rowProperties[4] is PropertyError) && rowProperties[4] is int)
						{
							latestInfo.RollingHighlight |= (int)rowProperties[4];
						}
						object obj2 = rowProperties[13];
						if (obj2 is int)
						{
							MeetingMessageType meetingMessageType = (MeetingMessageType)obj2;
							latestInfo.FullUpdateDeleted |= (meetingMessageType == MeetingMessageType.FullUpdate || meetingMessageType == MeetingMessageType.NewMeetingRequest);
						}
						itemsToDelete.Add(versionedId);
						return;
					}
					if (!this.SentRepresentingMatches(sentRepresenting, rowProperties[5]))
					{
						return;
					}
					itemsToDelete.Add(versionedId);
					return;
				}
				else if (flag4)
				{
					if (flag2)
					{
						this.HandleResponsesWithSameSequenceNumber(itemStore, item, versionedId, rowProperties, sentRepresenting, ref latestInfo, itemsToDelete, ref hasAttachments, ref hasBody);
						return;
					}
					if (flag && !(rowProperties[4] is PropertyError) && rowProperties[4] is int)
					{
						latestInfo.RollingHighlight |= (int)rowProperties[4];
					}
					if (!(rowProperties[8] is PropertyError) && rowProperties[8] is ExDateTime)
					{
						ExDateTime t = (ExDateTime)rowProperties[8];
						if (t < itemOwnerChangeTime)
						{
							if (!(rowProperties[4] is PropertyError) && rowProperties[4] is int)
							{
								latestInfo.RollingHighlight |= (int)rowProperties[4];
							}
							object obj3 = rowProperties[13];
							if (obj3 is int)
							{
								MeetingMessageType meetingMessageType2 = (MeetingMessageType)obj3;
								latestInfo.FullUpdateDeleted |= (meetingMessageType2 == MeetingMessageType.FullUpdate || meetingMessageType2 == MeetingMessageType.NewMeetingRequest);
							}
							itemsToDelete.Add(versionedId);
						}
					}
				}
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0003766C File Offset: 0x0003586C
		internal static void ApplyRollingHighlight(MeetingRequest meetingRequest, string internetMessageId, CalendarItemBase originalCalItem, OldMessageDeletion.LatestItemInfo latestInfo)
		{
			int i = 3;
			MeetingRequest meetingRequest2 = meetingRequest;
			CalendarAssistant.TracerPfd.TracePfd<int, object, string>(0L, "PFD IWC {0} {1} Applying the rolling highlight to the item{2}", 18583, TraceContext.Get(), internetMessageId);
			if (meetingRequest2 != null)
			{
				int hashCode = meetingRequest.GetHashCode();
				PropertyDefinition changeHighlight = CalendarItemBaseSchema.ChangeHighlight;
				while (i > 0)
				{
					try
					{
						if (meetingRequest2 == null)
						{
							meetingRequest2 = MeetingRequest.Bind(meetingRequest.Session, meetingRequest.Id);
							meetingRequest2.OpenAsReadWrite();
						}
						if (latestInfo.RollingHighlight != 0)
						{
							int num = (int)Utils.SafeGetProperty(meetingRequest2, changeHighlight, 0);
							if ((num & 8) == 0 && (latestInfo.RollingHighlight & 8) != 0)
							{
								if (!string.IsNullOrEmpty(latestInfo.LatestOldLocationStr))
								{
									meetingRequest2[CalendarItemBaseSchema.OldLocation] = latestInfo.LatestOldLocationStr;
								}
								else
								{
									latestInfo.RollingHighlight &= -9;
								}
							}
							if ((num & 3) == 0 && (latestInfo.RollingHighlight & 3) != 0)
							{
								if (latestInfo.LatestOldStartTime != ExDateTime.MinValue && latestInfo.LatestOldEndTime != ExDateTime.MinValue)
								{
									meetingRequest2[MeetingRequestSchema.OldStartWhole] = latestInfo.LatestOldStartTime;
									meetingRequest2[MeetingRequestSchema.OldEndWhole] = latestInfo.LatestOldEndTime;
								}
								else
								{
									latestInfo.RollingHighlight &= -4;
								}
							}
							num |= latestInfo.RollingHighlight;
							meetingRequest2[changeHighlight] = num;
							if (meetingRequest2.MeetingRequestType != MeetingMessageType.PrincipalWantsCopy)
							{
								ChangeHighlightHelper changeHighlightHelper = new ChangeHighlightHelper(num);
								MeetingMessageType suggestedMeetingType = changeHighlightHelper.SuggestedMeetingType;
								meetingRequest2.MeetingRequestType = suggestedMeetingType;
							}
						}
						if (originalCalItem != null && meetingRequest2.MeetingRequestType != MeetingMessageType.NewMeetingRequest && meetingRequest2.MeetingRequestType != MeetingMessageType.PrincipalWantsCopy)
						{
							ResponseType responseType = originalCalItem.ResponseType;
							if (responseType == ResponseType.NotResponded || responseType == ResponseType.None)
							{
								meetingRequest2.MeetingRequestType = MeetingMessageType.FullUpdate;
								meetingRequest2[ItemSchema.IconIndex] = CalendarItemBase.CalculateMeetingRequestIcon(meetingRequest2);
							}
						}
						if (!meetingRequest2.IsDirty)
						{
							break;
						}
						ConflictResolutionResult saveResults = meetingRequest2.Save(SaveMode.ResolveConflicts);
						meetingRequest2.Load();
						if (CalendarProcessing.CheckSaveResults(meetingRequest2, saveResults, internetMessageId))
						{
							break;
						}
						i--;
						if (meetingRequest2 != meetingRequest && meetingRequest2 != null)
						{
							meetingRequest2.Dispose();
						}
						meetingRequest2 = null;
					}
					catch (ObjectExistedException ex)
					{
						MailboxSession session = meetingRequest.Session as MailboxSession;
						OldMessageDeletion.Tracer.TraceError((long)hashCode, "{0}: Exception thrown when rolling forward the change highlight on item: {1}, attempt: {2}, exception = {3}", new object[]
						{
							TraceContext.Get(),
							internetMessageId,
							4 - i,
							ex
						});
						CalendarAssistantLog.LogEntry(session, ex, false, "Exception thrown when rolling forward the change highlight on item: {0}, attempt: {1}", new object[]
						{
							internetMessageId,
							4 - i
						});
						i--;
						if (meetingRequest2 != meetingRequest && meetingRequest2 != null)
						{
							meetingRequest2.Dispose();
							meetingRequest2 = null;
						}
					}
					catch (SaveConflictException ex2)
					{
						MailboxSession session2 = meetingRequest.Session as MailboxSession;
						OldMessageDeletion.Tracer.TraceError((long)hashCode, "{0}: Exception thrown when rolling forward the change highlight on item: {1}, attempt: {2}, exception = {3}", new object[]
						{
							TraceContext.Get(),
							internetMessageId,
							4 - i,
							ex2
						});
						CalendarAssistantLog.LogEntry(session2, ex2, false, "Exception thrown when rolling forward the change highlight on item: {0}, attempt: {1}", new object[]
						{
							internetMessageId,
							4 - i
						});
						i--;
						if (meetingRequest2 != meetingRequest && meetingRequest2 != null)
						{
							meetingRequest2.Dispose();
							meetingRequest2 = null;
						}
					}
					catch (ObjectNotFoundException ex3)
					{
						MailboxSession session3 = meetingRequest.Session as MailboxSession;
						OldMessageDeletion.Tracer.TraceError((long)hashCode, "{0}: Exception thrown when rolling forward the change highlight on item: {1},  attempt: {2}, exception = {3}", new object[]
						{
							TraceContext.Get(),
							internetMessageId,
							4 - i,
							ex3
						});
						CalendarAssistantLog.LogEntry(session3, ex3, false, "Exception thrown when rolling forward the change highlight on item: {0}, attempt: {1}", new object[]
						{
							internetMessageId,
							4 - i
						});
						break;
					}
					finally
					{
						if (meetingRequest2 != meetingRequest && meetingRequest2 != null)
						{
							meetingRequest2.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00037A84 File Offset: 0x00035C84
		private void HandleResponsesWithSameSequenceNumber(MailboxSession itemStore, MeetingMessage item, VersionedId iid, object[] rowProperties, string sentRepresenting, ref OldMessageDeletion.LatestItemInfo latestInfo, List<VersionedId> itemsToDelete, ref bool hasAttachments, ref bool hasBody)
		{
			if (!this.SentRepresentingMatches(sentRepresenting, rowProperties[5]))
			{
				return;
			}
			if (rowProperties[6] is PropertyError || !(rowProperties[6] is ExDateTime))
			{
				return;
			}
			if (rowProperties[7] is PropertyError || !(rowProperties[7] is bool) || (bool)rowProperties[7])
			{
				return;
			}
			try
			{
				using (MeetingResponse meetingResponse = MeetingResponse.Bind(item.Session, iid))
				{
					if (meetingResponse.Body.Size > 0L)
					{
						return;
					}
				}
			}
			catch (VirusDetectedException e)
			{
				OldMessageDeletion.Tracer.TraceError<object, VersionedId>((long)this.GetHashCode(), "{0}: A virus was detected in the CalendarItem associated with this message {1}. This message will be skipped.", TraceContext.Get(), iid);
				CalendarAssistantLog.LogEntry(itemStore, e, false, "A virus was detected in the CalendarItem associated with this message {0}. This message will be skipped.", new object[]
				{
					iid
				});
				return;
			}
			catch (VirusMessageDeletedException e2)
			{
				OldMessageDeletion.Tracer.TraceError<object, VersionedId>((long)this.GetHashCode(), "{0}: A virus was detected in the CalendarItem associated with this message and was deleted {1}. This message will be skipped.", TraceContext.Get(), iid);
				CalendarAssistantLog.LogEntry(itemStore, e2, false, "A virus was detected in the CalendarItem associated with this message and was deleted {0}. This message will be skipped.", new object[]
				{
					iid
				});
				return;
			}
			catch (StoragePermanentException ex)
			{
				OldMessageDeletion.Tracer.TraceError<object, StoragePermanentException>((long)this.GetHashCode(), "{0}: Exception caught when opening response and getting the body: {1}", TraceContext.Get(), ex);
				CalendarAssistantLog.LogEntry(itemStore, ex, false, "Exception caught when opening response and getting the body for message id {0}", new object[]
				{
					iid
				});
				return;
			}
			catch (StorageTransientException ex2)
			{
				OldMessageDeletion.Tracer.TraceError<object, StorageTransientException>((long)this.GetHashCode(), "{0}: Exception caught when opening response and getting the body: {1}", TraceContext.Get(), ex2);
				CalendarAssistantLog.LogEntry(itemStore, ex2, false, "Exception caught when opening response and getting the body for message id {0}", new object[]
				{
					iid
				});
				return;
			}
			if (latestInfo.LatestClientSubmitTime < (ExDateTime)rowProperties[6])
			{
				if (!hasAttachments && !hasBody)
				{
					itemsToDelete.Add(latestInfo.LatestItemId);
					latestInfo.LatestItemId = iid;
					latestInfo.LatestClientSubmitTime = (ExDateTime)rowProperties[6];
					hasAttachments = false;
					hasBody = false;
					return;
				}
			}
			else
			{
				itemsToDelete.Add(iid);
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00037C9C File Offset: 0x00035E9C
		internal static bool GlobalObjectIdMatches(byte[] itemGlobalObjId, byte[] rowGlobalObjectId)
		{
			bool result = true;
			if (rowGlobalObjectId != null)
			{
				if (rowGlobalObjectId.Length != itemGlobalObjId.Length)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < itemGlobalObjId.Length; i++)
					{
						if (rowGlobalObjectId[i] != itemGlobalObjId[i])
						{
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00037CD8 File Offset: 0x00035ED8
		private bool SentRepresentingMatches(string sentRepresenting, object rowProperty)
		{
			bool result = true;
			if (rowProperty is PropertyError || !(rowProperty is string))
			{
				result = false;
			}
			else if (string.Compare(sentRepresenting, (string)rowProperty, true) != 0)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000593 RID: 1427
		internal const int QueryBatchSize = 25;

		// Token: 0x04000594 RID: 1428
		private static DefaultFolderType[] FoldersToIgnore = new DefaultFolderType[]
		{
			DefaultFolderType.DeletedItems,
			DefaultFolderType.JunkEmail
		};

		// Token: 0x04000595 RID: 1429
		internal static readonly PropertyDefinition[] OMDColumnsToQuery = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			ItemSchema.Id,
			CalendarItemBaseSchema.GlobalObjectId,
			CalendarItemBaseSchema.AppointmentSequenceNumber,
			CalendarItemBaseSchema.ChangeHighlight,
			CalendarItemBaseSchema.OrganizerEmailAddress,
			ItemSchema.SentTime,
			MessageItemSchema.MapiHasAttachment,
			CalendarItemBaseSchema.OwnerCriticalChangeTime,
			MeetingRequestSchema.OldStartWhole,
			MeetingRequestSchema.OldEndWhole,
			CalendarItemBaseSchema.OldLocation,
			MeetingMessageSchema.CalendarProcessed,
			CalendarItemBaseSchema.MeetingRequestType
		};

		// Token: 0x04000596 RID: 1430
		private static readonly Trace Tracer = ExTraceGlobals.OldMessageDeletionTracer;

		// Token: 0x020000B8 RID: 184
		internal enum OMDColumns
		{
			// Token: 0x04000598 RID: 1432
			MessageClass,
			// Token: 0x04000599 RID: 1433
			Id,
			// Token: 0x0400059A RID: 1434
			GlobalObjectId,
			// Token: 0x0400059B RID: 1435
			ApptSeqNum,
			// Token: 0x0400059C RID: 1436
			ChangeHighlight,
			// Token: 0x0400059D RID: 1437
			OrganizerEmailAddr,
			// Token: 0x0400059E RID: 1438
			ClientSubmitTime,
			// Token: 0x0400059F RID: 1439
			MapiHasAttach,
			// Token: 0x040005A0 RID: 1440
			OwnerCriticalChgTime,
			// Token: 0x040005A1 RID: 1441
			OldStart,
			// Token: 0x040005A2 RID: 1442
			OldEnd,
			// Token: 0x040005A3 RID: 1443
			OldLocation,
			// Token: 0x040005A4 RID: 1444
			CalendarProcessed,
			// Token: 0x040005A5 RID: 1445
			MeetingRequestType
		}

		// Token: 0x020000B9 RID: 185
		internal struct LatestItemInfo
		{
			// Token: 0x040005A6 RID: 1446
			public bool FullUpdateDeleted;

			// Token: 0x040005A7 RID: 1447
			public int RollingHighlight;

			// Token: 0x040005A8 RID: 1448
			public VersionedId LatestItemId;

			// Token: 0x040005A9 RID: 1449
			public int LatestSequenceNumber;

			// Token: 0x040005AA RID: 1450
			public string LatestOldLocationStr;

			// Token: 0x040005AB RID: 1451
			public ExDateTime LatestOldStartTime;

			// Token: 0x040005AC RID: 1452
			public ExDateTime LatestOldEndTime;

			// Token: 0x040005AD RID: 1453
			public ExDateTime LatestClientSubmitTime;
		}
	}
}
