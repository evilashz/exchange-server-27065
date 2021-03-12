using System;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A26 RID: 2598
	public enum JournalNdrValidationCheckResult
	{
		// Token: 0x0400348B RID: 13451
		JournalNdrValidationPassed,
		// Token: 0x0400348C RID: 13452
		JournalNdrCannotBeNullReversePath,
		// Token: 0x0400348D RID: 13453
		JournalNdrExistInJournalRuleRecipient,
		// Token: 0x0400348E RID: 13454
		JournalNdrExistInJournalRuleJournalEmailAddress
	}
}
