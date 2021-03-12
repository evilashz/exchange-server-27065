using System;

namespace Microsoft.Exchange.Data.MailboxSignature
{
	// Token: 0x02000231 RID: 561
	internal interface IMailboxSignatureSectionProcessor
	{
		// Token: 0x06001375 RID: 4981
		bool Process(MailboxSignatureSectionMetadata sectionMetadata, byte[] buffer, ref int offset);
	}
}
