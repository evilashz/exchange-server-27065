using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200001C RID: 28
	public class CalendarItemRecipientWell : ItemRecipientWell
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000748F File Offset: 0x0000568F
		internal CalendarItemRecipientWell(UserContext userContext, CalendarItemBase calendarItemBase) : base(userContext)
		{
			this.calendarItemBase = calendarItemBase;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007634 File Offset: 0x00005834
		internal override IEnumerator<Participant> GetRecipientCollection(RecipientWellType type)
		{
			if (this.calendarItemBase != null)
			{
				AttendeeType attendeeType;
				switch (type)
				{
				case RecipientWellType.Cc:
					attendeeType = AttendeeType.Optional;
					goto IL_69;
				case RecipientWellType.Bcc:
					attendeeType = AttendeeType.Resource;
					goto IL_69;
				}
				attendeeType = AttendeeType.Required;
				IL_69:
				foreach (Attendee attendee in this.calendarItemBase.AttendeeCollection)
				{
					if (CalendarUtilities.IsExpectedTypeAttendee(attendee, attendeeType))
					{
						yield return attendee.Participant;
					}
				}
			}
			yield break;
		}

		// Token: 0x04000085 RID: 133
		private CalendarItemBase calendarItemBase;
	}
}
