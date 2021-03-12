using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000315 RID: 789
	internal sealed class RopQueryColumnsAll : InputRop
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x000329A8 File Offset: 0x00030BA8
		internal override RopId RopId
		{
			get
			{
				return RopId.QueryColumnsAll;
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000329AC File Offset: 0x00030BAC
		internal static Rop CreateRop()
		{
			return new RopQueryColumnsAll();
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000329B3 File Offset: 0x00030BB3
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000329BD File Offset: 0x00030BBD
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulQueryColumnsAllResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000329EB File Offset: 0x00030BEB
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopQueryColumnsAll.resultFactory;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000329F2 File Offset: 0x00030BF2
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00032A07 File Offset: 0x00030C07
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.QueryColumnsAll(serverObject, RopQueryColumnsAll.resultFactory);
		}

		// Token: 0x040009F6 RID: 2550
		private const RopId RopType = RopId.QueryColumnsAll;

		// Token: 0x040009F7 RID: 2551
		private static QueryColumnsAllResultFactory resultFactory = new QueryColumnsAllResultFactory();
	}
}
