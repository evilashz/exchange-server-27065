using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000066 RID: 102
	public enum IncidentReportContent
	{
		// Token: 0x0400021B RID: 539
		[LocDescription(TransportRulesStrings.IDs.IncidentReportSender)]
		Sender = 1,
		// Token: 0x0400021C RID: 540
		[LocDescription(TransportRulesStrings.IDs.IncidentReportRecipients)]
		Recipients,
		// Token: 0x0400021D RID: 541
		[LocDescription(TransportRulesStrings.IDs.IncidentReportSubject)]
		Subject,
		// Token: 0x0400021E RID: 542
		[LocDescription(TransportRulesStrings.IDs.IncidentReportCc)]
		Cc,
		// Token: 0x0400021F RID: 543
		[LocDescription(TransportRulesStrings.IDs.IncidentReportBcc)]
		Bcc,
		// Token: 0x04000220 RID: 544
		[LocDescription(TransportRulesStrings.IDs.IncidentReportSeverity)]
		Severity,
		// Token: 0x04000221 RID: 545
		[LocDescription(TransportRulesStrings.IDs.IncidentReportOverride)]
		Override,
		// Token: 0x04000222 RID: 546
		[LocDescription(TransportRulesStrings.IDs.IncidentReportRuleDetections)]
		RuleDetections,
		// Token: 0x04000223 RID: 547
		[LocDescription(TransportRulesStrings.IDs.IncidentReportFalsePositive)]
		FalsePositive,
		// Token: 0x04000224 RID: 548
		[LocDescription(TransportRulesStrings.IDs.IncidentReportDataClassifications)]
		DataClassifications,
		// Token: 0x04000225 RID: 549
		[LocDescription(TransportRulesStrings.IDs.IncidentReportIdMatch)]
		IdMatch,
		// Token: 0x04000226 RID: 550
		[LocDescription(TransportRulesStrings.IDs.IncidentReportAttachOriginalMail)]
		AttachOriginalMail
	}
}
