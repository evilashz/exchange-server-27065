using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D2 RID: 722
	internal sealed class RopGetPropertyList : InputRop
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0002ED42 File Offset: 0x0002CF42
		internal override RopId RopId
		{
			get
			{
				return RopId.GetPropertyList;
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0002ED46 File Offset: 0x0002CF46
		internal static Rop CreateRop()
		{
			return new RopGetPropertyList();
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0002ED4D File Offset: 0x0002CF4D
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0002ED57 File Offset: 0x0002CF57
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetPropertyListResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0002ED85 File Offset: 0x0002CF85
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetPropertyList.resultFactory;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0002ED8C File Offset: 0x0002CF8C
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0002EDA1 File Offset: 0x0002CFA1
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetPropertyList(serverObject, RopGetPropertyList.resultFactory);
		}

		// Token: 0x04000838 RID: 2104
		private const RopId RopType = RopId.GetPropertyList;

		// Token: 0x04000839 RID: 2105
		private static GetPropertyListResultFactory resultFactory = new GetPropertyListResultFactory();
	}
}
