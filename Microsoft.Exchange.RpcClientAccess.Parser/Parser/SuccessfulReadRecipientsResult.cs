using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200027F RID: 639
	internal sealed class SuccessfulReadRecipientsResult : RopResult
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00029C8F File Offset: 0x00027E8F
		internal SuccessfulReadRecipientsResult(RecipientCollector recipientCollector) : base(RopId.ReadRecipients, ErrorCode.None, null)
		{
			if (recipientCollector == null)
			{
				throw new ArgumentNullException("recipientCollector");
			}
			this.extraPropertyTags = recipientCollector.ExtraPropertyTags;
			this.recipientRows = recipientCollector.RecipientRows;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00029CC4 File Offset: 0x00027EC4
		internal SuccessfulReadRecipientsResult(Reader reader, PropertyTag[] extraPropertyTags) : base(reader)
		{
			this.extraPropertyTags = extraPropertyTags;
			byte b = reader.ReadByte();
			this.recipientRows = new RecipientRow[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.recipientRows[i] = new RecipientRow(reader, this.extraPropertyTags, RecipientSerializationFlags.RecipientRowId | RecipientSerializationFlags.ExtraUnicodeProperties | RecipientSerializationFlags.CodePageId);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00029D13 File Offset: 0x00027F13
		internal PropertyTag[] ExtraPropertyTags
		{
			get
			{
				return this.extraPropertyTags;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00029D1B File Offset: 0x00027F1B
		internal RecipientRow[] RecipientRows
		{
			get
			{
				return this.recipientRows;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00029D24 File Offset: 0x00027F24
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.recipientRows.Length);
			foreach (RecipientRow recipientRow in this.recipientRows)
			{
				recipientRow.Serialize(writer, this.extraPropertyTags, RecipientSerializationFlags.RecipientRowId | RecipientSerializationFlags.ExtraUnicodeProperties | RecipientSerializationFlags.CodePageId);
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00029D6E File Offset: 0x00027F6E
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Recipients=[");
			Util.AppendToString<RecipientRow>(stringBuilder, this.RecipientRows);
			stringBuilder.Append("]");
		}

		// Token: 0x0400072C RID: 1836
		private readonly PropertyTag[] extraPropertyTags;

		// Token: 0x0400072D RID: 1837
		private readonly RecipientRow[] recipientRows;
	}
}
