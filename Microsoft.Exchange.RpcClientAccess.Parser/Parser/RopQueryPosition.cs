using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000318 RID: 792
	internal sealed class RopQueryPosition : InputRop
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00032B52 File Offset: 0x00030D52
		internal override RopId RopId
		{
			get
			{
				return RopId.QueryPosition;
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00032B56 File Offset: 0x00030D56
		internal static Rop CreateRop()
		{
			return new RopQueryPosition();
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00032B5D File Offset: 0x00030D5D
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00032B67 File Offset: 0x00030D67
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00032B71 File Offset: 0x00030D71
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulQueryPositionResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00032B9F File Offset: 0x00030D9F
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopQueryPosition.resultFactory;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00032BA6 File Offset: 0x00030DA6
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00032BB0 File Offset: 0x00030DB0
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00032BC5 File Offset: 0x00030DC5
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.QueryPosition(serverObject, RopQueryPosition.resultFactory);
		}

		// Token: 0x04000A00 RID: 2560
		private const RopId RopType = RopId.QueryPosition;

		// Token: 0x04000A01 RID: 2561
		private static QueryPositionResultFactory resultFactory = new QueryPositionResultFactory();
	}
}
