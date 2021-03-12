using System;

namespace Microsoft.Exchange.Data.MailboxSignature
{
	// Token: 0x02000235 RID: 565
	internal class NullMailboxSignatureSectionProcessor : IMailboxSignatureSectionProcessor
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0003BCA7 File Offset: 0x00039EA7
		internal static IMailboxSignatureSectionProcessor Instance
		{
			get
			{
				return NullMailboxSignatureSectionProcessor.instance;
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0003BCAE File Offset: 0x00039EAE
		public bool Process(MailboxSignatureSectionMetadata sectionMetadata, byte[] buffer, ref int offset)
		{
			offset += sectionMetadata.Length;
			return true;
		}

		// Token: 0x04000B70 RID: 2928
		private static IMailboxSignatureSectionProcessor instance = new NullMailboxSignatureSectionProcessor();
	}
}
