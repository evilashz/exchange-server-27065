using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000357 RID: 855
	internal sealed class RopTransportDoneWithMessage : InputRop
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00036395 File Offset: 0x00034595
		internal override RopId RopId
		{
			get
			{
				return RopId.TransportDoneWithMessage;
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0003639C File Offset: 0x0003459C
		internal static Rop CreateRop()
		{
			return new RopTransportDoneWithMessage();
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000363A3 File Offset: 0x000345A3
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000363AD File Offset: 0x000345AD
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x000363B7 File Offset: 0x000345B7
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000363E5 File Offset: 0x000345E5
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopTransportDoneWithMessage.resultFactory;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000363EC File Offset: 0x000345EC
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000363F6 File Offset: 0x000345F6
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0003640B File Offset: 0x0003460B
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.TransportDoneWithMessage(serverObject, RopTransportDoneWithMessage.resultFactory);
		}

		// Token: 0x04000AE9 RID: 2793
		private const RopId RopType = RopId.TransportDoneWithMessage;

		// Token: 0x04000AEA RID: 2794
		private static TransportDoneWithMessageResultFactory resultFactory = new TransportDoneWithMessageResultFactory();
	}
}
