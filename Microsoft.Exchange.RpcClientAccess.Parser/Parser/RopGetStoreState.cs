using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002DE RID: 734
	internal sealed class RopGetStoreState : InputRop
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0002F7F3 File Offset: 0x0002D9F3
		internal override RopId RopId
		{
			get
			{
				return RopId.GetStoreState;
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0002F7F7 File Offset: 0x0002D9F7
		internal static Rop CreateRop()
		{
			return new RopGetStoreState();
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0002F7FE File Offset: 0x0002D9FE
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0002F808 File Offset: 0x0002DA08
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetStoreStateResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0002F836 File Offset: 0x0002DA36
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetStoreState.resultFactory;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0002F83D File Offset: 0x0002DA3D
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0002F852 File Offset: 0x0002DA52
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetStoreState(serverObject, RopGetStoreState.resultFactory);
		}

		// Token: 0x0400086A RID: 2154
		private const RopId RopType = RopId.GetStoreState;

		// Token: 0x0400086B RID: 2155
		private static GetStoreStateResultFactory resultFactory = new GetStoreStateResultFactory();
	}
}
