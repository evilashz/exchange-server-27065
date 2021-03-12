using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000EA RID: 234
	internal class AvailabilityQueryResult
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001B46D File Offset: 0x0001966D
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0001B475 File Offset: 0x00019675
		public FreeBusyQueryResult[] FreeBusyResults
		{
			get
			{
				return this.freeBusyResults;
			}
			internal set
			{
				this.freeBusyResults = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001B47E File Offset: 0x0001967E
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001B486 File Offset: 0x00019686
		public SuggestionDayResult[] DailyMeetingSuggestions
		{
			get
			{
				return this.dailyMeetingSuggestions;
			}
			internal set
			{
				this.dailyMeetingSuggestions = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001B48F File Offset: 0x0001968F
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0001B497 File Offset: 0x00019697
		public LocalizedException MeetingSuggestionsException
		{
			get
			{
				return this.meetingSuggestionsException;
			}
			internal set
			{
				this.meetingSuggestionsException = value;
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001B4A0 File Offset: 0x000196A0
		internal static AvailabilityQueryResult Create()
		{
			return new AvailabilityQueryResult();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001B4A7 File Offset: 0x000196A7
		private AvailabilityQueryResult()
		{
		}

		// Token: 0x04000389 RID: 905
		private FreeBusyQueryResult[] freeBusyResults;

		// Token: 0x0400038A RID: 906
		private SuggestionDayResult[] dailyMeetingSuggestions;

		// Token: 0x0400038B RID: 907
		private LocalizedException meetingSuggestionsException;
	}
}
