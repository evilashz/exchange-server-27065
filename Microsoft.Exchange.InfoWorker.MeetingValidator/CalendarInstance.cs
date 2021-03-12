using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CalendarInstance
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x0000593C File Offset: 0x00003B3C
		protected CalendarInstance(ExchangePrincipal remotePrincipal)
		{
			if (remotePrincipal == null)
			{
				throw new ArgumentNullException("remotePrincipal");
			}
			this.ExchangePrincipal = remotePrincipal;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005959 File Offset: 0x00003B59
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005961 File Offset: 0x00003B61
		internal ExchangePrincipal ExchangePrincipal { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000596A File Offset: 0x00003B6A
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005972 File Offset: 0x00003B72
		internal bool ShouldProcessMailbox { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000597B File Offset: 0x00003B7B
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005983 File Offset: 0x00003B83
		internal Inconsistency LoadInconsistency { get; set; }

		// Token: 0x060000E8 RID: 232
		internal abstract CalendarProcessingFlags? GetCalendarConfig();

		// Token: 0x060000E9 RID: 233
		internal abstract Inconsistency GetInconsistency(CalendarValidationContext context, string fullDescription);

		// Token: 0x060000EA RID: 234
		internal abstract bool WouldTryToRepairIfMissing(CalendarValidationContext context, out MeetingInquiryAction predictedAction);

		// Token: 0x060000EB RID: 235
		internal abstract ClientIntentFlags? GetLocationIntent(CalendarValidationContext context, GlobalObjectId globalObjectId, string organizerLocation, string attendeeLocation);
	}
}
