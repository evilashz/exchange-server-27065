using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000028 RID: 40
	public class CalendarValidationContext : IDisposable
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000A4E8 File Offset: 0x000086E8
		private CalendarValidationContext()
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A4F0 File Offset: 0x000086F0
		internal static CalendarValidationContext CreateInstance(CalendarItemBase calendarItem, bool isOrganizer, UserObject localUser, UserObject remoteUser, CalendarVersionStoreGateway cvsGateway, AttendeeExtractor attendeeExtractor)
		{
			CalendarValidationContext calendarValidationContext = new CalendarValidationContext();
			calendarValidationContext.LocalUser = localUser;
			calendarValidationContext.RemoteUser = remoteUser;
			if (isOrganizer)
			{
				calendarValidationContext.BaseRole = RoleType.Organizer;
				calendarValidationContext.OppositeRole = RoleType.Attendee;
				calendarValidationContext.Organizer = localUser;
				calendarValidationContext.Attendee = remoteUser;
			}
			else
			{
				calendarValidationContext.BaseRole = RoleType.Attendee;
				calendarValidationContext.OppositeRole = RoleType.Organizer;
				calendarValidationContext.Organizer = remoteUser;
				calendarValidationContext.Attendee = localUser;
			}
			calendarValidationContext.calendarItems = new Dictionary<RoleType, CalendarItemBase>(2);
			calendarValidationContext.calendarItems.Add(calendarValidationContext.BaseRole, calendarItem);
			calendarValidationContext.calendarItems.Add(calendarValidationContext.OppositeRole, null);
			calendarValidationContext.OrganizerRecurrence = null;
			calendarValidationContext.OrganizerExceptions = null;
			calendarValidationContext.OrganizerDeletions = null;
			calendarValidationContext.AttendeeRecurrence = null;
			calendarValidationContext.AttendeeExceptions = null;
			calendarValidationContext.AttendeeDeletions = null;
			calendarValidationContext.OppositeRoleOrganizerIsValid = false;
			calendarValidationContext.CvsGateway = cvsGateway;
			calendarValidationContext.AttendeeExtractor = attendeeExtractor;
			return calendarValidationContext;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000A5C0 File Offset: 0x000087C0
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000A5C8 File Offset: 0x000087C8
		internal UserObject Organizer { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000A5D1 File Offset: 0x000087D1
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000A5D9 File Offset: 0x000087D9
		internal UserObject Attendee { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000A5E2 File Offset: 0x000087E2
		internal CalendarItemBase OrganizerItem
		{
			get
			{
				return this.calendarItems[RoleType.Organizer];
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000A5F0 File Offset: 0x000087F0
		internal CalendarItemBase AttendeeItem
		{
			get
			{
				return this.calendarItems[RoleType.Attendee];
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000A5FE File Offset: 0x000087FE
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000A606 File Offset: 0x00008806
		internal RecurrenceInfo OrganizerRecurrence { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000A60F File Offset: 0x0000880F
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000A617 File Offset: 0x00008817
		internal RecurrenceInfo AttendeeRecurrence { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000A620 File Offset: 0x00008820
		internal bool AreItemsOccurrences
		{
			get
			{
				return this.AttendeeItem != null && this.AttendeeItem.CalendarItemType == CalendarItemType.Occurrence && this.OrganizerItem != null && this.OrganizerItem.CalendarItemType == CalendarItemType.Occurrence;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000A650 File Offset: 0x00008850
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000A658 File Offset: 0x00008858
		internal RoleType BaseRole { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000A661 File Offset: 0x00008861
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000A669 File Offset: 0x00008869
		internal RoleType OppositeRole { get; private set; }

		// Token: 0x06000158 RID: 344 RVA: 0x0000A672 File Offset: 0x00008872
		internal bool IsRoleGroupMailbox(RoleType roleType)
		{
			if (roleType != RoleType.Organizer)
			{
				return this.Attendee.IsGroupMailbox;
			}
			return this.Organizer.IsGroupMailbox;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000A68E File Offset: 0x0000888E
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000A696 File Offset: 0x00008896
		internal bool OppositeRoleOrganizerIsValid { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000A69F File Offset: 0x0000889F
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000A6B2 File Offset: 0x000088B2
		internal CalendarItemBase BaseItem
		{
			get
			{
				return this.calendarItems[this.BaseRole];
			}
			set
			{
				this.calendarItems[this.BaseRole] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000A6C6 File Offset: 0x000088C6
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000A6D9 File Offset: 0x000088D9
		internal CalendarItemBase OppositeItem
		{
			get
			{
				return this.calendarItems[this.OppositeRole];
			}
			set
			{
				this.calendarItems[this.OppositeRole] = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000A6ED File Offset: 0x000088ED
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000A6F5 File Offset: 0x000088F5
		internal List<OccurrenceInfo> OrganizerExceptions { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000A6FE File Offset: 0x000088FE
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000A706 File Offset: 0x00008906
		internal List<OccurrenceInfo> AttendeeExceptions { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000A70F File Offset: 0x0000890F
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000A717 File Offset: 0x00008917
		internal ExDateTime[] OrganizerDeletions { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000A720 File Offset: 0x00008920
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000A728 File Offset: 0x00008928
		internal ExDateTime[] AttendeeDeletions { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000A731 File Offset: 0x00008931
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000A739 File Offset: 0x00008939
		internal CalendarInstance CalendarInstance { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000A742 File Offset: 0x00008942
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000A74A File Offset: 0x0000894A
		internal UserObject LocalUser { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000A753 File Offset: 0x00008953
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000A75B File Offset: 0x0000895B
		internal UserObject RemoteUser { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000A764 File Offset: 0x00008964
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000A76C File Offset: 0x0000896C
		internal bool HasSentUpdateForItemOrMaster { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000A775 File Offset: 0x00008975
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000A77D File Offset: 0x0000897D
		internal CalendarVersionStoreGateway CvsGateway { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000A786 File Offset: 0x00008986
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000A78E File Offset: 0x0000898E
		internal AttendeeExtractor AttendeeExtractor { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000A797 File Offset: 0x00008997
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000A79F File Offset: 0x0000899F
		internal string ErrorString { get; set; }

		// Token: 0x06000175 RID: 373 RVA: 0x0000A7A8 File Offset: 0x000089A8
		public void Dispose()
		{
			if (this.BaseItem != null)
			{
				this.BaseItem.Dispose();
				this.BaseItem = null;
			}
			if (this.OppositeItem != null)
			{
				this.OppositeItem.Dispose();
				this.OppositeItem = null;
			}
		}

		// Token: 0x040000B4 RID: 180
		private Dictionary<RoleType, CalendarItemBase> calendarItems;
	}
}
