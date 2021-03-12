using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.FreeBusy;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.AssistantsClientResources;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x0200024E RID: 590
	internal class ReminderMessageManager : IReminderMessageManager
	{
		// Token: 0x0600160D RID: 5645 RVA: 0x0007B761 File Offset: 0x00079961
		public ReminderMessageManager(IRemindersAssistantLog log, IReminderTimeCalculatorFactory reminderTimeCalculatorFactory)
		{
			ArgumentValidator.ThrowIfNull("log", log);
			ArgumentValidator.ThrowIfNull("reminderTimeCalculatorFactory", reminderTimeCalculatorFactory);
			this.Log = log;
			this.reminderTimeCalculatorFactory = reminderTimeCalculatorFactory;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0007B78D File Offset: 0x0007998D
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x0007B795 File Offset: 0x00079995
		private IRemindersAssistantLog Log { get; set; }

		// Token: 0x06001610 RID: 5648 RVA: 0x0007B7A0 File Offset: 0x000799A0
		public int DeleteReminderMessages(IMailboxSession itemStore, ICalendarItem calendarItem)
		{
			ArgumentValidator.ThrowIfNull("itemStore", itemStore);
			ArgumentValidator.ThrowIfNull("calendarItem", calendarItem);
			int num = 0;
			Dictionary<Guid, VersionedId> reminderMessages = this.GetReminderMessages(itemStore, calendarItem.GlobalObjectId, "IPM.Note.Reminder.Event");
			foreach (Guid guid in reminderMessages.Keys)
			{
				this.Log.LogEntry(itemStore, "ReminderMessageManager.DeleteReminderMessages - Deleting event reminder message, Start Time:{0}, End Time:{1}, Reminder Id:{2}", new object[]
				{
					calendarItem.StartTime.UniversalTime,
					calendarItem.EndTime.UniversalTime,
					guid
				});
				this.DeleteReminderMessage(itemStore, calendarItem.GlobalObjectId, "IPM.Note.Reminder.Event", guid, reminderMessages[guid]);
				num++;
			}
			return num;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0007B890 File Offset: 0x00079A90
		public int CreateReminderMessages(IMailboxSession session, ICalendarItem calendarItem, Reminders<EventTimeBasedInboxReminder> reminders, List<KeyValuePair<string, object>> customDataToLog)
		{
			ArgumentValidator.ThrowIfNull("itemStore", session);
			ArgumentValidator.ThrowIfNull("calendarItem", calendarItem);
			int num = 0;
			RemindersState<EventTimeBasedInboxReminderState> remindersState = calendarItem.EventTimeBasedInboxRemindersState;
			HashSet<Guid> hashSet = new HashSet<Guid>();
			bool flag = calendarItem.Recurrence != null;
			bool flag2 = false;
			if (flag)
			{
				Dictionary<Guid, List<ExDateTime>> dictionary = new Dictionary<Guid, List<ExDateTime>>();
				this.LoadHasExceptionalInboxRemindersProperty(calendarItem);
				if (calendarItem.HasExceptionalInboxReminders)
				{
					this.Log.LogEntry(session, "ReminderMessageManager.CreateReminderMessages - Processing exceptional reminders", new object[0]);
					if (reminders != null)
					{
						foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder in reminders.ReminderList)
						{
							dictionary[eventTimeBasedInboxReminder.Identifier] = new List<ExDateTime>();
						}
					}
					int num2 = 0;
					int num3 = 0;
					IList<OccurrenceInfo> modifiedOccurrences = calendarItem.Recurrence.GetModifiedOccurrences();
					if (modifiedOccurrences.Count > 10)
					{
						customDataToLog.Add(new KeyValuePair<string, object>("EvtRemExc.Exceeded", true.ToString()));
						while (num2 < modifiedOccurrences.Count && modifiedOccurrences[num2].OriginalStartTime < ExDateTime.Now)
						{
							num2++;
						}
						num2 = ((num2 > 2) ? (num2 - 2) : 0);
					}
					while (num2 < modifiedOccurrences.Count && num3 < 10)
					{
						OccurrenceInfo occurrenceInfo = modifiedOccurrences[num2];
						using (ICalendarItemBase occurrenceData = this.GetOccurrenceData(calendarItem, occurrenceInfo.VersionedId.ObjectId))
						{
							Reminders<EventTimeBasedInboxReminder> eventTimeBasedInboxReminders = occurrenceData.EventTimeBasedInboxReminders;
							if (eventTimeBasedInboxReminders != null)
							{
								foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder2 in eventTimeBasedInboxReminders.ReminderList)
								{
									if (eventTimeBasedInboxReminder2.IsFormerSeriesReminder && dictionary.ContainsKey(eventTimeBasedInboxReminder2.SeriesReminderId))
									{
										dictionary[eventTimeBasedInboxReminder2.SeriesReminderId].Add(occurrenceInfo.OccurrenceDateId);
									}
									if (eventTimeBasedInboxReminder2.IsActiveExceptionReminder)
									{
										hashSet.Add(eventTimeBasedInboxReminder2.Identifier);
										bool flag3 = this.CreateAppointmentReminderMessage(session, calendarItem, occurrenceData, eventTimeBasedInboxReminder2, ref remindersState);
										if (flag3)
										{
											num++;
										}
									}
								}
							}
						}
						num2++;
						num3++;
					}
					customDataToLog.Add(new KeyValuePair<string, object>("EvtRemExc.Opened", num3));
				}
				if (reminders != null)
				{
					Dictionary<StoreObjectId, List<EventTimeBasedInboxReminder>> dictionary2 = new Dictionary<StoreObjectId, List<EventTimeBasedInboxReminder>>();
					foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder3 in reminders.ReminderList)
					{
						this.Log.LogEntry(session, "CreateReminderMessages - Calculating next occurrence for reminder id:{0}", new object[]
						{
							eventTimeBasedInboxReminder3.Identifier
						});
						List<ExDateTime> changedOccurrences = null;
						if (dictionary.ContainsKey(eventTimeBasedInboxReminder3.Identifier))
						{
							changedOccurrences = dictionary[eventTimeBasedInboxReminder3.Identifier];
						}
						OccurrenceInfo occurrenceInfo2 = this.CalculateNextOccurrenceForSeriesReminder(calendarItem, eventTimeBasedInboxReminder3, changedOccurrences);
						if (occurrenceInfo2 == null)
						{
							this.Log.LogEntry(session, "CreateReminderMessages - No occurrences to schedule before series end for reminder id:{0}", new object[]
							{
								eventTimeBasedInboxReminder3.Identifier
							});
						}
						else
						{
							StoreObjectId objectId = occurrenceInfo2.VersionedId.ObjectId;
							if (!dictionary2.ContainsKey(objectId))
							{
								dictionary2[objectId] = new List<EventTimeBasedInboxReminder>();
							}
							dictionary2[objectId].Add(eventTimeBasedInboxReminder3);
						}
					}
					foreach (StoreObjectId storeObjectId in dictionary2.Keys)
					{
						using (ICalendarItemBase occurrenceData2 = this.GetOccurrenceData(calendarItem, storeObjectId))
						{
							foreach (EventTimeBasedInboxReminder reminder in dictionary2[storeObjectId])
							{
								this.CreateSeriesReminderMessage(session, calendarItem, occurrenceData2, reminder);
								num++;
							}
						}
					}
				}
			}
			if (!flag && reminders != null)
			{
				foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder4 in reminders.ReminderList)
				{
					this.Log.LogEntry(session, "ReminderMessageManager.CreateReminderMessages - Adding single event reminder message, Start Time:{0}, End Time:{1}, Reminder Id:{2}", new object[]
					{
						calendarItem.StartTime.UniversalTime,
						calendarItem.EndTime.UniversalTime,
						eventTimeBasedInboxReminder4.Identifier
					});
					hashSet.Add(eventTimeBasedInboxReminder4.Identifier);
					bool flag3 = this.CreateAppointmentReminderMessage(session, calendarItem, null, eventTimeBasedInboxReminder4, ref remindersState);
					if (flag3)
					{
						num++;
					}
				}
			}
			if (remindersState != null)
			{
				bool flag4 = this.RemoveStaleRemindersState<EventTimeBasedInboxReminderState>(hashSet, remindersState.StateList);
				if (num > 0 || flag4)
				{
					flag2 = true;
				}
				if (remindersState.StateList.Count == 0)
				{
					flag2 = true;
					remindersState = null;
				}
			}
			if (flag2)
			{
				this.Log.LogEntry(session, "ReminderMessageManager.CreateReminderMessages - Saving reminder state", new object[0]);
				calendarItem.EventTimeBasedInboxRemindersState = remindersState;
				calendarItem.Save(SaveMode.ResolveConflicts);
			}
			return num;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0007BDEC File Offset: 0x00079FEC
		private bool CreateAppointmentReminderMessage(IMailboxSession session, ICalendarItem masterCalendarItem, ICalendarItemBase occurrenceCalendarItem, EventTimeBasedInboxReminder reminder, ref RemindersState<EventTimeBasedInboxReminderState> remindersState)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("masterCalendarItem", masterCalendarItem);
			ArgumentValidator.ThrowIfNull("reminder", reminder);
			this.Log.LogEntry(session, "CreateAppointmentReminderMessage - Scheduling single instance reminder with reminder id:{0}", new object[]
			{
				reminder.Identifier
			});
			bool flag = occurrenceCalendarItem != null;
			ICalendarItemBase calendarItemBase = flag ? occurrenceCalendarItem : masterCalendarItem;
			ExDateTime reminderTime = calendarItemBase.StartTime - new TimeSpan(0, reminder.ReminderOffset, 0);
			bool hasState;
			if (!this.ShouldScheduleEventTimeBasedInboxReminder(session, reminderTime, reminder.Identifier, ref remindersState, out hasState))
			{
				return false;
			}
			string reminderSubject;
			string reminderBody;
			this.GetEventReminderContent(session, calendarItemBase, reminder, out reminderSubject, out reminderBody);
			this.CreateEventReminderMessage(session, reminder.Identifier, reminderTime, reminder.CustomMessage, reminderSubject, reminderBody, reminder.IsOrganizerReminder, calendarItemBase.AttendeeCollection, masterCalendarItem.InternetMessageId, calendarItemBase.StartTime, calendarItemBase.EndTime, calendarItemBase.Location, masterCalendarItem.GlobalObjectId, flag ? occurrenceCalendarItem.GlobalObjectId : null, hasState, false, reminder.OccurrenceChange);
			return true;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0007BEF0 File Offset: 0x0007A0F0
		private OccurrenceInfo CalculateNextOccurrenceForSeriesReminder(ICalendarItem calendarItem, EventTimeBasedInboxReminder reminder, List<ExDateTime> changedOccurrences)
		{
			ArgumentValidator.ThrowIfNull("calendarItem", calendarItem);
			ArgumentValidator.ThrowIfNull("reminder", reminder);
			ExDateTime exDateTime = ExDateTime.Now + new TimeSpan(0, reminder.ReminderOffset + 1, 0);
			OccurrenceInfo occurrenceInfo = calendarItem.Recurrence.GetFirstOccurrenceAfterDate(exDateTime - new TimeSpan(24, 0, 0));
			if (occurrenceInfo == null)
			{
				return null;
			}
			OccurrenceInfo result;
			try
			{
				while (occurrenceInfo.StartTime < exDateTime)
				{
					occurrenceInfo = calendarItem.Recurrence.GetNextOccurrence(occurrenceInfo);
				}
				if (changedOccurrences != null)
				{
					while (changedOccurrences.Contains(occurrenceInfo.OccurrenceDateId))
					{
						occurrenceInfo = calendarItem.Recurrence.GetNextOccurrence(occurrenceInfo);
					}
				}
				result = occurrenceInfo;
			}
			catch (ArgumentOutOfRangeException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0007BFA4 File Offset: 0x0007A1A4
		private void CreateSeriesReminderMessage(IMailboxSession session, ICalendarItem calendarItem, ICalendarItemBase occurrence, EventTimeBasedInboxReminder reminder)
		{
			ExDateTime reminderTime = occurrence.StartTime - new TimeSpan(0, reminder.ReminderOffset, 0);
			string reminderSubject;
			string reminderBody;
			this.GetEventReminderContent(session, occurrence, reminder, out reminderSubject, out reminderBody);
			this.CreateEventReminderMessage(session, reminder.Identifier, reminderTime, reminder.CustomMessage, reminderSubject, reminderBody, reminder.IsOrganizerReminder, occurrence.AttendeeCollection, calendarItem.InternetMessageId, occurrence.StartTime, occurrence.EndTime, occurrence.Location, calendarItem.GlobalObjectId, occurrence.GlobalObjectId, false, true, reminder.OccurrenceChange);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0007C02C File Offset: 0x0007A22C
		private void GetEventReminderContent(IMailboxSession session, ICalendarItemBase calendarItem, EventTimeBasedInboxReminder reminder, out string subject, out string body)
		{
			subject = null;
			body = null;
			CultureInfo cultureInfo;
			ExTimeZoneValue exTimeZoneValue;
			string format;
			string format2;
			this.LoadRegionalConfiguration(session, out cultureInfo, out exTimeZoneValue, out format, out format2);
			subject = Strings.EventReminderSubject(calendarItem.Subject).ToString(cultureInfo);
			ExDateTime exDateTime = exTimeZoneValue.ExTimeZone.ConvertDateTime(calendarItem.StartTime);
			ExDateTime exDateTime2 = exTimeZoneValue.ExTimeZone.ConvertDateTime(calendarItem.EndTime);
			string dateTime;
			if (exDateTime2.Date == exDateTime.Date)
			{
				dateTime = Strings.DateTimeSingleDay(exDateTime.Date.ToString(format, cultureInfo), exDateTime.ToString(format2, cultureInfo), exDateTime2.ToString(format2, cultureInfo));
			}
			else
			{
				dateTime = Strings.DateTimeMultiDay(exDateTime.Date.ToString(format, cultureInfo), exDateTime.ToString(format2, cultureInfo), exDateTime2.Date.ToString(format, cultureInfo), exDateTime2.ToString(format2, cultureInfo));
			}
			string location = calendarItem.Location;
			string summary;
			if (!string.IsNullOrEmpty(location))
			{
				summary = Strings.EventReminderSummary(dateTime, location).ToString(cultureInfo);
			}
			else
			{
				summary = Strings.EventReminderSummaryNoLocation(dateTime).ToString(cultureInfo);
			}
			string customMessage;
			if (!string.IsNullOrEmpty(reminder.CustomMessage))
			{
				customMessage = reminder.CustomMessage;
			}
			else
			{
				customMessage = string.Empty;
			}
			body = Strings.ReminderMessageBody(summary, customMessage).ToString(cultureInfo);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0007C186 File Offset: 0x0007A386
		protected virtual ICalendarItemBase GetOccurrenceData(ICalendarItem calendarItem, StoreObjectId occurrenceObjectId)
		{
			return calendarItem.OpenOccurrence(occurrenceObjectId, null);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0007C190 File Offset: 0x0007A390
		protected virtual void LoadHasExceptionalInboxRemindersProperty(ICalendarItem calendarItem)
		{
			calendarItem.Load(new StorePropertyDefinition[]
			{
				CalendarItemSchema.HasExceptionalInboxReminders
			});
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0007C1B4 File Offset: 0x0007A3B4
		protected virtual void CreateEventReminderMessage(IMailboxSession session, Guid reminderId, ExDateTime reminderTime, string reminderCustomMessage, string reminderSubject, string reminderBody, bool isOrganizerReminder, IAttendeeCollection attendees, string eventInReplyTo, ExDateTime eventStartTime, ExDateTime eventEndTime, string eventLocation, GlobalObjectId eventGlobalObjectId, GlobalObjectId occurrenceGlobalObjectId, bool hasState, bool isRecurring, EmailReminderChangeType occurrenceChange)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			this.Log.LogEntry(mailboxSession, "ReminderMessageManager.CreateEventReminderMessage - Creating reminder message, Start Time:{0}, End Time:{1}, Reminder Id:{2}, Reminder Time:{3}", new object[]
			{
				eventStartTime.UniversalTime,
				eventEndTime.UniversalTime,
				reminderId,
				reminderTime.UniversalTime
			});
			using (Folder folder = this.OpenOrCreateFolder(mailboxSession, "Reminders Staging"))
			{
				using (ReminderMessage reminderMessage = ReminderMessage.CreateReminderMessage(mailboxSession, folder.Id, "IPM.Note.Reminder.Event"))
				{
					reminderMessage.Subject = reminderSubject;
					reminderMessage.From = new Participant(mailboxSession.MailboxOwner);
					reminderMessage.AutoResponseSuppress = AutoResponseSuppress.All;
					reminderMessage[ReminderMessageManager.DeferredSendTimeDefinition] = reminderTime;
					reminderMessage.ReminderId = reminderId;
					reminderMessage.InReplyTo = eventInReplyTo;
					reminderMessage.ReminderText = reminderCustomMessage;
					reminderMessage.Location = eventLocation;
					reminderMessage.ReminderStartTime = eventStartTime;
					reminderMessage.ReminderEndTime = eventEndTime;
					reminderMessage.ReminderItemGlobalObjectId = eventGlobalObjectId;
					if (occurrenceGlobalObjectId != null)
					{
						reminderMessage.ReminderOccurrenceGlobalObjectId = occurrenceGlobalObjectId;
					}
					reminderMessage.Recipients.Add(new Participant(session.MailboxOwner), RecipientItemType.To);
					if (isOrganizerReminder && attendees != null)
					{
						foreach (Attendee attendee in attendees)
						{
							if (attendee.ResponseType != ResponseType.Decline && attendee.IsSendable())
							{
								reminderMessage.Recipients.Add(attendee.Participant, RecipientItemType.To);
							}
						}
					}
					using (Stream stream = reminderMessage.Body.OpenWriteStream(new BodyWriteConfiguration(BodyFormat.TextHtml, Charset.UTF8.Name)))
					{
						byte[] bytes = Encoding.UTF8.GetBytes(reminderBody);
						stream.Write(bytes, 0, bytes.Length);
					}
					reminderMessage.SendWithoutSavingMessage();
					Dictionary<string, object> dictionary = new Dictionary<string, object>(5);
					dictionary.Add("LEN", reminderCustomMessage.Length);
					dictionary.Add("ET", eventStartTime.UniversalTime.ToString("o"));
					dictionary.Add("RT", reminderTime.UniversalTime.ToString("o"));
					dictionary.Add("ST", ExDateTime.UtcNow.ToString("o"));
					dictionary.Add("OF", (eventStartTime - reminderTime).ToString("c"));
					dictionary.Add("ORG", isOrganizerReminder.ToString());
					dictionary.Add("HS", hasState.ToString());
					dictionary.Add("IR", isRecurring.ToString());
					dictionary.Add("OC", occurrenceChange.ToString());
					dictionary.Add("MID", session.MailboxGuid);
					ReminderLogger.Instance.LogEvent(new ReminderLogEvent(ReminderLogEventType.Add, "IPM.Note.Reminder.Event", eventGlobalObjectId, reminderId, dictionary));
				}
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0007C50C File Offset: 0x0007A70C
		protected virtual bool ShouldScheduleEventTimeBasedInboxReminder(IMailboxSession session, ExDateTime reminderTime, Guid reminderId, ref RemindersState<EventTimeBasedInboxReminderState> remindersState, out bool hasState)
		{
			hasState = false;
			if (remindersState != null)
			{
				foreach (EventTimeBasedInboxReminderState eventTimeBasedInboxReminderState in remindersState.StateList)
				{
					if (reminderId == eventTimeBasedInboxReminderState.Identifier)
					{
						this.Log.LogEntry(session, "ShouldScheduleEventTimeBasedInboxReminder - Found state object for reminder Reminder Id:{0}", new object[]
						{
							reminderId
						});
						hasState = true;
						ExDateTime scheduledReminderTime = eventTimeBasedInboxReminderState.ScheduledReminderTime;
						if (reminderTime < ExDateTime.Now && scheduledReminderTime < ExDateTime.Now)
						{
							this.Log.LogEntry(session, "ShouldScheduleEventTimeBasedInboxReminder - Not scheduling reminder Reminder Id:{0}", new object[]
							{
								reminderId
							});
							return false;
						}
						eventTimeBasedInboxReminderState.ScheduledReminderTime = reminderTime;
						return true;
					}
				}
			}
			this.Log.LogEntry(session, "ShouldScheduleEventTimeBasedInboxReminder - Adding state for reminder Reminder Id:{0}", new object[]
			{
				reminderId
			});
			EventTimeBasedInboxReminderState eventTimeBasedInboxReminderState2 = new EventTimeBasedInboxReminderState();
			eventTimeBasedInboxReminderState2.Identifier = reminderId;
			eventTimeBasedInboxReminderState2.ScheduledReminderTime = reminderTime;
			if (remindersState == null)
			{
				remindersState = new RemindersState<EventTimeBasedInboxReminderState>();
			}
			remindersState.StateList.Add(eventTimeBasedInboxReminderState2);
			return true;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0007C650 File Offset: 0x0007A850
		public void ScheduleNextOccurrenceReminder(IMailboxSession session, ICalendarItem calendarItem, Guid reminderId)
		{
			if (calendarItem.Recurrence != null)
			{
				this.Log.LogEntry(session, "ScheduleNextOccurrenceReminder - Scheduling next occurrence of recurring reminder:{0}", new object[]
				{
					reminderId
				});
				Reminders<EventTimeBasedInboxReminder> eventTimeBasedInboxReminders = calendarItem.EventTimeBasedInboxReminders;
				if (eventTimeBasedInboxReminders != null)
				{
					foreach (EventTimeBasedInboxReminder eventTimeBasedInboxReminder in eventTimeBasedInboxReminders.ReminderList)
					{
						if (eventTimeBasedInboxReminder.Identifier == reminderId)
						{
							this.LoadHasExceptionalInboxRemindersProperty(calendarItem);
							OccurrenceInfo occurrenceInfo = this.CalculateNextOccurrenceForSeriesReminder(calendarItem, eventTimeBasedInboxReminder, null);
							if (occurrenceInfo == null)
							{
								return;
							}
							ICalendarItemBase calendarItemBase = null;
							try
							{
								calendarItemBase = this.GetOccurrenceData(calendarItem, occurrenceInfo.VersionedId.ObjectId);
								EventTimeBasedInboxReminder seriesReminder = EventTimeBasedInboxReminder.GetSeriesReminder(calendarItemBase.EventTimeBasedInboxReminders, eventTimeBasedInboxReminder.Identifier);
								while (seriesReminder.IsFormerSeriesReminder)
								{
									calendarItemBase.Dispose();
									calendarItemBase = null;
									occurrenceInfo = calendarItem.Recurrence.GetNextOccurrence(occurrenceInfo);
									if (occurrenceInfo == null)
									{
										return;
									}
									calendarItemBase = this.GetOccurrenceData(calendarItem, occurrenceInfo.VersionedId.ObjectId);
									seriesReminder = EventTimeBasedInboxReminder.GetSeriesReminder(calendarItemBase.EventTimeBasedInboxReminders, eventTimeBasedInboxReminder.Identifier);
								}
								this.CreateSeriesReminderMessage(session, calendarItem, calendarItemBase, eventTimeBasedInboxReminder);
								return;
							}
							catch (ArgumentOutOfRangeException)
							{
								return;
							}
							finally
							{
								if (calendarItemBase != null)
								{
									calendarItemBase.Dispose();
								}
							}
						}
					}
				}
				this.Log.LogEntry(session, "ScheduleNextOccurrenceReminder - Could not find setting for reminder id:{0}", new object[]
				{
					reminderId
				});
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0007C7D8 File Offset: 0x0007A9D8
		public int DeleteReminderMessages(IMailboxSession itemStore, IToDoItem messageItem)
		{
			ArgumentValidator.ThrowIfNull("itemStore", itemStore);
			ArgumentValidator.ThrowIfNull("messageItem", messageItem);
			int num = 0;
			Dictionary<Guid, VersionedId> reminderMessages = this.GetReminderMessages(itemStore, messageItem.GetGlobalObjectId(), "IPM.Note.Reminder.Modern");
			foreach (Guid guid in reminderMessages.Keys)
			{
				this.Log.LogEntry(itemStore, "ReminderMessageManager.DeleteReminderMessages - Deleting modern reminder message, Reminder Id:{0}", new object[]
				{
					guid
				});
				this.DeleteReminderMessage(itemStore, messageItem.GetGlobalObjectId(), "IPM.Note.Reminder.Modern", guid, reminderMessages[guid]);
				num++;
			}
			return num;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0007C894 File Offset: 0x0007AA94
		public int CreateReminderMessages(IMailboxSession itemStore, IToDoItem messageItem, Reminders<ModernReminder> reminders)
		{
			ArgumentValidator.ThrowIfNull("itemStore", itemStore);
			ArgumentValidator.ThrowIfNull("messageItem", messageItem);
			int num = 0;
			RemindersState<ModernReminderState> remindersState = messageItem.ModernRemindersState;
			bool flag = false;
			if (reminders != null)
			{
				foreach (ModernReminder modernReminder in reminders.ReminderList)
				{
					this.Log.LogEntry(itemStore, "ReminderMessageManager.CreateReminderMessages - Adding modern reminder message, Reminder Id:{0}", new object[]
					{
						modernReminder.Identifier
					});
					bool flag2 = this.CreateReminderMessage(itemStore, messageItem, "IPM.Note.Reminder.Modern", modernReminder, ref remindersState);
					if (flag2)
					{
						num++;
					}
				}
				HashSet<Guid> hashSet = new HashSet<Guid>();
				foreach (ModernReminder modernReminder2 in messageItem.ModernReminders.ReminderList)
				{
					hashSet.Add(modernReminder2.Identifier);
				}
				bool flag3 = this.RemoveStaleRemindersState<ModernReminderState>(hashSet, remindersState.StateList);
				if (num > 0 || flag3)
				{
					flag = true;
				}
			}
			else
			{
				this.Log.LogEntry(itemStore, "ReminderMessageManager.CreateReminderMessages - No reminders", new object[0]);
				if (remindersState != null)
				{
					remindersState = null;
					flag = true;
				}
			}
			if (flag)
			{
				this.Log.LogEntry(itemStore, "ReminderMessageManager.CreateReminderMessages - Saving reminder state", new object[0]);
				messageItem.ModernRemindersState = remindersState;
				messageItem.Save(SaveMode.ResolveConflicts);
			}
			return num;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0007CA08 File Offset: 0x0007AC08
		private bool CreateReminderMessage(IMailboxSession session, IToDoItem messageItem, string reminderObjectClass, ModernReminder reminder, ref RemindersState<ModernReminderState> remindersState)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("messageItem", messageItem);
			ArgumentValidator.ThrowIfNullOrEmpty("reminderObjectClass", reminderObjectClass);
			ArgumentValidator.ThrowIfNull("reminder", reminder);
			ExTraceGlobals.GeneralTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ReminderMessageManager.CreateReminderMessage for ModernReminder '{0}'", reminder.Identifier);
			string text = Strings.EventReminderSubject(messageItem.Subject).ToString(ReminderMessageManager.DefaultCulture);
			ExDateTime utcNow = ExDateTime.UtcNow;
			ExDateTime exDateTime = this.CalculateReminderTime(reminder, session, utcNow);
			this.Log.LogEntry(session, "CreateReminderMessage - Reminder Id:{0} reminderSubject:'{1}' scheduledReminderTime:{2} reminderTimeHint:{3}", new object[]
			{
				reminder.Identifier,
				text,
				exDateTime.UniversalTime,
				reminder.ReminderTimeHint
			});
			if (exDateTime == ExDateTime.MaxValue)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ReminderMessageManager.CreateReminderMessage - CustomReminderTime not set for ModernReminder '{0}'", reminder.Identifier);
				return false;
			}
			bool flag;
			if (!this.ShouldScheduleModernReminder(session, exDateTime, reminder.Identifier, ref remindersState, utcNow, out flag))
			{
				return false;
			}
			GlobalObjectId globalObjectId = messageItem.GetGlobalObjectId();
			this.CreateModernReminderMessage(session, reminderObjectClass, reminder.Identifier, exDateTime, text, messageItem.InternetMessageId, globalObjectId);
			return true;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0007CB40 File Offset: 0x0007AD40
		public int ScheduleReminder(IMailboxSession itemStore, IToDoItem toDoItem, Reminders<ModernReminder> reminders)
		{
			if (reminders.ReminderList.Count == 1)
			{
				ModernReminder reminder = reminders.ReminderList[0];
				ExDateTime value = this.CalculateReminderTime(reminder, itemStore, ExDateTime.UtcNow);
				Task task = (Task)toDoItem;
				task.Reminder.DueBy = new ExDateTime?(value);
				task.Reminder.IsSet = true;
				task.Reminder.Adjust();
				task.Save(SaveMode.ResolveConflicts);
				return 1;
			}
			ExTraceGlobals.GeneralTracer.TraceWarning<int>((long)this.GetHashCode(), "ReminderMessageManager.ScheduleReminder - expecting only one reminder. Actual count is {0}", reminders.ReminderList.Count);
			return 0;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0007CBD4 File Offset: 0x0007ADD4
		public void ClearReminder(IMailboxSession itemStore, IToDoItem toDoItem)
		{
			ExTraceGlobals.GeneralTracer.TraceWarning((long)this.GetHashCode(), "ReminderMessageManager.ClearReminder - clearing reminders");
			Task task = (Task)toDoItem;
			task.Reminder.Disable();
			task.Save(SaveMode.ResolveConflicts);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0007CC14 File Offset: 0x0007AE14
		private ExDateTime CalculateReminderTime(ModernReminder reminder, IMailboxSession session, ExDateTime nowInUTC)
		{
			IReminderTimeCalculator reminderTimeCalculator = this.reminderTimeCalculatorFactory.Create(session);
			return reminderTimeCalculator.CalculateReminderTime(reminder, nowInUTC);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0007CC38 File Offset: 0x0007AE38
		protected virtual void CreateModernReminderMessage(IMailboxSession session, string reminderObjectClass, Guid reminderId, ExDateTime reminderTime, string reminderSubject, string modernReminderInReplyTo, GlobalObjectId modernReminderGlobalObjectId)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			using (Folder folder = this.OpenOrCreateFolder(mailboxSession, "Reminders Staging"))
			{
				using (ReminderMessage reminderMessage = ReminderMessage.CreateReminderMessage(mailboxSession, folder.Id, reminderObjectClass))
				{
					this.Log.LogEntry(session, "CreateModernReminderMessage - Reminder Id:{0} scheduledReminderTime:{1} reminder subject:{2}", new object[]
					{
						reminderId,
						reminderTime.UniversalTime,
						reminderSubject
					});
					reminderMessage.Subject = reminderSubject;
					reminderMessage.Recipients.Add(new Participant(mailboxSession.MailboxOwner), RecipientItemType.To);
					reminderMessage.From = new Participant(mailboxSession.MailboxOwner);
					reminderMessage.AutoResponseSuppress = AutoResponseSuppress.All;
					reminderMessage[ReminderMessageManager.DeferredSendTimeDefinition] = reminderTime;
					reminderMessage.ReminderId = reminderId;
					reminderMessage.ReminderItemGlobalObjectId = modernReminderGlobalObjectId;
					reminderMessage.InReplyTo = modernReminderInReplyTo;
					reminderMessage.SendWithoutSavingMessage();
				}
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0007CD38 File Offset: 0x0007AF38
		protected virtual bool ShouldScheduleModernReminder(IMailboxSession session, ExDateTime reminderTime, Guid reminderId, ref RemindersState<ModernReminderState> remindersState, ExDateTime nowInUTC, out bool hasState)
		{
			hasState = false;
			if (remindersState != null)
			{
				foreach (ModernReminderState modernReminderState in remindersState.StateList)
				{
					if (modernReminderState.Identifier == reminderId)
					{
						this.Log.LogEntry(session, "ShouldScheduleModernReminder - Found state object for reminder Reminder Id:{0}", new object[]
						{
							reminderId
						});
						hasState = true;
						ExDateTime exDateTime = modernReminderState.ScheduledReminderTime.ToUtc();
						this.Log.LogEntry(session, "ShouldScheduleModernReminder - reminderTime:{0} previousSendTime:{1} ExDateTimeNow:{2}", new object[]
						{
							reminderTime,
							exDateTime,
							nowInUTC
						});
						if (reminderTime < nowInUTC && exDateTime < nowInUTC)
						{
							this.Log.LogEntry(session, "ShouldScheduleModernReminder - Not scheduling reminder Reminder Id:{0}", new object[]
							{
								reminderId
							});
							return false;
						}
						modernReminderState.ScheduledReminderTime = reminderTime;
						return true;
					}
				}
			}
			this.Log.LogEntry(session, "ShouldScheduleEventTimeBasedInboxReminder - Adding state for reminder Reminder Id:{0}", new object[]
			{
				reminderId
			});
			ModernReminderState modernReminderState2 = new ModernReminderState();
			modernReminderState2.Identifier = reminderId;
			modernReminderState2.ScheduledReminderTime = reminderTime;
			if (remindersState == null)
			{
				remindersState = new RemindersState<ModernReminderState>();
			}
			remindersState.StateList.Add(modernReminderState2);
			return true;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0007CEBC File Offset: 0x0007B0BC
		public void ReceivedReminderMessage(IMailboxSession session, IStoreObject item)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ReminderMessageManager.ReceivedReminderMessage");
			if (item != null && item is ReminderMessage)
			{
				ReminderMessage reminderMessage = (ReminderMessage)item;
				item.Load(ReminderMessageManager.ReminderPropertiesToLoad);
				Guid reminderId = reminderMessage.ReminderId;
				GlobalObjectId reminderItemGlobalObjectId = reminderMessage.ReminderItemGlobalObjectId;
				ExDateTime receivedTime = reminderMessage.ReceivedTime;
				string className = item.ClassName;
				this.Log.LogEntry(session, "ReminderMessageManager.ReceivedReminderMessage - reminder message received, Reminder:{0}, Reminder received time:{1}", new object[]
				{
					reminderId,
					receivedTime.UniversalTime
				});
				Dictionary<string, object> dictionary = new Dictionary<string, object>(1);
				dictionary.Add("RRT", receivedTime.UniversalTime.ToString("o"));
				dictionary.Add("MID", session.MailboxGuid);
				ReminderLogger.Instance.LogEvent(new ReminderLogEvent(ReminderLogEventType.Receive, className, reminderItemGlobalObjectId, reminderId, dictionary));
				CalendarItemBase cachedCorrelatedItem = reminderMessage.GetCachedCorrelatedItem();
				if (cachedCorrelatedItem is ICalendarItem)
				{
					this.ScheduleNextOccurrenceReminder(session, (ICalendarItem)cachedCorrelatedItem, reminderMessage.ReminderId);
				}
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0007CFDC File Offset: 0x0007B1DC
		public virtual int ResubmitReminderMessages(IMailboxSession session)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ExTraceGlobals.GeneralTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "ReminderMessageManager.ResubmitReminderMessages for mailbox '{0}'", session.MailboxOwner);
			MailboxSession mailboxSession = session as MailboxSession;
			int num = 0;
			Folder folder = null;
			if (this.TryOpenFolder(mailboxSession, "Reminders Staging", out folder))
			{
				using (folder)
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, ReminderMessageManager.ReminderPropertiesToLoad))
					{
						object[][] rows = queryResult.GetRows(10);
						while (rows != null && rows.Length > 0)
						{
							foreach (object[] propertyArray in rows)
							{
								string property = this.GetProperty<string>(propertyArray, 0, string.Empty);
								VersionedId property2 = this.GetProperty<VersionedId>(propertyArray, 1, null);
								if (ObjectClass.IsReminderMessage(property))
								{
									this.ResubmitReminderMessage(mailboxSession, property2);
									num++;
								}
							}
							rows = queryResult.GetRows(10);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0007D0E4 File Offset: 0x0007B2E4
		private void ResubmitReminderMessage(MailboxSession mbxSession, VersionedId reminderMessageItemId)
		{
			using (MessageItem messageItem = MessageItem.Bind(mbxSession, reminderMessageItemId))
			{
				this.Log.LogEntry(mbxSession, "ReminderMessageManager.ResubmitReminderMessage - Resubmitting reminder message", new object[0]);
				messageItem.SendWithoutSavingMessage();
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0007D134 File Offset: 0x0007B334
		public virtual Dictionary<Guid, VersionedId> GetReminderMessages(IMailboxSession session, GlobalObjectId reminderItemGlobalObjectId, string objectClass)
		{
			return this.GetReminderItems(session, reminderItemGlobalObjectId, objectClass, "Reminders Staging");
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0007D144 File Offset: 0x0007B344
		private Dictionary<Guid, VersionedId> GetReminderItems(IMailboxSession session, GlobalObjectId reminderItemGlobalObjectId, string reminderObjectClass, string folderName)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("reminderItemGlobalObjectId", reminderItemGlobalObjectId);
			ArgumentValidator.ThrowIfNullOrEmpty("reminderObjectClass", reminderObjectClass);
			ExTraceGlobals.GeneralTracer.TraceDebug<string>((long)this.GetHashCode(), "ReminderMessageManager.GetReminderMessages for reminder object class '{0}'", reminderObjectClass);
			MailboxSession session2 = session as MailboxSession;
			Dictionary<Guid, VersionedId> dictionary = new Dictionary<Guid, VersionedId>();
			Folder folder = null;
			if (this.TryOpenFolder(session2, folderName, out folder))
			{
				using (folder)
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, ReminderMessageManager.SortByReminderItemGlobalObjectId, ReminderMessageManager.ReminderPropertiesToLoad))
					{
						QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ReminderMessageSchema.ReminderItemGlobalObjectId, reminderItemGlobalObjectId.Bytes);
						queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
						object[][] rows = queryResult.GetRows(10);
						while (rows != null && rows.Length > 0)
						{
							foreach (object[] propertyArray in rows)
							{
								byte[] property = this.GetProperty<byte[]>(propertyArray, 3, null);
								if (property == null || !property.SequenceEqual(reminderItemGlobalObjectId.Bytes))
								{
									break;
								}
								string property2 = this.GetProperty<string>(propertyArray, 0, null);
								if (property2 == reminderObjectClass)
								{
									Guid property3 = this.GetProperty<Guid>(propertyArray, 2, Guid.Empty);
									VersionedId property4 = this.GetProperty<VersionedId>(propertyArray, 1, null);
									if (property3 != Guid.Empty && property4 != null)
									{
										dictionary.Add(property3, property4);
									}
								}
							}
							rows = queryResult.GetRows(10);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0007D2C4 File Offset: 0x0007B4C4
		protected virtual void DeleteReminderMessage(IMailboxSession session, GlobalObjectId reminderItemGlobalObjectId, string reminderObjectClass, Guid reminderId, VersionedId reminderMessageItemId)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("reminderItemGlobalObjectId", reminderItemGlobalObjectId);
			ArgumentValidator.ThrowIfNullOrEmpty("reminderObjectClass", reminderObjectClass);
			ArgumentValidator.ThrowIfNull("reminderMessageItemId", reminderMessageItemId);
			MailboxSession session2 = session as MailboxSession;
			this.Log.LogEntry(session, "ReminderMessageManager.DeleteReminderMessage - Deleting reminder message, Reminder:{0}, Reminder Object Class:{1}", new object[]
			{
				reminderId,
				reminderObjectClass
			});
			try
			{
				using (ReminderMessage reminderMessage = ReminderMessage.Bind(session2, reminderMessageItemId, ReminderMessageManager.ReminderPropertiesToLoad))
				{
					ExAssert.RetailAssert(reminderMessage.ReminderItemGlobalObjectId.Equals(reminderItemGlobalObjectId), "DeleteReminderMessage is trying to delete a message that does not have the matching reminder item global object id");
					ExAssert.RetailAssert(reminderMessage.ClassName == reminderObjectClass, "DeleteReminderMessage is trying to delete a message that is not a reminder message, reminderMessage.ClassName='{0}', reminderObjectClass='{1}'", new object[]
					{
						reminderMessage.ClassName,
						reminderObjectClass
					});
					ExAssert.RetailAssert(reminderMessage.ReminderId == reminderId, "DeleteReminderMessage is trying to delete a message that is not related to the reminder, reminderMessage.ReminderId='{0}', reminderId='{1}'", new object[]
					{
						reminderMessage.ReminderId,
						reminderId
					});
					reminderMessage.AbortSubmit();
				}
				session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					reminderMessageItemId
				});
				Dictionary<string, object> dictionary = new Dictionary<string, object>(1);
				dictionary.Add("MID", session.MailboxGuid);
				ReminderLogger.Instance.LogEvent(new ReminderLogEvent(ReminderLogEventType.Remove, reminderObjectClass, reminderItemGlobalObjectId, reminderId, dictionary));
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ReminderMessageManager.DeleteReminderMessage - Reminder already deleted for reminder '{0}', reminder object class '{1}'", reminderId, reminderObjectClass);
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0007D450 File Offset: 0x0007B650
		protected virtual void LoadRegionalConfiguration(IMailboxSession session, out CultureInfo culture, out ExTimeZoneValue timeZone, out string dateFormat, out string timeFormat)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			culture = ReminderMessageManager.DefaultCulture;
			timeZone = ReminderMessageManager.DefaultTimeZone;
			dateFormat = MailboxRegionalConfiguration.GetDefaultDateFormat(culture);
			timeFormat = MailboxRegionalConfiguration.GetDefaultTimeFormat(culture);
			ADRecipient adrecipient = DirectoryHelper.ReadADRecipient(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.IsArchive, mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(adrecipient as ADUser);
			mailboxStoreTypeProvider.MailboxSession = mailboxSession;
			MailboxRegionalConfiguration mailboxRegionalConfiguration = null;
			try
			{
				mailboxRegionalConfiguration = (MailboxRegionalConfiguration)mailboxStoreTypeProvider.Read<MailboxRegionalConfiguration>(mailboxSession.MailboxOwner.ObjectId);
			}
			catch (FormatException arg)
			{
				ExTraceGlobals.GeneralTracer.TraceError<IExchangePrincipal, FormatException>((long)this.GetHashCode(), "User '{0}' doesn't have a valid regional configuration - {1}", mailboxSession.MailboxOwner, arg);
			}
			if (mailboxRegionalConfiguration != null)
			{
				if (mailboxRegionalConfiguration.Language != null)
				{
					culture = mailboxRegionalConfiguration.Language;
				}
				if (mailboxRegionalConfiguration.TimeZone != null)
				{
					timeZone = mailboxRegionalConfiguration.TimeZone;
				}
				if (!string.IsNullOrEmpty(mailboxRegionalConfiguration.DateFormat))
				{
					dateFormat = mailboxRegionalConfiguration.DateFormat;
				}
				if (!string.IsNullOrEmpty(mailboxRegionalConfiguration.TimeFormat))
				{
					timeFormat = mailboxRegionalConfiguration.TimeFormat;
				}
			}
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "LoadRegionalConfiguration - Culture={0}, TimeZone={1}, DateFormat='{2}', TimeFormat='{3}'", new object[]
			{
				culture,
				timeZone,
				dateFormat,
				timeFormat
			});
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0007D5A4 File Offset: 0x0007B7A4
		private T GetProperty<T>(object[] propertyArray, int propertyIndex, T defaultValue)
		{
			object obj = propertyArray[propertyIndex];
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0007D5C8 File Offset: 0x0007B7C8
		internal bool RemoveStaleRemindersState<T1>(HashSet<Guid> reminderIdentifiers, List<T1> stateList) where T1 : IReminderState
		{
			bool result = false;
			List<T1> list = new List<T1>();
			foreach (T1 item in stateList)
			{
				if (!reminderIdentifiers.Contains(item.Identifier))
				{
					list.Add(item);
				}
			}
			foreach (T1 item2 in list)
			{
				stateList.Remove(item2);
				result = true;
			}
			return result;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0007D678 File Offset: 0x0007B878
		private Folder OpenOrCreateFolder(MailboxSession session, string folderName)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ExTraceGlobals.GeneralTracer.TraceDebug<IExchangePrincipal>(0L, "Attempting to open or create Reminders Staging folder for {0}", session.MailboxOwner);
			Folder result;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.Configuration))
			{
				Folder folder2 = null;
				bool flag = false;
				try
				{
					folder2 = Folder.Create(session, folder.Id, StoreObjectType.Folder, folderName, CreateMode.OpenIfExists);
					folder2.Save();
					folder2.Load(new PropertyDefinition[]
					{
						ItemSchema.Id,
						StoreObjectSchema.DisplayName
					});
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Reminders Staging folder opened successfully");
					flag = true;
				}
				finally
				{
					if (folder2 != null && !flag)
					{
						folder2.Dispose();
						folder2 = null;
					}
				}
				result = folder2;
			}
			return result;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0007D73C File Offset: 0x0007B93C
		private bool TryOpenFolder(MailboxSession session, string folderName, out Folder folder)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			folder = null;
			using (Folder folder2 = Folder.Bind(session, DefaultFolderType.Configuration))
			{
				StoreObjectId storeObjectId = QueryChildFolderByName.Query(folder2, folderName);
				if (storeObjectId != null)
				{
					folder = Folder.Bind(session, storeObjectId);
				}
			}
			return folder != null;
		}

		// Token: 0x04000D03 RID: 3331
		private const string StagingFolderName = "Reminders Staging";

		// Token: 0x04000D04 RID: 3332
		private const int QueryBatchSize = 10;

		// Token: 0x04000D05 RID: 3333
		private const int MaxExceptionsToOpen = 10;

		// Token: 0x04000D06 RID: 3334
		private static CultureInfo DefaultCulture = new CultureInfo("en-US");

		// Token: 0x04000D07 RID: 3335
		private static ExTimeZoneValue DefaultTimeZone = new ExTimeZoneValue(ExTimeZone.UtcTimeZone);

		// Token: 0x04000D08 RID: 3336
		private readonly IReminderTimeCalculatorFactory reminderTimeCalculatorFactory;

		// Token: 0x04000D09 RID: 3337
		private static StorePropertyDefinition[] ReminderPropertiesToLoad = new StorePropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			ItemSchema.Id,
			ReminderMessageSchema.ReminderId,
			ReminderMessageSchema.ReminderItemGlobalObjectId
		};

		// Token: 0x04000D0A RID: 3338
		private static SortBy[] SortByReminderItemGlobalObjectId = new SortBy[]
		{
			new SortBy(ReminderMessageSchema.ReminderItemGlobalObjectId, SortOrder.Ascending)
		};

		// Token: 0x04000D0B RID: 3339
		private static PropertyTagPropertyDefinition DeferredSendTimeDefinition = PropertyTagPropertyDefinition.CreateCustom("DeferredSendTime", 1072627776U);

		// Token: 0x0200024F RID: 591
		private enum ReminderProperties
		{
			// Token: 0x04000D0E RID: 3342
			ItemClass,
			// Token: 0x04000D0F RID: 3343
			Id,
			// Token: 0x04000D10 RID: 3344
			ReminderId,
			// Token: 0x04000D11 RID: 3345
			ReminderItemGlobalObjectId
		}
	}
}
