using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000FC RID: 252
	internal sealed class FreeBusyQuery : BaseQuery
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001E050 File Offset: 0x0001C250
		public AttendeeKind AttendeeKind
		{
			get
			{
				return this.attendeeKind;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001E058 File Offset: 0x0001C258
		public new FreeBusyQueryResult Result
		{
			get
			{
				return (FreeBusyQueryResult)base.Result;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001E065 File Offset: 0x0001C265
		internal FreeBusyQuery[] GroupMembersForFreeBusy
		{
			get
			{
				return this.groupMembersForFreeBusy;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001E06D File Offset: 0x0001C26D
		internal FreeBusyQuery[] GroupMembersForSuggestions
		{
			get
			{
				return this.groupMembersForSuggestions;
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001E075 File Offset: 0x0001C275
		public static FreeBusyQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return new FreeBusyQuery(recipientData, AttendeeKind.Unknown, new FreeBusyQueryResult(exception));
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001E084 File Offset: 0x0001C284
		public static FreeBusyQuery CreateFromIndividual(RecipientData recipientData)
		{
			return new FreeBusyQuery(recipientData, AttendeeKind.Individual, null);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001E08E File Offset: 0x0001C28E
		public static FreeBusyQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return new FreeBusyQuery(recipientData, AttendeeKind.Individual, new FreeBusyQueryResult(exception));
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001E09D File Offset: 0x0001C29D
		public static FreeBusyQuery CreateFromGroup(RecipientData recipientData, LocalizedException exception)
		{
			return new FreeBusyQuery(recipientData, AttendeeKind.Group, new FreeBusyQueryResult(exception));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		public static FreeBusyQuery CreateFromGroup(RecipientData recipientData, FreeBusyQuery[] groupMembersForFreeBusy, FreeBusyQuery[] groupMembersForSuggestions)
		{
			return new FreeBusyQuery(recipientData, AttendeeKind.Group, groupMembersForFreeBusy, groupMembersForSuggestions);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001E0B7 File Offset: 0x0001C2B7
		private FreeBusyQuery(RecipientData recipientData, AttendeeKind attendeeKind, FreeBusyQueryResult result) : base(recipientData, result)
		{
			this.attendeeKind = attendeeKind;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001E0C8 File Offset: 0x0001C2C8
		private FreeBusyQuery(RecipientData recipientData, AttendeeKind attendeeKind, FreeBusyQuery[] groupMembersForFreeBusy, FreeBusyQuery[] groupMembersForSuggestions) : base(recipientData, null)
		{
			this.attendeeKind = attendeeKind;
			this.groupMembersForFreeBusy = groupMembersForFreeBusy;
			this.groupMembersForSuggestions = groupMembersForSuggestions;
		}

		// Token: 0x04000403 RID: 1027
		private AttendeeKind attendeeKind;

		// Token: 0x04000404 RID: 1028
		private FreeBusyQuery[] groupMembersForFreeBusy;

		// Token: 0x04000405 RID: 1029
		private FreeBusyQuery[] groupMembersForSuggestions;
	}
}
