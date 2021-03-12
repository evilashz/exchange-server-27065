using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class MissingItemInconsistency : Inconsistency
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000461A File Offset: 0x0000281A
		protected MissingItemInconsistency(RoleType owner, string description, CalendarInconsistencyFlag flag, CalendarValidationContext context) : base(owner, description, flag, context)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004628 File Offset: 0x00002828
		internal static MissingItemInconsistency CreateAttendeeMissingItemInstance(string description, ClientIntentFlags? intent, int? deletedItemVersion, CalendarValidationContext context)
		{
			return new MissingItemInconsistency(RoleType.Attendee, description, CalendarInconsistencyFlag.MissingItem, context)
			{
				Intent = intent,
				DeletedItemVersion = deletedItemVersion
			};
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004651 File Offset: 0x00002851
		internal static MissingItemInconsistency CreateOrganizerMissingItemInstance(string description, CalendarValidationContext context)
		{
			return new MissingItemInconsistency(RoleType.Organizer, description, CalendarInconsistencyFlag.OrphanedMeeting, context);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000465C File Offset: 0x0000285C
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00004664 File Offset: 0x00002864
		public int? DeletedItemVersion { get; private set; }

		// Token: 0x06000077 RID: 119 RVA: 0x00004670 File Offset: 0x00002870
		internal override RumInfo CreateRumInfo(CalendarValidationContext context, IList<Attendee> attendees)
		{
			CalendarInconsistencyFlag flag = base.Flag;
			if (flag != CalendarInconsistencyFlag.OrphanedMeeting)
			{
				return MissingAttendeeItemRumInfo.CreateMasterInstance(attendees, base.Flag, this.DeletedItemVersion);
			}
			if (context.OppositeRole == RoleType.Organizer && !context.OppositeRoleOrganizerIsValid)
			{
				return NullOpRumInfo.CreateInstance();
			}
			MeetingInquiryAction predictedRepairAction;
			bool wouldRepair = context.CalendarInstance.WouldTryToRepairIfMissing(context, out predictedRepairAction);
			return AttendeeInquiryRumInfo.CreateMasterInstance(wouldRepair, predictedRepairAction);
		}
	}
}
