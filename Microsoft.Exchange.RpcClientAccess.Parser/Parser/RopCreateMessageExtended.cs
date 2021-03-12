using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000295 RID: 661
	internal sealed class RopCreateMessageExtended : InputOutputRop
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0002B380 File Offset: 0x00029580
		internal override RopId RopId
		{
			get
			{
				return RopId.CreateMessageExtended;
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0002B387 File Offset: 0x00029587
		internal static Rop CreateRop()
		{
			return new RopCreateMessageExtended();
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0002B38E File Offset: 0x0002958E
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, ushort codePageId, StoreId folderId, CreateMessageExtendedFlags createFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.codePageId = codePageId;
			this.folderId = folderId;
			this.createFlags = createFlags;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002B3B1 File Offset: 0x000295B1
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.codePageId);
			this.folderId.Serialize(writer);
			writer.WriteByte(0);
			writer.WriteUInt32((uint)this.createFlags);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0002B3E6 File Offset: 0x000295E6
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulCreateMessageExtendedResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002B414 File Offset: 0x00029614
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopCreateMessageExtended.resultFactory;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002B41B File Offset: 0x0002961B
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.codePageId = reader.ReadUInt16();
			this.folderId = StoreId.Parse(reader);
			reader.ReadByte();
			this.createFlags = (CreateMessageExtendedFlags)reader.ReadUInt32();
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002B450 File Offset: 0x00029650
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002B465 File Offset: 0x00029665
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.CreateMessageExtended(serverObject, this.codePageId, this.folderId, this.createFlags, RopCreateMessageExtended.resultFactory);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0002B48C File Offset: 0x0002968C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" CPID=").Append(this.codePageId);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
			stringBuilder.Append(" CreateFlags=").Append(this.createFlags);
		}

		// Token: 0x04000774 RID: 1908
		private const RopId RopType = RopId.CreateMessageExtended;

		// Token: 0x04000775 RID: 1909
		private static CreateMessageExtendedResultFactory resultFactory = new CreateMessageExtendedResultFactory();

		// Token: 0x04000776 RID: 1910
		private ushort codePageId;

		// Token: 0x04000777 RID: 1911
		private StoreId folderId;

		// Token: 0x04000778 RID: 1912
		private CreateMessageExtendedFlags createFlags;
	}
}
