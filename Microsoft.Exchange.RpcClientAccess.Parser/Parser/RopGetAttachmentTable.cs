using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002BF RID: 703
	internal sealed class RopGetAttachmentTable : InputOutputRop
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0002DC14 File Offset: 0x0002BE14
		internal override RopId RopId
		{
			get
			{
				return RopId.GetAttachmentTable;
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0002DC18 File Offset: 0x0002BE18
		internal static Rop CreateRop()
		{
			return new RopGetAttachmentTable();
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002DC1F File Offset: 0x0002BE1F
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, TableFlags tableFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.tableFlags = tableFlags;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002DC32 File Offset: 0x0002BE32
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.tableFlags);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0002DC48 File Offset: 0x0002BE48
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetAttachmentTableResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0002DC76 File Offset: 0x0002BE76
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetAttachmentTableResultFactory(base.PeekReturnServerObjectHandleValue);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002DC83 File Offset: 0x0002BE83
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.tableFlags = (TableFlags)reader.ReadByte();
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0002DC99 File Offset: 0x0002BE99
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002DCB0 File Offset: 0x0002BEB0
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetAttachmentTableResultFactory resultFactory = new GetAttachmentTableResultFactory(base.PeekReturnServerObjectHandleValue);
			this.result = ropHandler.GetAttachmentTable(serverObject, this.tableFlags, resultFactory);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0002DCDD File Offset: 0x0002BEDD
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.tableFlags);
		}

		// Token: 0x04000804 RID: 2052
		private const RopId RopType = RopId.GetAttachmentTable;

		// Token: 0x04000805 RID: 2053
		private TableFlags tableFlags;
	}
}
