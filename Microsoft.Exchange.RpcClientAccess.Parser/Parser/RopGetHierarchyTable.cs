using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C6 RID: 710
	internal sealed class RopGetHierarchyTable : InputOutputRop
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0002E208 File Offset: 0x0002C408
		internal override RopId RopId
		{
			get
			{
				return RopId.GetHierarchyTable;
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0002E20B File Offset: 0x0002C40B
		internal static Rop CreateRop()
		{
			return new RopGetHierarchyTable();
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0002E212 File Offset: 0x0002C412
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, TableFlags tableFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.tableFlags = tableFlags;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0002E225 File Offset: 0x0002C425
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.tableFlags);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0002E23B File Offset: 0x0002C43B
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetHierarchyTableResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0002E269 File Offset: 0x0002C469
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetHierarchyTableResultFactory(base.PeekReturnServerObjectHandleValue);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0002E276 File Offset: 0x0002C476
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.tableFlags = (TableFlags)reader.ReadByte();
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0002E28C File Offset: 0x0002C48C
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0002E2A4 File Offset: 0x0002C4A4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetHierarchyTableResultFactory resultFactory = new GetHierarchyTableResultFactory(base.PeekReturnServerObjectHandleValue);
			this.result = ropHandler.GetHierarchyTable(serverObject, this.tableFlags, resultFactory);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0002E2D1 File Offset: 0x0002C4D1
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.tableFlags);
		}

		// Token: 0x04000814 RID: 2068
		private const RopId RopType = RopId.GetHierarchyTable;

		// Token: 0x04000815 RID: 2069
		private TableFlags tableFlags;
	}
}
