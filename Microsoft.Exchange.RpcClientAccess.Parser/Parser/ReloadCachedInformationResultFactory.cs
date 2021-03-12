using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000117 RID: 279
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ReloadCachedInformationResultFactory : MessageHeaderResultFactory
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x0001072B File Offset: 0x0000E92B
		internal ReloadCachedInformationResultFactory(int maxSize) : base(RopId.ReloadCachedInformation)
		{
			this.maxSize = maxSize;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001073C File Offset: 0x0000E93C
		public override RecipientCollector CreateRecipientCollector(MessageHeader messageHeader, PropertyTag[] extraPropertyTags, Encoding string8Encoding)
		{
			this.messageHeader = messageHeader;
			int num = 0;
			using (Writer writer = new CountWriter())
			{
				this.messageHeader.Serialize(writer, string8Encoding);
				writer.WriteCountAndPropertyTagArray(extraPropertyTags, FieldLength.WordSize);
				writer.WriteByte(0);
				num = (int)writer.Position;
			}
			if (num > this.maxSize)
			{
				throw new BufferTooSmallException();
			}
			return new RecipientCollector(this.maxSize - num - 6, extraPropertyTags, RecipientSerializationFlags.ExtraUnicodeProperties | RecipientSerializationFlags.CodePageId);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000107B8 File Offset: 0x0000E9B8
		public RopResult CreateSuccessfulResult(RecipientCollector recipientCollector)
		{
			return new SuccessfulReloadCachedInformationResult(this.messageHeader, recipientCollector);
		}

		// Token: 0x0400031A RID: 794
		private readonly int maxSize;

		// Token: 0x0400031B RID: 795
		private MessageHeader messageHeader;
	}
}
