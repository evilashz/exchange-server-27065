using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.MailboxSignature;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000151 RID: 337
	internal static class MailboxSignatureConverter
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		public static byte[] ConvertTenantHint(byte[] originalSignature, MailboxSignatureFlags originalSignatureFlags, TenantPartitionHint tenantHint)
		{
			MailboxSignatureSectionsContainer mailboxSignatureSectionsContainer;
			if (originalSignatureFlags == MailboxSignatureFlags.GetLegacy)
			{
				mailboxSignatureSectionsContainer = MailboxSignatureSectionsContainer.Create(MailboxSignatureSectionType.BasicInformation, new MailboxSignatureConverter.MailboxBasicInformationSectionCreator(originalSignature));
			}
			else
			{
				mailboxSignatureSectionsContainer = MailboxSignatureSectionsContainer.Parse(originalSignature, NullMailboxSignatureSectionProcessor.Instance);
			}
			if (tenantHint == null)
			{
				tenantHint = TenantPartitionHint.FromOrganizationId(OrganizationId.ForestWideOrgId);
			}
			byte[] persistablePartitionHint = tenantHint.GetPersistablePartitionHint();
			int num = TenantHintHelper.SerializeTenantHint(persistablePartitionHint, null, 0);
			byte[] array = new byte[num];
			TenantHintHelper.SerializeTenantHint(persistablePartitionHint, array, 0);
			mailboxSignatureSectionsContainer.UpdateSection(new MailboxSignatureSectionMetadata(MailboxSignatureSectionType.TenantHint, 1, 1, persistablePartitionHint.Length), array);
			return mailboxSignatureSectionsContainer.Serialize();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001AB2C File Offset: 0x00018D2C
		public static byte[] ConvertPartitionInformation(byte[] originalSignature, MailboxSignatureFlags originalSignatureFlags, PartitionInformation partitionInformation)
		{
			MailboxSignatureSectionsContainer mailboxSignatureSectionsContainer;
			if (originalSignatureFlags == MailboxSignatureFlags.GetLegacy)
			{
				mailboxSignatureSectionsContainer = MailboxSignatureSectionsContainer.Create(MailboxSignatureSectionType.BasicInformation, new MailboxSignatureConverter.MailboxBasicInformationSectionCreator(originalSignature));
			}
			else
			{
				mailboxSignatureSectionsContainer = MailboxSignatureSectionsContainer.Parse(originalSignature, NullMailboxSignatureSectionProcessor.Instance);
			}
			int num = partitionInformation.Serialize(null, 0);
			byte[] array = new byte[num];
			partitionInformation.Serialize(array, 0);
			mailboxSignatureSectionsContainer.UpdateSection(new MailboxSignatureSectionMetadata(MailboxSignatureSectionType.PartitionInformation, 1, 1, array.Length), array);
			return mailboxSignatureSectionsContainer.Serialize();
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0001AB90 File Offset: 0x00018D90
		public static byte[] ExtractMailboxBasicInfo(byte[] signatureBlob)
		{
			MailboxSignatureConverter.MailboxBasicInformationExtractor mailboxBasicInformationExtractor = new MailboxSignatureConverter.MailboxBasicInformationExtractor();
			MailboxSignatureSectionsContainer.Parse(signatureBlob, mailboxBasicInformationExtractor);
			return mailboxBasicInformationExtractor.MailboxBasicInformation;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		public static byte[] CreatePublicFolderMailboxBasicInformation()
		{
			int num = 32 + MailboxSignatureConverter.defaultPublicFolderGlobCounts.Length;
			byte[] array = new byte[num];
			Buffer.BlockCopy(Guid.NewGuid().ToByteArray(), 0, array, 0, 16);
			Buffer.BlockCopy(Guid.NewGuid().ToByteArray(), 0, array, 16, 16);
			Buffer.BlockCopy(MailboxSignatureConverter.defaultPublicFolderGlobCounts, 0, array, 32, MailboxSignatureConverter.defaultPublicFolderGlobCounts.Length);
			return array;
		}

		// Token: 0x040006B6 RID: 1718
		private static byte[] defaultPublicFolderGlobCounts = new byte[]
		{
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			4,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			3,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			8,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			7,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			5,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			6,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			1
		};

		// Token: 0x02000152 RID: 338
		private class MailboxBasicInformationSectionCreator : IMailboxSignatureSectionCreator
		{
			// Token: 0x06000BC8 RID: 3016 RVA: 0x0001AC99 File Offset: 0x00018E99
			public MailboxBasicInformationSectionCreator(byte[] mailboxBasicInformation)
			{
				this.mailboxBasicInformation = mailboxBasicInformation;
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x0001ACA8 File Offset: 0x00018EA8
			bool IMailboxSignatureSectionCreator.Create(MailboxSignatureSectionType sectionType, out MailboxSignatureSectionMetadata sectionMetadata, out byte[] sectionData)
			{
				sectionMetadata = new MailboxSignatureSectionMetadata(MailboxSignatureSectionType.BasicInformation, 1, 1, this.mailboxBasicInformation.Length);
				sectionData = this.mailboxBasicInformation;
				return true;
			}

			// Token: 0x040006B7 RID: 1719
			private readonly byte[] mailboxBasicInformation;
		}

		// Token: 0x02000153 RID: 339
		private class MailboxBasicInformationExtractor : IMailboxSignatureSectionProcessor
		{
			// Token: 0x1700036F RID: 879
			// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0001ACC5 File Offset: 0x00018EC5
			// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0001ACCD File Offset: 0x00018ECD
			public byte[] MailboxBasicInformation { get; private set; }

			// Token: 0x06000BCC RID: 3020 RVA: 0x0001ACD6 File Offset: 0x00018ED6
			bool IMailboxSignatureSectionProcessor.Process(MailboxSignatureSectionMetadata sectionMetadata, byte[] buffer, ref int offset)
			{
				if (sectionMetadata.Type == MailboxSignatureSectionType.BasicInformation)
				{
					this.MailboxBasicInformation = new byte[sectionMetadata.Length];
					Array.Copy(buffer, offset, this.MailboxBasicInformation, 0, sectionMetadata.Length);
				}
				offset += sectionMetadata.Length;
				return true;
			}
		}
	}
}
