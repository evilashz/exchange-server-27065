using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000408 RID: 1032
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CancellationRumInfo : OrganizerRumInfo
	{
		// Token: 0x06002EEB RID: 12011 RVA: 0x000C1154 File Offset: 0x000BF354
		private CancellationRumInfo() : this(null, null)
		{
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000C1171 File Offset: 0x000BF371
		private CancellationRumInfo(ExDateTime? originalStartTime, IList<Attendee> attendees) : base(RumType.Cancellation, originalStartTime, attendees)
		{
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000C117C File Offset: 0x000BF37C
		public static CancellationRumInfo CreateMasterInstance(IList<Attendee> attendees)
		{
			return new CancellationRumInfo(null, attendees);
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000C1198 File Offset: 0x000BF398
		public static CancellationRumInfo CreateOccurrenceInstance(ExDateTime originalStartTime, IList<Attendee> attendees)
		{
			return new CancellationRumInfo(new ExDateTime?(originalStartTime), attendees);
		}
	}
}
