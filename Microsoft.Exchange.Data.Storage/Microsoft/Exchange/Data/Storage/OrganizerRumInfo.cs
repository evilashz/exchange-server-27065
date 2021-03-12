using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000404 RID: 1028
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class OrganizerRumInfo : RumInfo
	{
		// Token: 0x06002ED7 RID: 11991 RVA: 0x000C1008 File Offset: 0x000BF208
		private OrganizerRumInfo() : base(RumType.None, null)
		{
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000C1025 File Offset: 0x000BF225
		protected OrganizerRumInfo(RumType type, ExDateTime? originalStartTime, IList<Attendee> attendees) : base(type, originalStartTime)
		{
			this.AttendeeList = new List<Attendee>(attendees);
			this.AttendeeRequiredSequenceNumber = int.MinValue;
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x000C1046 File Offset: 0x000BF246
		// (set) Token: 0x06002EDA RID: 11994 RVA: 0x000C104E File Offset: 0x000BF24E
		public IList<Attendee> AttendeeList { get; private set; }

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000C1057 File Offset: 0x000BF257
		// (set) Token: 0x06002EDC RID: 11996 RVA: 0x000C105F File Offset: 0x000BF25F
		public int AttendeeRequiredSequenceNumber { get; set; }
	}
}
