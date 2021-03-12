using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002B9 RID: 697
	internal sealed class RopFindRow : InputRop
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0002D221 File Offset: 0x0002B421
		internal override RopId RopId
		{
			get
			{
				return RopId.FindRow;
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0002D225 File Offset: 0x0002B425
		internal static Rop CreateRop()
		{
			return new RopFindRow();
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0002D22C File Offset: 0x0002B42C
		internal void SetInput(byte logonIndex, byte handleTableIndex, FindRowFlags flags, Restriction restriction, BookmarkOrigin bookmarkOrigin, byte[] bookmark)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.flags = flags;
			this.restriction = restriction;
			this.bookmarkOrigin = bookmarkOrigin;
			this.bookmark = bookmark;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0002D258 File Offset: 0x0002B458
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.flags);
			writer.WriteSizedRestriction(this.restriction, string8Encoding, WireFormatStyle.Rop);
			writer.WriteByte((byte)this.bookmarkOrigin);
			if (this.bookmark == null)
			{
				writer.WriteUInt16(0);
				return;
			}
			writer.WriteUInt16((ushort)this.bookmark.Length);
			writer.WriteBytes(this.bookmark);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0002D2BE File Offset: 0x0002B4BE
		internal void SetParseOutputData(PropertyTag[] columns)
		{
			Util.ThrowOnNullArgument(columns, "columns");
			this.columns = columns;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0002D2F4 File Offset: 0x0002B4F4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			if (this.columns == null)
			{
				throw new InvalidOperationException("SetParseOutputData must be called before ParseOutput.");
			}
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => new SuccessfulFindRowResult(readerParameter, this.columns, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002D359 File Offset: 0x0002B559
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new FindRowResultFactory(outputBuffer.Count, connection.String8Encoding);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002D370 File Offset: 0x0002B570
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = (FindRowFlags)reader.ReadByte();
			this.restriction = reader.ReadSizeAndRestriction(WireFormatStyle.Rop);
			this.bookmarkOrigin = (BookmarkOrigin)reader.ReadByte();
			uint num = (uint)reader.ReadUInt16();
			if (num > 0U)
			{
				this.bookmark = reader.ReadBytes(num);
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0002D3C2 File Offset: 0x0002B5C2
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002D3D7 File Offset: 0x0002B5D7
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			if (this.restriction != null)
			{
				this.restriction.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0002D3F4 File Offset: 0x0002B5F4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			FindRowResultFactory resultFactory = new FindRowResultFactory(outputBuffer.Count, serverObject.String8Encoding);
			this.result = ropHandler.FindRow(serverObject, this.flags, this.restriction, this.bookmarkOrigin, this.bookmark, resultFactory);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0002D43C File Offset: 0x0002B63C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" Origin=").Append(this.bookmarkOrigin);
			if (this.bookmark != null)
			{
				stringBuilder.Append(" Bookmark=[");
				Util.AppendToString(stringBuilder, this.bookmark);
				stringBuilder.Append("]");
			}
			if (this.restriction != null)
			{
				stringBuilder.Append(" Restriction=[");
				this.restriction.AppendToString(stringBuilder);
				stringBuilder.Append("]");
			}
		}

		// Token: 0x040007EE RID: 2030
		private const RopId RopType = RopId.FindRow;

		// Token: 0x040007EF RID: 2031
		private FindRowFlags flags;

		// Token: 0x040007F0 RID: 2032
		private Restriction restriction;

		// Token: 0x040007F1 RID: 2033
		private BookmarkOrigin bookmarkOrigin;

		// Token: 0x040007F2 RID: 2034
		private byte[] bookmark;

		// Token: 0x040007F3 RID: 2035
		private PropertyTag[] columns;
	}
}
