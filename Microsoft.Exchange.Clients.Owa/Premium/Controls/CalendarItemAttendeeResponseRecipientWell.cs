using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000328 RID: 808
	public class CalendarItemAttendeeResponseRecipientWell : ItemRecipientWell
	{
		// Token: 0x06001EA0 RID: 7840 RVA: 0x000B095F File Offset: 0x000AEB5F
		internal CalendarItemAttendeeResponseRecipientWell(CalendarItemBase calendarItemBase)
		{
			this.calendarItemBase = calendarItemBase;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000B096E File Offset: 0x000AEB6E
		internal CalendarItemAttendeeResponseRecipientWell() : this(null)
		{
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000B0B0C File Offset: 0x000AED0C
		internal override IEnumerator<Participant> GetRecipientsCollection(RecipientWellType type)
		{
			if (this.calendarItemBase != null)
			{
				ResponseType responseType;
				switch (type)
				{
				case RecipientWellType.Cc:
					responseType = ResponseType.Tentative;
					goto IL_69;
				case RecipientWellType.Bcc:
					responseType = ResponseType.Decline;
					goto IL_69;
				}
				responseType = ResponseType.Accept;
				IL_69:
				foreach (Attendee attendee in this.calendarItemBase.AttendeeCollection)
				{
					if (attendee.ResponseType == responseType)
					{
						yield return attendee.Participant;
					}
				}
			}
			yield break;
		}

		// Token: 0x04001689 RID: 5769
		private CalendarItemBase calendarItemBase;
	}
}
