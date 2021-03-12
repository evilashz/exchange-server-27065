using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200003D RID: 61
	internal class MeetingData : IComparable<MeetingData>
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000C9AD File Offset: 0x0000ABAD
		private MeetingData()
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		private static MeetingData CreateDummyInstance(UserObject mailboxUser, UserObject organizer)
		{
			MeetingData meetingData = new MeetingData();
			meetingData.MailboxUserPrimarySmtpAddress = mailboxUser.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			if (organizer == null)
			{
				meetingData.OrganizerPrimarySmtpAddress = string.Empty;
			}
			else if (organizer.ExchangePrincipal == null)
			{
				meetingData.OrganizerPrimarySmtpAddress = organizer.EmailAddress;
			}
			else
			{
				meetingData.OrganizerPrimarySmtpAddress = organizer.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			}
			return meetingData;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		internal static MeetingData CreateInstance(UserObject mailboxUser, UserObject organizer, CalendarItemBase calendarItem)
		{
			MeetingData meetingData = MeetingData.CreateDummyInstance(mailboxUser, organizer);
			meetingData.ExtractDataFromCalendarItem(calendarItem);
			return meetingData;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		internal static MeetingData CreateInstance(UserObject mailboxUser, Item item)
		{
			MeetingData meetingData = MeetingData.CreateDummyInstance(mailboxUser, null);
			meetingData.ExtractDataFromItem(item);
			return meetingData;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		internal static MeetingData CreateInstance(UserObject mailboxUser, StoreId id)
		{
			MeetingData meetingData = MeetingData.CreateDummyInstance(mailboxUser, null);
			meetingData.SetDefaultCalendarItemBasedProperties();
			meetingData.Id = id;
			return meetingData;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		internal static MeetingData CreateInstance(UserObject mailboxUser, StoreId id, GlobalObjectId globalObjectId, int appointmentSequenceNumber, ExDateTime lastModifiedTime, ExDateTime ownerCriticalChangeTime, CalendarItemType itemType, string subject, int? itemVersion, int documentId)
		{
			MeetingData meetingData = MeetingData.CreateInstance(mailboxUser, id, globalObjectId, itemType);
			meetingData.SequenceNumber = appointmentSequenceNumber;
			meetingData.LastModifiedTime = lastModifiedTime;
			meetingData.OwnerCriticalChangeTime = ownerCriticalChangeTime;
			meetingData.CalendarItemType = itemType;
			meetingData.Subject = subject;
			meetingData.ItemVersion = itemVersion;
			meetingData.DocumentId = documentId;
			return meetingData;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		internal static MeetingData CreateInstance(UserObject mailboxUser, StoreId meetingId, GlobalObjectId globalObjectId, CalendarItemType itemType)
		{
			MeetingData meetingData = MeetingData.CreateDummyInstance(mailboxUser, null);
			meetingData.Id = meetingId;
			meetingData.GlobalObjectId = globalObjectId;
			meetingData.CalendarItemType = itemType;
			return meetingData;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		internal static MeetingData CreateInstance(UserObject mailboxUser, StoreId meetingId, GlobalObjectId globalObjectId, Exception exception)
		{
			MeetingData meetingData = MeetingData.CreateDummyInstance(mailboxUser, null);
			meetingData.Id = meetingId;
			meetingData.GlobalObjectId = globalObjectId;
			meetingData.Exception = exception;
			return meetingData;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public int CompareTo(MeetingData other)
		{
			if (other == null)
			{
				return 1;
			}
			if (this.Id == other.Id)
			{
				return 0;
			}
			if (this.Exception != null || other.Exception != null)
			{
				return ((other.Exception != null) ? 1 : 0) - ((this.Exception != null) ? 1 : 0);
			}
			int num;
			if (GlobalObjectId.CompareCleanGlobalObjectIds(this.GlobalObjectId.CleanGlobalObjectIdBytes, other.GlobalObjectId.CleanGlobalObjectIdBytes))
			{
				num = Nullable.Compare<int>(new int?(this.SequenceNumber), new int?(other.SequenceNumber));
				if (num != 0)
				{
					return num;
				}
				num = this.LastModifiedTime.CompareTo(other.LastModifiedTime, MeetingData.LastModifiedTimeTreshold);
				if (num != 0)
				{
					return num;
				}
				num = this.OwnerCriticalChangeTime.CompareTo(other.OwnerCriticalChangeTime);
				if (num != 0)
				{
					return num;
				}
				num = this.DocumentId.CompareTo(other.DocumentId);
				if (num != 0)
				{
					return num;
				}
				if (this.ItemVersion != null && other.ItemVersion != null)
				{
					num = Nullable.Compare<int>(this.ItemVersion, other.ItemVersion);
					if (num != 0)
					{
						return num;
					}
				}
			}
			else
			{
				ExDateTime startTime = this.StartTime;
				ExDateTime startTime2 = other.StartTime;
				num = ExDateTime.Compare(this.StartTime, other.StartTime);
			}
			return num;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000CC84 File Offset: 0x0000AE84
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public GlobalObjectId GlobalObjectId { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000CC95 File Offset: 0x0000AE95
		public string CleanGlobalObjectId
		{
			get
			{
				if (this.cleanGlobaObjectId == null)
				{
					this.cleanGlobaObjectId = ((this.GlobalObjectId != null) ? GlobalObjectId.ByteArrayToHexString(this.GlobalObjectId.CleanGlobalObjectIdBytes) : string.Empty);
				}
				return this.cleanGlobaObjectId;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000CCCA File Offset: 0x0000AECA
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000CCD2 File Offset: 0x0000AED2
		public string Subject { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000CCDB File Offset: 0x0000AEDB
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000CCE3 File Offset: 0x0000AEE3
		public CalendarItemType CalendarItemType { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public ExDateTime StartTime { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000CCFD File Offset: 0x0000AEFD
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000CD05 File Offset: 0x0000AF05
		public ExDateTime EndTime { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000CD0E File Offset: 0x0000AF0E
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000CD16 File Offset: 0x0000AF16
		public int? OwnerAppointmentId { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000CD1F File Offset: 0x0000AF1F
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000CD27 File Offset: 0x0000AF27
		public int? ItemVersion { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000CD30 File Offset: 0x0000AF30
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public int DocumentId { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000CD41 File Offset: 0x0000AF41
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000CD49 File Offset: 0x0000AF49
		internal bool HasConflicts { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000CD52 File Offset: 0x0000AF52
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000CD5A File Offset: 0x0000AF5A
		internal string MailboxUserPrimarySmtpAddress { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000CD63 File Offset: 0x0000AF63
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000CD6B File Offset: 0x0000AF6B
		internal string OrganizerPrimarySmtpAddress { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000CD74 File Offset: 0x0000AF74
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		internal bool IsOrganizer { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000CD85 File Offset: 0x0000AF85
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000CD8D File Offset: 0x0000AF8D
		internal ExDateTime ExtractTime { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000CD96 File Offset: 0x0000AF96
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		internal ExDateTime OwnerCriticalChangeTime { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000CDA7 File Offset: 0x0000AFA7
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000CDAF File Offset: 0x0000AFAF
		internal ExDateTime AttendeeCriticalChangeTime { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
		internal long ExtractVersion { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000CDC9 File Offset: 0x0000AFC9
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000CDD1 File Offset: 0x0000AFD1
		internal int SequenceNumber { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000CDDA File Offset: 0x0000AFDA
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000CDE2 File Offset: 0x0000AFE2
		internal int LastSequenceNumber { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000CDEB File Offset: 0x0000AFEB
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000CDF3 File Offset: 0x0000AFF3
		internal string InternetMessageId { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000CE04 File Offset: 0x0000B004
		internal ExDateTime CreationTime { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000CE0D File Offset: 0x0000B00D
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000CE15 File Offset: 0x0000B015
		internal ExDateTime LastModifiedTime { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000CE1E File Offset: 0x0000B01E
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000CE26 File Offset: 0x0000B026
		internal string Location { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000CE2F File Offset: 0x0000B02F
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000CE37 File Offset: 0x0000B037
		internal StoreId Id { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000CE40 File Offset: 0x0000B040
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000CE48 File Offset: 0x0000B048
		internal Exception Exception { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000CE51 File Offset: 0x0000B051
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000CE59 File Offset: 0x0000B059
		internal bool HasDuplicates { get; set; }

		// Token: 0x06000227 RID: 551 RVA: 0x0000CE64 File Offset: 0x0000B064
		private void ExtractDataFromCalendarItem(CalendarItemBase calendarItem)
		{
			if (this.GetPropertiesFromStoreObject(calendarItem))
			{
				this.IsOrganizer = calendarItem.IsOrganizer();
				this.GlobalObjectId = calendarItem.GlobalObjectId;
				this.OwnerAppointmentId = calendarItem.OwnerAppointmentId;
				this.Subject = calendarItem.Subject;
				this.StartTime = calendarItem.StartTime;
				this.EndTime = calendarItem.EndTime;
				this.CalendarItemType = calendarItem.CalendarItemType;
				this.SequenceNumber = calendarItem.AppointmentSequenceNumber;
				this.LastSequenceNumber = calendarItem.AppointmentLastSequenceNumber;
				this.OwnerCriticalChangeTime = calendarItem.OwnerCriticalChangeTime;
				this.AttendeeCriticalChangeTime = calendarItem.AttendeeCriticalChangeTime;
				this.CreationTime = calendarItem.CreationTime;
				this.LastModifiedTime = calendarItem.LastModifiedTime;
				this.Location = calendarItem.Location;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000CF28 File Offset: 0x0000B128
		private void ExtractDataFromItem(Item item)
		{
			if (this.GetPropertiesFromStoreObject(item))
			{
				this.IsOrganizer = false;
				this.GlobalObjectId = new GlobalObjectId(item.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.GlobalObjectId));
				this.OwnerAppointmentId = item.GetValueAsNullable<int>(CalendarItemBaseSchema.OwnerAppointmentID);
				this.Subject = item.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
				this.StartTime = item.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.StartTime, ExDateTime.MinValue);
				this.EndTime = item.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.EndTime, ExDateTime.MinValue);
				this.CalendarItemType = item.GetValueOrDefault<CalendarItemType>(CalendarItemBaseSchema.CalendarItemType, CalendarItemType.Single);
				this.SequenceNumber = item.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentSequenceNumber);
				this.LastSequenceNumber = item.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentLastSequenceNumber);
				this.OwnerCriticalChangeTime = item.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.OwnerCriticalChangeTime, ExDateTime.MinValue);
				this.AttendeeCriticalChangeTime = item.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.AttendeeCriticalChangeTime, ExDateTime.MinValue);
				this.CreationTime = item.GetValueOrDefault<ExDateTime>(StoreObjectSchema.CreationTime, ExDateTime.MinValue);
				this.LastModifiedTime = item.GetValueOrDefault<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
				this.Location = item.GetValueOrDefault<string>(CalendarItemBaseSchema.Location, string.Empty);
				this.ItemVersion = item.GetValueAsNullable<int>(CalendarItemBaseSchema.ItemVersion);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000D064 File Offset: 0x0000B264
		private bool GetPropertiesFromStoreObject(StoreObject storeObject)
		{
			if (storeObject == null)
			{
				this.SetDefaultCalendarItemBasedProperties();
				return false;
			}
			this.Id = storeObject.Id;
			this.ExtractTime = storeObject.GetValueOrDefault<ExDateTime>(CalendarItemBaseSchema.AppointmentExtractTime, ExDateTime.MinValue);
			this.ExtractVersion = storeObject.GetValueOrDefault<long>(CalendarItemBaseSchema.AppointmentExtractVersion, long.MinValue);
			this.HasConflicts = storeObject.GetValueOrDefault<bool>(MessageItemSchema.MessageInConflict);
			this.InternetMessageId = storeObject.GetValueOrDefault<string>(ItemSchema.InternetMessageId, string.Empty);
			this.DocumentId = storeObject.GetValueOrDefault<int>(ItemSchema.DocumentId, int.MinValue);
			return true;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		private void SetDefaultCalendarItemBasedProperties()
		{
			this.IsOrganizer = false;
			this.Id = null;
			this.GlobalObjectId = null;
			this.OwnerAppointmentId = null;
			this.Subject = null;
			this.StartTime = ExDateTime.MinValue;
			this.EndTime = ExDateTime.MinValue;
			this.CalendarItemType = CalendarItemType.Single;
			this.ExtractTime = ExDateTime.MinValue;
			this.ExtractVersion = 0L;
			this.HasConflicts = false;
			this.InternetMessageId = null;
			this.SequenceNumber = 0;
			this.LastSequenceNumber = 0;
			this.OwnerCriticalChangeTime = ExDateTime.MinValue;
			this.AttendeeCriticalChangeTime = ExDateTime.MinValue;
			this.CreationTime = ExDateTime.MinValue;
			this.LastModifiedTime = ExDateTime.MinValue;
			this.Location = string.Empty;
			this.DocumentId = int.MinValue;
		}

		// Token: 0x0400014D RID: 333
		private static readonly TimeSpan LastModifiedTimeTreshold = TimeSpan.FromSeconds(5.0);

		// Token: 0x0400014E RID: 334
		private string cleanGlobaObjectId;
	}
}
