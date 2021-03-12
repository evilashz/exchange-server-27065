using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002BB RID: 699
	internal sealed class RopFlushRecipients : InputRop
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0002D6E2 File Offset: 0x0002B8E2
		internal override RopId RopId
		{
			get
			{
				return RopId.FlushRecipients;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0002D6E6 File Offset: 0x0002B8E6
		internal PropertyTag[] ExtraPropertyTags
		{
			get
			{
				return this.extraPropertyTags;
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0002D6EE File Offset: 0x0002B8EE
		internal static Rop CreateRop()
		{
			return new RopFlushRecipients();
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0002D6F5 File Offset: 0x0002B8F5
		internal void SetInput(byte logonIndex, byte handleTableIndex, PropertyTag[] extraPropertyTags, RecipientRow[] recipientRows)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.extraPropertyTags = extraPropertyTags;
			this.recipientRows = recipientRows;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0002D710 File Offset: 0x0002B910
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountAndPropertyTagArray(this.extraPropertyTags, FieldLength.WordSize);
			writer.WriteUInt16((ushort)this.recipientRows.Length);
			foreach (RecipientRow recipientRow in this.recipientRows)
			{
				recipientRow.Serialize(writer, this.extraPropertyTags, RecipientSerializationFlags.RecipientRowId, string8Encoding);
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0002D769 File Offset: 0x0002B969
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0002D797 File Offset: 0x0002B997
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFlushRecipients.resultFactory;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0002D7A0 File Offset: 0x0002B9A0
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.extraPropertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
			ushort num = reader.ReadUInt16();
			this.recipientRows = new RecipientRow[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				this.recipientRows[i] = new RecipientRow(reader, this.extraPropertyTags, RecipientSerializationFlags.RecipientRowId);
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0002D7F6 File Offset: 0x0002B9F6
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002D80C File Offset: 0x0002BA0C
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			for (int i = 0; i < this.recipientRows.Length; i++)
			{
				this.recipientRows[i].ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0002D841 File Offset: 0x0002BA41
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FlushRecipients(serverObject, this.extraPropertyTags, this.recipientRows, RopFlushRecipients.resultFactory);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0002D864 File Offset: 0x0002BA64
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" ExtraTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.extraPropertyTags);
			stringBuilder.Append("]");
			stringBuilder.Append(" Recipients=[");
			Util.AppendToString<RecipientRow>(stringBuilder, this.recipientRows);
			stringBuilder.Append("]");
		}

		// Token: 0x040007F9 RID: 2041
		private const RopId RopType = RopId.FlushRecipients;

		// Token: 0x040007FA RID: 2042
		private static FlushRecipientsResultFactory resultFactory = new FlushRecipientsResultFactory();

		// Token: 0x040007FB RID: 2043
		private PropertyTag[] extraPropertyTags = Array<PropertyTag>.Empty;

		// Token: 0x040007FC RID: 2044
		private RecipientRow[] recipientRows;
	}
}
