using System;
using Microsoft.Exchange.Data.MailboxSignature;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000068 RID: 104
	internal abstract class MailboxSignatureParserBase : IMailboxSignatureParser, IMailboxSignatureSectionProcessor
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		public bool Process(MailboxSignatureSectionMetadata sectionMetadata, byte[] buffer, ref int offset)
		{
			MailboxSignatureSectionType type = sectionMetadata.Type;
			if (type <= MailboxSignatureSectionType.TenantHint)
			{
				switch (type)
				{
				case MailboxSignatureSectionType.BasicInformation:
					return this.ParseMailboxBasicInformation(sectionMetadata, buffer, ref offset);
				case MailboxSignatureSectionType.MappingMetadata:
					return this.ParseMailboxMappingMetadata(sectionMetadata, buffer, ref offset);
				case MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.MappingMetadata:
					break;
				case MailboxSignatureSectionType.NamedPropertyMapping:
					return this.ParseMailboxNamedPropertyMapping(sectionMetadata, buffer, ref offset);
				default:
					if (type == MailboxSignatureSectionType.ReplidGuidMapping)
					{
						return this.ParseMailboxReplidGuidMapping(sectionMetadata, buffer, ref offset);
					}
					if (type == MailboxSignatureSectionType.TenantHint)
					{
						return this.ParseMailboxTenantHint(sectionMetadata, buffer, ref offset);
					}
					break;
				}
			}
			else if (type <= MailboxSignatureSectionType.MailboxTypeVersion)
			{
				if (type == MailboxSignatureSectionType.MailboxShape)
				{
					return this.ParseMailboxShape(sectionMetadata, buffer, ref offset);
				}
				if (type == MailboxSignatureSectionType.MailboxTypeVersion)
				{
					return this.ParseMailboxTypeVersion(sectionMetadata, buffer, ref offset);
				}
			}
			else
			{
				if (type == MailboxSignatureSectionType.PartitionInformation)
				{
					return this.ParsePartitionInformation(sectionMetadata, buffer, ref offset);
				}
				if (type == MailboxSignatureSectionType.UserInformation)
				{
					return this.ParseUserInformation(sectionMetadata, buffer, ref offset);
				}
			}
			return this.ParseMailboxUnknownSection(sectionMetadata, buffer, ref offset);
		}

		// Token: 0x060001EF RID: 495
		public abstract bool ParseMailboxBasicInformation(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F0 RID: 496
		public abstract bool ParseMailboxMappingMetadata(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F1 RID: 497
		public abstract bool ParseMailboxNamedPropertyMapping(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F2 RID: 498
		public abstract bool ParseMailboxReplidGuidMapping(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F3 RID: 499
		public abstract bool ParseMailboxTenantHint(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F4 RID: 500
		public abstract bool ParseMailboxShape(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F5 RID: 501
		public abstract bool ParseMailboxTypeVersion(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F6 RID: 502
		public abstract bool ParsePartitionInformation(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F7 RID: 503
		public abstract bool ParseUserInformation(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset);

		// Token: 0x060001F8 RID: 504 RVA: 0x0000FC27 File Offset: 0x0000DE27
		public bool ParseMailboxUnknownSection(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			return false;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000FC34 File Offset: 0x0000DE34
		protected static void Skip(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (offset < 0 || offset > buffer.Length)
			{
				throw new InvalidParameterException((LID)33608U, "Buffer too small.");
			}
			int num = offset + mailboxSignatureSectionMetadata.Length;
			if (num < offset || num > buffer.Length)
			{
				throw new CorruptDataException((LID)49992U, "The new offset is invalid.");
			}
			offset = num;
		}
	}
}
