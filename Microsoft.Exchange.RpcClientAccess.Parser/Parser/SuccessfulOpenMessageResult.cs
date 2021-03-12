using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000277 RID: 631
	internal sealed class SuccessfulOpenMessageResult : RopResult
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x00029580 File Offset: 0x00027780
		internal SuccessfulOpenMessageResult(IServerObject serverObject, MessageHeader messageHeader, RecipientCollector recipientCollector) : base(RopId.OpenMessage, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			if (messageHeader == null)
			{
				throw new ArgumentNullException("messageHeader");
			}
			if (recipientCollector == null)
			{
				throw new ArgumentNullException("recipientCollector");
			}
			this.messageHeader = messageHeader;
			this.extraPropertyTags = recipientCollector.ExtraPropertyTags;
			this.recipientRows = recipientCollector.RecipientRows;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000295E0 File Offset: 0x000277E0
		internal SuccessfulOpenMessageResult(Reader reader, Encoding string8Encoding) : base(reader)
		{
			this.messageHeader = new MessageHeader(reader, string8Encoding);
			this.extraPropertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
			byte b = reader.ReadByte();
			this.recipientRows = new RecipientRow[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.recipientRows[i] = new RecipientRow(reader, this.extraPropertyTags, RecipientSerializationFlags.ExtraUnicodeProperties | RecipientSerializationFlags.CodePageId);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00029642 File Offset: 0x00027842
		internal bool HasNamedProperties
		{
			get
			{
				return this.messageHeader.HasNamedProperties;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0002964F File Offset: 0x0002784F
		internal string SubjectPrefix
		{
			get
			{
				return this.messageHeader.SubjectPrefix;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0002965C File Offset: 0x0002785C
		internal string NormalizedSubject
		{
			get
			{
				return this.messageHeader.NormalizedSubject;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00029669 File Offset: 0x00027869
		internal ushort MessageRecipientsCount
		{
			get
			{
				return this.messageHeader.MessageRecipientsCount;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00029676 File Offset: 0x00027876
		internal PropertyTag[] ExtraPropertyTags
		{
			get
			{
				return this.extraPropertyTags;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0002967E File Offset: 0x0002787E
		internal RecipientRow[] RecipientRows
		{
			get
			{
				return this.recipientRows;
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00029686 File Offset: 0x00027886
		internal static SuccessfulOpenMessageResult Parse(Reader reader, Encoding string8Encoding)
		{
			return new SuccessfulOpenMessageResult(reader, string8Encoding);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00029690 File Offset: 0x00027890
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.messageHeader.Serialize(writer, base.String8Encoding);
			writer.WriteCountAndPropertyTagArray(this.extraPropertyTags, FieldLength.WordSize);
			writer.WriteByte((byte)this.recipientRows.Length);
			foreach (RecipientRow recipientRow in this.recipientRows)
			{
				recipientRow.Serialize(writer, this.extraPropertyTags, RecipientSerializationFlags.ExtraUnicodeProperties | RecipientSerializationFlags.CodePageId);
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000296FC File Offset: 0x000278FC
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
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

		// Token: 0x0400071E RID: 1822
		private readonly MessageHeader messageHeader;

		// Token: 0x0400071F RID: 1823
		private readonly PropertyTag[] extraPropertyTags;

		// Token: 0x04000720 RID: 1824
		private readonly RecipientRow[] recipientRows;
	}
}
