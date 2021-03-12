using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000351 RID: 849
	internal sealed class RopSynchronizationOpenAdvisor : InputOutputRop
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00036065 File Offset: 0x00034265
		internal override RopId RopId
		{
			get
			{
				return RopId.SynchronizationOpenAdvisor;
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0003606C File Offset: 0x0003426C
		internal static Rop CreateRop()
		{
			return new RopSynchronizationOpenAdvisor();
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00036073 File Offset: 0x00034273
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0003607E File Offset: 0x0003427E
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSynchronizationOpenAdvisorResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x000360AC File Offset: 0x000342AC
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSynchronizationOpenAdvisor.resultFactory;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x000360B3 File Offset: 0x000342B3
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000360C8 File Offset: 0x000342C8
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SynchronizationOpenAdvisor(serverObject, RopSynchronizationOpenAdvisor.resultFactory);
		}

		// Token: 0x04000AD4 RID: 2772
		private const RopId RopType = RopId.SynchronizationOpenAdvisor;

		// Token: 0x04000AD5 RID: 2773
		private static SynchronizationOpenAdvisorResultFactory resultFactory = new SynchronizationOpenAdvisorResultFactory();
	}
}
