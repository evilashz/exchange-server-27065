using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000065 RID: 101
	public enum IncidentReportOriginalMail
	{
		// Token: 0x04000218 RID: 536
		[LocDescription(TransportRulesStrings.IDs.IncidentReportIncludeOriginalMail)]
		IncludeOriginalMail = 1,
		// Token: 0x04000219 RID: 537
		[LocDescription(TransportRulesStrings.IDs.IncidentReportDoNotIncludeOriginalMail)]
		DoNotIncludeOriginalMail
	}
}
