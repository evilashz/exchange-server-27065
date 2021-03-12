using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000275 RID: 629
	internal sealed class SuccessfulOpenEmbeddedMessageResult : RopResult
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x00029158 File Offset: 0x00027358
		internal SuccessfulOpenEmbeddedMessageResult(IServerObject serverObject, bool isNew, StoreId messageId, MessageHeader messageHeader, RecipientCollector recipientCollector) : base(RopId.OpenEmbeddedMessage, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			if (isNew)
			{
				if (messageHeader != null)
				{
					throw new ArgumentException("messageHeader should be null for new messages", "messageHeader");
				}
				if (recipientCollector != null)
				{
					throw new ArgumentException("recipientCollector should be null for new messages", "recipientCollector");
				}
			}
			else
			{
				if (messageHeader == null)
				{
					throw new ArgumentNullException("messageHeader");
				}
				if (recipientCollector == null)
				{
					throw new ArgumentNullException("recipientCollector");
				}
			}
			this.isNew = isNew;
			this.messageId = messageId;
			this.messageHeader = messageHeader;
			if (recipientCollector != null)
			{
				this.extraPropertyTags = recipientCollector.ExtraPropertyTags;
				this.recipientRows = recipientCollector.RecipientRows;
				return;
			}
			this.extraPropertyTags = null;
			this.recipientRows = null;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002920C File Offset: 0x0002740C
		internal SuccessfulOpenEmbeddedMessageResult(Reader reader, Encoding string8Encoding) : base(reader)
		{
			this.isNew = reader.ReadBool();
			this.messageId = StoreId.Parse(reader);
			if (!this.isNew)
			{
				this.messageHeader = new MessageHeader(reader, string8Encoding);
				this.extraPropertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
				byte b = reader.ReadByte();
				this.recipientRows = new RecipientRow[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					this.recipientRows[i] = new RecipientRow(reader, this.extraPropertyTags, RecipientSerializationFlags.ExtraUnicodeProperties | RecipientSerializationFlags.CodePageId);
				}
				return;
			}
			this.messageHeader = null;
			this.extraPropertyTags = null;
			this.recipientRows = null;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000292A4 File Offset: 0x000274A4
		internal bool HasNamedProperties
		{
			get
			{
				return this.messageHeader.HasNamedProperties;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x000292B1 File Offset: 0x000274B1
		internal string SubjectPrefix
		{
			get
			{
				return this.messageHeader.SubjectPrefix;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000292BE File Offset: 0x000274BE
		internal string NormalizedSubject
		{
			get
			{
				return this.messageHeader.NormalizedSubject;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x000292CB File Offset: 0x000274CB
		internal ushort MessageRecipientsCount
		{
			get
			{
				return this.messageHeader.MessageRecipientsCount;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000292D8 File Offset: 0x000274D8
		internal PropertyTag[] ExtraPropertyTags
		{
			get
			{
				return this.extraPropertyTags;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x000292E0 File Offset: 0x000274E0
		internal RecipientRow[] RecipientRows
		{
			get
			{
				return this.recipientRows;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000292E8 File Offset: 0x000274E8
		internal bool IsNew
		{
			get
			{
				return this.isNew;
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000292F0 File Offset: 0x000274F0
		internal static SuccessfulOpenEmbeddedMessageResult Parse(Reader reader, Encoding string8Encoding)
		{
			return new SuccessfulOpenEmbeddedMessageResult(reader, string8Encoding);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000292FC File Offset: 0x000274FC
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.isNew, 1);
			this.messageId.Serialize(writer);
			if (!this.isNew)
			{
				this.messageHeader.Serialize(writer, base.String8Encoding);
				writer.WriteCountAndPropertyTagArray(this.extraPropertyTags, FieldLength.WordSize);
				writer.WriteByte((byte)this.recipientRows.Length);
				foreach (RecipientRow recipientRow in this.recipientRows)
				{
					recipientRow.Serialize(writer, this.extraPropertyTags, RecipientSerializationFlags.ExtraUnicodeProperties | RecipientSerializationFlags.CodePageId);
				}
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002938C File Offset: 0x0002758C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
			if (this.messageHeader != null)
			{
				stringBuilder.Append(" Header=");
				this.messageHeader.AppendToString(stringBuilder);
			}
			stringBuilder.Append(" ExtraTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.extraPropertyTags);
			stringBuilder.Append("]");
			stringBuilder.Append(" Recipients=[");
			Util.AppendToString<RecipientRow>(stringBuilder, this.recipientRows);
			stringBuilder.Append("]");
		}

		// Token: 0x04000717 RID: 1815
		private readonly bool isNew;

		// Token: 0x04000718 RID: 1816
		private readonly StoreId messageId;

		// Token: 0x04000719 RID: 1817
		private readonly MessageHeader messageHeader;

		// Token: 0x0400071A RID: 1818
		private readonly PropertyTag[] extraPropertyTags;

		// Token: 0x0400071B RID: 1819
		private readonly RecipientRow[] recipientRows;
	}
}
