using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C0 RID: 704
	internal sealed class RopGetCollapseState : InputRop
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0002DD0A File Offset: 0x0002BF0A
		internal override RopId RopId
		{
			get
			{
				return RopId.GetCollapseState;
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0002DD0E File Offset: 0x0002BF0E
		internal static Rop CreateRop()
		{
			return new RopGetCollapseState();
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0002DD18 File Offset: 0x0002BF18
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" RowId=").Append(this.rowId.ToString());
			stringBuilder.Append(" RowInstanceNumber=").Append(this.rowInstanceNumber);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0002DD65 File Offset: 0x0002BF65
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId rowId, uint rowInstanceNumber)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.rowId = rowId;
			this.rowInstanceNumber = rowInstanceNumber;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002DD7E File Offset: 0x0002BF7E
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.rowId.Serialize(writer);
			writer.WriteUInt32(this.rowInstanceNumber);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0002DDA0 File Offset: 0x0002BFA0
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetCollapseStateResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0002DDCE File Offset: 0x0002BFCE
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetCollapseState.resultFactory;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0002DDD5 File Offset: 0x0002BFD5
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.rowId = StoreId.Parse(reader);
			this.rowInstanceNumber = reader.ReadUInt32();
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0002DDF7 File Offset: 0x0002BFF7
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002DE0C File Offset: 0x0002C00C
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetCollapseState(serverObject, this.rowId, this.rowInstanceNumber, RopGetCollapseState.resultFactory);
		}

		// Token: 0x04000806 RID: 2054
		private const RopId RopType = RopId.GetCollapseState;

		// Token: 0x04000807 RID: 2055
		private static GetCollapseStateResultFactory resultFactory = new GetCollapseStateResultFactory();

		// Token: 0x04000808 RID: 2056
		private StoreId rowId;

		// Token: 0x04000809 RID: 2057
		private uint rowInstanceNumber;
	}
}
