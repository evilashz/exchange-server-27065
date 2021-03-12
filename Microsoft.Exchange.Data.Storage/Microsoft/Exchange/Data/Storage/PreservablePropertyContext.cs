using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003F3 RID: 1011
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PreservablePropertyContext
	{
		// Token: 0x06002E33 RID: 11827 RVA: 0x000BDAD7 File Offset: 0x000BBCD7
		public PreservablePropertyContext(MeetingRequest mtg, CalendarItemBase calItem, ChangeHighlightProperties organizerHighlights)
		{
			this.MeetingRequest = mtg;
			this.CalendarItem = calItem;
			this.OrganizerHighlights = organizerHighlights;
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000BDAF4 File Offset: 0x000BBCF4
		// (set) Token: 0x06002E35 RID: 11829 RVA: 0x000BDAFC File Offset: 0x000BBCFC
		public MeetingRequest MeetingRequest { get; private set; }

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000BDB05 File Offset: 0x000BBD05
		// (set) Token: 0x06002E37 RID: 11831 RVA: 0x000BDB0D File Offset: 0x000BBD0D
		public CalendarItemBase CalendarItem { get; private set; }

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x000BDB16 File Offset: 0x000BBD16
		// (set) Token: 0x06002E39 RID: 11833 RVA: 0x000BDB1E File Offset: 0x000BBD1E
		public ChangeHighlightProperties OrganizerHighlights { get; private set; }
	}
}
