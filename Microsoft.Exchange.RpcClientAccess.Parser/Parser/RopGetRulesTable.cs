using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D8 RID: 728
	internal sealed class RopGetRulesTable : InputOutputRop
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0002F364 File Offset: 0x0002D564
		internal override RopId RopId
		{
			get
			{
				return RopId.GetRulesTable;
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0002F368 File Offset: 0x0002D568
		internal static Rop CreateRop()
		{
			return new RopGetRulesTable();
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0002F36F File Offset: 0x0002D56F
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, TableFlags tableFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.tableFlags = tableFlags;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0002F382 File Offset: 0x0002D582
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.tableFlags);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0002F398 File Offset: 0x0002D598
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetRulesTableResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0002F3C6 File Offset: 0x0002D5C6
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetRulesTableResultFactory(base.PeekReturnServerObjectHandleValue);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0002F3D3 File Offset: 0x0002D5D3
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.tableFlags = (TableFlags)reader.ReadByte();
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0002F3E9 File Offset: 0x0002D5E9
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0002F400 File Offset: 0x0002D600
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetRulesTableResultFactory resultFactory = new GetRulesTableResultFactory(base.PeekReturnServerObjectHandleValue);
			this.result = ropHandler.GetRulesTable(serverObject, this.tableFlags, resultFactory);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0002F42D File Offset: 0x0002D62D
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.tableFlags);
		}

		// Token: 0x04000847 RID: 2119
		private const RopId RopType = RopId.GetRulesTable;

		// Token: 0x04000848 RID: 2120
		private TableFlags tableFlags;
	}
}
