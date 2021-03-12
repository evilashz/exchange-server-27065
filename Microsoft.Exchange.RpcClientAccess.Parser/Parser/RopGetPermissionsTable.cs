using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002CE RID: 718
	internal sealed class RopGetPermissionsTable : InputOutputRop
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0002E938 File Offset: 0x0002CB38
		internal override RopId RopId
		{
			get
			{
				return RopId.GetPermissionsTable;
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0002E93C File Offset: 0x0002CB3C
		internal static Rop CreateRop()
		{
			return new RopGetPermissionsTable();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0002E943 File Offset: 0x0002CB43
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, TableFlags tableFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.tableFlags = tableFlags;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0002E956 File Offset: 0x0002CB56
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.tableFlags);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0002E96C File Offset: 0x0002CB6C
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetPermissionsTableResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0002E99A File Offset: 0x0002CB9A
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetPermissionsTableResultFactory(base.PeekReturnServerObjectHandleValue);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002E9A7 File Offset: 0x0002CBA7
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.tableFlags = (TableFlags)reader.ReadByte();
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002E9BD File Offset: 0x0002CBBD
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetPermissionsTableResultFactory resultFactory = new GetPermissionsTableResultFactory(base.PeekReturnServerObjectHandleValue);
			this.result = ropHandler.GetPermissionsTable(serverObject, this.tableFlags, resultFactory);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0002EA01 File Offset: 0x0002CC01
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.tableFlags);
		}

		// Token: 0x0400082D RID: 2093
		private const RopId RopType = RopId.GetPermissionsTable;

		// Token: 0x0400082E RID: 2094
		private TableFlags tableFlags;
	}
}
