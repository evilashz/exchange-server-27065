using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000732 RID: 1842
	internal class MailboxPlanSchema : MailboxSchema
	{
		// Token: 0x04003B8B RID: 15243
		public static readonly ADPropertyDefinition IsDefault = ADRecipientSchema.IsDefault;

		// Token: 0x04003B8C RID: 15244
		public static readonly ADPropertyDefinition IsDefault_R3 = ADRecipientSchema.IsDefault_R3;

		// Token: 0x04003B8D RID: 15245
		public static readonly ADPropertyDefinition MailboxPlanIndex = ADRecipientSchema.MailboxPlanIndex;

		// Token: 0x04003B8E RID: 15246
		public static readonly ADPropertyDefinition MailboxPlanRelease = ADRecipientSchema.MailboxPlanRelease;

		// Token: 0x04003B8F RID: 15247
		public static readonly ADPropertyDefinition IsPilotMailboxPlan = ADUserSchema.IsPilotMailboxPlan;
	}
}
