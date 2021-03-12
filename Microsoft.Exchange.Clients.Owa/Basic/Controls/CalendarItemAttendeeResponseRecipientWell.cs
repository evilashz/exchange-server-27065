using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200001B RID: 27
	public class CalendarItemAttendeeResponseRecipientWell : ItemRecipientWell
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000072C6 File Offset: 0x000054C6
		internal CalendarItemAttendeeResponseRecipientWell(UserContext userContext, CalendarItemBase calendarItemBase) : base(userContext)
		{
			this.calendarItemBase = calendarItemBase;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000746C File Offset: 0x0000566C
		internal override IEnumerator<Participant> GetRecipientCollection(RecipientWellType type)
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

		// Token: 0x04000084 RID: 132
		private CalendarItemBase calendarItemBase;
	}
}
