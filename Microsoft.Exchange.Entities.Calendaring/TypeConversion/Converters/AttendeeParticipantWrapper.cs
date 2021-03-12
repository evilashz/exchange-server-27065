using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000075 RID: 117
	internal class AttendeeParticipantWrapper : ParticipantWrapper<Attendee>
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000B3FF File Offset: 0x000095FF
		public AttendeeParticipantWrapper(Attendee attendee) : base(attendee)
		{
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000B408 File Offset: 0x00009608
		public override Participant Participant
		{
			get
			{
				if (base.Original != null)
				{
					return base.Original.Participant;
				}
				return null;
			}
		}
	}
}
