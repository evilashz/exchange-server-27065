using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004BA RID: 1210
	internal sealed class ContentFilterConfigSchema : MessageHygieneAgentConfigSchema
	{
		// Token: 0x04002547 RID: 9543
		public static readonly ADPropertyDefinition OutlookEmailPostmarkValidationEnabled = new ADPropertyDefinition("OutlookEmailPostmarkValidationEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageHygieneAgentConfigSchema.AgentsFlags
		}, null, ADObject.FlagGetterDelegate(64, MessageHygieneAgentConfigSchema.AgentsFlags), ADObject.FlagSetterDelegate(64, MessageHygieneAgentConfigSchema.AgentsFlags), null, null);

		// Token: 0x04002548 RID: 9544
		public static readonly ADPropertyDefinition RejectionResponse = new ADPropertyDefinition("RejectionResponse", ExchangeObjectVersion.Exchange2007, typeof(AsciiString), "msExchMessageHygieneRejectionMessage", ADPropertyDefinitionFlags.None, ContentFilterConfigSchema.Defaults.RejectionResponse, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 240)
		}, null, null);

		// Token: 0x04002549 RID: 9545
		public static readonly ADPropertyDefinition BypassedRecipients = SharedPropertyDefinitions.BypassedRecipients;

		// Token: 0x0400254A RID: 9546
		public static readonly ADPropertyDefinition EncodedPhrases = new ADPropertyDefinition("CustomWeightEntry", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMessageHygieneCustomWeightEntry", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400254B RID: 9547
		public static readonly ADPropertyDefinition QuarantineMailbox = new ADPropertyDefinition("QuarantineMailbox", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress?), "msExchMessageHygieneQuarantineMailbox", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400254C RID: 9548
		public static readonly ADPropertyDefinition SCLQuarantineThreshold = new ADPropertyDefinition("SCLQuarantineThreshold", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMessageHygieneSCLQuarantineThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 9, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 9)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400254D RID: 9549
		public static readonly ADPropertyDefinition SCLQuarantineEnabled = new ADPropertyDefinition("SCLQuarantineEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageHygieneAgentConfigSchema.AgentsFlags
		}, null, ADObject.FlagGetterDelegate(512, MessageHygieneAgentConfigSchema.AgentsFlags), ADObject.FlagSetterDelegate(512, MessageHygieneAgentConfigSchema.AgentsFlags), null, null);

		// Token: 0x0400254E RID: 9550
		public static readonly ADPropertyDefinition SCLDeleteThreshold = new ADPropertyDefinition("SCLDeleteThreshold", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMessageHygieneSCLDeleteThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 9, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 9)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400254F RID: 9551
		public static readonly ADPropertyDefinition SCLDeleteEnabled = new ADPropertyDefinition("SCLDeleteEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageHygieneAgentConfigSchema.AgentsFlags
		}, null, ADObject.FlagGetterDelegate(128, MessageHygieneAgentConfigSchema.AgentsFlags), ADObject.FlagSetterDelegate(128, MessageHygieneAgentConfigSchema.AgentsFlags), null, null);

		// Token: 0x04002550 RID: 9552
		public static readonly ADPropertyDefinition SCLRejectThreshold = new ADPropertyDefinition("SCLRejectThreshold", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMessageHygieneSCLRejectThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 7, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 9)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002551 RID: 9553
		public static readonly ADPropertyDefinition SCLRejectEnabled = new ADPropertyDefinition("SCLRejectEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageHygieneAgentConfigSchema.AgentsFlags
		}, null, ADObject.FlagGetterDelegate(256, MessageHygieneAgentConfigSchema.AgentsFlags), ADObject.FlagSetterDelegate(256, MessageHygieneAgentConfigSchema.AgentsFlags), null, null);

		// Token: 0x04002552 RID: 9554
		public static readonly ADPropertyDefinition BypassedSenders = new ADPropertyDefinition("BypassedSenders", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchMessageHygieneBypassedSenders", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, null, null);

		// Token: 0x04002553 RID: 9555
		public static readonly ADPropertyDefinition BypassedSenderDomains = new ADPropertyDefinition("BypassedSenderDomains", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomainWithSubdomains), "msExchMessageHygieneBypassedSenderDomains", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x020004BB RID: 1211
		internal static class Defaults
		{
			// Token: 0x04002554 RID: 9556
			public const int SCLDeleteThreshold = 9;

			// Token: 0x04002555 RID: 9557
			public const bool SCLDeleteEnabled = false;

			// Token: 0x04002556 RID: 9558
			public const int SCLRejectThreshold = 7;

			// Token: 0x04002557 RID: 9559
			public const bool SCLRejectEnabled = true;

			// Token: 0x04002558 RID: 9560
			public const int SCLQuarantineThreshold = 9;

			// Token: 0x04002559 RID: 9561
			public const bool SCLQuarantineEnabled = false;

			// Token: 0x0400255A RID: 9562
			public static readonly AsciiString RejectionResponse = new AsciiString("Message rejected as spam by Content Filtering.");
		}
	}
}
