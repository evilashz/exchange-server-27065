using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000107 RID: 263
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OpenMessageResultFactory : MessageHeaderResultFactory
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x0000FD88 File Offset: 0x0000DF88
		internal OpenMessageResultFactory(int maxSize) : base(RopId.OpenMessage)
		{
			this.maxSize = maxSize;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000FD98 File Offset: 0x0000DF98
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

		// Token: 0x0600054F RID: 1359 RVA: 0x0000FE14 File Offset: 0x0000E014
		public RopResult CreateSuccessfulResult(IServerObject serverObject, RecipientCollector recipientCollector)
		{
			return new SuccessfulOpenMessageResult(serverObject, this.messageHeader, recipientCollector);
		}

		// Token: 0x0400030C RID: 780
		private readonly int maxSize;

		// Token: 0x0400030D RID: 781
		private MessageHeader messageHeader;
	}
}
