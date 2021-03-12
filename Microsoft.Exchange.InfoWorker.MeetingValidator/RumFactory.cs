using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RumFactory
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00004A20 File Offset: 0x00002C20
		private RumFactory()
		{
			this.isInitialized = false;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004A50 File Offset: 0x00002C50
		internal static RumFactory Instance
		{
			get
			{
				RumFactory result;
				lock (RumFactory.threadSafetyLock)
				{
					if (RumFactory.instance == null)
					{
						RumFactory.instance = new RumFactory();
					}
					result = RumFactory.instance;
				}
				return result;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004AA4 File Offset: 0x00002CA4
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004AAC File Offset: 0x00002CAC
		internal CalendarRepairPolicy Policy { get; private set; }

		// Token: 0x0600009E RID: 158 RVA: 0x00004AB5 File Offset: 0x00002CB5
		internal void Initialize(CalendarRepairPolicy policy)
		{
			this.Policy = policy;
			this.isInitialized = true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004AC8 File Offset: 0x00002CC8
		internal RumInfo CreateRumInfo(CalendarValidationContext context, Inconsistency inconsistency)
		{
			this.CheckInitialized();
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (inconsistency == null)
			{
				throw new ArgumentNullException("inconsistency");
			}
			if (this.ShouldSendRum(context, inconsistency))
			{
				IList<Attendee> list = new List<Attendee>(1);
				if (context.BaseRole == RoleType.Organizer && context.Attendee != null)
				{
					if (context.Attendee.Attendee == null)
					{
						context.Attendee.Attendee = context.OrganizerItem.AttendeeCollection.Add(context.Attendee.Participant, AttendeeType.Required, null, null, false);
					}
					list.Add(context.Attendee.Attendee);
				}
				RumInfo rumInfo = inconsistency.CreateRumInfo(context, list);
				if (!rumInfo.IsNullOp && rumInfo is OrganizerRumInfo && context.AttendeeItem != null)
				{
					((OrganizerRumInfo)rumInfo).AttendeeRequiredSequenceNumber = context.AttendeeItem.AppointmentSequenceNumber;
				}
				return rumInfo;
			}
			return NullOpRumInfo.CreateInstance();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004BB4 File Offset: 0x00002DB4
		private bool ShouldSendRum(CalendarValidationContext context, Inconsistency inconsistency)
		{
			if (context.BaseRole == RoleType.Attendee && CalendarItemBase.IsTenantToBeFixed(context.BaseItem.Session as MailboxSession))
			{
				return false;
			}
			if (inconsistency.Owner == context.BaseRole)
			{
				return false;
			}
			if (!this.Policy.ContainsRepairFlag(inconsistency.Flag))
			{
				return false;
			}
			if (context.AttendeeItem != null && context.OrganizerItem != null && context.AttendeeItem.CalendarItemType == CalendarItemType.Occurrence && context.OrganizerItem.CalendarItemType == CalendarItemType.Occurrence)
			{
				return false;
			}
			if (context.RemoteUser.ExchangePrincipal == null)
			{
				return false;
			}
			if (this.CheckServerVersion(context, inconsistency))
			{
				CalendarProcessingFlags? calendarConfig = context.CalendarInstance.GetCalendarConfig();
				return calendarConfig != null && (inconsistency.Flag != CalendarInconsistencyFlag.MissingItem || !context.HasSentUpdateForItemOrMaster) && calendarConfig.Value == CalendarProcessingFlags.AutoUpdate;
			}
			return false;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004C88 File Offset: 0x00002E88
		private bool CheckServerVersion(CalendarValidationContext context, Inconsistency inconsistency)
		{
			ServerVersion serverVersion = new ServerVersion(context.RemoteUser.ExchangePrincipal.MailboxInfo.Location.ServerVersion);
			if (serverVersion.Major <= 8)
			{
				return false;
			}
			if (inconsistency.Flag == CalendarInconsistencyFlag.OrphanedMeeting)
			{
				return ServerVersion.Compare(serverVersion, this.supportsMeetingInquiryAfter) > 0;
			}
			return ServerVersion.Compare(serverVersion, this.supportsRumsAfter) > 0;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004CE8 File Offset: 0x00002EE8
		private void CheckInitialized()
		{
			if (!this.isInitialized)
			{
				throw new ObjectNotInitializedException(base.GetType());
			}
		}

		// Token: 0x0400002E RID: 46
		private static RumFactory instance;

		// Token: 0x0400002F RID: 47
		private static object threadSafetyLock = new object();

		// Token: 0x04000030 RID: 48
		private bool isInitialized;

		// Token: 0x04000031 RID: 49
		private ServerVersion supportsMeetingInquiryAfter = new ServerVersion(14, 1, 0, 0);

		// Token: 0x04000032 RID: 50
		private ServerVersion supportsRumsAfter = new ServerVersion(14, 0, 0, 0);
	}
}
