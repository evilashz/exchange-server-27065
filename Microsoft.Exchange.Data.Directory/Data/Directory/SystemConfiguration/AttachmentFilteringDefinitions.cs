using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003A9 RID: 937
	internal sealed class AttachmentFilteringDefinitions
	{
		// Token: 0x040019D7 RID: 6615
		public const string DefaultRejectResponse = "Message rejected due to unacceptable attachments";

		// Token: 0x040019D8 RID: 6616
		public static readonly PropertyDefinitionConstraint[] RejectResponseConstraints = new PropertyDefinitionConstraint[]
		{
			new SmtpResponseConstraint(),
			new StringLengthConstraint(1, 240),
			new AsciiCharactersOnlyConstraint()
		};
	}
}
