using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200037E RID: 894
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAttendeeCollection : IRecipientBaseCollection<Attendee>, IList<Attendee>, ICollection<Attendee>, IEnumerable<Attendee>, IEnumerable
	{
		// Token: 0x0600278F RID: 10127
		Attendee Add(Participant participant, AttendeeType attendeeType = AttendeeType.Required, ResponseType? responseType = null, ExDateTime? replyTime = null, bool checkExisting = false);
	}
}
