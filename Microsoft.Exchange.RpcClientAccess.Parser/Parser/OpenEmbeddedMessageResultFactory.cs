using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000105 RID: 261
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OpenEmbeddedMessageResultFactory : MessageHeaderResultFactory
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
		internal OpenEmbeddedMessageResultFactory(int maxSize) : base(RopId.OpenEmbeddedMessage)
		{
			this.maxSize = maxSize;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		public override RecipientCollector CreateRecipientCollector(MessageHeader messageHeader, PropertyTag[] extraPropertyTags, Encoding string8Encoding)
		{
			this.messageHeader = messageHeader;
			int num = 0;
			using (Writer writer = new CountWriter())
			{
				StoreId storeId = default(StoreId);
				writer.WriteBool(false);
				storeId.Serialize(writer);
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

		// Token: 0x06000549 RID: 1353 RVA: 0x0000FD58 File Offset: 0x0000DF58
		public RopResult CreateSuccessfulResult(IServerObject serverObject, StoreId messageId, RecipientCollector recipientCollector)
		{
			return new SuccessfulOpenEmbeddedMessageResult(serverObject, false, messageId, this.messageHeader, recipientCollector);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000FD69 File Offset: 0x0000DF69
		public RopResult CreateSuccessfulResult(IServerObject serverObject, StoreId messageId)
		{
			return new SuccessfulOpenEmbeddedMessageResult(serverObject, true, messageId, null, null);
		}

		// Token: 0x0400030A RID: 778
		private readonly int maxSize;

		// Token: 0x0400030B RID: 779
		private MessageHeader messageHeader;
	}
}
