using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Calendar
{
	// Token: 0x020000BA RID: 186
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RUMOldMessageDeletion
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x00037DC8 File Offset: 0x00035FC8
		public static void CleanUp(MailboxSession mailboxSession, MeetingMessage item, CalendarItemBase originalCalItem, Trace tracer, out bool fullUpdateDeleted)
		{
			fullUpdateDeleted = false;
			byte[] valueOrDefault = item.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.GlobalObjectId, null);
			if (valueOrDefault == null)
			{
				return;
			}
			int valueOrDefault2 = item.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentSequenceNumber, -1);
			if (valueOrDefault2 == -1)
			{
				return;
			}
			OldMessageDeletion.LatestItemInfo latestInfo;
			latestInfo.LatestSequenceNumber = -1;
			List<VersionedId> oldMeetingMessages = RUMOldMessageDeletion.GetOldMeetingMessages(mailboxSession, item, valueOrDefault, valueOrDefault2, out latestInfo);
			if (oldMeetingMessages == null && latestInfo.LatestSequenceNumber >= valueOrDefault2)
			{
				RUMOldMessageDeletion.MarkMeetingMessageAsOld(item, tracer);
				fullUpdateDeleted = false;
				return;
			}
			if (oldMeetingMessages != null && oldMeetingMessages.Count > 0)
			{
				foreach (VersionedId storeId in oldMeetingMessages)
				{
					using (MeetingMessage meetingMessage = MeetingMessage.Bind(mailboxSession, storeId))
					{
						RUMOldMessageDeletion.MarkMeetingMessageAsOld(meetingMessage, tracer);
					}
				}
			}
			MeetingRequest meetingRequest = item as MeetingRequest;
			if (meetingRequest != null)
			{
				OldMessageDeletion.ApplyRollingHighlight(meetingRequest, item.InternetMessageId, originalCalItem, latestInfo);
			}
			fullUpdateDeleted = latestInfo.FullUpdateDeleted;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00037EC4 File Offset: 0x000360C4
		private static List<VersionedId> GetOldMeetingMessages(MailboxSession mailboxSession, MeetingMessage item, byte[] globalObjectId, int rumSequenceNumber, out OldMessageDeletion.LatestItemInfo latestItemInfo)
		{
			SortBy[] array = new SortBy[2];
			latestItemInfo.FullUpdateDeleted = false;
			latestItemInfo.RollingHighlight = (int)Utils.SafeGetProperty(item, CalendarItemBaseSchema.ChangeHighlight, 0);
			latestItemInfo.LatestOldStartTime = ExDateTime.MinValue;
			latestItemInfo.LatestOldEndTime = ExDateTime.MinValue;
			latestItemInfo.LatestOldLocationStr = string.Empty;
			latestItemInfo.LatestSequenceNumber = -1;
			latestItemInfo.LatestItemId = item.Id;
			latestItemInfo.LatestClientSubmitTime = (ExDateTime)Utils.SafeGetProperty(item, ItemSchema.SentTime, ExDateTime.MinValue);
			ExDateTime valueOrDefault = item.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.OwnerCriticalChangeTime, ExDateTime.MinValue);
			VersionedId id = item.Id;
			array[0] = new SortBy(CalendarItemBaseSchema.GlobalObjectId, SortOrder.Ascending);
			array[1] = new SortBy(CalendarItemBaseSchema.AppointmentSequenceNumber, SortOrder.Descending);
			ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, CalendarItemBaseSchema.GlobalObjectId, globalObjectId);
			List<VersionedId> list = new List<VersionedId>();
			using (Folder folder = Folder.Bind(mailboxSession, item.ParentId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, array, OldMessageDeletion.OMDColumnsToQuery))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
					bool flag = true;
					while (flag)
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(25);
						if (propertyBags.Length <= 1)
						{
							break;
						}
						foreach (IStorePropertyBag storePropertyBag in propertyBags)
						{
							string valueOrDefault2 = storePropertyBag.GetValueOrDefault<string>(OldMessageDeletion.OMDColumnsToQuery[0], null);
							if (!string.IsNullOrEmpty(valueOrDefault2) && (ObjectClass.IsMeetingRequest(valueOrDefault2) || ObjectClass.IsMeetingCancellation(valueOrDefault2)))
							{
								byte[] valueOrDefault3 = storePropertyBag.GetValueOrDefault<byte[]>(OldMessageDeletion.OMDColumnsToQuery[2], null);
								if (!OldMessageDeletion.GlobalObjectIdMatches(globalObjectId, valueOrDefault3))
								{
									flag = false;
									break;
								}
								VersionedId valueOrDefault4 = storePropertyBag.GetValueOrDefault<VersionedId>(OldMessageDeletion.OMDColumnsToQuery[1], null);
								if (valueOrDefault4 != null && !id.Equals(valueOrDefault4))
								{
									int valueOrDefault5 = storePropertyBag.GetValueOrDefault<int>(OldMessageDeletion.OMDColumnsToQuery[3], -1);
									if (valueOrDefault5 != -1)
									{
										if (valueOrDefault5 > rumSequenceNumber)
										{
											latestItemInfo.LatestSequenceNumber = valueOrDefault5;
											return null;
										}
										if (valueOrDefault5 == rumSequenceNumber)
										{
											ExDateTime valueOrDefault6 = storePropertyBag.GetValueOrDefault<ExDateTime>(OldMessageDeletion.OMDColumnsToQuery[8], ExDateTime.MinValue);
											if (valueOrDefault6 > valueOrDefault)
											{
												latestItemInfo.LatestSequenceNumber = valueOrDefault5;
												return null;
											}
										}
										if (latestItemInfo.LatestSequenceNumber == -1)
										{
											ExDateTime valueOrDefault7 = storePropertyBag.GetValueOrDefault<ExDateTime>(OldMessageDeletion.OMDColumnsToQuery[9], ExDateTime.MinValue);
											if (valueOrDefault7 != ExDateTime.MinValue)
											{
												latestItemInfo.LatestOldStartTime = valueOrDefault7;
											}
											ExDateTime valueOrDefault8 = storePropertyBag.GetValueOrDefault<ExDateTime>(OldMessageDeletion.OMDColumnsToQuery[10], ExDateTime.MinValue);
											if (valueOrDefault7 != ExDateTime.MinValue)
											{
												latestItemInfo.LatestOldEndTime = valueOrDefault8;
											}
											string valueOrDefault9 = storePropertyBag.GetValueOrDefault<string>(OldMessageDeletion.OMDColumnsToQuery[11], null);
											if (!string.IsNullOrEmpty(valueOrDefault9))
											{
												latestItemInfo.LatestOldLocationStr = valueOrDefault9;
											}
										}
										latestItemInfo.RollingHighlight |= storePropertyBag.GetValueOrDefault<int>(OldMessageDeletion.OMDColumnsToQuery[4], 0);
										MeetingMessageType valueOrDefault10 = storePropertyBag.GetValueOrDefault<MeetingMessageType>(OldMessageDeletion.OMDColumnsToQuery[13], MeetingMessageType.None);
										latestItemInfo.FullUpdateDeleted |= (valueOrDefault10 == MeetingMessageType.FullUpdate || valueOrDefault10 == MeetingMessageType.NewMeetingRequest);
										list.Add(valueOrDefault4);
									}
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00038224 File Offset: 0x00036424
		private static void MarkMeetingMessageAsOld(MeetingMessage meetingMessage, Trace tracer)
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
				tracer.TraceWarning<object, ObjectNotFoundException>(0L, "{0}: RUMOldMessageDeletion: Tried to mark a meeting message as Outdated, which does not exist anymore. Exception: {1}", TraceContext.Get(), arg);
			}
		}
	}
}
