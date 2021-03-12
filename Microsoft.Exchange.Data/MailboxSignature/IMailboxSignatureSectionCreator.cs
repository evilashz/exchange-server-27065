using System;

namespace Microsoft.Exchange.Data.MailboxSignature
{
	// Token: 0x02000232 RID: 562
	internal interface IMailboxSignatureSectionCreator
	{
		// Token: 0x06001376 RID: 4982
		bool Create(MailboxSignatureSectionType sectionType, out MailboxSignatureSectionMetadata sectionMetadata, out byte[] sectionData);
	}
}
