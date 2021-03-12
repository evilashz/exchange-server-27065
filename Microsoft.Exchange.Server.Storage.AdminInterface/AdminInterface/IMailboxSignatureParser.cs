using System;
using Microsoft.Exchange.Data.MailboxSignature;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000067 RID: 103
	internal interface IMailboxSignatureParser
	{
		// Token: 0x060001E6 RID: 486
		bool ParseMailboxBasicInformation(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001E7 RID: 487
		bool ParseMailboxMappingMetadata(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001E8 RID: 488
		bool ParseMailboxNamedPropertyMapping(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001E9 RID: 489
		bool ParseMailboxReplidGuidMapping(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001EA RID: 490
		bool ParseMailboxTenantHint(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001EB RID: 491
		bool ParseMailboxTypeVersion(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001EC RID: 492
		bool ParsePartitionInformation(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001ED RID: 493
		bool ParseMailboxUnknownSection(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);
	}
}
