using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000282 RID: 642
	internal sealed class SuccessfulReloadCachedInformationResult : RopResult
	{
		// Token: 0x06000DE5 RID: 3557 RVA: 0x00029F88 File Offset: 0x00028188
		internal SuccessfulReloadCachedInformationResult(MessageHeader messageHeader, RecipientCollector recipientCollector) : base(RopId.ReloadCachedInformation, ErrorCode.None, null)
		{
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

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00029FDC File Offset: 0x000281DC
		internal SuccessfulReloadCachedInformationResult(Reader reader, Encoding string8Encoding) : base(reader)
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

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0002A03E File Offset: 0x0002823E
		internal bool HasNamedProperties
		{
			get
			{
				return this.messageHeader.HasNamedProperties;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0002A04B File Offset: 0x0002824B
		internal string SubjectPrefix
		{
			get
			{
				return this.messageHeader.SubjectPrefix;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0002A058 File Offset: 0x00028258
		internal string NormalizedSubject
		{
			get
			{
				return this.messageHeader.NormalizedSubject;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0002A065 File Offset: 0x00028265
		internal ushort MessageRecipientsCount
		{
			get
			{
				return this.messageHeader.MessageRecipientsCount;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0002A072 File Offset: 0x00028272
		internal PropertyTag[] ExtraPropertyTags
		{
			get
			{
				return this.extraPropertyTags;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0002A07A File Offset: 0x0002827A
		internal RecipientRow[] RecipientRows
		{
			get
			{
				return this.recipientRows;
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0002A082 File Offset: 0x00028282
		internal static SuccessfulReloadCachedInformationResult Parse(Reader reader, Encoding string8Encoding)
		{
			return new SuccessfulReloadCachedInformationResult(reader, string8Encoding);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0002A08C File Offset: 0x0002828C
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

		// Token: 0x06000DEF RID: 3567 RVA: 0x0002A0F8 File Offset: 0x000282F8
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

		// Token: 0x04000730 RID: 1840
		private readonly MessageHeader messageHeader;

		// Token: 0x04000731 RID: 1841
		private readonly PropertyTag[] extraPropertyTags;

		// Token: 0x04000732 RID: 1842
		private readonly RecipientRow[] recipientRows;
	}
}
