using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000355 RID: 853
	internal sealed class RopTransportDeliverMessage2 : RopTransportDeliverMessageBase
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00036324 File Offset: 0x00034524
		internal override RopId RopId
		{
			get
			{
				return RopId.TransportDeliverMessage2;
			}
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0003632B File Offset: 0x0003452B
		internal static Rop CreateRop()
		{
			return new RopTransportDeliverMessage2();
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00036332 File Offset: 0x00034532
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulTransportDeliverMessage2Result.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00036360 File Offset: 0x00034560
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopTransportDeliverMessage2.resultFactory;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00036367 File Offset: 0x00034567
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.TransportDeliverMessage2(serverObject, this.recipientType, RopTransportDeliverMessage2.resultFactory);
		}

		// Token: 0x04000ADE RID: 2782
		private const RopId RopType = RopId.TransportDeliverMessage2;

		// Token: 0x04000ADF RID: 2783
		private static TransportDeliverMessage2ResultFactory resultFactory = new TransportDeliverMessage2ResultFactory();
	}
}
