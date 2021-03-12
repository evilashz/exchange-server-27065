using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000054 RID: 84
	internal class CalendarNavigator
	{
		// Token: 0x06000356 RID: 854 RVA: 0x000103C4 File Offset: 0x0000E5C4
		internal CalendarNavigator(UMSubscriber user)
		{
			this.user = user;
			this.Today();
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000103D9 File Offset: 0x0000E5D9
		internal ExDateTime CurrentDay
		{
			get
			{
				return this.currentDay;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000103E1 File Offset: 0x0000E5E1
		internal UMSubscriber Owner
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000103EC File Offset: 0x0000E5EC
		internal ArrayList CurrentAgenda
		{
			get
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Getting view between {0} and {1}.", new object[]
				{
					this.currentDay,
					this.currentDay.AddDays(1.0).AddMinutes(-1.0)
				});
				ArrayList arrayList = new ArrayList();
				ArrayList result;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Calendar)))
					{
						object[][] calendarView = calendarFolder.GetCalendarView(this.currentDay, this.currentDay.AddDays(1.0).AddMinutes(-1.0), new PropertyDefinition[]
						{
							ItemSchema.Id,
							CalendarItemInstanceSchema.StartTime,
							CalendarItemInstanceSchema.EndTime,
							CalendarItemBaseSchema.IsAllDayEvent,
							CalendarItemBaseSchema.AppointmentState
						});
						CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Found {0} calendar items in this view.", new object[]
						{
							calendarView.Length
						});
						for (int i = 0; i < calendarView.Length; i++)
						{
							if (this.IsValidMeeting(calendarView[i][0], calendarView[i][1], calendarView[i][2], calendarView[i][4], this.currentDay))
							{
								arrayList.Add(new CalendarNavigator.MeetingInfo(calendarView[i], this.user));
							}
						}
						arrayList.Sort();
						result = arrayList;
					}
				}
				return result;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000105B0 File Offset: 0x0000E7B0
		internal void Next()
		{
			this.currentDay = this.currentDay.AddDays(1.0);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000105CC File Offset: 0x0000E7CC
		internal void Previous()
		{
			this.currentDay = this.currentDay.AddDays(-1.0);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000105E8 File Offset: 0x0000E7E8
		internal void Goto(ExDateTime target)
		{
			this.currentDay = target;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000105F4 File Offset: 0x0000E7F4
		internal bool SeekNext()
		{
			ExDateTime date = this.currentDay.AddDays(1.0).Date;
			ExDateTime date2 = date.AddDays(8.0).AddMinutes(-1.0).Date;
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Calendar navigator seeking next meeting between {0} and {1}.", new object[]
			{
				date,
				date2
			});
			bool result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Calendar)))
				{
					object[][] calendarView = calendarFolder.GetCalendarView(date, date2, new PropertyDefinition[]
					{
						ItemSchema.Id,
						CalendarItemInstanceSchema.StartTime,
						CalendarItemInstanceSchema.EndTime,
						CalendarItemBaseSchema.AppointmentState
					});
					ExDateTime exDateTime = ExDateTime.MaxValue;
					for (int i = 0; i < calendarView.Length; i++)
					{
						ExDateTime t = (ExDateTime)calendarView[i][1];
						if (t.Date > this.currentDay && t < exDateTime && this.IsValidMeeting(calendarView[i][0], calendarView[i][1], calendarView[i][2], calendarView[i][3], t.Date))
						{
							exDateTime = t.Date;
						}
					}
					if (exDateTime < ExDateTime.MaxValue)
					{
						this.currentDay = exDateTime;
						CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Next meeting found is {0}.", new object[]
						{
							exDateTime
						});
						result = true;
					}
					else
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "No meetings found for {0} days, starting, {1}.", new object[]
						{
							7,
							this.currentDay
						});
						this.currentDay = this.currentDay.AddDays(7.0).Date;
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00010838 File Offset: 0x0000EA38
		internal void Today()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Resetting calendar view to Today.", new object[0]);
			this.currentDay = this.user.Now.Date;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00010874 File Offset: 0x0000EA74
		internal void SkipMeeting(StoreObjectId objectId)
		{
			if (this.skipList == null)
			{
				this.skipList = new Dictionary<StoreObjectId, bool>();
			}
			this.skipList[objectId] = true;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00010896 File Offset: 0x0000EA96
		private bool IsInSkipList(StoreObjectId objectId)
		{
			return this.skipList != null && this.skipList.ContainsKey(objectId);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000108B0 File Offset: 0x0000EAB0
		private bool IsValidMeeting(object itemStoreId, object itemStartTime, object itemEndTime, object itemAppointmentState, ExDateTime agendaDate)
		{
			if (!(itemStoreId is StoreId) || !(itemStartTime is ExDateTime) || !(itemEndTime is ExDateTime))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Calendar Item doesn't have a valid ID or start or end time... ignoring it.", new object[0]);
				return false;
			}
			if (this.IsInSkipList(((VersionedId)itemStoreId).ObjectId))
			{
				return false;
			}
			int num = (itemAppointmentState is int) ? ((int)itemAppointmentState) : 0;
			return (num & 4) == 0 && (ExDateTime)itemEndTime > agendaDate;
		}

		// Token: 0x0400010F RID: 271
		internal const int MaxSearchWindowDays = 7;

		// Token: 0x04000110 RID: 272
		private ExDateTime currentDay;

		// Token: 0x04000111 RID: 273
		private UMSubscriber user;

		// Token: 0x04000112 RID: 274
		private Dictionary<StoreObjectId, bool> skipList;

		// Token: 0x02000055 RID: 85
		internal class AttendeeInfo
		{
			// Token: 0x06000362 RID: 866 RVA: 0x0001092C File Offset: 0x0000EB2C
			internal AttendeeInfo(Attendee attendee)
			{
				this.attendeeType = attendee.AttendeeType;
				this.responseType = attendee.ResponseType;
				this.participant = attendee.Participant;
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x06000363 RID: 867 RVA: 0x00010958 File Offset: 0x0000EB58
			internal AttendeeType AttendeeType
			{
				get
				{
					return this.attendeeType;
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000364 RID: 868 RVA: 0x00010960 File Offset: 0x0000EB60
			internal ResponseType ResponseType
			{
				get
				{
					return this.responseType;
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000365 RID: 869 RVA: 0x00010968 File Offset: 0x0000EB68
			internal Participant Participant
			{
				get
				{
					return this.participant;
				}
			}

			// Token: 0x04000113 RID: 275
			private AttendeeType attendeeType;

			// Token: 0x04000114 RID: 276
			private ResponseType responseType;

			// Token: 0x04000115 RID: 277
			private Participant participant;
		}

		// Token: 0x02000056 RID: 86
		internal class MeetingInfo : IComparable
		{
			// Token: 0x06000366 RID: 870 RVA: 0x00010970 File Offset: 0x0000EB70
			internal MeetingInfo(object[] meetingProps, UMSubscriber user)
			{
				this.id = ((VersionedId)meetingProps[0]).ObjectId;
				this.startTime = (ExDateTime)meetingProps[1];
				this.endTime = (ExDateTime)meetingProps[2];
				this.isAllDayEvent = (meetingProps[3] is bool && (bool)meetingProps[3]);
				this.user = user;
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000367 RID: 871 RVA: 0x000109D4 File Offset: 0x0000EBD4
			internal StoreObjectId UniqueId
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000368 RID: 872 RVA: 0x000109DC File Offset: 0x0000EBDC
			internal bool IsOrganizer
			{
				get
				{
					return this.Cache.IsOrganizer;
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000369 RID: 873 RVA: 0x000109E9 File Offset: 0x0000EBE9
			internal string Subject
			{
				get
				{
					return this.Cache.Subject;
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600036A RID: 874 RVA: 0x000109F6 File Offset: 0x0000EBF6
			internal string Location
			{
				get
				{
					return this.Cache.Location;
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600036B RID: 875 RVA: 0x00010A03 File Offset: 0x0000EC03
			internal string OrganizerEmail
			{
				get
				{
					return this.Cache.OrganizerEmail;
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600036C RID: 876 RVA: 0x00010A10 File Offset: 0x0000EC10
			internal string OrganizerName
			{
				get
				{
					return this.Cache.OrganizerName;
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600036D RID: 877 RVA: 0x00010A1D File Offset: 0x0000EC1D
			internal ExDateTime StartTime
			{
				get
				{
					return this.startTime;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600036E RID: 878 RVA: 0x00010A25 File Offset: 0x0000EC25
			internal ExDateTime EndTime
			{
				get
				{
					return this.endTime;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x0600036F RID: 879 RVA: 0x00010A2D File Offset: 0x0000EC2D
			internal BusyType FreeBusyStatus
			{
				get
				{
					return this.Cache.ClassicFreeBusyStatus;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000370 RID: 880 RVA: 0x00010A3A File Offset: 0x0000EC3A
			internal bool IsCancelled
			{
				get
				{
					return this.Cache.IsCancelled;
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000371 RID: 881 RVA: 0x00010A47 File Offset: 0x0000EC47
			internal bool IsMeeting
			{
				get
				{
					return this.Cache.IsMeeting;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000372 RID: 882 RVA: 0x00010A54 File Offset: 0x0000EC54
			internal bool IsAllDayEvent
			{
				get
				{
					return this.isAllDayEvent;
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000373 RID: 883 RVA: 0x00010A5C File Offset: 0x0000EC5C
			internal List<CalendarNavigator.AttendeeInfo> Attendees
			{
				get
				{
					return this.Cache.Attendees;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000374 RID: 884 RVA: 0x00010A69 File Offset: 0x0000EC69
			internal PhoneNumber OrganizerPhone
			{
				get
				{
					if (this.Cache.OrganizerPhone == null)
					{
						this.Cache.OrganizerPhone = this.BuildOrganizerPhone();
					}
					return this.Cache.OrganizerPhone;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000375 RID: 885 RVA: 0x00010A94 File Offset: 0x0000EC94
			internal PhoneNumber LocationPhone
			{
				get
				{
					if (this.Cache.LocationPhone == null)
					{
						this.Cache.LocationPhone = this.BuildLocationPhone();
					}
					return this.Cache.LocationPhone;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000376 RID: 886 RVA: 0x00010AC0 File Offset: 0x0000ECC0
			private CalendarNavigator.MeetingInfo.CachedData Cache
			{
				get
				{
					if (this.cache == null)
					{
						PIIMessage data = PIIMessage.Create(PIIType._User, this.user);
						CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, data, "Getting store item for User=_User, MeetingInfo={0}.", new object[]
						{
							this.user,
							this.id.ToBase64String()
						});
						using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
						{
							using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(mailboxSessionLock.Session, this.id))
							{
								this.cache = new CalendarNavigator.MeetingInfo.CachedData(calendarItemBase);
							}
						}
					}
					return this.cache;
				}
			}

			// Token: 0x06000377 RID: 887 RVA: 0x00010B78 File Offset: 0x0000ED78
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "Id={0}, Start={1}, End={2}, User={3}", new object[]
				{
					this.id.ToBase64String(),
					this.StartTime,
					this.EndTime,
					this.user.ExchangeLegacyDN
				});
			}

			// Token: 0x06000378 RID: 888 RVA: 0x00010BD4 File Offset: 0x0000EDD4
			public int CompareTo(object obj)
			{
				CalendarNavigator.MeetingInfo meetingInfo = (CalendarNavigator.MeetingInfo)obj;
				if (this.startTime < meetingInfo.startTime)
				{
					return -1;
				}
				if (!(this.startTime == meetingInfo.startTime))
				{
					return 1;
				}
				if (this.endTime < meetingInfo.endTime)
				{
					return -1;
				}
				if (this.endTime == meetingInfo.endTime)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000379 RID: 889 RVA: 0x00010C40 File Offset: 0x0000EE40
			internal void AcceptMeeting()
			{
				this.cache = null;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(mailboxSessionLock.Session, this.UniqueId))
					{
						calendarItemBase.OpenAsReadWrite();
						using (MessageItem messageItem = XsoUtil.RespondToMeetingRequest(calendarItemBase, ResponseType.Accept))
						{
							messageItem[MessageItemSchema.VoiceMessageDuration] = 0;
							XsoUtil.SetSubscriberAccessSenderProperties(messageItem, this.user);
							messageItem.Send();
						}
						calendarItemBase.Load();
						this.id = calendarItemBase.Id.ObjectId;
						this.cache = new CalendarNavigator.MeetingInfo.CachedData(calendarItemBase);
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "MeetingInfo::AcceptMeeting successfully built response.", new object[0]);
			}

			// Token: 0x0600037A RID: 890 RVA: 0x00010D28 File Offset: 0x0000EF28
			internal void MarkAsTentative()
			{
				this.cache = null;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(mailboxSessionLock.Session, this.UniqueId))
					{
						calendarItemBase.OpenAsReadWrite();
						using (MessageItem messageItem = XsoUtil.RespondToMeetingRequest(calendarItemBase, ResponseType.Tentative))
						{
							messageItem[MessageItemSchema.VoiceMessageDuration] = 0;
							XsoUtil.SetSubscriberAccessSenderProperties(messageItem, this.user);
							messageItem.Send();
							calendarItemBase.Load();
							this.id = calendarItemBase.Id.ObjectId;
							this.cache = new CalendarNavigator.MeetingInfo.CachedData(calendarItemBase);
						}
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "MeetingInfo::MarkAsTentative successfully built response.", new object[0]);
			}

			// Token: 0x0600037B RID: 891 RVA: 0x00010E10 File Offset: 0x0000F010
			private PhoneNumber BuildLocationPhone()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Looking for location phone for location={0}.", new object[]
				{
					this.Location
				});
				Participant participant = null;
				foreach (CalendarNavigator.AttendeeInfo attendeeInfo in this.Cache.Attendees)
				{
					if (!string.IsNullOrEmpty(attendeeInfo.Participant.DisplayName) && string.Compare(attendeeInfo.Participant.DisplayName, this.Location, StringComparison.OrdinalIgnoreCase) == 0)
					{
						participant = attendeeInfo.Participant;
						break;
					}
				}
				PhoneNumber phoneNumber = null;
				if (null != participant)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Found location participant={0}.", new object[]
					{
						participant
					});
					ContactInfo contactInfo = ContactInfo.FindByParticipant(this.user, participant);
					if (contactInfo != null)
					{
						phoneNumber = Util.GetNumberToDial(this.user, contactInfo);
					}
				}
				PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, data, "Returning location phone=_PhoneNumber.", new object[0]);
				return phoneNumber;
			}

			// Token: 0x0600037C RID: 892 RVA: 0x00010F28 File Offset: 0x0000F128
			private PhoneNumber BuildOrganizerPhone()
			{
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, this.OrganizerEmail);
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, data, "Looking for organizerPhone for organizer=_EmailAddress.", new object[0]);
				PhoneNumber phoneNumber = null;
				if (null != this.Cache.Organizer)
				{
					ContactInfo contactInfo = ContactInfo.FindByParticipant(this.user, this.Cache.Organizer);
					if (contactInfo != null)
					{
						phoneNumber = Util.GetNumberToDial(this.user, contactInfo);
					}
				}
				PIIMessage data2 = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, data2, "Returning organizer phone=_PhoneNumber.", new object[0]);
				return phoneNumber;
			}

			// Token: 0x04000116 RID: 278
			private StoreObjectId id;

			// Token: 0x04000117 RID: 279
			private ExDateTime startTime;

			// Token: 0x04000118 RID: 280
			private ExDateTime endTime;

			// Token: 0x04000119 RID: 281
			private bool isAllDayEvent;

			// Token: 0x0400011A RID: 282
			private UMSubscriber user;

			// Token: 0x0400011B RID: 283
			private CalendarNavigator.MeetingInfo.CachedData cache;

			// Token: 0x02000057 RID: 87
			private class CachedData
			{
				// Token: 0x0600037D RID: 893 RVA: 0x00010FB8 File Offset: 0x0000F1B8
				internal CachedData(CalendarItemBase calendarItem)
				{
					calendarItem.Load(new PropertyDefinition[]
					{
						ItemSchema.Subject,
						CalendarItemBaseSchema.Location,
						CalendarItemBaseSchema.FreeBusyStatus,
						CalendarItemBaseSchema.IsMeeting,
						CalendarItemBaseSchema.OrganizerEmailAddress,
						CalendarItemBaseSchema.IsAllDayEvent,
						ItemSchema.SentRepresentingDisplayName
					});
					this.subject = calendarItem.Subject;
					this.location = calendarItem.Location;
					this.classicFreeBusyStatus = calendarItem.FreeBusyStatus;
					this.isCancelled = calendarItem.IsCancelled;
					this.isMeeting = calendarItem.IsMeeting;
					this.organizer = calendarItem.Organizer;
					this.organizerEmail = (string)XsoUtil.SafeGetProperty(calendarItem, CalendarItemBaseSchema.OrganizerEmailAddress, string.Empty);
					this.organizerName = (string)XsoUtil.SafeGetProperty(calendarItem, ItemSchema.SentRepresentingDisplayName, this.organizerEmail);
					this.isOrganizer = (calendarItem.IsOrganizer() || null == calendarItem.Organizer);
					this.attendees = new List<CalendarNavigator.AttendeeInfo>();
					foreach (Attendee attendee in calendarItem.AttendeeCollection)
					{
						if (AttendeeType.Resource != attendee.AttendeeType && string.Compare(attendee.Participant.EmailAddress, this.organizerEmail, true, CultureInfo.InvariantCulture) != 0)
						{
							this.attendees.Add(new CalendarNavigator.AttendeeInfo(attendee));
						}
					}
				}

				// Token: 0x170000C6 RID: 198
				// (get) Token: 0x0600037E RID: 894 RVA: 0x0001112C File Offset: 0x0000F32C
				internal string Subject
				{
					get
					{
						return this.subject;
					}
				}

				// Token: 0x170000C7 RID: 199
				// (get) Token: 0x0600037F RID: 895 RVA: 0x00011134 File Offset: 0x0000F334
				internal bool IsOrganizer
				{
					get
					{
						return this.isOrganizer;
					}
				}

				// Token: 0x170000C8 RID: 200
				// (get) Token: 0x06000380 RID: 896 RVA: 0x0001113C File Offset: 0x0000F33C
				internal string Location
				{
					get
					{
						return this.location;
					}
				}

				// Token: 0x170000C9 RID: 201
				// (get) Token: 0x06000381 RID: 897 RVA: 0x00011144 File Offset: 0x0000F344
				internal BusyType ClassicFreeBusyStatus
				{
					get
					{
						return this.classicFreeBusyStatus;
					}
				}

				// Token: 0x170000CA RID: 202
				// (get) Token: 0x06000382 RID: 898 RVA: 0x0001114C File Offset: 0x0000F34C
				internal bool IsCancelled
				{
					get
					{
						return this.isCancelled;
					}
				}

				// Token: 0x170000CB RID: 203
				// (get) Token: 0x06000383 RID: 899 RVA: 0x00011154 File Offset: 0x0000F354
				internal bool IsMeeting
				{
					get
					{
						return this.isMeeting;
					}
				}

				// Token: 0x170000CC RID: 204
				// (get) Token: 0x06000384 RID: 900 RVA: 0x0001115C File Offset: 0x0000F35C
				internal string OrganizerEmail
				{
					get
					{
						return this.organizerEmail;
					}
				}

				// Token: 0x170000CD RID: 205
				// (get) Token: 0x06000385 RID: 901 RVA: 0x00011164 File Offset: 0x0000F364
				internal string OrganizerName
				{
					get
					{
						return this.organizerName;
					}
				}

				// Token: 0x170000CE RID: 206
				// (get) Token: 0x06000386 RID: 902 RVA: 0x0001116C File Offset: 0x0000F36C
				internal List<CalendarNavigator.AttendeeInfo> Attendees
				{
					get
					{
						return this.attendees;
					}
				}

				// Token: 0x170000CF RID: 207
				// (get) Token: 0x06000387 RID: 903 RVA: 0x00011174 File Offset: 0x0000F374
				// (set) Token: 0x06000388 RID: 904 RVA: 0x0001117C File Offset: 0x0000F37C
				internal PhoneNumber OrganizerPhone
				{
					get
					{
						return this.organizerPhone;
					}
					set
					{
						this.organizerPhone = value;
					}
				}

				// Token: 0x170000D0 RID: 208
				// (get) Token: 0x06000389 RID: 905 RVA: 0x00011185 File Offset: 0x0000F385
				internal Participant Organizer
				{
					get
					{
						return this.organizer;
					}
				}

				// Token: 0x170000D1 RID: 209
				// (get) Token: 0x0600038A RID: 906 RVA: 0x0001118D File Offset: 0x0000F38D
				// (set) Token: 0x0600038B RID: 907 RVA: 0x00011195 File Offset: 0x0000F395
				internal PhoneNumber LocationPhone
				{
					get
					{
						return this.locationPhone;
					}
					set
					{
						this.locationPhone = value;
					}
				}

				// Token: 0x0400011C RID: 284
				private string subject;

				// Token: 0x0400011D RID: 285
				private bool isOrganizer;

				// Token: 0x0400011E RID: 286
				private string location;

				// Token: 0x0400011F RID: 287
				private BusyType classicFreeBusyStatus;

				// Token: 0x04000120 RID: 288
				private bool isCancelled;

				// Token: 0x04000121 RID: 289
				private bool isMeeting;

				// Token: 0x04000122 RID: 290
				private string organizerEmail;

				// Token: 0x04000123 RID: 291
				private string organizerName;

				// Token: 0x04000124 RID: 292
				private List<CalendarNavigator.AttendeeInfo> attendees;

				// Token: 0x04000125 RID: 293
				private PhoneNumber organizerPhone;

				// Token: 0x04000126 RID: 294
				private Participant organizer;

				// Token: 0x04000127 RID: 295
				private PhoneNumber locationPhone;
			}
		}

		// Token: 0x02000058 RID: 88
		internal class AgendaContext
		{
			// Token: 0x0600038C RID: 908 RVA: 0x0001119E File Offset: 0x0000F39E
			internal AgendaContext(ArrayList agenda, UMSubscriber user, bool isInitialPosition, bool isReadConflicts)
			{
				this.agenda = agenda;
				this.conflicts = new ArrayList();
				this.isInitialPosition = isInitialPosition;
				this.isReadConflicts = isReadConflicts;
				this.user = user;
				this.isOnlyReadRemaining = isInitialPosition;
				this.SeekBest();
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x0600038D RID: 909 RVA: 0x000111DC File Offset: 0x0000F3DC
			internal int ConflictCount
			{
				get
				{
					return this.conflicts.Count;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x0600038E RID: 910 RVA: 0x000111EC File Offset: 0x0000F3EC
			internal int Remaining
			{
				get
				{
					int num = 0;
					for (int i = 0; i < this.agenda.Count; i++)
					{
						if (!this.IsOver((CalendarNavigator.MeetingInfo)this.agenda[i]))
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600038F RID: 911 RVA: 0x0001122F File Offset: 0x0000F42F
			internal int TotalCount
			{
				get
				{
					return this.agenda.Count;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000390 RID: 912 RVA: 0x0001123C File Offset: 0x0000F43C
			internal CalendarNavigator.MeetingInfo Current
			{
				get
				{
					return (CalendarNavigator.MeetingInfo)this.agenda[this.idx];
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000391 RID: 913 RVA: 0x00011254 File Offset: 0x0000F454
			internal CalendarNavigator.MeetingInfo CurrentConflict
			{
				get
				{
					return (CalendarNavigator.MeetingInfo)this.conflicts[this.conflictIdx];
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000392 RID: 914 RVA: 0x0001126C File Offset: 0x0000F46C
			internal ExDateTime ConflictTime
			{
				get
				{
					return this.conflictTime;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000393 RID: 915 RVA: 0x00011274 File Offset: 0x0000F474
			internal bool IsFirst
			{
				get
				{
					return 0 == this.idx;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000394 RID: 916 RVA: 0x0001127F File Offset: 0x0000F47F
			internal bool IsFirstConflict
			{
				get
				{
					return this.ConflictCount > 0 && 0 == this.conflictIdx;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000395 RID: 917 RVA: 0x00011295 File Offset: 0x0000F495
			internal bool IsLast
			{
				get
				{
					return this.agenda.Count == this.idx + 1;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000396 RID: 918 RVA: 0x000112AC File Offset: 0x0000F4AC
			internal bool IsInitialPosition
			{
				get
				{
					return this.isInitialPosition;
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000397 RID: 919 RVA: 0x000112B4 File Offset: 0x0000F4B4
			internal bool IsValid
			{
				get
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "AgendaContext::IsValid. agenda={0}, idx={1}.", new object[]
					{
						this.agenda,
						this.idx
					});
					return this.agenda != null && this.idx >= 0 && this.idx < this.agenda.Count;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000398 RID: 920 RVA: 0x00011318 File Offset: 0x0000F518
			internal bool ConflictsWithLastHeard
			{
				get
				{
					return this.lastMeetingHeard != null && this.IsValid && this.lastMeetingHeard != this.Current && ((this.lastMeetingHeard.StartTime >= this.Current.StartTime && this.lastMeetingHeard.StartTime < this.Current.EndTime) || (this.Current.StartTime >= this.lastMeetingHeard.StartTime && this.Current.StartTime < this.lastMeetingHeard.EndTime));
				}
			}

			// Token: 0x06000399 RID: 921 RVA: 0x000113BD File Offset: 0x0000F5BD
			internal bool Next()
			{
				return this.Next(false);
			}

			// Token: 0x0600039A RID: 922 RVA: 0x000113C8 File Offset: 0x0000F5C8
			internal bool Next(bool keepLastInContext)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "AgendaContext.Next().", new object[0]);
				this.lastMeetingHeard = (this.IsValid ? this.Current : null);
				if (this.IsValid)
				{
					this.isInitialPosition = false;
				}
				if (this.isReadConflicts && 0 < this.ConflictCount && ++this.conflictIdx < this.ConflictCount)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Still in conflict.", new object[0]);
					return true;
				}
				if (keepLastInContext && !this.HasNext())
				{
					return false;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Seeking next meeting of the day.", new object[0]);
				this.SeekNext();
				this.SetConflictContext();
				return this.idx < this.agenda.Count;
			}

			// Token: 0x0600039B RID: 923 RVA: 0x00011498 File Offset: 0x0000F698
			internal bool Previous()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "AgendaContext.Previous().", new object[0]);
				this.lastMeetingHeard = (this.IsValid ? this.Current : null);
				if (this.IsValid && !this.Current.IsAllDayEvent)
				{
					this.isInitialPosition = false;
				}
				this.isOnlyReadRemaining = false;
				if (!this.HasPrevious())
				{
					return false;
				}
				if (this.isReadConflicts && 0 < this.ConflictCount && --this.conflictIdx >= 0)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Still in conflict.", new object[0]);
					return true;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Seeking previous.", new object[0]);
				this.SeekPrevious();
				this.SetConflictContext();
				return 0 <= this.idx;
			}

			// Token: 0x0600039C RID: 924 RVA: 0x0001156C File Offset: 0x0000F76C
			internal bool SeekFirst()
			{
				this.lastMeetingHeard = (this.IsValid ? this.Current : null);
				this.isInitialPosition = false;
				this.isOnlyReadRemaining = false;
				this.idx = 0;
				this.SetConflictContext();
				return this.idx < this.agenda.Count;
			}

			// Token: 0x0600039D RID: 925 RVA: 0x000115C0 File Offset: 0x0000F7C0
			internal bool SeekLast()
			{
				this.lastMeetingHeard = (this.IsValid ? this.Current : null);
				this.isInitialPosition = false;
				this.isOnlyReadRemaining = false;
				this.idx = this.agenda.Count;
				this.SeekPrevious();
				this.SetConflictContext();
				return this.IsValid;
			}

			// Token: 0x0600039E RID: 926 RVA: 0x00011618 File Offset: 0x0000F818
			internal void Remove(StoreObjectId theIdToRemove)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "AgendaContext::Remove.", new object[0]);
				for (int i = 0; i < this.conflicts.Count; i++)
				{
					CalendarNavigator.MeetingInfo meetingInfo = (CalendarNavigator.MeetingInfo)this.conflicts[i];
					if (meetingInfo.UniqueId == theIdToRemove)
					{
						this.conflicts.RemoveAt(i);
					}
				}
				int num = -1;
				for (int j = 0; j < this.agenda.Count; j++)
				{
					CalendarNavigator.MeetingInfo meetingInfo2 = (CalendarNavigator.MeetingInfo)this.agenda[j];
					if (meetingInfo2.UniqueId == theIdToRemove)
					{
						num = j;
					}
				}
				if (-1 == num)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "AgendaContext::Remove. Did not find meeting to remove in agenda!", new object[0]);
					return;
				}
				this.agenda.RemoveAt(num);
				if (num < this.idx)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Removed a meeting that's before the current one in the agenda.", new object[0]);
					this.idx--;
				}
				this.PostRemovalCleanup();
			}

			// Token: 0x0600039F RID: 927 RVA: 0x0001170C File Offset: 0x0000F90C
			internal void RemoveMeetings(IList<StoreObjectId> idsToRemove)
			{
				int i = 0;
				while (i < this.agenda.Count)
				{
					CalendarNavigator.MeetingInfo meetingInfo = (CalendarNavigator.MeetingInfo)this.agenda[i];
					if (-1 != idsToRemove.IndexOf(meetingInfo.UniqueId))
					{
						this.agenda.RemoveAt(i);
						if (i < this.idx)
						{
							this.idx--;
						}
					}
					else
					{
						i++;
					}
				}
				i = 0;
				while (i < this.conflicts.Count)
				{
					CalendarNavigator.MeetingInfo meetingInfo2 = (CalendarNavigator.MeetingInfo)this.conflicts[i];
					if (-1 != idsToRemove.IndexOf(meetingInfo2.UniqueId))
					{
						this.conflicts.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
				this.PostRemovalCleanup();
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x000117C0 File Offset: 0x0000F9C0
			private bool SeekBest()
			{
				int num = 0;
				CalendarNavigator.MeetingInfo bestSoFar = null;
				if (this.isInitialPosition)
				{
					if (this.isReadConflicts)
					{
						num = this.agenda.Count;
						for (int i = 0; i < this.agenda.Count; i++)
						{
							CalendarNavigator.MeetingInfo meetingInfo = (CalendarNavigator.MeetingInfo)this.agenda[i];
							if (this.IsBetter(meetingInfo, bestSoFar))
							{
								bestSoFar = meetingInfo;
								num = i;
							}
						}
					}
					else
					{
						num = this.agenda.Count;
						for (int j = 0; j < this.agenda.Count; j++)
						{
							CalendarNavigator.MeetingInfo mi = (CalendarNavigator.MeetingInfo)this.agenda[j];
							if (!this.IsOver(mi))
							{
								num = j;
								break;
							}
						}
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "SeekBest stops at meeting= {0}.", new object[]
				{
					num
				});
				this.idx = num;
				this.SetConflictContext();
				return this.idx < this.agenda.Count;
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x000118B8 File Offset: 0x0000FAB8
			private void PostRemovalCleanup()
			{
				if (1 < this.ConflictCount && this.conflictIdx < this.ConflictCount)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Still in conflict after removal of meeting.", new object[0]);
					return;
				}
				if (!this.IsValid)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Not on a valid meeting after removal of meeting(s).", new object[0]);
					return;
				}
				if (this.isOnlyReadRemaining && this.IsOver(this.Current))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Removed a meeting with fOnlyReadRemaining and next meeting is over.", new object[0]);
					this.Next();
					return;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "setting conflict context after meeting removal.", new object[0]);
				this.SetConflictContext();
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x00011962 File Offset: 0x0000FB62
			private bool HasPrevious()
			{
				if (this.ConflictCount > 0)
				{
					return this.conflictIdx > 0 || this.idx > 0;
				}
				return this.idx > 0;
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x0001198C File Offset: 0x0000FB8C
			private bool HasNext()
			{
				int num = this.idx;
				this.SeekNext();
				bool isValid = this.IsValid;
				this.idx = num;
				return isValid;
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x000119B8 File Offset: 0x0000FBB8
			private void SetConflictContext()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Setting conflict context.", new object[0]);
				this.conflicts.Clear();
				this.conflictIdx = 0;
				if (!this.IsValid)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "idx not valid in setconflictcontext: idx={0}. returning empty arraylist.", new object[]
					{
						this.idx
					});
					return;
				}
				if (!this.isReadConflicts)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "not reading conflicts in setconflictcontext.", new object[0]);
					return;
				}
				if (this.Current.IsAllDayEvent)
				{
					return;
				}
				this.SetConflictTime();
				for (int i = 0; i < this.agenda.Count; i++)
				{
					CalendarNavigator.MeetingInfo meetingInfo = (CalendarNavigator.MeetingInfo)this.agenda[i];
					if (!meetingInfo.IsAllDayEvent)
					{
						if (meetingInfo.StartTime > this.Current.StartTime)
						{
							break;
						}
						if (meetingInfo != this.Current && !(meetingInfo.EndTime <= this.Current.StartTime))
						{
							if (!this.isInitialPosition)
							{
								if (meetingInfo.StartTime == this.Current.StartTime)
								{
									this.conflicts.Add(meetingInfo);
								}
							}
							else if (this.InProgress(this.Current))
							{
								if (this.InProgress(meetingInfo))
								{
									this.conflicts.Add(meetingInfo);
								}
							}
							else if (meetingInfo.StartTime == this.Current.StartTime)
							{
								this.conflicts.Add(meetingInfo);
							}
						}
					}
				}
				if (this.conflicts.Count > 0)
				{
					this.conflicts.Add(this.Current);
					this.conflicts.Sort();
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Found {0} conflicts at {1}.", new object[]
				{
					this.conflicts.Count,
					this.ConflictTime
				});
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x00011BA5 File Offset: 0x0000FDA5
			private void SetConflictTime()
			{
				if (this.isInitialPosition && this.InProgress(this.Current))
				{
					this.conflictTime = this.user.Now;
					return;
				}
				this.conflictTime = this.Current.StartTime;
			}

			// Token: 0x060003A6 RID: 934 RVA: 0x00011BE0 File Offset: 0x0000FDE0
			private void SeekNext()
			{
				if (this.idx >= this.agenda.Count)
				{
					return;
				}
				ExDateTime startTime = ((CalendarNavigator.MeetingInfo)this.agenda[this.idx]).StartTime;
				while (++this.idx < this.agenda.Count)
				{
					CalendarNavigator.MeetingInfo meetingInfo = (CalendarNavigator.MeetingInfo)this.agenda[this.idx];
					if (this.isOnlyReadRemaining)
					{
						if (!this.IsOver(meetingInfo))
						{
							if (!this.isReadConflicts || meetingInfo.StartTime > startTime)
							{
								break;
							}
							if (meetingInfo.IsAllDayEvent)
							{
								break;
							}
						}
					}
					else if (!this.isReadConflicts || meetingInfo.StartTime > startTime || meetingInfo.IsAllDayEvent)
					{
						break;
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Next meeting of the day found is at index: {0}.", new object[]
				{
					this.idx
				});
			}

			// Token: 0x060003A7 RID: 935 RVA: 0x00011CCC File Offset: 0x0000FECC
			private void SeekPrevious()
			{
				if (this.idx < 1)
				{
					return;
				}
				while (--this.idx > 0)
				{
					CalendarNavigator.MeetingInfo meetingInfo = (CalendarNavigator.MeetingInfo)this.agenda[this.idx];
					CalendarNavigator.MeetingInfo meetingInfo2 = (CalendarNavigator.MeetingInfo)this.agenda[this.idx - 1];
					if (!this.isReadConflicts || meetingInfo.StartTime > meetingInfo2.StartTime)
					{
						break;
					}
					if (meetingInfo.IsAllDayEvent)
					{
						return;
					}
				}
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00011D4B File Offset: 0x0000FF4B
			private bool IsOver(CalendarNavigator.MeetingInfo mi)
			{
				return mi.EndTime < this.user.Now;
			}

			// Token: 0x060003A9 RID: 937 RVA: 0x00011D64 File Offset: 0x0000FF64
			private bool IsBetter(CalendarNavigator.MeetingInfo challenger, CalendarNavigator.MeetingInfo bestSoFar)
			{
				if (!this.IsOver(challenger))
				{
					if (bestSoFar == null)
					{
						return true;
					}
					if (challenger.StartTime < bestSoFar.StartTime || (challenger.StartTime > bestSoFar.StartTime && this.InProgress(challenger) && this.InProgress(bestSoFar)))
					{
						return !bestSoFar.IsAllDayEvent;
					}
				}
				return false;
			}

			// Token: 0x060003AA RID: 938 RVA: 0x00011DC2 File Offset: 0x0000FFC2
			private bool InProgress(CalendarNavigator.MeetingInfo mi)
			{
				return mi.StartTime <= this.user.Now && mi.EndTime > this.user.Now;
			}

			// Token: 0x04000128 RID: 296
			private ArrayList agenda;

			// Token: 0x04000129 RID: 297
			private int idx;

			// Token: 0x0400012A RID: 298
			private ArrayList conflicts;

			// Token: 0x0400012B RID: 299
			private int conflictIdx;

			// Token: 0x0400012C RID: 300
			private bool isInitialPosition;

			// Token: 0x0400012D RID: 301
			private bool isOnlyReadRemaining;

			// Token: 0x0400012E RID: 302
			private bool isReadConflicts;

			// Token: 0x0400012F RID: 303
			private ExDateTime conflictTime;

			// Token: 0x04000130 RID: 304
			private UMSubscriber user;

			// Token: 0x04000131 RID: 305
			private CalendarNavigator.MeetingInfo lastMeetingHeard;
		}
	}
}
