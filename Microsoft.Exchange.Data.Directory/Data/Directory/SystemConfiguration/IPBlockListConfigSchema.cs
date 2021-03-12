using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C9 RID: 1225
	internal sealed class IPBlockListConfigSchema : MessageHygieneAgentConfigSchema
	{
		// Token: 0x04002576 RID: 9590
		internal static readonly AsciiString DefaultMachineRejectionResponse = new AsciiString("External client with IP address {0} does not have permissions to submit to this server. Visit http://support.microsoft.com/kb/928123 for more information.");

		// Token: 0x04002577 RID: 9591
		internal static readonly AsciiString DefaultStaticRejectionResponse = new AsciiString("External client with IP address {0} does not have permissions to submit to this server.");

		// Token: 0x04002578 RID: 9592
		public static readonly ADPropertyDefinition MachineEntryRejectionResponse = new ADPropertyDefinition("MachineEntryRejectionResponse", ExchangeObjectVersion.Exchange2007, typeof(AsciiString), "msExchMessageHygieneMachineGeneratedRejectionResponse", ADPropertyDefinitionFlags.None, IPBlockListConfigSchema.DefaultMachineRejectionResponse, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 240)
		}, null, null);

		// Token: 0x04002579 RID: 9593
		public static readonly ADPropertyDefinition StaticEntryRejectionResponse = new ADPropertyDefinition("StaticEntryRejectionResponse", ExchangeObjectVersion.Exchange2007, typeof(AsciiString), "msExchMessageHygieneStaticEntryRejectionResponse", ADPropertyDefinitionFlags.None, IPBlockListConfigSchema.DefaultStaticRejectionResponse, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 240)
		}, null, null);
	}
}
