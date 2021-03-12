using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D9 RID: 217
	internal sealed class CalendarChangeProcessor
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0003D618 File Offset: 0x0003B818
		public CalendarChangeProcessor(DatabaseInfo databaseInfo)
		{
			this.DatabaseInfo = databaseInfo;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0003D627 File Offset: 0x0003B827
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0003D62F File Offset: 0x0003B82F
		private DatabaseInfo DatabaseInfo { get; set; }

		// Token: 0x0600091C RID: 2332 RVA: 0x0003D638 File Offset: 0x0003B838
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (ObjectType.MAPI_MESSAGE != mapiEvent.ItemType)
			{
				return false;
			}
			bool flag = ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(mapiEvent.ObjectClass);
			if (!flag && ((MapiEventTypeFlags.NewMail & mapiEvent.EventMask) == (MapiEventTypeFlags)0 || (!ObjectClass.IsMeetingRequest(mapiEvent.ObjectClass) && !ObjectClass.IsMeetingCancellation(mapiEvent.ObjectClass))))
			{
				return false;
			}
			MailboxData fromCache = MailboxData.GetFromCache(mapiEvent.MailboxGuid);
			if (fromCache == null)
			{
				return false;
			}
			StoreObjectId storeObjectId = null;
			using (fromCache.CreateReadLock())
			{
				if (!NotificationFactories.Instance.IsInterestedInCalendarChangeEvent(fromCache.Settings))
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)this.GetHashCode(), "Calendar change event is uninteresting to {0}", fromCache.Settings.LegacyDN);
					return false;
				}
				storeObjectId = fromCache.DefaultCalendarFolderId;
				if (storeObjectId == null)
				{
					return false;
				}
			}
			if (flag)
			{
				if (((MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectDeleted | MapiEventTypeFlags.ObjectModified | MapiEventTypeFlags.ObjectCopied) & mapiEvent.EventMask) != (MapiEventTypeFlags)0 && !object.Equals(storeObjectId, StoreObjectId.FromProviderSpecificId(mapiEvent.ParentEntryId)))
				{
					return false;
				}
				if ((MapiEventTypeFlags.ObjectMoved & mapiEvent.EventMask) != (MapiEventTypeFlags)0 && !object.Equals(storeObjectId, StoreObjectId.FromProviderSpecificId(mapiEvent.ParentEntryId)) && !object.Equals(storeObjectId, StoreObjectId.FromProviderSpecificId(mapiEvent.OldParentEntryId)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0003D75C File Offset: 0x0003B95C
		private static CalendarNotificationType AnalyzeEvent(MapiEvent mapiEvent, StoreObjectId defaultCalFldrId, AppointmentStateFlags flags, ChangeHighlightProperties changeHilite, ResponseType responseType)
		{
			CalendarNotificationType result = CalendarNotificationType.Uninteresting;
			if ((AppointmentStateFlags.Cancelled & flags) != AppointmentStateFlags.None || (MapiEventTypeFlags.ObjectDeleted & mapiEvent.EventMask) != (MapiEventTypeFlags)0)
			{
				result = CalendarNotificationType.DeletedUpdate;
			}
			else if (((MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectCopied) & mapiEvent.EventMask) != (MapiEventTypeFlags)0)
			{
				result = CalendarNotificationType.NewUpdate;
			}
			else if ((MapiEventTypeFlags.ObjectModified & mapiEvent.EventMask) != (MapiEventTypeFlags)0 && (CalendarChangeProcessor.InterestingFlagsExists(changeHilite, mapiEvent.ExtendedEventFlags) || ResponseType.Organizer == responseType))
			{
				result = CalendarNotificationType.ChangedUpdate;
			}
			else if ((MapiEventTypeFlags.ObjectMoved & mapiEvent.EventMask) != (MapiEventTypeFlags)0)
			{
				if (object.Equals(defaultCalFldrId, StoreObjectId.FromProviderSpecificId(mapiEvent.ParentEntryId)))
				{
					result = CalendarNotificationType.NewUpdate;
				}
				else if (object.Equals(defaultCalFldrId, StoreObjectId.FromProviderSpecificId(mapiEvent.OldParentEntryId)))
				{
					result = CalendarNotificationType.DeletedUpdate;
				}
			}
			return result;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0003D7E8 File Offset: 0x0003B9E8
		private static CalendarNotificationType AnalyzeEvent(MailboxData mailboxData, MapiEvent mapiEvent, StoreObject item)
		{
			StoreObjectId defaultCalendarFolderId = mailboxData.DefaultCalendarFolderId;
			if (item == null)
			{
				return CalendarNotificationType.DeletedUpdate;
			}
			AppointmentStateFlags valueOrDefault = item.GetValueOrDefault<AppointmentStateFlags>(CalendarItemBaseSchema.AppointmentState);
			ChangeHighlightProperties changeHighlightProperties = item.GetValueOrDefault<ChangeHighlightProperties>(CalendarItemBaseSchema.ChangeHighlight);
			CalendarItemType valueOrDefault2 = item.GetValueOrDefault<CalendarItemType>(CalendarItemBaseSchema.CalendarItemType, CalendarItemType.Single);
			ResponseType valueOrDefault3 = item.GetValueOrDefault<ResponseType>(CalendarItemBaseSchema.ResponseType, ResponseType.None);
			if (changeHighlightProperties == ChangeHighlightProperties.None && valueOrDefault2 != CalendarItemType.Single)
			{
				changeHighlightProperties = (ChangeHighlightProperties)(-1);
			}
			return CalendarChangeProcessor.AnalyzeEvent(mapiEvent, defaultCalendarFolderId, valueOrDefault, changeHighlightProperties, valueOrDefault3);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0003D847 File Offset: 0x0003BA47
		private static bool InterestingFlagsExists(ChangeHighlightProperties changeHilite, MapiExtendedEventFlags mapiExtEvtFlags)
		{
			return ((ChangeHighlightProperties.MapiStartTime | ChangeHighlightProperties.MapiEndTime | ChangeHighlightProperties.RecurrenceProps | ChangeHighlightProperties.Location | ChangeHighlightProperties.Subject) & changeHilite) != ChangeHighlightProperties.None || MapiExtendedEventFlags.None == (MapiExtendedEventFlags.NoReminderPropertyModified & mapiExtEvtFlags);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0003D85C File Offset: 0x0003BA5C
		private static CalendarNotificationType AnalyzeEvent(MapiEvent mapiEvent, MeetingMessage meeting)
		{
			CalendarNotificationType result = CalendarNotificationType.Uninteresting;
			if (meeting is MeetingRequest)
			{
				MeetingRequest meetingRequest = (MeetingRequest)meeting;
				MeetingMessageType meetingRequestType = meetingRequest.MeetingRequestType;
				if (meetingRequestType <= MeetingMessageType.FullUpdate)
				{
					if (meetingRequestType != MeetingMessageType.NewMeetingRequest)
					{
						if (meetingRequestType != MeetingMessageType.FullUpdate)
						{
							return result;
						}
						goto IL_4A;
					}
				}
				else
				{
					if (meetingRequestType == MeetingMessageType.InformationalUpdate || meetingRequestType == MeetingMessageType.SilentUpdate)
					{
						goto IL_4A;
					}
					if (meetingRequestType != MeetingMessageType.PrincipalWantsCopy)
					{
						return result;
					}
				}
				return CalendarNotificationType.NewUpdate;
				IL_4A:
				if (CalendarChangeProcessor.InterestingFlagsExists(meetingRequest.ChangeHighlight, MapiExtendedEventFlags.NoReminderPropertyModified))
				{
					result = CalendarNotificationType.ChangedUpdate;
				}
			}
			else if (meeting is MeetingCancellation)
			{
				result = CalendarNotificationType.DeletedUpdate;
			}
			return result;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0003D8D4 File Offset: 0x0003BAD4
		private static bool TryGetMatched(IList<CalendarInfo> calEvents, StoreObjectId calItemId, StoreObjectId calItemOccId, bool returnExactlyMatchedOnly, out IList<CalendarInfo> eventsOnlyItemIdMatched, out CalendarInfo eventExactlyMatched)
		{
			eventsOnlyItemIdMatched = null;
			eventExactlyMatched = null;
			bool result = false;
			foreach (CalendarInfo calendarInfo in calEvents)
			{
				if (object.Equals(calItemId, calendarInfo.CalendarItemIdentity))
				{
					if (object.Equals(calItemOccId, calendarInfo.CalendarItemOccurrenceIdentity))
					{
						eventExactlyMatched = calendarInfo;
						if (returnExactlyMatchedOnly)
						{
							break;
						}
					}
					else if (returnExactlyMatchedOnly)
					{
						result = true;
					}
					else
					{
						if (eventsOnlyItemIdMatched == null)
						{
							eventsOnlyItemIdMatched = new List<CalendarInfo>();
						}
						eventsOnlyItemIdMatched.Add(calendarInfo);
					}
				}
			}
			if (!returnExactlyMatchedOnly)
			{
				return result;
			}
			return null != eventExactlyMatched;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0003D9C8 File Offset: 0x0003BBC8
		private static void UpdateReminderAccordingToDeletedUpdate(MailboxData mailboxData, StoreObjectId calItemId)
		{
			mailboxData.Actions.ForEach(delegate(CalendarNotificationAction action)
			{
				if (typeof(ReminderEmitting) != action.GetType())
				{
					return;
				}
				CalendarInfo calendarInfo = ((ReminderEmitting)action).CalendarInfo;
				if (!object.Equals(calItemId, calendarInfo.CalendarItemIdentity))
				{
					return;
				}
				CalendarNotificationInitiator.CompleteAction(action, "UpdateReminderAccordingToDeletedUpdate");
			});
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0003DA68 File Offset: 0x0003BC68
		private static void UpdateReminderAccordingToDeletedUpdate(MailboxData mailboxData, StoreObjectId calItemId, StoreObjectId calItemOccId)
		{
			mailboxData.Actions.ForEach(delegate(CalendarNotificationAction action)
			{
				if (typeof(ReminderEmitting) != action.GetType())
				{
					return;
				}
				CalendarInfo calendarInfo = ((ReminderEmitting)action).CalendarInfo;
				if (!object.Equals(calItemId, calendarInfo.CalendarItemIdentity) || !object.Equals(calItemOccId, calendarInfo.CalendarItemOccurrenceIdentity))
				{
					return;
				}
				CalendarNotificationInitiator.CompleteAction(action, "UpdateReminderAccordingToDeletedUpdate");
			});
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0003DBB0 File Offset: 0x0003BDB0
		private static void UpdateReminderAccordingToChangedUpdate(MailboxData mailboxData, IList<CalendarInfo> calEvents, IList<CalendarInfo> eventsToBeRemoved)
		{
			List<CalendarInfo> eventsToBeAdded = new List<CalendarInfo>(calEvents);
			ExDateTime nextReminderReloadTime = ExDateTime.MinValue;
			mailboxData.Actions.ForEach(delegate(CalendarNotificationAction action)
			{
				if (typeof(ReminderReloading) == action.GetType())
				{
					nextReminderReloadTime = action.ExpectedTime;
					return;
				}
				if (typeof(ReminderEmitting) != action.GetType())
				{
					return;
				}
				CalendarInfo calendarInfo2 = ((ReminderEmitting)action).CalendarInfo;
				IList<CalendarInfo> list = null;
				CalendarInfo calendarInfo3 = null;
				if (CalendarChangeProcessor.TryGetMatched(eventsToBeRemoved, calendarInfo2.CalendarItemIdentity, calendarInfo2.CalendarItemOccurrenceIdentity, true, out list, out calendarInfo3) && calendarInfo3 != null)
				{
					CalendarNotificationInitiator.CompleteAction(action, "UpdateReminderAccordingToChangedUpdate");
				}
				list = null;
				calendarInfo3 = null;
				if (CalendarChangeProcessor.TryGetMatched(eventsToBeAdded, calendarInfo2.CalendarItemIdentity, calendarInfo2.CalendarItemOccurrenceIdentity, true, out list, out calendarInfo3))
				{
					eventsToBeAdded.Remove(calendarInfo3);
					ReminderEmitting action3;
					if (!calendarInfo3.ReminderIsSet || !calendarInfo3.IsInteresting(CalendarNotificationType.ChangedUpdate) || calendarInfo3.ReminderTime < calendarInfo3.CreationRequestTime || !NotificationFactories.Instance.TryCreateReminderEmitting(calendarInfo3, action.Context, out action3))
					{
						CalendarNotificationInitiator.CompleteAction(action, "UpdateReminderAccordingToChangedUpdate");
						return;
					}
					CalendarNotificationInitiator.UpdateAction(action, action3, "UpdateReminderAccordingToChangedUpdate");
				}
			});
			foreach (CalendarInfo calendarInfo in eventsToBeAdded)
			{
				if (calendarInfo.ReminderIsSet)
				{
					if (calendarInfo.ReminderTime < calendarInfo.CreationRequestTime)
					{
						ExTraceGlobals.AssistantTracer.TraceDebug((long)typeof(CalendarChangeProcessor).GetHashCode(), "Reminder is out of date, subj: {0}, usr: {1}, event_t: {2}, rmd_t: {3}, s_t: {4}, e_t: {5}", new object[]
						{
							calendarInfo.NormalizedSubject,
							mailboxData.Settings.LegacyDN,
							calendarInfo.CreationRequestTime,
							calendarInfo.ReminderTime,
							calendarInfo.StartTime,
							calendarInfo.EndTime
						});
					}
					else if (nextReminderReloadTime == ExDateTime.MinValue || calendarInfo.ReminderTime <= nextReminderReloadTime)
					{
						ReminderEmitting action2;
						if (NotificationFactories.Instance.TryCreateReminderEmitting(calendarInfo, mailboxData, out action2))
						{
							CalendarNotificationInitiator.ScheduleAction(action2, "UpdateReminderAccordingToChangedUpdate");
						}
					}
					else
					{
						ExTraceGlobals.AssistantTracer.TraceDebug<ExDateTime, ExDateTime>((long)typeof(CalendarChangeProcessor).GetHashCode(), "We didn't process this event because reminderTime {0} is greater nextReminderReloadTime {1}. ", calendarInfo.ReminderTime, nextReminderReloadTime);
					}
				}
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0003DD58 File Offset: 0x0003BF58
		private static void UpdateReminder(CalendarNotificationType type, MailboxData mailboxData, StoreObjectId calItemId, IList<CalendarInfo> calEvents, IList<CalendarInfo> eventsToBeRemoved)
		{
			switch (type)
			{
			case CalendarNotificationType.NewUpdate:
			case CalendarNotificationType.ChangedUpdate:
				if (0 < calEvents.Count || 0 < eventsToBeRemoved.Count)
				{
					CalendarChangeProcessor.UpdateReminderAccordingToChangedUpdate(mailboxData, calEvents, eventsToBeRemoved);
					return;
				}
				CalendarChangeProcessor.UpdateReminderAccordingToDeletedUpdate(mailboxData, calItemId);
				return;
			case CalendarNotificationType.DeletedUpdate:
				CalendarChangeProcessor.UpdateReminderAccordingToDeletedUpdate(mailboxData, calItemId);
				return;
			default:
				throw new InvalidOperationException("unsupported CalendarNotificationType");
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0003DDB4 File Offset: 0x0003BFB4
		internal void HandleEvent(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			MailboxData fromCache = MailboxData.GetFromCache(mapiEvent.MailboxGuid);
			if (fromCache == null)
			{
				return;
			}
			using (fromCache.CreateReadLock())
			{
				ExDateTime eventTime = new ExDateTime(fromCache.Settings.TimeZone.ExTimeZone, mapiEvent.CreateTime);
				try
				{
					if (ObjectClass.IsMeetingMessage(mapiEvent.ObjectClass))
					{
						CalendarChangeProcessor.HandleMeetingEvent(eventTime, fromCache, mapiEvent, itemStore, item);
					}
					else if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(mapiEvent.ObjectClass))
					{
						CalendarChangeProcessor.HandleCalendarEvent(eventTime, fromCache, mapiEvent, itemStore, item);
					}
					else
					{
						ExTraceGlobals.AssistantTracer.TraceDebug<IExchangePrincipal, MapiEvent>((long)this.GetHashCode(), "why runs to here?! unhandled event for user {0}, event: {1}", itemStore.MailboxOwner, mapiEvent);
					}
				}
				catch (ConnectionFailedTransientException arg)
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<IExchangePrincipal, MapiEvent, ConnectionFailedTransientException>((long)this.GetHashCode(), "Exception is caught for user {0} when processing event: {1}\n{2}", itemStore.MailboxOwner, mapiEvent, arg);
				}
				catch (AdUserNotFoundException arg2)
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<IExchangePrincipal, MapiEvent, AdUserNotFoundException>((long)this.GetHashCode(), "Exception is caught for user {0} when processing event: {1}\n{2}", itemStore.MailboxOwner, mapiEvent, arg2);
				}
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
		private static void HandleMeetingEvent(ExDateTime eventTime, MailboxData mailboxData, MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			if (item == null || !NotificationFactories.Instance.IsInterestedInCalendarMeetingEvent(mailboxData.Settings))
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<MailboxData, MapiEvent>((long)typeof(CalendarChangeProcessor).GetHashCode(), "Ignore event because update is disabled for user {0}, event: {1}", mailboxData, mapiEvent);
				return;
			}
			if (object.Equals(item.Id.ObjectId, mailboxData.DefaultDeletedItemsFolderId) || object.Equals(item.Id.ObjectId, mailboxData.DefaultJunkEmailFolderId))
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<MailboxData, MapiEvent>((long)typeof(CalendarChangeProcessor).GetHashCode(), "Ignore event because item has been deleted for user {0}, event: {1}", mailboxData, mapiEvent);
				return;
			}
			MeetingMessage meetingMessage = item as MeetingMessage;
			if (meetingMessage == null)
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<MailboxData, MapiEvent>((long)typeof(CalendarChangeProcessor).GetHashCode(), "Meeting event handler could do nothing because the item is not meeting message for user {0}, event: {1}", mailboxData, mapiEvent);
				return;
			}
			if (meetingMessage.IsDelegated() || meetingMessage.IsMailboxOwnerTheSender() || meetingMessage.IsOutOfDate())
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<MailboxData, MapiEvent>((long)typeof(CalendarChangeProcessor).GetHashCode(), "Ignore event because mtg is delegated or out of data or is ornizer for user {0}, event: {1}", mailboxData, mapiEvent);
				return;
			}
			CalendarNotificationType calendarNotificationType = CalendarChangeProcessor.AnalyzeEvent(mapiEvent, meetingMessage);
			if (calendarNotificationType == CalendarNotificationType.Uninteresting)
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<MailboxData, MapiEvent>((long)typeof(CalendarChangeProcessor).GetHashCode(), "Ignore mtg event due to uninteresting for user {0}, event: {1}", mailboxData, mapiEvent);
				return;
			}
			List<CalendarInfo> list = null;
			Interval<ExDateTime> interval = new Interval<ExDateTime>(eventTime, false, eventTime + TimeSpan.FromDays((double)mailboxData.Settings.Text.TextNotification.NextDays), true);
			CalendarItemBase calendarItemBase = null;
			try
			{
				calendarItemBase = meetingMessage.GetCorrelatedItem();
				if (!meetingMessage.TryUpdateCalendarItem(ref calendarItemBase, false))
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<MailboxData, MapiEvent>((long)typeof(CalendarChangeProcessor).GetHashCode(), "Ignore mtg event because TryUpdateCalendarItem failed for user {0}, event: {1}", mailboxData, mapiEvent);
					return;
				}
				list = OccurrenceLoader.Load(eventTime, mailboxData.Settings.TimeZone.ExTimeZone, itemStore, meetingMessage as MeetingRequest, calendarItemBase, mailboxData.Settings.Text.TextNotification.CalendarNotificationSettings.UpdateSettings.Duration.NonWorkHoursExcluded ? mailboxData.Settings.Text.WorkingHours : null, interval.Minimum, interval.Maximum);
			}
			finally
			{
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
			int num = 0;
			foreach (CalendarInfo calendarInfo in list)
			{
				if (calendarInfo.IsInteresting(calendarNotificationType))
				{
					CalendarNotificationInitiator.ScheduleAction(new UpdateEmitting(mailboxData, calendarInfo, calendarNotificationType), "HandleMeetingEvent");
					num++;
				}
			}
			ExTraceGlobals.AssistantTracer.TraceDebug((long)typeof(CalendarChangeProcessor).GetHashCode(), "{0} updates has been sent according to mtg msg {1} for user {2}, type: {3}, event: {4}", new object[]
			{
				num,
				meetingMessage.Id.ObjectId,
				mailboxData,
				calendarNotificationType,
				mapiEvent
			});
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0003E288 File Offset: 0x0003C488
		private static void HandleCalendarEvent(ExDateTime eventTime, MailboxData mailboxData, MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			CalendarChangeProcessor.<>c__DisplayClassc CS$<>8__locals1 = new CalendarChangeProcessor.<>c__DisplayClassc();
			CS$<>8__locals1.mailboxData = mailboxData;
			CS$<>8__locals1.mapiEvent = mapiEvent;
			if (!NotificationFactories.Instance.IsReminderEnabled(CS$<>8__locals1.mailboxData.Settings))
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<MailboxData, MapiEvent>((long)typeof(CalendarChangeProcessor).GetHashCode(), "Ignore cal event because reminder is disabled for user {0}, event: {1}", CS$<>8__locals1.mailboxData, CS$<>8__locals1.mapiEvent);
				return;
			}
			CS$<>8__locals1.type = CalendarChangeProcessor.AnalyzeEvent(CS$<>8__locals1.mailboxData, CS$<>8__locals1.mapiEvent, item);
			if (CS$<>8__locals1.type == CalendarNotificationType.Uninteresting)
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<MailboxData, MapiEvent>((long)typeof(CalendarChangeProcessor).GetHashCode(), "Ignore cal event due to uninteresting for user {0}, event: {1}", CS$<>8__locals1.mailboxData, CS$<>8__locals1.mapiEvent);
				return;
			}
			CS$<>8__locals1.calItemId = ((item == null) ? StoreObjectId.FromProviderSpecificId(CS$<>8__locals1.mapiEvent.ItemEntryId) : item.Id.ObjectId);
			CalendarItem calendarItem = (CalendarItem)item;
			TimeSpan t = TimeSpan.FromDays(1.0);
			CS$<>8__locals1.calEvents = null;
			if (item != null)
			{
				TimeSpan t2 = TimeSpan.FromMinutes((double)calendarItem.Reminder.MinutesBeforeStart) + TimeSpan.FromTicks(1L);
				Interval<ExDateTime> interval = new Interval<ExDateTime>(eventTime, false, eventTime + t + t2, true);
				CS$<>8__locals1.calEvents = OccurrenceLoader.Load(eventTime, CS$<>8__locals1.mailboxData.Settings.TimeZone.ExTimeZone, itemStore, null, calendarItem, null, interval.Minimum, interval.Maximum);
			}
			else
			{
				CS$<>8__locals1.calEvents = new List<CalendarInfo>();
			}
			CS$<>8__locals1.eventsToBeRemoved = new List<CalendarInfo>();
			if (CalendarNotificationType.ChangedUpdate == CS$<>8__locals1.type)
			{
				CS$<>8__locals1.calEvents.RemoveAll(delegate(CalendarInfo calInfo)
				{
					CalendarNotificationType calendarNotificationType = CalendarChangeProcessor.AnalyzeEvent(CS$<>8__locals1.mapiEvent, CS$<>8__locals1.mailboxData.DefaultCalendarFolderId, calInfo.AppointmentState, calInfo.ChangeHighlight, calInfo.ResponseType);
					if (calendarNotificationType == CalendarNotificationType.Uninteresting)
					{
						return true;
					}
					if (CalendarNotificationType.DeletedUpdate == calendarNotificationType)
					{
						CS$<>8__locals1.eventsToBeRemoved.Add(calInfo);
						return true;
					}
					return false;
				});
			}
			using (ManualResetEvent customizedOperationFinished = new ManualResetEvent(false))
			{
				CalendarNotificationInitiator.PerformCustomizedOperationOnActionsList(delegate
				{
					try
					{
						using (CS$<>8__locals1.mailboxData.CreateReadLock())
						{
							CalendarChangeProcessor.UpdateReminder(CS$<>8__locals1.type, CS$<>8__locals1.mailboxData, CS$<>8__locals1.calItemId, CS$<>8__locals1.calEvents, CS$<>8__locals1.eventsToBeRemoved);
						}
					}
					finally
					{
						customizedOperationFinished.Set();
					}
				}, "HandleCalendarEvent");
				WaitHandle.WaitAny(new WaitHandle[]
				{
					customizedOperationFinished
				});
			}
			ExTraceGlobals.AssistantTracer.TraceDebug((long)typeof(CalendarChangeProcessor).GetHashCode(), "TYPE: {0}. reminders updated according to item {1} of user {2}, , cal evts #: {3}, cal evts to be removed #: {4}, event: {5}", new object[]
			{
				CS$<>8__locals1.type,
				CS$<>8__locals1.calItemId,
				CS$<>8__locals1.mailboxData,
				CS$<>8__locals1.calEvents.Count,
				CS$<>8__locals1.eventsToBeRemoved.Count,
				CS$<>8__locals1.mapiEvent
			});
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0003E54C File Offset: 0x0003C74C
		internal static void UpdateAverageProcessingLatency(ExDateTime mailboxEventTimestamp)
		{
			int milliseconds = (ExDateTime.UtcNow - mailboxEventTimestamp).Milliseconds;
			lock (CalendarChangeProcessor.latencySamples)
			{
				int[] array = CalendarChangeProcessor.latencySamples;
				int num = CalendarChangeProcessor.insertionIndex;
				int num2 = CalendarChangeProcessor.numSamples;
				int num3 = array[num];
				array[num] = milliseconds;
				CalendarChangeProcessor.insertionIndex = (num + 1) % array.Length;
				if (num2 < array.Length)
				{
					num2 = ++CalendarChangeProcessor.numSamples;
				}
				CalendarChangeProcessor.latencySum += milliseconds;
				CalendarChangeProcessor.latencySum -= num3;
				CalNotifsCounters.AverageUpdateLatency.RawValue = (long)(CalendarChangeProcessor.latencySum / num2);
			}
		}

		// Token: 0x04000628 RID: 1576
		private static int[] latencySamples = new int[1024];

		// Token: 0x04000629 RID: 1577
		private static int insertionIndex;

		// Token: 0x0400062A RID: 1578
		private static int latencySum;

		// Token: 0x0400062B RID: 1579
		private static int numSamples;
	}
}
