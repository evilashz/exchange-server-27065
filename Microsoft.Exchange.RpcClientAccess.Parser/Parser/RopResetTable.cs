using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000327 RID: 807
	internal sealed class RopResetTable : InputRop
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00033EAC File Offset: 0x000320AC
		internal override RopId RopId
		{
			get
			{
				return RopId.ResetTable;
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00033EB3 File Offset: 0x000320B3
		internal static Rop CreateRop()
		{
			return new RopResetTable();
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00033EBA File Offset: 0x000320BA
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00033EC4 File Offset: 0x000320C4
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00033ECE File Offset: 0x000320CE
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00033EFC File Offset: 0x000320FC
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopResetTable.resultFactory;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00033F03 File Offset: 0x00032103
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00033F0D File Offset: 0x0003210D
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00033F22 File Offset: 0x00032122
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ResetTable(serverObject, RopResetTable.resultFactory);
		}

		// Token: 0x04000A43 RID: 2627
		private const RopId RopType = RopId.ResetTable;

		// Token: 0x04000A44 RID: 2628
		private static ResetTableResultFactory resultFactory = new ResetTableResultFactory();
	}
}
