using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarExternalParticipant : CalendarParticipant
	{
		// Token: 0x060000DF RID: 223 RVA: 0x000058C8 File Offset: 0x00003AC8
		internal CalendarExternalParticipant(UserObject userObject, ExDateTime validateFrom, ExDateTime validateUntil) : base(userObject, validateFrom, validateUntil)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000058D4 File Offset: 0x00003AD4
		internal override void ValidateMeetings(ref Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent, Action<long> onItemRepaired)
		{
			foreach (CalendarInstanceContext calendarInstanceContext in base.ItemList.Values)
			{
				calendarInstanceContext.ValidationContext.CalendarInstance = null;
				base.ValidateInstance(calendarInstanceContext, organizerRumsSent, onItemRepaired);
			}
		}
	}
}
