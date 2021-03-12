using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200040A RID: 1034
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MissingAttendeeItemRumInfo : UpdateRumInfo
	{
		// Token: 0x06002EF6 RID: 12022 RVA: 0x000C1297 File Offset: 0x000BF497
		private MissingAttendeeItemRumInfo(ExDateTime? originalStartTime, IList<Attendee> attendees, CalendarInconsistencyFlag inconsistencyFlag, int? deletedItemVersion) : base(originalStartTime, attendees, inconsistencyFlag)
		{
			this.DeletedItemVersion = deletedItemVersion;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000C12AC File Offset: 0x000BF4AC
		public static MissingAttendeeItemRumInfo CreateMasterInstance(IList<Attendee> attendees, CalendarInconsistencyFlag inconsistencyFlag, int? deletedItemVersion)
		{
			return new MissingAttendeeItemRumInfo(null, attendees, inconsistencyFlag, deletedItemVersion);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000C12CC File Offset: 0x000BF4CC
		public new static MissingAttendeeItemRumInfo CreateOccurrenceInstance(ExDateTime originalStartTime, IList<Attendee> attendees, CalendarInconsistencyFlag inconsistencyFlag)
		{
			return new MissingAttendeeItemRumInfo(new ExDateTime?(originalStartTime), attendees, inconsistencyFlag, null);
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x000C12EF File Offset: 0x000BF4EF
		// (set) Token: 0x06002EFA RID: 12026 RVA: 0x000C12F7 File Offset: 0x000BF4F7
		public int? DeletedItemVersion { get; private set; }
	}
}
