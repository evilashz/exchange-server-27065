using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000354 RID: 852
	internal sealed class RopTransportDeliverMessage : RopTransportDeliverMessageBase
	{
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x000362B3 File Offset: 0x000344B3
		internal override RopId RopId
		{
			get
			{
				return RopId.TransportDeliverMessage;
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000362BA File Offset: 0x000344BA
		internal static Rop CreateRop()
		{
			return new RopTransportDeliverMessage();
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x000362C1 File Offset: 0x000344C1
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000362EF File Offset: 0x000344EF
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopTransportDeliverMessage.resultFactory;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000362F6 File Offset: 0x000344F6
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.TransportDeliverMessage(serverObject, this.recipientType, RopTransportDeliverMessage.resultFactory);
		}

		// Token: 0x04000ADC RID: 2780
		private const RopId RopType = RopId.TransportDeliverMessage;

		// Token: 0x04000ADD RID: 2781
		private static TransportDeliverMessageResultFactory resultFactory = new TransportDeliverMessageResultFactory();
	}
}
