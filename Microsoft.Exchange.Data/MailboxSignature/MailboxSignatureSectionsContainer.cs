using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Data.MailboxSignature
{
	// Token: 0x02000233 RID: 563
	internal class MailboxSignatureSectionsContainer
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0003B8B4 File Offset: 0x00039AB4
		public MailboxSignatureSectionType SectionTypes
		{
			get
			{
				MailboxSignatureSectionType mailboxSignatureSectionType = MailboxSignatureSectionType.None;
				foreach (MailboxSignatureSectionsContainer.SignatureSection signatureSection in this.sections)
				{
					mailboxSignatureSectionType |= signatureSection.Metadata.Type;
				}
				return mailboxSignatureSectionType;
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0003B914 File Offset: 0x00039B14
		public static MailboxSignatureSectionsContainer Parse(byte[] buffer, IMailboxSignatureSectionProcessor sectionProcessor)
		{
			MailboxSignatureSectionsContainer mailboxSignatureSectionsContainer = new MailboxSignatureSectionsContainer();
			mailboxSignatureSectionsContainer.InternalParse(buffer, sectionProcessor);
			return mailboxSignatureSectionsContainer;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0003B930 File Offset: 0x00039B30
		public static MailboxSignatureSectionsContainer Create(MailboxSignatureSectionType sectionsToCreate, IMailboxSignatureSectionCreator sectionCreator)
		{
			MailboxSignatureSectionsContainer mailboxSignatureSectionsContainer = new MailboxSignatureSectionsContainer();
			mailboxSignatureSectionsContainer.InternalCreate(sectionsToCreate, sectionCreator);
			return mailboxSignatureSectionsContainer;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0003B94C File Offset: 0x00039B4C
		public byte[] Serialize()
		{
			int num = 0;
			foreach (MailboxSignatureSectionsContainer.SignatureSection signatureSection in this.sections)
			{
				num += 12 + signatureSection.Data.Count;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			foreach (MailboxSignatureSectionsContainer.SignatureSection signatureSection2 in this.sections)
			{
				num2 += signatureSection2.Metadata.Serialize(array, num2);
				Array.Copy(signatureSection2.Data.Array, signatureSection2.Data.Offset, array, num2, signatureSection2.Data.Count);
				num2 += signatureSection2.Data.Count;
			}
			return array;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0003BA74 File Offset: 0x00039C74
		public MailboxSignatureSectionsContainer.SignatureSection GetSignatureSection(MailboxSignatureSectionType type)
		{
			int num = this.sections.FindIndex((MailboxSignatureSectionsContainer.SignatureSection e) => e.Metadata.Type == type);
			if (num != -1)
			{
				return this.sections.ElementAt(num);
			}
			return new MailboxSignatureSectionsContainer.SignatureSection(new MailboxSignatureSectionMetadata(MailboxSignatureSectionType.None, 0, 0, 0), default(ArraySegment<byte>));
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0003BAF4 File Offset: 0x00039CF4
		public void UpdateSection(MailboxSignatureSectionMetadata metadata, byte[] data)
		{
			int num = this.sections.FindIndex((MailboxSignatureSectionsContainer.SignatureSection e) => e.Metadata.Type == metadata.Type);
			if (num != -1)
			{
				this.sections.RemoveAt(num);
			}
			this.sections.Add(new MailboxSignatureSectionsContainer.SignatureSection(metadata, new ArraySegment<byte>(data)));
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0003BB60 File Offset: 0x00039D60
		private void InternalParse(byte[] buffer, IMailboxSignatureSectionProcessor sectionProcessor)
		{
			int i = 0;
			while (i < buffer.Length)
			{
				MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata = MailboxSignatureSectionMetadata.Parse(buffer, ref i);
				if (mailboxSignatureSectionMetadata.Length > buffer.Length - i)
				{
					throw new ArgumentException("Metadata declares length past our buffer end.");
				}
				int num = i;
				bool flag = sectionProcessor.Process(mailboxSignatureSectionMetadata, buffer, ref i);
				if (i - num != mailboxSignatureSectionMetadata.Length)
				{
					throw new ArgumentException("Parsed more data than declared in metadata.");
				}
				if (flag)
				{
					if ((from section in this.sections
					select section.Metadata.Type).Contains(mailboxSignatureSectionMetadata.Type))
					{
						throw new ArgumentException("Same section appears more than once.");
					}
					this.sections.Add(new MailboxSignatureSectionsContainer.SignatureSection(mailboxSignatureSectionMetadata, new ArraySegment<byte>(buffer, num, mailboxSignatureSectionMetadata.Length)));
				}
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0003BC24 File Offset: 0x00039E24
		private void InternalCreate(MailboxSignatureSectionType sectionsToCreate, IMailboxSignatureSectionCreator sectionCreator)
		{
			for (MailboxSignatureSectionType mailboxSignatureSectionType = MailboxSignatureSectionType.BasicInformation; mailboxSignatureSectionType != MailboxSignatureSectionType.None; mailboxSignatureSectionType <<= 1)
			{
				MailboxSignatureSectionMetadata metadata;
				byte[] array;
				if (sectionsToCreate.HasFlag(mailboxSignatureSectionType) && sectionCreator.Create(mailboxSignatureSectionType, out metadata, out array))
				{
					this.sections.Add(new MailboxSignatureSectionsContainer.SignatureSection(metadata, new ArraySegment<byte>(array)));
				}
			}
		}

		// Token: 0x04000B69 RID: 2921
		public const uint UnifiedSignatureFormat = 102U;

		// Token: 0x04000B6A RID: 2922
		public const uint XSOSpecificMRSStoreProtocol = 103U;

		// Token: 0x04000B6B RID: 2923
		public const uint IncrementalMailboxSignatureMappingMetadataMRSStoreProtocol = 104U;

		// Token: 0x04000B6C RID: 2924
		private List<MailboxSignatureSectionsContainer.SignatureSection> sections = new List<MailboxSignatureSectionsContainer.SignatureSection>();

		// Token: 0x02000234 RID: 564
		internal struct SignatureSection
		{
			// Token: 0x06001381 RID: 4993 RVA: 0x0003BC87 File Offset: 0x00039E87
			internal SignatureSection(MailboxSignatureSectionMetadata metadata, ArraySegment<byte> data)
			{
				this.metadata = metadata;
				this.data = data;
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06001382 RID: 4994 RVA: 0x0003BC97 File Offset: 0x00039E97
			public MailboxSignatureSectionMetadata Metadata
			{
				get
				{
					return this.metadata;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06001383 RID: 4995 RVA: 0x0003BC9F File Offset: 0x00039E9F
			public ArraySegment<byte> Data
			{
				get
				{
					return this.data;
				}
			}

			// Token: 0x04000B6E RID: 2926
			private MailboxSignatureSectionMetadata metadata;

			// Token: 0x04000B6F RID: 2927
			private ArraySegment<byte> data;
		}
	}
}
