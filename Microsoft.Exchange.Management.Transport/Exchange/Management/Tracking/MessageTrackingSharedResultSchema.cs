using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x0200009F RID: 159
	internal class MessageTrackingSharedResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040001FE RID: 510
		public static readonly SimpleProviderPropertyDefinition MessageTrackingReportId = new SimpleProviderPropertyDefinition("MessageTrackingReportId", ExchangeObjectVersion.Exchange2010, typeof(MessageTrackingReportId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001FF RID: 511
		public static readonly SimpleProviderPropertyDefinition SubmittedDateTime = new SimpleProviderPropertyDefinition("SubmittedDateTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.TaskPopulated, DateTime.Today, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000200 RID: 512
		public static readonly SimpleProviderPropertyDefinition Subject = new SimpleProviderPropertyDefinition("Subject", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000201 RID: 513
		public static readonly SimpleProviderPropertyDefinition FromAddress = new SimpleProviderPropertyDefinition("FromAddress", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000202 RID: 514
		public static readonly SimpleProviderPropertyDefinition FromDisplayName = new SimpleProviderPropertyDefinition("FromDisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000203 RID: 515
		public static readonly SimpleProviderPropertyDefinition RecipientAddresses = new SimpleProviderPropertyDefinition("RecipientAddresses", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000204 RID: 516
		public static readonly SimpleProviderPropertyDefinition RecipientDisplayNames = new SimpleProviderPropertyDefinition("RecipientDisplayNames", ExchangeObjectVersion.Exchange2010, typeof(string[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
