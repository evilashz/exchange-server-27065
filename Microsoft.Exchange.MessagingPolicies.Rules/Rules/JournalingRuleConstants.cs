using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000072 RID: 114
	internal static class JournalingRuleConstants
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x00014290 File Offset: 0x00012490
		internal static bool TryParseGccType(string gccTypeString, out GccType gccType)
		{
			if (gccTypeString != null)
			{
				if (gccTypeString == "none")
				{
					gccType = GccType.None;
					return true;
				}
				if (gccTypeString == "full")
				{
					gccType = GccType.Full;
					return true;
				}
				if (gccTypeString == "prtt")
				{
					gccType = GccType.Prtt;
					return true;
				}
			}
			gccType = GccType.None;
			return false;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000142E0 File Offset: 0x000124E0
		internal static string StringFromGccType(GccType type)
		{
			switch (type)
			{
			case GccType.Full:
				return "full";
			case GccType.Prtt:
				return "prtt";
			}
			return "none";
		}

		// Token: 0x0400023F RID: 575
		internal const string Journal = "Journal";

		// Token: 0x04000240 RID: 576
		internal const string JournalAndReconcile = "JournalAndReconcile";

		// Token: 0x04000241 RID: 577
		internal const char ReconciliationAccountTupleSeparator = '+';

		// Token: 0x04000242 RID: 578
		internal const char ReconciliationAccountDisabledPrefix = '!';

		// Token: 0x04000243 RID: 579
		internal const string AttributeGccType = "gccType";

		// Token: 0x04000244 RID: 580
		internal const string GccTypeNone = "none";

		// Token: 0x04000245 RID: 581
		internal const string GccTypeFull = "full";

		// Token: 0x04000246 RID: 582
		internal const string GccTypePrtt = "prtt";

		// Token: 0x04000247 RID: 583
		internal const string JournalAgentName = "JA";

		// Token: 0x04000248 RID: 584
		internal const string OriginalMessageGroup = "ORIG";

		// Token: 0x04000249 RID: 585
		internal const string JournalReportGroup = "JR";

		// Token: 0x0400024A RID: 586
		internal const string RuleType = "type";

		// Token: 0x0400024B RID: 587
		internal const string TenantRuleType = "tenant";

		// Token: 0x0400024C RID: 588
		internal const string LawfulInterceptRuleType = "LI";

		// Token: 0x0400024D RID: 589
		internal const string RuleId = "ruleid";

		// Token: 0x0400024E RID: 590
		internal const string MessageId = "mid";

		// Token: 0x0400024F RID: 591
		internal const string Destination = "dest";

		// Token: 0x04000250 RID: 592
		internal const string OriginalMessageId = "orig";

		// Token: 0x04000251 RID: 593
		internal const string JournalRecipsProperty = "Microsoft.Exchange.JournalTargetRecips";

		// Token: 0x04000252 RID: 594
		internal const string JournalReconciliationAccounts = "Microsoft.Exchange.JournalReconciliationAccounts";

		// Token: 0x04000253 RID: 595
		internal const string JournalRuleIdsProperty = "Microsoft.Exchange.JournalRuleIds";
	}
}
