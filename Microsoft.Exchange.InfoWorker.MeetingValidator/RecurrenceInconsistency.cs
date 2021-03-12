using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecurrenceInconsistency : Inconsistency
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000481B File Offset: 0x00002A1B
		private RecurrenceInconsistency()
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004823 File Offset: 0x00002A23
		private RecurrenceInconsistency(RoleType owner, string description, CalendarInconsistencyFlag flag, RecurrenceInconsistencyType recurrenceInconsistencyType, ExDateTime origstartDate, CalendarValidationContext context) : base(owner, description, flag, context)
		{
			this.InconsistencyType = recurrenceInconsistencyType;
			this.OriginalStartDate = origstartDate;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004840 File Offset: 0x00002A40
		internal static RecurrenceInconsistency CreateInstance(RoleType owner, string description, CalendarInconsistencyFlag flag, RecurrenceInconsistencyType recurrenceInconsistencyType, ExDateTime origstartDate, CalendarValidationContext context)
		{
			return new RecurrenceInconsistency(owner, description, flag, recurrenceInconsistencyType, origstartDate, context);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004850 File Offset: 0x00002A50
		internal override RumInfo CreateRumInfo(CalendarValidationContext context, IList<Attendee> attendees)
		{
			CalendarInconsistencyFlag flag = base.Flag;
			if (flag == CalendarInconsistencyFlag.ExtraOccurrenceDeletion)
			{
				return MissingAttendeeItemRumInfo.CreateOccurrenceInstance(this.OriginalStartDate, attendees, base.Flag);
			}
			return base.CreateRumInfo(context, attendees);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004884 File Offset: 0x00002A84
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000488C File Offset: 0x00002A8C
		internal RecurrenceInconsistencyType InconsistencyType { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004895 File Offset: 0x00002A95
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000489D File Offset: 0x00002A9D
		internal ExDateTime OriginalStartDate { get; set; }

		// Token: 0x0600008B RID: 139 RVA: 0x000048A8 File Offset: 0x00002AA8
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("Owner", base.Owner.ToString());
			writer.WriteElementString("Description", base.Description.ToString());
			writer.WriteElementString("InconsistencyType", this.InconsistencyType.ToString());
			writer.WriteElementString("OriginalStartDate", this.OriginalStartDate.ToString());
		}
	}
}
