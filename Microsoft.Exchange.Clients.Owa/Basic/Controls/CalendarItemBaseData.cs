using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x020000A9 RID: 169
	internal class CalendarItemBaseData
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00030EF0 File Offset: 0x0002F0F0
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00030EF8 File Offset: 0x0002F0F8
		public StoreObjectId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00030F01 File Offset: 0x0002F101
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00030F09 File Offset: 0x0002F109
		public string ChangeKey
		{
			get
			{
				return this.changeKey;
			}
			set
			{
				this.changeKey = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00030F12 File Offset: 0x0002F112
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00030F1A File Offset: 0x0002F11A
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00030F23 File Offset: 0x0002F123
		public List<AttachmentId> AttachmentIds
		{
			get
			{
				return this.attachmentIds;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00030F2B File Offset: 0x0002F12B
		public List<AttendeeData> Attendees
		{
			get
			{
				return this.attendees;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00030F33 File Offset: 0x0002F133
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00030F3B File Offset: 0x0002F13B
		public string BodyText
		{
			get
			{
				return this.bodyText;
			}
			set
			{
				this.bodyText = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00030F44 File Offset: 0x0002F144
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00030F4C File Offset: 0x0002F14C
		public BodyFormat BodyFormat
		{
			get
			{
				return this.bodyFormat;
			}
			set
			{
				this.bodyFormat = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00030F55 File Offset: 0x0002F155
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00030F5D File Offset: 0x0002F15D
		public CalendarItemType CalendarItemType
		{
			get
			{
				return this.calendarItemType;
			}
			set
			{
				this.calendarItemType = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00030F66 File Offset: 0x0002F166
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00030F6E File Offset: 0x0002F16E
		public ExDateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00030F77 File Offset: 0x0002F177
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00030F7F File Offset: 0x0002F17F
		public BusyType FreeBusyStatus
		{
			get
			{
				return this.freeBusyStatus;
			}
			set
			{
				this.freeBusyStatus = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00030F88 File Offset: 0x0002F188
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00030F90 File Offset: 0x0002F190
		public Importance Importance
		{
			get
			{
				return this.importance;
			}
			set
			{
				this.importance = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00030F99 File Offset: 0x0002F199
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00030FA1 File Offset: 0x0002F1A1
		public bool IsAllDayEvent
		{
			get
			{
				return this.isAllDayEvent;
			}
			set
			{
				this.isAllDayEvent = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00030FAA File Offset: 0x0002F1AA
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00030FB2 File Offset: 0x0002F1B2
		public bool IsMeeting
		{
			get
			{
				return this.isMeeting;
			}
			set
			{
				this.isMeeting = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00030FBB File Offset: 0x0002F1BB
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00030FC3 File Offset: 0x0002F1C3
		public bool IsOrganizer
		{
			get
			{
				return this.isOrganizer;
			}
			set
			{
				this.isOrganizer = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00030FCC File Offset: 0x0002F1CC
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00030FD4 File Offset: 0x0002F1D4
		public bool IsResponseRequested
		{
			get
			{
				return this.isResponseRequested;
			}
			set
			{
				this.isResponseRequested = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00030FDD File Offset: 0x0002F1DD
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00030FE5 File Offset: 0x0002F1E5
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00030FEE File Offset: 0x0002F1EE
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00030FF6 File Offset: 0x0002F1F6
		public bool MeetingRequestWasSent
		{
			get
			{
				return this.meetingRequestWasSent;
			}
			set
			{
				this.meetingRequestWasSent = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00030FFF File Offset: 0x0002F1FF
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00031007 File Offset: 0x0002F207
		public Participant Organizer
		{
			get
			{
				return this.organizer;
			}
			set
			{
				this.organizer = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00031010 File Offset: 0x0002F210
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00031018 File Offset: 0x0002F218
		public StoreObjectId ParentId
		{
			get
			{
				return this.parentId;
			}
			set
			{
				this.parentId = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00031021 File Offset: 0x0002F221
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00031029 File Offset: 0x0002F229
		public Sensitivity Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			set
			{
				this.sensitivity = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00031032 File Offset: 0x0002F232
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0003103A File Offset: 0x0002F23A
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00031043 File Offset: 0x0002F243
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0003104B File Offset: 0x0002F24B
		public ExDateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00031054 File Offset: 0x0002F254
		public CalendarItemBaseData()
		{
			this.attachmentIds = new List<AttachmentId>();
			this.attendees = new List<AttendeeData>();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00031079 File Offset: 0x0002F279
		public CalendarItemBaseData(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase is CalendarItem || calendarItemBase is CalendarItemOccurrence)
			{
				throw new ArgumentException("Constructing a CalendarItemBase from sub-class type CalendarItem or CalendarItemOccurrence probably causes unexpected behavior. Use Create() instead.");
			}
			this.SetFrom(calendarItemBase);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000310AC File Offset: 0x0002F2AC
		public CalendarItemBaseData(CalendarItemBaseData other)
		{
			if (other.attachmentIds != null)
			{
				this.attachmentIds = new List<AttachmentId>();
				foreach (AttachmentId item in other.attachmentIds)
				{
					this.attachmentIds.Add(item);
				}
			}
			if (other.attendees != null)
			{
				this.attendees = new List<AttendeeData>();
				foreach (AttendeeData other2 in other.attendees)
				{
					this.attendees.Add(new AttendeeData(other2));
				}
			}
			this.bodyText = other.bodyText;
			this.bodyFormat = other.bodyFormat;
			this.calendarItemType = other.calendarItemType;
			this.endTime = other.endTime;
			this.freeBusyStatus = other.freeBusyStatus;
			try
			{
				if (other.folderId != null)
				{
					this.folderId = StoreObjectId.Deserialize(other.folderId.GetBytes());
				}
				if (other.id != null)
				{
					this.id = StoreObjectId.Deserialize(other.id.GetBytes());
				}
			}
			catch (ArgumentException)
			{
				throw new OwaInvalidRequestException("Invalid store object id");
			}
			catch (FormatException)
			{
				throw new OwaInvalidRequestException("Invalid store object id");
			}
			this.changeKey = other.changeKey;
			this.importance = other.importance;
			this.isAllDayEvent = other.isAllDayEvent;
			this.isMeeting = other.isMeeting;
			this.isOrganizer = other.isOrganizer;
			this.isResponseRequested = other.isResponseRequested;
			this.location = other.location;
			this.meetingRequestWasSent = other.meetingRequestWasSent;
			this.organizer = other.organizer;
			this.parentId = other.parentId;
			this.sensitivity = other.sensitivity;
			this.startTime = other.startTime;
			this.subject = other.subject;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000312CC File Offset: 0x0002F4CC
		public static CalendarItemBaseData Create(CalendarItemBase calendarItemBase)
		{
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			if (calendarItem != null)
			{
				return new CalendarItemData(calendarItem);
			}
			CalendarItemOccurrence calendarItemOccurrence = calendarItemBase as CalendarItemOccurrence;
			if (calendarItemOccurrence != null)
			{
				return new CalendarItemOccurrenceData(calendarItemOccurrence);
			}
			return new CalendarItemBaseData(calendarItemBase);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00031304 File Offset: 0x0002F504
		public static bool SyncAttendeesToCalendarItem(CalendarItemBaseData data, CalendarItemBase calendarItemBase)
		{
			bool flag = !data.IsAttendeesEqual(calendarItemBase);
			if (flag)
			{
				calendarItemBase.AttendeeCollection.Clear();
				foreach (AttendeeData attendeeData in data.attendees)
				{
					calendarItemBase.AttendeeCollection.Add(attendeeData.Participant, attendeeData.AttendeeType, null, null, false);
				}
			}
			return flag;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00031398 File Offset: 0x0002F598
		public static Attendee GetFirstResourceAttendee(CalendarItemBase calendarItemBase)
		{
			Attendee result = null;
			if (calendarItemBase != null)
			{
				foreach (Attendee attendee in calendarItemBase.AttendeeCollection)
				{
					if (attendee.AttendeeType == AttendeeType.Resource && attendee.Participant != null && !string.IsNullOrEmpty(attendee.Participant.DisplayName))
					{
						result = attendee;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00031414 File Offset: 0x0002F614
		public static bool GetIsResponseRequested(CalendarItemBase calendarItemBase)
		{
			object obj = calendarItemBase.TryGetProperty(ItemSchema.IsResponseRequested);
			return obj is bool && (bool)obj;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00031440 File Offset: 0x0002F640
		public static void SetIsResponseRequested(CalendarItemBase calendarItemBase, bool value)
		{
			bool flag = CalendarItemBaseData.GetIsResponseRequested(calendarItemBase);
			if (flag != value)
			{
				calendarItemBase.SetProperties(CalendarItemBaseData.isResponseRequestedPropertyDefinition, new object[]
				{
					value
				});
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00031474 File Offset: 0x0002F674
		public virtual void SetFrom(CalendarItemBase calendarItemBase)
		{
			if (this.attachmentIds == null)
			{
				this.attachmentIds = new List<AttachmentId>();
			}
			else
			{
				this.attachmentIds.Clear();
			}
			if (this.attendees == null)
			{
				this.attendees = new List<AttendeeData>();
			}
			else
			{
				this.attendees.Clear();
			}
			if (calendarItemBase.AttachmentCollection != null)
			{
				foreach (AttachmentHandle handle in calendarItemBase.AttachmentCollection)
				{
					using (Attachment attachment = calendarItemBase.AttachmentCollection.Open(handle))
					{
						if (attachment.Id == null)
						{
							throw new ArgumentNullException("attachment.Id");
						}
						this.attachmentIds.Add(attachment.Id);
					}
				}
			}
			if (calendarItemBase.Body != null)
			{
				this.bodyText = ItemUtility.GetItemBody(calendarItemBase, BodyFormat.TextPlain);
				this.bodyFormat = BodyFormat.TextPlain;
			}
			this.calendarItemType = calendarItemBase.CalendarItemType;
			this.endTime = calendarItemBase.EndTime;
			this.freeBusyStatus = calendarItemBase.FreeBusyStatus;
			try
			{
				if (calendarItemBase.ParentId != null)
				{
					this.folderId = StoreObjectId.Deserialize(calendarItemBase.ParentId.GetBytes());
				}
				else
				{
					this.folderId = null;
				}
				if (calendarItemBase.Id != null && calendarItemBase.Id.ObjectId != null)
				{
					this.id = StoreObjectId.Deserialize(calendarItemBase.Id.ObjectId.GetBytes());
				}
				else
				{
					this.id = null;
				}
			}
			catch (ArgumentException)
			{
				throw new OwaInvalidRequestException("Invalid store object id");
			}
			catch (FormatException)
			{
				throw new OwaInvalidRequestException("Invalid store object id");
			}
			if (calendarItemBase.Id != null)
			{
				this.changeKey = calendarItemBase.Id.ChangeKeyAsBase64String();
			}
			else
			{
				this.changeKey = null;
			}
			this.importance = calendarItemBase.Importance;
			this.isAllDayEvent = calendarItemBase.IsAllDayEvent;
			this.isMeeting = calendarItemBase.IsMeeting;
			this.isOrganizer = calendarItemBase.IsOrganizer();
			this.isResponseRequested = CalendarItemBaseData.GetIsResponseRequested(calendarItemBase);
			this.location = calendarItemBase.Location;
			this.meetingRequestWasSent = calendarItemBase.MeetingRequestWasSent;
			this.organizer = AttendeeData.CloneParticipant(calendarItemBase.Organizer);
			if (calendarItemBase.ParentId != null)
			{
				this.parentId = StoreObjectId.FromProviderSpecificId(calendarItemBase.ParentId.ProviderLevelItemId);
			}
			if (calendarItemBase.AttendeeCollection != null)
			{
				foreach (Attendee attendee in calendarItemBase.AttendeeCollection)
				{
					this.attendees.Add(new AttendeeData(attendee));
				}
			}
			this.sensitivity = calendarItemBase.Sensitivity;
			this.startTime = calendarItemBase.StartTime;
			this.subject = calendarItemBase.Subject;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0003173C File Offset: 0x0002F93C
		public bool SetLocation(CalendarItemBase calendarItemBase)
		{
			bool result = false;
			if (string.IsNullOrEmpty(calendarItemBase.Location))
			{
				Attendee firstResourceAttendee = CalendarItemBaseData.GetFirstResourceAttendee(calendarItemBase);
				if (firstResourceAttendee != null)
				{
					this.Location = firstResourceAttendee.Participant.DisplayName;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00031778 File Offset: 0x0002F978
		public virtual EditCalendarItemHelper.CalendarItemUpdateFlags CopyTo(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase.Id != null && (this.id == null || this.id.CompareTo(calendarItemBase.Id.ObjectId) != 0))
			{
				throw new OwaLostContextException("Lost changes since last save.");
			}
			EditCalendarItemHelper.CalendarItemUpdateFlags calendarItemUpdateFlags = EditCalendarItemHelper.CalendarItemUpdateFlags.None;
			if (EditCalendarItemHelper.BodyChanged(this.bodyText, calendarItemBase))
			{
				if (!string.IsNullOrEmpty(this.bodyText))
				{
					if (this.bodyFormat == BodyFormat.TextHtml)
					{
						ItemUtility.SetItemBody(calendarItemBase, BodyFormat.TextHtml, this.bodyText);
						calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
					}
					else
					{
						if (this.bodyFormat != BodyFormat.TextPlain)
						{
							throw new ArgumentOutOfRangeException("calendarItemBase", "Unhandled body format type : " + this.bodyFormat);
						}
						ItemUtility.SetItemBody(calendarItemBase, BodyFormat.TextPlain, this.bodyText);
						calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
					}
				}
				else
				{
					ItemUtility.SetItemBody(calendarItemBase, BodyFormat.TextPlain, string.Empty);
					calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
				}
			}
			if (this.freeBusyStatus != calendarItemBase.FreeBusyStatus)
			{
				calendarItemBase.FreeBusyStatus = this.freeBusyStatus;
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			if (calendarItemBase.Importance != this.importance)
			{
				calendarItemBase.Importance = this.importance;
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			if (calendarItemBase.IsAllDayEvent != this.isAllDayEvent)
			{
				calendarItemBase.IsAllDayEvent = this.isAllDayEvent;
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.TimeChanged;
			}
			if (calendarItemBase.IsMeeting != this.isMeeting)
			{
				calendarItemBase.IsMeeting = this.isMeeting;
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			if (CalendarItemBaseData.GetIsResponseRequested(calendarItemBase) != this.isResponseRequested)
			{
				CalendarItemBaseData.SetIsResponseRequested(calendarItemBase, this.isResponseRequested);
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			if (!CalendarUtilities.StringsEqualNullEmpty(calendarItemBase.Location, this.location, StringComparison.CurrentCulture))
			{
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.LocationChanged;
				calendarItemBase.Location = ((this.location != null) ? this.location : string.Empty);
			}
			CalendarItemBaseData.SyncAttendeesToCalendarItem(this, calendarItemBase);
			if (calendarItemBase.AttendeesChanged)
			{
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.AttendeesChanged;
			}
			if (calendarItemBase.Sensitivity != this.sensitivity)
			{
				calendarItemBase.Sensitivity = this.sensitivity;
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			if (calendarItemBase.EndTime != this.endTime)
			{
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.TimeChanged;
				calendarItemBase.EndTime = this.endTime;
			}
			if (calendarItemBase.StartTime != this.startTime)
			{
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.TimeChanged;
				calendarItemBase.StartTime = this.startTime;
			}
			if (!CalendarUtilities.StringsEqualNullEmpty(calendarItemBase.Subject, this.subject, StringComparison.CurrentCulture))
			{
				calendarItemBase.Subject = ((this.subject != null) ? this.subject : string.Empty);
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			return calendarItemUpdateFlags;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000319B4 File Offset: 0x0002FBB4
		public bool IsAttendeesEqual(CalendarItemBase calendarItemBase)
		{
			bool flag = true;
			if (this.attendees.Count != calendarItemBase.AttendeeCollection.Count)
			{
				flag = false;
			}
			if (flag)
			{
				foreach (Attendee attendee in calendarItemBase.AttendeeCollection)
				{
					if (!this.attendees.Contains(new AttendeeData(attendee)))
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x04000469 RID: 1129
		private static PropertyDefinition[] isResponseRequestedPropertyDefinition = new PropertyDefinition[]
		{
			ItemSchema.IsResponseRequested
		};

		// Token: 0x0400046A RID: 1130
		private StoreObjectId id;

		// Token: 0x0400046B RID: 1131
		private string changeKey;

		// Token: 0x0400046C RID: 1132
		private StoreObjectId folderId;

		// Token: 0x0400046D RID: 1133
		private List<AttachmentId> attachmentIds;

		// Token: 0x0400046E RID: 1134
		private List<AttendeeData> attendees;

		// Token: 0x0400046F RID: 1135
		private string bodyText;

		// Token: 0x04000470 RID: 1136
		private BodyFormat bodyFormat = BodyFormat.TextPlain;

		// Token: 0x04000471 RID: 1137
		private CalendarItemType calendarItemType;

		// Token: 0x04000472 RID: 1138
		private ExDateTime endTime;

		// Token: 0x04000473 RID: 1139
		private BusyType freeBusyStatus;

		// Token: 0x04000474 RID: 1140
		private Importance importance;

		// Token: 0x04000475 RID: 1141
		private bool isAllDayEvent;

		// Token: 0x04000476 RID: 1142
		private bool isMeeting;

		// Token: 0x04000477 RID: 1143
		private bool isOrganizer;

		// Token: 0x04000478 RID: 1144
		private bool isResponseRequested;

		// Token: 0x04000479 RID: 1145
		private string location;

		// Token: 0x0400047A RID: 1146
		private bool meetingRequestWasSent;

		// Token: 0x0400047B RID: 1147
		private Participant organizer;

		// Token: 0x0400047C RID: 1148
		private StoreObjectId parentId;

		// Token: 0x0400047D RID: 1149
		private Sensitivity sensitivity;

		// Token: 0x0400047E RID: 1150
		private string subject;

		// Token: 0x0400047F RID: 1151
		private ExDateTime startTime;
	}
}
