using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004B7 RID: 1207
	internal class MessageHygieneAgentConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002536 RID: 9526
		private const int EnabledMask = 1;

		// Token: 0x04002537 RID: 9527
		private const int ExternalMailEnabledMask = 2;

		// Token: 0x04002538 RID: 9528
		private const int InternalMailEnabledMask = 4;

		// Token: 0x04002539 RID: 9529
		internal const int OutlookEmailPostmarkValidationEnabledMask = 64;

		// Token: 0x0400253A RID: 9530
		internal const int SCLDeleteEnabledMask = 128;

		// Token: 0x0400253B RID: 9531
		internal const int SCLRejectEnabledMask = 256;

		// Token: 0x0400253C RID: 9532
		internal const int SCLQuarantineEnabledMask = 512;

		// Token: 0x0400253D RID: 9533
		internal const int BlockListEnabledMask = 1024;

		// Token: 0x0400253E RID: 9534
		internal const int RecipientValidationEnabledMask = 2048;

		// Token: 0x0400253F RID: 9535
		internal const int BlockBlankSendersMask = 4096;

		// Token: 0x04002540 RID: 9536
		public static readonly ADPropertyDefinition AgentsFlags = new ADPropertyDefinition("AgentsFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchAgentsFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002541 RID: 9537
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageHygieneAgentConfigSchema.AgentsFlags
		}, null, ADObject.FlagGetterDelegate(1, MessageHygieneAgentConfigSchema.AgentsFlags), ADObject.FlagSetterDelegate(1, MessageHygieneAgentConfigSchema.AgentsFlags), null, null);

		// Token: 0x04002542 RID: 9538
		public static readonly ADPropertyDefinition ExternalMailEnabled = new ADPropertyDefinition("ExternalMailEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageHygieneAgentConfigSchema.AgentsFlags
		}, null, ADObject.FlagGetterDelegate(2, MessageHygieneAgentConfigSchema.AgentsFlags), ADObject.FlagSetterDelegate(2, MessageHygieneAgentConfigSchema.AgentsFlags), null, null);

		// Token: 0x04002543 RID: 9539
		public static readonly ADPropertyDefinition InternalMailEnabled = new ADPropertyDefinition("InternalMailEnabled ", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageHygieneAgentConfigSchema.AgentsFlags
		}, null, ADObject.FlagGetterDelegate(4, MessageHygieneAgentConfigSchema.AgentsFlags), ADObject.FlagSetterDelegate(4, MessageHygieneAgentConfigSchema.AgentsFlags), null, null);

		// Token: 0x020004B8 RID: 1208
		private struct Defaults
		{
			// Token: 0x04002544 RID: 9540
			public const int AgentFlags = 3;
		}
	}
}
