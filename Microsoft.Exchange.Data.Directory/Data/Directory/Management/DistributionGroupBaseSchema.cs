using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F3 RID: 1779
	internal abstract class DistributionGroupBaseSchema : MailEnabledRecipientSchema
	{
		// Token: 0x04003870 RID: 14448
		public static readonly ADPropertyDefinition ExpansionServer = ADGroupSchema.ExpansionServer;

		// Token: 0x04003871 RID: 14449
		public static readonly ADPropertyDefinition ReportToManagerEnabled = ADGroupSchema.ReportToManagerEnabled;

		// Token: 0x04003872 RID: 14450
		public static readonly ADPropertyDefinition ReportToOriginatorEnabled = ADGroupSchema.ReportToOriginatorEnabled;

		// Token: 0x04003873 RID: 14451
		public static readonly ADPropertyDefinition SendOofMessageToOriginatorEnabled = ADGroupSchema.SendOofMessageToOriginatorEnabled;

		// Token: 0x04003874 RID: 14452
		public static readonly ADPropertyDefinition DefaultDistributionListOU = ADRecipientSchema.DefaultDistributionListOU;
	}
}
