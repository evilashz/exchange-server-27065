using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C1 RID: 705
	internal sealed class RopGetContentsTable : InputOutputRop
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0002DE40 File Offset: 0x0002C040
		internal override RopId RopId
		{
			get
			{
				return RopId.GetContentsTable;
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0002DE43 File Offset: 0x0002C043
		internal static Rop CreateRop()
		{
			return new RopGetContentsTable();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0002DE4A File Offset: 0x0002C04A
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetContentsTableResultFactory(base.PeekReturnServerObjectHandleValue);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0002DE57 File Offset: 0x0002C057
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.tableFlags = (TableFlags)reader.ReadByte();
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0002DE6D File Offset: 0x0002C06D
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0002DE84 File Offset: 0x0002C084
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetContentsTableResultFactory resultFactory = new GetContentsTableResultFactory(base.PeekReturnServerObjectHandleValue);
			this.result = ropHandler.GetContentsTable(serverObject, this.tableFlags, resultFactory);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0002DEB1 File Offset: 0x0002C0B1
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, TableFlags tableFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.tableFlags = tableFlags;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0002DEC4 File Offset: 0x0002C0C4
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.tableFlags);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0002DEDA File Offset: 0x0002C0DA
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetContentsTableResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0002DF08 File Offset: 0x0002C108
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.tableFlags);
		}

		// Token: 0x0400080A RID: 2058
		private const RopId RopType = RopId.GetContentsTable;

		// Token: 0x0400080B RID: 2059
		private TableFlags tableFlags;
	}
}
