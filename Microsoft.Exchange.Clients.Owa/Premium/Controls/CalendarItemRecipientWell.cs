using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200032B RID: 811
	public class CalendarItemRecipientWell : ItemRecipientWell
	{
		// Token: 0x06001EA7 RID: 7847 RVA: 0x000B0DE3 File Offset: 0x000AEFE3
		internal CalendarItemRecipientWell(CalendarItemBase calendarItemBase)
		{
			this.calendarItemBase = calendarItemBase;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000B0DF2 File Offset: 0x000AEFF2
		internal CalendarItemRecipientWell() : this(null)
		{
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000B0FA0 File Offset: 0x000AF1A0
		internal override IEnumerator<Participant> GetRecipientsCollection(RecipientWellType type)
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
					if (attendee.AttendeeType == attendeeType && !attendee.IsOrganizer)
					{
						yield return attendee.Participant;
					}
				}
			}
			yield break;
		}

		// Token: 0x0400168A RID: 5770
		private CalendarItemBase calendarItemBase;
	}
}
